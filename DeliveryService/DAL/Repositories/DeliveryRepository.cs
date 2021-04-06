using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class DeliveryRepository : IRepository<Delivery>
    {
        private DSdb db;

        public DeliveryRepository(DSdb dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Delivery> GetList()
        {
            return db.Delivery.ToList();
        }

        public Delivery GetItem(int id)
        {
            return db.Delivery.Find(id);
        }

        public void Create(Delivery item)
        {
            db.Delivery.Add(item);
        }

        public void Update(Delivery item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Delivery item = db.Delivery.Find(id);
            if (item != null)
                db.Delivery.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
