using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class StatusRepository : IRepository<Status>
    {
        private DSdb db;

        public StatusRepository(DSdb dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Status> GetList()
        {
            return db.Status.ToList();
        }

        public Status GetItem(int id)
        {
            return db.Status.Find(id);
        }

        public void Create(Status item)
        {
            db.Status.Add(item);
        }

        public void Update(Status item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Status item = db.Status.Find(id);
            if (item != null)
                db.Status.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
