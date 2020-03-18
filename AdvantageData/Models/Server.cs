using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvantageData.Models
{
    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Status { get; set; }
        public bool IsOnline { get; set; }
    }
}
