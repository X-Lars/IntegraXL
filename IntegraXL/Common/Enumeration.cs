using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Common
{
    public interface IEnumeration
    {
        List<string> Values { get; }
    }

    public abstract class Enumeration : IEnumeration
    {
        public List<string> Values { get; }

        public int Value { get; set; }

        public override string ToString()
        {
            return Values[Value];
        }
    }
}
