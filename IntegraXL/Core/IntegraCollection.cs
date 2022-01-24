using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;

namespace IntegraXL.Core
{
    /// <summary>
    /// Base class for all INTEGRA-7 collections.
    /// </summary>
    /// <remarks>
    /// <b>Important:</b><br/>
    /// <i>Only intended for internal use.</i><br/>
    /// <i>Use the strongly typed <see cref="IntegraCollection{T}"/> instead.</i>
    /// </remarks>
    public abstract class IntegraCollection : IntegraModel
    {
        /// <summary>
        /// Creates a new <see cref="IntegraCollection"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the collection.</param>
        /// <remarks>
        /// <b>Important:</b><br/>
        /// <i>Only intended for internal use.</i><br/>
        /// <i>Use the strongly typed <see cref="IntegraCollection{T}"/> instead.</i>
        /// </remarks>
        internal protected IntegraCollection(Integra device) : base(device) { }
    }

    /// <summary>
    /// Base class for all INTEGRA-7 collections.
    /// </summary>
    /// <typeparam name="T">The collection type specifier.</typeparam>
    /// <remarks>
    /// <i>The type must be derived from either <see cref="IntegraModel{T}"/> or <see cref="IntegraTemplate"/>.</i>
    /// </remarks>
    public abstract class IntegraCollection<T> : IntegraCollection, IEnumerable<T>, INotifyCollectionChanged where T : class
    {
        #region Fields

        /// <summary>
        /// Stores the internal collection of items.
        /// </summary>
        private ObservableCollection<T> _Collection = new();

        /// <summary>
        /// Stores the item's size in bytes.
        /// </summary>
        protected int _ItemSize;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraCollection{T}"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the collection.</param>
        /// <remarks>
        /// <i>Requires derived collections to be decorated with the <see cref="IntegraAttribute"/>.</i><br/>
        /// <i>Requires the collection's items to be derived from either <see cref="IntegraModel"/> or <see cref="IntegraTemplate"/>.</i>
        /// </remarks>
        internal IntegraCollection(Integra device) : base(device) 
        {
            Debug.Assert(typeof(T).IsSubclassOf(typeof(IntegraModel<T>)) || typeof(T).IsSubclassOf(typeof(IntegraTemplate)));

            IntegraAttribute? attribute = typeof(T).GetCustomAttribute<IntegraAttribute>();

            Debug.Assert(attribute != null);

            _ItemSize = attribute.Size;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the collection of items.
        /// </summary>
        public ObservableCollection<T> Collection
        {
            get { return _Collection; }
        }

        /// <summary>
        /// Gets the number of items actually contained in the collection.
        /// </summary>
        public int Count => _Collection.Count;

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The item at the specified index.</returns>
        public T this[int index]
        {
            get { return _Collection[index]; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specified item to the collection.
        /// </summary>
        /// <param name="item">The item to add to the collection.</param>
        internal protected void Add(T item)
        {
            Collection.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
        }

        #endregion

        #region Overrides: Model

        /// <summary>
        /// Gets whether the collection is initialized.
        /// </summary>
        /// <remarks>
        /// <i>If the collection is initialized, the collection is disconnected.</i>
        /// </remarks>
        public override bool IsInitialized
        {
            get => base.IsInitialized;
            protected internal set
            {
                base.IsInitialized = value;

                if (base.IsInitialized)
                    Disconnect();
            }
        }

        /// <summary>
        /// Must be overridden to handle system exclusive messages.
        /// </summary>
        /// <param name="sender">The device that raised the event.</param>
        /// <param name="e">The system exclusive message data.</param>
        protected abstract override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e);

        /// <summary>
        /// Must be overriden to define data initialization logic.
        /// </summary>
        /// <param name="data">Should be the data part of received system exclusive messages.</param>
        /// <returns>Should return true if the collection is initialized.</returns>
        protected abstract override bool Initialize(byte[] data);

        #endregion

        #region Interfaces: INotifyCollectionChanged

        /// <summary>
        /// Event raised when the collection is modified.
        /// </summary>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        #endregion

        #region Interfaces: IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
