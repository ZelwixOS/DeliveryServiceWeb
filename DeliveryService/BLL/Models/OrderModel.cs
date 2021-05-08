using System;
using System.Collections.Generic;
using DAL;

namespace BLL.Models
{
    public partial class OrderModel
    {
        public int ID { get; set; }

        public double Cost { get; set; }
        public string AdressOrigin { get; set; }
        public int? Delivery_ID_FK { get; set; }
        public string Customer_ID_FK { get; set; }
        public string CustomerS { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDateS { get; set; }
        public DateTime Deadline { get; set; }
        public string DeadlineS { get; set; }
        public string AdressDestination { get; set; }
        public string ReceiverName { get; set; }
        public string AddNote { get; set; }
        public int Status_ID_FK { get; set; }
        public string Courier_ID_FK { get; set; }
        public string CourierS { get; set; }

        public void UpdateDates()
        {
            OrderDateS = OrderDate.ToString("dd/MM/yyyy");
            DeadlineS = Deadline.ToString("dd/MM/yyyy");
        }


        public OrderModel(Order o)
        {
            ID = o.ID;
            Cost = o.Cost;
            AdressOrigin = o.AdressOrigin;
            Delivery_ID_FK = o.Delivery_ID_FK;
            Customer_ID_FK = o.Customer_ID_FK;
            OrderDate = o.OrderDate;
            OrderDateS = OrderDate.ToString("dd/MM/yyyy");
            Deadline = o.Deadline;
            DeadlineS = Deadline.ToString("dd/MM/yyyy");
            AdressDestination = o.AdressDestination;
            ReceiverName = o.ReceiverName;
            AddNote = o.AddNote;
            Status_ID_FK = o.Status_ID_FK;
            Courier_ID_FK = o.Courier_ID_FK;

            if (o.Customer != null)
                CustomerS = o.Customer.UserName + " (" + o.Customer.SecondName + " " + o.Customer.FirstName + ")";


            if (o.Courier != null)
                CourierS = o.Courier.UserName + " ("+ o.Courier.SecondName + " " + o.Courier.FirstName + ")";


            OrderItem = new HashSet<OrderItemModel>();
            foreach (var oi in o.OrderItems)
            {
                if (oi.TypeOfCargo == null)
                    
                OrderItem.Add(new OrderItemModel(oi));
            }    
            
            Status = new StatusModel(o.Status);
        }
        public  OrderModel()
        {
            OrderItem = new HashSet<OrderItemModel>();
        }
        public virtual StatusModel Status { get; set; }
        public virtual ICollection<OrderItemModel> OrderItem { get; set; }
    }
}
