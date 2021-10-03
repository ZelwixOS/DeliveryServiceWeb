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

        public (string, UserModel) GetRole(IAccountService serv, HttpContext httpContext)
        {
            User usr = serv.GetCurrentUserAsync(httpContext).Result;
            UserModel usrM = null;
            if (usr != null)
                usrM = new UserModel(usr);
            var med = serv.GetRole(httpContext);
            string role = null;
            if (med.Status != TaskStatus.Faulted)
            {
                role = med.Result.ToList().First();
            }
            return (role, usrM);
        }

        public UsersByRole GetUsersByRole(IAccountService serv)
        {
            UsersByRole ubr = new UsersByRole();

            var cour = serv.GetByRole("courier").Result;
            foreach (User c in cour)
                ubr.Couriers.Add(new UserModel(c));

            var cust = serv.GetByRole("customer").Result;
            foreach (User c in cust)
                ubr.Customers.Add(new UserModel(c));

            return ubr;
        }

        #region Order
        public AllOrdersModel GetAllOrders(string role, UserModel usr)
        {
            AllOrdersModel allOrders = new AllOrdersModel();
            switch (role)
            {
                case "customer":
                    {
                        allOrders.Role = "customer";
                        var allUserOrders = db.Orders.GetList().Where(o => o.Customer_ID_FK == usr.ID);
                        allOrders.Active = allUserOrders.Where(o => o.Status_ID_FK != 2).Select(i => new OrderModel(i)).ToList();
                        allOrders.Past = allUserOrders.Where(o => o.Status_ID_FK == 2).Select(i => new OrderModel(i)).ToList();
                        return allOrders;
                    }
                case "courier":
                    {
                        allOrders.Role = "courier";
                        var allUserOrders = db.Orders.GetList().Where(o => o.Courier_ID_FK == usr.ID || o.Courier_ID_FK == null);
                        allOrders.Active = allUserOrders.Where(o => o.Status_ID_FK != 2 && o.Courier_ID_FK != null).Select(i => new OrderModel(i)).ToList();
                        allOrders.Past = allUserOrders.Where(o => o.Status_ID_FK == 2).Select(i => new OrderModel(i)).ToList();
                        allOrders.Available = allUserOrders.Where(o => o.Courier_ID_FK == null && o.Status_ID_FK == 1).Select(i => new OrderModel(i)).ToList();
                        return allOrders;
                    }
                case "admin":
                    {
                        allOrders.Role = "admin";
                        var allUserOrders = db.Orders.GetList();
                        allOrders.Active = allUserOrders.Where(o => o.Status_ID_FK != 2 && o.Courier_ID_FK != null).Select(i => new OrderModel(i)).ToList();
                        allOrders.Past = allUserOrders.Where(o => o.Status_ID_FK == 2).Select(i => new OrderModel(i)).ToList();
                        allOrders.Available = allUserOrders.Where(o => o.Courier_ID_FK == null).Select(i => new OrderModel(i)).ToList();
                        return allOrders;
                    }
                default: return allOrders;
            }
        }

        public int CreateOrder(OrderModel o, string role, UserModel usr)
        {
            try
            {
                if (DateTime.Compare(o.Deadline, DateTime.Today) > 0)
                {
                    db.Orders.Create(new Order() { AddNote = o.AddNote, AdressDestination = o.AdressDestination, AdressOrigin = o.AdressOrigin, Cost = 0, Courier_ID_FK = o.Courier_ID_FK, Customer_ID_FK = usr.ID, Deadline = o.Deadline, Delivery_ID_FK = o.Delivery_ID_FK, OrderDate = DateTime.Now.Date, ReceiverName = o.ReceiverName, Status_ID_FK = 5 });
                    Save();
                    return 1;
                }
                else
                    return 3;
            }
            catch
            {
                return 0;
            }

        }

        public int UpdateOrder(OrderModel o, UserModel usr)
        {
            Order ord = db.Orders.GetItem(o.ID); //1

            try //2
            {
                if (ord != null) //3
                {
                    if (ord.Customer_ID_FK == usr.ID) //4
                    {
                        if (DateTime.Compare(o.Deadline, DateTime.Today) > 0) //5
                        {
                            ord.AddNote = o.AddNote; //6
                            ord.AdressDestination = o.AdressDestination;
                            ord.AdressOrigin = o.AdressOrigin;
                            ord.Deadline = o.Deadline;
                            ord.ReceiverName = o.ReceiverName;
                            ord.Status_ID_FK = 5;
                            db.Orders.Update(ord);
                            Save();
                            return 1; //7
                        }
                        else
                            return 3; //7
                    }
                    else
                        return 2; // 7
                }
                else
                    return 4; // 7
            }
            catch
            {
                return 0; // 7
            }
        }


        public int UpdateOrderStatus(int id, int status, string role, UserModel usr)
        {
            Order ord = db.Orders.GetItem(id);
            try
            {
                if (ord != null)
                {
                    if (role == "courier")
                        ord.Courier_ID_FK = usr.ID;
                    if (role == "customer" && usr.ID == ord.Customer_ID_FK || role == "courier")
                    {
                        ord.Status_ID_FK = status;
                        db.Orders.Update(ord);
                        Save();
                        return 1;
                    }
                    else
                        return 2;
                }
                else
                    return 4;
            }
            catch
            { 
                return 0; 
            }

        }

        public int DeleteOrder(int id, UserModel usr)
        {
            Order ord = db.Orders.GetItem(id);

            try
            {
                if (ord != null)
                {
                    if (ord.Customer_ID_FK == usr.ID)
                    {
                        if (ord.Courier_ID_FK == null)
                        {
                            var allOI = GetAllOrderItems();
                            foreach (var item in allOI)
                                DeleteOrderItem(item.ID, usr);
                            db.Orders.Delete(ord.ID);
                            Save();
                            return 1;
                        }
                        else
                            return 5;
                    }
                    else
                        return 2;
                }
                else
                    return 4;
            }
            catch
            {
                return 0;
            }
        }

        public OrderModel GetOrder(int id)
        {
            Order ord = db.Orders.GetItem(id);
            OrderModel o = null;
            if (ord != null)
                o = new OrderModel(ord);

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

        #region TypeOfCargo

        public List<TypeOfCargoModel> GetAllTypesOfCargo(string role)
        {
            switch (role)
            {
                case "customer": return db.TypesOfCargo.GetList().Where(t => t.Active == true).Select(i => new TypeOfCargoModel(i)).ToList();
                case "admin": return db.TypesOfCargo.GetList().Select(i => new TypeOfCargoModel(i)).ToList();
                default: return db.TypesOfCargo.GetList().Where(t => t.Active == true).Select(i => new TypeOfCargoModel(i)).ToList();
            }
        }

        public int CreateCargoType(TypeOfCargoModel t)
        {
            try
            {
                db.TypesOfCargo.Create(new TypeOfCargo() { TypeName = t.TypeName, Coefficient = t.Coefficient, Active = true });
                return Save();
            }
            catch
            {
                return 0;
            }
        }

        public void UpdateCargoType(TypeOfCargoModel t)
        {
            TypeOfCargo ct = db.TypesOfCargo.GetItem(t.ID);
            ct.TypeName = t.TypeName;
            ct.Coefficient = t.Coefficient;
            db.TypesOfCargo.Update(ct);
            Save();
        }

        public int TurnCargoType(int id, bool status)
        {
            TypeOfCargo ct = db.TypesOfCargo.GetItem(id);
            try
            {
                if (ct != null)
                {
                    ct.Active = status;
                    db.TypesOfCargo.Update(ct);
                    return Save();
                }
                else
                    return 6;
            }
            catch
            {
                return 0;
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

        #region OrderItem
        public List<OrderItemModel> GetAllOrderItems()
        {
            return db.OrderItems.GetList().Select(i => new OrderItemModel(i)).ToList();
        }
        public List<OrderItemModel> GetOrderItems(int id)
        {
            return db.OrderItems.GetList().Select(i => new OrderItemModel(i)).Where(i => i.Order_ID_FK == id).ToList();
        }

        public int CreateOrderItem(OrderItemModel c, UserModel usr)
        {
            Order ord = db.Orders.GetItem(c.Order_ID_FK);
            if (ord != null)
            {
                if (ord.Customer_ID_FK != usr.ID)
                    return 2;
            }
            else
                return 6;

            if (c.TypeOfCargo == null)
            {
                c.TypeOfCargo = db.TypesOfCargo.GetItem(c.TypeOfCargo_ID_FK);
                if (c.TypeOfCargo == null || c.TypeOfCargo.Active == false)
                    return 6;
            }
            try
            {
                db.OrderItems.Create(new OrderItem() { TypeOfCargo_ID_FK = c.TypeOfCargo_ID_FK, OrderName = c.OrderName, Price = c.Price, Order_ID_FK = c.Order_ID_FK });
                Save();

                var cust = db.Users.GetItem(ord.Customer_ID_FK);
                if (cust != null)
                {
                    double dsc = 0;
                    if (cust.Discount != null)
                        dsc = (double)cust.Discount;
                    ord.Cost = ord.Cost + c.Price * c.TypeOfCargo.Coefficient / 100.0 * (100 - dsc) / 100.0;
                    ord.Status_ID_FK = 5;
                    db.Orders.Update(ord);
                    Save();
                    return 1;
                }
                else
                    return 6;

            }
            catch
            {
                return 0;
            }
        }

        public int UpdateOrderItem(OrderItemModel c, UserModel usr)
        {
            Order ord = db.Orders.GetItem(c.Order_ID_FK);
            if (ord != null)
            {
                if (ord.Customer_ID_FK != usr.ID)
                    return 2;
            }
            else
                return 6;

            if (c.TypeOfCargo == null)
            {
                c.TypeOfCargo = db.TypesOfCargo.GetItem(c.TypeOfCargo_ID_FK);
                if (c.TypeOfCargo == null || c.TypeOfCargo.Active == false)
                    return 6;
            }

            OrderItem oi = db.OrderItems.GetItem(c.ID);

            try
            {
                if (oi != null)
                {
                    oi.OrderName = c.OrderName;
                    oi.Order_ID_FK = c.Order_ID_FK;
                    oi.Price = c.Price;
                    oi.TypeOfCargo_ID_FK = c.TypeOfCargo_ID_FK;
                    db.OrderItems.Update(oi);

                    var cust = db.Users.GetItem(ord.Customer_ID_FK);
                    if (cust != null)
                    {
                        double dsc = 0;
                        if (cust.Discount != null)
                            dsc = (double)cust.Discount;
                        ord.Cost = ord.Cost + c.Price * c.TypeOfCargo.Coefficient * (100 - dsc) / 10000.0;
                    }
                    else
                        return 6;
                    Save();
                    return 1;
                }
                else
                    return 4;
            }
            catch
            {
                return 0;
            }
                 
        }

        public int DeleteOrderItem(int id, UserModel usr)
        {
            OrderItem cr = db.OrderItems.GetItem(id);
            Order ord = db.Orders.GetItem(cr.Order_ID_FK);

            if (ord != null && cr != null)
            {
                if (ord.Customer_ID_FK != usr.ID)
                    return 2;
            }
            else
                return 6;



            var toc = db.TypesOfCargo.GetItem(cr.TypeOfCargo_ID_FK);
            var cust = db.Users.GetItem(ord.Customer_ID_FK);

            try
            {
                if (toc != null && cust != null)
                {
                    double dsc = 0;
                    if (cust.Discount != null)
                        dsc = (double)cust.Discount;
                    ord.Cost = ord.Cost - cr.Price * toc.Coefficient * (100 - dsc) / 10000.0;
                }
                else
                    return 6;
                ord.Status_ID_FK = 5;
                db.OrderItems.Delete(cr.ID);
                Save();
                return 1;
            }
            catch
            {
                return 0;
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
                return 0;
            }
            if (SaveCh > 0) return 1;
            return 7;
        }

    }
}
