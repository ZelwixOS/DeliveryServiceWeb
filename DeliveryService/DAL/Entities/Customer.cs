namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    
    public partial class Customer: User
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public double Discount { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
