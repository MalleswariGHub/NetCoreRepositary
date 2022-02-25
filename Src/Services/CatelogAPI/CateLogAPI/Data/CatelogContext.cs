using CateLogAPI.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CateLogAPI.Data
{
    public class CatelogContext : ICatelogContext
    {
        public CatelogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSetting:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSetting:DatabaseName"));

            Products = (ICatelogContext<Product>)database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSetting:CollectionName"));
        }
        public ICatelogContext<Product> Products { get; }
            }
}
