using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvantageData.API.Controllers.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdvantageData.Models;


namespace AdvantageData.API.Controllers
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
            //try
            //{
               
            //}
            //catch
            //{
            //    return BadRequest();
            //}

            if (serverMessage?.Id == 0)
                return BadRequest();
            var server = _contex.Servers.Find(serverMessage.Id);
            if (server == null)
            {
                return NotFound("Server not found.");
            }
           
            if(serverMessage.Message == "activate")
            {
                server.IsOnline = true;
                server.Status = "Online";
                _contex.SaveChanges();
            }
            else if(serverMessage.Message == "deactivate")
            {
                server.IsOnline = false;
                server.Status = "Offline";
                _contex.SaveChanges();
            }

            return new NoContentResult(); //204
        }
    }
}