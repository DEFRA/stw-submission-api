using Microsoft.Extensions.Azure;
using STW.SubmissionApi.Presentation.Options;
using STW.SubmissionApi.Presentation.Services;

namespace STW.SubmissionApi.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMessagingService, MessagingService>();
        serviceCollection.AddScoped<IValidationService, ValidationService>();
    }

    public static void RegisterOptions(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<ServiceBusOptions>(configuration);
    }

    public static void RegisterAzureServiceBus(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAzureClients(
            builder =>
            {
                builder.AddServiceBusClient(configuration.GetValue<string>("ServiceBusConnectionString"));
            });
    }
}
