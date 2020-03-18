using AdvantageData.IRepository;
using AdvantageData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdvantageData.Repository
{
    public class OrderService : IOrder
    {
        private ModelsDbContex _contex;

        public OrderService(ModelsDbContex contex)
        {
            _contex = contex;
        }

        public IEnumerable<Order> GetAll()
        {
            return _contex.Orders
                .Include(x => x.Customer)
                .OrderBy(o => o.Id);
        }

        public Order GetById(int id)
        {
            return _contex.Orders
                .Include(x => x.Customer)
                .FirstOrDefault(x => x.Id == id);
        }
       
    }
}
