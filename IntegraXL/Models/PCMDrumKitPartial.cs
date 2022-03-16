using IntegraXL.Core;
using System.Text;
using IntegraXL.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Diagnostics;
using IntegraXL.Templates;
using System.ComponentModel;

namespace IntegraXL.Models
{

    [Integra(0x00001000, 0x00014000, 88)]
    public class PCMDrumKitPartials : IntegraCollection<PCMDrumKitPartial>
    {
        #region Constructor

        /// <summary>
        /// Creates a new uninitialized MIDI enabled partial collection.
        /// </summary>
        /// <param name="drumKit">The drum kit providing the device to initialize the collection.</param>
        internal PCMDrumKitPartials(PCMDrumKit drumKit) : base(drumKit.Device, false)
        {
            Address += drumKit.Address;

            IntegraRequest request = new (Attribute.Request);

            Requests.Add(request);

            for (int i = 0; i < Size; i++)
            {
                PCMDrumKitPartial? partial = Activator.CreateInstance(typeof(PCMDrumKitPartial), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { drumKit, i }, null) as PCMDrumKitPartial;

                Debug.Assert(partial != null);

                Add(partial);
            }

            Connect();
        }

        #endregion

        public override bool IsInitialized
        {
            get => this.Last().IsInitialized;
        }

        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            if(e.SystemExclusive.Address.InRange(this.First().Address, this.Last().Address))
            {
                Device.ReportProgress(this, Collection.Where(x => x.IsInitialized).Count(), Size, e.SystemExclusive.Address.GetTemporaryTonePart());
            }
        }

        internal override bool Initialize(byte[] data)
        {
            throw new NotImplementedException();
        }
    }


    [Integra(0x00001000, 0x00000143)]
    public class PCMDrumKitPartial : IntegraModel<PCMDrumKitPartial>
    {

        #region Fields: INTEGRA-7

        #region Fields: General

        [Offset(0x0000)] byte[] _PartialName = new byte[12];
        [Offset(0x000C)] IntegraAssignType _AssignType;
        [Offset(0x000D)] IntegraMuteGroup _MuteGroup;
        [Offset(0x000E)] byte _Level;
        [Offset(0x000F)] IntegraKeyRange _CoarseTune;
        [Offset(0x0010)] byte _FineTune;
        [Offset(0x0011)] byte _RandomPitchDepth;
        [Offset(0x0012)] byte _Pan;
        [Offset(0x0013)] byte _RandomPanDepth;
        [Offset(0x0014)] byte _AlternatePanDepth;
        [Offset(0x0015)] IntegraEnvelopeMode _EnvMode;
        [Offset(0x0016)] byte _OutputLevel;
        [Offset(0x0017)] byte[] _RESERVED01 = new byte[2];
        [Offset(0x0019)] byte _ChorusSendLevel;
        [Offset(0x001A)] byte _ReverbSendLevel;
        [Offset(0x001B)] IntegraPartialOutputAssign _OutputAssign;
        [Offset(0x001C)] byte _PitchBendRange;
        [Offset(0x001D)] IntegraSwitch _ReceiveExpression;
        [Offset(0x001E)] IntegraSwitch _ReceiveHold;
        [Offset(0x001F)] byte _RESERVED02;
        [Offset(0x0020)] IntegraPartialVelocityControl _WMTVelocityControl;

        #endregion

        #region Fields: WMT 01

        [Offset(0x0021)] IntegraSwitch _WMT1Switch;
        [Offset(0x0022)] IntegraWaveGroupType _WMT1WaveGroupType;
        [Offset(0x0023)] IntegraPCMWaveGroups _WMT1WaveGroupID;
        [Offset(0x0027)] int _WMT1WaveLeft;
        [Offset(0x002B)] int _WMT1WaveRight;
        [Offset(0x002F)] byte _WMT1Gain;
        [Offset(0x0030)] IntegraSwitch _WMT1FXMSwitch;
        [Offset(0x0031)] byte _WMT1FXMColor;
        [Offset(0x0032)] byte _WMT1FXMDepth;
        [Offset(0x0033)] IntegraSwitch _WMT1TempoSync;
        [Offset(0x0034)] byte _WMT1CoarseTune;
        [Offset(0x0035)] byte _WMT1FineTune;
        [Offset(0x0036)] byte _WMT1Pan;
        [Offset(0x0037)] IntegraSwitch _WMT1RandomPanSwitch;
        [Offset(0x0038)] IntegraControlSwitch _WMT1AlternatePanSwitch;
        [Offset(0x0039)] byte _WMT1Level;
        [Offset(0x003A)] byte _WMT1VelocityRangeLower;
        [Offset(0x003B)] byte _WMT1VelocityRangeUpper;
        [Offset(0x003C)] byte _WMT1VelocityFadeLower;
        [Offset(0x003D)] byte _WMT1VelocityFadeUpper;

        #endregion

        #region Fields: WMT 02

        [Offset(0x003E)] IntegraSwitch _WMT2Switch;
        [Offset(0x003F)] IntegraWaveGroupType _WMT2WaveGroupType;
        [Offset(0x0040)] IntegraPCMWaveGroups _WMT2WaveGroupID;
        [Offset(0x0044)] int _WMT2WaveLeft;
        [Offset(0x0048)] int _WMT2WaveRight;
        [Offset(0x004C)] byte _WMT2Gain;
        [Offset(0x004D)] IntegraSwitch _WMT2FXMSwitch;
        [Offset(0x004E)] byte _WMT2FXMColor;
        [Offset(0x004F)] byte _WMT2FXMDepth;
        [Offset(0x0050)] IntegraSwitch _WMT2TempoSync;
        [Offset(0x0051)] byte _WMT2CoarseTune;
        [Offset(0x0052)] byte _WMT2FineTune;
        [Offset(0x0053)] byte _WMT2Pan;
        [Offset(0x0054)] IntegraSwitch _WMT2RandomPanSwitch;
        [Offset(0x0055)] IntegraControlSwitch _WMT2AlternatePanSwitch;
        [Offset(0x0056)] byte _WMT2Level;
        [Offset(0x0057)] byte _WMT2VelocityRangeLower;
        [Offset(0x0058)] byte _WMT2VelocityRangeUpper;
        [Offset(0x0059)] byte _WMT2VelocityFadeLower;
        [Offset(0x005A)] byte _WMT2VelocityFadeUpper;

        #endregion

        #region Fields: WMT 03

        [Offset(0x005B)] IntegraSwitch _WMT3Switch;
        [Offset(0x005C)] IntegraWaveGroupType _WMT3WaveGroupType;
        [Offset(0x005D)] IntegraPCMWaveGroups _WMT3WaveGroupID;
        [Offset(0x0061)] int _WMT3WaveLeft;
        [Offset(0x0065)] int _WMT3WaveRight;
        [Offset(0x0069)] byte _WMT3Gain;
        [Offset(0x006A)] IntegraSwitch _WMT3FXMSwitch;
        [Offset(0x006B)] byte _WMT3FXMColor;
        [Offset(0x006C)] byte _WMT3FXMDepth;
        [Offset(0x006D)] IntegraSwitch _WMT3TempoSync;
        [Offset(0x006E)] byte _WMT3CoarseTune;
        [Offset(0x006F)] byte _WMT3FineTune;
        [Offset(0x0070)] byte _WMT3Pan;
        [Offset(0x0071)] IntegraSwitch _WMT3RandomPanSwitch;
        [Offset(0x0072)] IntegraControlSwitch _WMT3AlternatePanSwitch;
        [Offset(0x0073)] byte _WMT3Level;
        [Offset(0x0074)] byte _WMT3VelocityRangeLower;
        [Offset(0x0075)] byte _WMT3VelocityRangeUpper;
        [Offset(0x0076)] byte _WMT3VelocityFadeLower;
        [Offset(0x0077)] byte _WMT3VelocityFadeUpper;

        #endregion

        #region Fields: WMT 04

        [Offset(0x0078)] IntegraSwitch _WMT4Switch;
        [Offset(0x0079)] IntegraWaveGroupType _WMT4WaveGroupType;
        [Offset(0x007A)] IntegraPCMWaveGroups _WMT4WaveGroupID;
        [Offset(0x007E)] int _WMT4WaveLeft;
        [Offset(0x0102)] int _WMT4WaveRight;
        [Offset(0x0106)] byte _WMT4Gain;
        [Offset(0x0107)] IntegraSwitch _WMT4FXMSwitch;
        [Offset(0x0108)] byte _WMT4FXMColor;
        [Offset(0x0109)] byte _WMT4FXMDepth;
        [Offset(0x010A)] IntegraSwitch _WMT4TempoSync;
        [Offset(0x010B)] byte _WMT4CoarseTune;
        [Offset(0x010C)] byte _WMT4FineTune;
        [Offset(0x010D)] byte _WMT4Pan;
        [Offset(0x010E)] IntegraSwitch _WMT4RandomPanSwitch;
        [Offset(0x010F)] IntegraControlSwitch _WMT4AlternatePanSwitch;
        [Offset(0x0110)] byte _WMT4Level;
        [Offset(0x0111)] byte _WMT4VelocityRangeLower;
        [Offset(0x0112)] byte _WMT4VelocityRangeUpper;
        [Offset(0x0113)] byte _WMT4VelocityFadeLower;
        [Offset(0x0114)] byte _WMT4VelocityFadeUpper;

        #endregion

        #region Fields: Pitch Envelope

        [Offset(0x0115)] byte _PitchEnvDepth;
        [Offset(0x0116)] byte _PitchEnvVelocitySens;
        [Offset(0x0117)] byte _PitchEnvTime1VelocitySens;
        [Offset(0x0118)] byte _PitchEnvTime4VelocitySens;
        [Offset(0x0119)] byte _PitchEnvTime1;
        [Offset(0x011A)] byte _PitchEnvTime2;
        [Offset(0x011B)] byte _PitchEnvTime3;
        [Offset(0x011C)] byte _PitchEnvTime4;
        [Offset(0x011D)] byte _PitchEnvLevel0;
        [Offset(0x011E)] byte _PitchEnvLevel1;
        [Offset(0x011F)] byte _PitchEnvLevel2;
        [Offset(0x0120)] byte _PitchEnvLevel3;
        [Offset(0x0121)] byte _PitchEnvLevel4;

        #endregion

        #region Fields: TVF

        [Offset(0x0122)] IntegraTVFFilterType _TVFFilterType;
        [Offset(0x0123)] byte _TVFCutoffFrequency;
        [Offset(0x0124)] IntegraVelocityCurve _TVFCutoffVelocityCurve;
        [Offset(0x0125)] byte _TVFCutoffVelocitySens;
        [Offset(0x0126)] byte _TVFResonance;
        [Offset(0x0127)] byte _TVFResonanceVelocitySens;
        [Offset(0x0128)] byte _TVFEnvDepth;
        [Offset(0x0129)] IntegraVelocityCurve _TVFEnvVelocityCurve;
        [Offset(0x012A)] byte _TVFEnvVelocitySens;
        [Offset(0x012B)] byte _TVFEnvTime1VelocitySens;
        [Offset(0x012C)] byte _TVFEnvTime4VelocitySens;
        [Offset(0x012D)] byte _TVFEnvTime1;
        [Offset(0x012E)] byte _TVFEnvTime2;
        [Offset(0x012F)] byte _TVFEnvTime3;
        [Offset(0x0130)] byte _TVFEnvTime4;
        [Offset(0x0131)] byte _TVFEnvLevel0;
        [Offset(0x0132)] byte _TVFEnvLevel1;
        [Offset(0x0133)] byte _TVFEnvLevel2;
        [Offset(0x0134)] byte _TVFEnvLevel3;
        [Offset(0x0135)] byte _TVFEnvLevel4;

        #endregion

        #region Fields: TVA

        [Offset(0x0136)] IntegraVelocityCurve _TVALevelVelocityCurve;
        [Offset(0x0137)] byte _TVALevelVelocitySens;
        [Offset(0x0138)] byte _TVAEnvTime1VelocitySens;
        [Offset(0x0139)] byte _TVAEnvTime4VelocitySens;
        [Offset(0x013A)] byte _TVAEnvTime1;
        [Offset(0x013B)] byte _TVAEnvTime2;
        [Offset(0x013C)] byte _TVAEnvTime3;
        [Offset(0x013D)] byte _TVAEnvTime4;
        [Offset(0x013E)] byte _TVAEnvLevel1;
        [Offset(0x013F)] byte _TVAEnvLevel2;
        [Offset(0x0140)] byte _TVAEnvLevel3;

        #endregion

        #region Fields: Misc

        [Offset(0x0141)] IntegraSwitch _OneShotMode;
        [Offset(0x0142)] byte _RESERVED03;

        #endregion

        #endregion

        #region Constructor

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Class is created by reflection.")]
        private PCMDrumKitPartial(PCMDrumKit drumKit, int partial) : base(drumKit.Device)
        {
            Address += drumKit.Address;

            // TODO: Clean
            int lsb = (partial % 8) * 2;
            int msb = (partial / 8) << 4;

            int offset = (msb + lsb) > 0x7F ? (msb + lsb) - 0x80 : (msb + lsb);
            offset = offset << 8;
            int overflow = (partial / 64) << 16;
            int addr = overflow + offset;

            //int lsb = (partial % 16) << 8;
            //int msb = (partial / 16) << 12;

            //int offset = (msb + lsb);

            //Address += offset;
            Address += addr;
            Index = partial;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected partial.
        /// </summary>
        public IntegraPCMNoteIndex Partial => (IntegraPCMNoteIndex)Index;

        /// <summary>
        /// Gets the index of the partial.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Gets the left waveform of partial 1.
        /// </summary>
        public WaveformTemplate Wave1Left
        {
            get
            {
                if (WMT1WaveGroupType == IntegraWaveGroupType.INT)
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT1WaveLeft);
                }
                else
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, (IntegraWaveFormBanks)WMT1WaveGroupID, WMT1WaveLeft);
                }
            }
        }

        /// <summary>
        /// Gets the right waveform of partial 1.
        /// </summary>
        public WaveformTemplate Wave1Right
        {
            get
            {
                if (WMT1WaveGroupType == IntegraWaveGroupType.INT)
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT1WaveRight);
                }
                else
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, (IntegraWaveFormBanks)WMT1WaveGroupID, WMT1WaveRight);
                }
            }
        }

        /// <summary>
        /// Gets the left waveform of partial 2.
        /// </summary>
        public WaveformTemplate Wave2Left
        {
            get
            {
                if (WMT2WaveGroupType == IntegraWaveGroupType.INT)
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT2WaveLeft);
                }
                else
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, (IntegraWaveFormBanks)WMT2WaveGroupID, WMT2WaveLeft);
                }
            }
        }

        /// <summary>
        /// Gets the right waveform of partial 2.
        /// </summary>
        public WaveformTemplate Wave2Right
        {
            get
            {
                if (WMT2WaveGroupType == IntegraWaveGroupType.INT)
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT2WaveRight);
                }
                else
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, (IntegraWaveFormBanks)WMT2WaveGroupID, WMT2WaveRight);
                }
            }
        }

        /// <summary>
        /// Gets the left waveform of partial 3.
        /// </summary>
        public WaveformTemplate Wave3Left
        {
            get
            {
                if (WMT3WaveGroupType == IntegraWaveGroupType.INT)
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT3WaveLeft);
                }
                else
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, (IntegraWaveFormBanks)WMT3WaveGroupID, WMT3WaveLeft);
                }
            }
        }

        /// <summary>
        /// Gets the right waveform of partial 3.
        /// </summary>
        public WaveformTemplate Wave3Right
        {
            get
            {
                if (WMT3WaveGroupType == IntegraWaveGroupType.INT)
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT3WaveRight);
                }
                else
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, (IntegraWaveFormBanks)WMT3WaveGroupID, WMT3WaveRight);
                }
            }
        }

        /// <summary>
        /// Gets the left waveform of partial 4.
        /// </summary>
        public WaveformTemplate Wave4Left
        {
            get
            {
                if (WMT4WaveGroupType == IntegraWaveGroupType.INT)
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT4WaveLeft);
                }
                else
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, (IntegraWaveFormBanks)WMT4WaveGroupID, WMT4WaveLeft);
                }
            }
        }

        /// <summary>
        /// Gets the right waveform of partial 4.
        /// </summary>
        public WaveformTemplate Wave4Right
        {
            get
            {
                if (WMT4WaveGroupType == IntegraWaveGroupType.INT)
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT4WaveRight);
                }
                else
                {
                    return IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, (IntegraWaveFormBanks)WMT4WaveGroupID, WMT4WaveRight);
                }
            }
        }

        #endregion

        #region Properties: INTEGRA-7

        #region Properties: General

        [Offset(0x0000)]
        public string PartialName
        {
            get => Encoding.ASCII.GetString(_PartialName, 0, 12);
            set
            {
                if (PartialName != value)
                {
                    if (string.IsNullOrEmpty(value))
                        return;

                    _PartialName = Encoding.ASCII.GetBytes(value.FixedLength(_PartialName.Length));

                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public IntegraAssignType AssignType
        {
            get => _AssignType;
            set
            {
                if (_AssignType != value)
                {
                    _AssignType = value;
                    NotifyPropertyChanged();
                }

            }
        }

        [Offset(0x000D)]
        public IntegraMuteGroup MuteGroup
        {
            get => _MuteGroup;
            set
            {
                if (_MuteGroup != value)
                {
                    _MuteGroup = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000E)]
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

        [Offset(0x000F)]
        public IntegraKeyRange CoarseTune
        {
            get => _CoarseTune;
            set
            {
                if (_CoarseTune != value)
                {
                    _CoarseTune = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0010)]
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

        [Offset(0x0011)]
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

        [Offset(0x0012)]
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

        [Offset(0x0013)]
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

        [Offset(0x0014)]
        public byte AlternatePanDepth
        {
            get => _AlternatePanDepth;
            set
            {
                if (_AlternatePanDepth != value)
                {
                    _AlternatePanDepth = value.Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0015)]
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

        [Offset(0x0016)]
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

        [Offset(0x0019)]
        public byte ChorusSendLevel
        {
            get => _ChorusSendLevel;
            set
            {
                if (_ChorusSendLevel != value)
                {
                    _ChorusSendLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001A)]
        public byte ReverbSendLevel
        {
            get => _ReverbSendLevel;
            set
            {
                if (_ReverbSendLevel != value)
                {
                    _ReverbSendLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001B)]
        public IntegraPartialOutputAssign OutputAssign
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

        [Offset(0x001C)]
        public byte PitchBendRange
        {
            get => _PitchBendRange;
            set
            {
                if (_PitchBendRange != value)
                {
                    _PitchBendRange = value.Clamp(0, 48);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001D)]
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

        [Offset(0x001E)]
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

        [Offset(0x0020)]
        public IntegraPartialVelocityControl WMTVelocityControl
        {
            get => _WMTVelocityControl;
            set
            {
                if (_WMTVelocityControl != value)
                {
                    _WMTVelocityControl = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: WMT 01

        [Offset(0x0021)]
        public IntegraSwitch WMT1Switch
        {
            get => _WMT1Switch;
            set
            {
                if (_WMT1Switch != value)
                {
                    _WMT1Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0022)]
        public IntegraWaveGroupType WMT1WaveGroupType
        {
            get => _WMT1WaveGroupType;
            set
            {
                if (_WMT1WaveGroupType != value)
                {
                    _WMT1WaveGroupType = value;
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave1Left));
                    NotifyPropertyChanged(nameof(Wave1Right));
                }
            }
        }

        [Offset(0x0023)]
        public IntegraPCMWaveGroups WMT1WaveGroupID
        {
            get => _WMT1WaveGroupID;
            set
            {
                if (_WMT1WaveGroupID != value)
                {
                    _WMT1WaveGroupID = value;
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave1Left));
                    NotifyPropertyChanged(nameof(Wave1Right));
                }
            }
        }

        [Offset(0x0027)]
        public int WMT1WaveLeft
        {
            get => _WMT1WaveLeft.Deserialize();
            set
            {
                if (WMT1WaveLeft != value)
                {
                    _WMT1WaveLeft = value.Clamp(0, IntegraConstants.WAVE_COUNT_INT).SerializeInt();
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave1Left));
                }
            }
        }

        [Offset(0x002B)]
        public int WMT1WaveRight
        {
            get => _WMT1WaveRight.Deserialize();
            set
            {
                if (WMT1WaveRight != value)
                {
                    _WMT1WaveRight = value.Clamp(0, IntegraConstants.WAVE_COUNT_INT).SerializeInt();
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave1Right));
                }
            }
        }

        [Offset(0x002F)]
        public int WMT1Gain
        {
            get => _WMT1Gain.Deserialize(1, 6);
            set
            {
                if (WMT1Gain != value)
                {
                    _WMT1Gain = value.Serialize(1, 6).Clamp(0, 3);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0030)]
        public IntegraSwitch WMT1FXMSwitch
        {
            get => _WMT1FXMSwitch;
            set
            {
                if (_WMT1FXMSwitch != value)
                {
                    _WMT1FXMSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0031)]
        public int WMT1FXMColor
        {
            get => _WMT1FXMColor.Deserialize(-1);
            set
            {
                if (WMT1FXMColor != value)
                {
                    _WMT1FXMColor = value.Clamp(1, 4).Serialize(-1);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0032)]
        public byte WMT1FXMDepth
        {
            get => _WMT1FXMDepth;
            set
            {
                if (_WMT1FXMDepth != value)
                {
                    _WMT1FXMDepth = value.Clamp(0, 16);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0033)]
        public IntegraSwitch WMT1TempoSync
        {
            get => _WMT1TempoSync;
            set
            {
                if (_WMT1TempoSync != value)
                {
                    _WMT1TempoSync = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0034)]
        public int WMT1CoarseTune
        {
            get => _WMT1CoarseTune.Deserialize(64);
            set
            {
                if (WMT1CoarseTune != value)
                {
                    _WMT1CoarseTune = value.Clamp(-48, 48).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0035)]
        public int WMT1FineTune
        {
            get => _WMT1FineTune.Deserialize(64);
            set
            {
                if (WMT1FineTune != value)
                {
                    _WMT1FineTune = value.Clamp(-50, 50).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0036)]
        public byte WMT1Pan
        {
            get => _WMT1Pan;
            set
            {
                if (_WMT1Pan != value)
                {
                    _WMT1Pan = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0037)]
        public IntegraSwitch WMT1RandomPanSwitch
        {
            get => _WMT1RandomPanSwitch;
            set
            {
                if (_WMT1RandomPanSwitch != value)
                {
                    _WMT1RandomPanSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0038)]
        public IntegraControlSwitch WMT1AlternatePanSwitch
        {
            get => _WMT1AlternatePanSwitch;
            set
            {
                if (_WMT1AlternatePanSwitch != value)
                {
                    _WMT1AlternatePanSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0039)]
        public byte WMT1Level
        {
            get => _WMT1Level;
            set
            {
                if (_WMT1Level != value)
                {
                    _WMT1Level = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003A)]
        public byte WMT1VelocityRangeLower
        {
            get => _WMT1VelocityRangeLower;
            set
            {
                if (_WMT1VelocityRangeLower != value)
                {
                    _WMT1VelocityRangeLower = value.Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003B)]
        public byte WMT1VelocityRangeUpper
        {
            get => _WMT1VelocityRangeUpper;
            set
            {
                if (_WMT1VelocityRangeUpper != value)
                {
                    _WMT1VelocityRangeUpper = value.Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003C)]
        public byte WMT1VelocityFadeLower
        {
            get => _WMT1VelocityFadeLower;
            set
            {
                if (_WMT1VelocityFadeLower != value)
                {
                    _WMT1VelocityFadeLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003D)]
        public byte WMT1VelocityFadeUpper
        {
            get => _WMT1VelocityFadeUpper;
            set
            {
                if (_WMT1VelocityFadeUpper != value)
                {
                    _WMT1VelocityFadeUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: WMT 02

        [Offset(0x003E)]
        public IntegraSwitch WMT2Switch
        {
            get => _WMT2Switch;
            set
            {
                if (_WMT2Switch != value)
                {
                    _WMT2Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003F)]
        public IntegraWaveGroupType WMT2WaveGroupType
        {
            get => _WMT2WaveGroupType;
            set
            {
                if (_WMT2WaveGroupType != value)
                {
                    _WMT2WaveGroupType = value;
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave2Left));
                    NotifyPropertyChanged(nameof(Wave2Right));
                }
            }
        }

        [Offset(0x0040)]
        public IntegraPCMWaveGroups WMT2WaveGroupID
        {
            get => _WMT2WaveGroupID;
            set
            {
                if (_WMT2WaveGroupID != value)
                {
                    _WMT2WaveGroupID = value;
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave2Left));
                    NotifyPropertyChanged(nameof(Wave2Right));
                }
            }
        }

        [Offset(0x0044)]
        public int WMT2WaveLeft
        {
            get => _WMT2WaveLeft.Deserialize();
            set
            {
                if (WMT2WaveLeft != value)
                {
                    _WMT2WaveLeft = value.Clamp(0, IntegraConstants.WAVE_COUNT_INT).SerializeInt();
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave2Left));
                }
            }
        }

        [Offset(0x0048)]
        public int WMT2WaveRight
        {
            get => _WMT2WaveRight.Deserialize();
            set
            {
                if (WMT2WaveRight != value)
                {
                    _WMT2WaveRight = value.Clamp(0, IntegraConstants.WAVE_COUNT_INT).SerializeInt();
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave2Right));
                }
            }
        }

        [Offset(0x004C)]
        public int WMT2Gain
        {
            get => _WMT2Gain.Deserialize(1, 6);
            set
            {
                if (WMT2Gain != value)
                {
                    _WMT2Gain = value.Serialize(1, 6).Clamp(0, 3);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004D)]
        public IntegraSwitch WMT2FXMSwitch
        {
            get => _WMT2FXMSwitch;
            set
            {
                if (_WMT2FXMSwitch != value)
                {
                    _WMT2FXMSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004E)]
        public int WMT2FXMColor
        {
            get => _WMT2FXMColor.Deserialize(-1);
            set
            {
                if (WMT2FXMColor != value)
                {
                    _WMT2FXMColor = value.Clamp(1, 4).Serialize(-1);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004F)]
        public byte WMT2FXMDepth
        {
            get => _WMT2FXMDepth;
            set
            {
                if (_WMT2FXMDepth != value)
                {
                    _WMT2FXMDepth = value.Clamp(0, 16);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0050)]
        public IntegraSwitch WMT2TempoSync
        {
            get => _WMT2TempoSync;
            set
            {
                if (_WMT2TempoSync != value)
                {
                    _WMT2TempoSync = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0051)]
        public int WMT2CoarseTune
        {
            get => _WMT2CoarseTune.Deserialize(64);
            set
            {
                if (WMT2CoarseTune != value)
                {
                    _WMT2CoarseTune = value.Clamp(-48, 48).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0052)]
        public int WMT2FineTune
        {
            get => _WMT2FineTune.Deserialize(64);
            set
            {
                if (WMT2FineTune != value)
                {
                    _WMT2FineTune = value.Clamp(-50, 50).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0053)]
        public byte WMT2Pan
        {
            get => _WMT2Pan;
            set
            {
                if (_WMT2Pan != value)
                {
                    _WMT2Pan = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0054)]
        public IntegraSwitch WMT2RandomPanSwitch
        {
            get => _WMT2RandomPanSwitch;
            set
            {
                if (_WMT2RandomPanSwitch != value)
                {
                    _WMT2RandomPanSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0055)]
        public IntegraControlSwitch WMT2AlternatePanSwitch
        {
            get => _WMT2AlternatePanSwitch;
            set
            {
                if (_WMT2AlternatePanSwitch != value)
                {
                    _WMT2AlternatePanSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0056)]
        public byte WMT2Level
        {
            get => _WMT2Level;
            set
            {
                if (_WMT2Level != value)
                {
                    _WMT2Level = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0057)]
        public byte WMT2VelocityRangeLower
        {
            get => _WMT2VelocityRangeLower;
            set
            {
                if (_WMT2VelocityRangeLower != value)
                {
                    _WMT2VelocityRangeLower = value.Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0058)]
        public byte WMT2VelocityRangeUpper
        {
            get => _WMT2VelocityRangeUpper;
            set
            {
                if (_WMT2VelocityRangeUpper != value)
                {
                    _WMT2VelocityRangeUpper = value.Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0059)]
        public byte WMT2VelocityFadeLower
        {
            get => _WMT2VelocityFadeLower;
            set
            {
                if (_WMT2VelocityFadeLower != value)
                {
                    _WMT2VelocityFadeLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x005A)]
        public byte WMT2VelocityFadeUpper
        {
            get => _WMT2VelocityFadeUpper;
            set
            {
                if (_WMT2VelocityFadeUpper != value)
                {
                    _WMT2VelocityFadeUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: WMT 03

        [Offset(0x005B)]
        public IntegraSwitch WMT3Switch
        {
            get => _WMT3Switch;
            set
            {
                if (_WMT3Switch != value)
                {
                    _WMT3Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x005C)]
        public IntegraWaveGroupType WMT3WaveGroupType
        {
            get => _WMT3WaveGroupType;
            set
            {
                if (_WMT3WaveGroupType != value)
                {
                    _WMT3WaveGroupType = value;
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave3Left));
                    NotifyPropertyChanged(nameof(Wave3Right));
                }
            }
        }

        [Offset(0x005D)]
        public IntegraPCMWaveGroups WMT3WaveGroupID
        {
            get => _WMT3WaveGroupID;
            set
            {
                if (_WMT3WaveGroupID != value)
                {
                    _WMT3WaveGroupID = value;
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave3Left));
                    NotifyPropertyChanged(nameof(Wave3Right));
                }
            }
        }

        [Offset(0x0061)]
        public int WMT3WaveLeft
        {
            get => _WMT3WaveLeft.Deserialize();
            set
            {
                if (WMT3WaveLeft != value)
                {
                    _WMT3WaveLeft = value.Clamp(0, IntegraConstants.WAVE_COUNT_INT).SerializeInt();
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave3Left));
                }
            }
        }

        [Offset(0x0065)]
        public int WMT3WaveRight
        {
            get => _WMT3WaveRight.Serialize();
            set
            {
                if (WMT3WaveRight != value)
                {
                    _WMT3WaveRight = value.Clamp(0, IntegraConstants.WAVE_COUNT_INT).SerializeInt();
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave3Right));
                }
            }
        }

        [Offset(0x0069)]
        public int WMT3Gain
        {
            get => _WMT3Gain.Deserialize(1, 6);
            set
            {
                if (WMT3Gain != value)
                {
                    _WMT3Gain = value.Serialize(1, 6).Clamp(0, 3);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x006A)]
        public IntegraSwitch WMT3FXMSwitch
        {
            get => _WMT3FXMSwitch;
            set
            {
                if (_WMT3FXMSwitch != value)
                {
                    _WMT3FXMSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x006B)]
        public int WMT3FXMColor
        {
            get => _WMT3FXMColor.Deserialize(-1);
            set
            {
                if (WMT3FXMColor != value)
                {
                    _WMT3FXMColor = value.Clamp(1, 4).Serialize(-1);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x006C)]
        public byte WMT3FXMDepth
        {
            get => _WMT3FXMDepth;
            set
            {
                if (_WMT3FXMDepth != value)
                {
                    _WMT3FXMDepth = value.Clamp(0, 16);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x006D)]
        public IntegraSwitch WMT3TempoSync
        {
            get => _WMT3TempoSync;
            set
            {
                if (_WMT3TempoSync != value)
                {
                    _WMT3TempoSync = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x006E)]
        public int WMT3CoarseTune
        {
            get => _WMT3CoarseTune.Deserialize(64);
            set
            {
                if (WMT3CoarseTune != value)
                {
                    _WMT3CoarseTune = value.Clamp(-48, 48).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x006F)]
        public int WMT3FineTune
        {
            get => _WMT3FineTune.Deserialize(64);
            set
            {
                if (WMT3FineTune != value)
                {
                    _WMT3FineTune = value.Clamp(-50, 50).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0070)]
        public byte WMT3Pan
        {
            get => _WMT3Pan;
            set
            {
                if (_WMT3Pan != value)
                {
                    _WMT3Pan = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0071)]
        public IntegraSwitch WMT3RandomPanSwitch
        {
            get => _WMT3RandomPanSwitch;
            set
            {
                if (_WMT3RandomPanSwitch != value)
                {
                    _WMT3RandomPanSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0072)]
        public IntegraControlSwitch WMT3AlternatePanSwitch
        {
            get => _WMT3AlternatePanSwitch;
            set
            {
                if (_WMT3AlternatePanSwitch != value)
                {
                    _WMT3AlternatePanSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0073)]
        public byte WMT3Level
        {
            get => _WMT3Level;
            set
            {
                if (_WMT3Level != value)
                {
                    _WMT3Level = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0074)]
        public byte WMT3VelocityRangeLower
        {
            get => _WMT3VelocityRangeLower;
            set
            {
                if (_WMT3VelocityRangeLower != value)
                {
                    _WMT3VelocityRangeLower = value.Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0075)]
        public byte WMT3VelocityRangeUpper
        {
            get => _WMT3VelocityRangeUpper;
            set
            {
                if (_WMT3VelocityRangeUpper != value)
                {
                    _WMT3VelocityRangeUpper = value.Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0076)]
        public byte WMT3VelocityFadeLower
        {
            get => _WMT3VelocityFadeLower;
            set
            {
                if (_WMT3VelocityFadeLower != value)
                {
                    _WMT3VelocityFadeLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0077)]
        public byte WMT3VelocityFadeUpper
        {
            get => _WMT3VelocityFadeUpper;
            set
            {
                if (_WMT3VelocityFadeUpper != value)
                {
                    _WMT3VelocityFadeUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: WMT 04

        [Offset(0x0078)]
        public IntegraSwitch WMT4Switch
        {
            get => _WMT4Switch;
            set
            {
                if (_WMT4Switch != value)
                {
                    _WMT4Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0079)]
        public IntegraWaveGroupType WMT4WaveGroupType
        {
            get => _WMT4WaveGroupType;
            set
            {
                if (_WMT4WaveGroupType != value)
                {
                    _WMT4WaveGroupType = value;
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave4Left));
                    NotifyPropertyChanged(nameof(Wave4Right));
                }
            }
        }

        [Offset(0x007A)]
        public IntegraPCMWaveGroups WMT4WaveGroupID
        {
            get => _WMT4WaveGroupID;
            set
            {
                if (_WMT4WaveGroupID != value)
                {
                    _WMT4WaveGroupID = value;
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave4Left));
                    NotifyPropertyChanged(nameof(Wave4Right));
                }
            }
        }

        [Offset(0x007E)]
        public int WMT4WaveLeft
        {
            get => _WMT4WaveLeft.Deserialize();
            set
            {
                if (WMT4WaveLeft != value)
                {
                    _WMT4WaveLeft = value.Clamp(0, IntegraConstants.WAVE_COUNT_INT).SerializeInt();
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave4Left));
                }
            }
        }

        [Offset(0x0102)]
        public int WMT4WaveRight
        {
            get => _WMT4WaveRight.Deserialize();
            set
            {
                if (WMT4WaveRight != value)
                {
                    _WMT4WaveRight = value.Clamp(0, IntegraConstants.WAVE_COUNT_INT).SerializeInt();
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(Wave4Right));
                }

            }
        }

        [Offset(0x0106)]
        public int WMT4Gain
        {
            get => _WMT4Gain.Deserialize(1, 6);
            set
            {
                if (WMT4Gain != value)
                {
                    _WMT4Gain = value.Serialize(1, 6).Clamp(0, 3);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0107)]
        public IntegraSwitch WMT4FXMSwitch
        {
            get => _WMT4FXMSwitch;
            set
            {
                if (_WMT4FXMSwitch != value)
                {
                    _WMT4FXMSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0108)]
        public int WMT4FXMColor
        {
            get => _WMT4FXMColor.Deserialize(-1);
            set
            {
                if (WMT4FXMColor != value)
                {
                    _WMT4FXMColor = value.Clamp(1, 4).Serialize(-1);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0109)]
        public byte WMT4FXMDepth
        {
            get => _WMT4FXMDepth;
            set
            {
                if (_WMT4FXMDepth != value)
                {
                    _WMT4FXMDepth = value.Clamp(0, 16);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x010A)]
        public IntegraSwitch WMT4TempoSync
        {
            get => _WMT4TempoSync;
            set
            {
                if (_WMT4TempoSync != value)
                {
                    _WMT4TempoSync = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x010B)]
        public int WMT4CoarseTune
        {
            get => _WMT4CoarseTune.Deserialize(64);
            set
            {
                if (WMT4CoarseTune != value)
                {
                    _WMT4CoarseTune = value.Clamp(-48, 48).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x010C)]
        public int WMT4FineTune
        {
            get => _WMT4FineTune.Deserialize(64);
            set
            {
                if (WMT4FineTune != value)
                {
                    _WMT4FineTune = value.Clamp(-50, 50).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x010D)]
        public byte WMT4Pan
        {
            get => _WMT4Pan;
            set
            {
                if (_WMT4Pan != value)
                {
                    _WMT4Pan = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x010E)]
        public IntegraSwitch WMT4RandomPanSwitch
        {
            get => _WMT4RandomPanSwitch;
            set
            {
                if (_WMT4RandomPanSwitch != value)
                {
                    _WMT4RandomPanSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x010F)]
        public IntegraControlSwitch WMT4AlternatePanSwitch
        {
            get => _WMT4AlternatePanSwitch;
            set
            {
                if (_WMT4AlternatePanSwitch != value)
                {
                    _WMT4AlternatePanSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0110)]
        public byte WMT4Level
        {
            get => _WMT4Level;
            set
            {
                if (_WMT4Level != value)
                {
                    _WMT4Level = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0111)]
        public byte WMT4VelocityRangeLower
        {
            get => _WMT4VelocityRangeLower;
            set
            {
                if (_WMT4VelocityRangeLower != value)
                {
                    _WMT4VelocityRangeLower = value.Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0112)]
        public byte WMT4VelocityRangeUpper
        {
            get => _WMT4VelocityRangeUpper;
            set
            {
                if (_WMT4VelocityRangeUpper != value)
                {
                    _WMT4VelocityRangeUpper = value.Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0113)]
        public byte WMT4VelocityFadeLower
        {
            get => _WMT4VelocityFadeLower;
            set
            {
                if (_WMT4VelocityFadeLower != value)
                {
                    _WMT4VelocityFadeLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0114)]
        public byte WMT4VelocityFadeUpper
        {
            get => _WMT4VelocityFadeUpper;
            set
            {
                if (_WMT4VelocityFadeUpper != value)
                {
                    _WMT4VelocityFadeUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: Pitch Envelope

        [Offset(0x0115)]
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

        [Offset(0x0116)]
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

        [Offset(0x0117)]
        public int PitchEnvTime1VelocitySens
        {
            get => _PitchEnvTime1VelocitySens.Deserialize(64);
            set
            {
                if (PitchEnvTime1VelocitySens != value)
                {
                    _PitchEnvTime1VelocitySens = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0118)]
        public int PitchEnvTime4VelocitySens
        {
            get => _PitchEnvTime4VelocitySens.Deserialize(64);
            set
            {
                if (PitchEnvTime4VelocitySens != value)
                {
                    _PitchEnvTime4VelocitySens = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0119)]
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

        [Offset(0x011A)]
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

        [Offset(0x011B)]
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

        [Offset(0x011C)]
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

        [Offset(0x011D)]
        public int PitchEnvLevel0
        {
            get => _PitchEnvLevel0.Deserialize(64);
            set
            {
                if (PitchEnvLevel0 != value)
                {
                    _PitchEnvLevel0 = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x011E)]
        public int PitchEnvLevel1
        {
            get => _PitchEnvLevel1.Deserialize(64);
            set
            {
                if (PitchEnvLevel1 != value)
                {
                    _PitchEnvLevel1 = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x011F)]
        public int PitchEnvLevel2
        {
            get => _PitchEnvLevel2.Deserialize(64);
            set
            {
                if (PitchEnvLevel2 != value)
                {
                    _PitchEnvLevel2 = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0120)]
        public int PitchEnvLevel3
        {
            get => _PitchEnvLevel3.Deserialize(64);
            set
            {
                if (PitchEnvLevel3 != value)
                {
                    _PitchEnvLevel3 = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0121)]
        public int PitchEnvLevel4
        {
            get => _PitchEnvLevel4.Deserialize(64);
            set
            {
                if (PitchEnvLevel4 != value)
                {
                    _PitchEnvLevel4 = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: TVF

        [Offset(0x0122)]
        public IntegraTVFFilterType TVFFilterType
        {
            get => _TVFFilterType;
            set
            {
                if (_TVFFilterType != value)
                {
                    _TVFFilterType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0123)]
        public byte TVFCutoffFrequency
        {
            get => _TVFCutoffFrequency;
            set
            {
                if (_TVFCutoffFrequency != value)
                {
                    _TVFCutoffFrequency = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0124)]
        public IntegraVelocityCurve TVFCutoffVelocityCurve
        {
            get => _TVFCutoffVelocityCurve;
            set
            {
                if (_TVFCutoffVelocityCurve != value)
                {
                    _TVFCutoffVelocityCurve = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0125)]
        public int TVFCutoffVelocitySens
        {
            get => _TVFCutoffVelocitySens.Deserialize(64);
            set
            {
                if (TVFCutoffVelocitySens != value)
                {
                    _TVFCutoffVelocitySens = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0126)]
        public byte TVFResonance
        {
            get => _TVFResonance;
            set
            {
                if (_TVFResonance != value)
                {
                    _TVFResonance = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0127)]
        public int TVFResonanceVelocitySens
        {
            get => _TVFResonanceVelocitySens.Deserialize(64);
            set
            {
                if (TVFResonanceVelocitySens != value)
                {
                    _TVFResonanceVelocitySens = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0128)]
        public int TVFEnvDepth
        {
            get => _TVFEnvDepth.Deserialize(64);
            set
            {
                if (TVFEnvDepth != value)
                {
                    _TVFEnvDepth = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0129)]
        public IntegraVelocityCurve TVFEnvVelocityCurve
        {
            get => _TVFEnvVelocityCurve;
            set
            {
                if (_TVFEnvVelocityCurve != value)
                {
                    _TVFEnvVelocityCurve = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x012A)]
        public int TVFEnvVelocitySens
        {
            get => _TVFEnvVelocitySens.Deserialize(64);
            set
            {
                if (TVFEnvVelocitySens != value)
                {
                    _TVFEnvVelocitySens = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x012B)]
        public int TVFEnvTime1VelocitySens
        {
            get => _TVFEnvTime1VelocitySens.Deserialize(64);
            set
            {
                if (TVFEnvTime1VelocitySens != value)
                {
                    _TVFEnvTime1VelocitySens = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x012C)]
        public int TVFEnvTime4VelocitySens
        {
            get => _TVFEnvTime4VelocitySens.Deserialize(64);
            set
            {
                if (TVFEnvTime4VelocitySens != value)
                {
                    _TVFEnvTime4VelocitySens = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x012D)]
        public byte TVFEnvTime1
        {
            get => _TVFEnvTime1;
            set
            {
                if (_TVFEnvTime1 != value)
                {
                    _TVFEnvTime1 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x012E)]
        public byte TVFEnvTime2
        {
            get => _TVFEnvTime2;
            set
            {
                if (_TVFEnvTime2 != value)
                {
                    _TVFEnvTime2 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x012F)]
        public byte TVFEnvTime3
        {
            get => _TVFEnvTime3;
            set
            {
                if (_TVFEnvTime3 != value)
                {
                    _TVFEnvTime3 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0130)]
        public byte TVFEnvTime4
        {
            get => _TVFEnvTime4;
            set
            {
                if (_TVFEnvTime4 != value)
                {
                    _TVFEnvTime4 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0131)]
        public byte TVFEnvLevel0
        {
            get => _TVFEnvLevel0;
            set
            {
                if (_TVFEnvLevel0 != value)
                {
                    _TVFEnvLevel0 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0132)]
        public byte TVFEnvLevel1
        {
            get => _TVFEnvLevel1;
            set
            {
                if (_TVFEnvLevel1 != value)
                {
                    _TVFEnvLevel1 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0133)]
        public byte TVFEnvLevel2
        {
            get => _TVFEnvLevel2;
            set
            {
                if (_TVFEnvLevel2 != value)
                {
                    _TVFEnvLevel2 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0134)]
        public byte TVFEnvLevel3
        {
            get => _TVFEnvLevel3;
            set
            {
                if (_TVFEnvLevel3 != value)
                {
                    _TVFEnvLevel3 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0135)]
        public byte TVFEnvLevel4
        {
            get => _TVFEnvLevel4;
            set
            {
                if (_TVFEnvLevel4 != value)
                {
                    _TVFEnvLevel4 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }


        #endregion

        #region Properties: TVA

        [Offset(0x0136)]
        public IntegraVelocityCurve TVALevelVelocityCurve
        {
            get => _TVALevelVelocityCurve;
            set
            {
                if (_TVALevelVelocityCurve != value)
                {
                    _TVALevelVelocityCurve = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0137)]
        public int TVALevelVelocitySens
        {
            get => _TVALevelVelocitySens.Deserialize(64);
            set
            {
                if (TVALevelVelocitySens != value)
                {
                    _TVALevelVelocitySens = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0138)]
        public int TVAEnvTime1VelocitySens
        {
            get => _TVAEnvTime1VelocitySens.Deserialize(64);
            set
            {
                if (TVAEnvTime1VelocitySens != value)
                {
                    _TVAEnvTime1VelocitySens = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0139)]
        public int TVAEnvTime4VelocitySens
        {
            get => _TVAEnvTime4VelocitySens.Deserialize(64);
            set
            {
                if (TVAEnvTime4VelocitySens != value)
                {
                    _TVAEnvTime4VelocitySens = value.Clamp(-63, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x013A)]
        public byte TVAEnvTime1
        {
            get => _TVAEnvTime1;
            set
            {
                if (_TVAEnvTime1 != value)
                {
                    _TVAEnvTime1 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x013B)]
        public byte TVAEnvTime2
        {
            get => _TVAEnvTime2;
            set
            {
                if (_TVAEnvTime2 != value)
                {
                    _TVAEnvTime2 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x013C)]
        public byte TVAEnvTime3
        {
            get => _TVAEnvTime3;
            set
            {
                if (_TVAEnvTime3 != value)
                {
                    _TVAEnvTime3 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x013D)]
        public byte TVAEnvTime4
        {
            get => _TVAEnvTime4;
            set
            {
                if (_TVAEnvTime4 != value)
                {
                    _TVAEnvTime4 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x013E)]
        public byte TVAEnvLevel1
        {
            get => _TVAEnvLevel1;
            set
            {
                if (_TVAEnvLevel1 != value)
                {
                    _TVAEnvLevel1 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x013F)]
        public byte TVAEnvLevel2
        {
            get => _TVAEnvLevel2;
            set
            {
                if (_TVAEnvLevel2 != value)
                {
                    _TVAEnvLevel2 = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0140)]
        public byte TVAEnvLevel3
        {
            get => _TVAEnvLevel3;
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

        #region Properties: Misc

        [Offset(0x0141)]
        public IntegraSwitch OneShotMode
        {
            get => _OneShotMode;
            set
            {
                if (_OneShotMode != value)
                {
                    _OneShotMode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #endregion

        #region Enumerations

        public static List<string> PanValues
        {
            get { return IntegraPan.Values; }
        }

        public static List<string> PitchDepths
        {
            get { return IntegraPitchDepths.Values; }
        }

        #endregion
    }
}
