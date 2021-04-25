using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class AllOrdersModel
    {
        public AllOrdersModel()
        {
            Role = null;
            Active = new List<OrderModel>();
            Past = new List<OrderModel>();
            Available = new List<OrderModel>();
        }

        public string Role { get; set; }

        public List<OrderModel> Active { get; set; }

        public List<OrderModel> Past { get; set; }

        public List<OrderModel> Available { get; set; }
    }
}
