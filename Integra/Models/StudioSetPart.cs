using Integra.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class StudioSetPart : IntegraBase<StudioSetPart>
    {
        private IntegraParts _Part;
        
        private TemporaryTone _TemporaryTone;
        private IntegraTone _Tone = new IntegraTone();

        private SuperNATURALAcousticTone _SuperNaturalAcousticTone;
        private SuperNATURALSynthTone _SuperNaturalSynthTone;
        private SuperNATURALDrumKit _SuperNaturalDrumKit;
        private PCMSynthTone _PCMSynthTone;
        private PCMDrumKit _PCMDrumKit;

        [Offset(0x0000)] private byte _ReceiveChannel;
        [Offset(0x0001)] private byte _ReceiveSwitch;
        [Offset(0x0006)] private byte _ToneBankSelectMSB;
        [Offset(0x0007)] private byte _ToneBankSelectLSB;
        [Offset(0x0008)] private byte _ToneProgramNumber;

        public StudioSetPart(IntegraParts part) : base(new IntegraAddress(0x18, 0x00, (byte)(0x20 + part), 0x00), new IntegraRequest(0x00, 0x00, 0x00, 0x4D))
        {
            Name = $"Studio Set Part {(int)part + 1}";
            _Part = part;
        }

        public TemporaryTone TemporaryTone
        {
            get { return _TemporaryTone; }
            set
            {
                if(_TemporaryTone != value)
                {
                    _TemporaryTone = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public SuperNATURALAcousticTone SuperNATURALAcousticTone
        {
            get { return _SuperNaturalAcousticTone; }
        }

        public SuperNATURALSynthTone SuperNATURALSynthTone
        {
            get { return _SuperNaturalSynthTone; }
        }

        public SuperNATURALDrumKit SuperNATURALDrumKit
        {
            get { return _SuperNaturalDrumKit; }
        }

        public PCMSynthTone PCMSynthTone
        {
            get { return _PCMSynthTone; }
        }

        public PCMDrumKit PCMDrumKit
        {
            get { return _PCMDrumKit; }
        }

        public IntegraTone Tone
        {
            get { return _Tone; }
            set
            {
                if (_Tone != value)
                {
                    //_Tone = value;

                    ToneBankSelectMSB = value.MSB;
                    ToneBankSelectLSB = value.LSB;
                    ToneProgramNumber = value.PC;

                    _SuperNaturalAcousticTone = null;
                    _SuperNaturalSynthTone = null;
                    _SuperNaturalDrumKit = null;
                    _PCMSynthTone = null;
                    _PCMDrumKit = null;

                    //_TemporaryTone = null;
                    Reinitialize();
                    //NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0000)]
        public IntegraChannels ReceiveChannel
        {
            get { return (IntegraChannels)_ReceiveChannel; }
            set
            {
                _ReceiveChannel = (byte)value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0001)]
        public IntegraSwitch ReceiveSwitch
        {
            get { return (IntegraSwitch)_ReceiveSwitch; }
            set
            {
                _ReceiveSwitch = (byte)value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public byte ToneBankSelectMSB
        {
            get { return _ToneBankSelectMSB; }
            set
            {
                _ToneBankSelectMSB = value;
                _Tone.MSB = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0007)]
        public byte ToneBankSelectLSB
        {
            get { return _ToneBankSelectLSB; }
            set
            {
                _ToneBankSelectLSB = value;
                _Tone.LSB = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0008)]
        public byte ToneProgramNumber
        {
            get { return _ToneProgramNumber; }
            set
            {
                _ToneProgramNumber = value;
                _Tone.PC = value;
                NotifyPropertyChanged();
            }
        }

        protected override bool Initialize(byte[] data)
        {
            if(!IsInitialized)
            {
                _Part = (IntegraParts)((Address & 0x00000F00) >> 8);

                base.Initialize(data);

                switch(IntegraToneExtensions.Type(ToneBankSelectMSB))
                {
                    case IntegraToneTypes.SuperNATURALAcousticTone:
                        _TemporaryTone = new TemporaryTone(_Part, IntegraToneTypes.SuperNATURALAcousticTone);
                        _SuperNaturalAcousticTone = new SuperNATURALAcousticTone(_TemporaryTone.Address);
                        break;
                    case IntegraToneTypes.SuperNATURALSynthTone:
                        _TemporaryTone = new TemporaryTone(_Part, IntegraToneTypes.SuperNATURALSynthTone);
                        _SuperNaturalSynthTone = new SuperNATURALSynthTone(_TemporaryTone.Address);
                        break;
                    case IntegraToneTypes.SuperNATURALDrumkit:
                        _TemporaryTone = new TemporaryTone(_Part, IntegraToneTypes.SuperNATURALDrumkit);
                        _SuperNaturalDrumKit = new SuperNATURALDrumKit(_TemporaryTone.Address);
                        break;
                    case IntegraToneTypes.PCMSynthTone:
                        _TemporaryTone = new TemporaryTone(_Part, IntegraToneTypes.PCMSynthTone);
                        _PCMSynthTone = new PCMSynthTone(_TemporaryTone.Address);
                        break;
                    case IntegraToneTypes.PCMDrumkit:
                        _TemporaryTone = new TemporaryTone(_Part, IntegraToneTypes.PCMDrumkit);
                        _PCMDrumKit = new PCMDrumKit(_TemporaryTone.Address);
                        break;
                }

                //_Tone = new Tone(ToneBankSelectMSB, ToneBankSelectLSB, ToneProgramNumber);
                _Tone.MSB = ToneBankSelectMSB;
                _Tone.LSB = ToneBankSelectLSB;
                _Tone.PC  = ToneProgramNumber;

                NotifyPropertyChanged(string.Empty, false);
                IsInitialized = true;

            }

            return IsInitialized;
        }
    }
}
