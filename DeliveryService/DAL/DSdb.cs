namespace DAL
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public partial class DSdb : IdentityDbContext<User>
    {
        public DSdb(DbContextOptions<DSdb> options) : base(options)
        { }

        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<TypeOfCargo> TypeOfCargo { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.Customer).WithMany(p => p.MyOrders).HasForeignKey(d => d.Customer_ID_FK);
                entity.HasOne(d => d.Courier).WithMany(p => p.OrdersToDeliver).HasForeignKey(d => d.Courier_ID_FK);
                entity.HasOne(d => d.Delivery).WithMany(p => p.Orders).HasForeignKey(d => d.Delivery_ID_FK);
                entity.HasOne(d => d.Status).WithMany(p => p.Orders).HasForeignKey(d => d.Status_ID_FK);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasForeignKey(d => d.Order_ID_FK);
                entity.HasOne(d => d.TypeOfCargo).WithMany(p => p.OrderItems).HasForeignKey(d => d.TypeOfCargo_ID_FK);
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasOne(d => d.Courier).WithMany(p => p.Deliveries).HasForeignKey(d => d.Courier_ID_FK);
            });
        }
    }
}
