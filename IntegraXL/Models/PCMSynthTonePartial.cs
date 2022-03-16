using IntegraXL.Core;
using System.Diagnostics;
using IntegraXL.Extensions;
using System.Reflection;
using IntegraXL.Templates;

namespace IntegraXL.Models
{

    [Integra(0x00002000, 0x00001000, 4)]
    public sealed class PCMSynthTonePartials : IntegraCollection<PCMSynthTonePartial>
    {
        #region Constructor

        /// <summary>
        /// Creates a new uninitialized MIDI enabled partial collection.
        /// </summary>
        /// <param name="device">The device for data transmission.</param>
        internal PCMSynthTonePartials(PCMSynthTone tone) : base(tone.Device, false)
        {
            Address = tone.Address;

            IntegraRequest request = new (Attribute.Request);

            Requests.Add(request);

            for (int i = 0; i < Size; i++)
            {
                PCMSynthTonePartial? partial = Activator.CreateInstance(typeof(PCMSynthTonePartial), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { tone, i }, null) as PCMSynthTonePartial;

                Debug.Assert(partial != null);

                Add(partial);
            }

            Connect();
        }

        #endregion

        #region Overrides: IntegraModel

        public override bool IsInitialized
        {
            get => Collection.All(x => x.IsInitialized);// Collection.Last().IsInitialized;
        }

        protected override void SystemExclusiveReceived(object sender, IntegraSystemExclusiveEventArgs e)
        {
            if (!IsInitialized)
            {
                if (e.SystemExclusive.Address.InRange(this.First().Address, this.Last().Address))
                {
                    Device.ReportProgress(this, Collection.Where(x => x.IsInitialized).Count(), Size, e.SystemExclusive.Address.GetTemporaryTonePart());
                }
            }
        }

        internal override bool Initialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    /// <summary>
    /// Defines the structure of an INTEGRA-7 PCM synth tone partial.
    /// </summary>
    [Integra(0x00002000, 0x0000011A)]
    public class PCMSynthTonePartial : IntegraModel<PCMSynthTonePartial>
    {
        #region Fields: INTEGRA-7

        #region Fields: General

        [Offset(0x0000)] byte _PartialLevel;
        [Offset(0x0001)] byte _CoarseTune;
        [Offset(0x0002)] byte _FineTune;
        [Offset(0x0003)] byte _RandomPitchDepth;
        [Offset(0x0004)] byte _Pan;
        [Offset(0x0005)] byte _PanKeyFollow;
        [Offset(0x0006)] byte _RandomPanDepth;
        [Offset(0x0007)] byte _AlternatePanDepth;
        [Offset(0x0008)] IntegraEnvelopeMode _EnvMode;
        [Offset(0x0009)] IntegraDelayMode _DelayMode;
        [Offset(0x000A)] byte[] _DelayTime = new byte[2];
        [Offset(0x000C)] byte _OutputLevel;
        
        [Offset(0x000D)] byte[] RESERVED01 = new byte[2];

        [Offset(0x000F)] byte _ChorusSendLevel;
        [Offset(0x0010)] byte _ReverbSendLevel;

        [Offset(0x0011)] byte RESERVED02;

        [Offset(0x0012)] IntegraSwitch _ReceiveBender;
        [Offset(0x0013)] IntegraSwitch _ReceiveExpression;
        [Offset(0x0014)] IntegraSwitch _ReceiveHold;

        [Offset(0x0015)] byte RESERVED03;

        [Offset(0x0016)] IntegraSwitch _RedamperSwitch;

        #endregion

        #region Fields: Matrix Control

        [Offset(0x0017)] IntegraControlSwitch _Control1Switch1;
        [Offset(0x0018)] IntegraControlSwitch _Control1Switch2;
        [Offset(0x0019)] IntegraControlSwitch _Control1Switch3;
        [Offset(0x001A)] IntegraControlSwitch _Control1Switch4;

        [Offset(0x001B)] IntegraControlSwitch _Control2Switch1;
        [Offset(0x001C)] IntegraControlSwitch _Control2Switch2;
        [Offset(0x001D)] IntegraControlSwitch _Control2Switch3;
        [Offset(0x001E)] IntegraControlSwitch _Control2Switch4;

        [Offset(0x001F)] IntegraControlSwitch _Control3Switch1;
        [Offset(0x0020)] IntegraControlSwitch _Control3Switch2;
        [Offset(0x0021)] IntegraControlSwitch _Control3Switch3;
        [Offset(0x0022)] IntegraControlSwitch _Control3Switch4;

        [Offset(0x0023)] IntegraControlSwitch _Control4Switch1;
        [Offset(0x0024)] IntegraControlSwitch _Control4Switch2;
        [Offset(0x0025)] IntegraControlSwitch _Control4Switch3;
        [Offset(0x0026)] IntegraControlSwitch _Control4Switch4;

        #endregion

        #region Fields: Waveform

        [Offset(0x0027)] IntegraWaveGroupType _WaveGroupType;
        [Offset(0x0028)] IntegraPCMWaveGroups _WaveGroupID;
        [Offset(0x002C)] int _WaveNumberL;
        [Offset(0x0030)] int _WaveNumberR;
        [Offset(0x0034)] byte _WaveGain;
        [Offset(0x0035)] IntegraSwitch _WaveFXMSwitch;
        [Offset(0x0036)] byte _WaveFXMColor;
        [Offset(0x0037)] byte _WaveFXMDepth;
        [Offset(0x0038)] IntegraSwitch _WaveTempoSync;
        [Offset(0x0039)] byte _WavePitchKeyFollow;

        #endregion

        #region Fields: Pitch Envelope

        [Offset(0x003A)] byte _PitchEnvDepth;
        [Offset(0x003B)] byte _PitchEnvVelocitySens;
        [Offset(0x003C)] byte _PitchEnvTime1VelocitySens;
        [Offset(0x003D)] byte _PitchEnvTime4VelocitySens;
        [Offset(0x003E)] byte _PitchEnvTimeKeyFollow;
        [Offset(0x003F)] byte _PitchEnvTime1;
        [Offset(0x0040)] byte _PitchEnvTime2;
        [Offset(0x0041)] byte _PitchEnvTime3;
        [Offset(0x0042)] byte _PitchEnvTime4;
        [Offset(0x0043)] byte _PitchEnvLevel0;
        [Offset(0x0044)] byte _PitchEnvLevel1;
        [Offset(0x0045)] byte _PitchEnvLevel2;
        [Offset(0x0046)] byte _PitchEnvLevel3;
        [Offset(0x0047)] byte _PitchEnvLevel4;

        #endregion

        #region Fields: TVF

        [Offset(0x0048)] IntegraTVFFilterType _TVFFilterType;
        [Offset(0x0049)] byte _TVFCutoffFrequency;
        [Offset(0x004A)] byte _TVFCutoffKeyFollow;
        [Offset(0x004B)] IntegraVelocityCurve _TVFCutoffVelocityCurve;
        [Offset(0x004C)] byte _TVFCutoffVelocitySens;
        [Offset(0x004D)] byte _TVFResonance;
        [Offset(0x004E)] byte _TVFResonanceVelocitySens;
        [Offset(0x004F)] byte _TVFEnvDepth;
        [Offset(0x0050)] IntegraVelocityCurve _TVFEnvVelocityCurve;
        [Offset(0x0051)] byte _TVFEnvVelocitySens;
        [Offset(0x0052)] byte _TVFEnvTime1VelocitySens;
        [Offset(0x0053)] byte _TVFEnvTime4VelocitySens;
        [Offset(0x0054)] byte _TVFEnvTimeKeyFollow;
        [Offset(0x0055)] byte _TVFTime1;
        [Offset(0x0056)] byte _TVFTime2;
        [Offset(0x0057)] byte _TVFTime3;
        [Offset(0x0058)] byte _TVFTime4;
        [Offset(0x0059)] byte _TVFLevel0;
        [Offset(0x005A)] byte _TVFLevel1;
        [Offset(0x005B)] byte _TVFLevel2;
        [Offset(0x005C)] byte _TVFLevel3;
        [Offset(0x005D)] byte _TVFLevel4;

        #endregion

        #region Fields: TVA

        [Offset(0x005E)] byte _BiasLevel;
        [Offset(0x005F)] IntegraKeyRange _BiasPosition;
        [Offset(0x0060)] IntegraBiasDirection _BiasDirection;
        [Offset(0x0061)] IntegraVelocityCurve _TVALevelVelocityCurve;
        [Offset(0x0062)] byte _TVALevelVelocitySens;
        [Offset(0x0063)] byte _TVAEnvTime1VelocitySens;
        [Offset(0x0064)] byte _TVAEnvTime4VelocitySens;
        [Offset(0x0065)] byte _TVAEnvTimeKeyFollow;
        [Offset(0x0066)] byte _TVAEnvTime1;
        [Offset(0x0067)] byte _TVAEnvTime2;
        [Offset(0x0068)] byte _TVAEnvTime3;
        [Offset(0x0069)] byte _TVAEnvTime4;
        [Offset(0x006A)] byte _TVAEnvLevel1;
        [Offset(0x006B)] byte _TVAEnvLevel2;
        [Offset(0x006C)] byte _TVAEnvLevel3;

        #endregion

        #region Fields: LFO 01

        [Offset(0x006D)] IntegraLFOWaveform _LFO1WaveForm;
        [Offset(0x006E)] byte[] _LFO1Rate = new byte[2];
        [Offset(0x0070)] byte _LFO1Offset;
        [Offset(0x0071)] byte _LFO1Detune;
        [Offset(0x0072)] byte _LFO1DelayTime;
        [Offset(0x0073)] byte _LFO1DelayTimeKeyFollow;
        [Offset(0x0074)] IntegraLFOFadeMode _LFO1FadeMode;
        [Offset(0x0075)] byte _LFO1FadeTime;
        [Offset(0x0076)] IntegraSwitch _LFO1KeyTrigger;
        [Offset(0x0077)] byte _LFO1PitchDepth;
        [Offset(0x0078)] byte _LFO1TVFDepth;
        [Offset(0x0079)] byte _LFO1TVADepth;
        [Offset(0x007A)] byte _LFO1PanDepth;

        #endregion

        #region Fields: LFO 02

        [Offset(0x007B)] IntegraLFOWaveform _LFO2WaveForm;
        [Offset(0x007C)] byte[] _LFO2Rate = new byte[2];
        [Offset(0x007E)] byte _LFO2Offset;
        [Offset(0x007F)] byte _LFO2Detune;
        [Offset(0x0100)] byte _LFO2DelayTime;
        [Offset(0x0101)] byte _LFO2DelayTimeKeyFollow;
        [Offset(0x0102)] IntegraLFOFadeMode _LFO2FadeMode;
        [Offset(0x0103)] byte _LFO2FadeTime;
        [Offset(0x0104)] IntegraSwitch _LFO2KeyTrigger;
        [Offset(0x0105)] byte _LFO2PitchDepth;
        [Offset(0x0106)] byte _LFO2TVFDepth;
        [Offset(0x0107)] byte _LFO2TVADepth;
        [Offset(0x0108)] byte _LFO2PanDepth;

        #endregion

        #region Fields: LFO Step Sequencer

        [Offset(0x0109)] IntegraStepLFOType _LFOStepType;
        [Offset(0x010A)] byte _LFOStep1;
        [Offset(0x010B)] byte _LFOStep2;
        [Offset(0x010C)] byte _LFOStep3;
        [Offset(0x010D)] byte _LFOStep4;
        [Offset(0x010E)] byte _LFOStep5;
        [Offset(0x010F)] byte _LFOStep6;
        [Offset(0x0110)] byte _LFOStep7;
        [Offset(0x0111)] byte _LFOStep8;
        [Offset(0x0112)] byte _LFOStep9;
        [Offset(0x0113)] byte _LFOStep10;
        [Offset(0x0114)] byte _LFOStep11;
        [Offset(0x0115)] byte _LFOStep12;
        [Offset(0x0116)] byte _LFOStep13;
        [Offset(0x0117)] byte _LFOStep14;
        [Offset(0x0118)] byte _LFOStep15;
        [Offset(0x0119)] byte _LFOStep16;

        #endregion

        #endregion

        #region Constructor
        
        internal PCMSynthTonePartial(PCMSynthTone tone, int partial) : base(tone.Device)
        {
            Address += tone.Address;
            Address += (partial * 2) << 8;

            Debug.Print($"{Address}");

            Index = partial;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected partial.
        /// </summary>
        public IntegraPCMSynthToneParts Partial => (IntegraPCMSynthToneParts)Index;

        /// <summary>
        /// Gets the index of the partial.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Gets the left waveform template.
        /// </summary>
        public WaveformTemplate WaveFormLeft
        {
            get
            {
                if(WaveGroupType == IntegraWaveGroupType.INT)
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WaveNumberL);
                }
                else
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, (IntegraWaveFormBanks)WaveGroupID, WaveNumberL);
                }
            }
        }
        
        /// <summary>
        /// Gets the right waveform template.
        /// </summary>
        public WaveformTemplate WaveFormRight
        {
            get
            {
                if (WaveGroupType == IntegraWaveGroupType.INT)
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WaveNumberR);
                }
                else
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, (IntegraWaveFormBanks)WaveGroupID, WaveNumberR);
                }
            }
        }

        #endregion

        #region Properties: INTEGRA-7

        #region Properties: General

        [Offset(0x0000)]
        public byte PartialLevel
        {
            get => _PartialLevel;
            set
            {
                if (_PartialLevel != value)
                {
                    _PartialLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0001)]
        public int CoarseTune
        {
            get => _CoarseTune.Deserialize(64);
            set
            {
                if (CoarseTune != value)
                {
                    _CoarseTune = value.Serialize(64).Clamp(16, 112);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0002)]
        public int FineTune
        {
            get => _FineTune.Deserialize(64);
            set
            {
                if (FineTune != value)
                {
                    _FineTune = value.Serialize(64).Clamp(14, 114);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0003)]
        public byte RandomPitchDepth
        {
            get => _RandomPitchDepth;
            set
            {
                if (_RandomPitchDepth != value)
                {
                    _RandomPitchDepth = value.Clamp(0, 30);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0004)]
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

        [Offset(0x0005)]
        public int PanKeyFollow
        {
            get => _PanKeyFollow.Deserialize(64, 10);
            set
            {
                if (PanKeyFollow != value)
                {
                    _PanKeyFollow = value.Serialize(64, 10).Clamp(54, 74);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public byte RandomPanDepth
        {
            get => _RandomPanDepth;
            set
            {
                if (_RandomPanDepth != value)
                {
                    _RandomPanDepth = value.Clamp(0, 63);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0007)]
        public int AlternatePanDepth
        {
            get => _AlternatePanDepth.Deserialize(64);
            set
            {
                if (_AlternatePanDepth != value)
                {
                    _AlternatePanDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0008)]
        public IntegraEnvelopeMode EnvMode
        {
            get => _EnvMode;
            set
            {
                if (_EnvMode != value)
                {
                    _EnvMode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0009)]
        public IntegraDelayMode DelayMode
        {
            get => _DelayMode;
            set
            {
                if (_DelayMode != value)
                {
                    _DelayMode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000A)]
        public short DelayTime
        {
            get => _DelayTime.Deserialize();
            set
            {
                if (DelayTime != value)
                {
                    _DelayTime = value.Serialize(0, 149);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public byte OutputLevel
        {
            get => _OutputLevel;
            set
            {
                if (_OutputLevel != value)
                {
                    _OutputLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000F)]
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

        [Offset(0x0010)]
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

        [Offset(0x0012)]
        public IntegraSwitch ReceiveBender
        {
            get => _ReceiveBender;
            set
            {
                if (_ReceiveBender != value)
                {
                    _ReceiveBender = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0013)]
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

        [Offset(0x0014)]
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

        [Offset(0x0016)]
        public IntegraSwitch RedamperSwitch
        {
            get => _RedamperSwitch;
            set
            {
                if (_RedamperSwitch != value)
                {
                    _RedamperSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: Matrix Control

        [Offset(0x0017)]
        public IntegraControlSwitch Control1Switch1
        {
            get => _Control1Switch1;
            set
            {
                if (_Control1Switch1 != value)
                {
                    _Control1Switch1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0018)]
        public IntegraControlSwitch Control1Switch2
        {
            get => _Control1Switch2;
            set
            {
                if (_Control1Switch2 != value)
                {
                    _Control1Switch2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0019)]
        public IntegraControlSwitch Control1Switch3
        {
            get => _Control1Switch3;
            set
            {
                if (_Control1Switch3 != value)
                {
                    _Control1Switch3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001A)]
        public IntegraControlSwitch Control1Switch4
        {
            get => _Control1Switch4;
            set
            {
                if (_Control1Switch4 != value)
                {
                    _Control1Switch4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001B)]
        public IntegraControlSwitch Control2Switch1
        {
            get => _Control2Switch1;
            set
            {
                if (_Control2Switch1 != value)
                {
                    _Control2Switch1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001C)]
        public IntegraControlSwitch Control2Switch2
        {
            get => _Control2Switch2;
            set
            {
                if (_Control2Switch2 != value)
                {
                    _Control2Switch2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001D)]
        public IntegraControlSwitch Control2Switch3
        {
            get => _Control2Switch3; 
            set
            {
                if (_Control2Switch3 != value)
                {
                    _Control2Switch3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001E)]
        public IntegraControlSwitch Control2Switch4
        {
            get => _Control2Switch4; 
            set
            {
                if (_Control2Switch4 != value)
                {
                    _Control2Switch4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001F)]
        public IntegraControlSwitch Control3Switch1
        {
            get => _Control3Switch1; 
            set
            {
                if (_Control3Switch1 != value)
                {
                    _Control3Switch1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0020)]
        public IntegraControlSwitch Control3Switch2
        {
            get => _Control3Switch2;
            set
            {
                if (_Control3Switch2 != value)
                {
                    _Control3Switch2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0021)]
        public IntegraControlSwitch Control3Switch3
        {
            get => _Control3Switch3; 
            set
            {
                if (_Control3Switch3 != value)
                {
                    _Control3Switch3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0022)]
        public IntegraControlSwitch Control3Switch4
        {
            get => _Control3Switch4; 
            set
            {
                if (_Control3Switch4 != value)
                {
                    _Control3Switch4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0023)]
        public IntegraControlSwitch Control4Switch1
        {
            get => _Control4Switch1; 
            set
            {
                if (_Control4Switch1 != value)
                {
                    _Control4Switch1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0024)]
        public IntegraControlSwitch Control4Switch2
        {
            get => _Control4Switch2; 
            set
            {
                if (_Control4Switch2 != value)
                {
                    _Control4Switch2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0025)]
        public IntegraControlSwitch Control4Switch3
        {
            get => _Control4Switch3; 
            set
            {
                if (_Control4Switch3 != value)
                {
                    _Control4Switch3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0026)]
        public IntegraControlSwitch Control4Switch4
        {
            get => _Control4Switch4; 
            set
            {
                if (_Control4Switch4 != value)
                {
                    _Control4Switch4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: Waveform

        [Offset(0x0027)]
        public IntegraWaveGroupType WaveGroupType // 0 = INT, 1 = SRX
        {
            get => _WaveGroupType;
            set
            {
                if (_WaveGroupType != value)
                {
                    _WaveGroupType = value;
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(WaveFormLeft));
                    NotifyPropertyChanged(nameof(WaveFormRight));

                    //InvalidateWaveCount();
                }
            }
        }

        [Offset(0x0028)]
        public IntegraPCMWaveGroups WaveGroupID // INT = 1, SRX01 = 1 .. SRX12 = 12
        {
            get => _WaveGroupID;
            set
            {
                if (_WaveGroupID != value)
                {
                    _WaveGroupID = value;
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(WaveFormLeft));
                    NotifyPropertyChanged(nameof(WaveFormRight));

                    //InvalidateWaveCount();
                }
            }
        }

        [Offset(0x002C)]
        public int WaveNumberL
        {
            get => _WaveNumberL.Deserialize();
            set
            {
                if (WaveNumberL != value)
                {
                    _WaveNumberL = value.Clamp(0, IntegraConstants.WAVE_COUNT_INT).SerializeInt();

                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(WaveFormLeft));
                }
            }
        }

        [Offset(0x0030)]
        public int WaveNumberR
        {
            get => _WaveNumberR.Deserialize();
            set
            {
                if (WaveNumberR != value)
                {
                    _WaveNumberR = value.Clamp(0, IntegraConstants.WAVE_COUNT_INT).SerializeInt();

                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(WaveFormRight));
                }
            }
        }

        [Offset(0x0034)]
        public int WaveGain
        {
            get => _WaveGain.Deserialize(1, 6);
            set
            {
                if (WaveGain != value)
                {
                    _WaveGain = value.Serialize(1, 6).Clamp(0, 3);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0035)]
        public IntegraSwitch WaveFXMSwitch
        {
            get => _WaveFXMSwitch;
            set
            {
                if (_WaveFXMSwitch != value)
                {
                    _WaveFXMSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0036)]
        public int WaveFXMColor
        {
            get => _WaveFXMColor.Deserialize(-1);
            
            set
            {
                if (WaveFXMColor != value)
                {
                    _WaveFXMColor = value.Serialize(-1).Clamp(0, 3);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0037)]
        public byte WaveFXMDepth
        {
            get => _WaveFXMDepth;
            set
            {
                if (_WaveFXMDepth != value)
                {
                    _WaveFXMDepth = value.Clamp(0, 16);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0038)]
        public IntegraSwitch WaveTempoSync
        {
            get => _WaveTempoSync;
            set
            {
                if (_WaveTempoSync != value)
                {
                    _WaveTempoSync = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0039)]
        public int WavePitchKeyFollow
        {
            get => _WavePitchKeyFollow.Deserialize(64, 10);
            set
            {
                if (WavePitchKeyFollow != value)
                {
                    _WavePitchKeyFollow = value.Serialize(64, 10).Clamp(44, 84);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: Pitch Envelope

        [Offset(0x003A)]
        public int PitchEnvDepth
        {
            get => _PitchEnvDepth.Deserialize(64);
            set
            {
                if (PitchEnvDepth != value)
                {
                    _PitchEnvDepth = value.Clamp(-12, 12).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003B)]
        public int PitchEnvVelocitySens
        {
            get => _PitchEnvVelocitySens.Deserialize(64);
            set
            {
                if (PitchEnvVelocitySens != value)
                {
                    _PitchEnvVelocitySens = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003C)]
        public int PitchEnvTime1VelocitySens
        {
            get => _PitchEnvTime1VelocitySens.Deserialize(64);
            set
            {
                if (PitchEnvTime1VelocitySens != value)
                {
                    _PitchEnvTime1VelocitySens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003D)]
        public int PitchEnvTime4VelocitySens
        {
            get => _PitchEnvTime4VelocitySens.Deserialize(64);
            set
            {
                if (PitchEnvTime4VelocitySens != value)
                {
                    _PitchEnvTime4VelocitySens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003E)]
        public int PitchEnvTimeKeyFollow
        {
            get => _PitchEnvTimeKeyFollow.Deserialize(64, 10);
            set
            {
                if (PitchEnvTimeKeyFollow != value)
                {
                    _PitchEnvTimeKeyFollow = value.Serialize(64, 10).Clamp(54, 74);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003F)]
        public byte PitchEnvTime1
        {
            get => _PitchEnvTime1;
            set
            {
                if (_PitchEnvTime1 != value)
                {
                    _PitchEnvTime1 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0040)]
        public byte PitchEnvTime2
        {
            get => _PitchEnvTime2;
            set
            {
                if (_PitchEnvTime2 != value)
                {
                    _PitchEnvTime2 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0041)]
        public byte PitchEnvTime3
        {
            get => _PitchEnvTime3;
            set
            {
                if (_PitchEnvTime3 != value)
                {
                    _PitchEnvTime3 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0042)]
        public byte PitchEnvTime4
        {
            get => _PitchEnvTime4;
            set
            {
                if (_PitchEnvTime4 != value)
                {
                    _PitchEnvTime4 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0043)]
        public int PitchEnvLevel0
        {
            get => _PitchEnvLevel0.Deserialize(64);
            set
            {
                if (PitchEnvLevel0 != value)
                {
                    _PitchEnvLevel0 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0044)]
        public int PitchEnvLevel1
        {
            get => _PitchEnvLevel1.Deserialize(64);
            set
            {
                if (PitchEnvLevel1 != value)
                {
                    _PitchEnvLevel1 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0045)]
        public int PitchEnvLevel2
        {
            get => _PitchEnvLevel2.Deserialize(64);
            set
            {
                if (PitchEnvLevel2 != value)
                {
                    _PitchEnvLevel2 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0046)]
        public int PitchEnvLevel3
        {
            get => _PitchEnvLevel3.Deserialize(64);
            set
            {
                if (PitchEnvLevel3 != value)
                {
                    _PitchEnvLevel3 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0047)]
        public int PitchEnvLevel4
        {
            get => _PitchEnvLevel4.Deserialize(64);
            set
            {
                if (PitchEnvLevel4 != value)
                {
                    _PitchEnvLevel4 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: TVF

        [Offset(0x0048)]
        public IntegraTVFFilterType TVFFilterType
        {
            get { return _TVFFilterType; }
            set
            {
                if (_TVFFilterType != value)
                {
                    _TVFFilterType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0049)]
        public byte TVFCutoffFrequency
        {
            get { return _TVFCutoffFrequency; }
            set
            {
                if (_TVFCutoffFrequency != value)
                {
                    _TVFCutoffFrequency = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004A)]
        public int TVFCutoffKeyFollow
        {
            get { return _TVFCutoffKeyFollow.Deserialize(64, 10); }
            set
            {
                if (TVFCutoffKeyFollow != value)
                {
                    _TVFCutoffKeyFollow = value.Serialize(64, 10).Clamp(44, 84);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004B)]
        public IntegraVelocityCurve TVFCutoffVelocityCurve
        {
            get { return _TVFCutoffVelocityCurve; }
            set
            {
                if (_TVFCutoffVelocityCurve != value)
                {
                    _TVFCutoffVelocityCurve = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004C)]
        public int TVFCutoffVelocitySens
        {
            get { return _TVFCutoffVelocitySens.Deserialize(64); }
            set
            {
                if (TVFCutoffVelocitySens != value)
                {
                    _TVFCutoffVelocitySens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004D)]
        public byte TVFResonance
        {
            get { return _TVFResonance; }
            set
            {
                if (_TVFResonance != value)
                {
                    _TVFResonance = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004E)]
        public int TVFResonanceVelocitySens
        {
            get { return _TVFResonanceVelocitySens.Deserialize(64); }
            set
            {
                if (TVFResonanceVelocitySens != value)
                {
                    _TVFResonanceVelocitySens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region TVF Envelope

        [Offset(0x004F)]
        public int TVFEnvDepth
        {
            get { return _TVFEnvDepth.Deserialize(64); }
            set
            {
                if (TVFEnvDepth != value)
                {
                    _TVFEnvDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0050)]
        public IntegraVelocityCurve TVFEnvVelocityCurve
        {
            get { return _TVFEnvVelocityCurve; }
            set
            {
                if (TVFEnvVelocityCurve != value)
                {
                    _TVFEnvVelocityCurve = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0051)]
        public int TVFEnvVelocitySens
        {
            get { return _TVFEnvVelocitySens.Deserialize(64); }
            set
            {
                if (TVFEnvVelocitySens != value)
                {
                    _TVFEnvVelocitySens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0052)]
        public int TVFEnvTime1VelocitySens
        {
            get { return _TVFEnvTime1VelocitySens.Deserialize(64); }
            set
            {
                if (TVFEnvTime1VelocitySens != value)
                {
                    _TVFEnvTime1VelocitySens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0053)]
        public int TVFEnvTime4VelocitySens
        {
            get { return _TVFEnvTime4VelocitySens.Deserialize(64); }
            set
            {
                if (TVFEnvTime4VelocitySens != value)
                {
                    _TVFEnvTime4VelocitySens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0054)]
        public int TVFEnvTimeKeyFollow
        {
            get { return _TVFEnvTimeKeyFollow.Deserialize(64, 10); }
            set
            {
                if (TVFEnvTimeKeyFollow != value)
                {
                    _TVFEnvTimeKeyFollow = value.Serialize(64, 10).Clamp(54, 74);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0055)]
        public byte TVFTime1
        {
            get { return _TVFTime1; }
            set
            {
                if (_TVFTime1 != value)
                {
                    _TVFTime1 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0056)]
        public byte TVFTime2
        {
            get { return _TVFTime2; }
            set
            {
                if (_TVFTime2 != value)
                {
                    _TVFTime2 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0057)]
        public byte TVFTime3
        {
            get { return _TVFTime3; }
            set
            {
                if (_TVFTime3 != value)
                {
                    _TVFTime3 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0058)]
        public byte TVFTime4
        {
            get { return _TVFTime4; }
            set
            {
                if (_TVFTime4 != value)
                {
                    _TVFTime4 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0059)]
        public byte TVFLevel0
        {
            get { return _TVFLevel0; }
            set
            {
                if (_TVFLevel0 != value)
                {
                    _TVFLevel0 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x005A)]
        public byte TVFLevel1
        {
            get { return _TVFLevel1; }
            set
            {
                if (_TVFLevel1 != value)
                {
                    _TVFLevel1 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x005B)]
        public byte TVFLevel2
        {
            get { return _TVFLevel2; }
            set
            {
                if (_TVFLevel2 != value)
                {
                    _TVFLevel2 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x005C)]
        public byte TVFLevel3
        {
            get { return _TVFLevel3; }
            set
            {
                if (_TVFLevel3 != value)
                {
                    _TVFLevel3 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x005D)]
        public byte TVFLevel4
        {
            get { return _TVFLevel4; }
            set
            {
                if (_TVFLevel4 != value)
                {
                    _TVFLevel4 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: TVA

        [Offset(0x005E)]
        public int BiasLevel
        {
            get { return _BiasLevel.Deserialize(64, 10); }
            set
            {
                if (BiasLevel != value)
                {
                    _BiasLevel = value.Serialize(64, 10).Clamp(54, 74);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x005F)]
        public IntegraKeyRange BiasPosition
        {
            get { return _BiasPosition; }
            set
            {
                if (_BiasPosition != value)
                {
                    _BiasPosition = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0060)]
        public IntegraBiasDirection BiasDirection
        {
            get { return _BiasDirection; }
            set
            {
                if (_BiasDirection != value)
                {
                    _BiasDirection = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0061)]
        public IntegraVelocityCurve TVALevelVelocityCurve
        {
            get { return _TVALevelVelocityCurve; }
            set
            {
                if (_TVALevelVelocityCurve != value)
                {
                    _TVALevelVelocityCurve = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0062)]
        public int TVALevelVelocitySens
        {
            get { return _TVALevelVelocitySens.Deserialize(64); }
            set
            {
                if (TVALevelVelocitySens != value)
                {
                    _TVALevelVelocitySens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: TVA ENVELOPE

        [Offset(0x0063)]
        public int TVAEnvTime1VelocitySens
        {
            get { return _TVAEnvTime1VelocitySens.Deserialize(64); }
            set
            {
                if (TVAEnvTime1VelocitySens != value)
                {
                    _TVAEnvTime1VelocitySens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0064)]
        public int TVAEnvTime4VelocitySens
        {
            get { return _TVAEnvTime4VelocitySens.Deserialize(64); }
            set
            {
                if (TVAEnvTime4VelocitySens != value)
                {
                    _TVAEnvTime4VelocitySens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0065)]
        public int TVAEnvTimeKeyFollow
        {
            get { return _TVAEnvTimeKeyFollow.Deserialize(64, 10); }
            set
            {
                if (TVAEnvTimeKeyFollow != value)
                {
                    _TVAEnvTimeKeyFollow = value.Serialize(64, 10).Clamp(54, 74);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0066)]
        public byte TVAEnvTime1
        {
            get { return _TVAEnvTime1; }
            set
            {
                if (_TVAEnvTime1 != value)
                {
                    _TVAEnvTime1 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0067)]
        public byte TVAEnvTime2
        {
            get { return _TVAEnvTime2; }
            set
            {
                if (_TVAEnvTime2 != value)
                {
                    _TVAEnvTime2 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0068)]
        public byte TVAEnvTime3
        {
            get { return _TVAEnvTime3; }
            set
            {
                if (_TVAEnvTime3 != value)
                {
                    _TVAEnvTime3 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0069)]
        public byte TVAEnvTime4
        {
            get { return _TVAEnvTime4; }
            set
            {
                if (_TVAEnvTime4 != value)
                {
                    _TVAEnvTime4 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x006A)]
        public byte TVAEnvLevel1
        {
            get { return _TVAEnvLevel1; }
            set
            {
                if (_TVAEnvLevel1 != value)
                {
                    _TVAEnvLevel1 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x006B)]
        public byte TVAEnvLevel2
        {
            get { return _TVAEnvLevel2; }
            set
            {
                if (_TVAEnvLevel2 != value)
                {
                    _TVAEnvLevel2 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x006C)]
        public byte TVAEnvLevel3
        {
            get { return _TVAEnvLevel3; }
            set
            {
                if (_TVAEnvLevel3 != value)
                {
                    _TVAEnvLevel3 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: LFO 1

        [Offset(0x006D)]
        public IntegraLFOWaveform LFO1WaveForm
        {
            get { return _LFO1WaveForm; }
            set
            {
                if (_LFO1WaveForm != value)
                {
                    _LFO1WaveForm = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x006E)]
        public short LFO1Rate
        {
            get { return _LFO1Rate.Deserialize(); }
            set
            {
                if (LFO1Rate != value)
                {
                    _LFO1Rate = value.Serialize(0, 149);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0070)]
        public int LFO1Offset
        {
            get { return _LFO1Offset.Deserialize(2, 50); }
            set
            {
                if (LFO1Offset != value)
                {
                    _LFO1Offset = value.Serialize(2, 50);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0071)]
        public byte LFO1Detune
        {
            get { return _LFO1Detune; }
            set
            {
                if (_LFO1Detune != value)
                {
                    _LFO1Detune = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0072)]
        public byte LFO1DelayTime
        {
            get { return _LFO1DelayTime; }
            set
            {
                if (_LFO1DelayTime != value)
                {
                    _LFO1DelayTime = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0073)]
        public int LFO1DelayTimeKeyFollow
        {
            get { return _LFO1DelayTimeKeyFollow.Deserialize(64, 10); }
            set
            {
                if (LFO1DelayTimeKeyFollow != value)
                {
                    _LFO1DelayTimeKeyFollow = value.Serialize(64, 10);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0074)]
        public IntegraLFOFadeMode LFO1FadeMode
        {
            get { return _LFO1FadeMode; }
            set
            {
                if (_LFO1FadeMode != value)
                {
                    _LFO1FadeMode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0075)]
        public byte LFO1FadeTime
        {
            get { return _LFO1FadeTime; }
            set
            {
                if (_LFO1FadeTime != value)
                {
                    _LFO1FadeTime = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0076)]
        public IntegraSwitch LFO1KeyTrigger
        {
            get { return _LFO1KeyTrigger; }
            set
            {
                if (_LFO1KeyTrigger != value)
                {
                    _LFO1KeyTrigger = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0077)]
        public int LFO1PitchDepth
        {
            get { return _LFO1PitchDepth.Deserialize(64); }
            set
            {
                if (LFO1PitchDepth != value)
                {
                    _LFO1PitchDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0078)]
        public int LFO1TVFDepth
        {
            get { return _LFO1TVFDepth.Deserialize(64); }
            set
            {
                if (LFO1TVFDepth != value)
                {
                    _LFO1TVFDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0079)]
        public int LFO1TVADepth
        {
            get { return _LFO1TVADepth.Deserialize(64); }
            set
            {
                if (LFO1TVADepth != value)
                {
                    _LFO1TVADepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x007A)]
        public int LFO1PanDepth
        {
            get { return _LFO1PanDepth.Deserialize(64); }
            set
            {
                if (LFO1PanDepth != value)
                {
                    _LFO1PanDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: LFO 2

        [Offset(0x007B)]
        public IntegraLFOWaveform LFO2WaveForm
        {
            get { return _LFO2WaveForm; }
            set
            {
                if (_LFO2WaveForm != value)
                {
                    _LFO2WaveForm = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x007C)]
        public short LFO2Rate
        {
            get { return _LFO2Rate.Deserialize(); }
            set
            {
                if (LFO2Rate != value)
                {
                    _LFO2Rate = value.Serialize(0, 149);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x007E)]
        public int LFO2Offset
        {
            get { return _LFO2Offset.Deserialize(2, 50); }
            set
            {
                if (LFO2Offset != value)
                {
                    _LFO2Offset = value.Serialize(2, 50);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x007F)]
        public byte LFO2Detune
        {
            get { return _LFO2Detune; }
            set
            {
                if (_LFO2Detune != value)
                {
                    _LFO2Detune = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0100)]
        public byte LFO2DelayTime
        {
            get { return _LFO2DelayTime; }
            set
            {
                if (_LFO2DelayTime != value)
                {
                    _LFO2DelayTime = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0101)]
        public int LFO2DelayTimeKeyFollow
        {
            get { return _LFO2DelayTimeKeyFollow.Deserialize(64, 10); }
            set
            {
                if (LFO2DelayTimeKeyFollow != value)
                {
                    _LFO2DelayTimeKeyFollow = value.Serialize(64, 10).Clamp(54, 74);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0102)]
        public IntegraLFOFadeMode LFO2FadeMode
        {
            get { return _LFO2FadeMode; }
            set
            {
                if (_LFO2FadeMode != value)
                {
                    _LFO2FadeMode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0103)]
        public byte LFO2FadeTime
        {
            get { return _LFO2FadeTime; }
            set
            {
                if (_LFO2FadeTime != value)
                {
                    _LFO2FadeTime = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0104)]
        public IntegraSwitch LFO2KeyTrigger
        {
            get { return _LFO2KeyTrigger; }
            set
            {
                if (_LFO2KeyTrigger != value)
                {
                    _LFO2KeyTrigger = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0105)]
        public int LFO2PitchDepth
        {
            get { return _LFO2PitchDepth.Deserialize(64); }
            set
            {
                if (LFO2PitchDepth != value)
                {
                    _LFO2PitchDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0106)]
        public int LFO2TVFDepth
        {
            get { return _LFO2TVFDepth.Deserialize(64); }
            set
            {
                if (LFO2TVFDepth != value)
                {
                    _LFO2TVFDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0107)]
        public int LFO2TVADepth
        {
            get { return _LFO2TVADepth.Deserialize(64); }
            set
            {
                if (LFO2TVADepth != value)
                {
                    _LFO2TVADepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0108)]
        public int LFO2PanDepth
        {
            get { return _LFO2PanDepth.Deserialize(64); }
            set
            {
                if (LFO2PanDepth != value)
                {
                    _LFO2PanDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: LFO Step Sequencer

        [Offset(0x0109)]
        public IntegraStepLFOType LFOStepType
        {
            get { return _LFOStepType; }
            set
            {
                if (_LFOStepType != value)
                {
                    _LFOStepType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x010A)]
        public int LFOStep1
        {
            get { return _LFOStep1.Deserialize(64); }
            set
            {
                if (_LFOStep1 != value)
                {
                    _LFOStep1 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x010B)]
        public int LFOStep2
        {
            get { return _LFOStep2.Deserialize(64); }
            set
            {
                if (_LFOStep2 != value)
                {
                    _LFOStep2 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x010C)]
        public int LFOStep3
        {
            get { return _LFOStep3.Deserialize(64); }
            set
            {
                if (_LFOStep3 != value)
                {
                    _LFOStep3 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x010D)]
        public int LFOStep4
        {
            get { return _LFOStep4.Deserialize(64); }
            set
            {
                if (_LFOStep4 != value)
                {
                    _LFOStep4 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x010E)]
        public int LFOStep5
        {
            get { return _LFOStep5.Deserialize(64); }
            set
            {
                if (_LFOStep5 != value)
                {
                    _LFOStep5 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x010F)]
        public int LFOStep6
        {
            get { return _LFOStep6.Deserialize(64); }
            set
            {
                if (_LFOStep6 != value)
                {
                    _LFOStep6 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0110)]
        public int LFOStep7
        {
            get { return _LFOStep7.Deserialize(64); }
            set
            {
                if (_LFOStep7 != value)
                {
                    _LFOStep7 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0111)]
        public int LFOStep8
        {
            get { return _LFOStep8.Deserialize(64); }
            set
            {
                if (_LFOStep8 != value)
                {
                    _LFOStep8 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0112)]
        public int LFOStep9
        {
            get { return _LFOStep9.Deserialize(64); }
            set
            {
                if (_LFOStep9 != value)
                {
                    _LFOStep9 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0113)]
        public int LFOStep10
        {
            get { return _LFOStep10.Deserialize(64); }
            set
            {
                if (_LFOStep10 != value)
                {
                    _LFOStep10 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0114)]
        public int LFOStep11
        {
            get { return _LFOStep11.Deserialize(64); }
            set
            {
                if (_LFOStep11 != value)
                {
                    _LFOStep11 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0115)]
        public int LFOStep12
        {
            get { return _LFOStep12.Deserialize(64); }
            set
            {
                if (_LFOStep12 != value)
                {
                    _LFOStep12 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0116)]
        public int LFOStep13
        {
            get { return _LFOStep13.Deserialize(64); }
            set
            {
                if (_LFOStep13 != value)
                {
                    _LFOStep13 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0117)]
        public int LFOStep14
        {
            get { return _LFOStep14.Deserialize(64); }
            set
            {
                if (_LFOStep14 != value)
                {
                    _LFOStep14 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0118)]
        public int LFOStep15
        {
            get { return _LFOStep15.Deserialize(64); }
            set
            {
                if (_LFOStep15 != value)
                {
                    _LFOStep15 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0119)]
        public int LFOStep16
        {
            get { return _LFOStep16.Deserialize(64); }
            set
            {
                if (_LFOStep16 != value)
                {
                    _LFOStep16 = value.Serialize(64).Clamp(28, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #endregion

        #region Methods

        private void InvalidateWaveCount()
        {
            //if(WaveGroupType == IntegraWaveGroupType.INT)
            //{
            //    WaveCount = IntegraConstants.WAVE_COUNT_INT;
            //}
            //else
            //{
            //    switch((IntegraWaveFormBanks)WaveGroupID)
            //    {
            //        case IntegraWaveFormBanks.SRX01: WaveCount = IntegraConstants.WAVE_COUNT_SRX01; break;
            //        case IntegraWaveFormBanks.SRX02: WaveCount = IntegraConstants.WAVE_COUNT_SRX02; break;
            //        case IntegraWaveFormBanks.SRX03: WaveCount = IntegraConstants.WAVE_COUNT_SRX03; break;
            //        case IntegraWaveFormBanks.SRX04: WaveCount = IntegraConstants.WAVE_COUNT_SRX04; break;
            //        case IntegraWaveFormBanks.SRX05: WaveCount = IntegraConstants.WAVE_COUNT_SRX05; break;
            //        case IntegraWaveFormBanks.SRX06: WaveCount = IntegraConstants.WAVE_COUNT_SRX06; break;
            //        case IntegraWaveFormBanks.SRX07: WaveCount = IntegraConstants.WAVE_COUNT_SRX07; break;
            //        case IntegraWaveFormBanks.SRX08: WaveCount = IntegraConstants.WAVE_COUNT_SRX08; break;
            //        case IntegraWaveFormBanks.SRX09: WaveCount = IntegraConstants.WAVE_COUNT_SRX09; break;
            //        case IntegraWaveFormBanks.SRX10: WaveCount = IntegraConstants.WAVE_COUNT_SRX10; break;
            //        case IntegraWaveFormBanks.SRX11: WaveCount = IntegraConstants.WAVE_COUNT_SRX11; break;
            //        case IntegraWaveFormBanks.SRX12: WaveCount = IntegraConstants.WAVE_COUNT_SRX12; break;
            //    }
            //}

            NotifyPropertyChanged(nameof(WaveFormLeft));
            NotifyPropertyChanged(nameof(WaveFormRight));

            // CLAMP WAVE NUMBERS TO THE WAVE COUNT
            //if (WaveNumberL > WaveCount)
            //    WaveNumberL = WaveCount;
            //if (WaveNumberR > WaveCount)
            //    WaveNumberR = WaveCount;
        }

        #endregion

        #region Overrides: Model

        /// <summary>
        /// Gets wheter the model is initialized.
        /// </summary>
        public override bool IsInitialized
        {
            get => base.IsInitialized;
            protected internal set
            {
                base.IsInitialized = value;

                InvalidateWaveCount();
            }
        }

        #endregion

        #region Enumerations

        public List<string> PanValues
        {
            get { return IntegraPan.Values; }
        }

        public List<string> Rates
        {
            get { return IntegraExtendedRate.Values; }
        }

        public List<string> PitchDepths
        {
            get { return IntegraPitchDepths.Values; }
        }

        #endregion

    }
}