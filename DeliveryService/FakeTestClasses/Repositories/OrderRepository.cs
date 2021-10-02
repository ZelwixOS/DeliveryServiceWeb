using DAL;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FakeTestClasses.Repositories
{
    class OrderRepository : IRepository<Order>
    {
        public static Order GetOrder1()
        {
            return new Order() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = "2", Customer_ID_FK = "1", Deadline = new DateTime(2022, 10, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 };
        }

        public static Order GetOrder2()
        {
            return new Order() { ID = 2, AddNote = "", Cost = 210, AdressDestination = "Destination2", AdressOrigin = "Origin2", Customer_ID_FK = "3", Deadline = new DateTime(2020, 10, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user2", Status_ID_FK = 0 };
        }

        public List<Order> GetList()
        {
            List<Order> orders = new List<Order>();
            orders.Add(GetOrder1());
            orders.Add(GetOrder2());
            return orders.ToList();
        }

        public Order GetItem(int id)
        {
            if (id == 1)
                return GetOrder1();
            else
                return GetOrder2();
        }

        public void Create(Order item)
        {
        }

        public void Update(Order item)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
