using IntegraXL.Core;
using IntegraXL.Interfaces;
using System.Text;

namespace IntegraXL.Templates
{
    /// <summary>
    /// Defines a structure to hold immutable tone data.
    /// </summary>
    [Integra(0x0F000402, 0x00000015)]
    public sealed class ToneTemplate : IntegraTemplate<ToneTemplate>, IBankSelect
    {
        #region Constructor

        /// <summary>
        /// Creates and initializes a new <see cref="ToneTemplate"/> instance.
        /// </summary>
        /// <param name="id">The ID of the template for display purpose.</param>
        /// <param name="data">The data to initialize the template.</param>
#pragma warning disable IDE0051 // Remove unused private members
        private ToneTemplate(int id, byte[] data) : base(id, data)
        {
            MSB = data[0];
            LSB = data[1];
            PC  = data[2];

            Category = (IntegraToneCategories)data[3];
            Name     = Encoding.ASCII.GetString(data, 5, 12);
        }
#pragma warning restore IDE0051 // Remove unused private members

        #endregion

        #region Properties

        /// <summary>
        /// Gets the tone name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the tone category.
        /// </summary>
        public IntegraToneCategories Category { get; }

        #endregion

        #region Interface: IBankSelect

        /// <summary>
        /// Gets the (M)ost (S)ignificant (B)yte to select the tone.
        /// </summary>
        public byte MSB { get; }

        /// <summary>
        /// Gets the (L)east (S)ignificant (B)yte to select the tone.
        /// </summary>
        public byte LSB { get; }

        /// <summary>
        /// Gets the (P)rogram (C)hange byte to select the tone.
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
            return $"{ID:0000} {Name, -15} [{Category}] (0x{MSB:X2} 0x{LSB:X2} 0x{PC:X2})";
        }

        #endregion
    }
}
