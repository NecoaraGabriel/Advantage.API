using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Advantage.API.Controllers.DataModels;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ModelsDbContex _contex;

        public OrderController(ModelsDbContex contex)
        {
            _contex = contex;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _contex.Orders.Include(x => x.Customer).OrderBy(o => o.Id);

            return Ok(data);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var order = _contex.Orders.Include(x => x.Customer).FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                return Ok(order);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (order != null)
            {
                _contex.Orders.Add(order);
                _contex.SaveChanges();

                return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
            }

            return BadRequest("Body is not properly set.");
        }

        [HttpGet("GetPage")]
        public IActionResult GetPage(Pagination pagination)
        {
            int pageIndex = pagination.IndexPage;
            int pageSize = pagination.PageSize;

            var data = _contex.Orders.Include(x => x.Customer).OrderBy(x => x.Id);
            var page = new PaginationResponse<Order>(data, pageIndex, pageSize);

            var totalPages = Math.Ceiling((double)data.Count() / pageSize);

            if(totalPages > 1 && pageIndex <= totalPages )
            {
                var response = new
                {
                    Page = page,
                    TotalCount = totalPages
                };
                return Ok(response);
            }

            return BadRequest("Index out of range");
        }
    }
}