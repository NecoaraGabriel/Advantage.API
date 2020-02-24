using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ModelsDbContex _contex;

        public CustomerController(ModelsDbContex contex)
        {
            _contex = contex;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _contex.Customers.OrderBy(c => c.Id);

            return Ok(data);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = _contex.Customers.FirstOrDefault(X => X.Id == id);

            if(customer != null)
            {
                return Ok(customer);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if(customer == null)
            {
                return BadRequest();
            }

            _contex.Customers.Add(customer);
            _contex.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }
    }
}