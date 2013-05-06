using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using KLib.Practices.DAL;

namespace KLib.Practices.NinjectSuite.DAL
{
    public class DataContext : IDataContext
    {
        private Dictionary<Type, object> Repositories { get; set; }
        private Dictionary<Type, IDisposable> Contexts { get; set; }
        private NinjectIoCProvider IoCProvider { get; set; }
        private string UniqueName { get; set; }
        
        public DataContext(NinjectIoCProvider iocProvider)
        {
            UniqueName = Guid.NewGuid().ToString();
            Contexts = new Dictionary<Type, IDisposable>();
            Repositories = new Dictionary<Type, object>();
            IoCProvider = iocProvider;
            IoCProvider.Kernel.Bind<IDataContext>()
                       .ToConstant(this)
                       .Named(UniqueName);

        }

        public void Dispose()
        {
            foreach (var context in Contexts.Values)
            {
                try
                {
                    var binding = IoCProvider.Kernel.GetBindings(typeof (IDataContext))
                                             .FirstOrDefault(b => b.Metadata.Name == UniqueName);
                    if (binding != null) IoCProvider.Kernel.RemoveBinding(binding);
                    context.Dispose();
                }
                catch (ObjectDisposedException)
                {
                    // Do nothing, object disposed
                }
            }
        }

        public IDisposable GetContext(Type contextType)
        {
            return ResolveContext(contextType);
        }

        public TContext GetContext<TContext>() where TContext : class, IDisposable
        {
            return GetContext(typeof(TContext)) as TContext;
        }

        public TRepository GetRepository<TRepository>()
            where TRepository : class
        {
            return ResolveRepository(typeof(TRepository)) as TRepository;
        }

        public GenericRepository<TContext, TEntity> GetRepository<TContext, TEntity>()
            where TContext : class, IDisposable
            where TEntity : class
        {
            return GetRepository<GenericRepository<TContext, TEntity>>();
        }

        public void SaveChanges(params Type[] contextTypes)
        {
            foreach (var context in contextTypes.Select(contextType => Contexts[contextType])
                                                .Where(context => context != null))
            {
                DbContext dbContext;
                ObjectContext objContext;
                if ((dbContext = context as DbContext) != null)
                {
                    dbContext.ChangeTracker.DetectChanges();
                    dbContext.SaveChanges();
                }
                else if ((objContext = context as ObjectContext) != null)
                {
                    objContext.DetectChanges();
                    objContext.SaveChanges();
                    objContext.AcceptAllChanges();
                }
            }
        }

        public void SaveChanges<TContext>() where TContext : class, IDisposable
        {
            SaveChanges(typeof(TContext));
        }

        public void SaveAllChanges()
        {
            SaveChanges(Contexts.Keys.ToArray());
        }

        private IDisposable ResolveContext(Type contextType)
        {
            if (contextType == null) return null;
            if (!Contexts.ContainsKey(contextType))
                Contexts[contextType] = IoCProvider
                    .CreateInstance(contextType) as IDisposable;
            return Contexts[contextType];
        }

        private object ResolveRepository(Type requestedType)
        {
            if (requestedType == null) return null;
            if (!Repositories.ContainsKey(requestedType))
                Repositories[requestedType] = IoCProvider
                    .CreateInstance(requestedType);
            return Repositories[requestedType];
        }
    }
}
