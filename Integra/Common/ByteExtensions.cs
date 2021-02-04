using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Common
{
    public static class ByteExtensions
    {
        /// <summary>
        /// Applies an offset to the <see cref="byte"/>.
        /// </summary>
        /// <param name="value">The value to offset.</param>
        /// <param name="offset">The offset to apply.</param>
        /// <returns></returns>
        public static byte Offset(this byte value, int offset)
        {
            return (byte)(value + offset);
        }

        public static short GetShort(this byte[] value)
        {
            // TODO: Check Result
            return (short)((value[0] & 0x0F) << 4 | (value[1] & 0x0F));
        }

       
    }
}
