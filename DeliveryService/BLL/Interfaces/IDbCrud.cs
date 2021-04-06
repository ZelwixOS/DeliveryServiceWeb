using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IDbCrud
    {
        List<OrderModel> GetAllOrders();
        List<StatusModel> GetAllStatuses();
        List<CustomerModel> GetAllCustomers();
        List<TypeOfCargoModel> GetAllTypesOfCargo();
        List<DeliveryModel> GetAllDeliveries();
        List<CourierModel> GetAllCouriers();
        List<UserModel> GetAllUsers();
        List<OrderItemModel> GetAllOrderItems();
        List<OrderItemModel> GetOrderItems(int id);


        CustomerModel GetClient(int id);
        TypeOfCargoModel GetTypeOfCargo(int id);
        OrderModel GetOrder(int id);
        StatusModel GetStatus(int id);
        DeliveryModel GetDelivery(int id);
        CourierModel GetCourier(int id);
        OrderItemModel GetOrderItem(int id);
        UserModel GetUser(int id);

        int CreateOrder(OrderModel o);
        void UpdateOrder(OrderModel o);
        void DeleteOrder(int id);

        void CreateCustomer(CustomerModel c);
        void UpdateCustomer(CustomerModel c);
        void DeleteCustomer(int id);

        void CreateCourier(CourierModel c);
        void UpdateCourier(CourierModel c);
        void DeleteCourier(int id);

        void CreateCargoType(TypeOfCargoModel t);
        void UpdateCargoType(TypeOfCargoModel t);
        void DeleteCargoType(int id);

        void CreateStatus(StatusModel s);
        void UpdateStatus(StatusModel s);
        void DeleteStatus(int id);


        int CreateDelivery(DeliveryModel s);
        void UpdateDelivery(DeliveryModel s);
        void DeleteDelivery(int id);

        void CreateUser(UserModel c);
        void UpdateUser(UserModel c);
        void DeleteUser(int id);

        void CreateOrderItem(OrderItemModel c);
        void UpdateOrderItem(OrderItemModel c);
        void DeleteOrderItem(int id);

        int Save();
    }
}
