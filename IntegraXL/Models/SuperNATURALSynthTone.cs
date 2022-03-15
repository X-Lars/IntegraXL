using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x00010000, 0x00100000)]
    public class SuperNATURALSynthTone : IntegraModel<SuperNATURALSynthTone>
    {
        private IntegraSNSynthToneParts _SelectedPartial = IntegraSNSynthToneParts.Partial01;

        public SuperNATURALSynthTone(TemporaryTone tone) : base(tone.Device, false)
        {
            Address += tone.Address;

            Common   = new SuperNATURALSynthToneCommon(this);
            Partials = new SuperNATURALSynthTonePartials(this);
            Misc     = new SuperNATURALSynthToneMisc(this);
        }

        public int SelectedIndex
        {
            get => (int)_SelectedPartial;
            set => SelectedPartial = (IntegraSNSynthToneParts)value;
        }

        public IntegraSNSynthToneParts SelectedPartial
        {
            get => _SelectedPartial;
            set
            {
                if (_SelectedPartial != value)
                {
                    _SelectedPartial = value;

                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(Partial));
                }
            }
        }

        public SuperNATURALSynthTonePartial? Partial
        {
            get => Partials?[(int)_SelectedPartial];
        }

        public override bool IsInitialized 
        { 
            get => Common.IsInitialized && Partials.IsInitialized && Misc.IsInitialized; 
        }

        public SuperNATURALSynthToneCommon Common { get; }
        public SuperNATURALSynthTonePartials Partials { get; }
        public SuperNATURALSynthToneMisc Misc { get; }
    }
}
