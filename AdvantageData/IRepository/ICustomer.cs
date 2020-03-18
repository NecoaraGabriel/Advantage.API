using AdvantageData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvantageData.IRepository
{
    public interface ICustomer
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        void AddCustomer(Customer customer);
    }
}
