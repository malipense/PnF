using System;
using System.Threading;
using System.Threading.Tasks;
using P_n_F.Core;
using P_n_F.ServiceBus;

namespace P_n_F
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string message;
            AzureServiceBusClient serviceBusClient = new AzureServiceBusClient();
            HL7Converter hl7Converter = new HL7Converter();

            while (true)
            {
                await serviceBusClient.Peek();
                if (!string.IsNullOrEmpty(serviceBusClient.Message))
                    message = hl7Converter.Convert(serviceBusClient.Message);
            }
        }
        static void Process()
        {

        }
    }
}
