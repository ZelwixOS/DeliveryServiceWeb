using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private DSdb db;

        public OrderItemRepository(DSdb dbcontext)
        {
            this.db = dbcontext;
        }

        public List<OrderItem> GetList()
        {
            return db.OrderItem.Include(i=> i.TypeOfCargo).ToList();
        }

        public OrderItem GetItem(int id)
        {
            var oi = db.OrderItem.Find(id);
            if (oi != null)
                oi.TypeOfCargo = db.TypeOfCargo.Find(oi.TypeOfCargo_ID_FK);
            return oi;
        }

        public void Create(OrderItem item)
        {
            db.OrderItem.Add(item);
        }

        public void Update(OrderItem item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            OrderItem item = db.OrderItem.Find(id);
            if (item != null)
                db.OrderItem.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }

    }
}
