using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UsersByRole
    {
        public UsersByRole()
        {
            Customers = new List<UserModel>();
            Couriers = new List<UserModel>();
        }

        public List<UserModel> Customers { get; set; }
        public List<UserModel> Couriers { get; set; }
    }
}
