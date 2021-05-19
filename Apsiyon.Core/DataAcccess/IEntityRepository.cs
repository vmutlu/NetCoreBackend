using Apsiyon.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Apsiyon.Core.DataAcccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeEntities);
        List<T> GetList(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeEntities);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
