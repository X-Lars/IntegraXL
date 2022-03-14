using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using System.ComponentModel;
using System.Text;

namespace IntegraXL.Models
{
    [Integra(0x00000000, 0x00000046)]
    public sealed class SuperNATURALAcousticToneCommon : IntegraModel<SuperNATURALAcousticToneCommon>, IParameterProvider<byte>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private byte[] _ToneName = new byte[12];
        [Offset(0x000C)] private byte[] RESERVED01 = new byte[4];
        [Offset(0x0010)] private byte _ToneLevel;
        [Offset(0x0011)] private IntegraMonyPolySwitch _MonoPoly;
        [Offset(0x0012)] private byte _PortamentoTimeOffset;
        [Offset(0x0013)] private byte _CutoffOffset;
        [Offset(0x0014)] private byte _ResonanceOffset;
        [Offset(0x0015)] private byte _AttackTimeOffset;
        [Offset(0x0016)] private byte _ReleaseTimeOffset;
        [Offset(0x0017)] private byte _VibratoRate;
        [Offset(0x0018)] private byte _VibratoDepth;
        [Offset(0x0019)] private byte _VibratoDelay;
        [Offset(0x001A)] private byte _OctaveShift;
        [Offset(0x001B)] private IntegraTemporaryToneCategories _Category;
        [Offset(0x001C)] private IntegraSNAPhrase _PhraseNumber;
        [Offset(0x001E)] private byte _PhraseOctaveShift;
        [Offset(0x001F)] private IntegraSwitch _TFXSwitch;
        [Offset(0x0020)] private byte _InstVariation;
        [Offset(0x0021)] private byte _InstNumber;
        [Offset(0x0022)] private byte[] _ModifyParameter = new byte[32];
        [Offset(0x0042)] private byte[] RESERVED02 = new byte[4];

        #endregion

        #region Constructor

        internal SuperNATURALAcousticToneCommon(SuperNATURALAcousticTone tone) : base(tone.Device) 
        {
            Address = tone.Address;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the selected instrument.
        /// </summary>
        public IntegraSNAInstruments Instrument
        {
            get => this.GetInstrument();
            set
            {
                // TODO: Skip selection of expansions that are not loaded
                if((int)value >= 77 && (int)value <= 86)
                {
                    // ExSN01
                }
                else if((int)value >= 87 && (int)value <= 101)
                {
                    // ExSN02
                }
                else if ((int)value >= 102 && (int)value <= 109)
                {
                    // ExSN03
                }
                else if ((int)value >= 110 && (int)value <= 115)
                {
                    // ExSN04
                }
                else if ((int)value >= 116 && (int)value <= 126)
                {
                    // ExSN05
                }

                if (Instrument != value)
                {
                    this.SetInstrument(value);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public string ToneName
        {
            get { return Encoding.ASCII.GetString(_ToneName, 0, 12); }
            set
            {
                if (ToneName != value)
                {
                    if (string.IsNullOrEmpty(value))
                        return;

                    _ToneName = Encoding.ASCII.GetBytes(value.FixedLength(_ToneName.Length));

                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0010)]
        public byte ToneLevel
        {
            get => _ToneLevel;
            set
            {
                if (_ToneLevel != value)
                {
                    _ToneLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }
        [Offset(0x0011)]
        public IntegraMonyPolySwitch MonoPoly
        {
            get => _MonoPoly;
            set
            {
                if (_MonoPoly != value)
                {
                    _MonoPoly = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0012)]
        public int PortamentoTimeOffset
        {
            get => _PortamentoTimeOffset.Deserialize(64);
            set
            {
                if (PortamentoTimeOffset != value)
                {
                    _PortamentoTimeOffset = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }


        [Offset(0x0013)]
        public int CutoffOffset
        {
            get => _CutoffOffset.Deserialize(64);
            set
            {
                if (CutoffOffset != value)
                {
                    _CutoffOffset = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0014)]
        public int ResonanceOffset
        {
            get => _ResonanceOffset.Deserialize(64);
            set
            {
                if (ResonanceOffset != value)
                {
                    _ResonanceOffset = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
                
            }
        }

        [Offset(0x0015)]
        public int AttackTimeOffset
        {
            get => _AttackTimeOffset.Deserialize(64);
            set
            {
                if (AttackTimeOffset != value)
                {
                    _AttackTimeOffset = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }
        [Offset(0x0016)]
        public int ReleaseTimeOffset
        {
            get => _ReleaseTimeOffset.Deserialize(64);
            set
            {
                if (ReleaseTimeOffset != value)
                {
                    _ReleaseTimeOffset = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0017)]
        public int VibratoRate
        {
            get => _VibratoRate.Deserialize(64);
            set
            {
                if (VibratoRate != value)
                {
                    _VibratoRate = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0018)]
        public int VibratoDepth
        {
            get => _VibratoDepth.Deserialize(64);
            set
            {
                if (VibratoDepth != value)
                {
                    _VibratoDepth = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0019)]
        public int VibratoDelay
        {
            get => _VibratoDelay.Deserialize(64);
            set
            {
                if (VibratoDelay != value)
                {
                    _VibratoDelay = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001A)]
        public int OctaveShift
        {
            get => _OctaveShift.Deserialize(64);
            set
            {
                if (OctaveShift != value)
                {
                    _OctaveShift = value.Serialize(64).Clamp(61, 67);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001B)]
        public IntegraTemporaryToneCategories Category
        {
            get => _Category;
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001C)]
        public IntegraSNAPhrase PhraseNumber
        {
            get => _PhraseNumber;
            set
            {
                if (PhraseNumber != value)
                {
                    _PhraseNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001E)]
        public int PhraseOctaveShift
        {
            get => _PhraseOctaveShift.Deserialize(64);
            set
            {
                if (PhraseOctaveShift != value)
                {
                    _PhraseOctaveShift = value.Serialize(64).Clamp(61, 67);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001F)]
        public IntegraSwitch TFXSwitch
        {
            get { return _TFXSwitch; }
            set
            {
                if (_TFXSwitch != value)
                {
                    _TFXSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

       
        [Offset(0x0020)]
        public byte InstVariation
        {
            get { return _InstVariation; }
            set
            {
                if (_InstVariation != value)
                {
                    _InstVariation = value;
                    NotifyPropertyChanged();

                    InitializeParameters();
                }
            }
        }

        [Offset(0x0021)]
        public byte InstNumber
        {
            get { return _InstNumber; }
            set
            {
                if (_InstNumber != value)
                {
                    _InstNumber = value;
                    NotifyPropertyChanged();

                    InitializeParameters();
                }
            }
        }

        // TODO: Make set internal to ensure the parameters property used is and the parameters are validated?
        [Offset(0x0022)]
        public byte this[int index]
        {
            get { return _ModifyParameter[index]; }
            set
            {
                if (_ModifyParameter[index] != value)
                {
                    _ModifyParameter[index] = value;
                    NotifyPropertyChanged("Item", index);
                }
            }
        }

        #endregion

        #region Interfaces: IParameterProvider

        /// <summary>
        /// Event raised when the instrument type is changed.
        /// </summary>
        public event EventHandler<IntegraTypeChangedEventArgs>? TypeChanged;

        /// <summary>
        /// Gets the tone parameters.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public IntegraParameterMapper<byte>? Parameters { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the <see cref="Parameters"/> property.
        /// </summary>
        private void InitializeParameters()
        {
            IntegraParameterMapper<byte> parameters = this.NewGetParameterType();

            if (Parameters != parameters)
            {
                Parameters = this.NewGetParameterType();
                TypeChanged?.Invoke(this, new IntegraTypeChangedEventArgs(Parameters.GetType()));
                NotifyPropertyChanged(string.Empty);
            }
        }

        // TODO: No loop / remove completely
        private void ReceivedParameter(IntegraSystemExclusive e)
        {
            
            int index = (e.Address - Address - 0x00000022);

            for (int i = 0; i < e.Data.Length; i++)
            {
                _ModifyParameter[index] = e.Data[i];
            }

            NotifyPropertyChanged(string.Empty);
        }

       

        #endregion

        #region Overrides: Model

        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            //base.SystemExclusiveReceived(sender, e);
            if (e.SystemExclusive.Address == Address)
            {
                //Debug.Assert(Address != 0x1c220000);
                if (e.SystemExclusive.Data.Length == Size)
                    // Always initialize, the the first property offset = 0 and determines the type
                    Initialize(e.SystemExclusive.Data);
            }
            else if (e.SystemExclusive.Address.InRange(Address, Address + Size))
            {
                if (e.SystemExclusive.Address - Address >= 0x00000022)
                {
                    ReceivedParameter(e.SystemExclusive);
                }
                else
                {
                    ReceivedProperty(e.SystemExclusive);
                }
            }
        }

        
        /// <summary>
        /// Gets wheter the model is initialized.
        /// </summary>
        public override bool IsInitialized
        {
            get => base.IsInitialized;
            protected internal set
            {
                base.IsInitialized = value;

                if(value == true)
                {
                    InitializeParameters();
                }
            }
        }

        #endregion
    }
}
