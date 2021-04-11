namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Delivery
    {
        public Delivery()
        {
            Order = new HashSet<Order>();
        }

        public int ID { get; set; }

        public double Distance { get; set; }

        public double KmPrice { get; set; }

        public string Courier_ID_FK { get; set; }

        public DateTime Date { get; set; }

        public virtual Courier Courier { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
