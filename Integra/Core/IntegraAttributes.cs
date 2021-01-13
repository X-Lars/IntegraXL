using System;

namespace Integra.Core
{
    /// <summary>
    /// Specifies <see cref="IntegraBase{T}"/> derived class property or field address offset into the base address.
    /// </summary>
    /// <remarks><i>Used for reflection inside the <see cref="IntegraBase{T}"/> to determin the property or field offset.</i></remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class Offset : Attribute
    {
        /// <summary>
        /// Creates and initalizes a new <see cref="Offset"/> attribute instance.
        /// </summary>
        /// <param name="offset">An <see cref="ushort"/> specifying the offset into the base address.</param>
        public Offset(ushort offset = 0x0000)
        {
            Value = offset;
        }

        /// <summary>
        /// Gets the attribute address offset.
        /// </summary>
        public ushort Value { get; }
    }
}
