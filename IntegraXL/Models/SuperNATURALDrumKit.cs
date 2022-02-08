using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x00030000, 0x00100000)]
    public class SuperNATURALDrumKit : IntegraModel<SuperNATURALDrumKit>
    {
        internal SuperNATURALDrumKit(TemporaryTone tone) : base(tone.Device)
        {
            Address += tone.Address;

            Common = new SuperNATURALDrumKitCommon(this);
            CompEQ = new DrumKitCommonCompEQ(this);
            Notes  = new SuperNATURALDrumKitNotes(this);
        }

        
        public SuperNATURALDrumKitCommon Common { get; }
        public DrumKitCommonCompEQ CompEQ { get; }
        public SuperNATURALDrumKitNotes Notes { get; }

        #region Overrides: Model

        public override bool IsInitialized
        {
            get => Notes.IsInitialized;
        }

        #endregion
    }
}
