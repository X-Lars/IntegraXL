using IntegraXL.Core;
using System.ComponentModel;

namespace IntegraXL.Interfaces
{
    /// <summary>
    /// Defines a contract for models containing a parameter array.
    /// </summary>
    /// <typeparam name="TIndexer">The parameter indexer property type.</typeparam>
    public interface IParameterProvider<TIndexer> : INotifyPropertyChanged
    {
        /// <summary>
        /// The parameter indexer property.
        /// </summary>
        /// <param name="index">The index of the parameter.</param>
        /// <returns>The parameter at the specified index.</returns>
        TIndexer this[int index] { get; set; }

        /// <summary>
        /// Gets the parameter provider that names and validates the model's parameter indexer property.
        /// </summary>
        /// <remarks><i>The property name is for convenience to follows the Roland MIDI implementation naming as close as possible.</i></remarks>
        IntegraParameterProvider<TIndexer>? Parameters { get; }

        /// <summary>
        /// Event to raise when the provider type is changed.
        /// </summary>
        event EventHandler<IntegraParametersChangedEventArgs>? ParametersChanged;
    }
}
