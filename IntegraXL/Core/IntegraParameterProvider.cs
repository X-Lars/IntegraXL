using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using IntegraXL.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace IntegraXL.Core
{
    /// <summary>
    /// Base class for all <see cref="SuperNATURALAcousticTone"/> parameter mappers enables naming and validation of the <see cref="SuperNATURALAcousticTone"/> indexer property.
    /// </summary>
    public abstract class IntegraSNAProvider : IntegraParameterProvider<byte>
    {
        /// <summary>
        /// Creates a new <see cref="IntegraSNAProvider"/> instance.
        /// </summary>
        /// <param name="provider">The model that provides the parameters.</param>
        protected IntegraSNAProvider(SuperNATURALAcousticToneCommon provider) : base(provider) { }
    }

    /// <summary>
    /// Base class for all <see cref="MFX"/> parameter mappers enables naming and validation of the <see cref="MFX"/> indexer property.
    /// </summary>
    /// <remarks>
    /// <i>(De)serializes the property values to and from INTEGRA-7 MFX parameters.</i>
    /// </remarks>
    public abstract class IntegraMFXProvider : IntegraParameterProvider<int>
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraMFXProvider"/> instance.
        /// </summary>
        /// <param name="provider">The model that provides the parameters.</param>
        protected IntegraMFXProvider(IParameterProvider<int> provider) : base(provider) { }

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
    /// Base class for all parameter mappers enables naming and validation of parameters provided by a model's indexer property.
    /// </summary>
    /// <typeparam name="TIndexer">The parameter indexer property type.</typeparam>
    public abstract class IntegraParameterProvider<TIndexer> : IntegraParameterProvider, INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Stores a reference to the to the model providing the parameter indexer property.
        /// </summary>
        protected IParameterProvider<TIndexer> Provider;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraParameterProvider{TIndexer}"/> instance.
        /// </summary>
        /// <param name="provider">The model providing the parameter indexer property.</param>
        protected IntegraParameterProvider(IParameterProvider<TIndexer> provider) : base()
        {
            Debug.Assert(provider.GetType().GetInterfaces().Contains(typeof(IParameterProvider<TIndexer>)));

            Provider = provider;
            Provider.PropertyChanged += ProviderPropertyChanged;
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the parameter at the specified index.
        /// </summary>
        /// <param name="index">The index of the parameter.</param>
        /// <returns>The parameter value.</returns>
        public virtual TIndexer this[int index]
        {
            get { return Provider[index]; }
            set { Provider[index] = value; }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the property changed event of the provider.
        /// </summary>
        /// <param name="sender">The provider that raised the event.</param>
        /// <param name="e">The event data.</param>
        /// <remarks><i>Enables the UI to bind to the property name when the data context is set to <see cref="IParameterProvider.Parameter"/>.</i></remarks>
        private void ProviderPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // TODO: Raise on indexer property only
            NotifyPropertyChanged(string.Empty);
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
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public abstract class IntegraParameterProvider 
    {
        protected IntegraParameterProvider() { }
    }
}
