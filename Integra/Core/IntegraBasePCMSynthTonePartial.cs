using Integra.Core.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Integra.Core
{
    /// <summary>
    /// Base collection for INTEGRA-7 synth tone partial data structures.
    /// </summary>
    /// <typeparam name="T">The type contained by the collection.</typeparam>
    public class IntegraBasePCMSynthTonePartial<T> : ObservableCollection<T>, IIntegraDataClass  where T : IntegraBase<T>, IIntegraPartial, IIntegraSynthTonePartial, INotifyPropertyChanged
    {
        private IntegraParts _Part;
        private readonly SynchronizationContext _Context;

        #region Constructor

        public IntegraBasePCMSynthTonePartial(IntegraAddress address, IntegraParts part)
        {

            _Context = Device.UIContext;

            _Part = part;

            for (int i = 0; i < 4; i++)
            {
                Debug.Print($"[{nameof(IntegraBasePCMSynthTonePartial<T>)}] {_Part}");
                T item = Activator.CreateInstance<T>();

                item.Address = (uint)address + 0x00002000 + (uint)((i * 2) << 8);
                item.Requests.Add(0x0000011A);
                item.Part = _Part;
                item.Partial = (IntegraSynthTonePartials)i;
                item.Initialize();

                Add(item);

               
            }
        }
      
        #endregion

        public IntegraParts Part
        {
            get { return _Part; }
            set
            {
                if (_Part != value)
                {
                    // TODO: NotifyPropertyChanged() ?
                    _Part = value;
                }
            }
        }
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
