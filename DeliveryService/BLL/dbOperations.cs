using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace BLL
{
    public class dbOperations : IDbCrud
    {
        IdbOperations db;

        public dbOperations(IdbOperations repos)
        {
            db = repos;
        }

        #region Order
        public AllOrdersModel GetAllOrders(IAccountService serv, HttpContext httpContext)
        {
            User usr = serv.GetCurrentUserAsync(httpContext).Result;
            var med = serv.GetRole(httpContext);
            string role = null;
            if (med.Status != TaskStatus.Faulted)
            {
                role = med.Result.ToList().First();
            }
            AllOrdersModel allOrders = new AllOrdersModel();
            switch (role)
            {
                case "customer": {
                        allOrders.Role = "customer";
                        var allUserOrders = db.Orders.GetList().Where(o => o.Customer_ID_FK == usr.Id);
                        allOrders.Active = allUserOrders.Where(o => o.Status_ID_FK != 2).Select(i => new OrderModel(i)).ToList();
                        allOrders.Past = allUserOrders.Where(o => o.Status_ID_FK == 2).Select(i => new OrderModel(i)).ToList();
                        return allOrders; 
                    } 
                case "courier": {
                        allOrders.Role = "courier";
                        var allUserOrders = db.Orders.GetList().Where(o => o.Courier_ID_FK == usr.Id || o.Courier_ID_FK == null);
                        allOrders.Active = allUserOrders.Where(o => o.Status_ID_FK != 2 && o.Courier_ID_FK != null).Select(i => new OrderModel(i)).ToList();
                        allOrders.Past = allUserOrders.Where(o => o.Status_ID_FK == 2).Select(i => new OrderModel(i)).ToList();
                        allOrders.Available = allUserOrders.Where(o => o.Courier_ID_FK == null).Select(i => new OrderModel(i)).ToList();
                        return allOrders;
                    } 
                case "admin": {
                        allOrders.Role = "admin";
                        var allUserOrders = db.Orders.GetList();
                        allOrders.Active = allUserOrders.Where(o => o.Status_ID_FK != 2 && o.Courier_ID_FK != null).Select(i => new OrderModel(i)).ToList();
                        allOrders.Past = allUserOrders.Where(o => o.Status_ID_FK == 2).Select(i => new OrderModel(i)).ToList();
                        allOrders.Available = allUserOrders.Where(o => o.Courier_ID_FK == null).Select(i => new OrderModel(i)).ToList();
                        return allOrders;
                    }
                default: return  allOrders; 
            }
        }

        public int CreateOrder(OrderModel o, IAccountService serv, HttpContext httpContext)
        {
            if (DateTime.Compare(o.Deadline, DateTime.Today)>0)
            {
                User usr = serv.GetCurrentUserAsync(httpContext).Result;

                db.Orders.Create(new Order() { AddNote = o.AddNote, AdressDestination = o.AdressDestination, AdressOrigin = o.AdressOrigin, Cost = 0, Courier_ID_FK = o.Courier_ID_FK, Customer_ID_FK = usr.Id, Deadline = o.Deadline, Delivery_ID_FK = o.Delivery_ID_FK, OrderDate = DateTime.Now.Date, ReceiverName = o.ReceiverName, Status_ID_FK = 1 });
                Save();
                int id = db.Orders.GetList().Where(i => i.AddNote == o.AddNote && i.AdressDestination == o.AdressDestination && i.AdressOrigin == o.AdressOrigin && i.Courier_ID_FK == o.Courier_ID_FK && i.Customer_ID_FK == usr.Id && i.Deadline == o.Deadline && i.Delivery_ID_FK == o.Delivery_ID_FK && i.ReceiverName == o.ReceiverName && i.Status_ID_FK == 1).First().ID;
                return id;
            }
            else
                return 0;
        }

        public void UpdateOrder(OrderModel o)
        {
            Order ord = db.Orders.GetItem(o.ID);
            if (DateTime.Compare(ord.Deadline, DateTime.Today) > 1)
            {
                ord.AddNote = o.AddNote;
                ord.AdressDestination = o.AdressDestination;
                ord.AdressOrigin = o.AdressOrigin;
                ord.Courier_ID_FK = o.Courier_ID_FK;
                ord.Customer_ID_FK = o.Customer_ID_FK;
                ord.Deadline = o.Deadline;
                ord.Delivery_ID_FK = o.Delivery_ID_FK;
                ord.ReceiverName = o.ReceiverName;
                ord.Status_ID_FK = o.Status_ID_FK;
                db.Orders.Update(ord);
                Save();
            }
            else
                return;

        }


        public void UpdateOrderStatus(int id)
        {
            Order ord = db.Orders.GetItem(id);
            if (ord != null)
            {
                ord.Status_ID_FK = 2;
                db.Orders.Update(ord);
                Save();
            }
        }

        public void DeleteOrder(int id)
        {
            Order ord = db.Orders.GetItem(id);
            if (ord != null)
            {
                if (DateTime.Compare(ord.Deadline, DateTime.Today) > 1)
                {
                    var allOI = GetAllOrderItems();
                    foreach (var item in allOI)
                        DeleteOrderItem(item.ID);
                    db.Orders.Delete(ord.ID);
                    Save();
                }
                else
                    return;
            }
        }

        public OrderModel GetOrder(int id)
        {
            Order ord = db.Orders.GetItem(id);
            OrderModel o = null;
            if (ord != null)
                o = new OrderModel(ord);

            //OrderModel o = new OrderModel(db.Orders.GetItem(id));
            return o;
        }
        #endregion


        #region Status
        public List<StatusModel> GetAllStatuses()
        {
            return db.Statuses.GetList().Select(i => new StatusModel(i)).ToList();
        }

        public void CreateStatus(StatusModel s)
        {
            db.Statuses.Create(new Status() { StatusName = s.StatusName });
            Save();
        }

        public void UpdateStatus(StatusModel s)
        {
            Status st = db.Statuses.GetItem(s.ID);
            st.StatusName = s.StatusName;
            db.Statuses.Update(st);
            Save();
        }

        public void DeleteStatus(int id)
        {
            Status st = db.Statuses.GetItem(id);
            if (st != null)
            {
                db.Statuses.Delete(st.ID);
                Save();
            }
        }

        public StatusModel GetStatus(int id)
        {
            StatusModel s = new StatusModel(db.Statuses.GetItem(id));
            return s;
        }
        #endregion

        #region Customer
        public List<CustomerModel> GetAllCustomers()
        {
            return db.Users.GetList().Select(i => new CustomerModel(i)).ToList();
        }

        public CustomerModel GetClient(string id)
        {
            CustomerModel cl = new CustomerModel(db.Users.GetItem(id));
            return cl;
        }

        public void CreateCustomer(CustomerModel c)
        {
            db.Users.Create(new User() { Email = c.Email, PasswordHash = c.Password, UserName = c.UserName, Discount = c.Discount });
            Save();
        }

        public void UpdateCustomer(CustomerModel c)
        {
            User cl = db.Users.GetItem(c.ID);
            cl.Discount = c.Discount;
            cl.Email = c.Email;
            cl.PasswordHash = c.Password;
            cl.UserName = c.UserName;
            db.Users.Update(cl);
            Save();
        }
        public void DeleteCustomer(string id)
        {
            User cl = db.Users.GetItem(id);
            if (cl != null)
            {
                db.Users.Delete(cl.Id);
                Save();
            }
        }

        #endregion

        #region TypeOfCargo

        public List<TypeOfCargoModel> GetAllTypesOfCargo()
        {
            return db.TypesOfCargo.GetList().Select(i => new TypeOfCargoModel(i)).ToList();
        }

        public void CreateCargoType(TypeOfCargoModel t)
        {
            db.TypesOfCargo.Create(new TypeOfCargo() { TypeName = t.TypeName, Coefficient = t.Coefficient });
            Save();
        }

        public void UpdateCargoType(TypeOfCargoModel t)
        {
            TypeOfCargo ct = db.TypesOfCargo.GetItem(t.ID);
            ct.TypeName = t.TypeName;
            ct.Coefficient = t.Coefficient;
            db.TypesOfCargo.Update(ct);
            Save();
        }

        public void DeleteCargoType(int id)
        {
            TypeOfCargo ct = db.TypesOfCargo.GetItem(id);
            if (ct != null)
            {
                db.TypesOfCargo.Delete(ct.ID);
                Save();
            }
        }

        public TypeOfCargoModel GetTypeOfCargo(int id)
        {
            TypeOfCargoModel tc = new TypeOfCargoModel(db.TypesOfCargo.GetItem(id));
            return tc;
        }

        #endregion

        #region Delivery

        public List<DeliveryModel> GetAllDeliveries()
        {
            return db.Deliveries.GetList().Select(i => new DeliveryModel(i)).ToList();
        }

        public DeliveryModel GetDelivery(int id)
        {
            DeliveryModel dv = new DeliveryModel(db.Deliveries.GetItem(id));
            return dv;
        }

        public int CreateDelivery(DeliveryModel d)
        {
            db.Deliveries.Create(new Delivery() { Courier_ID_FK = d.Courier_ID_FK, Date = d.Date, Distance = d.Distance, KmPrice = d.KmPrice });
            Save();
            int id = db.Deliveries.GetList().Where(i => i.Courier_ID_FK == d.Courier_ID_FK && i.Date == d.Date && i.Distance == d.Distance && i.KmPrice == d.KmPrice).First().ID;
            return id;
        }

        public void UpdateDelivery(DeliveryModel d)
        {
            Delivery dl = db.Deliveries.GetItem(d.ID);
            dl.Courier_ID_FK = d.Courier_ID_FK;
            dl.Date = d.Date;
            dl.Distance = d.Distance;
            dl.KmPrice = d.KmPrice;

            db.Deliveries.Update(dl);
            Save();
        }
        public void DeleteDelivery(int id)
        {
            Delivery dl = db.Deliveries.GetItem(id);
            if (dl != null)
            {
                db.Deliveries.Delete(dl.ID);
                Save();
            }
        }
        #endregion


        #region Courier

        public List<CourierModel> GetAllCouriers()
        {
            return db.Users.GetList().Select(i => new CourierModel(i)).ToList();
        }

        public void CreateCourier(CourierModel c)
        {
            db.Users.Create(new User() { Email = c.Email, PasswordHash = c.Password, UserName = c.UserName, PhoneNumber = c.PhoneNumber });
            Save();
        }

        public void UpdateCourier(CourierModel c)
        {
            User cr = db.Users.GetItem(c.ID);
            cr.Email = c.Email;
            cr.PasswordHash = c.Password;
            cr.UserName = c.UserName;
            cr.PhoneNumber = c.PhoneNumber;
            db.Users.Update(cr);
            Save();
        }
        public void DeleteCourier(string id)
        {
            User cr = db.Users.GetItem(id);
            if (cr != null)
            {
                db.Users.Delete(cr.Id);
                Save();
            }
        }

        public CourierModel GetCourier(string id)
        {
            CourierModel dv = new CourierModel(db.Users.GetItem(id));
            return dv;
        }
        #endregion


        #region OrderItem
        public List<OrderItemModel> GetAllOrderItems()
        {
            return db.OrderItems.GetList().Select(i => new OrderItemModel(i)).ToList();
        }
        public List<OrderItemModel> GetOrderItems(int id)
        {
            return db.OrderItems.GetList().Select(i => new OrderItemModel(i)).Where(i => i.Order_ID_FK == id).ToList();
        }
        public void CreateOrderItem(OrderItemModel c)
        {
            db.OrderItems.Create(new OrderItem() { TypeOfCargo_ID_FK = c.TypeOfCargo_ID_FK, OrderName = c.OrderName, Price = c.Price, Order_ID_FK = c.Order_ID_FK });
            Save();
            var ord = db.Orders.GetItem(c.Order_ID_FK);
            var toc = db.TypesOfCargo.GetItem(c.TypeOfCargo_ID_FK);
            if (ord != null)
            {
                var cust = db.Users.GetItem(ord.Customer_ID_FK);
                if (toc != null && cust != null)
                {
                    double dsc = 0;
                    if (cust.Discount != null)
                        dsc = (double)cust.Discount;
                    ord.Cost = ord.Cost + c.Price * toc.Coefficient / 100.0 * (100 - dsc) / 100.0;
                    db.Orders.Update(ord);
                    Save();
                }
            }

        }

        public void UpdateOrderItem(OrderItemModel c)
        {
            OrderItem oi = db.OrderItems.GetItem(c.ID);
            if (oi != null)
            {
                oi.OrderName = c.OrderName;
                oi.Order_ID_FK = c.Order_ID_FK;
                oi.Price = c.Price;
                oi.TypeOfCargo_ID_FK = c.TypeOfCargo_ID_FK;
                db.OrderItems.Update(oi);

                var ord = db.Orders.GetItem(c.Order_ID_FK);
                var toc = db.TypesOfCargo.GetItem(c.TypeOfCargo_ID_FK);
                if (ord != null)
                {
                    var cust = db.Users.GetItem(ord.Customer_ID_FK);
                    if (toc != null && cust != null)
                    {
                        double dsc = 0;
                        if (cust.Discount != null)
                            dsc = (double)cust.Discount;
                        ord.Cost = ord.Cost + c.Price * toc.Coefficient / 100.0 * (100 - dsc);
                    }
                }
                Save();
            }


        }
        public void DeleteOrderItem(int id)
        {
            OrderItem cr = db.OrderItems.GetItem(id);
            if (cr != null)
            {
                var ord = db.Orders.GetItem(cr.Order_ID_FK);
                var toc = db.TypesOfCargo.GetItem(cr.TypeOfCargo_ID_FK);
                if (ord != null)
                {
                    var cust = db.Users.GetItem(ord.Customer_ID_FK);
                    if (toc != null && cust != null)
                    {
                        double dsc = 0;
                        if (cust.Discount != null)
                            dsc = (double)cust.Discount;
                        ord.Cost = ord.Cost - cr.Price * toc.Coefficient / 100.0 * (100 - dsc);
                    }
                }
                db.OrderItems.Delete(cr.ID);
                Save();
            }
        }
        public OrderItemModel GetOrderItem(int id)
        {
            OrderItemModel dv = new OrderItemModel(db.OrderItems.GetItem(id));
            return dv;
        }
        #endregion


        #region User
        public List<UserModel> GetAllUsers()
        {
            return db.Users.GetList().Select(i => new UserModel(i)).ToList();
        }

        public void CreateUser(UserModel c)
        {
            db.Users.Create(new User() { Email = c.Email, PasswordHash = c.Password, UserName = c.UserName });
            Save();
        }

        public void UpdateUser(UserModel c)
        {
            User us = db.Users.GetItem(c.ID);
            us.Email = c.Email;
            us.PasswordHash = c.Password;
            us.UserName = c.UserName;
            db.Users.Update(us);
            Save();
        }
        public void DeleteUser(string id)
        {
            User cr = db.Users.GetItem(id);
            if (cr != null)
            {
                db.Users.Delete(cr.Id);
                Save();
            }
        }

        public UserModel GetUser(string id)
        {
            UserModel dv = new UserModel(db.Users.GetItem(id));
            return dv;
        }
        #endregion



        public int Save()
        {
            int SaveCh = 0;
            try
            {
                SaveCh = db.Save();
            }
            catch
            {
                return 2;
            }
            if (SaveCh > 0) return 1;
            return 0;
        }

    }
}
