namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class Status
    {
        public Status()
        {
            Orders = new HashSet<Order>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string StatusName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
