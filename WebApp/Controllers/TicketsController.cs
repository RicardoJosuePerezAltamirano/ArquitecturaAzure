using Azure.Messaging.ServiceBus;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IConfiguration config;
        public TicketsController(IConfiguration _config)
        {
            config = _config;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Ticket ticket)
        {
            var Url = config.GetValue<string>("ConnectionStringBus");
            var cola = config.GetValue<string>("cola");
            await using(ServiceBusClient client=new ServiceBusClient(Url))
            {
                ServiceBusSender sender = client.CreateSender(cola);
                ServiceBusMessage message = new ServiceBusMessage($"Usuario {ticket.Email} registrado");
                await sender.SendMessageAsync(message);
            }
            return Ok("usuario registrado");
        }
    }
}
