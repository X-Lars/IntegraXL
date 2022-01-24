using IntegraXL.Core;

namespace IntegraXL.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 studio set model.
    /// </summary>
    [Integra(0x18000000, 0x01000000)]
    public sealed class StudioSet : IntegraModel<StudioSet>
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="StudioSet"/> model instance.
        /// </summary>
        /// <param name="device">The device that manages the model.</param>
        private StudioSet(Integra device) : base(device)
        {
            Common                 = device.CreateModel<StudioSetCommon>();
            CommonChorus           = device.CreateModel<StudioSetCommonChorus>();
            CommonReverb           = device.CreateModel<StudioSetCommonReverb>();
            CommonMotionalSurround = device.CreateModel<StudioSetCommonMotionalSurround>();
            MasterEQ               = device.CreateModel<StudioSetMasterEQ>();
            Midis                  = device.CreateModel<StudioSetMidis>();
            Parts                  = device.CreateModel<StudioSetParts>();
            PartEQs                = device.CreateModel<StudioSetPartEQs>();

            // TODO: Determin last models address value to filter system exclusives for IsInitialized?

            // Raises the property change event to update properties indexed by the selected part
            Device.PartChanged += PartChanged;

            // TODO: Remove event listener
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specified studio set part model.
        /// </summary>
        /// <param name="part">The part to retreive.</param>
        /// <returns>The specified studio set part model.</returns>
        public StudioSetPart this[Parts part] 
        {
            get => Parts[(int)part];
        }

        /// <summary>
        /// Gets the specified studio set part model.
        /// </summary>
        /// <param name="part">The <i>zero based</i> part to retreive</param>
        /// <returns>The specified studio set part model.</returns>
        public StudioSetPart this[int part]
        {
            get => Parts[part];
        }

        /// <summary>
        /// Gets the studio set common model.
        /// </summary>
        public StudioSetCommon Common { get; private set; }

        /// <summary>
        /// Gets the studio set common chorus model.
        /// </summary>
        public StudioSetCommonChorus CommonChorus { get; private set; }

        /// <summary>
        /// Gets the studio set common reverb model.
        /// </summary>
        public StudioSetCommonReverb CommonReverb { get; private set; }

        /// <summary>
        /// Gets the studio set common motional surround model.
        /// </summary>
        public StudioSetCommonMotionalSurround CommonMotionalSurround { get; private set; }

        /// <summary>
        /// Gets the studio set master EQ.
        /// </summary>
        public StudioSetMasterEQ MasterEQ { get; private set; }

        /// <summary>
        /// Gets the collection of each part's studio set MIDI models.
        /// </summary>
        public StudioSetMidis Midis { get; private set; }

        /// <summary>
        /// Gets the collection of each part's studio set part models.
        /// </summary>
        public StudioSetParts Parts { get; private set; }

        /// <summary>
        /// Gets the collection of each part's studio set EQ models.
        /// </summary>
        public StudioSetPartEQs PartEQs { get; private set; }

        /// <summary>
        /// Gets the studio set part model based on the active selected part.
        /// </summary>
        public StudioSetPart Part => Parts[(int)Device.SelectedPart];

        /// <summary>
        /// Gets the studio set MIDI model based on the active selected part.
        /// </summary>
        public StudioSetMidi Midi => Midis[(int)Device.SelectedPart];

        /// <summary>
        /// Gets the studio set EQ model based on the active selected part.
        /// </summary>
        public StudioSetPartEQ PartEQ => PartEQs[(int)Device.SelectedPart];

        #endregion

        #region Overrides

        /// <summary>
        /// Gets wheter the studio set is initialized.
        /// </summary>
        public override bool IsInitialized 
        { 
            get => PartEQs.IsInitialized; 
            protected internal set => base.IsInitialized = value; 
        }

        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            //if (!IsInitialized)
            //    if (e.SystemExclusive.Address == PartEQs[15].Address)
            //        if (e.SystemExclusive.Data.Length == PartEQs[15].Size)
            //        {
            //            IsInitialized = true;
            //            Disconnect();
            //        }
        }

        /// <summary>
        /// Gets a hash code based on the model's address with the LSB maxed out.
        /// </summary>
        /// <returns>A hash code for the model.</returns>
        protected internal override int GetModelHash()
        {
            // Base hash conflicts with studio set common hash
            return base.GetModelHash() | 0xFF;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the <see cref="Integra.PartChanged"/> event.
        /// </summary>
        /// <param name="sender">The device that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void PartChanged(object? sender, IntegraPartChangedEventArgs e)
        {
            NotifyPropertyChanged(string.Empty);
        }

        #endregion
    }
}
