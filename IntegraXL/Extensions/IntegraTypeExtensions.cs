using IntegraXL.Core;
using System.Diagnostics;

namespace IntegraXL.Extensions
{
    public static class IntegraTypeExtensions
    {
        #region Extensions: String

        /// <summary>
        /// Clamps or pads the current string to the specified length.
        /// </summary>
        /// <param name="instance">The string to clamp or pad.</param>
        /// <param name="length">The required string length.</param>
        /// <returns>The string clamped or padded to the specified length.</returns>
        public static string FixedLength(this string instance, int length)
        {
            return instance.Length > length ? instance.Substring(0, length) : instance.PadRight(length);
        }

        #endregion

        #region Extensions: Array

        /// <summary>
        /// Copies a part of the current array to a new array.
        /// </summary>
        /// <typeparam name="TArray">The array type specifier.</typeparam>
        /// <param name="array">The source array.</param>
        /// <param name="index">The copy start index.</param>
        /// <param name="length">The number of elements to copy.</param>
        /// <returns>A partial copy of the current array.</returns>
        public static TArray[] GetArrayPart<TArray>(this TArray[] array, int index, int length)
        {
            TArray[] result = new TArray[length];

            Array.Copy(array, index, result, 0, length);

            return result;
        }

        #endregion

        #region Extensions: Byte

        /// <summary>
        /// Clamps the current <see cref="byte"/> to the specified minimum and maximum value within the MIDI range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value, defaults to the maximum MIDI value.</param>
        /// <returns>The current value if it doesn't exceed the provided or MIDI range, the minimum or maximum otherwise.</returns>
        public static byte Clamp(this byte value, byte min = 0, byte max = 127)
        {
            Debug.Assert(min <= max);
            Debug.Assert(max < 128);
            return Math.Max(Math.Min(value, max), min);
        }

        #endregion

        public static int Clamp(this int value, int min = 0, int max = 127)
        {
            Debug.Assert(min < max);

            value = Math.Min(value, max);
            value = Math.Max(value, min);

            return value;
        }

        public static short Clamp(this short value, byte min = 0, byte max = 127)
        {
            Debug.Assert(min <= max);
            return Math.Max(Math.Min(value, max), min);
        }
        //public static short Clamp(this short value, short min = 0, short max = 127)
        //{
        //    Debug.Assert(min < max);

        //    value = Math.Min(value, max);
        //    value = Math.Max(value, min);

        //    return value;
        //}

        /// <summary>
        /// Serializes a signed <see cref="int"/> to a MIDI byte.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <param name="offset">The offset to add.</param>
        /// <returns></returns>
        /// <remarks><i>The optional offset is added to the value.</i></remarks>
        public static byte Serialize(this int value, int offset = 0)
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
        /// <remarks><i>The optional offset is <b>subtracted</b> from the byte value.</i></remarks>
        public static int Deserialize(this byte value, int offset = 0)
        {
            //Debug.Assert(offset >= 0);
            Debug.Assert(offset <= 64);

            return value - offset;
        }

       
        /// <summary>
        /// Serializes a signd <see cref="int"/> to a MIDI byte divided by a factor.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <param name="offset">The offset to add.</param>
        /// <param name="factor">The factor to divide.</param>
        /// <returns></returns>
        /// <remarks><i>The value is divided by the factor before the offset is added.</i></remarks>
        public static byte Serialize(this int value, byte offset, int factor)
        {

            Debug.Assert(((value / factor) + offset) >= 0);
            Debug.Assert(((value / factor) + offset) < 128);

            return (byte)((value / factor) + offset);
        }

        public static byte Deserialize(this byte value, byte offset, int factor)
        {
            return (byte)((value - factor) * factor);
        }

        /// <summary>
        /// Deserializes a MIDI byte to a signed <see cref="int"/> multiplied by a factor.
        /// </summary>
        /// <param name="value">The value to deserialize.</param>
        /// <param name="offset">The offset to subtract.</param>
        /// <param name="factor">The factor to multiply.</param>
        /// <returns></returns>
        /// <remarks><i>The offset is <b>subtracted</b> from the byte before the multiplication.</i></remarks>
        public static int Deserialize(this byte value, int offset, int factor)
        {
            return (value - offset) * factor;
        }

        public static byte[] Serialize(this short value, short min = 0, short max = 127)
        {
            Debug.Assert(min < max);

            value = Math.Min(value, max);
            value = Math.Max(value, min);

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

        public static int ToMidi(this int value)
        {
            byte[] values = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(values);

            // TODO: ? Remove logical AND's ?
            return (values[0] & 0x0F) << 12 | (values[1] & 0x0F) << 8 | (values[2] & 0x0F) << 4 | (values[3] & 0x0F);
        }

        public static int SerializeInt(this int value)
        {
            byte[] bytes = new byte[4];

            bytes[0] = (byte)((value >> 12) & 0x0F);
            bytes[1] = (byte)((value >> 8) & 0x0F);
            bytes[2] = (byte)((value >> 4) & 0x0F);
            bytes[3] = (byte)((value & 0x0F));

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Deserializes a MIDI integer.
        /// </summary>
        /// <param name="value">The value to deserialize.</param>
        /// <returns>A deserialized MIDI integer value.</returns>
        public static int Deserialize(this int value)
        {
            byte[] values = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(values);

            // TODO: ? Remove logical AND's ?
            return (values[0] & 0x0F) << 12 | (values[1] & 0x0F) << 8 | (values[2] & 0x0F) << 4 | (values[3] & 0x0F);
        }

        public static int DeserializeInt(this int value, int offset, int factor = 1)
        {
            byte[] values = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(values);

            int integer = (values[0] & 0x0F) << 12 | (values[1] & 0x0F) << 8 | (values[2] & 0x0F) << 4 | (values[3] & 0x0F);

            integer -= offset;
            integer *= factor;
            // TODO: ? Remove logical AND's ?
            return integer;
        }

        public static int SerializeInt(this int value, int offset, int factor = 1)
        {
            int integer = value / factor;
            integer += offset;

            return integer.SerializeInt();
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
