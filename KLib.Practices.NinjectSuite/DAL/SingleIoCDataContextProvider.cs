using System;

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
