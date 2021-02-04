using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Common
{
    public static class ShortExtensions
    {
        public static byte[] GetBytes(this short value)
        {
            byte[] bytes = new byte[2];

            bytes[0] = (byte)((value >> 4) & 0x0F);
            bytes[1] = (byte)((value & 0x0F));

            return bytes;
        }

    }
}
