using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLib.Practices.Common.AppService
{
    public interface IMarshalService
    {
        object CopyProperties(object source, object target);
        T CopyProperties<T>(object source, T target);
        object CopyProperties(object source, object target, out IEnumerable<string> changes);
        T CopyProperties<T>(object source, T target, out IEnumerable<string> changes);
    }
}
