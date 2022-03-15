using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x00030000, 0x00100000)]
    public class SuperNATURALDrumKit : IntegraModel<SuperNATURALDrumKit>
    {
        private IntegraSNDNoteIndex _SelectedNote = IntegraSNDNoteIndex.Key27;

        internal SuperNATURALDrumKit(TemporaryTone tone) : base(tone.Device)
        {
            Address += tone.Address;

            Common = new SuperNATURALDrumKitCommon(this);
            CompEQ = new DrumKitCommonCompEQ(this);
            Notes  = new SuperNATURALDrumKitNotes(this);
        }

        public int SelectedIndex
        {
            get => (int)_SelectedNote;
            set => SelectedNote = (IntegraSNDNoteIndex)value;
        }

        public IntegraSNDNoteIndex SelectedNote
        {
            get => _SelectedNote;
            set
            {
                if (_SelectedNote != value)
                {
                    _SelectedNote = value;

                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(Note));
                }
            }
        }
        
        public SuperNATURALDrumKitCommon Common { get; }
        public DrumKitCommonCompEQ CompEQ { get; }
        public SuperNATURALDrumKitNotes Notes { get; }
        public SuperNATURALDrumKitNote Note
        {
            get => Notes[(int)_SelectedNote];
        }
            

        #region Overrides: Model

        public override bool IsInitialized
        {
            get => Common.IsInitialized && CompEQ.IsInitialized && Notes.IsInitialized;
        }

        #endregion
    }
}
