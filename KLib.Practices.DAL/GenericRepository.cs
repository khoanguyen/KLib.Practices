using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLib.Practices.DAL
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
