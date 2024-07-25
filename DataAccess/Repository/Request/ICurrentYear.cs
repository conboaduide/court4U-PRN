using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Request
{
    public interface ICurrentYear
    {
        public string Month { get; set; }
        public int Count { get; set; }
    }
}
