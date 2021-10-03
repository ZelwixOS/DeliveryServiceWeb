using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class dbReposSQL : IdbOperations
    {
        private DSdb db;
        private DeliveryRepository deliveryRepository;
        private OrderRepository orderRepository;
        private StatusRepository statusRepository;
        private UserRepository userRepository;
        private OrderItemRepository orderItemRepository;
        private TypeOfCargoRepository typeOfCargoRepository;

        public dbReposSQL(DSdb dSdb)
        {
            db = dSdb;
        }

        public IRepository<Delivery> Deliveries
        {
            get
            {
                if (deliveryRepository == null)
                    deliveryRepository = new DeliveryRepository(db);
                return deliveryRepository;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }
        public IRepository<Status> Statuses
        {
            get
            {
                if (statusRepository == null)
                    statusRepository = new StatusRepository(db);
                return statusRepository;
            }
        }
        public IRepository<TypeOfCargo> TypesOfCargo
        {
            get
            {
                if (typeOfCargoRepository == null)
                    typeOfCargoRepository = new TypeOfCargoRepository(db);
                return typeOfCargoRepository;
            }
        }
        public IRepository<OrderItem> OrderItems
        {
            get
            {
                if (orderItemRepository == null)
                    orderItemRepository = new OrderItemRepository(db);
                return orderItemRepository;
            }
        }
        public IUsersRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }


        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
