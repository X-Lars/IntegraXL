using Integra.Core;
using Integra.Core.Interfaces;
using Integra.Common;

namespace Integra.Models
{
    public class StudioSetPart : IntegraBase<StudioSetPart>, IIntegraPartial
    {
        private IntegraParts _Part;
        
        private IntegraToneTypes _Type = IntegraToneTypes.SuperNATURALAcousticTone;

        private TemporaryTone _TemporaryTone;

        

        [Offset(0x0000)] private byte _ReceiveChannel;
        [Offset(0x0001)] private byte _ReceiveSwitch;
        [Offset(0x0006)] private byte _ToneBankSelectMSB;
        [Offset(0x0007)] private byte _ToneBankSelectLSB;
        [Offset(0x0008)] private byte _ToneProgramNumber;

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

        [Offset(0x0027)] private byte _ChorusSendLevel;
        [Offset(0x0028)] private byte _ReverbSendLevel;
        [Offset(0x0029)] private IntegraOutputAssigns _OutputAssign;

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
        [Offset(0x0046)] private byte _MotionalSurroundFB;
        [Offset(0x0048)] private byte _MotionalSurroundWidth;
        [Offset(0x0049)] private byte _MotionalSurroundAmbienceSendLevel;

        public StudioSetPart()
        {
        }

        public StudioSetPart(IntegraParts part)// : base(new IntegraAddress(0x18, 0x00, (byte)(0x20 + part), 0x00), new IntegraRequest(0x00, 0x00, 0x00, 0x4D))
        {
            Name = $"Studio Set Part {(int)part + 1}";
            Part = part;

            Address = new IntegraAddress(0x18, 0x00, (byte)(0x20 + part), 0x00);
            Requests.Add(new IntegraRequest(0x0000004D));

            Initialize();
        }

        public IntegraParts Part
        {
            get { return _Part; }
            set
            {
                _Part = value;
                NotifyPropertyChanged();
            }
        }

        public IntegraToneTypes Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                NotifyPropertyChanged();
            }
        }

        public TemporaryTone TemporaryTone
        {
            get { return _TemporaryTone; }
            set
            {
                if(_TemporaryTone != value)
                {
                    _TemporaryTone = value;
                    NotifyPropertyChanged();
                }
            }
        }

        

        [Offset(0x0000)]
        public IntegraChannels ReceiveChannel
        {
            get { return (IntegraChannels)_ReceiveChannel; }
            set
            {
                _ReceiveChannel = (byte)value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0001)]
        public IntegraSwitch ReceiveSwitch
        {
            get { return (IntegraSwitch)_ReceiveSwitch; }
            set
            {
                _ReceiveSwitch = (byte)value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public byte ToneBankSelectMSB
        {
            get { return _ToneBankSelectMSB; }
            set
            {
                _ToneBankSelectMSB = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0007)]
        public byte ToneBankSelectLSB
        {
            get { return _ToneBankSelectLSB; }
            set
            {
                _ToneBankSelectLSB = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0008)]
        public byte ToneProgramNumber
        {
            get { return _ToneProgramNumber; }
            set
            {
                _ToneProgramNumber = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0009)]
        public byte PartLevel
        {
            get { return _PartLevel; }
            set
            {
                _PartLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000A)]
        public byte Pan
        {
            get { return _Pan.Offset(-64); }
            set
            {
                _Pan = value.Offset(64);
                NotifyPropertyChanged();
            }
        }
        [Offset(0x000B)]
        public byte CoarseTune
        {
            get { return _CoarseTune.Offset(-48); }
            set
            {
                _CoarseTune = value.Offset(48);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000C)]
        public byte FineTune
        {
            get { return _FineTune.Offset(-50); }
            set
            {
                _FineTune = value.Offset(50);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000D)]
        public IntegraMonyPolySwitch MonoPolySwitch
        {
            get { return _MonoPolySwitch; }
            set
            {
                _MonoPolySwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000E)]
        public IntegraToneSwitch LegatoSwitch
        {
            get { return _LegatoSwitch; }
            set
            {
                _LegatoSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000F)]
        public byte PitchBendRange
        {
            get { return _PitchBendRange; }
            set
            {
                _PitchBendRange = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0010)]
        public IntegraToneSwitch PortamentoSwitch
        {
            get { return _PortamentoSwitch; }
            set
            {
                _PortamentoSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0011)]
        public short PortamentoTime
        {
            get
            {
                return _PortamentoTime.GetShort();
            }
            set
            {
                _PortamentoTime = value.GetBytes();
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0013)]
        public byte CutoffOffset
        {
            get { return _CutoffOffset.Offset(-64); }
            set
            {
                _CutoffOffset = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0014)]
        public byte ResonanceOffset
        {
            get { return _ResonanceOffset.Offset(-64); }
            set
            {
                _ResonanceOffset = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0015)]
        public byte AttackTimeOffset
        {
            get { return _AttackTimeOffset.Offset(-64); }
            set
            {
                _AttackTimeOffset = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0016)]
        public byte DecayTimeOffset
        {
            get { return _DecayTimeOffset.Offset(-64); }
            set
            {
                _DecayTimeOffset = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0017)]
        public byte ReleaseTimeOffset
        {
            get { return _ReleaseTimeOffset.Offset(-64); }
            set
            {
                _ReleaseTimeOffset = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0018)]
        public byte VibratoRate
        {
            get { return _VibratoRate.Offset(-64); }
            set
            {
                _VibratoRate = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0019)]
        public byte VibratoDepth
        {
            get { return _VibratoDepth.Offset(-64); }
            set
            {
                _VibratoDepth = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001A)]
        public byte VibratoDelay
        {
            get { return _VibratoDelay.Offset(-64); }
            set
            {
                _VibratoDelay = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001B)]
        public byte OctaveShift
        {
            get { return _OctaveShift.Offset(-3); }
            set
            {
                _OctaveShift = value.Offset(3);
            }
        }

        [Offset(0x001C)]
        public byte VelocitySensOffset
        {
            get { return _VelocitySensOffset.Offset(-63); }
            set
            {
                _VelocitySensOffset = value.Offset(63);
            }
        }

        // TODO: Keyboard ranges
        //[Offset(0x001D)] 
        //public byte _KeyboardRangeLower;
        //[Offset(0x001E)] 
        //public byte _KeyboardRangeUpper;

        [Offset(0x001F)]
        public byte KeyboardFadeWidthLower
        {
            get { return _KeyboardFadeWidthLower; }
            set
            {
                _KeyboardFadeWidthLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0020)]
        public byte KeyboardFadeWidthUpper
        {
            get { return _KeyboardFadeWidthUpper; }
            set
            {
                _KeyboardFadeWidthUpper = value;
                NotifyPropertyChanged();
            }
        }
        // TODO: Velocity ranges
        //[Offset(0x0021)] 
        //public byte _VelocityRangeLower;
        //[Offset(0x0022)] 
        //public byte _VelocityRangeUpper;

        [Offset(0x0023)]
        public byte VelocityFadeWidthLower
        {
            get { return _VelocityFadeWidthLower; }
            set
            {
                _VelocityFadeWidthLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0024)]
        public byte VelocityFadeWidthUpper
        {
            get { return _VelocityFadeWidthUpper; }
            set
            {
                _VelocityFadeWidthUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0025)]
        public IntegraMuteSwitch MuteSwitch
        {
            get { return _MuteSwitch; }
            set
            {
                _MuteSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0027)]
        public byte ChorusSendLevel
        {
            get { return _ChorusSendLevel; }
            set
            {
                _ChorusSendLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0028)]
        public byte ReverbSendLevel
        {
            get { return _ReverbSendLevel; }
            set
            {
                _ReverbSendLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0029)]
        public IntegraOutputAssigns OutputAssign
        {
            get { return _OutputAssign; }
            set
            {
                _OutputAssign = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002B)]
        public IntegraScaleTuneTypes ScaleTuneType
        {
            get { return _ScaleTuneType; }
            set
            {
                _ScaleTuneType = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002C)]
        public IntegraScaleTuneKeys ScaleTuneKey
        {
            get { return _ScaleTuneKey; }
            set
            {
                _ScaleTuneKey = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002D)]
        public byte ScaleTuneC
        {
            get { return _ScaleTuneC.Offset(-64); }
            set
            {
                _ScaleTuneC = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002E)]
        public byte ScaleTuneCSharp
        {
            get { return _ScaleTuneCSharp.Offset(-64); }
            set
            {
                _ScaleTuneCSharp = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002F)]
        public byte ScaleTuneD
        {
            get { return _ScaleTuneD.Offset(-64); }
            set
            {
                _ScaleTuneD = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0030)]
        public byte ScaleTuneDSharp
        {
            get { return _ScaleTuneDSharp.Offset(-64); }
            set
            {
                _ScaleTuneDSharp = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0031)]
        public byte ScaleTuneE
        {
            get { return _ScaleTuneE.Offset(-64); }
            set
            {
                _ScaleTuneE = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0032)]
        public byte ScaleTuneF
        {
            get { return _ScaleTuneF.Offset(-64); }
            set
            {
                _ScaleTuneF = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0033)]
        public byte ScaleTuneFSharp
        {
            get { return _ScaleTuneFSharp.Offset(-64); }
            set
            {
                _ScaleTuneFSharp = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0034)]
        public byte ScaleTuneG
        {
            get { return _ScaleTuneG.Offset(-64); }
            set
            {
                _ScaleTuneG = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0035)]
        public byte ScaleTuneGSharp
        {
            get { return _ScaleTuneGSharp.Offset(-64); }
            set
            {
                _ScaleTuneGSharp = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0036)]
        public byte ScaleTuneA
        {
            get { return _ScaleTuneA.Offset(-64); }
            set
            {
                _ScaleTuneA = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0037)]
        public byte ScaleTuneASharp
        {
            get { return _ScaleTuneASharp.Offset(-64); }
            set
            {
                _ScaleTuneASharp = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0038)]
        public byte ScaleTuneB
        {
            get { return _ScaleTuneB.Offset(-64); }
            set
            {
                _ScaleTuneB = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0039)]
        public IntegraSwitch ReceiveProgramChange
        {
            get { return _ReceiveProgramChange; }
            set
            {
                _ReceiveProgramChange = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003A)]
        public IntegraSwitch ReceiveBankSelect
        {
            get { return _ReceiveBankSelect; }
            set
            {
                _ReceiveBankSelect = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003B)]
        public IntegraSwitch ReceivePitchBend
        {
            get { return _ReceivePitchBend; }
            set
            {
                _ReceivePitchBend = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003C)]
        public IntegraSwitch ReceivePolyPhonicKeyPressure
        {
            get { return _ReceivePolyPhonicKeyPressure; }
            set
            {
                _ReceivePolyPhonicKeyPressure = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003D)]
        public IntegraSwitch ReceiveChannelPressure
        {
            get { return _ReceiveChannelPressure; }
            set
            {
                _ReceiveChannelPressure = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003E)]
        public IntegraSwitch ReceiveModulation
        {
            get { return _ReceiveModulation; }
            set
            {
                _ReceiveModulation = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003F)]
        public IntegraSwitch ReceiveVolume
        {
            get { return _ReceiveVolume; }
            set
            {
                _ReceiveVolume = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0040)]
        public IntegraSwitch ReceivePan
        {
            get { return _ReceivePan; }
            set
            {
                _ReceivePan = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0041)]
        public IntegraSwitch ReceiveExpression
        {
            get { return _ReceiveExpression; }
            set
            {
                _ReceiveExpression = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0042)]
        public IntegraSwitch ReceiveHold
        {
            get { return _ReceiveHold; }
            set
            {
                _ReceiveHold = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0043)]
        public IntegraVelocityCurveTypes VelocityCurveType
        {
            get { return _VelocityCurveType; }
            set
            {
                _VelocityCurveType = value;
                NotifyPropertyChanged();
            }
        }


        [Offset(0x0044)]
        public byte MotionalSurroundLR
        {
            get { return _MotionalSurroundLR.Offset(-64); }
            set
            {
                _MotionalSurroundLR = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0046)]
        public byte MotionalSurroundFB
        {
            get { return _MotionalSurroundFB.Offset(-64); }
            set
            {
                _MotionalSurroundFB = value.Offset(64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0048)]
        public byte MotionalSurroundWidth
        {
            get { return _MotionalSurroundWidth; }
            set
            {
                _MotionalSurroundWidth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0049)]
        public byte MotionalSurroundAmbienceSendLevel
        {
            get { return _MotionalSurroundAmbienceSendLevel; }
            set
            {
                _MotionalSurroundAmbienceSendLevel = value;
                NotifyPropertyChanged();
            }
        }

        #region Overrides



        protected override bool Initialize(byte[] data)
        {
            if(!IsInitialized)
            {
                //Part = (IntegraParts)((Address & 0x00000F00) >> 8);

                base.Initialize(data);

                Type = IntegraToneExtensions.Type(ToneBankSelectMSB);

                TemporaryTone = new TemporaryTone(Part, Type);

                //switch(Type)
                //{
                //    case IntegraToneTypes.SuperNATURALAcousticTone:
                //        SuperNATURALAcousticTone = new SuperNATURALAcousticTone(TemporaryTone.Address);
                //        break;
                //    case IntegraToneTypes.SuperNATURALSynthTone:
                //        SuperNATURALSynthTone = new SuperNATURALSynthTone(TemporaryTone.Address);
                //        break;
                //    case IntegraToneTypes.SuperNATURALDrumkit:
                //        SuperNATURALDrumKit = new SuperNATURALDrumKit(TemporaryTone.Address);
                //        break;
                //    case IntegraToneTypes.PCMSynthTone:
                //        PCMSynthTone = new PCMSynthTone(TemporaryTone.Address);
                //        break;
                //    case IntegraToneTypes.PCMDrumkit:
                //        PCMDrumKit = new PCMDrumKit(TemporaryTone.Address);
                //        break;
                //}

                NotifyPropertyChanged(nameof(Part), false);
                NotifyPropertyChanged(nameof(Type), false);
                IsInitialized = true;
            }

            return IsInitialized;
        }

        
        #endregion
    }
}
