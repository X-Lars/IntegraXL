using System;
using System.Linq;

namespace Integra.Core
{
    /// <summary>
    /// Provides methods to convert INTEGRA-7 array parameters.
    /// </summary>
    /// <remarks><i>Arrays of parameters are received as 0x08000000 where the leading array indicator byte of 0x08 has to be stripped.</i></remarks>
    public static class IntegraParameter
    {
        /// <summary>
        /// Converts an INTEGRA-7 parameter to an <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The actual value of the INTEGRA-7 parameter.</returns>
        /// <remarks><i>Removes the leading array indicator byte of 0x08 from the parameter and retreives the actual value.</i></remarks>
        public static int Get(this int value)
        {
            // Remove the leading 0x08
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                bytes.Reverse();

            return ((bytes[1] & 0x0F) << 8 | (bytes[2] & 0x0F) << 4 | (bytes[3] & 0x0F));
        }

        /// <summary>
        /// Converts an <see cref="int"/> to an INTEGRA-7 parameter.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>An INTEGRA-7 ready parameter.</returns>
        /// <remarks><i>Adds the leading array indicator byte of 0x08 and converts to an INTEGRA-7 ready parameter.</i></remarks>
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
