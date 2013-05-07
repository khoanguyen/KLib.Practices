using System;
using System.Linq;
using KLib.Practices.Common.DependencyInjection;
using Ninject;
using Ninject.Parameters;

namespace KLib.Practices.NinjectSuite
{
    public class NinjectIoCProvider : IIoCProvider
    {
        public IKernel Kernel { get; private set; }

        public NinjectIoCProvider()
        {
            Kernel = new StandardKernel();            
        }

        public T CreateInstance<T>(object constructorParams = null)
        {
            return (T)CreateInstance(typeof(T), constructorParams);
        }

        public object CreateInstance(Type type, object constructorParams = null)
        {
            return Kernel.Get(type, GetParameters(constructorParams));
        }

        public T CreateInstance<T>(string name, object constructorParams = null)
        {
            return (T)CreateInstance(typeof(T), name, constructorParams);
        }

        public object CreateInstance(Type type, string name, object constructorParams = null)
        {
            return Kernel.Get(type, name, GetParameters(constructorParams));
        }

        public void Inject(object target)
        {
            Kernel.Inject(target);
        }

        private static IParameter[] GetParameters(object input)
        {
            if (input == null) return new IParameter[0];
            var fields = input.GetType().GetFields();
            return fields.Select(field => new ConstructorArgument(field.Name, field.GetValue(input)))
                         .Cast<IParameter>()
                         .ToArray();
        }
    }
}
