using AdvantageData.IRepository;
using AdvantageData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdvantageData.Repository
{
    public class CustomerService : ICustomer
    {
        private readonly ModelsDbContex _contex;

        public CustomerService(ModelsDbContex contex)
        {
            _contex = contex;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _contex.Customers.OrderBy(x=>x.Id);
        }

        public Customer GetById(int id)
        {
            return _contex.Customers.FirstOrDefault(x => x.Id == id);
        }

        public void AddCustomer(Customer customer)
        {
            _contex.Customers.Add(customer);
            _contex.SaveChanges();
        }
    }
}
