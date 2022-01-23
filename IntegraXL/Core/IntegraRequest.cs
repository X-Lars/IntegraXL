using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{
    public class IntegraRequest : IntegraAddress
    {
        #region Constructor

        public IntegraRequest() : base() { }

        public IntegraRequest(int request) : base(request) { }
        //{
        //    this[0] = (byte)((request & 0xFF000000) >> 24);
        //    this[1] = (byte)((request & 0xFF0000) >> 16);
        //    this[2] = (byte)((request & 0xFF00) >> 8);
        //    this[3] = (byte)((request & 0xFF));
        //}

        public IntegraRequest(byte[] request) : base(request) { }
        //{
        //    this[0] = request[0];
        //    this[1] = request[1];
        //    this[2] = request[2];
        //    this[3] = request[3];
        //}

        public IntegraRequest(byte byte01, byte byte02, byte byte03, byte byte04) : base(new byte[] { byte01, byte02, byte03, byte04 }) { }
        //{
        //    this[0] = byte01;
        //    this[1] = byte02;
        //    this[2] = byte03;
        //    this[3] = byte04;
        //}

        #endregion

        #region Properties

        //public byte this[int index]
        //{
        //    get { return Request[index]; }

        //    set 
        //    {
        //        if (value > 0x7F)
        //            throw new IntegraException($"[{nameof(IntegraRequest)}[{index}]]\nValue out of range [0x00..0x7F].");

        //        Request[index] = value; 
        //    }
        //}

        //public byte[] Request { get; } = new byte[4];

        /// <summary>
        /// Gets the size based on the least significant 16 bits of the request.
        /// </summary>
        public int Size
        {
            get { return (this[2] * 128 + this[3]); }
        }

        public byte MSB { get { return this[0]; } }
        public byte LSB { get { return this[1]; } }

        #endregion

        #region Conversion

        /// <summary>
        /// Implicitly converts an <see cref="IntegraRequest"/> to a <see cref="byte"/>[].
        /// </summary>
        /// <param name="instance">The <see cref="IntegraAddress"/> to convert.</param>
        //public static implicit operator byte[](IntegraRequest instance) => instance.Request;

        /// <summary>
        /// Implicitly creates a new <see cref="IntegraRequest"/> from a <see cref="byte"/>[].
        /// </summary>
        /// <param name="address">The <see cref="byte"/>[] to convert.</param>
        //public static implicit operator IntegraRequest(byte[] request) => new IntegraRequest(request);

        /// <summary>
        /// Overloads the assignment operator to implicitly convert an unsigned integer to an INTEGRA-7 request.
        /// </summary>
        /// <param name="request">The unsigned integer to assign to the request.</param>
        //public static implicit operator IntegraRequest(int request)
        //{
        //    byte[] bytes = BitConverter.GetBytes(request);

        //    if (BitConverter.IsLittleEndian)
        //        Array.Reverse(bytes);

        //    return new IntegraRequest(bytes);
        //}

        /// <summary>
        /// Implicitly converts an <see cref="IntegraRequest"/> to an <see cref="int"/>.
        /// </summary>
        //public static implicit operator int(IntegraRequest instance)
        //{
        //    IntegraRequest request = new IntegraRequest(instance.Request);

        //    if (BitConverter.IsLittleEndian)
        //        Array.Reverse(request);

        //    return BitConverter.ToInt32(request, 0);
        //}

        #endregion

        //public override string ToString()
        //{
        //    return "0x" + ((int)this).ToString("X4");
        //}

    }
}
