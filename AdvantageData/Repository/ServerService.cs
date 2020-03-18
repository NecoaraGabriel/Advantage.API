using AdvantageData.IRepository;
using AdvantageData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdvantageData.Repository
{
    public class ServerService : IServer
    {
        private readonly ModelsDbContex _contex;

        public ServerService(ModelsDbContex contex)
        {
            _contex = contex;
        }

        public IEnumerable<Server> GetAll()
        {
            return _contex.Servers.OrderBy(x => x.Id);
        }

        public Server GetById(int id)
        {
            return _contex.Servers.FirstOrDefault(x=>x.Id == id);
        }
    }
}
