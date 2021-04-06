namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public partial class OrderItem
    { 
        public OrderItem()
        {

        }

        public int ID { get; set; }

        public int Order_ID_FK { get; set; }
        public int TypeOfCargo_ID_FK { get; set; }
        public double Price { get; set; }

        [Required]
        [StringLength(20)]
        public string OrderName { get; set; }

        public virtual Order Order { get; set; }
        public virtual TypeOfCargo TypeOfCargo { get; set; }



    }
}
