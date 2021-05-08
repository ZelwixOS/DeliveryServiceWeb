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
        List<TypeOfCargoModel> GetAllTypesOfCargo(string role);
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
        int UpdateOrder(OrderModel o, UserModel usr);
        int DeleteOrder(int id, UserModel usr);
        int UpdateOrderStatus(int id, int status, string role, UserModel usr);

        int CreateCargoType(TypeOfCargoModel t);
        int TurnCargoType(int id, bool status);

        int CreateOrderItem(OrderItemModel c, UserModel usr);
        int UpdateOrderItem(OrderItemModel c, UserModel usr);
        int DeleteOrderItem(int id, UserModel usr);

        int Save();
    }
}
