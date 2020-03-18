using AdvantageData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AdvantageData.API;

namespace AdvantageData.API
{
    public class DataSeed
    {
        private readonly ModelsDbContex _contex;

        public DataSeed(ModelsDbContex contex)
        {
            _contex = contex;
        }

        public void SeedData(int nCustomers, int nOrders)
        {
            if(!_contex.Customers.Any())
            {
                SeedCustomers(nCustomers);
                _contex.SaveChanges();
            }

            if (!_contex.Orders.Any())
            {
                SeedOrders(nOrders);
                _contex.SaveChanges();
            }

            if (!_contex.Servers.Any())
            {
                SeedServers();
                _contex.SaveChanges();
            }

        }

        private void SeedServers()
        {
            List<Server> servers = BuildServerList();

            foreach(var server in servers)
            {
                _contex.Servers.Add(server);
            }
        }

        private List<Server> BuildServerList()
        {
            return new List<Server>() {
                new Server
                {
                    Id = 1,
                    Name = "Dev-Web",
                    IsOnline = true,
                    Status = "Online"
                },
                new Server
                {
                    Id = 2,
                    Name = "Dev-Mail",
                    IsOnline = false,
                    Status = "Offline"
                },
                new Server
                {
                    Id = 3,
                    Name = "Dev-Service",
                    IsOnline = true,
                    Status = "Online"
                },
                new Server
                {
                    Id = 4,
                    Name = "Prod-Web",
                    IsOnline = true,
                    Status = "Online"
                },
                new Server
                {
                    Id = 5,
                    Name = "Prod-Mail",
                    IsOnline = false,
                    Status = "Offline"
                },
                new Server
                {
                    Id = 6,
                    Name = "Prod-Service",
                    IsOnline = true,
                    Status = "Online"
                }
            };
        }

        private void SeedOrders(int nOrders)
        {
            List<Order> orders = BuildOrdersList(nOrders);

            foreach (var order in orders)
            {
                _contex.Orders.Add(order);
            }
        }

        private List<Order> BuildOrdersList(int nOrders)
        {
            var orders = new List<Order>();
            Random rnd = new Random();
            int n = _contex.Customers.Count();
            int index;

            for (int i = 1; i <= nOrders; i++)
            {
                index = rnd.Next(1, n+1);
                var customer = _contex.Customers.ToList().First(x => x.Id == index);

                var order = Generator.GenerateOrder(i, customer, DateTime.Now.AddDays(-50));
                _contex.Orders.Add(order);

            }
            return orders;
        }

        private void SeedCustomers(int nCustomers)
        {
            List<Customer> customers = BuildCustomerList(nCustomers);

            foreach (var customer in customers)
            {
                _contex.Customers.Add(customer);
            }
        }

        private List<Customer> BuildCustomerList(int nCustomers)
        {
            var customers = new List<Customer>();
            string name = "";

            for(int i=1; i<=nCustomers; i++)
            {
                name = Generator.GenerateNameUnique();
                customers.Add(new Customer
                {
                    Id = i,
                    Name = name,
                    Email = Generator.GenerateEmail(name)
                });
            }

            return customers;
        }
    }
}
