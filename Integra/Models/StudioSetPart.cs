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

        [Offset(0x0000)] private byte _ReceiveChannel;
        [Offset(0x0001)] private byte _ReceiveSwitch;
        [Offset(0x0006)] private byte _ToneBankSelectMSB;
        [Offset(0x0007)] private byte _ToneBankSelectLSB;
        [Offset(0x0008)] private byte _ToneProgramNumber;

        public StudioSetPart(IntegraParts part) : base(new IntegraAddress(0x18, 0x00, (byte)(0x20 + part), 0x00), new IntegraRequest(0x00, 0x00, 0x00, 0x4D))
        {
            _Part = part;
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

        protected override bool Initialize(byte[] data)
        {
            if(!IsInitialized)
            {
                _Part = (IntegraParts)((Address & 0x00000F00) >> 8);

                base.Initialize(data);
            }

            return IsInitialized;
        }
    }
}
