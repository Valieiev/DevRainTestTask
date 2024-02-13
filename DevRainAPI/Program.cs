using DevRainAPI.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<DevRainDBContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("DevRainDB", EnvironmentVariableTarget.Process), options => options.EnableRetryOnFailure());
        });
    })
    .Build();

host.Run();