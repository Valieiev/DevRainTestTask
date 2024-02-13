using DevRain.DevRainBlazor;
using DevRain.DevRainBlazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Azure.Functions.Authentication.WebAssembly;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddStaticWebAppsAuthentication();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<FeedbackService>();

await builder.Build().RunAsync();
