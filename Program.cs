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
            AzureServiceBusClient azureClient = new AzureServiceBusClient();

            
            while (true)
            {
                await azureClient.Peek();
            }
            //ReadPort readPort1 = new ReadPort();
            //ReadPort readPort2 = new ReadPort();

            //Thread thread = new Thread(readPort1.Listen);
            //Thread thread2 = new Thread(readPort2.Listen);

            //thread.Start(25000);
            //thread2.Start(25001);
            //PayloadAnalyzer p = new PayloadAnalyzer();

            //var type = p.GetType("");



        }
    }
}
