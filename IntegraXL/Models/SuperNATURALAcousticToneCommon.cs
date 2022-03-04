using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace IntegraXL.Models
{
    [Integra(0x00000000, 0x00000046)]
    public sealed class SuperNATURALAcousticToneCommon : IntegraModel<SuperNATURALAcousticToneCommon>, IParameterProvider<byte>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] byte[] _ToneName = new byte[12];
        [Offset(0x000C)] byte[] RESERVED01 = new byte[4];
        [Offset(0x0010)] byte _ToneLevel;
        [Offset(0x0011)] IntegraMonyPolySwitch _MonoPoly;
        [Offset(0x0012)] byte _PortamentTimeOffset;
        [Offset(0x0013)] byte _CutoffOffset;
        [Offset(0x0014)] byte _ResonanceOffset;
        [Offset(0x0015)] byte _AttackTimeOffset;
        [Offset(0x0016)] byte _ReleaseTimeOffset;
        [Offset(0x0017)] byte _VibratoRate;
        [Offset(0x0018)] byte _VibratoDepth;
        [Offset(0x0019)] byte _VibratoDelay;
        [Offset(0x001A)] byte _OctaveShift;
        [Offset(0x001B)] byte _Category;
        [Offset(0x001C)] byte[] _PhraseNumber = new byte[2];
        [Offset(0x001E)] byte _PhraseOctaveShift;
        [Offset(0x001F)] IntegraSwitch _TFXSwitch;
        [Offset(0x0020)] byte _InstVariation;
        [Offset(0x0021)] byte _InstNumber;
        [Offset(0x0022)] byte[] _ModifyParameter = new byte[32];

        [Offset(0x0042)] byte[] RESERVED02 = new byte[4];

        #endregion

        #region Constructor

        internal SuperNATURALAcousticToneCommon(SuperNATURALAcousticTone tone) : base(tone.Device) 
        {
            Address = tone.Address;
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

                    // Copy the string to the backing field byte array
                    Array.Copy(Encoding.ASCII.GetBytes(value), 0, _ToneName, 0, 12);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0010)]
        public byte ToneLevel
        {
            get { return _ToneLevel; }
            set
            {
                if (_ToneLevel != value)
                {
                    _ToneLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }
        [Offset(0x0011)]
        public IntegraMonyPolySwitch MonoPoly
        {
            get { return _MonoPoly; }
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
        public byte PortamentoTimeOffset
        {
            get { return _PortamentTimeOffset; }
            set
            {
                if (_PortamentTimeOffset != value)
                {
                    _PortamentTimeOffset = value;
                    NotifyPropertyChanged();
                }
            }
        }


        [Offset(0x0013)]
        public byte CutoffOffset
        {
            get { return _CutoffOffset; }
            set
            {
                if (_CutoffOffset != value)
                {
                    _CutoffOffset = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0014)]
        public byte ResonanceOffset
        {
            get { return _ResonanceOffset; }
            set
            {
                if (_ResonanceOffset != value)
                {
                    _ResonanceOffset = value;
                    NotifyPropertyChanged();
                }
                
            }
        }

        [Offset(0x0015)]
        public byte AttackTimeOffset
        {
            get { return _AttackTimeOffset; }
            set
            {
                if (_AttackTimeOffset != value)
                {
                    _AttackTimeOffset = value;
                    NotifyPropertyChanged();
                }
            }
        }
        [Offset(0x0016)]
        public byte ReleaseTimeOffset
        {
            get { return _ReleaseTimeOffset; }
            set
            {
                if (_ReleaseTimeOffset != value)
                {
                    _ReleaseTimeOffset = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0017)]
        public byte VibratoRate
        {
            get { return _VibratoRate; }
            set
            {
                if (_VibratoRate != value)
                {
                    _VibratoRate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0018)]
        public byte VibratoDepth
        {
            get { return _VibratoDepth; }
            set
            {
                if (_VibratoDepth != value)
                {
                    _VibratoDepth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0019)]
        public byte VibratoDelay
        {
            get { return _VibratoDelay; }
            set
            {
                if (_VibratoDelay != value)
                {
                    _VibratoDelay = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001A)]
        public byte OctaveShift
        {
            get { return _OctaveShift; }
            set
            {
                if (_OctaveShift != value)
                {
                    _OctaveShift = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001B)]
        public byte Category
        {
            get { return _Category; }
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // TODO:
        //[Offset(0x001C)]
        //public short PhraseNumber
        //{
        //    get { return _PhraseNumber.DeserializeShort(); }
        //    set
        //    {
        //        if (_PhraseNumber.DeserializeShort() != value)
        //        {
        //            _PhraseNumber = value.SerializeShort();
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        [Offset(0x001E)]
        public byte PhraseOctaveShift
        {
            get { return _PhraseOctaveShift; }
            set
            {
                if (_PhraseOctaveShift != value)
                {
                    _PhraseOctaveShift = value;
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
                    ReinitializeAsync();
                }
            }
        }

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

        private void SetParameterProvider()
        {
            // TODO: Get Type 
            //switch (Type)
            //{
            //    case IntegraReverbTypes.Room1:
            //    case IntegraReverbTypes.Room2:
            //    case IntegraReverbTypes.Hall1:
            //    case IntegraReverbTypes.Hall2:
            //    case IntegraReverbTypes.Plate:
            //        Parameter = new CommonReverb(this);
            //        break;

            //    case IntegraReverbTypes.GM2:
            //        Parameter = new CommonReverbGM2(this);
            //        break;

            //    default:
            //        Parameter = new CommonReverbOff(this);
            //        break;
            //}

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

        internal override bool Initialize(byte[] data)
        {
            IsInitialized = false;
            //Debug.Assert(data[33] < 0);
            base.Initialize(data);

            Parameters = this.GetParameterType();
            NotifyPropertyChanged(string.Empty);

            Debug.Print($"[{GetType().Name}] Parameter Type: {Parameters.GetType().Name}");

            return IsInitialized = true;
        }

        #endregion
    }
}
