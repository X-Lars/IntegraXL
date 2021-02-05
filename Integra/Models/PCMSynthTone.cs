using Integra.Core;
using Integra.Core.Interfaces;

namespace Integra.Models
{
    public class PCMSynthTone : IntegraBase<PCMSynthTone>, IIntegraPartial
    {
        private IntegraParts _Part;

        private PCMSynthToneCommon _Common;
        private PCMSynthTonePMT _PMT;
        private PCMSynthToneCommon02 _Common02;
        private IntegraBaseSynthTonePartial<PCMSynthTonePartial> _Partials;

        public PCMSynthTone(IntegraAddress address, IntegraParts part) : base(address + 0x00003000, 0x0000003C)
        {
            Name = "PCM Synth Tone";
            _Part = part;
            Common = new PCMSynthToneCommon(address, part);
            PMT = new PCMSynthTonePMT(address, part);
            _Partials = new IntegraBaseSynthTonePartial<PCMSynthTonePartial>(address, Part);
            Common02 = new PCMSynthToneCommon02(address, part);
        }

        public IntegraParts Part
        {
            get { return _Part; }
            set
            {
                if(_Part != value)
                {
                    _Part = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public PCMSynthToneCommon Common
        {
            get { return _Common; }
            set
            {
                if(_Common != value)
                {
                    _Common = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public PCMSynthTonePMT PMT
        {
            get { return _PMT; }
            set
            {
                if(_PMT != value)
                {
                    _PMT = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraBaseSynthTonePartial<PCMSynthTonePartial> Partials
        {
            get { return _Partials; }
        }

        public PCMSynthToneCommon02 Common02
        {
            get { return _Common02; }
            set
            {
                if(_Common02 != value)
                {
                    _Common02 = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
