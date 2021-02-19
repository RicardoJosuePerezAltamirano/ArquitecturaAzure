using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Configuraciones:TableEntity
    {
        public Configuraciones()
        {

        }
        public Configuraciones(string key,string app)
        {
            PartitionKey = key;
            RowKey = app;
            
        }
        public string Key { get; set; }
        public string  Value { get; set; }
        public string Description { get; set; }

    }
}
