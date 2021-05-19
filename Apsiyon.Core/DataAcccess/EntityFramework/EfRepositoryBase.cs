using Apsiyon.Core.Entities.Abstract;
using Apsiyon.Core.Utilities.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Apsiyon.Core.DataAcccess.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeEntities)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();

                if (filter != null)
                    query = query.Where(filter);
                if (includeEntities.Length > 0)
                    query = query.IncludeMultiple(includeEntities);

                return query.SingleOrDefault();
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeEntities)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();

                if (filter != null)
                    query = query.Where(filter);

                if (includeEntities.Length > 0)
                    query = query.IncludeMultiple(includeEntities);

                return query.ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
