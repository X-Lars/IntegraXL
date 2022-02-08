using IntegraXL.Core;

namespace IntegraXL.Extensions
{
    /// <summary>
    /// Contains <see cref="IntegraAddress"/> extension methods.
    /// </summary>
    internal static class IntegraAddressExtensions
    {
        /// <summary>
        /// Gets wheter the current address is within the specified address range.
        /// </summary>
        /// <param name="instance">The current address.</param>
        /// <param name="min">The address range lower limit.</param>
        /// <param name="max">The address range upper limit.</param>
        /// <returns>True if the current address lies within range of the provided addresses.</returns>
        public static bool InRange(this IntegraAddress instance, IntegraAddress min, IntegraAddress max)
        {
            return min > max ? instance >= max && instance <= min : instance >= min && instance <= max;
        }

        /// <summary>
        /// Gets the studio set part from the current address.
        /// </summary>
        /// <param name="instance">The current address.</param>
        /// <returns>The studio set part.</returns>
        public static Parts GetStudioSetPart(this IntegraAddress instance)
        {
            return (Parts)((instance & 0x00000F00) >> 8);
        }

        /// <summary>
        /// Gets the temporary tone part from the current address.
        /// </summary>
        /// <param name="instance">The current address.</param>
        /// <returns>The temporary tone part.</returns>
        public static Parts GetTemporaryTonePart(this IntegraAddress instance)
        {
            // MSB: 0x19, 0x19, 0x19, 0x19, 0x20, ...
            // LSB: 0x00, 0x20, 0x40, 0x60, 0x00, ...

            int msb = (instance[0] - 0x19) << 2; // << 2 equals multiplication by 4
            int lsb = (instance[1] / 0x20);

            return (Parts)(lsb + msb);
        }
    }
}
