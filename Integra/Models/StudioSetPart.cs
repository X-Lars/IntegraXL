using Integra.Core;
using Integra.Core.Interfaces;
using MidiXL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class StudioSetPart : IntegraBase<StudioSetPart>, IIntegraPartial
    {
        private IntegraParts _Part;
        
        private IntegraToneTypes _Type = IntegraToneTypes.SuperNATURALAcousticTone;

        private TemporaryTone _TemporaryTone;

        

        [Offset(0x0000)] private byte _ReceiveChannel;
        [Offset(0x0001)] private byte _ReceiveSwitch;
        [Offset(0x0006)] private byte _ToneBankSelectMSB;
        [Offset(0x0007)] private byte _ToneBankSelectLSB;
        [Offset(0x0008)] private byte _ToneProgramNumber;

        public StudioSetPart()
        {
        }

        public StudioSetPart(IntegraParts part)// : base(new IntegraAddress(0x18, 0x00, (byte)(0x20 + part), 0x00), new IntegraRequest(0x00, 0x00, 0x00, 0x4D))
        {
            Name = $"Studio Set Part {(int)part + 1}";
            Part = part;

            Address = new IntegraAddress(0x18, 0x00, (byte)(0x20 + part), 0x00);
            Requests.Add(new IntegraRequest(0x0000004D));

            Initialize();
        }

        public IntegraParts Part
        {
            get { return _Part; }
            set
            {
                _Part = value;
                NotifyPropertyChanged();
            }
        }

        public IntegraToneTypes Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                NotifyPropertyChanged();
            }
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
                NotifyPropertyChanged();
            }
        }

        #region Overrides

        

        protected override bool Initialize(byte[] data)
        {
            if(!IsInitialized)
            {
                //Part = (IntegraParts)((Address & 0x00000F00) >> 8);

                base.Initialize(data);

                Type = IntegraToneExtensions.Type(ToneBankSelectMSB);

                TemporaryTone = new TemporaryTone(Part, Type);

                //switch(Type)
                //{
                //    case IntegraToneTypes.SuperNATURALAcousticTone:
                //        SuperNATURALAcousticTone = new SuperNATURALAcousticTone(TemporaryTone.Address);
                //        break;
                //    case IntegraToneTypes.SuperNATURALSynthTone:
                //        SuperNATURALSynthTone = new SuperNATURALSynthTone(TemporaryTone.Address);
                //        break;
                //    case IntegraToneTypes.SuperNATURALDrumkit:
                //        SuperNATURALDrumKit = new SuperNATURALDrumKit(TemporaryTone.Address);
                //        break;
                //    case IntegraToneTypes.PCMSynthTone:
                //        PCMSynthTone = new PCMSynthTone(TemporaryTone.Address);
                //        break;
                //    case IntegraToneTypes.PCMDrumkit:
                //        PCMDrumKit = new PCMDrumKit(TemporaryTone.Address);
                //        break;
                //}

                NotifyPropertyChanged(nameof(Part), false);
                NotifyPropertyChanged(nameof(Type), false);
                IsInitialized = true;
            }

            return IsInitialized;
        }

        
        #endregion
    }
}
