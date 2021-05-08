namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TypeOfCargo
    {
        public TypeOfCargo()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }
        [Required]
        public double Coefficient { get; set; }
        [Required]
        public bool Active { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
