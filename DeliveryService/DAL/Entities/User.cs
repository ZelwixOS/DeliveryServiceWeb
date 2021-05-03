using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DAL
{
    public partial class User : IdentityUser
    {
        public User()
        {
            MyOrders = new HashSet<Order>();
            OrdersToDeliver = new HashSet<Order>();
            Deliveries = new HashSet<Delivery>();
        }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public double? Discount { get; set; }

        public virtual ICollection<Order> MyOrders { get; set; }
        public virtual ICollection<Order> OrdersToDeliver { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }

    }
}
