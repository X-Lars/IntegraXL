using Integra.Core.Interfaces;
using MidiXL;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;

namespace Integra.Core
{
    public class IntegraBaseSuperNATURALDrumKitNotes<T> : IntegraObservableCollection<T>, IIntegraDataClass where T: IntegraBase<T>, IIntegraPartial, IIntegraDrumKitPartial, INotifyPropertyChanged
    {
        private const int NOTE_COUNT = 62;
        private IntegraParts _Part;
        private readonly SynchronizationContext _Context;
        public IntegraBaseSuperNATURALDrumKitNotes(IntegraAddress address, IntegraParts part) : base(address + 0x00001000, 0x00004D00)
        {
            _Context = Device.UIContext;
            // 
            uint p = (((address - 0x19000000) & 0xFF000000) >> 24) * 4;
            p += ((address & 0x00FF0000) >> 16) / 0x20;

            Part = (IntegraParts)p;
            
            //Part = part;
            //address += 0x00001000;

            for (int i = 0; i < NOTE_COUNT; i++)
            {
                T item = Activator.CreateInstance<T>();

                item.Address = (uint)Address + (uint)((i) << 8);
                item.Requests.Add(0x00000013);
                item.Part = _Part;
                item.Note = i;
                //item.Initialize();

                Add(item);
            }

            Initialize();
        }

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

            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (e.OldItems != null)
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
            _Context.Post(o => base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)), null);
            //base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public double Progress
        {
            get { return (100d / NOTE_COUNT * InitializationCounter); }
        }

        protected override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            if (!IsInitialized)
            {
                //if ((syx.Address & 0xFFFF0000) == (Address & 0xFFFF0000))
                //{
                    if (syx.Address.IsInRange(Address, Address + 0x004D0000))
                    {
                        int item = (int)((syx.Address - Address) >> 8);

                        if (Items[item].Initialize(syx.Data))
                        {
                            InitializationCounter++;

                            if (InitializationCounter == NOTE_COUNT)
                            {
                                Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
                                IsInitialized = true;
                            }
                            else
                                Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Please wait...", Progress, $"{InitializationCounter} of {NOTE_COUNT}"));
                        }
                    }
                //}
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
}
