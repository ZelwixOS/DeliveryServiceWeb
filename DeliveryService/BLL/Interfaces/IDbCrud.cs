using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    public interface IDbCrud
    {
        AllOrdersModel GetAllOrders(IAccountService serv, HttpContext httpContext);
        List<StatusModel> GetAllStatuses();
        List<CustomerModel> GetAllCustomers();
        List<TypeOfCargoModel> GetAllTypesOfCargo();
        List<DeliveryModel> GetAllDeliveries();
        List<CourierModel> GetAllCouriers();
        List<UserModel> GetAllUsers();
        List<OrderItemModel> GetAllOrderItems();
        List<OrderItemModel> GetOrderItems(int id);


        CustomerModel GetClient(string id);
        TypeOfCargoModel GetTypeOfCargo(int id);
        OrderModel GetOrder(int id);
        StatusModel GetStatus(int id);
        DeliveryModel GetDelivery(int id);
        CourierModel GetCourier(string id);
        OrderItemModel GetOrderItem(int id);
        UserModel GetUser(string id);

        int CreateOrder(OrderModel o, IAccountService serv, HttpContext httpContext);
        void UpdateOrder(OrderModel o);
        void DeleteOrder(int id);
        void UpdateOrderStatus(int id);

        void CreateCustomer(CustomerModel c);
        void UpdateCustomer(CustomerModel c);
        void DeleteCustomer(string id);

        void CreateCourier(CourierModel c);
        void UpdateCourier(CourierModel c);
        void DeleteCourier(string id);

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
        void DeleteUser(string id);

        void CreateOrderItem(OrderItemModel c);
        void UpdateOrderItem(OrderItemModel c);
        void DeleteOrderItem(int id);

        int Save();
    }
}
