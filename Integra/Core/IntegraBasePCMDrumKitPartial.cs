using Integra.Core.Interfaces;
using MidiXL;
using System;
using System.ComponentModel;

namespace Integra.Core
{
    public class IntegraBasePCMDrumKitPartial<T> : IntegraObservableCollection<T>, IIntegraDataClass where T : IntegraBase<T>, IIntegraPartial, IIntegraDrumKitPartial, INotifyPropertyChanged
    {
        private const int NOTE_COUNT = 88;
        private IntegraParts _Part;

        public IntegraBasePCMDrumKitPartial(IntegraAddress address, IntegraParts part): base(address + 0x00001000, 0x00013E00)
        {
            uint p = (((address - 0x19000000) & 0xFF000000) >> 24) * 4;
            p += ((address & 0x00FF0000) >> 16) / 0x20;

            Part = (IntegraParts)p;
            address += 0x00001000;

            for (int i = 0; i < NOTE_COUNT; i++)
            {
                T item = Activator.CreateInstance<T>();

                item.Address = (uint)address + (uint)((i * 2) << 8);

                if ((item.Address & 0x00FF0000) >> 8 > 0x7F)
                {
                    item.Address -= 0x007F0000;
                    item.Address += 0x01000000;
                }

                Console.WriteLine(item.Address.ToString());
                item.Requests.Add(0x00000143);
                item.Part = Part;
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
                if (syx.Address.IsInRange(Address, Address + 0x00013E00))
                {

                    uint item = ((syx.Address& 0x0000FF00) >> 8) - 0x10;
                    item += ((syx.Address & 0x000F0000) >> 16) * 128;
                    item /= 2;

                    if (Items[(int)item].Initialize(syx.Data))
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
                    uint item = ((syx.Address & 0x0000FF00) >> 8) - 0x10;
                    item += ((syx.Address & 0x000F0000) >> 16) * 128;
                    item /= 2;

                    Items[(int)item].InitializeField(syx);
                }
            }
        }


        ///// <summary>
        ///// Override to bind collection items to notify property changed event.
        ///// </summary>
        ///// <param name="e">Event data.</param>
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


        //#region IIntegraDataClass

        //public int ID { get; }

        //public void Update()
        //{
        //    for (int i = 0; i < Items.Count; i++)
        //    {
        //        Items[i].Update();
        //    }
        //}

        //public void Delete()
        //{
        //    for (int i = 0; i < Items.Count; i++)
        //    {
        //        Items[i].Delete();
        //    }
        //}

        //public void Select(int id)
        //{
        //    for (int i = 0; i < Items.Count; i++)
        //    {
        //        Items[i].Select(id);
        //    }
        //}

        //public void Insert()
        //{
        //    for (int i = 0; i < Items.Count; i++)
        //    {
        //        Items[i].Insert();
        //    }
        //}

        //public void Truncate()
        //{
        //    // TODO: Truncate only once
        //    for (int i = 0; i < Items.Count; i++)
        //    {
        //        Items[i].Truncate();
        //    }
        //}


        //#endregion


    }
}
