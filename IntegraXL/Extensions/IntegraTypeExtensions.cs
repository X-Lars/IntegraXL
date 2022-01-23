using IntegraXL.Core;
using System.Diagnostics;

namespace IntegraXL.Extensions
{
    public static class IntegraTypeExtensions
    {
        /// <summary>
        /// Serializes a <see cref="byte"/> to a MIDI byte.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns></returns>
        public static byte Serialize(this byte value, byte min = 0, byte max = 127)
        {
            Debug.Assert(min <= max);
            Debug.Assert(max < 128);

            value = Math.Min(value, max);
            value = Math.Max(value, min);

            return value;
        }

        /// <summary>
        /// Serializes a signed <see cref="int"/> to a MIDI byte.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <param name="offset">The offset to add.</param>
        /// <returns></returns>
        /// <remarks><i>The optional offset is added to the value.</i></remarks>
        public static byte Serialize(this int value, byte offset = 0)
        {
            Debug.Assert(value + offset >= 0);
            Debug.Assert(value + offset < 128);

            return (byte)(value + offset);
        }

        /// <summary>
        /// Deserializes a MIDI byte to a signed <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value to deserialize.</param>
        /// <param name="offset">The offset to subtract.</param>
        /// <returns></returns>
        /// <remarks><i>The offset is <b>subtracted</b> from the byte value.</i></remarks>
        public static int Deserialize(this byte value, int offset = 0)
        {
            Debug.Assert(offset >= 0);
            Debug.Assert(offset < 128);

            return value - offset;
        }

        public static byte[] Serialize(this short value)
        {
            byte[] bytes = new byte[2];

            bytes[0] = (byte)((value >> 4) & 0x0F).Serialize();
            bytes[1] = (byte)((value & 0x0F));

            if (bytes[0] > 0x7F || bytes[1] > 0x7F)
                throw new IntegraException($"[{nameof(IntegraTypeExtensions)}.{nameof(Serialize)}]\nUnable to serialize the short to a MIDI byte array.");

            return bytes;
        }

        public static short Deserialize(this byte[] value)
        {
            if (value.Length != 2)
                throw new IntegraException($"[{nameof(IntegraTypeExtensions)}.{nameof(Deserialize)}]\nThe MIDI byte array does not represent as short value.");

            // TODO: Check little endian
            return (short)((value[0] & 0x0F) << 4 | (value[1] & 0x0F));
        }

        //public static int MIDIValue(this short value)
        //{
        //    return (((value & 0xFF00) >> 8) * 128) + (value & 0x00FF);
        //}

        //public static short Deserialize(this byte[] data)
        //{
        //    return (short)((data[0] << 8) | data[1]);
        //}

        //public static byte[] Serialize(this short value)
        //{
        //    byte[] bytes = BitConverter.GetBytes(value);

        //    bytes[0] = (byte)(value >> 8);
        //    bytes[1] = (byte)(value & 0xFF);

        //    return bytes;
        //}
    }
}
