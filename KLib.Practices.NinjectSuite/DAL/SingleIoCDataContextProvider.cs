using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLib.Practices.Common.DependencyInjection;

namespace KLib.Practices.NinjectSuite.DAL
{
    public class SingleIoCDataContextProvider : DataContextProvider
    {
        private static NinjectIoCProvider _provider;

        private static NinjectIoCProvider Provider
        {
            get
            {
                return _provider ?? (_provider = new NinjectIoCProvider());
            }
        }

        protected override NinjectIoCProvider CreateIoCProvider()
        {
            return Provider;
        } 

        public static void UpdateProviderSingleton(Action<NinjectIoCProvider> action)
        {
            action(Provider);            
        }
    }
}
