using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KLib.Practices.Common.AppService
{
    public class MarshalService : IMarshalService
    {
        public object CopyProperties(object source, object target)
        {
            IEnumerable<string> changes = null;
            return CopyProperties(source, target, out changes);
        }

        public T CopyProperties<T>(object source, T target)
        {
            return (T) CopyProperties(source, (object) target);
        }

        public object CopyProperties(object source, object target, out IEnumerable<string> changes)
        {
            var sourceProperties = GetPropertyInfos(source.GetType());
            var targetProperties = GetPropertyInfos(target.GetType());
            var changedPropList = new List<string>();
            foreach (var propName in sourceProperties.Keys)
            {
                if (!targetProperties.ContainsKey(propName)) continue;

                var sourceProp = sourceProperties[propName];
                var targetProp = targetProperties[propName];
                if (!sourceProp.CanRead ||
                    !targetProp.CanWrite ||
                    sourceProp.PropertyType != targetProp.PropertyType) continue;

                var value = sourceProp.GetValue(source);
                targetProp.SetValue(target, value);
                changedPropList.Add(propName);
            }
            changes = changedPropList.AsEnumerable();
            return target;
        }

        public T CopyProperties<T>(object source, T target, out IEnumerable<string> changes)
        {
            return (T) CopyProperties(source, (object)target, out changes);            
        }

        private static Dictionary<string, PropertyInfo> GetPropertyInfos(Type targetType)
        {
            return targetType.GetProperties()
                             .ToDictionary(propInfo => propInfo.Name);
        } 
    }
}
