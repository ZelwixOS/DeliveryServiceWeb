using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CourierRepository : IUsersRepository<Courier>
    {
        private DSdb db;

        public CourierRepository(DSdb dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Courier> GetList()
        {
            return db.Courier.ToList();
        }

        public Courier GetItem(string id)
        {
            return db.Courier.Find(id);
        }

        public void Create(Courier item)
        {
            db.Courier.Add(item);
        }

        public void Update(Courier item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(string id)
        {
            Courier item = db.Courier.Find(id);
            if (item != null)
                db.Courier.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
