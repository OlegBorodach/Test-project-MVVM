using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UI.Data.Interfaces
{
    public interface IGenericRepository<T>
    {
        bool Any(Func<T, bool> predicate);
        T GetById(int id);
        List<T> GetAll(string includeProperties=null);
        T Add(T model);
        void Remove(T model);
    }
}
