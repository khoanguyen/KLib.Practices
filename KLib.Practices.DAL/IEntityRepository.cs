using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace KLib.Practices.DAL
{
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> List();
        TEntity FindByKey(params object[] pk);
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        void AddNew(TEntity entity);
        void Delete(TEntity entity);
        //void Delete(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity entity, IEnumerable<string> updatedProperties);
        //void Update(Expression<Func<TEntity, bool>> predicate, TEntity changeset);
        void Replace(TEntity entity);
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);
        long LongCount();
        long LongCount(Expression<Func<TEntity, bool>> predicate);
    }
}
