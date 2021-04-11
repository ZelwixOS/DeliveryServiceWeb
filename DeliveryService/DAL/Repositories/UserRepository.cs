using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UserRepository : IUsersRepository<User>
    {
        private DSdb db;

        public UserRepository(DSdb dbcontext)
        {
            this.db = dbcontext;
        }

        public List<User> GetList()
        {
            return db.User.ToList();
        }

        public User GetItem(string id)
        {
            return db.User.Find(id);
        }

        public void Create(User item)
        {
            db.User.Add(item);
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(string id)
        {
            User item = db.User.Find(id);
            if (item != null)
                db.User.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
