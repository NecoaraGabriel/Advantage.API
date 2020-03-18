using AdvantageData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvantageData.IRepository
{
    public interface IOrder
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
    }
}
