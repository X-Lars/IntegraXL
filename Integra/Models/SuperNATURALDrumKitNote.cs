using Integra.Common;
using Integra.Core;
using Integra.Core.Interfaces;
using MidiXL;

namespace Integra.Models
{
    public class SuperNATURALDrumKitNote : IntegraBase<SuperNATURALDrumKitNote>, IIntegraPartial, IIntegraDrumKitPartial
    {
        private IntegraParts _Part;
        private int _Note;

        [Offset(0x0000)] int _InstNumber;
        [Offset(0x0004)] byte _NoteLevel;
        [Offset(0x0005)] byte _Pan;
        [Offset(0x0006)] byte _ChorusSendLevel;
        [Offset(0x0007)] byte _ReverbSendLevel;
        [Offset(0x0008)] int _Tune;
        [Offset(0x000C)] byte _Attack;
        [Offset(0x000D)] byte _Decay;
        [Offset(0x000E)] byte _Brilliance;
        [Offset(0x000F)] IntegraNoteVariation _Variation;
        [Offset(0x0010)] byte _DynamicRange;
        [Offset(0x0011)] byte _StereoWidth;
        [Offset(0x0012)] IntegraNoteOutputAssign _OutputAssign;

        public SuperNATURALDrumKitNote()
        {

        }

        public IntegraParts Part
        {
            get { return _Part; }
            set
            {
                if (_Part != value)
                {
                    _Part = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Note
        {
            get { return _Note; }
            set
            {
                _Note = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0000)]
        public int InstNumber
        {
            get { return _InstNumber.GetIntegraValue(); }
            set
            {
                _InstNumber = value.SetIntegraValue();
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0004)]
        public byte NoteLevel
        {
            get { return _NoteLevel; }
            set
            {
                _NoteLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0005)]
        public byte Pan
        {
            get { return _Pan; }
            set
            {
                _Pan = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public byte ChorusSendLevel
        {
            get { return _ChorusSendLevel; }
            set
            {
                _ChorusSendLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0007)]
        public byte ReverbSendLevel
        {
            get { return _ReverbSendLevel; }
            set
            {
                _ReverbSendLevel = value;
                NotifyPropertyChanged();

            }
        }
        
        [Offset(0x0008)]
        public int Tune
        {
            // TODO: Calulate tune values -1200 / 1200 (8 / 248)
            get { return _Tune.GetIntegraValue(); }
            set
            {
                _Tune = value.SetIntegraValue();
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000C)]
        public byte Attack
        {
            get { return _Attack; }
            set
            {
                _Attack = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000D)] 
        public byte Decay
        {
            get { return _Decay; }
            set
            {
                _Decay = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000E)] 
        public byte Brilliance
        {
            get { return _Brilliance; }
            set
            {
                _Brilliance = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000F)] 
        public IntegraNoteVariation Variation
        {
            get { return _Variation; }
            set
            {
                _Variation = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0010)] 
        public byte DynamicRange
        {
            get { return _DynamicRange; }
            set
            {
                _DynamicRange = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0011)] 
        public byte StereoWidth
        {
            get { return _StereoWidth; }
            set
            {
                _StereoWidth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0012)] 
        public IntegraNoteOutputAssign OutputAssign
        {
            get { return _OutputAssign; }
            set
            {
                _OutputAssign = value;
                NotifyPropertyChanged();
            }
        }

        // TODO: Remove temporary iintegrapartial issue
        protected override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);



            if (!IsInitialized)
            {
                if (syx.Address == Address)
                {
                    // Exact match
                    if (syx.Data.Length == Requests[0].Size)
                    {
                        if (Initialize(syx.Data))
                        {
                            Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
                        }
                    }
                    else
                    {
                        InitializeField(syx);
                    }
                }
            }
            else
            {
                //TODO: Check MFX 0xFFFFFF00 won't catch 00 00 01 11 ? or is it catched because of receiving a complete array
                if ((syx.Address & 0xFFFFFF00) == (Address & 0xFFFFFF00))
                {
                    InitializeField(syx);
                }
            }
        }
    }
}
