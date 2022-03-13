using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.File;
using System.Diagnostics;

namespace IntegraXL.Models
{
    /// <summary>
    /// Defines the collection of <see cref="TemporaryTone"/> models for all 16 parts.
    /// </summary>
    [Integra(0x19000000, 0x04000000)]
    public sealed class TemporaryTones : IntegraPartialCollection<TemporaryTone>
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="TemporaryTones"/> collection instance.
        /// </summary>
        /// <param name="integra">The device to connect the collection.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "The class is created by reflection.")]
        private TemporaryTones(Integra integra) : base(integra) { }

        #endregion

        #region Overrides: Model

        /// <summary>
        /// Requests the device to initialize the <see cref="TemporaryTones"/> collection.
        /// </summary>
        /// <returns>An awaitable task that returns true if the <see cref="TemporaryTones"/> collection is initialized.</returns>
        internal override async Task<bool> InitializeAsync()
        {
            try
            {
                // IMPORTANT! Determin the tone types and create the tone type's associated models before making the initialization request
                foreach (var tone in this)
                {
                   await tone.InitializeToneTypeAsync();
                }
            }
            catch (TaskCanceledException)
            {
                return false;
            }

            return await base.InitializeAsync();
        }

        /// <summary>
        /// Handles the <see cref="Integra.SystemExclusiveReceived"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="Integra"/> that raised the event.</param>
        /// <param name="e">The event's associated data.</param>
        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e) { }

        #endregion
    }

    /// <summary>
    /// Defines the INTEGRA-7 temporary tone model.
    /// </summary>
    [Integra(0x19000000, 0x00200000)]
    public sealed class TemporaryTone : IntegraPartial<TemporaryTone>
    {
        #region Fields

        /// <summary>
        /// Stores a reference to the associated tone.
        /// </summary>
        private readonly IntegraTone _Tone;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="TemporaryTone"/> instance.
        /// </summary>
        /// <param name="integra">The device to connect the model.</param>
        /// <param name="part">The associated part.</param>
        /// <remarks><i>
        /// - The temporary tone 
        /// </i></remarks>
        internal TemporaryTone(Integra integra, Parts part) : base(integra, part, false) 
        {
            Address = Attribute.Address;
            
            // 0x19, 0x19, 0x19, 0x19, 0x20, ...
            Address[0] += (byte)((int)part >> 2); // >> 2 equals division by 4

            // 0x00, 0x20, 0x40, 0x60, 0x00, ...
            Address[1] += (byte)((int)part % 4 * 0x20);

            _Tone = integra.CreateModel<IntegraTone>(Part);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of temporary tone.
        /// </summary>
        public IntegraToneTypes Type { get; private set; }

        /// <summary>
        /// Gets wheter the temporary tone can be modified by the user.
        /// </summary>
        public bool IsEditable { get; private set; }

        #endregion

        #region Properties: INTEGRA-7

        /// <summary>
        /// Gets the SuperNATURAL acoustic tone model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>Can return <see langword="null"/>, check the <see cref="Type"/> property first.</i>
        /// </remarks>
        public SuperNATURALAcousticTone? SuperNATURALAcousticTone { get; private set; }

        /// <summary>
        /// Gets the SuperNATURAL Synth tone model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>Can return <see langword="null"/>, check the <see cref="Type"/> property first.</i>
        /// </remarks>
        public SuperNATURALSynthTone? SuperNATURALSynthTone { get; private set; }

        /// <summary>
        /// Gets the SuperNATURAL drum kit model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>Can return <see langword="null"/>, check the <see cref="Type"/> property first.</i>
        /// </remarks>
        public SuperNATURALDrumKit? SuperNATURALDrumKit { get; private set; }

        /// <summary>
        /// Gets the PCM synth tone model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>Can return <see langword="null"/>, check the <see cref="Type"/> property first.</i>
        /// </remarks>
        public PCMSynthTone? PCMSynthTone { get; private set; }

        /// <summary>
        /// Gets the PCM drum kit model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>Can return <see langword="null"/>, check the <see cref="Type"/> property first.</i>
        /// </remarks>
        public PCMDrumKit? PCMDrumKit { get; private set; }

        /// <summary>
        /// Gets the tone MFX model.
        /// </summary>
        public MFX? MFX { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines, creates and initializes the specific type of <see cref="TemporaryTone"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IntegraException"></exception>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>This method has to be called before the initialization request is made.</i><br/><br/>
        /// - Determines the type of temporary tone<br/>
        /// - Binds the tone changed event<br/>
        /// - Creates the temporary tone type's associated model<br/>
        /// - Sets the associated MFX address<br/>
        /// </remarks>
        internal async Task<bool> InitializeToneTypeAsync()
        {
            try
            {
                // Prevents duplicate event listeners although the method should be called only once
                _Tone.Changed -= ToneChanged;

                // TODO: Can return false
                if (!_Tone.IsInitialized)
                    await Device.InitializeModel(_Tone);

                _Tone.Changed += ToneChanged;

                Type       = _Tone.Type;
                IsEditable = _Tone.IsEditable; // REQUIRED: Required by the child models

                NotifyPropertyChanged(nameof(Type));
            }
            catch (Exception)
            {
                return false;
            }

            // TODO: Handle expansion tones that are not loaded

            MFX = new MFX(this);

            switch (Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:
                    SuperNATURALAcousticTone = new SuperNATURALAcousticTone(this);
                    MFX.Address |= SuperNATURALAcousticTone.Address;
                    break;

                case IntegraToneTypes.SuperNATURALSynthTone:
                    SuperNATURALSynthTone = new SuperNATURALSynthTone(this);
                    MFX.Address |= SuperNATURALSynthTone.Address;
                    break;

                case IntegraToneTypes.SuperNATURALDrumkit:
                    SuperNATURALDrumKit = new SuperNATURALDrumKit(this);
                    MFX.Address |= SuperNATURALDrumKit.Address;
                    break;

                case IntegraToneTypes.PCMSynthTone:
                    PCMSynthTone = new PCMSynthTone(this);
                    MFX.Address |= PCMSynthTone.Address;
                    break;

                case IntegraToneTypes.PCMDrumkit:
                    PCMDrumKit = new PCMDrumKit(this);
                    MFX.Address |= PCMDrumKit.Address;
                    break;

                default:
                    throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(InitializeToneTypeAsync)}({Part})]\n" +
                                               $"Unspecified temporary tone type.");
            }

            return true;
        }

        #endregion

        #region Overrides: Model

        public override bool IsDirty 
        { 
            get
            {
                if (MFX != null && MFX.IsDirty)
                    return true;

                switch(Type)
                {
                    case IntegraToneTypes.SuperNATURALAcousticTone: 
                        return SuperNATURALAcousticTone != null && SuperNATURALAcousticTone.IsDirty;

                    case IntegraToneTypes.SuperNATURALSynthTone:
                        return SuperNATURALSynthTone != null && SuperNATURALSynthTone.IsDirty;

                    case IntegraToneTypes.SuperNATURALDrumkit:
                        return SuperNATURALDrumKit != null && SuperNATURALDrumKit.IsDirty;

                    case IntegraToneTypes.PCMSynthTone:
                        return PCMSynthTone != null && PCMSynthTone.IsDirty;

                    case IntegraToneTypes.PCMDrumkit:
                        return PCMDrumKit != null && PCMDrumKit.IsDirty;

                    default:
                        return false;
                };
            }

            protected set => base.IsDirty = value; 
        }

        /// <summary>
        /// Requests the device to initialize the temporary tone.
        /// </summary>
        /// <returns>An awaitable task that returns true if the temporary tone is initialized.</returns>
        internal async override Task<bool> InitializeAsync()
        {
            // IMPORTANT: The method has to be called before the base method to determine the tone type and create the tone type's associated model
            if (!await InitializeToneTypeAsync())
                return false;

            return await base.InitializeAsync();
        }

        /// <summary>
        /// Handles the <see cref="Integra.SystemExclusiveReceived"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="Integra"/> that raised the event.</param>
        /// <param name="e">The event's associated data.</param>
        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e) { }

        /// <summary>
        /// Gets wheter the <see cref="TemporaryTone"/> is initialized.
        /// </summary>
        public override bool IsInitialized
        {
            get
            {
                switch (Type)
                {
                    case IntegraToneTypes.SuperNATURALAcousticTone:
                        return SuperNATURALAcousticTone != null && SuperNATURALAcousticTone.IsInitialized;

                    case IntegraToneTypes.SuperNATURALSynthTone:
                        return SuperNATURALSynthTone != null && SuperNATURALSynthTone.IsInitialized;

                    case IntegraToneTypes.SuperNATURALDrumkit:
                        return SuperNATURALDrumKit != null && SuperNATURALDrumKit.IsInitialized;

                    case IntegraToneTypes.PCMSynthTone:
                        return PCMSynthTone != null && PCMSynthTone.IsInitialized;

                    case IntegraToneTypes.PCMDrumkit:
                        return PCMDrumKit != null && PCMDrumKit.IsInitialized;

                    default:
                        return false;
                }
            }

            internal protected set => base.IsInitialized = value;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the <see cref="IntegraTone.Changed"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="IntegraTone"/> that raised the event.</param>
        /// <param name="e">The tone data.</param>
        private void ToneChanged(object? sender, IntegraToneChangedEventArgs e)
        {
            Debug.Print($"[{nameof(TemporaryTone)}] *** {nameof(ToneChanged)} ***");

            SuperNATURALAcousticTone = null;
            SuperNATURALSynthTone = null;
            SuperNATURALDrumKit = null;
            PCMSynthTone = null;
            PCMDrumKit = null;

            ReinitializeAsync();
            // Old: Remove type from device cache
            // Old: Disconnect
            // Old: Remove MFX ?
            // User message no longer valid?
            // New: Set type
            // New: Reinit
            // New: New / Reinit MFX
        }

        internal override bool Initialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void Load(TemporaryToneFile file)
        {
            SuperNATURALAcousticTone = null;
            SuperNATURALSynthTone = null;
            SuperNATURALDrumKit = null;
            PCMSynthTone = null;
            PCMDrumKit = null;

            Type = (IntegraToneTypes)file.ToneType;

            MFX = new MFX(this);

            switch(Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:

                    SuperNATURALAcousticTone = new SuperNATURALAcousticTone(this);
                    MFX.Address |= SuperNATURALAcousticTone.Address;

                    SuperNATURALAcousticTone.Common.Load(file.SuperNATURALAcousticToneCommon);

                    break;

                case IntegraToneTypes.SuperNATURALSynthTone:

                    SuperNATURALSynthTone = new SuperNATURALSynthTone(this);
                    MFX.Address |= SuperNATURALSynthTone.Address;

                    SuperNATURALSynthTone.Common.Load(file.SuperNATURALSynthToneCommon);

                    for (int i = 0; i < IntegraConstants.SNS_PARTIAL_COUNT; i++)
                    {
                        SuperNATURALSynthTone.Partials[i].Load(file.SuperNATURALSynthTonePartials[i]);
                    }

                    break;

                case IntegraToneTypes.SuperNATURALDrumkit:

                    SuperNATURALDrumKit = new SuperNATURALDrumKit(this);
                    MFX.Address |= SuperNATURALDrumKit.Address;

                    SuperNATURALDrumKit.Common.Load(file.SuperNATURALDrumKitCommon);
                    SuperNATURALDrumKit.CompEQ.Load(file.SuperNATURALDrumKitCommonCompEQ);

                    for (int i = 0; i < IntegraConstants.SND_NOTE_COUNT; i++)
                    {
                        SuperNATURALDrumKit.Notes[i].Load(file.SuperNATURALDrumKitNotes[i]);
                    }

                    break;

                case IntegraToneTypes.PCMSynthTone:

                    PCMSynthTone = new PCMSynthTone(this);
                    MFX.Address |= PCMSynthTone.Address;

                    PCMSynthTone.Common.Load(file.PCMSynthToneCommon);
                    PCMSynthTone.PMT.Load(file.PMT);

                    for (int i = 0; i < IntegraConstants.PCM_PARTIAL_COUNT; i++)
                    {
                        PCMSynthTone.Partials[i].Load(file.PCMSynthTonePartials[i]);
                    }

                    PCMSynthTone.Common02.Load(file.PCMSynthToneCommon2);

                    break;

                case IntegraToneTypes.PCMDrumkit:

                    PCMDrumKit = new PCMDrumKit(this);
                    MFX.Address |= PCMDrumKit.Address;

                    PCMDrumKit.Common.Load(file.PCMDrumKitCommon);
                    PCMDrumKit.CompEQ.Load(file.PCMDrumKitCommonCompEQ);

                    for (int i = 0; i < IntegraConstants.PCM_NOTE_COUNT; i++)
                    {
                        PCMDrumKit.Partials[i].Load(file.PCMDrumKitNotes[i]);
                    }

                    PCMDrumKit.Common02.Load(file.PCMDrumKitCommon2);

                    break;
            }

            MFX.Load(file.MFX);
            Store();
        }

        /// <summary>
        /// Creates a new <see cref="TemporaryToneFile"/> initialized with the <see cref="TemporaryTone"/>'s data. 
        /// </summary>
        /// <returns>A <see cref="TemporaryToneFile"/> containing the <see cref="TemporaryTone"/>'s data.</returns>
        /// <exception cref="IntegraException"/>
        internal TemporaryToneFile Save()
        {
            Debug.Print($"[{nameof(TemporaryTone)}.{nameof(Save)}()] {Part}, {Type} ");

            if (_Tone == null || _Tone.IsInitialized == false)
                throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n{nameof(IntegraTone)} is uninitialized.");

            TemporaryToneFile file = new()
            {
                Header = FileManager.TEMPORARY_TONE_FILE_HEADER,
                ToneType  = (uint)Type, 
                Expansion = (byte)_Tone.GetExpansion()
            };

            switch (Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:

                    if (SuperNATURALAcousticTone == null || SuperNATURALAcousticTone.IsInitialized == false)
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n{nameof(SuperNATURALAcousticTone)} is uninitialized.");

                    file.SuperNATURALAcousticToneCommon = SuperNATURALAcousticTone.Common.Serialize();

                    break;

                case IntegraToneTypes.SuperNATURALSynthTone:
                    
                    if (SuperNATURALSynthTone == null || SuperNATURALSynthTone.IsInitialized == false)
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n{nameof(SuperNATURALSynthTone)} is uninitialized.");

                    file.SuperNATURALSynthToneCommon = SuperNATURALSynthTone.Common.Serialize();

                    for (int i = 0; i < IntegraConstants.SNS_PARTIAL_COUNT; i++)
                    {
                        file.SuperNATURALSynthTonePartials[i] = SuperNATURALSynthTone.Partials[i].Serialize();
                    }

                    break;

                case IntegraToneTypes.SuperNATURALDrumkit:

                    if (SuperNATURALDrumKit == null || SuperNATURALDrumKit.IsInitialized == false)
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n{nameof(SuperNATURALDrumKit)} is uninitialized.");

                    file.SuperNATURALDrumKitCommon = SuperNATURALDrumKit.Common.Serialize();
                    file.SuperNATURALDrumKitCommonCompEQ = SuperNATURALDrumKit.CompEQ.Serialize();

                    for (int i = 0; i < IntegraConstants.SND_NOTE_COUNT; i++)
                    {
                        file.SuperNATURALDrumKitNotes[i] = SuperNATURALDrumKit.Notes[i].Serialize();
                    }

                    break;

                case IntegraToneTypes.PCMSynthTone:

                    if (PCMSynthTone == null || PCMSynthTone.IsInitialized == false)
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n{nameof(PCMSynthTone)} is uninitialized.");

                    if(PCMSynthTone.IsEditable == false)
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n{nameof(PCMSynthTone)} is not editable.");

                    file.PCMSynthToneCommon = PCMSynthTone.Common.Serialize();
                    file.PMT = PCMSynthTone.PMT.Serialize();

                    for (int i = 0; i < IntegraConstants.PCM_PARTIAL_COUNT; i++)
                    {
                        file.PCMSynthTonePartials[i] = PCMSynthTone.Partials[i].Serialize();
                    }

                    file.PCMSynthToneCommon2 = PCMSynthTone.Common02.Serialize();

                    break;

                case IntegraToneTypes.PCMDrumkit:
                    
                    if (PCMDrumKit == null || PCMDrumKit.IsInitialized == false)
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n{nameof(PCMDrumKit)} is uninitialized.");

                    if (PCMDrumKit.IsEditable == false)
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n{nameof(PCMDrumKit)} is not editable.");

                    file.PCMDrumKitCommon = PCMDrumKit.Common.Serialize();
                    file.PCMDrumKitCommonCompEQ = PCMDrumKit.CompEQ.Serialize();

                    for (int i = 0; i < IntegraConstants.PCM_NOTE_COUNT; i++)
                    {
                        file.PCMDrumKitNotes[i] = PCMDrumKit.Partials[i].Serialize();
                    }

                    file.PCMDrumKitCommon2 = PCMDrumKit.Common02.Serialize();

                    break;
            }

            if (MFX == null || MFX.IsInitialized == false)
                throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n{nameof(MFX)} is uninitialized.");

            file.MFX = MFX.Serialize();

            return file;
        }

        public void Store()
        {
            Debug.Print($"[{nameof(TemporaryTone)}.{nameof(Store)}()] {Type}");

            byte[] data = new byte[4];
            int index = 0; // User tone bank ID to store the temporary tone
            switch (Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:
                    if (index < 0 || index > 255)
                        throw new IntegraException($"");

                    data[0] = 0x59;

                    if (index > 127)
                    {
                        data[1] = 0x01;
                        index -= 128;
                        data[2] = (byte)index;
                    }
                    else
                    {
                        data[1] = 0x00;
                        data[2] = (byte)index;
                    }

                    break;

                case IntegraToneTypes.SuperNATURALSynthTone:
                    if (index < 0 || index > 511)
                        throw new IntegraException($"");
                    data[0] = 0x5F;

                    if (index >= 128)
                    {
                        data[1] = (byte)(index / 128);
                        index -= 128 * data[1];
                        data[2] = (byte)index;
                    }
                    else
                    {
                        data[1] = 0x00;
                        data[2] = (byte)index;
                    }

                    break;

                case IntegraToneTypes.SuperNATURALDrumkit:
                    if (index < 0 || index > 63)
                        throw new IntegraException($"");
                    data[0] = 0x58;
                    data[1] = 0x00;
                    data[2] = (byte)index;

                    break;

                case IntegraToneTypes.PCMSynthTone:
                    if (index < 0 || index > 255)
                        throw new IntegraException($"");
                    data[0] = 0x57;
                    if (index > 127)
                    {
                        data[1] = 0x01;
                        index -= 128;
                        data[2] = (byte)index;
                    }
                    else
                    {
                        data[1] = 0x00;
                        data[2] = (byte)index;
                    }

                    break;

                case IntegraToneTypes.PCMDrumkit:
                    if (index < 0 || index > 31)
                        throw new IntegraException($"");
                    data[0] = 0x56;
                    data[1] = 0x00;
                    data[2] = (byte)index;

                    break;
            }

            data[3] = (byte)Part;

            Device.TransmitSystemExclusive(new IntegraSystemExclusive(new IntegraAddress(0x0F001000), new IntegraRequest(data)));

            NotifyPropertyChanged(string.Empty);
        }
    }
}
