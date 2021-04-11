using System;
using DAL;

namespace BLL.Models
{
    public class DeliveryModel
    {
        public int ID { get; set; }

        public double Distance { get; set; }

        public double KmPrice { get; set; }

        public string Courier_ID_FK { get; set; }

        public int? Transport_ID_FK { get; set; }

        public DateTime Date { get; set; }

        public string DateS { get; set; }

        public void UpdateDates()
        {
           DateS = Date.ToString("dd/MM/yyyy");
        }

        public DeliveryModel(Delivery d)
        {
            ID = d.ID;
            Distance = d.Distance;
            KmPrice = d.KmPrice;
            Courier_ID_FK = d.Courier_ID_FK;
            Date = d.Date;
            DateS = Date.ToString("dd/MM/yyyy");
        }

        public DeliveryModel() { }
    }
}
