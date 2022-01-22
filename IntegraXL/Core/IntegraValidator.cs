using IntegraXL.Extensions;
using System.Diagnostics;

namespace IntegraXL.Core
{
    /// <summary>
    /// Base class for INTEGRA-7 parameter validation.
    /// </summary>
    public abstract class IntegraValidator
    {
        public abstract double Get(int index, int value);
        public abstract int Set(int index, double value);
    }

    /// <summary>
    /// Base class for INTEGRA-7 MFX parameter validation.
    /// </summary>
    public abstract class IntegraMFXValidator : IntegraValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">The parameter index.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>Retreive the deserialized parameter by calling <see langword="base"/>.<see cref="Get(int, int)"/> before modifcation.</i>
        /// </remarks>
        public override double Get(int index, int value)
        {
            return value.DeserializeMFX();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">The parameter index.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>Method requires to return the <see langword="base"/>.<see cref="Set(int, double)"/>.</i>
        /// </remarks>
        public override int Set(int index, double value)
        {
            return ((int)value).SerializeMFX();
        }

        /// <summary>
        /// Invalidates the specified value against the provide enumeration range.
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type.</typeparam>
        /// <param name="value">The value to invalidate.</param>
        /// <returns>The invalidated value.</returns>
        public static int Invalidate<TEnum>(double value) where TEnum : Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), (byte)value))
                throw new IntegraException($"[{nameof(IntegraMFXValidator)}].{nameof(Invalidate)} {typeof(TEnum).Name} Undefined enumeration value.");

            value = Math.Min(value, Enum.GetValues(typeof(TEnum)).Cast<byte>().Max());
            value = Math.Max(value, Enum.GetValues(typeof(TEnum)).Cast<byte>().Min());

            return (int)value;
        }

        /// <summary>
        /// Invalidates the specified value against the specified range.
        /// </summary>
        /// <param name="value">The value to invalidate.</param>
        /// <param name="min">The minimum value of the range.</param>
        /// <param name="max">The maximum Value of the range.</param>
        /// <returns>The invalidated value.</returns>
        public static int Invalidate(double value, int min, int max)
        {
            value = Math.Min(value, max);
            value = Math.Max(value, min);

            return (int)value;
        }
    }
}
