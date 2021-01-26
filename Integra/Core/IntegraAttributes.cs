using System;

namespace Integra.Core
{
    /// <summary>
    /// Specifies the property or field offset into the base address.
    /// </summary>
    /// <remarks><i>Used for reflection to determin the property or field offset.</i></remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    internal class Offset : Attribute
    {
        /// <summary>
        /// Creates and initalizes a new <see cref="Offset"/> attribute instance.
        /// </summary>
        /// <param name="offset">The offset specifying the offset into the base address.</param>
        /// <param name="type">Specifies the type of offset.</param>
        /// <remarks><i>Offset types</i></remarks>
        public Offset(ushort offset = 0x0000)
        {
            Value = offset;
        }

        /// <summary>
        /// Gets the offset of the associated property or field.
        /// </summary>
        public ushort Value { get; }
    }

    /// <summary>
    /// Specifies the database table associated with the class.
    /// </summary>
    /// <remarks><i>Used for reflection to override the default table naming convention.</i></remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class Table : Attribute
    {
        /// <summary>
        /// Creates and initializes a new <see cref="Table"/> attribute instance.
        /// </summary>
        /// <param name="name">The name of the table associated with the class.</param>
        public Table(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of the table associated with the class.
        /// </summary>
        public string Name { get; }
    }
}
