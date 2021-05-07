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
        (string, UserModel) GetRole(IAccountService serv, HttpContext httpContext);
        UsersByRole GetUsersByRole(IAccountService serv);

        AllOrdersModel GetAllOrders(string role, UserModel usr);
        List<StatusModel> GetAllStatuses();
        List<TypeOfCargoModel> GetAllTypesOfCargo();
        List<DeliveryModel> GetAllDeliveries();
        List<UserModel> GetAllUsers();
        List<OrderItemModel> GetAllOrderItems();
        List<OrderItemModel> GetOrderItems(int id);


        TypeOfCargoModel GetTypeOfCargo(int id);
        OrderModel GetOrder(int id);
        StatusModel GetStatus(int id);
        DeliveryModel GetDelivery(int id);
        OrderItemModel GetOrderItem(int id);
        UserModel GetUser(string id);

        int CreateOrder(OrderModel o, string role, UserModel usr);
        void UpdateOrder(OrderModel o, UserModel usr);
        void DeleteOrder(int id, UserModel usr);
        void UpdateOrderStatus(int id, int status, string role, UserModel usr);

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
