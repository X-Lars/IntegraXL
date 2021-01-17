using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks><i>Parameters are received as 0x08000000</i></remarks>
    public static class IntegraParameter
    {
        public static int Get(this int value)
        {
            // Remove the leading 0x08
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                bytes.Reverse();

            return ((bytes[1] & 0x0F) << 8 | (bytes[2] & 0x0F) << 4 | (bytes[3] & 0x0F));
        }

        public static int Set(this int value)
        {
            byte[] bytes = new byte[4];

            bytes[0] = 0x08;
            bytes[1] = (byte)((value >> 8) & 0x0F);
            bytes[2] = (byte)((value >> 4) & 0x0F);
            bytes[3] = (byte)((value & 0x0F));

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
