using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLib.Practices.Common.DAL;
using KLib.Practices.DAL;

namespace KLib.Practices.NinjectSuite.DAL
{
    public abstract class DataContextProvider : IDataContextProvider
    {
        protected virtual NinjectIoCProvider CreateIoCProvider()
        {
            return new NinjectIoCProvider();
        }

        public virtual IDataContext Create()
        {
            return new DataContext(CreateIoCProvider());
        }
    }
}
