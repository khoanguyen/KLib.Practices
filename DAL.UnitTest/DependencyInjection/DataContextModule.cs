using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLib.Practices.DAL;
using KLib.Practices.NinjectSuite;
using Ninject.Modules;

namespace DAL.UnitTest.DependencyInjection
{
    public class DataContextModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof (GenericRepository<,>)).ToSelf();
            //Bind<GenericRepository<ObjectContextModel.ObjectContextTestModelContainer, ObjectContextModel.User>>()
            //    .To<GenericRepository<ObjectContextModel.ObjectContextTestModelContainer, ObjectContextModel.User>>();
            //Bind<GenericRepository<ObjectContextModel.ObjectContextTestModelContainer, ObjectContextModel.Note>>()
            //    .To<GenericRepository<ObjectContextModel.ObjectContextTestModelContainer, ObjectContextModel.Note>>();
        }
    }
}
