using Integra.Core.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Integra.Core
{
    /// <summary>
    /// Base collection for INTEGRA-7 per part data structures.
    /// </summary>
    /// <typeparam name="T">The type contained by the collection.</typeparam>
    public class IntegraBasePartial<T> : ObservableCollection<T>, IIntegraDataClass  where T : IntegraBase<T>, IIntegraPartial, INotifyPropertyChanged
    {
        private readonly SynchronizationContext _Context;

        #region Constructor

        public IntegraBasePartial(IntegraAddress address, IntegraRequest request)
        {

            _Context = Device.UIContext;

            for (int i = 0; i < 16; i++)
            {
                T item = Activator.CreateInstance<T>();

                item.Address = (uint)address + (uint)(i << 8);
                item.Requests.Add(request);
                item.Part = (IntegraParts)i;
                item.Initialize();

                Add(item);

            }
        }
      
        #endregion

        /// <summary>
        /// Override to bind collection items to notify property changed event.
        /// </summary>
        /// <param name="e">Event data.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (e.OldItems != null)
                {
                    foreach (INotifyPropertyChanged item in e.OldItems)
                    {
                        item.PropertyChanged -= ItemChanged;
                    }
                }
            }
            
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems != null)
                {
                    foreach (INotifyPropertyChanged item in e.NewItems)
                    {
                        item.PropertyChanged += ItemChanged;
                    }
                }
            }

            if(e.Action == NotifyCollectionChangedAction.Replace)
            {
                if(e.OldItems != null)
                {
                    foreach (INotifyPropertyChanged item in e.OldItems)
                    {
                        item.PropertyChanged -= ItemChanged;
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (INotifyPropertyChanged item in e.NewItems)
                    {
                        item.PropertyChanged += ItemChanged;
                    }
                }
            }

            base.OnCollectionChanged(e);
        }

        /// <summary>
        /// Raises the collection changed with reset action to notify property changes.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event data containing the property name.</param>
        private void ItemChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

       
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
    }
}
