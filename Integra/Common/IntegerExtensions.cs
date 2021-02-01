using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Common
{
    public static class UIntExtensions
    {
        /// <summary>
        /// Filter the <see cref="uint"/> based on the provided filter and mask.
        /// </summary>
        /// <param name="value">The <see cref="uint"/> to filter.</param>
        /// <param name="filter">The <see cref="uint"/> providing the value to filter.</param>
        /// <param name="mask">The <see cref="uint"/> providing the mask for filtering.</param>
        /// <returns>A <see cref="bool"/> containing true if the masked <paramref name="value"/> matches masked <paramref name="filter"/>.</returns>
        public static bool Filter(this uint value, uint filter, uint mask)
        {
            return (value & mask) == (filter & mask);
        }
    }

    public static class IntExtensions
    {

        /// <summary>
        /// Gets the parameter value without the INTEGRA-7 parameter prefix.
        /// </summary>
        /// <param name="value">The <see cref="int"/> prefixed parameter.</param>
        /// <returns>The <paramref name="value"/> without the parameter prefix.</returns>
        public static int ConvertFromIntegraParameter(this int value)
        {
            byte[] values = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(values);

            
            return ((values[1] & 0x0F) << 8 | (values[2] & 0x0F) << 4 | (values[3] & 0x0F));
        }

        /// <summary>
        /// Prefixes the value with the INTEGRA-7 parameter prefix of 0x08.
        /// </summary>
        /// <param name="value">The <see cref="int"/> to prefix.</param>
        /// <returns>The <paramref name="value"/> with the parameter prefix.</returns>
        public static int ConvertToIntegraParameter(this int value)
        {
            byte[] result = new byte[4];

            result[0] = 0x08;
            result[1] = (byte)((value >> 8) & 0x0F);
            result[2] = (byte)((value >> 4) & 0x0F);
            result[3] = (byte)((value & 0x0F));

            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return BitConverter.ToInt32(result, 0);
        }

        /// <summary>
        /// Invalidates the <see cref="int"/> is in the specified range.
        /// </summary>
        /// <param name="value">An <see cref="int"/> to invalidate.</param>
        /// <param name="min">An <see cref="int"/> specifying the minimum value.</param>
        /// <param name="max">An <see cref="int"/> specifying the maximum value.</param>
        /// <returns>The invalidated <paramref name="value"/>.</returns>
        public static int InvalidateRange(this int value, int min, int max)
        {
            value = Math.Min(value, max);
            value = Math.Max(value, min);

            return value;
        }

        /// <summary>
        /// Filter the <see cref="int"/> based on the provided filter and mask.
        /// </summary>
        /// <param name="value">The <see cref="int"/> to filter.</param>
        /// <param name="filter">The <see cref="int"/> providing the value to filter.</param>
        /// <param name="mask">The <see cref="int"/> providing the mask for filtering.</param>
        /// <returns>A <see cref="bool"/> containing true if the masked <paramref name="value"/> matches masked <paramref name="filter"/>.</returns>
        public static bool Filter(this int value, int filter, int mask)
        {
            return (value & mask) == (filter & mask);
        }

        /// <summary>
        /// Invalidates the <see cref="int"/> is in the range of the specified type of enumeration.
        /// </summary>
        /// <param name="value">An <see cref="int"/> to invalidate.</param>
        /// <returns>The invalidated <paramref name="value"/>.</returns>
        public static int InvalidateRange<T>(this int value) where T: Enum
        {
            value = Math.Min(value, Enum.GetValues(typeof(T)).Cast<byte>().Max());
            value = Math.Max(value, Enum.GetValues(typeof(T)).Cast<byte>().Min());

            return value;
        }
    }
}
