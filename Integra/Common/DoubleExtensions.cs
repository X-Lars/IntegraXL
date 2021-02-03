using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Common
{
    public static class DoubleExtensions
    {
        /// <summary>
        /// Invalidates the <see cref="int"/> is in the specified range.
        /// </summary>
        /// <param name="value">An <see cref="int"/> to invalidate.</param>
        /// <param name="min">An <see cref="int"/> specifying the minimum value.</param>
        /// <param name="max">An <see cref="int"/> specifying the maximum value.</param>
        /// <returns>The invalidated <paramref name="value"/>.</returns>
        public static int InvalidateRange(this double value, int min, int max)
        {
            value = Math.Min(value, max);
            value = Math.Max(value, min);

            return (int)value;
        }

        /// <summary>
        /// Invalidates the <see cref="int"/> is in the range of the specified type of enumeration.
        /// </summary>
        /// <param name="value">An <see cref="int"/> to invalidate.</param>
        /// <returns>The invalidated <paramref name="value"/>.</returns>
        public static int InvalidateRange<T>(this double value) where T : Enum
        {
            value = Math.Min(value, Enum.GetValues(typeof(T)).Cast<byte>().Max());
            value = Math.Max(value, Enum.GetValues(typeof(T)).Cast<byte>().Min());

            return (int)value;
        }
    }
}
