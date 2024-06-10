namespace STW.SubmissionApi.Presentation.Services;

using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Options;

public class MessagingService : IMessagingService
{
    private readonly ServiceBusSender _serviceBusSender;

    public MessagingService(ServiceBusClient serviceBusClient, IOptions<ServiceBusOptions> options)
    {
        _serviceBusSender = serviceBusClient.CreateSender(
            options.Value.ServiceBusQueueName);
    }

    public async Task SendMessageAsync(string message)
    {
        ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message);
        await _serviceBusSender.SendMessageAsync(serviceBusMessage);
    }
}
