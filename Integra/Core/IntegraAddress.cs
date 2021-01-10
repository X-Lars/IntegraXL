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
    public class IntegraAddress
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
            Address[0] = (byte)((address & 0xFF000000) >> 24);
            Address[1] = (byte)((address & 0xFF0000) >> 16);
            Address[2] = (byte)((address & 0xFF00) >>  8);
            Address[3] = (byte)((address & 0xFF));

            foreach (byte value in Address)
            {
                if (value > IntegraConstants.MIDI_MAX_VALUE)
                    throw new ArgumentException(string.Format("0x{0:X2}", value), nameof(address));
            }
        }

        /// <summary>
        /// Creates a new <see cref="IntegraAddress"/> instance initialized with the specified <paramref name="address"/>.
        /// </summary>
        /// <param name="address">A <see cref="byte"/> array specifying the address.</param>
        public IntegraAddress(byte[] address)
        {
            if(address.Length != IntegraConstants.ADDRESS_SIZE)
                throw new ArgumentException($"Size doesn't match INTEGRA-7 address size of {IntegraConstants.ADDRESS_SIZE}.", nameof(address));

            Address = address;
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
        }

        /// <summary>
        /// Gets the address associated with the <see cref="IntegraAddress"/>.
        /// </summary>
        public byte[] Address { get; } = new byte[IntegraConstants.ADDRESS_SIZE];

        #endregion

        #region Overloads

        /// <summary>
        /// Overloads the assignment operator to be able to assign a <see cref="byte"/> array to a <see cref="IntegraAddress"/>.
        /// </summary>
        /// <param name="address">The <see cref="byte"/> array to assign to the <see cref="IntegraAddress"/>.</param>
        public static implicit operator IntegraAddress(byte[] address) => new IntegraAddress(address);

        /// <summary>
        /// Overloads the assignment operator to be able to assign an <see cref="IntegraAddress"/> to a <see cref="byte"/> array.
        /// </summary>
        /// <param name="address">The <see cref="IntegraAddress"/> to assign to the <see cref="byte"/> array.</param>
        public static implicit operator byte[](IntegraAddress instance) => instance.Address;

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

        /// <summary>
        /// Overloads the addition operator to be able to add two <see cref="IntegraAddress"/>es to create an address offset.
        /// </summary>
        /// <param name="lhs">The <see cref="IntegraAddress"/> to apply the offset to.</param>
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

        #endregion
    }
}
