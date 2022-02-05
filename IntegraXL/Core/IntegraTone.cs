using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using IntegraXL.Models;
using IntegraXL.Templates;
using System.Diagnostics;
using System.Reflection;

namespace IntegraXL.Core
{
    /// <summary>
    /// Collection of all selected INTEGRA-7 tones.
    /// </summary>
    [Integra(0x18002006, 0x00000003, 16)]
    public sealed class IntegraTones : IntegraPartialCollection<IntegraTone> 
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraTones"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the collection.</param>
        internal IntegraTones(Integra device) : base(device) { }

        #endregion

        #region Overrides: Model

        /// <summary>
        /// Initializes the <see cref="IntegraTones"/> collection.
        /// </summary>
        /// <returns>An awaitable task that returns always true.</returns>
        internal override async Task<bool> InitializeAsync()
        {
            for (int i = 0; i < Count; i++)
            {
                 await this[i].InitializeAsync();
            }

            return IsInitialized = true;
        }

        /// <summary>
        /// Gets the unique identifier for the collection.
        /// </summary>
        /// <returns>A unique identifier for the collection.</returns>
        protected internal override int GetUID()
        {
            return (int)(0xFF0000FF | (Address | 0x00FFFF00));
        }

        #endregion
    }

    /// <summary>
    /// Defines a model to receive, set and delegate tone selection.
    /// </summary>
    /// <remarks><i>The model covers the <see cref="StudioSetPart.Tone"/> property.</i></remarks>
    [Integra(0x18002006, 0x00000003)]
    public sealed class IntegraTone : IntegraPartial<IntegraTone>, IBankSelect
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)]
        private byte[] _BankSelect = new byte[3];

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the tone selection is changed.
        /// </summary>
        public event EventHandler<IntegraToneChangedEventArgs>? Changed;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraTone"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the model.</param>
        /// <param name="part">The model's associated part.</param>
#pragma warning disable IDE0051 // Remove unused private members
        private IntegraTone(Integra device, Parts part) : base(device, part) { }
#pragma warning restore IDE0051 // Remove unused private members

        #endregion

        #region Properties

        /// <summary>
        /// Gets the ID of the tone.
        /// </summary>
        /// <remarks><i>For display purpose only, do not use for calculations.</i></remarks>
        public int ID { get; private set; }

        /// <summary>
        /// Gets the tone name.
        /// </summary>
        public override string Name { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets the tone category.
        /// </summary>
        public IntegraToneCategories Category { get; private set; }

        /// <summary>
        /// Gets the tone bank.
        /// </summary>
        public IntegraToneBanks ToneBank { get; private set; }

        /// <summary>
        /// Gets the tone type.
        /// </summary>
        public IntegraToneTypes Type { get; private set; } 

        /// <summary>
        /// Gets wheter the tone is editable.
        /// </summary>
        public bool IsEditable { get; private set; }

        /// <summary>
        /// Gets the tone variation.
        /// </summary>
        /// <remarks><i>Only for GM2 tone bank and drum kit.</i></remarks>
        public int? Variation { get; private set; }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public IBankSelect BankSelect
        {
            get => this;
            set
            {
                if (value != null & !this.Equals(value))
                {
                    Initialize(new byte[] { value.MSB, value.LSB, value.PC });
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Requests the tone template.
        /// </summary>
        private void RequestToneTemplate()
        {
            // TODO: check if expansion is loaded
            Device.TransmitSystemExclusive(new IntegraSystemExclusive(0x0F000402, new IntegraRequest(MSB, LSB, PC, 0x01)));
        }

        #endregion

        #region Overrides: Model

        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            if (e.SystemExclusive.Address == Address)
            {
                if (e.SystemExclusive.Data.Length == Size)
                {
                    Initialize(e.SystemExclusive.Data);
                }
            }
            else if (e.SystemExclusive.Address == 0x0F000402)
            {
                // Tone template request
                if(e.SystemExclusive.Data.Length == 0x00000015)
                {
                    if(e.SystemExclusive.Data[0] == MSB && e.SystemExclusive.Data[1] == LSB && e.SystemExclusive.Data[2] == PC)
                    {
                        int id = PC + 1;

                        switch(ToneBank)
                        {
                            case IntegraToneBanks.GM2Tone:
                                Variation = LSB;
                                break;

                            case IntegraToneBanks.GM2Drum:
                                id = 1;
                                Variation = PC;
                                break;

                            default:
                                id += ((LSB - (int)ToneBank & 0x00FF) % 64 * 128);
                                Variation = null;
                                break;
                        }

                        ToneTemplate? info = Activator.CreateInstance(typeof(ToneTemplate), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { id, e.SystemExclusive.Data }, null) as ToneTemplate;

                        Debug.Assert(info != null);

                        ID       = id;
                        Name     = info.Name;
                        Category = info.Category;

                        NotifyPropertyChanged(string.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the model with data.
        /// </summary>
        /// <param name="data">The data to initialize the model.</param>
        /// <returns>Always true.</returns>
        protected override bool Initialize(byte[] data)
        {
            _BankSelect[0] = data[0];
            _BankSelect[1] = data[1];
            _BankSelect[2] = data[2];

            IsEditable = this.IsEditable(); // REQUIRED: IsEditable is required by the temporary tone
            Type       = this.ToneType();   // REQUIRED: Type is required by the temporary tone
            ToneBank   = this.ToneBank();   // REQUIRED: Required to determine the tone ID

            // Required functional data is initialized
            Changed?.Invoke(this, new IntegraToneChangedEventArgs(this, Part));

            // Request optional data
            RequestToneTemplate();

            return IsInitialized = true;
        }

        #endregion

        #region Overrides: Object

        /// <summary>
        /// Provides string that represents the current tone.
        /// </summary>
        /// <returns>A string that represents the current tone.</returns>
        public override string ToString()
        {
            return $"{ID:0000} {Name,-15} [{Category}] (0x{MSB:X2} 0x{LSB:X2} 0x{PC:X2})";
        }

        #endregion

        #region Interface: IBankSelect

        /// <summary>
        /// Gets the (M)ost (S)ignificant (B)yte to select the tone.
        /// </summary>
        public byte MSB { get { return _BankSelect[0]; } }

        /// <summary>
        /// Gets the (L)east (S)ignificant (B)yte to select the tone.
        /// </summary>
        public byte LSB { get { return _BankSelect[1]; } }

        /// <summary>
        /// Gets the (P)rogram (C)hange byte to select the tone.
        /// </summary>
        public byte PC { get { return _BankSelect[2]; } }

        /// <summary>
        /// Gets whether the current <see cref="IBankSelect"/> interface data equals the provided <see cref="IBankSelect"/> interface data.
        /// </summary>
        /// <param name="bankSelect">The interface to compare.</param>
        /// <returns>True if both <see cref="IBankSelect"/> interfaces have equal data.</returns>
        public bool Equals(IBankSelect? bankSelect)
        {
            if (bankSelect is null)
                return false;

            return MSB == bankSelect.MSB && LSB == bankSelect.LSB && PC == bankSelect.PC;
        }

        #endregion

    }
}
