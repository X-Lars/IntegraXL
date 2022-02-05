using IntegraXL.Core;
using System.Text;

namespace IntegraXL.Templates
{
    /// <summary>
    /// Defines a structure to hold immutable studio set data.
    /// </summary>
    [Integra(0x0F000302, 0x00000015)]
    public sealed class StudioSetTemplate : IntegraTemplate<StudioSetTemplate>
    {
        #region Constructor

        /// <summary>
        /// Creates and initializes a new <see cref="StudioSetTemplate"/> instance.
        /// </summary>
        /// <param name="id">The ID of the template for display purpose.</param>
        /// <param name="data">The data to initialize the template.</param>
#pragma warning disable IDE0051 // Remove unused private members
        private StudioSetTemplate(int id, byte[] data) : base(id, data)
        {
            MSB  = data[0];
            LSB  = data[1];
            PC   = data[2];

            Name = Encoding.ASCII.GetString(data, 5, 16);
        }
#pragma warning restore IDE0051 // Remove unused private members

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the studio set.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the (M)ost (S)ignificant (B)yte to select the studio set.
        /// </summary>
        public byte MSB { get; }

        /// <summary>
        /// Gets the (L)east (S)ignificant (B)yte to select the studio set.
        /// </summary>
        public byte LSB { get; }

        /// <summary>
        /// Gets the (P)rogram (C)hange to select the studio set.
        /// </summary>
        public byte PC { get; }

        #endregion

        #region Overrides: Object

        /// <summary>
        /// Provides string that represents the current template.
        /// </summary>
        /// <returns>A string that represents the current template.</returns>
        public override string ToString()
        {
            return $"{ID:00} {Name, -15} (0x{MSB:X2} 0x{LSB:X2} 0x{PC:X2})";
        }

        #endregion
    }
}
