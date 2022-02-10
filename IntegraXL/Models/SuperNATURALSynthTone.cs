using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x00010000, 0x00100000)]
    public class SuperNATURALSynthTone : IntegraModel<SuperNATURALSynthTone>
    {
        public SuperNATURALSynthTone(TemporaryTone tone) : base(tone.Device, false)
        {
            // 0x19000000
            // 0x00010000
            Address += tone.Address;

            Common   = new SuperNATURALSynthToneCommon(this);
            Partials = new SuperNATURALSynthTonePartials(this);
            
        }

        public override bool IsInitialized 
        { 
            get => Partials.IsInitialized; 
        }

        public SuperNATURALSynthToneCommon Common { get; }
        public SuperNATURALSynthTonePartials Partials { get; }
    }
}
