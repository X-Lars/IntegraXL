using IntegraXL.Core;


// TODO: Parameter context based on tone category (See Roland parameter reference)
namespace IntegraXL.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 super natural acoustic tone model.
    /// </summary>
    [Integra(0x00020000, 0x00100000)]
    public class SuperNATURALAcousticTone : IntegraModel
    {
        //private MFX _MFX;

        internal SuperNATURALAcousticTone(TemporaryTone parent) : base(parent.Device)
        {
            Address += parent.Address;
            Common = new SuperNATURALAcousticToneCommon(this);
            //_MFX = parent.MFX;// ?new MFX(temporaryTone);
        }

        public override bool IsInitialized 
        { 
            get => Common.IsInitialized; 
            protected internal set => base.IsInitialized = value; 
        }

        /// <summary>
        /// Gets the INTEGRA-7 super natural acoustic tone common model.
        /// </summary>
        public SuperNATURALAcousticToneCommon Common { get; }
    }
}
