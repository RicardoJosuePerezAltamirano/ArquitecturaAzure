using Common;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Threading.Tasks;

namespace ConsoleExternalConfiguration
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=demosarquitectura;AccountKey=Thx03NdE0OrQmROkh3WCSuLGihnEOjTiyN9JHKIpfOpJT3vjAeNDsv53M8yqrKE0vnoqLNnPc4W7M8p6gRC9Xg==;EndpointSuffix=core.windows.net";
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            var table = cloudTableClient.GetTableReference("Configuraciones");
            //foreach(var table in cloudTableClient.ListTables())
            //{
            //    Console.WriteLine(table.Name);
            //}
            Configuraciones config = new Configuraciones("Mode", "AppService");
            config.Description = "describe el modo de la aplicacion";
            config.Value = "Normal";
            //TableOperation insert = TableOperation.InsertOrMerge(config);
            //var Result = await table.ExecuteAsync(insert);
            //if (Result.RequestCharge.HasValue)
            //{
            //    Console.WriteLine($"RequestCharge {Result.RequestCharge}");
            //}
            TableOperation Get = TableOperation.Retrieve<Configuraciones>("Mode", "AppService");
            TableResult table1 = await table.ExecuteAsync(Get);
            Configuraciones response = table1.Result as Configuraciones;
            Console.WriteLine($"configuracion desde Table Storage {response.Key} - {response.Description} {response.Value}");
            
        }
    }
}
