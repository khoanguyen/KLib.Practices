using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KLib.Practices.DAL;

namespace KLib.Practices.Common.DAL.Strategies
{
    internal abstract class DataStrategy<TEntity> : IEntityRepository<TEntity>
        where TEntity : class        
    {

        public virtual object Context { get; protected set; }

        protected DataStrategy(object context)
        {
// ReSharper disable DoNotCallOverridableMethodsInConstructor
            Context = context;
// ReSharper restore DoNotCallOverridableMethodsInConstructor

        }
        
        public abstract IQueryable<TEntity> List();
        public abstract TEntity FindByKey(params object[] pk);
        public abstract TEntity Single(Expression<Func<TEntity, bool>> predicate);
        public abstract void AddNew(TEntity entity);
        public abstract void Delete(TEntity entity);
        public abstract void Update(TEntity entity, IEnumerable<string> updatedProperties);
        public abstract void ReplaceWith(TEntity entity);
        public abstract int Count();
        public abstract int Count(Expression<Func<TEntity, bool>> predicate);
        public abstract long LongCount();
        public abstract long LongCount(Expression<Func<TEntity, bool>> predicate);       
    }
}
