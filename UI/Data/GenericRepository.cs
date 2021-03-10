using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UI.Data.Interfaces;

namespace UI.Data
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected readonly Func<TContext> ContextCreator;

        protected GenericRepository(Func<TContext> contextCreator)
        {
            ContextCreator = contextCreator;
        }
        public TEntity Add(TEntity model)
        {
            using var context = ContextCreator.Invoke();
            var added = context.Set<TEntity>().Add(model).Entity;
            context.SaveChanges();
            return added;
        }

        private IQueryable<TEntity> Include(IQueryable<TEntity> query, string includeProperties)
        {
            return includeProperties
                .Split(new char[','], StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


        public virtual TEntity GetById(int id)
        {
           using var context = ContextCreator.Invoke();
           return context.Set<TEntity>().Find(id);
        }

        public List<TEntity> GetAll(string includeProperties = null)
        {
            using var context = ContextCreator.Invoke();
            IQueryable<TEntity> query = context.Set<TEntity>();
            if (includeProperties != null)
            {
                query = includeProperties
                    .Split(new char[','], StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return query.ToList();
        }

        public bool Any(Func<TEntity, bool> predicate)
        {
            using var context = ContextCreator.Invoke();
            return context.Set<TEntity>().Any(predicate);
        }

        public void Remove(TEntity entity)
        {
            using var context = ContextCreator.Invoke();
            context.Set<TEntity>().Remove(entity);
            context.SaveChanges();
        }
    }

}
