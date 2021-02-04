using Integra.Core;
using Integra.Core.Interfaces;

namespace Integra.Models
{
    public class SuperNATURALSynthTone : IntegraBase<SuperNATURALSynthTone>, IIntegraPartial
    {
        private IntegraParts _Part;
        private SuperNATURALSynthToneCommon _Common;
        public SuperNATURALSynthTone(IntegraAddress address, IntegraParts part) : base(address)
        {
            Name = "SuperNATURAL Synth Tone";
            Part = part;

            Common = new SuperNATURALSynthToneCommon(address, part);
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

        public SuperNATURALSynthToneCommon Common
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
