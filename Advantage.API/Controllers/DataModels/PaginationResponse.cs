using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvantageData.API.Controllers.DataModels
{
    public class PaginationResponse<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }


        public PaginationResponse(IEnumerable<T> data, int index, int size)
        {
            Data = data.Skip(size * (index - 1)).Take(size);
            TotalCount = Data.Count();
        }
    }
}
