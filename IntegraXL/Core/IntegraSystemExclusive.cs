using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{
    public class IntegraSystemExclusive
    {
        private const byte SYX_STATUS     = 0xF0;
        private const byte MANUFACTURER_ID = 0x41;
        private static readonly byte[] MODEL_ID        = { 0x00, 0x00, 0x64 };
        private const byte SYX_END = 0xF7;
        private const byte TRANSMIT = 0x12;
        private const byte REQUEST  = 0x11;

        public const int INTEGRA_SYSTEM_EXCLUSIVE_HEADER_SIZE = 6;
        #region Fields

        //private static readonly byte[] _Header               = { 0xF0, 0x41, 0x10, 0x00, 0x00, 0x64, 0x11 };
        private static readonly byte[] _SystemExclusiveStart = { 0xF0 };
        private byte[]                 _DeviceID             = { 0x10 };
        private static readonly byte[] _ModelID              = { 0x00, 0x00, 0x64 };
        private readonly byte[]        _Mode                 = { 0x11 };
        private readonly byte[]        _Address              = { 0x00, 0x00, 0x00, 0x00};
        private readonly byte[]        _Data                 = { };
        private byte[]                 _Checksum             = { };
        private static readonly byte[] _SystemExclusiveEnd   = { 0xF7 };

        #endregion


        public IntegraSystemExclusive(byte deviceID, IntegraAddress address, IntegraAddress request)
        {
            DeviceID = deviceID;
            Address = address;
            Data = request;

            InvalidateChecksum();
        }


        public IntegraSystemExclusive(byte[] syx)
        {
            Array.Copy(syx, 7, Address, 0, 4);

            Data = new byte[syx.Length - 13];

            Array.Copy(syx, 11, Data, 0, Data.Length);

            InvalidateChecksum();

            if (syx[syx.Length - 2] != _Checksum[0])
                throw new IntegraException($"[{nameof(IntegraSystemExclusive)}]\nInvalid checksum.");
        }

        public IntegraSystemExclusive(byte deviceID, IntegraAddress address, int offset, byte[] data)
        {
            DeviceID = deviceID;
            Mode = TRANSMIT;
            Address = address + offset;
            Data = data;

            InvalidateChecksum();
        }

        public byte DeviceID { get; set; } = 0x10;
        public byte Mode { get; set; } = REQUEST;

        public IntegraAddress Address { get; private set; } = new byte[4];
        //{ 
        //    get { return _Address; } 
        //}

       
        public byte[] Data { get; private set; }
        //{
        //    get { return _Data; }
        //}

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
            List<byte> bytes = new List<byte>();

            bytes.Add(SYX_STATUS);
            bytes.Add(MANUFACTURER_ID);
            bytes.Add(instance.DeviceID);
            bytes.Add(0x00);
            bytes.Add(0x00);
            bytes.Add(0x64);
            bytes.Add(instance.Mode);
            bytes.Add(instance.Address[0]);
            bytes.Add(instance.Address[1]);
            bytes.Add(instance.Address[2]);
            bytes.Add(instance.Address[3]);

            for (int i = 0; i < instance.Data.Length; i++)
                bytes.Add(instance.Data[i]);

            bytes.Add(instance._Checksum[0]);
            bytes.Add(SYX_END);

            return bytes.ToArray();
            //// Create a jagged array to combine all system exclusive fields
            //byte[][] fields =
            //{
            //    SYX_STATUS,
            //    MANUFACTURER_ID,
            //    new byte[] {instance.DeviceID },
            //    MODEL_ID,
            //    instance._Mode,
            //    instance._Address,
            //    instance._Data,
            //    instance._Checksum,
            //    STATUS_END
            //};

            //// Create the byte array with the total size of the jagged array dimensions
            //byte[] byteArray = new byte[fields.Sum(b => b.Length)];

            //int offset = 0;

            //// Copy all bytes from the jagged array into one sequential data array
            //foreach (byte[] value in fields)
            //{
            //    Buffer.BlockCopy(value, 0, byteArray, offset, value.Length);
            //    offset += value.Length;
            //}

            //return byteArray;
        }

        #region Overrides: Object

        public override string ToString()
        {
            return string.Join(" ", ((byte[])this).Select(x => string.Format("{0:X2}", x)));
        }

        #endregion
    }
}
