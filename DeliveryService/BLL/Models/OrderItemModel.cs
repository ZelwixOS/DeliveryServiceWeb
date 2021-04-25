using DAL;

namespace BLL.Models
{
    public class OrderItemModel
    {
        public OrderItemModel(OrderItem o)
        {
            ID = o.ID;
            Order_ID_FK = o.Order_ID_FK;
            TypeOfCargo_ID_FK = o.TypeOfCargo_ID_FK;
            TypeOfCargoS = o.TypeOfCargo.TypeName;
            OrderName = o.OrderName;
            Price = o.Price;
        }
        public OrderItemModel()
        {

        }

        public int ID { get; set; }

        public int Order_ID_FK { get; set; }
        public int TypeOfCargo_ID_FK { get; set; }
        public double Price { get; set; }
        public string OrderName { get; set; }
        public string TypeOfCargoS { get; set; }

        public virtual Order Order { get; set; }
        public virtual TypeOfCargo TypeOfCargo { get; set; }

    }
}
