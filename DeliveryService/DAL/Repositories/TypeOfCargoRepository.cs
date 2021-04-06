using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TypeOfCargoRepository : IRepository<TypeOfCargo>
    {
        private DSdb db;

        public TypeOfCargoRepository(DSdb dbcontext)
        {
            this.db = dbcontext;
        }

        public List<TypeOfCargo> GetList()
        {
            return db.TypeOfCargo.ToList();
        }

        public TypeOfCargo GetItem(int id)
        {
            return db.TypeOfCargo.Find(id);
        }

        public void Create(TypeOfCargo item)
        {
            db.TypeOfCargo.Add(item);
        }

        public void Update(TypeOfCargo item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            TypeOfCargo item = db.TypeOfCargo.Find(id);
            if (item != null)
                db.TypeOfCargo.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
