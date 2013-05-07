using System;
using System.Data.Entity;
using System.IO;
using System.Reflection;

namespace KLib.Practices.Common.UnitTesting
{
    public class TestDataManager<TContext>
        where TContext : DbContext
    {
        private readonly string _prefix;
        private readonly Assembly _assembly;

        public TestDataManager(Assembly assembly, string prefix)
        {
            var name = assembly.FullName.Substring(0, assembly.FullName.IndexOf(',')).Trim();
            _prefix = string.Format("{0}.{1}", name, prefix);
            _assembly = assembly;
        }

        public void RunScript(string scriptName)
        {
            var resourceName = string.Format("{0}.{1}", _prefix, scriptName);
            using (var context = Activator.CreateInstance<TContext>())
            {
                var sql = ReadResource(_assembly, resourceName);
                context.Database.ExecuteSqlCommand(sql);
            }
        }

        private static string ReadResource(Assembly asm, string resouceName)
        {
            using (var stream = asm.GetManifestResourceStream(resouceName))
            {
                if (stream == null) return string.Empty;
                var reader = new StreamReader(stream);
                var result = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                return result;
            }
        }
    }
}
