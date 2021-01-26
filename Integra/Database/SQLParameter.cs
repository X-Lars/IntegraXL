using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Database
{
    public class SQLParameter
    {
        public SQLParameter(Type type, string name, object value)
        {
            Type = type;
            Value = value;
            Name = name;
        }
        public object Value { get; private set; }

        public Type Type { get; private set; }

        public string Name { get; set; }
    }
}
