using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using System.ComponentModel;
using System.Diagnostics;

namespace IntegraXL.Models
{
    /// <summary>
    /// Collection of all INTEGRA-7 studio set part models.
    /// </summary>
    [Integra(0x18002000, 0x00001000)]
    public sealed class StudioSetParts : IntegraPartialCollection<StudioSetPart>
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="StudioSetParts"/> collection instance.
        /// </summary>
        /// <param name="device">The device to connect the collection.</param>
#pragma warning disable IDE0051 // Remove unused private members
        private StudioSetParts(Integra device) : base(device) { }
#pragma warning restore IDE0051 // Remove unused private members

        #endregion
    }

    /// <summary>
    /// Defines the INTEGRA-7 studio set part model.
    /// </summary>
    [Integra(0x18002000, 0x0000004D)]
    public sealed class StudioSetPart : IntegraPartial<StudioSetPart>, IBankSelect
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private IntegraChannels _ReceiveChannel;
        [Offset(0x0001)] private IntegraSwitch _ReceiveSwitch;

        [Offset(0x0002)] private byte[] _Reserved1 = new byte[4];
        // Combines the MSB, LSB & PC into one field
        [Offset(0x0006)] private byte[] _BankSelect = new byte[3];

        //[Offset(0x0006)] private byte _ToneBankSelectMSB;
        //[Offset(0x0007)] private byte _ToneBankSelectLSB;
        //[Offset(0x0008)] private byte _ToneProgramNumber;

        [Offset(0x0009)] private byte _PartLevel;
        [Offset(0x000A)] private byte _Pan;
        [Offset(0x000B)] private byte _CoarseTune;
        [Offset(0x000C)] private byte _FineTune;
        [Offset(0x000D)] private IntegraMonyPolySwitch _MonoPolySwitch;
        [Offset(0x000E)] private IntegraToneSwitch _LegatoSwitch;
        [Offset(0x000F)] private byte _PitchBendRange;
        [Offset(0x0010)] private IntegraToneSwitch _PortamentoSwitch;
        [Offset(0x0011)] private byte[] _PortamentoTime = new byte[2];

        [Offset(0x0013)] private byte _CutoffOffset;
        [Offset(0x0014)] private byte _ResonanceOffset;
        [Offset(0x0015)] private byte _AttackTimeOffset;
        [Offset(0x0016)] private byte _DecayTimeOffset;
        [Offset(0x0017)] private byte _ReleaseTimeOffset;
        [Offset(0x0018)] private byte _VibratoRate;
        [Offset(0x0019)] private byte _VibratoDepth;
        [Offset(0x001A)] private byte _VibratoDelay;
        [Offset(0x001B)] private byte _OctaveShift;
        [Offset(0x001C)] private byte _VelocitySensOffset;
        [Offset(0x001D)] private byte _KeyboardRangeLower;
        [Offset(0x001E)] private byte _KeyboardRangeUpper;
        [Offset(0x001F)] private byte _KeyboardFadeWidthLower;
        [Offset(0x0020)] private byte _KeyboardFadeWidthUpper;
        [Offset(0x0021)] private byte _VelocityRangeLower;
        [Offset(0x0022)] private byte _VelocityRangeUpper;
        [Offset(0x0023)] private byte _VelocityFadeWidthLower;
        [Offset(0x0024)] private byte _VelocityFadeWidthUpper;
        [Offset(0x0025)] private IntegraMuteSwitch _MuteSwitch;

        [Offset(0x0026)] private byte _Reserved2;

        [Offset(0x0027)] private byte _ChorusSendLevel;
        [Offset(0x0028)] private byte _ReverbSendLevel;
        [Offset(0x0029)] private IntegraOutputAssigns _OutputAssign;

        [Offset(0x002A)] private byte _Reserved3;

        [Offset(0x002B)] private IntegraScaleTuneTypes _ScaleTuneType;
        [Offset(0x002C)] private IntegraScaleTuneKeys _ScaleTuneKey;
        [Offset(0x002D)] private byte _ScaleTuneC;
        [Offset(0x002E)] private byte _ScaleTuneCSharp;
        [Offset(0x002F)] private byte _ScaleTuneD;
        [Offset(0x0030)] private byte _ScaleTuneDSharp;
        [Offset(0x0031)] private byte _ScaleTuneE;
        [Offset(0x0032)] private byte _ScaleTuneF;
        [Offset(0x0033)] private byte _ScaleTuneFSharp;
        [Offset(0x0034)] private byte _ScaleTuneG;
        [Offset(0x0035)] private byte _ScaleTuneGSharp;
        [Offset(0x0036)] private byte _ScaleTuneA;
        [Offset(0x0037)] private byte _ScaleTuneASharp;
        [Offset(0x0038)] private byte _ScaleTuneB;

        [Offset(0x0039)] private IntegraSwitch _ReceiveProgramChange;
        [Offset(0x003A)] private IntegraSwitch _ReceiveBankSelect;
        [Offset(0x003B)] private IntegraSwitch _ReceivePitchBend;
        [Offset(0x003C)] private IntegraSwitch _ReceivePolyPhonicKeyPressure;
        [Offset(0x003D)] private IntegraSwitch _ReceiveChannelPressure;
        [Offset(0x003E)] private IntegraSwitch _ReceiveModulation;
        [Offset(0x003F)] private IntegraSwitch _ReceiveVolume;
        [Offset(0x0040)] private IntegraSwitch _ReceivePan;
        [Offset(0x0041)] private IntegraSwitch _ReceiveExpression;
        [Offset(0x0042)] private IntegraSwitch _ReceiveHold;

        [Offset(0x0043)] private IntegraVelocityCurveTypes _VelocityCurveType;

        [Offset(0x0044)] private byte _MotionalSurroundLR;

        [Offset(0x0045)] private byte _Reserved4;

        [Offset(0x0046)] private byte _MotionalSurroundFB;
        [Offset(0x0047)] private byte _Reserved5;
        [Offset(0x0048)] private byte _MotionalSurroundWidth;
        [Offset(0x0049)] private byte _MotionalSurroundAmbienceSendLevel;

        [Offset(0x004A)] private byte[] _Reserved6 = new byte[3];

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="StudioSetPart"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the model.</param>
        /// <param name="part">The model's associated part.</param>
        private StudioSetPart(Integra device, Parts part) : base(device, part) 
        {
            //_Tone = device.CreateChildModel<Tone>(part);

            PropertyChanged += StudioSetPartPropertyChanged;
        }

        private void StudioSetPartPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BankSelect))
            {
                Device.NotifyToneChanged(this, Part);
            }
        }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public IntegraChannels ReceiveChannel
        {
            get { return _ReceiveChannel; }
            set
            {
                if (_ReceiveChannel != value)
                {
                    _ReceiveChannel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0001)]
        public IntegraSwitch ReceiveSwitch
        {
            get { return _ReceiveSwitch; }
            set
            {
                if (_ReceiveSwitch != value)
                {
                    _ReceiveSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public IBankSelect BankSelect
        {
            get => this;
            set
            {
                if (!Equals(value))
                {
                    _BankSelect[0] = value.MSB.Clamp();
                    _BankSelect[1] = value.LSB.Clamp();
                    _BankSelect[2] = value.PC.Clamp();

                    NotifyPropertyChanged();

                    //_Tone.Update(value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks><i>The bank select properties are combined in the <see cref="BankSelect"/> property.</i></remarks>
        //[Offset(0x0006)]
        [Bindable(BindableSupport.No)]
        public byte MSB
        {
            get => _BankSelect[0];
            set => throw new NotImplementedException($"[{nameof(StudioSetPart)}({Part}).{nameof(MSB)}]\nUse the {nameof(BankSelect)}.{nameof(BankSelect.MSB)} property instead.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks><i>The bank select properties are combined in the <see cref="BankSelect"/> property.</i></remarks>
        //[Offset(0x0007)]
        [Bindable(BindableSupport.No)]
        public byte LSB
        {
            get => _BankSelect[1];
            set => throw new NotImplementedException($"[{nameof(StudioSetPart)}({Part}).{nameof(LSB)}]\nUse the {nameof(BankSelect)}.{nameof(BankSelect.LSB)} property instead.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks><i>The bank select properties are combined in the <see cref="BankSelect"/> property.</i></remarks>
        //[Offset(0x0008)]
        [Bindable(BindableSupport.No)]
        public byte PC
        {
            get => _BankSelect[2];
            set => throw new NotImplementedException($"[{nameof(StudioSetPart)}({Part}).{nameof(PC)}]\nUse the {nameof(BankSelect)}.{nameof(BankSelect.PC)} property instead.");
        }

        [Offset(0x0009)]
        public byte PartLevel
        {
            get { return _PartLevel; }
            set
            {
                if (_PartLevel != value)
                {
                    _PartLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000A)]
        public byte Pan
        {
            get { return _Pan; }
            set
            {
                if (_Pan != value)
                {
                    _Pan = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000B)]
        public int CoarseTune
        {
            get { return _CoarseTune.Deserialize(64); }
            set
            {
                if (CoarseTune != value)
                {
                    _CoarseTune = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public int FineTune
        {
            get { return _FineTune.Deserialize(64); }
            set
            {
                if (FineTune != value)
                {
                    _FineTune = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000D)]
        public IntegraMonyPolySwitch MonoPolySwitch
        {
            get { return _MonoPolySwitch; }
            set
            {
                if (_MonoPolySwitch != value)
                {
                    _MonoPolySwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000E)]
        public IntegraToneSwitch LegatoSwitch
        {
            get { return _LegatoSwitch; }
            set
            {
                if (_LegatoSwitch != value)
                {
                    _LegatoSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000F)]
        public byte PitchBendRange
        {
            get { return _PitchBendRange; }
            set
            {
                if (_PitchBendRange != value)
                {
                    _PitchBendRange = value.Clamp(0, 25);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0010)]
        public IntegraToneSwitch PortamentoSwitch
        {
            get { return _PortamentoSwitch; }
            set
            {
                if (_PortamentoSwitch != value)
                {
                    _PortamentoSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0011)]
        public short PortamentoTime
        {
            get
            {
                return _PortamentoTime.Deserialize();
            }
            set
            {
                if (PortamentoTime != value)
                {
                    _PortamentoTime = value.Serialize();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0013)]
        public int CutoffOffset
        {
            get { return _CutoffOffset.Deserialize(64); }
            set
            {
                if (CutoffOffset != value)
                {
                    _CutoffOffset = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0014)]
        public int ResonanceOffset
        {
            get { return _ResonanceOffset.Deserialize(64); }
            set
            {
                if (ResonanceOffset != value)
                {
                    _ResonanceOffset = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0015)]
        public int AttackTimeOffset
        {
            get { return _AttackTimeOffset.Deserialize(64); }
            set
            {
                if (AttackTimeOffset != value)
                {
                    _AttackTimeOffset = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0016)]
        public int DecayTimeOffset
        {
            get { return _DecayTimeOffset.Deserialize(64); }
            set
            {
                if (DecayTimeOffset != value)
                {
                    _DecayTimeOffset = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0017)]
        public int ReleaseTimeOffset
        {
            get { return _ReleaseTimeOffset.Deserialize(64); }
            set
            {
                if (ReleaseTimeOffset != value)
                {
                    _ReleaseTimeOffset = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0018)]
        public int VibratoRate
        {
            get { return _VibratoRate.Deserialize(64); }
            set
            {
                if (VibratoRate != value)
                {
                    _VibratoRate = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0019)]
        public int VibratoDepth
        {
            get { return _VibratoDepth.Deserialize(64); }
            set
            {
                if (VibratoDepth != value)
                {
                    _VibratoDepth = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001A)]
        public int VibratoDelay
        {
            get { return _VibratoDelay.Deserialize(64); }
            set
            {
                if (VibratoDelay != value)
                {
                    _VibratoDelay = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001B)]
        public int OctaveShift
        {
            get { return _OctaveShift.Deserialize(64); }
            set
            {
                if (OctaveShift != value)
                {
                    _OctaveShift = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001C)]
        public int VelocitySensOffset
        {
            get { return _VelocitySensOffset.Deserialize(63); }
            set
            {
                if (VelocitySensOffset != value)
                {
                    _VelocitySensOffset = value.Serialize(63);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001D)]
        public byte KeyboardRangeLower
        {
            get { return _KeyboardRangeLower; }
            set
            {
                if (_KeyboardRangeLower != value)
                {
                    _KeyboardRangeLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001E)]
        public byte KeyboardRangeUpper
        {
            get { return _KeyboardRangeUpper; }
            set
            {
                if (_KeyboardRangeUpper != value)
                {
                    _KeyboardRangeUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001F)]
        public byte KeyboardFadeWidthLower
        {
            get { return _KeyboardFadeWidthLower; }
            set
            {
                if (_KeyboardFadeWidthLower != value)
                {
                    _KeyboardFadeWidthLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0020)]
        public byte KeyboardFadeWidthUpper
        {
            get { return _KeyboardFadeWidthUpper; }
            set
            {
                if (_KeyboardFadeWidthUpper != value)
                {
                    _KeyboardFadeWidthUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0021)]
        public byte VelocityRangeLower
        {
            get { return _VelocityRangeLower; }
            set
            {
                if (_VelocityRangeLower != value)
                {
                    _VelocityRangeLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0022)]
        public byte VelocityRangeUpper
        {
            get { return _VelocityRangeUpper; }
            set
            {
                if (_VelocityRangeUpper != value)
                {
                    _VelocityRangeUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0023)]
        public byte VelocityFadeWidthLower
        {
            get { return _VelocityFadeWidthLower; }
            set
            {
                if (_VelocityFadeWidthLower != value)
                {
                    _VelocityFadeWidthLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0024)]
        public byte VelocityFadeWidthUpper
        {
            get { return _VelocityFadeWidthUpper; }
            set
            {
                if (_VelocityFadeWidthUpper != value)
                {
                    _VelocityFadeWidthUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0025)]
        public IntegraMuteSwitch MuteSwitch
        {
            get { return _MuteSwitch; }
            set
            {
                if (_MuteSwitch != value)
                {
                    _MuteSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0027)]
        public byte ChorusSendLevel
        {
            get { return _ChorusSendLevel; }
            set
            {
                if (_ChorusSendLevel != value)
                {
                    _ChorusSendLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0028)]
        public byte ReverbSendLevel
        {
            get { return _ReverbSendLevel; }
            set
            {
                if (_ReverbSendLevel != value)
                {
                    _ReverbSendLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0029)]
        public IntegraOutputAssigns OutputAssign
        {
            get { return _OutputAssign; }
            set
            {
                if (_OutputAssign != value)
                {
                    _OutputAssign = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002B)]
        public IntegraScaleTuneTypes ScaleTuneType
        {
            get { return _ScaleTuneType; }
            set
            {
                if (_ScaleTuneType != value)
                {
                    _ScaleTuneType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002C)]
        public IntegraScaleTuneKeys ScaleTuneKey
        {
            get { return _ScaleTuneKey; }
            set
            {
                if (_ScaleTuneKey != value)
                {
                    _ScaleTuneKey = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002D)]
        public int ScaleTuneC
        {
            get { return _ScaleTuneC.Deserialize(64); }
            set
            {
                if (ScaleTuneC != value)
                {
                    _ScaleTuneC = value.Serialize(64);
                    NotifyPropertyChanged();
                }
             }
        }

        [Offset(0x002E)]
        public int ScaleTuneCSharp
        {
            get { return _ScaleTuneCSharp.Deserialize(64); }
            set
            {
                if (ScaleTuneCSharp != value)
                {
                    _ScaleTuneCSharp = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002F)]
        public int ScaleTuneD
        {
            get { return _ScaleTuneD.Deserialize(64); }
            set
            {
                if (ScaleTuneD != value)
                {
                    _ScaleTuneD = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0030)]
        public int ScaleTuneDSharp
        {
            get { return _ScaleTuneDSharp.Deserialize(64); }
            set
            {
                if (ScaleTuneDSharp != value)
                {
                    _ScaleTuneDSharp = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0031)]
        public int ScaleTuneE
        {
            get { return _ScaleTuneE.Deserialize(64); }
            set
            {
                if (ScaleTuneE != value)
                {
                    _ScaleTuneE = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0032)]
        public int ScaleTuneF
        {
            get { return _ScaleTuneF.Deserialize(64); }
            set
            {
                if (ScaleTuneF != value)
                {
                    _ScaleTuneF = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0033)]
        public int ScaleTuneFSharp
        {
            get { return _ScaleTuneFSharp.Deserialize(64); }
            set
            {
                if (ScaleTuneFSharp != value)
                {
                    _ScaleTuneFSharp = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0034)]
        public int ScaleTuneG
        {
            get { return _ScaleTuneG.Deserialize(64); }
            set
            {
                if(ScaleTuneG != value)
                {
                    _ScaleTuneG = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0035)]
        public int ScaleTuneGSharp
        {
            get { return _ScaleTuneGSharp.Deserialize(64); }
            set
            {
                if (ScaleTuneGSharp != value)
                {
                    _ScaleTuneGSharp = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0036)]
        public int ScaleTuneA
        {
            get { return _ScaleTuneA.Deserialize(64); }
            set
            {
                if (ScaleTuneA != value)
                {
                    _ScaleTuneA = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0037)]
        public int ScaleTuneASharp
        {
            get { return _ScaleTuneASharp.Deserialize(64); }
            set
            {
                if (ScaleTuneASharp != value)
                {
                    _ScaleTuneASharp = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0038)]
        public int ScaleTuneB
        {
            get { return _ScaleTuneB.Deserialize(64); }
            set
            {
                if (ScaleTuneB != value)
                {
                    _ScaleTuneB = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0039)]
        public IntegraSwitch ReceiveProgramChange
        {
            get { return _ReceiveProgramChange; }
            set
            {
                if (_ReceiveProgramChange != value)
                {
                    _ReceiveProgramChange = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003A)]
        public IntegraSwitch ReceiveBankSelect
        {
            get { return _ReceiveBankSelect; }
            set
            {
                if (_ReceiveBankSelect != value)
                {
                    _ReceiveBankSelect = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003B)]
        public IntegraSwitch ReceivePitchBend
        {
            get { return _ReceivePitchBend; }
            set
            {
                if (_ReceivePitchBend != value)
                {
                    _ReceivePitchBend = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003C)]
        public IntegraSwitch ReceivePolyPhonicKeyPressure
        {
            get { return _ReceivePolyPhonicKeyPressure; }
            set
            {
                if (_ReceivePolyPhonicKeyPressure != value)
                {
                    _ReceivePolyPhonicKeyPressure = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003D)]
        public IntegraSwitch ReceiveChannelPressure
        {
            get { return _ReceiveChannelPressure; }
            set
            {
                if (_ReceiveChannelPressure != value)
                {
                    _ReceiveChannelPressure = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003E)]
        public IntegraSwitch ReceiveModulation
        {
            get { return _ReceiveModulation; }
            set
            {
                if (_ReceiveModulation != value)
                {
                    _ReceiveModulation = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003F)]
        public IntegraSwitch ReceiveVolume
        {
            get { return _ReceiveVolume; }
            set
            {
                if (_ReceiveVolume != value)
                {
                    _ReceiveVolume = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0040)]
        public IntegraSwitch ReceivePan
        {
            get { return _ReceivePan; }
            set
            {
                if (_ReceivePan != value)
                {
                    _ReceivePan = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0041)]
        public IntegraSwitch ReceiveExpression
        {
            get { return _ReceiveExpression; }
            set
            {
                if (_ReceiveExpression != value)
                {
                    _ReceiveExpression = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0042)]
        public IntegraSwitch ReceiveHold
        {
            get { return _ReceiveHold; }
            set
            {
                if (_ReceiveHold != value)
                {
                    _ReceiveHold = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0043)]
        public IntegraVelocityCurveTypes VelocityCurveType
        {
            get { return _VelocityCurveType; }
            set
            {if (_VelocityCurveType != value)
                {
                    _VelocityCurveType = value;
                    NotifyPropertyChanged();
                }
            }
        }


        [Offset(0x0044)]
        public int MotionalSurroundLR
        {
            get { return _MotionalSurroundLR.Deserialize(64); }
            set
            {
                if (MotionalSurroundLR != value)
                {
                    _MotionalSurroundLR = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0046)]
        public int MotionalSurroundFB
        {
            get { return _MotionalSurroundFB.Deserialize(64); }
            set
            {
                if (MotionalSurroundFB != value)
                {
                    _MotionalSurroundFB = value.Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0048)]
        public byte MotionalSurroundWidth
        {
            get { return _MotionalSurroundWidth; }
            set
            {
                if (_MotionalSurroundWidth != value)
                {
                    _MotionalSurroundWidth = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0049)]
        public byte MotionalSurroundAmbienceSendLevel
        {
            get { return _MotionalSurroundAmbienceSendLevel; }
            set
            {
                if (_MotionalSurroundAmbienceSendLevel != value)
                {
                    _MotionalSurroundAmbienceSendLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Overrides: Model

        public override bool IsInitialized
        {
            get => base.IsInitialized;
            protected internal set
            {
                if (base.IsInitialized = value)
                {
                    Device.NotifyToneChanged(this, Part);
                }
            }
        }

        #endregion
    }
}