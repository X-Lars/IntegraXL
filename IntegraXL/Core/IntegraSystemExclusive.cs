using IntegraXL.Extensions;
using System.Diagnostics;

namespace IntegraXL.Core
{
    public class IntegraSystemExclusive
    {
        #region Constants

        /// <summary>
        /// Defines the system exclusive status byte.
        /// </summary>
        private const byte SYX_STATUS = 0xF0;

        /// <summary>
        /// Defines the Roland's MIDI manufacturers ID.
        /// </summary>
        private const byte SYX_MANUFACTURER = 0x41;

        /// <summary>
        /// Defines the LSB of the INTEGRA-7 model ID.
        /// </summary>
        private const byte SYX_MODEL = 0x64;

        /// <summary>
        /// Defines the system exclusive mode to request data.
        /// </summary>
        private const byte SYX_MODE_RX = 0x11;

        /// <summary>
        /// Defines the system exclusive mode to transmit data.
        /// </summary>
        private const byte SYX_MODE_TX = 0x12;

        /// <summary>
        /// Defines the system exclusive closing byte.
        /// </summary>
        private const byte SYX_END = 0xF7;

        /// <summary>
        /// Defines the fixed index of the system exclusive address part.
        /// </summary>
        private const byte SYX_ADDRESS_IDX = 7;

        /// <summary>
        /// Defines the fixed index of the system exclusive data part.
        /// </summary>
        private const byte SYX_DATA_IDX = 11;

        /// <summary>
        /// Defines the fixed size the system exclusive.
        /// </summary>
        /// <remarks><i>Fixed size without the variable sized data part.</i></remarks>
        private const byte SYX_FIXED_SIZE = 13;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes a new system exclusive message to request data.
        /// </summary>
        /// <param name="address">The physical INTEGRA-7 address of the data.</param>
        /// <param name="request">The data request.</param>
        /// <remarks><i>Request</i></remarks>
        public IntegraSystemExclusive( IntegraAddress address, IntegraRequest request)
        {
            Address = address;
            Data = request;

            CalculateChecksum();
        }

        /// <summary>
        /// Creates and initializes a new system exclusive message from received data.
        /// </summary>
        /// <param name="raw">The raw MIDI data.</param>
        /// <exception cref="IntegraException"></exception>
        public IntegraSystemExclusive(byte[] raw)
        {
            Address = raw.GetArrayPart(SYX_ADDRESS_IDX, 4);
            Data    = raw.GetArrayPart(SYX_DATA_IDX, raw.Length - SYX_FIXED_SIZE);
        }

        /// <summary>
        /// Creates and initializes a new system exclusive message to transmit data.
        /// </summary>
        /// <param name="address">The physical INTEGRA-7 base address of the model.</param>
        /// <param name="offset">The property offset into the model's base address.</param>
        /// <param name="data">The serialized MIDI data.</param>
        public IntegraSystemExclusive(IntegraAddress address, IntegraAddress offset, byte[] data)
        {
            Debug.Assert(data.All(x => x <= IntegraConstants.MAX_MIDI_VALUE));

            Address = address + offset;
            Data = data;
            Mode = SYX_MODE_TX;

            CalculateChecksum();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the system exclusive address.
        /// </summary>
        public IntegraAddress Address { get; private set; } = new byte[4];

        /// <summary>
        /// Gets the system exclusive device ID.
        /// </summary>
        public byte DeviceID { get; internal set; } = 0x10;

        /// <summary>
        /// Gets the system exclusive mode, either <see cref="SYX_MODE_RX"/> or <see cref="SYX_MODE_TX"/>.
        /// </summary>
        /// <remarks><i>Defaults to <see cref="SYX_MODE_RX"/>.</i></remarks>
        public byte Mode { get; private set; } = SYX_MODE_RX;

        /// <summary>
        /// Gets the system exclusive data part.
        /// </summary>
        public byte[] Data { get; private set; }

        /// <summary>
        /// Gets the system exclusive checsum.
        /// </summary>
        public byte Checksum { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Calculates and sets the system exclusive checksum.
        /// </summary>
        /// <returns>The calculated checksum.</returns>
        private byte CalculateChecksum()
        {
            int checkSum = 0;

            for (int i = 0; i < 4; i++)
            {
                checkSum += Address[i];
            }

            if (Data != null)
            {
                for (int i = 0; i < Data.Length; i++)
                {
                    checkSum += Data[i];
                }
            }

            checkSum %= 128;

            if (checkSum == 0)
                return Checksum = 0;

            checkSum  = 128 - checkSum;

            return Checksum = (byte)checkSum;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Makes the <see cref="IntegraSystemExclusive"/> assignable to a <see cref="byte"/>[] array.
        /// </summary>
        /// <param name="instance">The instance to assign.</param>
        public static implicit operator byte[](IntegraSystemExclusive instance)
        {
            List<byte> bytes = new();

            bytes.Add(SYX_STATUS);
            bytes.Add(SYX_MANUFACTURER);
            bytes.Add(instance.DeviceID);
            bytes.Add(0x00);
            bytes.Add(0x00);
            bytes.Add(SYX_MODEL);
            bytes.Add(instance.Mode);
            bytes.Add(instance.Address[0]);
            bytes.Add(instance.Address[1]);
            bytes.Add(instance.Address[2]);
            bytes.Add(instance.Address[3]);

            for (int i = 0; i < instance.Data.Length; i++)
                bytes.Add(instance.Data[i]);

            bytes.Add(instance.Checksum);
            bytes.Add(SYX_END);

            return bytes.ToArray();
        }

        #endregion

        #region Overrides: Object

        /// <summary>
        /// Returns a hexadecimal string that represents the current system exclusive message.
        /// </summary>
        /// <returns>A hexadecimal string that represents the current system exclusive message.</returns>
        public override string ToString()
        {
            return string.Join(" ", ((byte[])this).Select(x => string.Format("{0:X2}", x)));
        }

        #endregion
    }
}
