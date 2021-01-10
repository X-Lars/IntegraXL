using MidiXL;

namespace Integra.Core
{
    /// <summary>
    /// Enumeration defining all possible system exclusive modes.
    /// </summary>
    public enum SystemExclusiveModes : byte
    {
        /// <summary>A system exclusive data request.</summary>
        Request = 0x11,

        /// <summary>A system exclusive data transmission.</summary>
        Transmit = 0x12
    }

    /// <summary>
    /// INTEGRA-7 System exclusive data structure provides conversion and checksum functionality for system exclusive messages.
    /// </summary>
    public class IntegraSystemExclusive
    {
        #region Fields

        private static readonly byte[] _SystemExclusiveBegin = { (byte)SystemExclusiveMessageTypes.SystemExclusive };
        private static readonly byte[] _ManufacturerID       = { 0x41 };
        private readonly byte[]        _DeviceID             = { 0x10 };
        private static readonly byte[] _ModelID              = { 0x00, 0x00, 0x64 };
        private readonly byte[]        _Mode                 = { (byte)SystemExclusiveModes.Transmit };
        private readonly byte[]        _Address              = { };
        private readonly byte[]        _Data                 = { };
        private byte[]                 _Checksum             = { };
        private static readonly byte[] _SystemExclusiveEnd   = { (byte)SystemExclusiveMessageTypes.SystemExclusiveEnd };
        
        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes a new <see cref="IntegraSystemExclusive"/> instance initialized from the provided <paramref name="systemExclusiveMessage"/>.
        /// </summary>
        /// <param name="systemExclusiveMessage">A <see cref="SystemExclusiveMessage"/> to initialize the <see cref="IntegraSystemExclusive"/>.</param>
        /// <remarks><i>Use for conversion of received system exclusive messages.</i></remarks>
        public IntegraSystemExclusive(SystemExclusiveMessage systemExclusiveMessage)
        {
            // Read variable bytes from the system exclusive message
            _DeviceID               = new byte[] { systemExclusiveMessage.Data[2] };
            _Address                = new byte[] { systemExclusiveMessage.Data[7], systemExclusiveMessage.Data[8], systemExclusiveMessage.Data[9], systemExclusiveMessage.Data[10] };

            // Set the size of the data array
            _Data                   = new byte[systemExclusiveMessage.Data.Length - IntegraConstants.SYSTEM_EXCLUSIVE_HEADER_SIZE - IntegraConstants.ADDRESS_SIZE - 2];

            int offset = IntegraConstants.SYSTEM_EXCLUSIVE_HEADER_SIZE + IntegraConstants.ADDRESS_SIZE;

            // Initialize the data array
            for (int i = offset; i < _Data.Length; i++)
            {
                _Data[i - offset] = systemExclusiveMessage.Data[i];
            }
        }

        /// <summary>
        /// Creates and initializes a new <see cref="IntegraSystemExclusive"/> instance to request data from the INTEGRA-7.
        /// </summary>
        /// <param name="address">An <see cref="IntegraAddress"/> specifiying the address of the data to request.</param>
        /// <param name="request">An <see cref="IntegraAddress"/> specifiying the request to obtain the data.</param>
        public IntegraSystemExclusive(IntegraAddress address, IntegraAddress request)
        {
            _Address = address;
            _Data = request;
            _Mode = new byte[] { (byte)SystemExclusiveModes.Request };

            InvalidateChecksum();
        }

        /// <summary>
        /// Creates and initializes new <see cref="IntegraSystemExclusive"/> instance to transmit data to the INTEGRA-7.
        /// </summary>
        /// <param name="address">An <see cref="IntegraAddress"/> containing the base address of the value to transmit.</param>
        /// <param name="offset">An <see cref="ushort"/> containing the offset into the base address of the value to transmit.</param>
        /// <param name="value">A <see cref="byte[]"/> containing the value to transmit.</param>
        /// <remarks><i>The address represents the addres of the class, the offset represents the offset of the property into the class.</i></remarks>
        public IntegraSystemExclusive(IntegraAddress address, uint offset, byte[] value)
        {
            _Address = address + offset;
            _Data = value;
            _Mode = new byte[] { (byte)SystemExclusiveModes.Transmit };

            InvalidateChecksum();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the address of the <see cref="IntegraSystemExclusive"/>.
        /// </summary>
        public IntegraAddress Address
        {
            get { return _Address; }
        }

        /// <summary>
        /// Gets the data part of the <see cref="IntegraSystemExclusive"/>.
        /// </summary>
        public byte[] Data
        {
            get { return _Data; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calculates and sets the checksum of the <see cref="IntegraSystemExclusive"/>.
        /// </summary>
        /// <returns>A <see cref="byte"/> containg the checksum.</returns>
        private byte InvalidateChecksum()
        {
            int checkSum = 0;

            for (uint i = 0; i < IntegraConstants.ADDRESS_SIZE; i++)
            {
                checkSum += Address[i];
            }

            if (Data != null)
            {
                for (uint i = 0; i < Data.Length; i++)
                {
                    checkSum += Data[i];
                }
            }

            checkSum %= 128;
            checkSum = 128 - checkSum;

            _Checksum = new byte[] { (byte)checkSum };

            return (byte)checkSum;
        }

        #endregion
    }
}
