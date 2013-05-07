using KLib.Practices.Common.DAL;
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
