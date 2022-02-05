namespace IntegraXL.Core
{
    /// <summary>
    /// Defines an address based structure to specify parameters to generate a data request to the INTEGRA-7.
    /// </summary>
    public sealed class IntegraRequest : IntegraAddress
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraRequest"/> instance with default value.
        /// </summary>
        /// <remarks><i>Defaults to 0x00000000.</i></remarks>
        public IntegraRequest() : base() { }

        /// <summary>
        /// Creates a new <see cref="IntegraRequest"/> instance initialized with the specified address.
        /// </summary>
        /// <param name="address">The address to initialize.</param>
        /// <remarks><i>The address is split into a byte array and requires each byte to be within the MIDI range.</i> [0x00..0x7F]</remarks>
        public IntegraRequest(int address) : base(address) { }

        /// <summary>
        /// Creates a new <see cref="IntegraRequest"/> instance initialized with the specified address.
        /// </summary>
        /// <param name="address">The address to initialize.</param>
        /// <remarks><i>The address is required to be 4 bytes in length and requires each byte to be within the MIDI range.</i> [0x00..0x7F]</remarks>
        public IntegraRequest(byte[] address) : base(address) { }

        /// <summary>
        /// Creates a new <see cref="IntegraRequest"/> instance initialized with the specified individual address bytes.
        /// </summary>
        /// <param name="byte01">The address MSB.</param>
        /// <param name="byte02">The second address byte.</param>
        /// <param name="byte03">The third address byte.</param>
        /// <param name="byte04">The address LSB.</param>
        /// <remarks><i>All bytes are required to be within the MIDI range.</i> [0x00..0x7F]</remarks>
        public IntegraRequest(byte byte01, byte byte02, byte byte03, byte byte04) : base(new byte[] { byte01, byte02, byte03, byte04 }) { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the size of the request based on the last 16 bits of the request.
        /// </summary>
        /// <remarks><i>The size returned is the MIDI size.</i></remarks>
        public int Size
        {
            get { return this[2] * 128 + this[3]; }
        }

        /// <summary>
        /// Gets the (M)ost (S)ignificant (B)yte of the first 16 bits of the request.
        /// </summary>
        public byte MSB { get { return this[0]; } }

        /// <summary>
        /// Gets the (L)east (S)ignificant (B)yte of the first 16 bits of the request.
        /// </summary>
        public byte LSB { get { return this[1]; } }

        #endregion
    }
}
