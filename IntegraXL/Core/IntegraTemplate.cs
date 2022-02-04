namespace IntegraXL.Core
{
    /// <summary>
    /// Base class for all immutable INTEGRA-7 data like studio sets, tones and waveforms.
    /// </summary>
    /// <remarks>
    /// <b>IMPORTANT</b><br/>
    /// <i>Only intended for internal use, use the strongly typed <see cref="IntegraTemplate{T}"/> instead.</i>
    /// </remarks>
    public abstract class IntegraTemplate { }

    /// <summary>
    /// Base class for all immutable INTEGRA-7 data like studio sets, tones and waveforms.
    /// </summary>
    /// <typeparam name="T">The template type specifier.</typeparam>
    public abstract class IntegraTemplate<T> : IntegraTemplate where T : IntegraTemplate<T>
    {
        /// <summary>
        /// Creates a new <see cref="IntegraTemplate{T}"/> instance.
        /// </summary>
        /// <param name="id">The ID of the template for display purpose.</param>
        /// <param name="data">Not implemented.</param>
#pragma warning disable IDE0060 // Remove unused parameter
        protected IntegraTemplate(int id, byte[] data)
        {
            ID = id;
        }
#pragma warning restore IDE0060 // Remove unused parameter

        /// <summary>
        /// Gets the ID of the template.
        /// </summary>
        /// <remarks><i>For display purpose only, do not use for calculations.</i></remarks>
        public int ID { get; }
    }
}
