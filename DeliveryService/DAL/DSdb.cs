namespace DAL
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    public partial class DSdb : DbContext
    {
        public DSdb(DbContextOptions<DSdb> options) : base(options)
        { }

        public virtual DbSet<Courier> Courier { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<TypeOfCargo> TypeOfCargo { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.Customer).WithMany(p => p.Order).HasForeignKey(d => d.Customer_ID_FK);
                entity.HasOne(d => d.Courier).WithMany(p => p.Order).HasForeignKey(d => d.Courier_ID_FK);
                entity.HasOne(d => d.Delivery).WithMany(p => p.Order).HasForeignKey(d => d.Delivery_ID_FK);
                entity.HasOne(d => d.Status).WithMany(p => p.Order).HasForeignKey(d => d.Status_ID_FK);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasOne(d => d.Order).WithMany(p => p.OrderItem).HasForeignKey(d => d.Order_ID_FK);
                entity.HasOne(d => d.TypeOfCargo).WithMany(p => p.OrderItem).HasForeignKey(d => d.TypeOfCargo_ID_FK);
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasOne(d => d.Courier).WithMany(p => p.Delivery).HasForeignKey(d => d.Courier_ID_FK);
            });
        }
    }
}
