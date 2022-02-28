using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x00000000, 0x00100000)]
    public class PCMSynthTone : IntegraModel<PCMSynthTone>
    {
        private IntegraPCMSynthToneParts _SelectedPartial = IntegraPCMSynthToneParts.Partial01;

        public PCMSynthTone(TemporaryTone tone) : base(tone.Device, false)
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

        
        public IntegraPCMSynthToneParts SelectedPartial
        {
            get => _SelectedPartial;
            set
            {
                if(_SelectedPartial != value)
                {
                    _SelectedPartial = value;

                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(Partial));
                }
            }
        }


        public PCMSynthTonePartial? Partial
        {
            get => Partials?[(int)_SelectedPartial];
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
        }

        public PCMSynthToneCommon Common { get; }
        public PCMSynthTonePMT PMT { get; }
        public PCMSynthTonePartials Partials { get; }
        public PCMSynthToneCommon02 Common02 { get; }

    }
}
