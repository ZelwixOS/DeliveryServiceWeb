namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Courier: User
    {
        public Courier()
        {
            Delivery = new HashSet<Delivery>();
        }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Delivery> Delivery { get; set; }
    }
}
