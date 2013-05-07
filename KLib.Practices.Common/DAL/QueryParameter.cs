using System.Data.Objects;

namespace KLib.Practices.Common.DAL
{
    public class QueryParameter 
    {
        public string Name { get; private set; }
        public object Value { get; private set; }

        public QueryParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        internal ObjectParameter ToObjectParameter()
        {
            return new ObjectParameter(Name, Value);
        }        
    }
}
