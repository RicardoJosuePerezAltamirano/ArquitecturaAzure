using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiResources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        IConfiguration Config;
        
        ConfigurationServerApp ConfigurationServer;
        public PlacesController(IConfiguration configuration,  ConfigurationServerApp serverApp)
        {
            Config = configuration;
            
            ConfigurationServer = serverApp;
        }

        [HttpGet]
        [Route("Available")]
        public List<Place> GetAvailable()
        {
            return GeneratePlaces();
        }
        private List<Place> GeneratePlaces()
        {
            List<Place> places = new List<Place>();
            List<string> roms = new List<string>()
            {
                "Principal",
                "Platinum",
                "Gold",
                "Secundary",
                "Normal"
            };
            for(int i=0;i<100;i++)
            {
                places.Add(new Place
                {
                    Id = new Random().Next(1, 10000),
                    Column = new Random().Next(1, 28).ToString(),
                    Row = new Random().Next(1, 8).ToString(),
                    Room = roms[new Random().Next(0, 4)]
                });

            }
            return places;
        }
        [HttpGet]
        [Route("configuration")]
        public async Task<Configuraciones> GetConfig()
        {
            var conf = Config.GetValue<string>("ConnectionStorage");
            var dataconfig = await ConfigurationServer.GetConfig(conf);
            return dataconfig;
        }
    }
}
