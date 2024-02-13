using DevRainAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevRainAPI
{
    public class GetTopFeedbacks
    {
        private readonly ILogger<GetTopFeedbacks> _logger;
        private readonly DevRainDBContext _dbContext;

        public GetTopFeedbacks(ILogger<GetTopFeedbacks> logger, DevRainDBContext devRainDBContext)
        {
            _logger = logger;
            _dbContext = devRainDBContext;
        }

        [Function("GetTopFeedbacks")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetTopFeedbacks")] HttpRequest req)
        {
            try
            {
                
                List<Feedback> feedbacks = await _dbContext.Feedbacks.OrderByDescending(x => x.PositiveScore).Take(3).ToListAsync();

                return new OkObjectResult(feedbacks.Select(x=> new DevRain.Shared.FeedbackClientDataModel { FullName= x.FullName, Company = x.Company, Text= x.Text }));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
