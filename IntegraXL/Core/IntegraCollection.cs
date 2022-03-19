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
    /// <b>Important</b><br/>
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
        internal protected IntegraCollection(Integra device, bool connect = true) : base(device, connect) { }
    }

    /// <summary>
    /// Base class for all INTEGRA-7 model and template collections.
    /// </summary>
    /// <typeparam name="TItem">The collection type specifier.</typeparam>
    /// <remarks>
    /// <i>The type must be derived from either <see cref="IntegraModel{T}"/> or <see cref="IntegraTemplate"/>.</i>
    /// </remarks>
    public abstract class IntegraCollection<TItem> : IntegraCollection, IEnumerable<TItem>, INotifyCollectionChanged where TItem : class
    {
        // TODO: Make thread safe

        #region Fields

        /// <summary>
        /// Stores the internal collection of items.
        /// </summary>
        private readonly ObservableCollection<TItem> _Collection = new();

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
        /// <remarks><i>
        /// - Requires derived collections to be decorated with the <see cref="IntegraAttribute"/>.<br/>
        /// - Requires the collection's items to be derived from either <see cref="IntegraModel"/> or <see cref="IntegraTemplate"/>.<br/>
        /// - Derived collection needs to add its request(s) to the <see cref="IntegraModel.Requests"/> list.<br/>
        /// - Derived collection is disconnected by default.
        /// </i></remarks>
        internal IntegraCollection(Integra device, bool connect = true) : base(device, connect) 
        {
            Debug.Assert(typeof(TItem).IsSubclassOf(typeof(IntegraModel<TItem>)) || typeof(TItem).IsSubclassOf(typeof(IntegraTemplate)));

            var typeAttribute = typeof(TItem).GetCustomAttribute<IntegraAttribute>();

            Debug.Assert(typeAttribute != null);

            _ItemSize = typeAttribute.Size;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the collection of items.
        /// </summary>
        public ObservableCollection<TItem> Collection
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
        public TItem this[int index]
        {
            get { return _Collection[index]; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specified item to the collection.
        /// </summary>
        /// <param name="item">The item to add to the collection.</param>
        internal protected void Add(TItem item)
        {
            Collection.Add(item);

            if (Integra.UIContext != null)
            {
                Integra.UIContext.Post(o => CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item)), null);
            }
            else
            {
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }
        }

        #endregion

        #region Overrides: Model

        internal override void Initialize()
        {
            foreach (var request in Requests)
            {
                IntegraSystemExclusive systemExclusive = new(Address, request);
                Device.TransmitSystemExclusive(systemExclusive);
            }
        }

        /// <summary>
        /// Gets whether the collection is initialized.
        /// </summary>
        /// <remarks>
        /// <i>If the collection is initialized, the collection is disconnected.</i>
        /// </remarks>
        public override bool IsInitialized
        {
            get => base.IsInitialized;
            internal protected set
            {
                base.IsInitialized = value;

                if (base.IsInitialized)
                    Disconnect();
            }
        }

        /// <summary>
        /// Handles the <see cref="Integra.SystemExclusiveReceived"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="Integra"/> that raised the event.</param>
        /// <param name="e">The event's associated data.</param>
        protected abstract override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e);

        /// <summary>
        /// Method to initialize the collection data..
        /// </summary>
        /// <param name="data">The data to initialize the collection.</param>
        /// <returns>Must return true if the collection is initialized.</returns>
        internal abstract override bool Initialize(byte[] data);

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Interfaces: INotifyCollectionChanged

        /// <summary>
        /// Event raised when the collection is modified.
        /// </summary>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        #endregion

        #region Interfaces: IEnumerable

        public IEnumerator<TItem> GetEnumerator()
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
