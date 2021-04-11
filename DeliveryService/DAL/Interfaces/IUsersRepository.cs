using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUsersRepository<T> where T : class
    {
        List<T> GetList();
        T GetItem(string id);
        void Create(T item);
        void Update(T item);
        void Delete(string id);
    }
}
