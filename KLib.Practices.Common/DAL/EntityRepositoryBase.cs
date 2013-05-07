using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using KLib.Practices.Common.DAL.Strategies;

namespace KLib.Practices.Common.DAL
{
    public abstract class EntityRepositoryBase<TContext, TEntity> : IEntityRepository<TEntity>
        where TEntity : class
        where TContext : class, IDisposable
    {

        private readonly DataStrategy<TEntity> _strategy;
        private readonly IDataContext _dataContext;

        protected virtual IDataContext DataContext
        {
            get { return _dataContext; }
        }

        protected virtual TContext Context
        {
            get { return DataContext.GetContext<TContext>(); }
        }

        private DbContext DbContext
        {
            get { return Context as DbContext; }
        }

        private ObjectContext ObjectContext
        {
            get { return Context as ObjectContext; }
        }

        protected EntityRepositoryBase(IDataContext dataContext)
        {
            _dataContext = dataContext;

            // Initialize DataStrategy
            var type = typeof (TContext);
            if (typeof (DbContext).IsAssignableFrom(type))
                _strategy = new DbContextDataStrategy<TEntity>(DbContext);
            else if (typeof (ObjectContext).IsAssignableFrom(type))
                _strategy = new ObjectContextDataStrategy<TEntity>(ObjectContext);
            else
                throw new ArgumentException(string.Format("{0} is not a subclass of DbContext or ObjectContext",
                                                          type.FullName));
        }

        public virtual IQueryable<TEntity> List()
        {
            return _strategy.List();
        }

        public virtual TEntity FindByKey(params object[] pk)
        {
            return _strategy.FindByKey(pk);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _strategy.Single(predicate);
        }

        public virtual void AddNew(TEntity entity)
        {
            _strategy.AddNew(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _strategy.Delete(entity);
        }

        public virtual void Update(TEntity entity, IEnumerable<string> updatedProperties)
        {
            _strategy.Update(entity, updatedProperties);
        }

        public virtual void ReplaceWith(TEntity entity)
        {
            _strategy.ReplaceWith(entity);
        }

        public int Count()
        {
            return _strategy.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _strategy.Count(predicate);
        }

        public long LongCount()
        {
            return _strategy.LongCount();
        }

        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return _strategy.LongCount(predicate);
        }
    }
}
