namespace IntegraXL.Core
{
    /// <summary>
    /// Defines a structure to reference a physical INTEGRA-7 memory address.
    /// </summary>
    /// <remarks><i>
    /// - Implicitly assignable from and to </i><see cref="int"/><i> and </i><see cref="byte"/>[].<i><br/>
    /// - Provides arithmetic and logical operator overloads.<br/>
    /// - Ensures that individual bytes are within the MIDI range.</i> [0x00..0x7F]<i><br/>
    /// </i></remarks>
    public class IntegraAddress
    {
        #region Fields

        /// <summary>
        /// Stores the indiviual address bytes.
        /// </summary>
        private readonly byte[] _Bytes = new byte[4];

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraAddress"/> instance with default value.
        /// </summary>
        /// <remarks><i>Defaults to 0x00000000.</i></remarks>
        public IntegraAddress() { }

        /// <summary>
        /// Creates a new <see cref="IntegraAddress"/> instance initialized with the specified address.
        /// </summary>
        /// <param name="address">The address to initialize.</param>
        /// <remarks><i>The address is split into a byte array and requires each byte to be within the MIDI range.</i> [0x00..0x7F]</remarks>
        public IntegraAddress(int address)
        {
            try
            {
                this[0] = (byte)((address & 0xFF000000) >> 24);
                this[1] = (byte)((address & 0xFF0000) >> 16);
                this[2] = (byte)((address & 0xFF00) >> 8);
                this[3] = (byte)((address & 0xFF));
            }
            catch { throw; }
        }

        /// <summary>
        /// Creates a new <see cref="IntegraAddress"/> instance initialized with the specified address.
        /// </summary>
        /// <param name="address">The address to initialize.</param>
        /// <remarks><i>The address is required to be 4 bytes in length and requires each byte to be within the MIDI range.</i> [0x00..0x7F]</remarks>
        public IntegraAddress(byte[] address)
        {
            try
            {
                this[0] = address[0];
                this[1] = address[1];
                this[2] = address[2];
                this[3] = address[3];
            }
            catch { throw; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the address byte at the specified index.
        /// </summary>
        /// <param name="index">The index of the address byte.</param>
        /// <returns>The address byte at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="OverflowException"></exception>
        public byte this[int index]
        {
            get { return _Bytes[index]; }

            set 
            {
                if(index < 0 || index > 3)
                    throw new IndexOutOfRangeException($"[{nameof(IntegraAddress)}[{index}]]\nThe index is out of range [0..3].");

                if (value > IntegraConstants.MAX_MIDI_VALUE)
                    throw new OverflowException($"[{nameof(IntegraAddress)}[{index}]]\nThe value exceeds the maximum MIDI value of 0x{IntegraConstants.MAX_MIDI_VALUE:X2}.");

                _Bytes[index] = value;
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implicitly makes an <see cref="IntegraAddress"/> assignable to a <see cref="byte"/>[].
        /// </summary>
        /// <param name="rhs">The right hand side address.</param>
        public static implicit operator byte[](IntegraAddress rhs) => rhs._Bytes;

        /// <summary>
        /// Implicitly makes a <see cref="byte"/>[] assignable to a new <see cref="IntegraAddress"/> instance.
        /// </summary>
        /// <param name="rhs">The right hand side address bytes.</param>
        public static implicit operator IntegraAddress(byte[] rhs) => new (rhs);

        /// <summary>
        /// Implicitly makes an <see cref="IntegraAddress"/> assignable to an <see cref="int"/>.
        /// </summary>
        /// <param name="rhs">The right hand side address.</param>
        public static implicit operator int(IntegraAddress rhs)
        {
            IntegraAddress address = new (rhs._Bytes);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(address);

            return BitConverter.ToInt32(address, 0);
        }

        /// <summary>
        /// Implicitly makes an <see cref="int"/> assignable to a new <see cref="IntegraAddress"/> instance.
        /// </summary>
        /// <param name="rhs">The right hand side integer address.</param>
        public static implicit operator IntegraAddress(int rhs)
        {
            byte[] bytes = BitConverter.GetBytes(rhs);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            for (int i = 3; i >= 0; i--)
            {
                if (bytes[3] > IntegraConstants.MAX_MIDI_VALUE)
                {
                    bytes[3] -= IntegraConstants.MAX_MIDI_VALUE + 1;
                    bytes[2]++;
                }

                if (bytes[2] > IntegraConstants.MAX_MIDI_VALUE)
                {
                    bytes[2] -= IntegraConstants.MAX_MIDI_VALUE + 1;
                    bytes[1]++;
                }

                if (bytes[1] > IntegraConstants.MAX_MIDI_VALUE)
                {
                    bytes[1] -= IntegraConstants.MAX_MIDI_VALUE + 1;
                    bytes[0]++;
                }

                if (bytes[0] > IntegraConstants.MAX_MIDI_VALUE)
                    throw new OverflowException($"[{nameof(IntegraAddress)}]\nAssigning the integer value {rhs} to the address results in a MIDI overflow.");
            }

            return new (bytes);
        }

        /// <summary>
        /// Overloads the equality operator to compare two addresses.
        /// </summary>
        /// <param name="lhs">The left hand side address.</param>
        /// <param name="rhs">The right hand side address.</param>
        /// <returns>True if the addresses reference to the same physical address.</returns>
        public static bool operator ==(IntegraAddress lhs, IntegraAddress rhs)
        {
            // IMPORTANT: Object cast is required to prevent stack overflow
            if (lhs is null)
                return rhs is null;

            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Overloads the inequality operator to compare two addresses.
        /// </summary>
        /// <param name="lhs">The left hand side address.</param>
        /// <param name="rhs">The right hand side address.</param>
        /// <returns>True if the addresses don't reference to the same physical address.</returns>
        public static bool operator !=(IntegraAddress lhs, IntegraAddress rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Overloads the smaller than operator to compare two addresses.
        /// </summary>
        /// <param name="lhs">The left hand side address.</param>
        /// <param name="rhs">The right hand side address.</param>
        /// <returns>True if the left hand side address is smaller.</returns>
        public static bool operator <(IntegraAddress lhs, IntegraAddress rhs)
        {
            return (int)lhs < (int)rhs;
        }

        /// <summary>
        /// Overloads the greater than operator to compare two addresses.
        /// </summary>
        /// <param name="lhs">The left hand side address.</param>
        /// <param name="rhs">The right hand side address.</param>
        /// <returns>True if the left hand side address is greater.</returns>
        public static bool operator >(IntegraAddress lhs, IntegraAddress rhs)
        {
            return (int)lhs > (int)rhs;
        }

        /// <summary>
        /// Overloads the addition operator to add two addresses.
        /// </summary>
        /// <param name="lhs">The left hand side address.</param>
        /// <param name="rhs">The right hand side address.</param>
        /// <returns>A new <see cref="IntegraAddress"/> with summed result.</returns>
        /// <exception cref="OverflowException"/>
        /// <remarks><i>Adds two addresses maintaining the per byte maximum MIDI range.</i> [0x00 ... 0x7F]</remarks>
        public static IntegraAddress operator +(IntegraAddress lhs, IntegraAddress rhs)
        {
            byte[] sum = new IntegraAddress(lhs._Bytes);

            sum[0] += rhs[0];
            sum[1] += rhs[1];
            sum[2] += rhs[2];
            sum[3] += rhs[3];

            if (sum[3] > IntegraConstants.MAX_MIDI_VALUE)
            {
                sum[3] -= IntegraConstants.MAX_MIDI_VALUE + 1;
                sum[2]++;
            }

            if (sum[2] > IntegraConstants.MAX_MIDI_VALUE)
            {
                sum[2] -= IntegraConstants.MAX_MIDI_VALUE + 1;
                sum[1]++;
            }

            if (sum[1] > IntegraConstants.MAX_MIDI_VALUE)
            {
                sum[1] -= IntegraConstants.MAX_MIDI_VALUE + 1;
                sum[0]++;
            }

            if (sum[0] > IntegraConstants.MAX_MIDI_VALUE)
                throw new OverflowException($"[{nameof(IntegraAddress)}]\nAddition of {lhs} and {rhs} results in a MIDI overflow.");

            return sum;
        }

        public static IntegraAddress operator -(IntegraAddress lhs, IntegraAddress rhs)
        {
            if (rhs > lhs)
                return new IntegraAddress();

            int[] difference = new int[4];

            difference[0] = lhs[0] - rhs[0];
            difference[1] = lhs[1] - rhs[1];
            difference[2] = lhs[2] - rhs[2];
            difference[3] = lhs[3] - rhs[3];

            if (difference[3] < 0)
            {
                difference[3] += IntegraConstants.MAX_MIDI_VALUE;
                difference[2]--;
            }

            if (difference[2] < 0)
            {
                difference[2] += IntegraConstants.MAX_MIDI_VALUE;
                difference[1]--;
            }

            if (difference[1] < 0)
            {
                difference[1] += IntegraConstants.MAX_MIDI_VALUE;
                difference[0]--;
            }

            return new IntegraAddress(new byte[] { (byte)difference[0], (byte)difference[1], (byte)difference[2], (byte)difference[3] });
        }

        #endregion

        #region Overrides: Object

        /// <summary>
        /// Determines whether the specified object references the the same physical address.
        /// </summary>
        /// <param name="obj">The object to compare with the current address.</param>
        /// <returns>True if the specified object references the same physical address.</returns>
        public sealed override bool Equals(object? obj)
        {
            if (obj is not IntegraAddress address)
                return false;

            return _Bytes.SequenceEqual(address._Bytes);
        }

        /// <summary>
        /// Returns the default hash function code for the current address.
        /// </summary>
        /// <returns>A hash code for the current address.</returns>
        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current address.
        /// </summary>
        /// <returns>A string representation of the current address.</returns>
        public override string ToString()
        {
            return $"0x{(int)this:X4}";
        }

        #endregion
    }
}
