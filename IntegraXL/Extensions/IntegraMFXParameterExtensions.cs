namespace IntegraXL.Extensions
{
    internal static class IntegraMFXParameterExtensions
    {
        /// <summary>
        /// Deserializes an MFX parameter.
        /// </summary>
        /// <param name="value">The value to deserialize.</param>
        /// <returns>The integer value of the MFX parameter.</returns>
        /// <remarks><i>
        /// - Removes the MFX parameter byte.<br/>
        /// </i></remarks>
        internal static int DeserializeMFX(this int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return ((bytes[1] & 0x0F) << 8 | (bytes[2] & 0x0F) << 4 | (bytes[3] & 0x0F));
        }

        /// <summary>
        /// Serializes a value to an MFX parameter.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The integer MFX parameter.</returns>
        /// <remarks><i>
        /// - Adds the MFX parameter byte.<br/>
        /// </i></remarks>
        internal static int SerializeMFX(this int value)
        {
            byte[] bytes = new byte[4];

            bytes[0] = 0x08; // Magic number
            bytes[1] = (byte)((value >> 8) & 0x0F);
            bytes[2] = (byte)((value >> 4) & 0x0F);
            bytes[3] = (byte)((value & 0x0F));

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }

        internal static int DeserializeFeedback(this int value)
        {
            // TODO: Check Clamp value
            return (value * 2) - 98;
        }

        internal static int SerializeFeedback(this int value)
        {
            // TODO: Check Clamp value
            return (value + 98) / 2;
        }
    }
}
