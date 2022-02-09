using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x00000000, 0x00100000)]
    public class PCMSynthTone : IntegraModel<PCMSynthTone>
    {
        public PCMSynthTone(TemporaryTone tone) : base(tone.Device)
        {
            Address += tone.Address;

            IsEditable = tone.IsEditable;

            if (IsEditable)
            {
                Common = new PCMSynthToneCommon(this);
                PMT = new PCMSynthTonePMT(this);
                Partials = new PCMSynthTonePartials(this);
                Common02 = new PCMSynthToneCommon02(this);
            }
            else
            {
                IsInitialized = true;
            }
        }

        public bool IsEditable { get; private set; }

        public override bool IsInitialized 
        { 
            get
            {
                if (!IsEditable)
                    return true;

                return Common02.IsInitialized;
            }

            protected internal set => base.IsInitialized = value; 
        }

        public PCMSynthToneCommon Common { get; }
        public PCMSynthTonePMT PMT { get; }
        public PCMSynthTonePartials Partials { get; }
        public PCMSynthToneCommon02 Common02 { get; }

    }
}
