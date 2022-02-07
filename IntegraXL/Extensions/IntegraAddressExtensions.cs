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
    }
}
