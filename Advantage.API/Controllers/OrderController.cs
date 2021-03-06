﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using AdvantageData.API.Controllers.DataModels;
using AdvantageData.Models;

namespace AdvantageData.API.Controllers
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
            var data = _contex.Orders.Include(x => x.Customer).OrderBy(o => o.Placed);

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

        //HttpGet("GetPage")]
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        //public IActionResult GetPage(Pagination pagination)
        public IActionResult GetPage(int pageIndex, int pageSize)
        {
            //int pageIndex = pagination.IndexPage;
            //int pageSize = pagination.PageSize;

            var data = _contex.Orders.Include(x => x.Customer).OrderBy(x => x.Id);
            var page = new PaginationResponse<Order>(data, pageIndex, pageSize);

            var totalPages = Math.Ceiling((double)data.Count() / pageSize);

            if(totalPages > 1 && pageIndex <= totalPages && pageIndex > 0)
            {
                var response = new
                {
                    Page = page,
                    TotalPages = totalPages
                };
                return Ok(response);
            }

            return BadRequest("Index out of range");
        }

        [HttpGet("ByCustomer")]
        public IActionResult ByCustomer()
        {
            var orders = _contex.Orders.Include(o => o.Customer).ToList();
            var groupedResults = orders.GroupBy(x => x.Customer.Id)
                .ToList()
                .Select(grep => new {
                    Customer = _contex.Customers.Find(grep.Key),
                    Total = grep.Sum(x => x.Amount)
                })
                .OrderByDescending(res => res.Total)
                .ToList();

            return Ok(groupedResults);
        }

        [HttpGet("ByCustomer/{take}")]
        public IActionResult ByCustomer(int take)
        {
            var orders = _contex.Orders.Include(o => o.Customer).ToList();
            var groupedResults = orders.GroupBy(x => x.Customer.Id)
                .ToList()
                .Select(grep => new {
                    Customer = _contex.Customers.Find(grep.Key),
                    Total = grep.Sum(x => x.Amount)
                })
                .OrderByDescending(res => res.Total)
                .Take(take)
                .ToList();

            return Ok(groupedResults);
        }

        [HttpGet("ByDate/{startDt:long}/{endDt:long}")]
        public IActionResult ByDate(long startDt, long endDt)
        {
            var orders = _contex.Orders.Include(o => o.Customer)
                .Where(x => x.Placed >= startDt && x.Fulfilled < endDt)
                .OrderByDescending(x => x.Placed)
                .ToList();
            return Ok(orders);
        }
    }
}