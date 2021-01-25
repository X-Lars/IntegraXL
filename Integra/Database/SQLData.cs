using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Database
{
    public class SQLData
    {
        public SQLData(int offset, Type type, int size, string name, object value = null)
        {
            Offset = offset;
            Type = type;
            Size = size;
            Name = name;
            Value = value;
        }

        public int Offset { get; private set; }
        public Type Type { get; private set; }
        public int Size { get; private set; }
        public string Name { get; private set; }
        public object Value { get; set; }
    }
}
