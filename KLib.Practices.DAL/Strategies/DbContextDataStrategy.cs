using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace KLib.Practices.DAL.Strategies
{
    internal sealed class DbContextDataStrategy<TEntity> : DataStrategy<TEntity>
        where TEntity : class
    {
        private DbContext DbContext
        {
            get { return Context as DbContext; }
        }

        private DbSet<TEntity> DbSet
        {
            get { return DbContext.Set<TEntity>(); }
        }

        public DbContextDataStrategy(DbContext context)
            : base(context)
        {
        }

        public override IQueryable<TEntity> List()
        {
            return DbSet;
        }

        public override TEntity FindByKey(params object[] pk)
        {
            return DbSet.Find(pk);
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public override void AddNew(TEntity entity)
        {            
            DbSet.Add(entity);
        }

        public override void Delete(TEntity entity)
        {
            var entry = GetEntrywithAttach(entity);
            entry.State = System.Data.EntityState.Deleted;
        }

        public override void Update(TEntity entity, IEnumerable<string> updatedProperties)
        {
            if (updatedProperties == null) throw new ArgumentException("No changed property found");            
            var entityEntry = GetEntrywithAttach(entity);
            foreach (var propEntry in updatedProperties.Select(entityEntry.Property)
                                                       .Where(propEntry => propEntry != null))
            {
                propEntry.IsModified = true;
            }
        }

        public override void Replace(TEntity entity)
        {            
            var entityEntry = GetEntrywithAttach(entity);
            entityEntry.State = System.Data.EntityState.Modified;
        }

        public override int Count()
        {
            return DbSet.Count();
        }

        public override int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public override long LongCount()
        {
            return DbSet.LongCount();
        }

        public override long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.LongCount(predicate);
        }

        private DbEntityEntry<TEntity> GetEntrywithAttach(TEntity entity)
        {
            var entry = DbContext.Entry(entity);
            if (entry == null || entry.State == System.Data.EntityState.Detached)
            {
                DbSet.Attach(entity);
                entry = DbContext.Entry(entity);
            }
            return entry;
        }
    }
}
