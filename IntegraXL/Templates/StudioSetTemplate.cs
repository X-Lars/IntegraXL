using IntegraXL.Core;
using IntegraXL.Models;
using System.Text;

namespace IntegraXL.Templates
{
    /// <summary>
    /// Defines a structure to hold immutable studio set info.
    /// </summary>
    [Integra(0x0F000302, 0x00000015)]
    public sealed class StudioSetTemplate : IntegraTemplate<StudioSetTemplate>
    {
        #region Constructor

        /// <summary>
        /// Creates and initializes a new <see cref="StudioSetTemplate"/>.
        /// </summary>
        /// <param name="id">The ID of the studio set.</param>
        /// <param name="data">The data to initialize the template.</param>
#pragma warning disable IDE0051 // Remove unused private members
        private StudioSetTemplate(int id, byte[] data) : base(id, data)
        {
            Name = Encoding.ASCII.GetString(data, 5, 16);
            MSB  = data[0];
            LSB  = data[1];
            PC   = data[2];
        }
#pragma warning restore IDE0051 // Remove unused private members

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the studio set.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the (M)ost (S)ignificant (B)yte to request the studio set.
        /// </summary>
        public byte MSB { get; }

        /// <summary>
        /// Gets the (L)east (S)ignificant (B)yte to request the studio set.
        /// </summary>
        public byte LSB { get; }

        /// <summary>
        /// Gets the (P)rogram (C)hange to request the studio set.
        /// </summary>
        public byte PC { get; }

        #endregion

        #region Overrides: Object

        /// <summary>
        /// Provides a user friendly <see cref="string"/> representation of the template.
        /// </summary>
        /// <returns>A user friendly <see cref="string"/> representation of the template.</returns>
        public override string ToString()
        {
            return $"{ID:0000} {Name, -15} (0x{MSB:X2} 0x{LSB:X2} 0x{PC:X2})";
        }

        #endregion
    }
}
