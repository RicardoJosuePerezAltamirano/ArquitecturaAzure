using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace WebAppCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertsController : ControllerBase
    {
        Context Context;
        private static IConfiguration config;
        public ConcertsController(Context context
            ,IConfiguration configuration)
        {
            Context = context;
            config = configuration;
        }
        
        [HttpPost]
        [Route("add")]
        public async Task<Concerts> Add(Concerts concerts)
        {
            var data=await Context.AddAsync<Concerts>(concerts);
            await Context.SaveChangesAsync();
            return data.Entity;

        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Concerts>> GetConcerts()
        {
            List<Concerts> concerts = new List<Concerts>();
            IDatabase Cache = Connection.GetDatabase();
            var value=Cache.StringGet(new RedisKey("Concerts"));
            if(value.HasValue)
            {
                concerts = JsonSerializer.Deserialize<List<Concerts>>(value.ToString());
                System.Diagnostics.Debug.WriteLine("+++++++USO DE CACHE+++++++");
            }
            else
            {
                var data = await Context.Concerts.ToListAsync();
                if(data!=null)
                {
                    string valuestring = JsonSerializer.Serialize(data);
                    Cache.StringSet(new RedisKey("Concerts"), new RedisValue(valuestring), TimeSpan.FromSeconds(300));
                    concerts = data;
                }
                System.Diagnostics.Debug.WriteLine("-------NO USO DE CACHE-------");
            }
            return concerts;
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = config.GetValue<string>("CacheConnection"); //ConfigurationManager.AppSettings["CacheConnection"].ToString();
            return ConnectionMultiplexer.Connect(cacheConnection);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

    }
}
