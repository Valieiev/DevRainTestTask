using DevRainAPI.Models;
using DevRainAPI.Services;
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
        private readonly DevRainDBContext _dbContext;

        public GetFeedback( DevRainDBContext devRainDBContext)
        {
            _dbContext = devRainDBContext;
        }

        [Function("GetFeedbacks")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "secured/GetFeedbacks")] HttpRequest req)
        {
            try
            {


                ClaimsPrincipal clientPrincipal = StaticWebAppsAuth.Parse(req);

                if (!clientPrincipal.IsInRole("admin"))
                {
                    return new UnauthorizedObjectResult($"Hi {clientPrincipal.Identity.Name}. You are not authorized to access this function.");
                }

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
