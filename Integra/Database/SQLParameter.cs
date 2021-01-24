using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Database
{
    public class SQLParameter
    {
        public SQLParameter(int offset, Type type, string name, object value)
        {
            Offset = offset;
            Type = type;
            Value = value;
            Name = name;
        }
        public object Value { get; private set; }

        public Type Type { get; private set; }

        public int Offset { get; private set; }

        public string Name { get; set; }
    }
}
