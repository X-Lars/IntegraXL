namespace IntegraXL.Extensions
{
    public static class IntegraMFXExtensions
    {
        /// <summary>
        /// Deserializes an MFX parameter.
        /// </summary>
        /// <param name="value">The value to deserialize.</param>
        /// <returns>A deserialized MFX parameter value.</returns>
        public static int DeserializeMFX(this int value)
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
        /// <returns>A serialized MFX parameter.</returns>
        public static int SerializeMFX(this int value)
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
    }
}
