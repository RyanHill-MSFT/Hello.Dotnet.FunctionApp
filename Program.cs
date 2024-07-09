using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((context, builder) =>
    {
        var managedIdentityId = context.Configuration["ManagedIdentityId"];
        var tokenCredential = string.IsNullOrEmpty(managedIdentityId)
        ? new DefaultAzureCredential()
        : new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = managedIdentityId });

        builder.SetBasePath(context.HostingEnvironment.ContentRootPath)
               .AddEnvironmentVariables()/*
               .AddAzureAppConfiguration(options =>
               {
                   options.Connect(new Uri(context.Configuration["AppConfigurationEndpoint"].ToString()), tokenCredential);
               })*/;
    })
    .ConfigureLogging((builder, logging) =>
    {
        logging.AddConsole();
    })
    .ConfigureServices(services =>
    {
        services.AddLogging()
                .AddApplicationInsightsTelemetryWorkerService()
                .ConfigureFunctionsApplicationInsights();
    })
    .Build()
    .Run();