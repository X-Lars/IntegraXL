using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{
    public class IntegraSystemExclusive
    {
        public const int INTEGRA_SYSTEM_EXCLUSIVE_HEADER_SIZE = 6;
        #region Fields

        private static readonly byte[] _Header               = { 0xF0, 0x41, 0x10, 0x00, 0x00, 0x64, 0x11 };
        private static readonly byte[] _SystemExclusiveStart = { 0xF0 };
        private static readonly byte[] _ManufacturerID       = { 0x41 };
        private byte[]                 _DeviceID             = { 0x10 };
        private static readonly byte[] _ModelID              = { 0x00, 0x00, 0x64 };
        private readonly byte[]        _Mode                 = { 0x11 };
        private readonly byte[]        _Address              = { 0x00, 0x00, 0x00, 0x00};
        private readonly byte[]        _Data                 = { };
        private byte[]                 _Checksum             = { };
        private static readonly byte[] _SystemExclusiveEnd   = { 0xF7 };

        #endregion


        public IntegraSystemExclusive(byte deviceID, IntegraAddress address, IntegraRequest request)
        {
            DeviceID = deviceID;
            _Address = address;
            _Data = request;

            InvalidateChecksum();
        }

        public IntegraSystemExclusive(byte[] syx)
        {
            Array.Copy(syx, 7, _Address, 0, 4);

            _Data = new byte[syx.Length - 13];

            Array.Copy(syx, 11, _Data, 0, _Data.Length);

            InvalidateChecksum();

            if (syx[syx.Length - 2] != _Checksum[0])
                throw new IntegraException($"[{nameof(IntegraSystemExclusive)}]\nInvalid checksum.");
        }

        public IntegraSystemExclusive(IntegraAddress address, uint offset, byte[] data)
        {
            _Mode = new byte[] { 0x12 };
            _Address = address + offset;
            _Data = data;

            InvalidateChecksum();
        }

        public byte DeviceID 
        { 
            get { return _DeviceID[0]; }
            internal set
            {
                _DeviceID = new byte[] { value};
            }
        }
        public IntegraAddress Address { get { return _Address; } }
        /// <summary>
        /// Gets the data part of the system exclusive message.
        /// </summary>
        public byte[] Data
        {
            get { return _Data; }
        }
        private byte InvalidateChecksum()
        {
            int checkSum = 0;

            for (int i = 0; i < 4; i++)
            {
                checkSum += _Address[i];
            }

            if (_Data != null)
            {
                for (int i = 0; i < _Data.Length; i++)
                {
                    checkSum += _Data[i];
                }
            }

            checkSum %= 128;
            checkSum = 128 - checkSum;

            checkSum = checkSum == 128 ? 0 : checkSum;

            _Checksum = new byte[] { (byte)checkSum };

            return (byte)checkSum;
        }

        /// <summary>
        /// Overloads the assignment operator to implicitly convert an INTEGRA-7 system exclusive to a raw MIDI byte array.
        /// </summary>
        /// <param name="instance">The instance to assign to the byte array.</param>
        public static implicit operator byte[](IntegraSystemExclusive instance)
        {
            // Create a jagged array to combine all system exclusive fields
            byte[][] fields =
            {
                _SystemExclusiveStart,
                _ManufacturerID,
                instance._DeviceID,
                _ModelID,
                instance._Mode,
                instance._Address,
                instance._Data,
                instance._Checksum,
                _SystemExclusiveEnd
            };

            // Create the byte array with the total size of the jagged array dimensions
            byte[] byteArray = new byte[fields.Sum(b => b.Length)];

            int offset = 0;

            // Copy all bytes from the jagged array into one sequential data array
            foreach (byte[] value in fields)
            {
                Buffer.BlockCopy(value, 0, byteArray, offset, value.Length);
                offset += value.Length;
            }

            return byteArray;
        }

        #region Overrides: Object

        public override string ToString()
        {
            return string.Join(" ", ((byte[])this).Select(x => string.Format("{0:X2}", x)));
        }

        #endregion
    }
}
