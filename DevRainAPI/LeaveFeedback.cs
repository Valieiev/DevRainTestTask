using DevRainAPI.Models;
using DevRainAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DevRainAPI
{
    public class LeaveFeedback
    {
        private readonly ILogger<LeaveFeedback> _logger;
        private readonly DevRainDBContext _dbContext;

        public LeaveFeedback(ILogger<LeaveFeedback> logger, DevRainDBContext devRainDBContext)
        {
            _logger = logger;
            _dbContext = devRainDBContext;
        }

        [Function("LeaveFeedback")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Feedback feedback = JsonConvert.DeserializeObject<Feedback>(requestBody);

                ValidationContext context = new ValidationContext(feedback, serviceProvider: null, items: null);
                List<ValidationResult> results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(feedback, context, results);

                if(!isValid)
                {
                    String errors = "400 Bad Request:";
                    foreach (var validationResult in results)
                    {
                        errors += ("\n" +validationResult.ErrorMessage);
                    }

                    return new BadRequestObjectResult(errors);
                } 

                //According to the latest information received, 
                //Azure DB does not support the NewID() function on the INSERT statement; 
                feedback.Id = Guid.NewGuid();
                feedback.CreatedDate = DateTime.Now;

                //Fill sentiment analysis data
                await TextAnalyticsService.SentimentAnalysis(feedback);

                await _dbContext.Feedbacks.AddAsync(feedback);
                await _dbContext.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
