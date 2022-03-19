using IntegraXL.Core;

namespace IntegraXL.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 SuperNATURAL acoustic tone model.
    /// </summary>
    [Integra(0x00020000, 0x00100000)]
    public class SuperNATURALAcousticTone : IntegraModel<SuperNATURALAcousticTone>
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="SuperNATURALAcousticTone"/> instance.
        /// </summary>
        /// <param name="tone">The parent model.</param>
        internal SuperNATURALAcousticTone(TemporaryTone tone) : base(tone.Device)
        {
            Address += tone.Address;
            Common = new SuperNATURALAcousticToneCommon(this);
        }

        #endregion

        #region Properties: INTEGRA-7
        
        /// <summary>
        /// Gets the INTEGRA-7 SuperNATURAL acoustic tone common model.
        /// </summary>
        public SuperNATURALAcousticToneCommon Common { get; }

        #endregion

        #region Overrides: Model

        /// <summary>
        /// Gets whether all models contained by the <see cref="SuperNATURALAcousticTone"/> are initialized.
        /// </summary>
        public override bool IsInitialized => Common.IsInitialized;

        /// <summary>
        /// Gets wheter any of the models contained by the <see cref="SuperNATURALAcousticTone"/> has unsaved changes.
        /// </summary>
        public override bool IsDirty => Common.IsDirty;

        #endregion

    }
}
