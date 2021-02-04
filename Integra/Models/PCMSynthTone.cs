using Integra.Core;

namespace Integra.Models
{
    public class PCMSynthTone : IntegraBase<PCMSynthTone>
    {
        private IntegraParts _Part;

        private PCMSynthToneCommon _Common;

        public PCMSynthTone(IntegraAddress address, IntegraParts part) : base(address)
        {
            Name = "PCM Synth Tone";
            Part = part;
            Common = new PCMSynthToneCommon(address, part);
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
    }
}
