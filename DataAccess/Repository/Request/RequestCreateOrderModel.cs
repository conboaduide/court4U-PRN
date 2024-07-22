using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Request
{
    public class RequestCreateOrderModel
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public float Price { get; set; }
        public string Type { get; set; }
        public DateTime Buy_date { get; set; }
    }
}
