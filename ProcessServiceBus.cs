using Azure.Messaging.ServiceBus;
using DnsClient.Internal;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Hello.Dotnet.FunctionApp
{
    public class ProcessServiceBus
    {
        private readonly ILogger<ProcessServiceBus> _logger;

        public ProcessServiceBus(ILogger<ProcessServiceBus> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ProcessServiceBus))]
        public async Task Run(
            [ServiceBusTrigger("my-queue", Connection = "ServiceBusConnection")] ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("ID: {id}\r\nBody: {body}\r\nContent-Type: {contentType}", message.MessageId, message.Body, message.ContentType);

            await messageActions.CompleteMessageAsync(message);
        }
    }
}
