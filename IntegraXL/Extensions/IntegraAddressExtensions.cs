using IntegraXL.Core;
using System.Diagnostics;

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
        /// <param name="min">The address range inclusive lower limit.</param>
        /// <param name="max">The address range inclusive upper limit.</param>
        /// <returns>True if the current address lies within range of the provided addresses.</returns>
        internal static bool InRange(this IntegraAddress instance, IntegraAddress min, IntegraAddress max)
        {
            Debug.Assert(max >= min);

            return instance >= min && instance <= max;
        }

        /// <summary>
        /// Gets the studio set part from the current address.
        /// </summary>
        /// <param name="instance">The current address.</param>
        /// <returns>The studio set part.</returns>
        internal static Parts GetStudioSetPart(this IntegraAddress instance)
        {
            Debug.Assert(instance.InRange(0x18001000, 0x18005F00));

            return (Parts)((instance & 0x00000F00) >> 8);
        }

        /// <summary>
        /// Gets the temporary tone part from the current address.
        /// </summary>
        /// <param name="instance">The current address.</param>
        /// <returns>The temporary tone part.</returns>
        internal static Parts GetTemporaryTonePart(this IntegraAddress instance)
        {
            Debug.Assert(instance.InRange(0x19000000, 0x1D000000));

            // MSB: 0x19, 0x19, 0x19, 0x19, 0x20, ...
            // LSB: 0x00, 0x20, 0x40, 0x60, 0x00, ...

            int msb = (instance[0] - 0x19) * 4;
            int lsb = (instance[1] / 0x20);

            return (Parts)(msb + lsb);
        }
    }
}
