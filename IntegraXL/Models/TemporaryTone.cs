using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.File;
using IntegraXL.Interfaces;
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
    public sealed class TemporaryTone : IntegraPartial<TemporaryTone>, IBankSelect
    {
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

            MFX = new MFX(this);

            Device.ToneChanged += ToneChanged;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of temporary tone.
        /// </summary>
        public IntegraToneTypes Type { get; private set; }

        /// <summary>
        /// Gets wheter the temporary tone can be edited by the user.
        /// </summary>
        /// <remarks><i>
        /// The tone is not editable if it is a GM2 or ExPCM expansion tone or drum kit.
        /// </i></remarks>
        public bool IsEditable { get; private set; }

        /// <summary>
        /// Gets the name of the temporary tone or drum kit.
        /// </summary>
        /// <remarks><i>
        /// For display purpose, can be edited via the associated tone's Common.ToneName or Common.DrumKit property.
        /// </i></remarks>
        public string? TemporaryToneName
        {
            get
            {
                switch (Type)
                {
                    case IntegraToneTypes.SuperNATURALAcousticTone: return SuperNATURALAcousticTone?.Common.ToneName;
                    case IntegraToneTypes.SuperNATURALSynthTone: return SuperNATURALSynthTone?.Common.ToneName;
                    case IntegraToneTypes.SuperNATURALDrumkit: return SuperNATURALDrumKit?.Common.KitName;
                    case IntegraToneTypes.PCMSynthTone: return PCMSynthTone?.Common?.ToneName;
                    case IntegraToneTypes.PCMDrumkit: return PCMDrumKit?.Common.KitName;

                    default:
                        return string.Empty;
                }
            }
        }

        #endregion

        #region Properties: INTEGRA-7

        /// <summary>
        /// Gets the SuperNATURAL acoustic tone model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>Returns <see langword="null"/> if the <see cref="Type"/> property not equals <see cref="IntegraToneTypes.SuperNATURALAcousticTone"/>.</i>
        /// </remarks>
        public SuperNATURALAcousticTone? SuperNATURALAcousticTone { get; private set; }

        /// <summary>
        /// Gets the SuperNATURAL synth tone model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>Returns <see langword="null"/> if the <see cref="Type"/> property not equals <see cref="IntegraToneTypes.SuperNATURALSynthTone"/>.</i>
        /// </remarks>
        public SuperNATURALSynthTone? SuperNATURALSynthTone { get; private set; }

        /// <summary>
        /// Gets the SuperNATURAL drum kit model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>Returns <see langword="null"/> if the <see cref="Type"/> property not equals <see cref="IntegraToneTypes.SuperNATURALDrumkit"/>.</i>
        /// </remarks>
        public SuperNATURALDrumKit? SuperNATURALDrumKit { get; private set; }

        /// <summary>
        /// Gets the PCM synth tone model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>Return <see langword="null"/> if the <see cref="Type"/> property not equals <see cref="IntegraToneTypes.PCMSynthTone"/>.</i>
        /// </remarks>
        public PCMSynthTone? PCMSynthTone { get; private set; }

        /// <summary>
        /// Gets the PCM drum kit model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>Returns <see langword="null"/> if the <see cref="Type"/> property not equals <see cref="IntegraToneTypes.PCMDrumkit"/>.</i>
        /// </remarks>
        public PCMDrumKit? PCMDrumKit { get; private set; }

        /// <summary>
        /// Gets the tone MFX model.
        /// </summary>
        public MFX MFX { get; private set; }

        #endregion

        #region Properties: IBankSelect

        /// <summary>
        /// Gets the (M)ost (S)ignificant (B)yte of the <see cref="TemporaryTone"/>'s base tone, represents the MIDI control change bank select MSB.
        /// </summary>
        /// <remarks><i>MIDI Controller 0.</i></remarks>
        public byte MSB { get; private set; }

        /// <summary>
        /// Gets the (L)east (S)ignificant (B)yte of the <see cref="TemporaryTone"/>'s base tone, represents the MIDI control change bank select LSB.
        /// </summary>
        /// <remarks><i>MIDI Controller 32.</i></remarks>
        public byte LSB { get; private set; }

        /// <summary>
        /// Gets the (P)rogram (C)hange of the <see cref="TemporaryTone"/>'s base tone, represents the MIDI program change program number.
        /// </summary>
        public byte PC  { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the <see cref="TemporaryTone"/> with the given <see cref="IBankSelect"/> interface.
        /// </summary>
        /// <param name="bankselect"></param>
        /// <exception cref="IntegraException">When the type is <see cref="IntegraToneTypes.Unavailable"/>.</exception>
        /// <remarks><i>
        /// <b>Important</b><br/>
        /// The <see cref="StudioSetPart"/> has to be initialized before the <see cref="TemporaryTone"/>.<br/>
        /// The method is invoked when a <see cref="Integra.ToneChanged"/> event is received which is generated by the <see cref="StudioSetPart"/>.<br/>
        /// </i></remarks>
        private void InitializeTemporaryTone(IBankSelect bankselect)
        {
            // IMPORTANT! Quick tone changes can corrupt the model initialization queue, dequeue the temporary tone as prevention
            Device.Dequeue(this);

            SuperNATURALAcousticTone?.Dispose();
            SuperNATURALSynthTone?.Dispose();
            SuperNATURALDrumKit?.Dispose();
            PCMSynthTone?.Dispose();
            PCMDrumKit?.Dispose();
            MFX.Dispose();

            MSB        = bankselect.MSB;
            LSB        = bankselect.LSB;
            PC         = bankselect.PC;
            Type       = bankselect.ToneType();
            IsEditable = bankselect.IsEditable();

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
                    throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(InitializeTemporaryTone)}({Part})]\n" +
                                               $"Unspecified temporary tone type.");
            }

            Device.Enqueue(this);
        }

        #endregion

        #region Overrides: Model

        /// <summary>
        /// Handles the <see cref="Integra.SystemExclusiveReceived"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="Integra"/> that raised the event.</param>
        /// <param name="e">The event's associated data.</param>
        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e) { }

        /// <summary>
        /// Gets wheter the <see cref="TemporaryTone"/> is initialized.
        /// </summary>
        /// <remarks><i>
        /// The property is polled by the model initialization queue and raises the property changed event for all properties on completion.
        /// </i></remarks>
        public override bool IsInitialized
        {
            get
            {
                bool initialized = false;

                switch (Type)
                {
                    case IntegraToneTypes.SuperNATURALAcousticTone:
                        initialized = SuperNATURALAcousticTone?.IsInitialized ?? false;
                        break;

                    case IntegraToneTypes.SuperNATURALSynthTone:
                        initialized = SuperNATURALSynthTone?.IsInitialized ?? false;
                        break;

                    case IntegraToneTypes.SuperNATURALDrumkit:
                        initialized = SuperNATURALDrumKit?.IsInitialized ?? false;
                        break;

                    case IntegraToneTypes.PCMSynthTone:
                        initialized = PCMSynthTone?.IsInitialized ?? false;
                        break;

                    case IntegraToneTypes.PCMDrumkit:
                        initialized = PCMDrumKit?.IsInitialized ?? false;
                        break;
                }

                initialized = initialized && MFX.IsInitialized;

                if(initialized)
                {
                    NotifyPropertyChanged(string.Empty);
                }

                return initialized;
            }
        }

        /// <summary>
        /// Gets wheter the <see cref="TemporaryTone"/> as unsaved changes.
        /// </summary>
        public override bool IsDirty 
        { 
            get 
            {
                bool dirty = false;

                switch(Type)
                {
                    case IntegraToneTypes.SuperNATURALAcousticTone:
                        dirty = SuperNATURALSynthTone?.IsDirty ?? false;
                        break;

                    case IntegraToneTypes.SuperNATURALSynthTone:
                        dirty = SuperNATURALSynthTone?.IsDirty ?? false;
                        break;

                    case IntegraToneTypes.SuperNATURALDrumkit:
                        dirty = SuperNATURALDrumKit?.IsDirty ?? false;
                        break;

                    case IntegraToneTypes.PCMSynthTone:
                        dirty = PCMSynthTone?.IsDirty ?? false;
                        break;

                    case IntegraToneTypes.PCMDrumkit:
                        dirty = PCMDrumKit?.IsDirty ?? false;
                        break;
                }

                dirty = dirty || MFX.IsDirty;

                if (dirty)
                {
                    NotifyPropertyChanged(nameof(IsDirty));
                }

                return dirty;
            }

            protected set => base.IsDirty = value; 
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="data">Ignored.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">When invoked.</exception>
        internal override bool Initialize(byte[] ignored)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the <see cref="Integra.ToneChanged"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="Integra"/> that raised the event.</param>
        /// <param name="e">The event's associated data.</param>
        private void ToneChanged(object? sender, IntegraToneChangedEventArgs e)
        {
            if (e.Part == Part)
            {
                InitializeTemporaryTone(e.Tone);
            }
        }
        
        #endregion

        internal void Load(TemporaryToneFile file, IBankSelect location = null)
        {
            SuperNATURALAcousticTone?.Dispose();
            SuperNATURALSynthTone?.Dispose();
            SuperNATURALDrumKit?.Dispose();
            PCMSynthTone?.Dispose();
            PCMDrumKit?.Dispose();
            MFX.Dispose();


            // TODO: Select user tone first in case of different type

            Type = (IntegraToneTypes)file.ToneType;

            MFX = new MFX(this);

            switch (Type)
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

                    SuperNATURALSynthTone.Misc.Load(file.SuperNATURALSynthToneMisc);

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


            // TODO: Remove, testing purpose
            Store();
        }

        /// <summary>
        /// Creates a new <see cref="TemporaryToneFile"/> initialized with the <see cref="TemporaryTone"/>'s data. 
        /// </summary>
        /// <returns>A <see cref="TemporaryToneFile"/> containing the <see cref="TemporaryTone"/>'s data.</returns>
        /// <exception cref="IntegraException">When any of the required models is null, missing or not editable.</exception>
        internal TemporaryToneFile Save()
        {
            if(!Device.IsInitialized)
                throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n" +
                                           $"The {nameof(Integra)}] is uninitialized.");

            if(!IsEditable)
                throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n" +
                                           $"The {nameof(TemporaryTone)}] is not editable.");

            TemporaryToneFile file = new()
            {
                Header    = FileManager.TEMPORARY_TONE_FILE_HEADER,
                ToneType  = (uint)Type,
                Expansion = (byte)this.GetExpansion()
            };

            switch (Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:

                    if (SuperNATURALAcousticTone == null || SuperNATURALAcousticTone.IsInitialized == false)
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n" +
                                                   $"The {nameof(SuperNATURALAcousticTone)} is uninitialized.");

                    try
                    {
                        file.SuperNATURALAcousticToneCommon = SuperNATURALAcousticTone.Common.Serialize();
                    }
                    catch(Exception ex)
                    {
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]", ex);
                    }

                    break;

                case IntegraToneTypes.SuperNATURALSynthTone:

                    if (SuperNATURALSynthTone == null || SuperNATURALSynthTone.IsInitialized == false)
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n" +
                                                   $"The {nameof(SuperNATURALSynthTone)} is uninitialized.");

                    file.SuperNATURALSynthToneCommon = SuperNATURALSynthTone.Common.Serialize();

                    for (int i = 0; i < IntegraConstants.SNS_PARTIAL_COUNT; i++)
                    {
                        file.SuperNATURALSynthTonePartials[i] = SuperNATURALSynthTone.Partials[i].Serialize();
                    }

                    file.SuperNATURALSynthToneMisc = SuperNATURALSynthTone.Misc.Serialize();

                    break;

                case IntegraToneTypes.SuperNATURALDrumkit:

                    if (SuperNATURALDrumKit == null || SuperNATURALDrumKit.IsInitialized == false)
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n" +
                                                   $"The {nameof(SuperNATURALDrumKit)} is uninitialized.");

                    try
                    {
                        file.SuperNATURALDrumKitCommon = SuperNATURALDrumKit.Common.Serialize();
                        file.SuperNATURALDrumKitCommonCompEQ = SuperNATURALDrumKit.CompEQ.Serialize();

                        for (int i = 0; i < IntegraConstants.SND_NOTE_COUNT; i++)
                        {
                            file.SuperNATURALDrumKitNotes[i] = SuperNATURALDrumKit.Notes[i].Serialize();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]", ex);
                    }

                    break;

                case IntegraToneTypes.PCMSynthTone:

                    if (PCMSynthTone == null || PCMSynthTone.IsInitialized == false)
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n" +
                                                   $"The {nameof(PCMSynthTone)} is uninitialized.");

                    try
                    {
                        file.PCMSynthToneCommon = PCMSynthTone.Common.Serialize();
                        file.PMT = PCMSynthTone.PMT.Serialize();

                        for (int i = 0; i < IntegraConstants.PCM_PARTIAL_COUNT; i++)
                        {
                            file.PCMSynthTonePartials[i] = PCMSynthTone.Partials[i].Serialize();
                        }

                        file.PCMSynthToneCommon2 = PCMSynthTone.Common02.Serialize();
                    }
                    catch (Exception ex)
                    {
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]", ex);
                    }

                    break;

                case IntegraToneTypes.PCMDrumkit:

                    if (PCMDrumKit == null || PCMDrumKit.IsInitialized == false)
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n" + 
                                                   $"The {nameof(PCMDrumKit)} is uninitialized.");

                    try
                    {
                        file.PCMDrumKitCommon = PCMDrumKit.Common.Serialize();
                        file.PCMDrumKitCommonCompEQ = PCMDrumKit.CompEQ.Serialize();

                        for (int i = 0; i < IntegraConstants.PCM_NOTE_COUNT; i++)
                        {
                            file.PCMDrumKitNotes[i] = PCMDrumKit.Partials[i].Serialize();
                        }

                        file.PCMDrumKitCommon2 = PCMDrumKit.Common02.Serialize();
                    }
                    catch(Exception ex)
                    {
                        throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]", ex);
                    }

                    break;
            }

            if (MFX == null || MFX.IsInitialized == false)
                throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]\n" +
                                           $"The {nameof(TemporaryTone)}'s {nameof(MFX)} is uninitialized.");

            try
            {
                file.MFX = MFX.Serialize();
            }
            catch(Exception ex)
            {
                throw new IntegraException($"[{nameof(TemporaryTone)}.{nameof(Save)}()]", ex);
            }

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
