using IntegraXL.Core;

namespace IntegraXL.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 super natural acoustic tone model.
    /// </summary>
    [Integra(0x00020000, 0x00100000)]
    public class SuperNATURALAcousticTone : IntegraModel<SuperNATURALAcousticTone>
    {
        //private MFX _MFX;

        internal SuperNATURALAcousticTone(TemporaryTone tone) : base(tone.Device)
        {
            
            Address += tone.Address;
            Common = new SuperNATURALAcousticToneCommon(this);
            //_MFX = parent.MFX;// ?new MFX(temporaryTone);
        }

        public override bool IsInitialized 
        { 
            get => Common.IsInitialized; 
            internal protected set => base.IsInitialized = value; 
        }

        /// <summary>
        /// Gets the INTEGRA-7 super natural acoustic tone common model.
        /// </summary>
        public SuperNATURALAcousticToneCommon Common { get; }

    }
}
