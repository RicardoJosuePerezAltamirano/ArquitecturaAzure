using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConfigurationServerApp
    {
        public async Task<Configuraciones> GetConfig(string ConnectionString)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConnectionString);
            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            var table = cloudTableClient.GetTableReference("Configuraciones");
            //foreach(var table in cloudTableClient.ListTables())
            //{
            //    Console.WriteLine(table.Name);
            //}
            //Configuraciones config = new Configuraciones("Mode", "AppService");
            //config.Description = "describe el modo de la aplicacion";
            //config.Value = "Normal";
            //TableOperation insert = TableOperation.InsertOrMerge(config);
            //var Result = await table.ExecuteAsync(insert);
            //if (Result.RequestCharge.HasValue)
            //{
            //    Console.WriteLine($"RequestCharge {Result.RequestCharge}");
            //}
            TableOperation Get = TableOperation.Retrieve<Configuraciones>("Mode", "AppService");
            TableResult table1 = await table.ExecuteAsync(Get);
            Configuraciones response = table1.Result as Configuraciones;
            return response;
        }
    }
}
