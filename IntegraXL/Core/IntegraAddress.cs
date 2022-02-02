using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IntegraXLTest")]

namespace IntegraXL.Core
{
    /// <summary>
    /// - Ensures that individual bytes are within the MIDI range of 0x7F.
    /// </summary>
    public class IntegraAddress
    {
        #region Constructor

        internal IntegraAddress() { }

        internal IntegraAddress(int address)
        {
            this[0] = (byte)((address & 0xFF000000) >> 24);
            this[1] = (byte)((address & 0xFF0000) >> 16);
            this[2] = (byte)((address & 0xFF00) >> 8);
            this[3] = (byte)((address & 0xFF));
        }

        internal IntegraAddress(byte[] address)
        {
            Debug.Assert(address.Length == 4);

            this[0] = address[0];
            this[1] = address[1];
            this[2] = address[2];
            this[3] = address[3];
        }

        #endregion

        #region Properties

        internal byte this[int index]
        {
            get { return Address[index]; }

            set 
            {
                Debug.Assert(index >= 0 && index < 4, $"[{nameof(IntegraAddress)}[{index}]]\nIndex out of range [0..3].");
                Debug.Assert(value <= 0x7F, $"[{nameof(IntegraAddress)}[{index}]]\nValue out of range [0x00..0x7F]. {this}");

                Address[index] = value;
            }
        }

        internal byte[] Address { get; } = new byte[4];

        #endregion

        #region Methods

        internal bool InRange(IntegraAddress min, IntegraAddress max)
        {
            if (min > max)
            {
                return this >= max && this <= min;
            }
            else
            {
                return this >= min && this <= max;
            }
        }

        #endregion

        #region Conversion

        /// <summary>
        /// Implicitly converts an <see cref="IntegraAddress"/> to a <see cref="byte"/>[].
        /// </summary>
        public static implicit operator byte[](IntegraAddress instance) => instance.Address;

        /// <summary>
        /// Implicitly creates a new <see cref="IntegraAddress"/> from a <see cref="byte"/>[].
        /// </summary>
        public static implicit operator IntegraAddress(byte[] address) => new IntegraAddress(address);

        /// <summary>
        /// Implicitly converts an <see cref="IntegraAddress"/> to an <see cref="int"/>.
        /// </summary>
        public static implicit operator int(IntegraAddress instance)
        {
            IntegraAddress address = new IntegraAddress(instance.Address);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(address);

            return BitConverter.ToInt32(address, 0);
        }

        /// <summary>
        /// Implicitly creates a new <see cref="IntegraAddress"/> from an <see cref="int"/>.
        /// </summary>
        public static implicit operator IntegraAddress(int address)
        {
            byte[] bytes = BitConverter.GetBytes(address);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return new IntegraAddress(bytes);
        }

        #endregion

        #region Operator Overloads

        /// <summary>
        /// Overloads the == operator to compare two INTEGRA-7 addresses.
        /// </summary>
        /// <param name="lhs">The address to compare.</param>
        /// <param name="rhs">The address to compare to.</param>
        /// <returns>True if the addresses refer to the same INTEGRA-7 address, false otherwise.</returns>
        public static bool operator ==(IntegraAddress lhs, IntegraAddress rhs)
        {
            // Object cast is important to prevent stack overflow
            if ((object)lhs == null)
                return (object)rhs == null;

            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Overloads the != operator to compare two INTEGRA-7 addresses.
        /// </summary>
        /// <param name="lhs">The address to compare.</param>
        /// <param name="rhs">The address to compare to.</param>
        /// <returns>True if the addresses don't refer to the same INTEGRA-7 address, false otherwise.</returns>
        public static bool operator !=(IntegraAddress lhs, IntegraAddress rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Overloads the &lt; operator to compare two INTEGRA-7 addresses.
        /// </summary>
        /// <param name="lhs">The address to compare.</param>
        /// <param name="rhs">The address to compare to.</param>
        /// <returns>True if the left hand side address is smaller, false otherwise.</returns>
        public static bool operator <(IntegraAddress lhs, IntegraAddress rhs)
        {
            return (int)lhs < (int)rhs;
        }

        /// <summary>
        /// Overloads the &gt; operator to compare two INTEGRA-7 addresses.
        /// </summary>
        /// <param name="lhs">The address to compare.</param>
        /// <param name="rhs">The address to compare to.</param>
        /// <returns>True if the left hand side address is greater, false otherwise.</returns>
        public static bool operator >(IntegraAddress lhs, IntegraAddress rhs)
        {
            return (int)lhs > (int)rhs;
        }

        //public static IntegraAddress operator -(IntegraAddress lhs, IntegraAddress rhs)
        //{
        //    throw new NotImplementedException();
        //}


        /// <summary>
        /// Overloads the + operator to add two INTEGRA-7 addresses to create an offset.
        /// </summary>
        /// <param name="lhs">The address to apply the offset.</param>
        /// <param name="rhs">The address specifying the offset.</param>
        /// <returns>A new <see cref="IntegraAddress"/> with the offset applied.</returns>
        public static IntegraAddress operator +(IntegraAddress lhs, IntegraAddress rhs)
        {
            byte[] sum = new IntegraAddress(lhs.Address);

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
                throw new OverflowException($"[{nameof(IntegraAddress)}]\nAddress addition results to a MIDI overflow.");

            return sum;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Determines whether the specified object and this class refer to the same INTEGRA-7 address.
        /// </summary>
        /// <param name="obj">The object to compare for equality.</param>
        /// <returns>True if the object and this class refer to the same address, false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            IntegraAddress? address = obj as IntegraAddress;

            if (address is null)
                return false;

            return Address.SequenceEqual(address.Address);
        }

        /// <summary>
        /// Override to retreive a hash code based on the INTEGRA-7 address associated with this class.
        /// </summary>
        /// <returns>The hash code for the address.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "0x" + ((int)this).ToString("X4");
        }

        #endregion
    }
}
