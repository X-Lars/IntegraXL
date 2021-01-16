using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    /// <summary>
    /// INTEGRA-7 Address data structure provides a <i>Big Endian</i> address with offset, arithmetic and validation functionality.
    /// </summary>
    public class IntegraAddress : IEquatable<IntegraAddress>
    {
        #region Constructor

        /// <summary>
        /// Default constructor creates and initializes a new <see cref="IntegraAddress"/> with the default <see cref="Address"/> of 0x00000000.
        /// </summary>
        public IntegraAddress() { }

        /// <summary>
        /// Creates a new <see cref="IntegraAddress"/> instance initialized with the specified <paramref name="address"/>.
        /// </summary>
        /// <param name="address">A <see cref="uint"/> specifying the address.</param>
        public IntegraAddress(uint address)
        {
            this[0] = (byte)((address & 0xFF000000) >> 24);
            this[1] = (byte)((address & 0xFF0000) >> 16);
            this[2] = (byte)((address & 0xFF00) >>  8);
            this[3] = (byte)((address & 0xFF));

            foreach (byte value in Address)
            {
                if (value > IntegraConstants.MIDI_MAX_VALUE)
                    throw new ArgumentException(string.Format("0x{0:X2}", value), nameof(address));
            }
        }

        /// <summary>
        /// Creates a new <see cref="IntegraAddress"/> instance initialized with the supplied parameter <see cref="byte"/>s.
        /// </summary>
        /// <param name="addressByte01">The most significant byte of the address.</param>
        /// <param name="addressByte02">The second byte of the address.</param>
        /// <param name="addressByte03">The third byte of the address.</param>
        /// <param name="addressByte04">The least significant byte of the address.</param>
        public IntegraAddress(byte addressByte01, byte addressByte02, byte addressByte03, byte addressByte04)
        {
            this[0] = addressByte01;
            this[1] = addressByte02;
            this[2] = addressByte03;
            this[3] = addressByte04;
        }

        /// <summary>
        /// Creates a new <see cref="IntegraAddress"/> instance initialized with the specified <paramref name="address"/>.
        /// </summary>
        /// <param name="address">A <see cref="byte"/> array specifying the address.</param>
        public IntegraAddress(byte[] address)
        {
            if(address.Length != IntegraConstants.ADDRESS_SIZE)
                throw new ArgumentException($"Size exceeds the INTEGRA-7 address size of {IntegraConstants.ADDRESS_SIZE}.", nameof(address));

            // Array has to be copied because it's a reference type, don't assign it directly to the address property
            Array.Copy(address, Address, IntegraConstants.ADDRESS_SIZE);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a <see cref="byte"/> from the specified index of the <see cref="IntegraAddress"/>.
        /// </summary>
        /// <param name="index">An <see cref="uint"/> specifying the <see cref="byte"/> to get.</param>
        /// <returns>The <see cref="byte"/> at the specified index.</returns>
        public byte this[uint index]
        {
            get { return Address[index]; }
            set
            {
                if (value > IntegraConstants.MIDI_MAX_VALUE)
                    throw new ArgumentException(string.Format("0x{0:X2}", value), nameof(index));

                Address[index] = value;
            }
        }

        /// <summary>
        /// Gets the address associated with the <see cref="IntegraAddress"/>.
        /// </summary>
        public byte[] Address { get;} = new byte[IntegraConstants.ADDRESS_SIZE];

        #endregion

        #region Methods

        /// <summary>
        /// Checks if an <see cref="IntegraAddress"/> is within a specific address range.
        /// </summary>
        /// <param name="min">An <see cref="IntegraAddress"/> specifying the range's lowest value.</param>
        /// <param name="max">An <see cref="IntegraAddress"/> specifying the range's highest value.</param>
        /// <returns>A <see cref="bool"/> containing true if the <see cref="IntegraAddress"/> is within the specified range.</returns>
        public bool IsInRange(IntegraAddress min, IntegraAddress max)
        {
            bool reversed = min > max;

            if (reversed == true)
            {
                if (this > min || this < max)
                    return false;
            }
            else
            {
                if (this < min || this > max)
                    return false;

            }

            return true;
        }

        #endregion

        #region Overloads

        #region Overloads: Conversion

        /// <summary>
        /// Overloads the assignment operator to be able to assign an <see cref="IntegraAddress"/> to a <see cref="byte"/> array.
        /// </summary>
        /// <param name="address">The <see cref="IntegraAddress"/> to assign to the <see cref="byte"/> array.</param>
        public static implicit operator byte[](IntegraAddress instance) => instance.Address;

        /// <summary>
        /// Overloads the assignment operator to be able to assign a <see cref="byte"/> array to a <see cref="IntegraAddress"/>.
        /// </summary>
        /// <param name="address">The <see cref="byte"/> array to assign to the <see cref="IntegraAddress"/>.</param>
        public static implicit operator IntegraAddress(byte[] address) => new IntegraAddress(address);

        /// <summary>
        /// Overloads the assignment operator to be able to assign an <see cref="IntegraAddress"/> to an <see cref="uint"/>.
        /// </summary>
        /// <param name="instance">The <see cref="IntegraAddress"/> to assign to the <see cref="uint"/>.</param>
        public static implicit operator uint(IntegraAddress instance)
        {
            IntegraAddress address = new IntegraAddress(instance.Address);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(address);

            return BitConverter.ToUInt32(address, 0);
        }

        /// <summary>
        /// Overloads the assignment opertor to be able to assign an <see cref="uint"/> to an <see cref="IntegraAddress"/>.
        /// </summary>
        /// <param name="address">The <see cref="uint"/> to assign to the <see cref="IntegraAddress"/>.</param>
        public static implicit operator IntegraAddress(uint address)
        {
            byte[] bytes = BitConverter.GetBytes(address);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return new IntegraAddress(bytes);
        }

        #endregion

        #region Overloads: Arithmetic

        /// <summary>
        /// Overloads the == operator to provide comparison of two <see cref="IntegraAddress"/>es.
        /// </summary>
        /// <param name="lhs">The <see cref="IntegraAddress"/> to compare.</param>
        /// <param name="rhs">The <see cref="IntegraAddress"/> to compare to.</param>
        /// <returns>A <see cref="bool"/> containing true if both <see cref="Address"/>es are equal.</returns>
        public static bool operator ==(IntegraAddress lhs, IntegraAddress rhs)
        {
            if (lhs.Address == null)
                return rhs.Address == null;

            return lhs.Address.SequenceEqual(rhs.Address);
        }

        /// <summary>
        /// Overloads the != operator to provide comparison of two <see cref="IntegraAddress"/>es.
        /// </summary>
        /// <param name="lhs">The <see cref="IntegraAddress"/> to compare.</param>
        /// <param name="rhs">The <see cref="IntegraAddress"/> to compare to.</param>
        /// <returns>A <see cref="bool"/> containing true if the <see cref="Address"/>es are not equal.</returns>
        public static bool operator !=(IntegraAddress lhs, IntegraAddress rhs)
        {
            if (lhs.Address == null)
                return rhs.Address != null;

            return !lhs.Address.SequenceEqual(rhs.Address);
        }

        /// <summary>
        /// Overloads the &lt; operator to provide comparison of two <see cref="IntegraAddress"/>es for the smaller value.
        /// </summary>
        /// <param name="lhs">The <see cref="IntegraAddress"/> to compare.</param>
        /// <param name="rhs">An <see cref="IntegraAddress"/> to compare to.</param>
        /// <returns>A <see cref="bool"/> containing true if <paramref name="lhs"/>'s <see cref="Address"/> is smaller than <paramref name="rhs"/>'s <see cref="Address"/>, false otherwise.</returns>
        public static bool operator <(IntegraAddress lhs, IntegraAddress rhs)
        {
            return (uint)lhs < (uint)rhs;
        }

        /// <summary>
        /// Overloads the &gt; operator to provide the comparison of two <see cref="IntegraAddress"/>es for the greater value.
        /// </summary>
        /// <param name="lhs">The <see cref="IntegraAddress"/> to compare.</param>
        /// <param name="rhs">An <see cref="IntegraAddress"/> to compare to.</param>
        /// <returns>A <see cref="bool"/> containing true if <paramref name="lhs"/>'s <see cref="Address"/> is greater than <paramref name="rhs"/>'s <see cref="Address"/>, false otherwise.</returns>
        public static bool operator >(IntegraAddress lhs, IntegraAddress rhs)
        {
            return (uint)lhs > (uint)rhs;
        }

        /// <summary>
        /// Overloads the + operator to provide addition of two <see cref="IntegraAddress"/>es to add an offset.
        /// </summary>
        /// <param name="lhs">The <see cref="IntegraAddress"/> to apply the addition.</param>
        /// <param name="rhs">The <see cref="IntegraAddress"/> specifying the offset.</param>
        /// <returns>A new <see cref="IntegraAddress"/> with the offset applied.</returns>
        public static IntegraAddress operator +(IntegraAddress lhs, IntegraAddress rhs)
        {
            byte[] sum = new IntegraAddress(lhs.Address);

            sum[0] += rhs[0];
            sum[1] += rhs[1];
            sum[2] += rhs[2];
            sum[3] += rhs[3];

            if (sum[3] > IntegraConstants.MIDI_MAX_VALUE)
            {
                sum[3] -= IntegraConstants.MIDI_MAX_VALUE + 1;
                sum[2]++;
            }

            if (sum[2] > IntegraConstants.MIDI_MAX_VALUE)
            {
                sum[2] -= IntegraConstants.MIDI_MAX_VALUE + 1;
                sum[1]++;
            }

            if (sum[1] > IntegraConstants.MIDI_MAX_VALUE)
            {
                sum[1] -= IntegraConstants.MIDI_MAX_VALUE + 1;
                sum[0]++;
            }

            if (sum[0] > IntegraConstants.MIDI_MAX_VALUE)
                throw new OverflowException();

            return new IntegraAddress(sum);
        }

        /// <summary>
        /// Overloads the - operator to provide subtraction of two <see cref="IntegraAddress"/>es to create an offset.
        /// </summary>
        /// <param name="lhs">The <see cref="IntegraAddress"/> to apply the subtraction.</param>
        /// <param name="rhs">The <see cref="IntegraAddress"/> specifying the offset.</param>
        /// <returns>A new <see cref="IntegraAddress"/> with the offset applied.</returns>
        public static IntegraAddress operator -(IntegraAddress lhs, IntegraAddress rhs)
        {
            if (rhs > lhs)
                return new IntegraAddress();

            int[] difference = new int[IntegraConstants.ADDRESS_SIZE];

            difference[0] = lhs[0] - rhs[0];
            difference[1] = lhs[1] - rhs[1];
            difference[2] = lhs[2] - rhs[2];
            difference[3] = lhs[3] - rhs[3];

            if (difference[3] < 0)
            {
                difference[3] += IntegraConstants.MIDI_MAX_VALUE;
                difference[2]--;
            }

            if (difference[2] < 0)
            {
                difference[2] += IntegraConstants.MIDI_MAX_VALUE;
                difference[1]--;
            }

            if (difference[1] < 0)
            {
                difference[1] += IntegraConstants.MIDI_MAX_VALUE;
                difference[0]--;
            }

            return new IntegraAddress(new byte[] { (byte)difference[0], (byte)difference[1], (byte)difference[2], (byte)difference[3] });
        }

        #endregion

        #region Overloads: Logic

        /// <summary>
        /// Overloads the & operator to provide masking of <see cref="IntegraAddress"/>es.
        /// </summary>
        /// <param name="lhs">The <see cref="IntegraAddress"/> to apply the mask.</param>
        /// <param name="rhs">An <see cref="uint"/> specifying the bitmask.</param>
        /// <returns>A new <see cref="IntegraAddress"/> with the bitmask applied.</returns>
        public static IntegraAddress operator &(IntegraAddress lhs, uint rhs)
        {
            IntegraAddress address = new IntegraAddress(lhs.Address);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(address);

            return BitConverter.ToUInt32(address, 0) & rhs;
        }

        #endregion

        #endregion

        #region Overrides

        /// <summary>
        /// Overrides the <see cref="ToString"/> method to return a hexadecimal <see cref="string"/> representation of the <see cref="IntegraAddress"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> containing a hexadecimal representation of the <see cref="IntegraAddress"/>.</returns>
        public override string ToString()
        {
            return $"0x" + string.Join(string.Empty, Address.Select(x => string.Format("{0:X2}", x)));
        }

        /// <summary>
        /// Determines whether the specified object is equals the <see cref="IntegraAddress"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare the <see cref="IntegraAddress"/> to.</param>
        /// <returns>A <see cref="bool"/> containing true if the specified object is equals the <see cref="IntegraAddress"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            IntegraAddress address = obj as IntegraAddress;

            if (address == null)
                return false;

            return Address.SequenceEqual(Address);
        }

        /// <summary>
        /// Overrides the <see cref="GetHashCode"/> method to return the hash code of the <see cref="Address"/> instead.
        /// </summary>
        /// <returns>An <see cref="int"/> representing the hash code for the <see cref="Address"/>.</returns>
        public override int GetHashCode()
        {
            return Address.GetHashCode();
        }

        #endregion

        #region IEquatable

        /// <summary>
        /// Implements the <see cref="IEquatable{T}"/> interface to provide type specific equality comparison.
        /// </summary>
        /// <param name="address">The <see cref="IntegraAddress"/> to compare to this instance.</param>
        /// <returns>A <see cref="bool"/> containing true if the provided <paramref name="address"/> equals this instance's <see cref="Address"/>.</returns>
        public bool Equals(IntegraAddress address)
        {
            if (address == null)
                return Address == null;

            return address.Address.SequenceEqual(this.Address) ? true : false;
        }

        #endregion
    }
}
