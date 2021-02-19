using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace ConsoleAdministration
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var Url = "Endpoint=sb://demoarquitectura.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=snDulmLzY6zsyZtd/1pDAnHaK3MDBS4MxjdIweTes9U=";
            var cola = "demo";
            await using (ServiceBusClient client = new ServiceBusClient(Url))
            {
                ServiceBusProcessor processor = client.CreateProcessor(cola, new ServiceBusProcessorOptions());
                processor.ProcessMessageAsync += Processor_ProcessMessageAsync;
                processor.ProcessErrorAsync += Processor_ProcessErrorAsync;

                await processor.StartProcessingAsync();
                Console.ReadLine();
                await processor.StartProcessingAsync();
            }
        }

        private static async Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            Console.WriteLine($"error: {arg.Exception.Message}");
            await Task.CompletedTask;
        }

        private static async System.Threading.Tasks.Task Processor_ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            string body = arg.Message.Body.ToString();
            Console.WriteLine($"Mensaje del servicio : {body}");
            await arg.CompleteMessageAsync(arg.Message);
        }
    }
}
