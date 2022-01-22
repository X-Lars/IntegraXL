using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Interfaces
{
    public interface IBankSelect : IEquatable<IBankSelect>, IEquatable<byte[]>
    {
        public byte MSB { get; }
        public byte LSB { get; }
        public byte PC  { get; }
    }
}
