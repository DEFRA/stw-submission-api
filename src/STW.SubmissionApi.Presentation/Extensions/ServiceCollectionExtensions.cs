namespace STW.SubmissionApi.Presentation.Extensions;

using Microsoft.Extensions.Azure;
using Options;
using Services;

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
