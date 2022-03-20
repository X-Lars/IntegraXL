using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using IntegraXL.Templates;
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

        [Offset(0x0002)] private byte[] _RESERVED1 = new byte[4];

        // Combines the MSB, LSB & PC into one field
        [Offset(0x0006)] private byte[] _BankSelect = new byte[3];
        //[Offset(0x0006)] private byte _ToneBankSelectMSB;
        //[Offset(0x0007)] private byte _ToneBankSelectLSB;
        //[Offset(0x0008)] private byte _ToneProgramNumber;

        [Offset(0x0009)] private byte _Level;
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
        [Offset(0x001D)] private IntegraKeyRange _KeyRangeLower;
        [Offset(0x001E)] private IntegraKeyRange _KeyRangeUpper;
        [Offset(0x001F)] private byte _KeyFadeLower;
        [Offset(0x0020)] private byte _KeyFadeUpper;
        [Offset(0x0021)] private byte _VelocityRangeLower;
        [Offset(0x0022)] private byte _VelocityRangeUpper;
        [Offset(0x0023)] private byte _VelocityFadeLower;
        [Offset(0x0024)] private byte _VelocityFadeUpper;
        [Offset(0x0025)] private IntegraMuteSwitch _MuteSwitch;

        [Offset(0x0026)] private byte _RESERVED2;

        [Offset(0x0027)] private byte _ChorusSendLevel;
        [Offset(0x0028)] private byte _ReverbSendLevel;
        [Offset(0x0029)] private IntegraOutputAssigns _OutputAssign;

        [Offset(0x002A)] private byte _RESERVED3;

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
        [Offset(0x003C)] private IntegraSwitch _ReceiveKeyPressure;
        [Offset(0x003D)] private IntegraSwitch _ReceiveChannelPressure;
        [Offset(0x003E)] private IntegraSwitch _ReceiveModulation;
        [Offset(0x003F)] private IntegraSwitch _ReceiveVolume;
        [Offset(0x0040)] private IntegraSwitch _ReceivePan;
        [Offset(0x0041)] private IntegraSwitch _ReceiveExpression;
        [Offset(0x0042)] private IntegraSwitch _ReceiveHold;

        [Offset(0x0043)] private IntegraVelocityCurveTypes _VelocityCurveType;

        [Offset(0x0044)] private byte _MotionalSurroundLR;

        [Offset(0x0045)] private byte _RESERVED4;

        [Offset(0x0046)] private byte _MotionalSurroundFB;
        [Offset(0x0047)] private byte _RESERVED5;
        [Offset(0x0048)] private byte _MotionalSurroundWidth;
        [Offset(0x0049)] private byte _MotionalSurroundAmbienceSendLevel;

        [Offset(0x004A)] private byte[] _RESERVED6 = new byte[3];

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="StudioSetPart"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the model.</param>
        /// <param name="part">The model's associated part.</param>
        private StudioSetPart(Integra device, Parts part) : base(device, part) 
        {
            PropertyChanged += ModelPropertyChanged;
        }

        
        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public IntegraChannels ReceiveChannel
        {
            get => _ReceiveChannel;
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
            get => _ReceiveSwitch;
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
        public byte Level
        {
            get => _Level;
            set
            {
                if (_Level != value)
                {
                    _Level = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000A)]
        public byte Pan
        {
            get => _Pan;
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
            get => _CoarseTune.Deserialize(64);
            set
            {
                if (CoarseTune != value)
                {
                    _CoarseTune = value.Clamp(-48, 48).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public int FineTune
        {
            get => _FineTune.Deserialize(64);
            set
            {
                if (FineTune != value)
                {
                    _FineTune = value.Clamp(-50, 50).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000D)]
        public IntegraMonyPolySwitch MonoPolySwitch
        {
            get => _MonoPolySwitch;
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
            get => _LegatoSwitch;
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
            get => _PitchBendRange;
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
            get => _PortamentoSwitch;
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
            get => _PortamentoTime.Deserialize();
            set
            {
                if (PortamentoTime != value)
                {
                    _PortamentoTime = value.Serialize(0, 128);
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
                    _CutoffOffset = value.Clamp(-64, 63).Serialize(64);
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
                    _ResonanceOffset = value.Clamp(-64, 63).Serialize(64);
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
                    _AttackTimeOffset = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0016)]
        public int DecayTimeOffset
        {
            get => _DecayTimeOffset.Deserialize(64);
            set
            {
                if (DecayTimeOffset != value)
                {
                    _DecayTimeOffset = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0017)]
        public int ReleaseTimeOffset
        {
            get => _ReleaseTimeOffset.Deserialize(64);
            set
            {
                if (ReleaseTimeOffset != value)
                {
                    _ReleaseTimeOffset = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0018)]
        public int VibratoRate
        {
            get => _VibratoRate.Deserialize(64);
            set
            {
                if (VibratoRate != value)
                {
                    _VibratoRate = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0019)]
        public int VibratoDepth
        {
            get => _VibratoDepth.Deserialize(64);
            set
            {
                if (VibratoDepth != value)
                {
                    _VibratoDepth = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001A)]
        public int VibratoDelay
        {
            get => _VibratoDelay.Deserialize(64);
            set
            {
                if (VibratoDelay != value)
                {
                    _VibratoDelay = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001B)]
        public int OctaveShift
        {
            get => _OctaveShift.Deserialize(64);
            set
            {
                if (OctaveShift != value)
                {
                    _OctaveShift = value.Clamp(-3, 3).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001C)]
        public int VelocitySensOffset
        {
            get => _VelocitySensOffset.Deserialize(64);
            set
            {
                if (VelocitySensOffset != value)
                {
                    _VelocitySensOffset = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001D)]
        public IntegraKeyRange KeyRangeLower
        {
            get => _KeyRangeLower;
            set
            {
                if (_KeyRangeLower != value)
                {
                    _KeyRangeLower = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001E)]
        public IntegraKeyRange KeyRangeUpper
        {
            get => _KeyRangeUpper;
            set
            {
                if (_KeyRangeUpper != value)
                {
                    _KeyRangeUpper = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001F)]
        public byte KeyFadeLower
        {
            get => _KeyFadeLower;
            set
            {
                if (_KeyFadeLower != value)
                {
                    _KeyFadeLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0020)]
        public byte KeyFadeUpper
        {
            get => _KeyFadeUpper;
            set
            {
                if (_KeyFadeUpper != value)
                {
                    _KeyFadeUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0021)]
        public byte VelocityRangeLower
        {
            get => _VelocityRangeLower;
            set
            {
                if (_VelocityRangeLower != value)
                {
                    _VelocityRangeLower = value.Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0022)]
        public byte VelocityRangeUpper
        {
            get => _VelocityRangeUpper;
            set
            {
                if (_VelocityRangeUpper != value)
                {
                    _VelocityRangeUpper = value.Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0023)]
        public byte VelocityFadeLower
        {
            get => _VelocityFadeLower;
            set
            {
                if (_VelocityFadeLower != value)
                {
                    _VelocityFadeLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0024)]
        public byte VelocityFadeUpper
        {
            get => _VelocityFadeUpper;
            set
            {
                if (_VelocityFadeUpper != value)
                {
                    _VelocityFadeUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0025)]
        public IntegraMuteSwitch MuteSwitch
        {
            get => _MuteSwitch;
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
            get => _ChorusSendLevel;
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
            get => _ReverbSendLevel;
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
            get => _OutputAssign;
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
            get => _ScaleTuneType;
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
            get => _ScaleTuneKey;
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
            get => _ScaleTuneC.Deserialize(64);
            set
            {
                if (ScaleTuneC != value)
                {
                    SetCustomScaleTuneTemplate();

                    _ScaleTuneC = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
             }
        }

        [Offset(0x002E)]
        public int ScaleTuneCSharp
        {
            get => _ScaleTuneCSharp.Deserialize(64);
            set
            {
                if (ScaleTuneCSharp != value)
                {
                    SetCustomScaleTuneTemplate();

                    _ScaleTuneCSharp = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002F)]
        public int ScaleTuneD
        {
            get => _ScaleTuneD.Deserialize(64);
            set
            {
                if (ScaleTuneD != value)
                {
                    SetCustomScaleTuneTemplate();

                    _ScaleTuneD = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0030)]
        public int ScaleTuneDSharp
        {
            get => _ScaleTuneDSharp.Deserialize(64);
            set
            {
                if (ScaleTuneDSharp != value)
                {
                    SetCustomScaleTuneTemplate();

                    _ScaleTuneDSharp = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0031)]
        public int ScaleTuneE
        {
            get => _ScaleTuneE.Deserialize(64);
            set
            {
                if (ScaleTuneE != value)
                {
                    SetCustomScaleTuneTemplate();

                    _ScaleTuneE = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0032)]
        public int ScaleTuneF
        {
            get => _ScaleTuneF.Deserialize(64);
            set
            {
                if (ScaleTuneF != value)
                {
                    SetCustomScaleTuneTemplate();

                    _ScaleTuneF = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0033)]
        public int ScaleTuneFSharp
        {
            get => _ScaleTuneFSharp.Deserialize(64);
            set
            {
                if (ScaleTuneFSharp != value)
                {
                    SetCustomScaleTuneTemplate();

                    _ScaleTuneFSharp = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0034)]
        public int ScaleTuneG
        {
            get => _ScaleTuneG.Deserialize(64);
            set
            {
                if (ScaleTuneG != value)
                {
                    SetCustomScaleTuneTemplate();

                    _ScaleTuneG = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0035)]
        public int ScaleTuneGSharp
        {
            get => _ScaleTuneGSharp.Deserialize(64);
            set
            {
                if (ScaleTuneGSharp != value)
                {
                    SetCustomScaleTuneTemplate();

                    _ScaleTuneGSharp = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0036)]
        public int ScaleTuneA
        {
            get => _ScaleTuneA.Deserialize(64);
            set
            {
                if (ScaleTuneA != value)
                {
                    SetCustomScaleTuneTemplate();

                    _ScaleTuneA = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0037)]
        public int ScaleTuneASharp
        {
            get => _ScaleTuneASharp.Deserialize(64);
            set
            {
                if (ScaleTuneASharp != value)
                {
                    SetCustomScaleTuneTemplate();

                    _ScaleTuneASharp = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0038)]
        public int ScaleTuneB
        {
            get => _ScaleTuneB.Deserialize(64);
            set
            {
                if (ScaleTuneB != value)
                {
                    SetCustomScaleTuneTemplate();

                    _ScaleTuneB = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0039)]
        public IntegraSwitch ReceiveProgramChange
        {
            get => _ReceiveProgramChange;
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
            get => _ReceiveBankSelect;
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
            get => _ReceivePitchBend;
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
        public IntegraSwitch ReceiveKeyPressure
        {
            get => _ReceiveKeyPressure;
            set
            {
                if (_ReceiveKeyPressure != value)
                {
                    _ReceiveKeyPressure = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003D)]
        public IntegraSwitch ReceiveChannelPressure
        {
            get => _ReceiveChannelPressure;
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
            get => _ReceiveModulation;
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
            get => _ReceiveVolume;
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
            get => _ReceivePan;
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
            get => _ReceiveExpression;
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
            get => _ReceiveHold;
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
            get => _VelocityCurveType;
            set
            {
                if (_VelocityCurveType != value)
                {
                    _VelocityCurveType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0044)]
        public int MotionalSurroundLR
        {
            get => _MotionalSurroundLR.Deserialize(64);
            set
            {
                if (MotionalSurroundLR != value)
                {
                    _MotionalSurroundLR = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0046)]
        public int MotionalSurroundFB
        {
            get => _MotionalSurroundFB.Deserialize(64);
            set
            {
                if (MotionalSurroundFB != value)
                {
                    _MotionalSurroundFB = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0048)]
        public byte MotionalSurroundWidth
        {
            get => _MotionalSurroundWidth;
            set
            {
                if (_MotionalSurroundWidth != value)
                {
                    _MotionalSurroundWidth = value.Clamp(0, 32);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0049)]
        public byte MotionalSurroundAmbienceSendLevel
        {
            get => _MotionalSurroundAmbienceSendLevel;
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

        #region Methods

        /// <summary>
        /// Sets the scale tune template to custom.
        /// </summary>
        /// <remarks><i>
        /// Invoked by the individual scale tune properties.
        /// </i></remarks>
        private void SetCustomScaleTuneTemplate()
        {
            if(ScaleTuneType != IntegraScaleTuneTypes.Custom)
            {
                _ScaleTuneType   = IntegraScaleTuneTypes.Custom;
                _ScaleTuneKey    = IntegraScaleTuneKeys.C;

                NotifyPropertyChanged(string.Empty);
            }
        }

        #endregion

        #region Overrides: Model

        /// <summary>
        /// Gets wheter the model is initialized.
        /// </summary>
        /// <remarks><i>
        /// Raises the <see cref="Integra.ToneChanged"/> on initialization.
        /// </i></remarks>
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

        #region Event Handlers

        /// <summary>
        /// Handles the <see cref="IntegraModel.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="IntegraModel"/> that raised the event.</param>
        /// <param name="e">The event's associated data.</param>
        /// <remarks><i>
        /// - Raises the <see cref="Integra.ToneChanged"/> event for the <see cref="BankSelect"/> property.<br/>
        /// - Sets the scale tune template for the <see cref="ScaleTuneType"/> and <see cref="ScaleTuneKey"/> properties.<br/>
        /// </i></remarks>
        private void ModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BankSelect))
            {
                Device.NotifyToneChanged(this, Part);
            }
            else if(e.PropertyName == nameof(ScaleTuneType) || e.PropertyName == nameof(ScaleTuneKey))
            {
                if (ScaleTuneType == IntegraScaleTuneTypes.Custom)
                    return;

                ScaleTuneTemplate template = this.GetTemplate();

                _ScaleTuneC      = template.Values[0];
                _ScaleTuneCSharp = template.Values[1];
                _ScaleTuneD      = template.Values[2];
                _ScaleTuneDSharp = template.Values[3];
                _ScaleTuneE      = template.Values[4];
                _ScaleTuneF      = template.Values[5];
                _ScaleTuneFSharp = template.Values[6];
                _ScaleTuneG      = template.Values[7];
                _ScaleTuneGSharp = template.Values[8];
                _ScaleTuneA      = template.Values[9];
                _ScaleTuneASharp = template.Values[10];
                _ScaleTuneB      = template.Values[11];

                NotifyPropertyChanged(string.Empty);
            }
        }

        #endregion

        #region Enumerations

        public List<string> PanValues
        {
            get { return IntegraPan.Values; }
        }


        public List<string> PitchBendRanges => IntegraPitchBendRanges.Values;
        public List<string> PortamentoTimes => IntegraPortamentoTime.Values;

        #endregion
    }
}