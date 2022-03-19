namespace IntegraXL.Interfaces
{
    /// <summary>
    /// Contract to exchange and compare tones by there bank select MSB, LSB and PC.
    /// </summary>
    public interface IBankSelect
    {
        /// <summary>
        /// The (M)ost (S)ignificant (B)yte of the bank select.
        /// </summary>
        /// <remarks><i>Controller 0.</i></remarks>
        public byte MSB { get; }

        /// <summary>
        /// The (L)east (S)ignificant (B)yte of the bank select.
        /// </summary>
        /// <remarks><i>Controller 32.</i></remarks>
        public byte LSB { get; }

        /// <summary>
        /// The (P)rogram (C)hange byte of the bank select.
        /// </summary>
        public byte PC  { get; }

        /// <summary>
        /// Gets wheter this <see cref="IBankSelect"/>'s property values equal the provided <see cref="IBankSelect"/>'s property values.
        /// </summary>
        /// <param name="bankselect">The bank select interface to compare.</param>
        /// <returns>True if both <see cref="MSB"/>, <see cref="LSB"/> and <see cref="PC"/> property values are equal.</returns>
        public virtual bool Equals(IBankSelect bankselect) 
        { 
            return MSB == bankselect.MSB && LSB == bankselect.LSB && PC == bankselect.PC; 
        }
    }
}
