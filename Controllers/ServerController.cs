using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Controllers.DataModels;
using Advantage.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : Controller
    {
        private readonly ModelsDbContex _contex;

        public ServerController(ModelsDbContex contex)
        {
            _contex = contex;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var servers = _contex.Servers.OrderBy(x=>x.Id);
            return Ok(servers);
        }

        [HttpGet("{id}", Name ="GetServer")]
        public IActionResult Get(int id)
        {
            var server = _contex.Servers.Find(id);
            if(server != null)
            {
                return Ok(server);
            }

            return NotFound("Server not found.");
        }

        //[HttpPut]
        public IActionResult Put([FromBody] ServerMessage serverMessage)
        {
            if (serverMessage.Id == 0) return BadRequest();
            var server = _contex.Servers.Find(serverMessage.Id);
            if (server == null)
            {
                return NotFound("Server not found.");
            }
           
            if(serverMessage.Message == "activate")
            {
                server.IsOnline = true;
                _contex.SaveChanges();
            }
            else if(serverMessage.Message == "deactivate")
            {
                server.IsOnline = false;
                _contex.SaveChanges();
            }

            return new NoContentResult(); //204
        }
    }
}