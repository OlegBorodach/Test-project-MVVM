using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataService.Interfaces
{
    public interface IRepository<T>:IDisposable where T : class
    {
        T Get(int id);

        List<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
        );

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null);

        T Add(T entity);

        void Remove(int id);

        void Remove(T entity);

        void Save();
    }
}
