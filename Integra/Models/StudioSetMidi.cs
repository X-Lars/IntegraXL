using Integra.Core;
using MidiXL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class StudioSetMidi : IntegraBase<StudioSetMidi>
    {
        [Offset(0x0000)] private byte _PhaseLock;
        private IntegraParts _Part;
        public StudioSetMidi()
        {

        }
        public StudioSetMidi(IntegraAddress address, IntegraRequest request) : base(address, request)
        {
            _Part = (IntegraParts)((address & 0x00000F00) >> 8);
            Initialize();
        }

        [Offset(0x0000)]
        public byte PhaseLock
        {
            get { return _PhaseLock; }
            set
            {
                _PhaseLock = value;
                NotifyPropertyChanged();
            }
        }

        internal override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            if (syx.Address == Address)
            {

                if (!IsInitialized)
                {
                    if (Initialize(syx.Data))
                    {
                        Device.Instance.ReportProgress(this, new StatusMessage($"Initializing Studio Set MIDI {_Part}", "Initialized", 100, "Done"));
                    }
                    else
                    {
                        Device.Instance.ReportProgress(this, new StatusMessage($"Initializing Studio Set MIDI {_Part}", "Please wait...", 100, "Initializing"));
                    }
                }
                else
                {
                    InitializeField(syx);
                }
            }
        }

        protected override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                DebugPrint();
                _Part = (IntegraParts)((Address & 0x00000F00) >> 8);

                _PhaseLock = data[0x00];
                NotifyPropertyChanged(string.Empty);
                IsInitialized = true;
            }

            return IsInitialized;
        }
    }


    //public class StudioSetMidi : IntegraBase<StudioSetMidi>
    //{
    //    [Offset(0x0000)] private byte _PhaseLock;
    //    private IntegraParts _Part;
    //    public StudioSetMidi(IntegraParts part) : base(new IntegraAddress(0x18, 0x00, (byte)(0x10 + part), 0x00), 0x00000001)
    //    {
    //        _Part = part;
    //    }

    //    [Offset(0x0000)]
    //    public byte PhaseLock
    //    {
    //        get { return _PhaseLock; }
    //        set
    //        {
    //            _PhaseLock = value;
    //            NotifyPropertyChanged();
    //        }
    //    }

    //    internal override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
    //    {
    //        IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

    //        if (syx.Address == Address)
    //        {

    //            if (!IsInitialized)
    //            {
    //                if (Initialize(syx.Data))
    //                {
    //                    Device.Instance.ReportProgress(this, new StatusMessage($"Initializing Studio Set MIDI {_Part}", "Initialized", 100, "Done"));
    //                }
    //                else
    //                {
    //                    Device.Instance.ReportProgress(this, new StatusMessage($"Initializing Studio Set MIDI {_Part}", "Please wait...", 100, "Initializing"));
    //                }
    //            }
    //            else
    //            {
    //                InitializeField(syx);
    //            }
    //        }
    //    }

    //    protected override bool Initialize(byte[] data)
    //    {
    //        if (!IsInitialized)
    //        {
    //            //_Part = (IntegraParts)((Address & 0x00000F00) >> 8);

    //            _PhaseLock = data[0x00];

    //            IsInitialized = true;
    //        }

    //        return IsInitialized;
    //    }
    //}
}
