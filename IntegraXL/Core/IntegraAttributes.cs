
using System.Diagnostics;

namespace IntegraXL.Core
{

    /// <summary>
    /// Attribute to ensure required values are provided to generate an INTEGRA-7 model, template or collection initialization request.
    /// </summary>
    public class IntegraAttribute : Attribute
    {
        #region Constructor

        /// <summary>
        /// Creates and initializes a new <see cref="IntegraAttribute"/>, decorates the class as INTEGRA-7 collection.
        /// </summary>
        public IntegraAttribute(int address, int request, int size = 0)
        {
            //Debug.Assert(address != 0);
            //Debug.Assert(request != 0 || size != 0);

            Address = address;

            if (request != 0)
            {
                Request = request;
            }
            else
            {
                Request = size;
            }
                
            if(size != 0)
            {
                Size = size;
            }
            else
            {
                Size = (int)(((request & 0x00000F00) >> 8) * 128 + (request & 0x000000FF));
            }
           
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the physical INTEGRA-7 address of the model, template or collection.
        /// </summary>
        public int Address { get; }

        /// <summary>
        /// Gets the request to generate an INTEGRA-7 initialization request.
        /// </summary>
        public int Request { get; }

        /// <summary>
        /// Gets the size of a model or template in bytes or the size of a collection in items.
        /// </summary>
        public int Size { get; }

        #endregion
    }

    /// <summary>
    /// Specifies the offset of a property or field into the containing model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal class OffsetAttribute : Attribute
    {
        /// <summary>
        /// Creates and initalizes a new offset attribute.
        /// </summary>
        /// <param name="offset">The offset into the containing model.</param>
        public OffsetAttribute(short offset)
        {
            Value = ((((offset & 0xFF00) >> 8) * 128) + offset & 0x00FF);
        }

        /// <summary>
        /// Gets the offset into the containing model of the associated property or field.
        /// </summary>
        public int Value { get; }
    }
}
