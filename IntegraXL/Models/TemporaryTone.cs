using IntegraXL.Core;
using System.Diagnostics;
using System.Reflection;

namespace IntegraXL.Models
{
    /// <summary>
    /// Defines a collection of <see cref="TemporaryTone"/>s for all parts.
    /// </summary>
    [Integra(0x19000000, 0x04000000)]
    public sealed class TemporaryTones : IntegraPartialCollection<TemporaryTone>
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="TemporaryTones"/> collection instance.
        /// </summary>
        /// <param name="device">The device to connect the collection.</param>
#pragma warning disable IDE0051 // Remove unused private members
        private TemporaryTones(Integra device) : base(device) { }
#pragma warning restore IDE0051 // Remove unused private members

        #endregion
    }

    /// <summary>
    /// Defines the INTEGRA-7 temporary tone model.
    /// </summary>
    [Integra(0x19000000, 0x00200000)]
    public class TemporaryTone : IntegraPartial<TemporaryTone>
    {
        /// <summary>
        /// Stores a reference to the associated tone.
        /// </summary>
        private IntegraTone _Tone;

        /// <summary>
        /// Creates a new <see cref="TemporaryTone"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the model.</param>
        /// <param name="part">The associated part.</param>
        internal TemporaryTone(Integra device, Parts part) : base(device, part) 
        {
            IntegraAttribute? attribute = GetType().GetCustomAttribute<IntegraAttribute>();

            Debug.Assert(attribute != null);

            Address = attribute.Address;
            
            // 0x19, 0x19, 0x19, 0x19, 0x20, ...
            Address[0] += (byte)((int)part >> 2); // >> 2 equals division by 4

            // 0x00, 0x20, 0x40, 0x60, 0x00, ...
            Address[1] += (byte)((int)part % 4 * 0x20);

            MFX = new MFX(this);
            _Tone = device.CreateModel<IntegraTone>(Part);

            // TODO: Remove from constructor
            InitializeToneAsync();

        }

        #region Properties

        /// <summary>
        /// Gets the type of temporary tone.
        /// </summary>
        public IntegraToneTypes Type { get; private set; }

        #endregion

        #region Properties: INTEGRA-7

        /// <summary>
        /// Gets the SuperNATURAL acoustic tone model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT:</b><br/>
        /// <i>Can return <see langword="null"/>, check the <see cref="Type"/> property first.</i>
        /// </remarks>
        public SuperNATURALAcousticTone? SuperNATURALAcousticTone { get; private set; }

        /// <summary>
        /// Gets the SuperNATURAL Synth tone model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT:</b><br/>
        /// <i>Can return <see langword="null"/>, check the <see cref="Type"/> property first.</i>
        /// </remarks>
        public SuperNATURALSynthTone? SuperNATURALSynthTone { get; private set; }

        /// <summary>
        /// Gets the SuperNATURAL drum kit model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT:</b><br/>
        /// <i>Can return <see langword="null"/>, check the <see cref="Type"/> property first.</i>
        /// </remarks>
        public SuperNATURALDrumKit? SuperNATURALDrumKit { get; private set; }

        /// <summary>
        /// Gets the PCM synth tone model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT:</b><br/>
        /// <i>Can return <see langword="null"/>, check the <see cref="Type"/> property first.</i>
        /// </remarks>
        public PCMSynthTone? PCMSynthTone { get; private set; }

        /// <summary>
        /// Gets the PCM drum kit model.
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT:</b><br/>
        /// <i>Can return <see langword="null"/>, check the <see cref="Type"/> property first.</i>
        /// </remarks>
        public PCMDrumKit? PCMDrumKit { get; private set; }

        /// <summary>
        /// Gets the tone MFX model.
        /// </summary>
        public MFX MFX { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the temporary tone type and binds the tone changed event listener.
        /// </summary>
        private async void InitializeToneAsync()
        {
            // Prevents duplicate event listeners although the method should be called only once
            _Tone.Changed -= ToneChanged;

            if (!_Tone.IsInitialized)
                await Device.InitializeModel(_Tone);

            _Tone.Changed += ToneChanged;

            Type = _Tone.Type;

            //Debug.Assert(Part != Parts.Part15);
            switch (Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:
                    SuperNATURALAcousticTone = new SuperNATURALAcousticTone(this);
                    MFX.Address |= SuperNATURALAcousticTone.Address;
                    Debug.Print($"{nameof(TemporaryTone)}] {nameof(InitializeToneAsync)} {Part}: {SuperNATURALAcousticTone.Address}");
                    break;
                case IntegraToneTypes.SuperNATURALSynthTone:
                    SuperNATURALSynthTone = new SuperNATURALSynthTone(this);
                    MFX.Address |= SuperNATURALSynthTone.Address;
                    Debug.Print($"{nameof(TemporaryTone)}] {nameof(InitializeToneAsync)} {Part}: {SuperNATURALSynthTone.Address}");
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
            }

            Debug.Print($"{nameof(TemporaryTone)}] {nameof(InitializeToneAsync)} MFX: {MFX.Address}");
        }

        #endregion

        #region Overrides: Model

        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            //base.SystemExclusiveReceived(sender, e);
        }
        /// <summary>
        /// Gets wheter the temorary tone is initialized.
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
                }

                return false;
            }

            protected internal set => base.IsInitialized = value;
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

            // Old: Remove type from device cache
            // Old: Disconnect
            // Old: Remove MFX ?
            // User message no longer valid?
            // New: Set type
            // New: Reinit
            // New: New / Reinit MFX
        }

        protected override bool Initialize(byte[] data)
        {
            //throw new NotImplementedException();

            return false;
        }

        #endregion
    }
}
