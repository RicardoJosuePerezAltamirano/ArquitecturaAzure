using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StagePlacesController : ControllerBase
    {
        IHttpClientFactory HttpClientFactory;
        IConfiguration Config;
        ConfigurationServerApp ConfigurationServer;
        public StagePlacesController(IHttpClientFactory httpClient,IConfiguration configuration, ConfigurationServerApp serverApp)
        {
            HttpClientFactory = httpClient;
            Config = configuration;
            ConfigurationServer = serverApp;
        }
        [HttpGet]
        [Route("available")]
        public async Task<List<Place>> GetAvailablePlaces()
        {
           
            var Client = HttpClientFactory.CreateClient("Resources");
            
            return await Client.GetFromJsonAsync<List<Place>>("api/places/Available");
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
