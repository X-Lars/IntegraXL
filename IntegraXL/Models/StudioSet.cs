﻿using IntegraXL.Core;
using IntegraXL.File;
using System.Diagnostics.CodeAnalysis;

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
        /// Creates a new <see cref="StudioSet"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the model.</param>
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "The class is created by reflection.")]
        private StudioSet(Integra device) : base(device, false)
        {
            Common                 = device.CreateChildModel<StudioSetCommon>();
            CommonChorus           = device.CreateChildModel<StudioSetCommonChorus>();
            CommonReverb           = device.CreateChildModel<StudioSetCommonReverb>();
            CommonMotionalSurround = device.CreateChildModel<StudioSetCommonMotionalSurround>();
            MasterEQ               = device.CreateChildModel<StudioSetMasterEQ>();
            Midis                  = device.CreateChildModel<StudioSetMidis>();
            Parts                  = device.CreateChildModel<StudioSetParts>();
            PartEQs                = device.CreateChildModel<StudioSetPartEQs>();

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
        public StudioSetPart Part => Parts[(int)Device.PartIndex];

        /// <summary>
        /// Gets the studio set MIDI model based on the active selected part.
        /// </summary>
        public StudioSetMidi Midi => Midis[(int)Device.PartIndex];

        /// <summary>
        /// Gets the studio set EQ model based on the active selected part.
        /// </summary>
        public StudioSetPartEQ PartEQ => PartEQs[(int)Device.PartIndex];

        #endregion

        #region Overrides

        /// <summary>
        /// Gets wheter the studio set is initialized.
        /// </summary>
        public override bool IsInitialized 
        { 
            get => PartEQs.IsInitialized; 
            internal protected set => base.IsInitialized = value; 
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
        internal protected override int GetUID()
        {
            // Base hash conflicts with studio set common hash
            return base.GetUID() | 0xFF;
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

        public void Load(StudioSetFile file)
        {
            Common.Load(file.Common);
            CommonChorus.Load(file.CommonChorus);
            CommonReverb.Load(file.CommonReverb);
            CommonMotionalSurround.Load(file.MotionalSurround);
            MasterEQ.Load(file.MasterEQ);

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                Midis[i].Load(file.Midis[i]);
            }

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                Parts[i].Load(file.Parts[i]);
            }

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                PartEQs[i].Load(file.PartEQs[i]);
            }
        }

        internal StudioSetFile Save()
        {
            StudioSetFile file = FileManager.CreateStudioSetFile();

            file.Common = Common.Serialize();
            file.CommonChorus = CommonChorus.Serialize();
            file.CommonReverb = CommonReverb.Serialize();
            file.MotionalSurround = CommonMotionalSurround.Serialize();
            file.MasterEQ = MasterEQ.Serialize();

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                file.Midis[i] = Midis[i].Serialize();
            }

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                file.Parts[i] = Parts[i].Serialize();
            }

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                file.PartEQs[i] = PartEQs[i].Serialize();
            }

            return file;
        }
    }
}
