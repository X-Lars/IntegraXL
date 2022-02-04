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
        /// Creates and initializes a new <see cref="ToneTemplate"/>.
        /// </summary>
        /// <param name="id">The ID of the tone.</param>
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
        /// Gets the (M)ost (S)ignificant (B)yte to request the tone.
        /// </summary>
        public byte MSB { get; }

        /// <summary>
        /// Gets the (L)east (S)ignificant (B)yte to request the tone.
        /// </summary>
        public byte LSB { get; }

        /// <summary>
        /// Gets the (P)rogram (C)hange to request the tone.
        /// </summary>
        public byte PC { get; }

        /// <summary>
        /// Gets whether this <see cref="IBankSelect"/> interface equals the provided <see cref="IBankSelect"/> interface.
        /// </summary>
        /// <param name="compare">The <see cref="IBankSelect"/> interface to compare.</param>
        /// <returns>True if both <see cref="IBankSelect"/> interfaces have equal property values.</returns>
        public bool Equals(IBankSelect? compare)
        {
            if (compare == null)
                return false;

            return MSB == compare.MSB && LSB == compare.LSB && PC == compare.PC;
        }

        /// <summary>
        /// Gets whether this <see cref="IBankSelect.MSB"/>, <see cref="IBankSelect.LSB"/> and <see cref="IBankSelect.PC"/> equal the bytes of the provided <see cref="byte"/>[] array.
        /// </summary>
        /// <param name="compare">The <see cref="byte"/>[] array to compare.</param>
        /// <returns>True if this <see cref="IBankSelect"/> interface property values equal the <see cref="byte"/>[] array in <i>respective</i> order.</returns>
        public bool Equals(byte[]? compare)
        {
            if (compare == null)
                return false;

            if (compare.Length != 3)
                return false;

            return MSB == compare[0] && LSB == compare[1] && PC == compare[2];
        }

        #endregion

        #region Overrides: Object

        /// <summary>
        /// Provides a user friendly <see cref="string"/> representation of the template.
        /// </summary>
        /// <returns>A user friendly <see cref="string"/> representation of the template.</returns>
        public override string ToString()
        {
            return $"{ID:0000} {Name, -15} [{Category}] (0x{MSB:X2} 0x{LSB:X2} 0x{PC:X2})";
        }

        #endregion
    }
}
