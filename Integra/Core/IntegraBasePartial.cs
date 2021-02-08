using Integra.Core.Interfaces;
using Integra.Models;
using MidiXL;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Integra.Core
{
    public class StudioSetPartial<T> : IntegraObservableCollection<T>, IIntegraDataClass where T : IntegraBase<T>, IIntegraPartial
    {
        private const int PARTIAL_COUNT = 16;

        public StudioSetPartial(IntegraAddress baseAddress) : base(baseAddress, 0x00001000)
        {
            for (int i = 0; i < PARTIAL_COUNT; i++)
            {
               
                T item = Activator.CreateInstance<T>();

                item.Address = (uint)(baseAddress + (i << 8));
                item.Part = (IntegraParts)i;
                //item.Initialize();

                Add(item);
            }

            Initialize();
        }

        public double Progress
        {
            get { return (100d / PARTIAL_COUNT * InitializationCounter); }
        }

        protected override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            if(!IsInitialized)
            {
                if((syx.Address & 0xFFFFF000) == (Address & 0xFFFFF000))
                {
                    int item = (int)((syx.Address & 0x00000F00) >> 8);

                    if(Items[item].Initialize(syx.Data))
                    {
                        InitializationCounter++;

                        if (InitializationCounter == PARTIAL_COUNT)
                        {
                            Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
                            IsInitialized = true;
                        }
                        else
                            Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Please wait...", Progress, $"{InitializationCounter} of {PARTIAL_COUNT}"));
                    }
                }
            }
            else
            {
                if ((syx.Address & 0xFFFFF000) == (Address & 0xFFFFF000))
                {
                    int item = (int)((syx.Address & 0x00000F00) >> 8);

                    Items[item].InitializeField(syx);
                }
            }
        }
    }

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
