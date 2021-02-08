using MidiXL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace Integra.Core
{
   
    public abstract class IntegraBaseCollection<T, U> : IntegraBase<T>, IEnumerable<U>, INotifyCollectionChanged where T: IntegraBase<T> where U: IntegraDataTemplate<U>
    {
        /// <summary>
        /// Stores the collection of items of type <see cref="U"/>.
        /// </summary>
        private ObservableCollection<U> _Collection = new ObservableCollection<U>();

        private readonly SynchronizationContext _Context;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #region Constructor

        public IntegraBaseCollection(IntegraAddress address) : base(address)
        {
            _Context = Device.UIContext;
        }

        public IntegraBaseCollection(IntegraAddress address, IntegraRequest request) : this(address, new IntegraRequest[] { request }) { }

        public IntegraBaseCollection(IntegraAddress address, IntegraRequest[] requests) : base(address, requests)
        {
            _Context = Device.UIContext;

            for (int i = 0; i < requests.Length; i++)
            {
                DataSize += requests[i].DataSize;
            }

            Debug.Print($"[{GetType().Name}] Size: {DataSize}");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index">The <see cref="int"/> index of the item to get.</param>
        /// <returns>An item of type <typeparamref name="U"/> from the specified index.</returns>
        public U this[int index]
        {
            get 
            {
                if (index >= Collection.Count)
                    return default;

                lock (Collection)
                {
                    return Collection[index];
                }
            }
        }

        /// <summary>
        /// Gets the collection of items.
        /// </summary>
        public ObservableCollection<U> Collection
        {
            get { return _Collection; }
        }

        /// <summary>
        /// Gets or sets the INTEGRA-7 defined number of items in the collection.
        /// </summary>
        protected virtual uint DataSize { get; set; }

        /// <summary>
        /// Gets or sets the counter to create an identifier for the received item.
        /// </summary>
        protected int IDCounter { get; set; } = 0;

        /// <summary>
        /// Gets the progress of the initialization.
        /// </summary>
        public double Progress 
        { 
            get { return (100d / DataSize * IDCounter); } 
        }

        public override bool IsInitialized
        {
            get { return base.IsInitialized; }
            internal protected set
            {
                base.IsInitialized = value;

                // Reset the ID counter for reinitialization
                if (value)
                    IDCounter = 0;
            }
        }
        #endregion

        #region Methods

        protected void Add(U item)
        {
            lock(_Collection)
                _Context.Post(o => Collection.Add(item), null);

            _Context.Post(o => CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item)), null);
        }

        #endregion

        #region Overrides

        protected override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            if (IsInitialized)
                return;

            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);
            //Console.WriteLine(e.Message);
            if (syx.Address == Address)
            {
                // Ensure ID's starts from 1
                IDCounter++;

                if(Initialize(syx.Data))
                {
                    Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
                }
                else
                {
                    //Device.Instance.OnOperationProgress(new StatusMessage($"Initializing {Name}", "Please wait...", Progress, $"Item {IDCounter} of {DataSize}"));
                }
            }
        }

        internal override bool Initialize(byte[] data)
        {
            Add((U)Activator.CreateInstance(typeof(U), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { IDCounter, data }, null));


            if (IDCounter == DataSize)
            {
                IsInitialized = true;
                NotifyPropertyChanged(string.Empty);
            }

            return IsInitialized;
        }

        #endregion

        #region IEnumerable

        public IEnumerator<U> GetEnumerator()
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
