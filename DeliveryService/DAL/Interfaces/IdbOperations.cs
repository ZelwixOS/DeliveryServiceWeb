  namespace DAL.Interfaces
{
    public interface IdbOperations
    {
        IUsersRepository<Courier> Couriers { get; }
        IUsersRepository<Customer> Customers { get; }
        IRepository<Delivery> Deliveries { get; }
        IRepository<Order> Orders { get; }
        IRepository<Status> Statuses { get; }
        IRepository<OrderItem> OrderItems { get; }
        IUsersRepository<User> Users { get; }
        IRepository<TypeOfCargo> TypesOfCargo { get; }
        int Save();
    }
}
