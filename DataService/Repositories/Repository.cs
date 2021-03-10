using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataService.Repositories
{
    public class Repository<T> :IDisposable, IRepository<T> where T : class
    {
        protected readonly DbContext DbContext;

        internal DbSet<T> DbSet;

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public List<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
        {
            IQueryable<T> query = DbSet.AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                query = includeProperties
                    .Split(new char[','], StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                query = includeProperties
                    .Split(new char[','], StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return query.FirstOrDefault();
        }

        public T Add(T entity)
        {
          return  DbSet.Add(entity).Entity;
            
        }

        public void Remove(int id)
        {
            var entityToRemove = DbSet.Find(id);
            Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
