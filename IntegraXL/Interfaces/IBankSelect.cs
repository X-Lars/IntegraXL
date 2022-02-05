namespace IntegraXL.Interfaces
{
    /// <summary>
    /// Contract to exchange and compare tones by there bank selection MSB, LSB and PC.
    /// </summary>
    public interface IBankSelect : IEquatable<IBankSelect>
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
    }
}
