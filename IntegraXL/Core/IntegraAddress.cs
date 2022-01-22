using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{
    public class IntegraAddress
    {
        #region Constructor

        public IntegraAddress() { }

        public IntegraAddress(uint address)
        {
            this[0] = (byte)((address & 0xFF000000) >> 24);
            this[1] = (byte)((address & 0xFF0000) >> 16);
            this[2] = (byte)((address & 0xFF00) >> 8);
            this[3] = (byte)((address & 0xFF));
        }

        public IntegraAddress(byte[] address)
        {
            this[0] = address[0];
            this[1] = address[1];
            this[2] = address[2];
            this[3] = address[3];
        }

        #endregion

        #region Properties

        public byte this[int index]
        {
            get { return Address[index]; }

            internal set 
            {
                if (value > 0x7F)
                    throw new IntegraException($"[{nameof(IntegraAddress)}[{index}]]\nValue out of range [0x00..0x7F]. {this.ToString()}");

                Address[index] = value; 
            }
        }

        public byte[] Address { get; } = new byte[4];

        #endregion

        #region Methods

        public bool InRange(IntegraAddress min, IntegraAddress max)
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
        /// Implicitly converts an <see cref="IntegraAddress"/> to an <see cref="uint"/>.
        /// </summary>
        public static implicit operator uint(IntegraAddress instance)
        {
            IntegraAddress address = new IntegraAddress(instance.Address);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(address);

            return BitConverter.ToUInt32(address, 0);
        }

        /// <summary>
        /// Implicitly creates a new <see cref="IntegraAddress"/> from an <see cref="uint"/>.
        /// </summary>
        public static implicit operator IntegraAddress(uint address)
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
            return (uint)lhs < (uint)rhs;
        }

        /// <summary>
        /// Overloads the &gt; operator to compare two INTEGRA-7 addresses.
        /// </summary>
        /// <param name="lhs">The address to compare.</param>
        /// <param name="rhs">The address to compare to.</param>
        /// <returns>True if the left hand side address is greater, false otherwise.</returns>
        public static bool operator >(IntegraAddress lhs, IntegraAddress rhs)
        {
            return (uint)lhs > (uint)rhs;
        }

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
            return Address.GetHashCode();
        }

        public override string ToString()
        {
            return "0x" + ((uint)this).ToString("X4");
        }

        #endregion
    }
}
