using DevRainAPI.Models;
using DevRainAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;

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
        [Authorize]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "secured/GetFeedbacks")] HttpRequest req, ClaimsPrincipal clientPrincipal)
        {

            if (clientPrincipal == null || !clientPrincipal.Identity.IsAuthenticated) {

                return new BadRequestObjectResult(JsonConvert.SerializeObject(StaticWebAppsAuth.Parse(req)));
            }
            try
            {
                IQueryable<Feedback> queryableFeedbacks = _dbContext.Feedbacks.AsQueryable();

                if (!string.IsNullOrEmpty(req.Query["startDate"]))
                {
                    DateTime startDateFilter = DateTime.Parse(req.Query["startDate"]);
                    queryableFeedbacks = queryableFeedbacks.Where(feedback => feedback.CreatedDate >= startDateFilter);
                }

                if (!string.IsNullOrEmpty(req.Query["endDate"]))
                {
                    DateTime endDateFilter = DateTime.Parse(req.Query["endDate"]);
                    queryableFeedbacks = queryableFeedbacks.Where(feedback => feedback.CreatedDate <= endDateFilter.AddHours(24));
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
