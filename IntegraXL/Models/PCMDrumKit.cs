using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x00100000, 0x00100000)]
    public class PCMDrumKit : IntegraModel<PCMDrumKit>
    {
        private int _SelectedIndex;

        public PCMDrumKit(TemporaryTone tone) : base(tone.Device, false)
        {
            Address += tone.Address;

            IsEditable = tone.IsEditable;

            if (IsEditable)
            {
                Common   = new PCMDrumKitCommon(this);
                CompEQ   = new DrumKitCommonCompEQ(this);
                Partials = new PCMDrumKitPartials(this);
                Common02 = new PCMDrumKitCommon02(this);
            }
            else
            {
                IsInitialized = true;
            }
        }

        public int SelectedIndex
        {
            get => _SelectedIndex;
            set
            {
                if (_SelectedIndex != value)
                {
                    _SelectedIndex = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(Partial));
                }
            }
        }

        public bool IsEditable { get; private set; }

        public override bool IsInitialized 
        { 
            get
            {
                if (!IsEditable)
                    return true;

                return Common.IsInitialized && CompEQ.IsInitialized && Partials.IsInitialized && Common02.IsInitialized;
            }
            
        }

        public PCMDrumKitCommon Common { get; }
        public PCMDrumKitCommon02 Common02 { get; }
        public DrumKitCommonCompEQ CompEQ { get; }
        public PCMDrumKitPartials Partials { get; }

        public PCMDrumKitPartial Partial => Partials[SelectedIndex];
    }
}
