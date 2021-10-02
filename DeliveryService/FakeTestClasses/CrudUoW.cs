using DAL;
using DAL.Interfaces;
using FakeTestClasses.Repositories;
using System;

namespace FakeTestClasses
{
    public class CrudUoW : IdbOperations
    {
        private OrderRepository orderRepository;

        public IRepository<Delivery> Deliveries
        {
            get
            {
                return null;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository();
                return orderRepository;
            }
        }
        public IRepository<Status> Statuses
        {
            get
            {
                return null;
            }
        }
        public IRepository<TypeOfCargo> TypesOfCargo
        {
            get
            {
                return null;
            }
        }
        public IRepository<OrderItem> OrderItems
        {
            get
            {
                return null;
            }
        }
        public IUsersRepository<User> Users
        {
            get
            {
                return null;
            }
        }


        public int Save()
        {
            return 1;
        }
    }
}
