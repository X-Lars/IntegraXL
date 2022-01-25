using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IntegraXL.Core
{
    /// <summary>
    /// Provides functionality to name and validate properties contained in a model's indexer property.
    /// </summary>
    /// <remarks>
    /// <i>(De)serializes the property values to and from INTEGRA-7 MFX parameters.</i>
    /// </remarks>
    public abstract class IntegraMFXParameter : IntegraParameter
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraMFXParameter"/> instance.
        /// </summary>
        /// <param name="provider">The parameter provider.</param>
        public IntegraMFXParameter(IParameterProvider provider) : base(provider) { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the MFX parameter at the specified index.
        /// </summary>
        /// <param name="index">The index of the parameter.</param>
        /// <returns>The deserialized MFX parameter value.</returns>
        public override int this[int index]
        {
            get { return Provider[index].DeserializeMFX(); }
            set { Provider[index] = value.SerializeMFX(); }
        }

        #endregion
    }

    /// <summary>
    /// Provides functionality to name and validate properties contained in a model's indexer property.
    /// </summary>
    public abstract class IntegraParameter : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Stores a reference to the parameter provider.
        /// </summary>
        protected IParameterProvider Provider;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraParameter"/> instance.
        /// </summary>
        /// <param name="provider">The parameter provider.</param>
        public IntegraParameter(IParameterProvider provider)
        {
            Provider = provider;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the parameter at the specified index.
        /// </summary>
        /// <param name="index">The index of the parameter.</param>
        /// <returns>The parameter value.</returns>
        public virtual int this[int index]
        {
            get { return Provider[index]; }
            set { Provider[index] = value; }
        }

        #endregion

        #region Interfaces: INotifyPropertyChanged

        /// <summary>
        /// Event raised when a property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Method to invoke when a property is changed.
        /// </summary>
        /// <param name="propertyName">The name of the property, defaults to the caller member name.</param>
        /// <remarks>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
