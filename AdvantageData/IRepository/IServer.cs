using AdvantageData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvantageData.IRepository
{
    public interface IServer
    {
        IEnumerable<Server> GetAll();
        Server GetById(int id);
    }
}
