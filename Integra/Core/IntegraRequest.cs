using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    /// <summary>
    /// INTEGRA-7 Request address data structure, extends the <see cref="IntegraAddress"/> 
    /// </summary>
    public sealed class IntegraRequest : IntegraAddress
    {
        #region Constructor

        /// <summary>
        /// Default constructor creates and initializes a new <see cref="IntegraRequest"/> with the default <see cref="Address"/> of 0x00000000.
        /// </summary>
        public IntegraRequest() : base() { }

        /// <summary>
        /// Creates a new <see cref="IntegraRequest"/> instance initialized with the specified <paramref name="address"/>.
        /// </summary>
        /// <param name="address">A <see cref="uint"/> specifying the address.</param>
        public IntegraRequest(uint address) : base(address) { }

        /// <summary>
        /// Creates a new <see cref="IntegraAddress"/> instance initialized with the specified <paramref name="address"/>.
        /// </summary>
        /// <param name="address">A <see cref="byte"/> array specifying the address.</param>
        public IntegraRequest(byte[] address) : base(address) { }

        /// <summary>
        /// Creates a new <see cref="IntegraRequest"/> instance initialized with the supplied parameter <see cref="byte"/>s.
        /// </summary>
        /// <param name="addressByte01">The most significant byte of the address.</param>
        /// <param name="addressByte02">The second byte of the address.</param>
        /// <param name="addressByte03">The third byte of the address.</param>
        /// <param name="addressByte04">The least significant byte of the address.</param>
        public IntegraRequest(byte addressByte01, byte addressByte02, byte addressByte03, byte addressByte04) : base(addressByte01, addressByte02, addressByte03, addressByte04) { }

        #endregion

        #region Properties

        /// <summary>
        ///  Gets the size of the request from the last two address bytes.
        /// </summary>
        public uint Size
        {
            get
            {
                return (uint)((this[2] * 128) + this[3]);
            }
        }

        /// <summary>
        /// Gets the size of the data structure.
        /// </summary>
        public uint DataSize
        {
            // TODO: Check MFX data structure length 01 11
            get { return this[3]; }
        }

        #endregion

        #region Overloads

        /// <summary>
        /// Overloads the assignment opertor to be able to assign an <see cref="uint"/> to an <see cref="IntegraRequest"/>.
        /// </summary>
        /// <param name="address">The <see cref="uint"/> to assign to the <see cref="IntegraRequest"/>.</param>
        public static implicit operator IntegraRequest(uint address)
        {
            byte[] bytes = BitConverter.GetBytes(address);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return new IntegraRequest(bytes);
        }

        #endregion
    }
}
