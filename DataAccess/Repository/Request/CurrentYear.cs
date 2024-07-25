using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Request
{
    public class CurrentYear : ICurrentYear
    {
        public string Month { get; set; }
        public int Count { get; set; }
    }
}
