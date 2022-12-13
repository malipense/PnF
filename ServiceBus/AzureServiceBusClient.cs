using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace P_n_F.ServiceBus
{
    internal class AzureServiceBusClient
    {
        private const string connectionString = "";
        private const string queueName = "message-queue";
        private ServiceBusClient client;
        private ServiceBusProcessor busProcessor;
        public AzureServiceBusClient()
        {
            var options = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient(connectionString, options);

            busProcessor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions()
            {   });

            busProcessor.ProcessMessageAsync += MessageHandler;
            busProcessor.ProcessErrorAsync += ErrorHandler;
        }
        public async Task Peek()
        {
            await busProcessor.StartProcessingAsync();
            await Task.Delay(2000);
            await busProcessor.StopProcessingAsync();
        }
        public async void Dispose()
        {
            await busProcessor.DisposeAsync();
            await client.DisposeAsync();
        }
        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            await args.CompleteMessageAsync(args.Message);
        }
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
