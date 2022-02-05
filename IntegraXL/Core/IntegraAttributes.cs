namespace IntegraXL.Core
{
    /// <summary>
    /// Attribute to ensure required values are provided to generate an INTEGRA-7 model, template or collection initialization request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class IntegraAttribute : Attribute
    {
        #region Constructor

        /// <summary>
        /// Creates and initializes a new <see cref="IntegraAttribute"/> instance.
        /// </summary>
        public IntegraAttribute(int address, int request, int size = 0)
        {
            Address = address;
            Request = request != 0 ? request : size;
            Size    = size != 0 ? size : ((request & 0x00000F00) >> 8) * 128 + (request & 0x000000FF);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the physical INTEGRA-7 memory address of the model, template or collection.
        /// </summary>
        public int Address { get; }

        /// <summary>
        /// Gets the parameters to generate an INTEGRA-7 data request to initialize the model, template or collection.
        /// </summary>
        public int Request { get; }

        /// <summary>
        /// Gets the size of a model or template in bytes or the size of a collection in items.
        /// </summary>
        public int Size { get; }

        #endregion
    }

    /// <summary>
    /// Attribute to mark a field or property as INTEGRA-7 property and specify its offset into the containing model's address.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal class OffsetAttribute : Attribute
    {
        #region Constructor

        /// <summary>
        /// Creates and initalizes a new <see cref="OffsetAttribute"/> instance.
        /// </summary>
        /// <param name="offset">The offset into the containing model's address.</param>
        public OffsetAttribute(short offset)
        {
            Value = (((offset & 0xFF00) >> 8) * 128) + offset & 0x00FF;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the offset into the containing model's address.
        /// </summary>
        public int Value { get; }

        #endregion
    }
}
