using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.UnitTest.DependencyInjection;
using KLib.Practices.DAL;
using KLib.Practices.NinjectSuite;
using KLib.Practices.NinjectSuite.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DAL.UnitTest.Init
{
    [TestClass]
    public class Init
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            DefaultDataContextProvider.UpdateProviderSingleton(provider =>
                {
                    provider.Kernel.Load(new[] {new DataContextModule()});
                });
        }
    }
}
