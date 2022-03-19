using IntegraXL.Core;

namespace IntegraXL.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 SuperNATURAL synth tone model.
    /// </summary>
    [Integra(0x00010000, 0x00100000)]
    public sealed class SuperNATURALSynthTone : IntegraModel<SuperNATURALSynthTone>
    {
        #region Fields

        /// <summary>
        /// Tracks the selected partial.
        /// </summary>
        private IntegraSNSynthToneParts _SelectedPartial = IntegraSNSynthToneParts.Partial01;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="SuperNATURALSynthTone"/> instance.
        /// </summary>
        /// <param name="tone">The parent model.</param>
        internal SuperNATURALSynthTone(TemporaryTone tone) : base(tone.Device, false)
        {
            Address += tone.Address;

            Common   = new SuperNATURALSynthToneCommon(this);
            Partials = new SuperNATURALSynthTonePartials(this);
            Misc     = new SuperNATURALSynthToneMisc(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or selects the active partial by index.
        /// </summary>
        public int SelectedIndex
        {
            get => (int)_SelectedPartial;
            set => SelectedPartial = (IntegraSNSynthToneParts)value;
        }

        /// <summary>
        /// Gets or selects the active partial.
        /// </summary>
        public IntegraSNSynthToneParts SelectedPartial
        {
            get => _SelectedPartial;
            set
            {
                if (_SelectedPartial != value)
                {
                    _SelectedPartial = value;

                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(SelectedIndex));
                    NotifyPropertyChanged(nameof(Partial));
                }
            }
        }

        /// <summary>
        /// Gets the partial specified by the <see cref="SelectedIndex"/> or <see cref="SelectedPartial"/> properties.
        /// </summary>
        public SuperNATURALSynthTonePartial? Partial => Partials?[(int)_SelectedPartial];

        #endregion

        #region Properties: INTEGRA-7

        /// <summary>
        /// Gets the SuperNATURAL synth tone common model.
        /// </summary>
        public SuperNATURALSynthToneCommon Common { get; }

        /// <summary>
        /// Gets the SuperNATURAL synth tone partials collection.
        /// </summary>
        public SuperNATURALSynthTonePartials Partials { get; }

        /// <summary>
        /// Gets the SuperNATURAL synth tone misc model.
        /// </summary>
        /// <remarks><i>
        /// The misc model is not documented but has it's own tab on the physical device an can be found when editing SuperNATURAL synth tones.
        /// </i></remarks>
        public SuperNATURALSynthToneMisc Misc { get; }

        #endregion

        #region Overrides: Model

        /// <summary>
        /// Gets whether all models contained by the <see cref="SuperNATURALSynthTone"/> are initialized.
        /// </summary>
        public override bool IsInitialized => Common.IsInitialized && Partials.IsInitialized && Misc.IsInitialized;

        /// <summary>
        /// Gets wheter any of the models contained by the <see cref="SuperNATURALSynthTone"/> has unsaved changes.
        /// </summary>
        public override bool IsDirty => Common.IsDirty || Partials.IsDirty || Misc.IsDirty;

        #endregion
    }
}
