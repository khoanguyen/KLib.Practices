using System;
using KLib.Practices.DAL;

namespace KLib.Practices.Common.DAL
{
    public class GenericRepository<TContext, TEntity> : EntityRepositoryBase<TContext, TEntity>
        where TEntity : class
        where TContext : class, IDisposable
    {
        private readonly IDataContext _dataContext;

        public GenericRepository(IDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
