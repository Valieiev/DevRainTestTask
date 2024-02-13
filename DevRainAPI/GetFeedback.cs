using DevRainAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DevRainAPI
{
    public class GetFeedback
    {
        private readonly ILogger<GetFeedback> _logger;
        private readonly DevRainDBContext _dbContext;

        public GetFeedback(ILogger<GetFeedback> logger, DevRainDBContext devRainDBContext)
        {
            _logger = logger;
            _dbContext = devRainDBContext;
        }

        [Function("GetFeedbacks")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            try
            {
                IQueryable<Feedback> queryableFeedbacks = _dbContext.Feedbacks.AsQueryable();

                if (!string.IsNullOrEmpty(req.Query["startDate"]))
                {
                    DateOnly startDateFilter = DateOnly.ParseExact(req.Query["startDate"], "dd/MM/yyyy");
                    queryableFeedbacks = queryableFeedbacks.Where(feedback => feedback.CreatedDate >= startDateFilter);
                }

                if (!string.IsNullOrEmpty(req.Query["endDate"]))
                {
                    DateOnly endDateFilter = DateOnly.ParseExact(req.Query["startDate"], "dd/MM/yyyy");
                    queryableFeedbacks = queryableFeedbacks.Where(feedback => feedback.CreatedDate <= endDateFilter);
                }


                if (!string.IsNullOrEmpty(req.Query["top"]))
                {
                    if (int.TryParse(req.Query["top"], out _))
                    {
                        queryableFeedbacks = queryableFeedbacks.OrderByDescending(feedback => feedback.PositiveScore).Take(int.Parse(req.Query["top"]));
                    }

                }

                List<Feedback> feedbacks = await queryableFeedbacks.OrderByDescending(x => x.PositiveScore).ToListAsync();

                return new OkObjectResult(feedbacks);
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
