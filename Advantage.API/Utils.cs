using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvantageData.API
{
    public static class Utils
    {
        public static long ToEpoch(DateTime date) { return (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds; }
        public static DateTime FromEpoch(long epoch) => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddMilliseconds(epoch);

    }
}
