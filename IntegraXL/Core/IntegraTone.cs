using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using IntegraXL.Models;
using IntegraXL.Templates;
using System.Diagnostics;
using System.Reflection;

namespace IntegraXL.Core
{
    [Integra(0x18002006, 0x00000003, 16)]
    public sealed class IntegraTones : IntegraPartialCollection<IntegraTone> 
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        internal IntegraTones(Integra device) : base(device) { }

        #endregion

        #region Overrides

        internal override async Task<bool> Initialize()
        {
            for (int i = 0; i < Count; i++)
            {
                 await this[i].Initialize();
            }

            return IsInitialized = true;
        }

        protected internal override int GetModelHash()
        {
            return (int)(0xFF0000FF | (Address | 0x00FFFF00));
        }

        #endregion
    }

    /// <summary>
    /// Defines a model to receive, set and delegate tone selection.
    /// </summary>
    /// <remarks><i>The model covers the <see cref="StudioSetPart.ToneBankSelectMSB"/>, <see cref="StudioSetPart.ToneBankSelectLSB"/> and <see cref="StudioSetPart.ToneProgramNumber"/> properties.</i></remarks>
    [Integra(0x18002006, 0x00000003)]
    public sealed class IntegraTone : IntegraPartial<IntegraTone>, IBankSelect
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)]
        private byte[] _data = new byte[3];

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
        private IntegraTone(Integra device, Parts part) : base(device, part) { }

        #endregion

        #region Properties
        
        public int ID { get; private set; }
        public new string? Name { get; private set; }
        public IntegraToneCategories Category { get; private set; }
        public IntegraToneBanks ToneBank { get; private set; }
        public IntegraToneTypes Type { get; private set; } 
        public bool IsEditable { get; private set; }

        [Offset(0x0000)]
        public IBankSelect BankSelect
        {
            get => this;
            set
            {
                if(value != null & !this.Equals(value))
                {
                    Initialize(new byte[] { value.MSB, value.LSB, value.PC });
                    NotifyPropertyChanged();
                }
            }
        }
        #region Properties: INTEGRA-7

        //[Offset(0x0000)]
        public byte MSB { get { return _data[0]; } }

        //[Offset(0x0001)]
        public byte LSB { get { return _data[1]; } }

        //[Offset(0x0002)]
        public byte PC { get { return _data[2]; } }

        #endregion

        #endregion

        #region Overrides

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
                        // TODO: Check ID for single request tone banks?
                        int id = ((LSB % 64) * 128) + PC + 1;
                        ToneTemplate? info = Activator.CreateInstance(typeof(ToneTemplate), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { id, e.SystemExclusive.Data }, null) as ToneTemplate;

                        Debug.Assert(info != null);
                        ID = id;
                        Name = info.Name;
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
            _data[0] = data[0];
            _data[1] = data[1];
            _data[2] = data[2];

            // Get IBankSelect dependent properties
            Type     = this.ToneType(); // REQUIRED: Type is required by the temporary tone
            ToneBank = this.ToneBank();

            // Required data is initialized
            Changed?.Invoke(this, new IntegraToneChangedEventArgs(this, Part));

            //NotifyPropertyChanged(string.Empty);
            // Optional data is async
            RequestToneTemplate();

            return IsInitialized = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generates a request to the device to get the tone template.
        /// </summary>
        private void RequestToneTemplate()
        {
            Debug.Print($"[{nameof(IntegraTone)}({Part})] SX");
            // TODO: check if expansion is loaded
            Device.TransmitSystemExclusive(new IntegraSystemExclusive(0x0F000402, new IntegraRequest(MSB, LSB, PC, 0x01)));
        }

        #endregion

        #region Interfaces: IBankSelect

        /// <summary>
        /// Gets whether this <see cref="IBankSelect"/> interface equals the provided <see cref="IBankSelect"/> interface.
        /// </summary>
        /// <param name="compare">The <see cref="IBankSelect"/> interface to compare.</param>
        /// <returns>True if both <see cref="IBankSelect"/> interfaces have equal property values.</returns>
        public bool Equals(IBankSelect? bankSelect)
        {
            if (bankSelect == null)
                return false;

            return MSB == bankSelect.MSB && LSB == bankSelect.LSB && PC == bankSelect.PC;
        }

        /// <summary>
        /// Gets whether this <see cref="IBankSelect.MSB"/>, <see cref="IBankSelect.LSB"/> and <see cref="IBankSelect.PC"/> equal the <paramref name="bytes"/> array.
        /// </summary>
        /// <param name="compare">The <see cref="byte"/>[] array to compare.</param>
        /// <returns>True if this <see cref="IBankSelect"/> interface property values equal the <paramref name="bytes"/> array in <i>respective</i> order.</returns>
        public bool Equals(byte[]? bytes)
        {
            if (bytes == null || bytes.Length != 3)
                return false;

            return MSB == bytes[0] && LSB == bytes[1] && PC == bytes[3];
        }

        #endregion

        /// <summary>
        /// Provides a user friendly <see cref="string"/> representation of the model.
        /// </summary>
        /// <returns>A user friendly <see cref="string"/> representation of the model.</returns>
        public override string ToString()
        {
            return $"{ID:0000} {Name, -15} [{Category}] (0x{MSB:X2} 0x{LSB:X2} 0x{PC:X2})";
        }
    }
}
