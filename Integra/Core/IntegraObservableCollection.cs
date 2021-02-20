using Integra.Core.Interfaces;
using MidiXL;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Integra.Core
{
    public abstract class IntegraObservableCollection<T> : ObservableCollection<T>, IIntegraDataClass, INotifyPropertyChanged where T: IntegraBase<T>
    {
        private bool _IsInitialized = false;

        public IntegraObservableCollection(IntegraAddress address, IntegraRequest request)
        {
            Name = typeof(T).Name;

            Address = address;
            Request = request;

            //Initialize();
        }

        protected void Initialize()
        {
            if (Device._IsConnected)
            {
                Device.Instance.MidiInputDevice.SystemExclusiveReceived += SystemExclusiveReceived;
                Task.Factory.StartNew(() => Device.Instance.Initialize(this), TaskCreationOptions.LongRunning);
            }
            else
            {
                Device.Connected += DeviceConnected;
            }
        }

        private void DeviceConnected(object sender, EventArgs e)
        {
            Device.Connected -= DeviceConnected;
            Device.Instance.MidiInputDevice.SystemExclusiveReceived += SystemExclusiveReceived;
            Task.Factory.StartNew(() => Device.Instance.Initialize(this), TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Gets whether the collection items are initialized with data.
        /// </summary>
        public virtual bool IsInitialized
        {
            get { return _IsInitialized; }
            internal protected set
            {
                if (_IsInitialized != value)
                {
                    _IsInitialized = value;

                    //NotifyPropertyChanged();

                    InitializationCounter = 0;
                }
            }
        }

        public string Name { get; protected set; }
        public IntegraAddress Address { get; protected set; }
        public IntegraRequest Request { get; protected set; }
        protected abstract void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e);
        protected int InitializationCounter { get; set; } = 0;
        /// <summary>
        /// Gets the progress of the initialization.
        /// </summary>
        
        /// <summary>
        /// Override to bind collection items to notify property changed event.
        /// </summary>
        /// <param name="e">Event data.</param>
        //protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.Action == NotifyCollectionChangedAction.Remove)
        //    {
        //        if (e.OldItems != null)
        //        {
        //            foreach (INotifyPropertyChanged item in e.OldItems)
        //            {
        //                item.PropertyChanged -= ItemChanged;
        //            }
        //        }
        //    }

        //    if (e.Action == NotifyCollectionChangedAction.Add)
        //    {
        //        if (e.NewItems != null)
        //        {
        //            foreach (INotifyPropertyChanged item in e.NewItems)
        //            {
        //                item.PropertyChanged += ItemChanged;
                        
        //            }
        //        }
        //    }

        //    if (e.Action == NotifyCollectionChangedAction.Replace)
        //    {
        //        if (e.OldItems != null)
        //        {
        //            foreach (INotifyPropertyChanged item in e.OldItems)
        //            {
        //                item.PropertyChanged -= ItemChanged;
        //            }
        //        }
        //        if (e.NewItems != null)
        //        {
        //            foreach (INotifyPropertyChanged item in e.NewItems)
        //            {
        //                item.PropertyChanged += ItemChanged;
        //            }
        //        }
        //    }

        //    base.OnCollectionChanged(e);
        //}

        ///// <summary>
        ///// Raises the collection changed with reset action to notify property changes.
        ///// </summary>
        ///// <param name="sender">The object that raised the event.</param>
        ///// <param name="e">Event data containing the property name.</param>
        //private void ItemChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        //}

        #region IIntegraDataClass

        public int ID { get; }

        public void Update()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Update();
            }
        }

        public void Delete()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Delete();
            }
        }

        public void Select(int id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Select(id);
            }
        }

        public void Insert()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Insert();
            }
        }

        public void Truncate()
        {

            // TODO: Truncate only once
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Truncate();
            }
        }

        #endregion

        #region INotifyPropertyChanged

        /// <summary>
        /// Event raised when a property value is changed.
        /// </summary>
        

        protected override event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the specified property.
        /// </summary>
        /// <param name="propertyName">A <see cref="string"/> containing the name of the property that is changed.</param>
        /// <param name="transmit">A <see cref="bool"/> to determin if the property has to be transmitted.</param>
        /// <remarks><i>If no property name is specified, the actual name of the property in code is used.</i></remarks>
        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
