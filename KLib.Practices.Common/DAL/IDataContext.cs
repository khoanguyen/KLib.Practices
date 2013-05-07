using System;

namespace KLib.Practices.Common.DAL
{
    public interface IDataContext : IDisposable
    {
        IDisposable GetContext(Type contextType);
        TContext GetContext<TContext>() where TContext : class, IDisposable;
        TRepository GetRepository<TRepository>() where TRepository : class;

        GenericRepository<TContext, TEntity> GetRepository<TContext, TEntity>()
            where TContext : class, IDisposable
            where TEntity : class;
        void SaveChanges(params Type[] contextTypes);
        void SaveChanges<TContext>() where TContext : class, IDisposable;
        void SaveAllChanges();
    }
}
