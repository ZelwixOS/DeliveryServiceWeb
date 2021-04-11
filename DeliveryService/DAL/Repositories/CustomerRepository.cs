using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CustomerRepository : IUsersRepository<Customer>
    {
        private DSdb db;

        public CustomerRepository(DSdb dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Customer> GetList()
        {
            return db.Customer.ToList();
        }

        public Customer GetItem(string id)
        {
            return db.Customer.Find(id);
        }

        public void Create(Customer item)
        {
            db.Customer.Add(item);
        }

        public void Update(Customer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(string id)
        {
            Customer item = db.Customer.Find(id);
            if (item != null)
                db.Customer.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
