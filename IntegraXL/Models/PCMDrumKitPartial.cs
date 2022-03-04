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
        [Offset(0x000D)] byte _MuteGroup;
        [Offset(0x000E)] byte _PartialLevel;
        [Offset(0x000F)] byte _CoarseTune;
        [Offset(0x0010)] byte _FineTune;
        [Offset(0x0011)] byte _RandomPitchDepth;
        [Offset(0x0012)] byte _Pan;
        [Offset(0x0013)] byte _RandomPanDepth;
        [Offset(0x0014)] byte _AlternatePanDepth;
        [Offset(0x0015)] IntegraEnvelopeMode _EnvMode;
        [Offset(0x0016)] byte _OutputLevel;
        [Offset(0x0017)] byte[] RESERVED01 = new byte[2];
        [Offset(0x0019)] byte _ChorusSendLevel;
        [Offset(0x001A)] byte _ReverbSendLevel;
        [Offset(0x001B)] IntegraPartialOutputAssign _OutputAssign;
        [Offset(0x001C)] byte _PitchBendRange;
        [Offset(0x001D)] IntegraSwitch _ReceiveExpression;
        [Offset(0x001E)] IntegraSwitch _ReceiveHold;
        [Offset(0x001F)] byte RESERVED02;
        [Offset(0x0020)] IntegraPartialVelocityControl _WMTVelocityControl;

        #endregion

        #region Fields: WMT 01

        [Offset(0x0021)] IntegraSwitch _WMT01WaveSwitch;
        [Offset(0x0022)] IntegraWaveGroupType _WMT01WaveGroupType;
        [Offset(0x0023)] int _WMT01WaveGroupID;
        [Offset(0x0027)] int _WMT01WaveNumberL;
        [Offset(0x002B)] int _WMT01WaveNumberR;
        [Offset(0x002F)] byte _WMT01WaveGain;
        [Offset(0x0030)] IntegraSwitch _WMT01WaveFXMSwitch;
        [Offset(0x0031)] byte _WMT01WaveFXMColor;
        [Offset(0x0032)] byte _WMT01WaveFXMDepth;
        [Offset(0x0033)] IntegraSwitch _WMT01WaveTempoSync;
        [Offset(0x0034)] byte _WMT01WaveCoarseTune;
        [Offset(0x0035)] byte _WMT01WaveFineTune;
        [Offset(0x0036)] byte _WMT01WavePan;
        [Offset(0x0037)] IntegraSwitch _WMT01WaveRandomPanSwitch;
        [Offset(0x0038)] IntegraControlSwitch _WMT01WaveAlternatePanSwitch;
        [Offset(0x0039)] byte _WMT01WaveLevel;
        [Offset(0x003A)] byte _WMT01VelocityRangeLower;
        [Offset(0x003B)] byte _WMT01VelocityRangeUpper;
        [Offset(0x003C)] byte _WMT01VelocityFadeWidthLower;
        [Offset(0x003D)] byte _WMT01VelocityFadeWidthUpper;

        #endregion

        #region Fields: WMT 02

        [Offset(0x003E)] IntegraSwitch _WMT02WaveSwitch;
        [Offset(0x003F)] IntegraWaveGroupType _WMT02WaveGroupType;
        [Offset(0x0040)] int _WMT02WaveGroupID;
        [Offset(0x0044)] int _WMT02WaveNumberL;
        [Offset(0x0048)] int _WMT02WaveNumberR;
        [Offset(0x004C)] byte _WMT02WaveGain;
        [Offset(0x004D)] IntegraSwitch _WMT02WaveFXMSwitch;
        [Offset(0x004E)] byte _WMT02WaveFXMColor;
        [Offset(0x004F)] byte _WMT02WaveFXMDepth;
        [Offset(0x0050)] IntegraSwitch _WMT02WaveTempoSync;
        [Offset(0x0051)] byte _WMT02WaveCoarseTune;
        [Offset(0x0052)] byte _WMT02WaveFineTune;
        [Offset(0x0053)] byte _WMT02WavePan;
        [Offset(0x0054)] IntegraSwitch _WMT02WaveRandomPanSwitch;
        [Offset(0x0055)] IntegraControlSwitch _WMT02WaveAlternatePanSwitch;
        [Offset(0x0056)] byte _WMT02WaveLevel;
        [Offset(0x0057)] byte _WMT02VelocityRangeLower;
        [Offset(0x0058)] byte _WMT02VelocityRangeUpper;
        [Offset(0x0059)] byte _WMT02VelocityFadeWidthLower;
        [Offset(0x005A)] byte _WMT02VelocityFadeWidthUpper;

        #endregion

        #region Fields: WMT 03

        [Offset(0x005B)] IntegraSwitch _WMT03WaveSwitch;
        [Offset(0x005C)] IntegraWaveGroupType _WMT03WaveGroupType;
        [Offset(0x005D)] int _WMT03WaveGroupID;
        [Offset(0x0061)] int _WMT03WaveNumberL;
        [Offset(0x0065)] int _WMT03WaveNumberR;
        [Offset(0x0069)] byte _WMT03WaveGain;
        [Offset(0x006A)] IntegraSwitch _WMT03WaveFXMSwitch;
        [Offset(0x006B)] byte _WMT03WaveFXMColor;
        [Offset(0x006C)] byte _WMT03WaveFXMDepth;
        [Offset(0x006D)] IntegraSwitch _WMT03WaveTempoSync;
        [Offset(0x006E)] byte _WMT03WaveCoarseTune;
        [Offset(0x006F)] byte _WMT03WaveFineTune;
        [Offset(0x0070)] byte _WMT03WavePan;
        [Offset(0x0071)] IntegraSwitch _WMT03WaveRandomPanSwitch;
        [Offset(0x0072)] IntegraControlSwitch _WMT03WaveAlternatePanSwitch;
        [Offset(0x0073)] byte _WMT03WaveLevel;
        [Offset(0x0074)] byte _WMT03VelocityRangeLower;
        [Offset(0x0075)] byte _WMT03VelocityRangeUpper;
        [Offset(0x0076)] byte _WMT03VelocityFadeWidthLower;
        [Offset(0x0077)] byte _WMT03VelocityFadeWidthUpper;

        #endregion

        #region Fields: WMT 04

        [Offset(0x0078)] IntegraSwitch _WMT04WaveSwitch;
        [Offset(0x0079)] IntegraWaveGroupType _WMT04WaveGroupType;
        [Offset(0x007A)] int _WMT04WaveGroupID;
        [Offset(0x007E)] int _WMT04WaveNumberL;
        [Offset(0x0102)] int _WMT04WaveNumberR;
        [Offset(0x0106)] byte _WMT04WaveGain;
        [Offset(0x0107)] IntegraSwitch _WMT04WaveFXMSwitch;
        [Offset(0x0108)] byte _WMT04WaveFXMColor;
        [Offset(0x0109)] byte _WMT04WaveFXMDepth;
        [Offset(0x010A)] IntegraSwitch _WMT04WaveTempoSync;
        [Offset(0x010B)] byte _WMT04WaveCoarseTune;
        [Offset(0x010C)] byte _WMT04WaveFineTune;
        [Offset(0x010D)] byte _WMT04WavePan;
        [Offset(0x010E)] IntegraSwitch _WMT04WaveRandomPanSwitch;
        [Offset(0x010F)] IntegraControlSwitch _WMT04WaveAlternatePanSwitch;
        [Offset(0x0110)] byte _WMT04WaveLevel;
        [Offset(0x0111)] byte _WMT04VelocityRangeLower;
        [Offset(0x0112)] byte _WMT04VelocityRangeUpper;
        [Offset(0x0113)] byte _WMT04VelocityFadeWidthLower;
        [Offset(0x0114)] byte _WMT04VelocityFadeWidthUpper;

        #endregion

        #region Fields: Pitch Envelope

        [Offset(0x0115)] byte _PitchEnvDepth;
        [Offset(0x0116)] byte _PitchEnvVelocitySens;
        [Offset(0x0117)] byte _PitchEnvTime01VelocitySens;
        [Offset(0x0118)] byte _PitchEnvTime04VelocitySens;
        [Offset(0x0119)] byte _PitchEnvTime01;
        [Offset(0x011A)] byte _PitchEnvTime02;
        [Offset(0x011B)] byte _PitchEnvTime03;
        [Offset(0x011C)] byte _PitchEnvTime04;
        [Offset(0x011D)] byte _PitchEnvLevel00;
        [Offset(0x011E)] byte _PitchEnvLevel01;
        [Offset(0x011F)] byte _PitchEnvLevel02;
        [Offset(0x0120)] byte _PitchEnvLevel03;
        [Offset(0x0121)] byte _PitchEnvLevel04;

        #endregion

        #region Fields: TVF

        [Offset(0x0122)] IntegraTVFFilterType _TVFFilterType;
        [Offset(0x0123)] byte _TVFCutoffFrequency;
        [Offset(0x0124)] IntegraVelocityCurve _TVFCutoffVelocityCurve;
        [Offset(0x0125)] byte _TVFCutoffVelocitySens;
        [Offset(0x0126)] byte _TVFResonance;
        [Offset(0x0127)] byte _TVFResonanceVelocitySens;
        [Offset(0x0128)] byte _TVFEnvDepth;
        [Offset(0x0129)] IntegraVelocityCurve _TVFEnvVelocityCurveType;
        [Offset(0x012A)] byte _TVFEnvVelocitySens;
        [Offset(0x012B)] byte _TVFEnvTime01VelocitySens;
        [Offset(0x012C)] byte _TVFEnvTime04VelocitySens;
        [Offset(0x012D)] byte _TVFEnvTime01;
        [Offset(0x012E)] byte _TVFEnvTime02;
        [Offset(0x012F)] byte _TVFEnvTime03;
        [Offset(0x0130)] byte _TVFEnvTime04;
        [Offset(0x0131)] byte _TVFEnvLevel00;
        [Offset(0x0132)] byte _TVFEnvLevel01;
        [Offset(0x0133)] byte _TVFEnvLevel02;
        [Offset(0x0134)] byte _TVFEnvLevel03;
        [Offset(0x0135)] byte _TVFEnvLevel04;

        #endregion

        #region Fields: TVA

        [Offset(0x0136)] IntegraVelocityCurve _TVALevelVelocityCurve;
        [Offset(0x0137)] byte _TVALevelVelocitySens;
        [Offset(0x0138)] byte _TVAEnvTime01VelocitySens;
        [Offset(0x0139)] byte _TVAEnvTime04VelocitySens;
        [Offset(0x013A)] byte _TVAEnvTime01;
        [Offset(0x013B)] byte _TVAEnvTime02;
        [Offset(0x013C)] byte _TVAEnvTime03;
        [Offset(0x013D)] byte _TVAEnvTime04;
        [Offset(0x013E)] byte _TVAEnvLevel01;
        [Offset(0x013F)] byte _TVAEnvLevel02;
        [Offset(0x0140)] byte _TVAEnvLevel03;

        #endregion

        #region Fields: Misc

        [Offset(0x0141)] IntegraSwitch _OneShotMode;
        [Offset(0x0142)] byte RESERVED03;
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
            Index = (IntegraPCMNoteIndex)partial;
        }

        #endregion

        #region Properties

        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public IntegraPCMNoteIndex Index { get; }

        public WaveformTemplate Waveform01L => IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT01WaveNumberL);
        public WaveformTemplate Waveform01R => IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT01WaveNumberR);
        public WaveformTemplate Waveform02L => IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT02WaveNumberL);
        public WaveformTemplate Waveform02R => IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT02WaveNumberR);
        public WaveformTemplate Waveform03L => IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT03WaveNumberL);
        public WaveformTemplate Waveform03R => IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT03WaveNumberR);
        public WaveformTemplate Waveform04L => IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT04WaveNumberL);
        public WaveformTemplate Waveform04R => IntegraWaveformLookup.Template(IntegraWaveFormTypes.PCM, IntegraWaveFormBanks.INT, WMT04WaveNumberR);

        #endregion

        #region Properties: INTEGRA-7

        #region Properties: General

        [Offset(0x0000)]
        public string PartialName
        {
            get { return Encoding.ASCII.GetString(_PartialName); }
            set
            {
                _PartialName = Encoding.ASCII.GetBytes(value);
                NotifyPropertyChanged();

            }
        }

        [Offset(0x000C)]
        public IntegraAssignType AssignType
        {
            get { return _AssignType; }
            set
            {
                _AssignType = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x000D)]
        public byte MuteGroup
        {
            get { return _MuteGroup; }
            set
            {
                _MuteGroup = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x000E)]
        public byte PartialLevel
        {
            get { return _PartialLevel; }
            set
            {
                _PartialLevel = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x000F)]
        public byte CoarseTune
        {
            get { return _CoarseTune; }
            set
            {
                _CoarseTune = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0010)]
        public byte FineTune
        {
            get { return _FineTune; }
            set
            {
                _FineTune = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0011)]
        public byte RandomPitchDepth
        {
            get { return _RandomPitchDepth; }
            set
            {
                _RandomPitchDepth = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0012)]
        public byte Pan
        {
            get { return _Pan; }
            set
            {
                _Pan = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0013)]
        public byte RandomPanDepth
        {
            get { return _RandomPanDepth; }
            set
            {
                _RandomPanDepth = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0014)]
        public byte AlternatePanDepth
        {
            get { return _AlternatePanDepth; }
            set
            {
                _AlternatePanDepth = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0015)]
        public IntegraEnvelopeMode EnvMode
        {
            get { return _EnvMode; }
            set
            {
                _EnvMode = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0016)]
        public byte OutputLevel
        {
            get { return _OutputLevel; }
            set
            {
                _OutputLevel = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0019)]
        public byte ChorusSendLevel
        {
            get { return _ChorusSendLevel; }
            set
            {
                _ChorusSendLevel = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x001A)]
        public byte ReverbSendLevel
        {
            get { return _ReverbSendLevel; }
            set
            {
                _ReverbSendLevel = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x001B)]
        public IntegraPartialOutputAssign OutputAssign
        {
            get { return _OutputAssign; }
            set
            {
                _OutputAssign = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x001C)]
        public byte PitchBendRange
        {
            get { return _PitchBendRange; }
            set
            {
                _PitchBendRange = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x001D)]
        public IntegraSwitch ReceiveExpression
        {
            get { return _ReceiveExpression; }
            set
            {
                _ReceiveExpression = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x001E)]
        public IntegraSwitch ReceiveHold
        {
            get { return _ReceiveHold; }
            set
            {
                _ReceiveHold = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0020)]
        public IntegraPartialVelocityControl WMTVelocityControl
        {
            get { return _WMTVelocityControl; }
            set
            {
                _WMTVelocityControl = value;
                NotifyPropertyChanged();

            }
        }


        #endregion

        #region Properties: WMT 01

        [Offset(0x0021)]
        public IntegraSwitch WMT01WaveSwitch
        {
            get { return _WMT01WaveSwitch; }
            set
            {
                _WMT01WaveSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0022)]
        public IntegraWaveGroupType WMT01WaveGroupType
        {
            get { return _WMT01WaveGroupType; }
            set
            {
                _WMT01WaveGroupType = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0023)]
        public int WMT01WaveGroupID
        {
            get { return _WMT01WaveGroupID.ToMidi(); }
            set
            {
                _WMT01WaveGroupID = value.SerializeInt(); ;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0027)]
        public int WMT01WaveNumberL
        {
            get { return _WMT01WaveNumberL.ToMidi(); }
            set
            {
                _WMT01WaveNumberL = value.SerializeInt();
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Waveform01L));

            }
        }

        [Offset(0x002B)]
        public int WMT01WaveNumberR
        {
            get { return _WMT01WaveNumberR.ToMidi(); }
            set
            {
                _WMT01WaveNumberR = value.SerializeInt();
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Waveform01R));
            }
        }

        [Offset(0x002F)]
        public byte WMT01WaveGain
        {
            get { return _WMT01WaveGain; }
            set
            {
                _WMT01WaveGain = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0030)]
        public IntegraSwitch WMT01WaveFXMSwitch
        {
            get { return _WMT01WaveFXMSwitch; }
            set
            {
                _WMT01WaveFXMSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0031)]
        public byte WMT01WaveFXMColor
        {
            get { return _WMT01WaveFXMColor; }
            set
            {
                _WMT01WaveFXMColor = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0032)]
        public byte WMT01WaveFXMDepth
        {
            get { return _WMT01WaveFXMDepth; }
            set
            {
                _WMT01WaveFXMDepth = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0033)]
        public IntegraSwitch WMT01WaveTempoSync
        {
            get { return _WMT01WaveTempoSync; }
            set
            {
                _WMT01WaveTempoSync = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0034)]
        public byte WMT01WaveCoarseTune
        {
            get { return _WMT01WaveCoarseTune; }
            set
            {
                _WMT01WaveCoarseTune = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0035)]
        public byte WMT01WaveFineTune
        {
            get { return _WMT01WaveFineTune; }
            set
            {
                _WMT01WaveFineTune = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0036)]
        public byte WMT01WavePan
        {
            get { return _WMT01WavePan; }
            set
            {
                _WMT01WavePan = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0037)]
        public IntegraSwitch WMT01WaveRandomPanSwitch
        {
            get { return _WMT01WaveRandomPanSwitch; }
            set
            {
                _WMT01WaveRandomPanSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0038)]
        public IntegraControlSwitch WMT01WaveAlternatePanSwitch
        {
            get { return _WMT01WaveAlternatePanSwitch; }
            set
            {
                _WMT01WaveAlternatePanSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0039)]
        public byte WMT01WaveLevel
        {
            get { return _WMT01WaveLevel; }
            set
            {
                _WMT01WaveLevel = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x003A)]
        public byte WMT01VelocityRangeLower
        {
            get { return _WMT01VelocityRangeLower; }
            set
            {
                _WMT01VelocityRangeLower = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x003B)]
        public byte WMT01VelocityRangeUpper
        {
            get { return _WMT01VelocityRangeUpper; }
            set
            {
                _WMT01VelocityRangeUpper = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x003C)]
        public byte WMT01VelocityFadeWidthLower
        {
            get { return _WMT01VelocityFadeWidthLower; }
            set
            {
                _WMT01VelocityFadeWidthLower = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x003D)]
        public byte WMT01VelocityFadeWidthUpper
        {
            get { return _WMT01VelocityFadeWidthUpper; }
            set
            {
                _WMT01VelocityFadeWidthUpper = value;
                NotifyPropertyChanged();

            }
        }


        #endregion

        #region Properties: WMT 02

        [Offset(0x003E)]
        public IntegraSwitch WMT02WaveSwitch
        {
            get { return _WMT02WaveSwitch; }
            set
            {
                _WMT02WaveSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x003F)]
        public IntegraWaveGroupType WMT02WaveGroupType
        {
            get { return _WMT02WaveGroupType; }
            set
            {
                _WMT02WaveGroupType = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0040)]
        public int WMT02WaveGroupID
        {
            get { return _WMT02WaveGroupID.ToMidi(); }
            set
            {
                _WMT02WaveGroupID = value.SerializeInt();
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0044)]
        public int WMT02WaveNumberL
        {
            get { return _WMT02WaveNumberL.ToMidi(); }
            set
            {
                _WMT02WaveNumberL = value.SerializeInt();
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0048)]
        public int WMT02WaveNumberR
        {
            get { return _WMT02WaveNumberR.ToMidi(); }
            set
            {
                _WMT02WaveNumberR = value.SerializeInt();
                NotifyPropertyChanged();

            }
        }

        [Offset(0x004C)]
        public byte WMT02WaveGain
        {
            get { return _WMT02WaveGain; }
            set
            {
                _WMT02WaveGain = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x004D)]
        public IntegraSwitch WMT02WaveFXMSwitch
        {
            get { return _WMT02WaveFXMSwitch; }
            set
            {
                _WMT02WaveFXMSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x004E)]
        public byte WMT02WaveFXMColor
        {
            get { return _WMT02WaveFXMColor; }
            set
            {
                _WMT02WaveFXMColor = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x004F)]
        public byte WMT02WaveFXMDepth
        {
            get { return _WMT02WaveFXMDepth; }
            set
            {
                _WMT02WaveFXMDepth = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0050)]
        public IntegraSwitch WMT02WaveTempoSync
        {
            get { return _WMT02WaveTempoSync; }
            set
            {
                _WMT02WaveTempoSync = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0051)]
        public byte WMT02WaveCoarseTune
        {
            get { return _WMT02WaveCoarseTune; }
            set
            {
                _WMT02WaveCoarseTune = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0052)]
        public byte WMT02WaveFineTune
        {
            get { return _WMT02WaveFineTune; }
            set
            {
                _WMT02WaveFineTune = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0053)]
        public byte WMT02WavePan
        {
            get { return _WMT02WavePan; }
            set
            {
                _WMT02WavePan = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0054)]
        public IntegraSwitch WMT02WaveRandomPanSwitch
        {
            get { return _WMT02WaveRandomPanSwitch; }
            set
            {
                _WMT02WaveRandomPanSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0055)]
        public IntegraControlSwitch WMT02WaveAlternatePanSwitch
        {
            get { return _WMT02WaveAlternatePanSwitch; }
            set
            {
                _WMT02WaveAlternatePanSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0056)]
        public byte WMT02WaveLevel
        {
            get { return _WMT02WaveLevel; }
            set
            {
                _WMT02WaveLevel = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0057)]
        public byte WMT02VelocityRangeLower
        {
            get { return _WMT02VelocityRangeLower; }
            set
            {
                _WMT02VelocityRangeLower = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0058)]
        public byte WMT02VelocityRangeUpper
        {
            get { return _WMT02VelocityRangeUpper; }
            set
            {
                _WMT02VelocityRangeUpper = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0059)]
        public byte WMT02VelocityFadeWidthLower
        {
            get { return _WMT02VelocityFadeWidthLower; }
            set
            {
                _WMT02VelocityFadeWidthLower = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x005A)]
        public byte WMT02VelocityFadeWidthUpper
        {
            get { return _WMT02VelocityFadeWidthUpper; }
            set
            {
                _WMT02VelocityFadeWidthUpper = value;
                NotifyPropertyChanged();

            }
        }


        #endregion

        #region Properties: WMT 03

        [Offset(0x005B)]
        public IntegraSwitch WMT03WaveSwitch
        {
            get { return _WMT03WaveSwitch; }
            set
            {
                _WMT03WaveSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x005C)]
        public IntegraWaveGroupType WMT03WaveGroupType
        {
            get { return _WMT03WaveGroupType; }
            set
            {
                _WMT03WaveGroupType = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x005D)]
        public int WMT03WaveGroupID
        {
            get { return _WMT03WaveGroupID.ToMidi(); }
            set
            {
                _WMT03WaveGroupID = value.SerializeInt();
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0061)]
        public int WMT03WaveNumberL
        {
            get { return _WMT03WaveNumberL.ToMidi(); }
            set
            {
                _WMT03WaveNumberL = value.SerializeInt();
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0065)]
        public int WMT03WaveNumberR
        {
            get { return _WMT03WaveNumberR.ToMidi(); }
            set
            {
                _WMT03WaveNumberR = value.SerializeInt();
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0069)]
        public byte WMT03WaveGain
        {
            get { return _WMT03WaveGain; }
            set
            {
                _WMT03WaveGain = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x006A)]
        public IntegraSwitch WMT03WaveFXMSwitch
        {
            get { return _WMT03WaveFXMSwitch; }
            set
            {
                _WMT03WaveFXMSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x006B)]
        public byte WMT03WaveFXMColor
        {
            get { return _WMT03WaveFXMColor; }
            set
            {
                _WMT03WaveFXMColor = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x006C)]
        public byte WMT03WaveFXMDepth
        {
            get { return _WMT03WaveFXMDepth; }
            set
            {
                _WMT03WaveFXMDepth = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x006D)]
        public IntegraSwitch WMT03WaveTempoSync
        {
            get { return _WMT03WaveTempoSync; }
            set
            {
                _WMT03WaveTempoSync = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x006E)]
        public byte WMT03WaveCoarseTune
        {
            get { return _WMT03WaveCoarseTune; }
            set
            {
                _WMT03WaveCoarseTune = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x006F)]
        public byte WMT03WaveFineTune
        {
            get { return _WMT03WaveFineTune; }
            set
            {
                _WMT03WaveFineTune = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0070)]
        public byte WMT03WavePan
        {
            get { return _WMT03WavePan; }
            set
            {
                _WMT03WavePan = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0071)]
        public IntegraSwitch WMT03WaveRandomPanSwitch
        {
            get { return _WMT03WaveRandomPanSwitch; }
            set
            {
                _WMT03WaveRandomPanSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0072)]
        public IntegraControlSwitch WMT03WaveAlternatePanSwitch
        {
            get { return _WMT03WaveAlternatePanSwitch; }
            set
            {
                _WMT03WaveAlternatePanSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0073)]
        public byte WMT03WaveLevel
        {
            get { return _WMT03WaveLevel; }
            set
            {
                _WMT03WaveLevel = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0074)]
        public byte WMT03VelocityRangeLower
        {
            get { return _WMT03VelocityRangeLower; }
            set
            {
                _WMT03VelocityRangeLower = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0075)]
        public byte WMT03VelocityRangeUpper
        {
            get { return _WMT03VelocityRangeUpper; }
            set
            {
                _WMT03VelocityRangeUpper = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0076)]
        public byte WMT03VelocityFadeWidthLower
        {
            get { return _WMT03VelocityFadeWidthLower; }
            set
            {
                _WMT03VelocityFadeWidthLower = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0077)]
        public byte WMT03VelocityFadeWidthUpper
        {
            get { return _WMT03VelocityFadeWidthUpper; }
            set
            {
                _WMT03VelocityFadeWidthUpper = value;
                NotifyPropertyChanged();

            }
        }


        #endregion

        #region Properties: WMT 04

        [Offset(0x0078)]
        public IntegraSwitch WMT04WaveSwitch
        {
            get { return _WMT04WaveSwitch; }
            set
            {
                _WMT04WaveSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0079)]
        public IntegraWaveGroupType WMT04WaveGroupType
        {
            get { return _WMT04WaveGroupType; }
            set
            {
                _WMT04WaveGroupType = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x007A)]
        public int WMT04WaveGroupID
        {
            get { return _WMT04WaveGroupID.ToMidi(); }
            set
            {
                _WMT04WaveGroupID = value.SerializeInt();
                NotifyPropertyChanged();

            }
        }

        [Offset(0x007E)]
        public int WMT04WaveNumberL
        {
            get { return _WMT04WaveNumberL.ToMidi(); }
            set
            {
                _WMT04WaveNumberL = value.SerializeInt();
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0102)]
        public int WMT04WaveNumberR
        {
            get { return _WMT04WaveNumberR.ToMidi(); }
            set
            {
                _WMT04WaveNumberR = value.SerializeInt();
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0106)]
        public byte WMT04WaveGain
        {
            get { return _WMT04WaveGain; }
            set
            {
                _WMT04WaveGain = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0107)]
        public IntegraSwitch WMT04WaveFXMSwitch
        {
            get { return _WMT04WaveFXMSwitch; }
            set
            {
                _WMT04WaveFXMSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0108)]
        public byte WMT04WaveFXMColor
        {
            get { return _WMT04WaveFXMColor; }
            set
            {
                _WMT04WaveFXMColor = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0109)]
        public byte WMT04WaveFXMDepth
        {
            get { return _WMT04WaveFXMDepth; }
            set
            {
                _WMT04WaveFXMDepth = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x010A)]
        public IntegraSwitch WMT04WaveTempoSync
        {
            get { return _WMT04WaveTempoSync; }
            set
            {
                _WMT04WaveTempoSync = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x010B)]
        public byte WMT04WaveCoarseTune
        {
            get { return _WMT04WaveCoarseTune; }
            set
            {
                _WMT04WaveCoarseTune = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x010C)]
        public byte WMT04WaveFineTune
        {
            get { return _WMT04WaveFineTune; }
            set
            {
                _WMT04WaveFineTune = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x010D)]
        public byte WMT04WavePan
        {
            get { return _WMT04WavePan; }
            set
            {
                _WMT04WavePan = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x010E)]
        public IntegraSwitch WMT04WaveRandomPanSwitch
        {
            get { return _WMT04WaveRandomPanSwitch; }
            set
            {
                _WMT04WaveRandomPanSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x010F)]
        public IntegraControlSwitch WMT04WaveAlternatePanSwitch
        {
            get { return _WMT04WaveAlternatePanSwitch; }
            set
            {
                _WMT04WaveAlternatePanSwitch = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0110)]
        public byte WMT04WaveLevel
        {
            get { return _WMT04WaveLevel; }
            set
            {
                _WMT04WaveLevel = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0111)]
        public byte WMT04VelocityRangeLower
        {
            get { return _WMT04VelocityRangeLower; }
            set
            {
                _WMT04VelocityRangeLower = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0112)]
        public byte WMT04VelocityRangeUpper
        {
            get { return _WMT04VelocityRangeUpper; }
            set
            {
                _WMT04VelocityRangeUpper = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0113)]
        public byte WMT04VelocityFadeWidthLower
        {
            get { return _WMT04VelocityFadeWidthLower; }
            set
            {
                _WMT04VelocityFadeWidthLower = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0114)]
        public byte WMT04VelocityFadeWidthUpper
        {
            get { return _WMT04VelocityFadeWidthUpper; }
            set
            {
                _WMT04VelocityFadeWidthUpper = value;
                NotifyPropertyChanged();

            }
        }


        #endregion

        #region Properties: Pitch Envelope

        [Offset(0x0115)]
        public byte PitchEnvDepth
        {
            get { return _PitchEnvDepth; }
            set
            {
                _PitchEnvDepth = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0116)]
        public byte PitchEnvVelocitySens
        {
            get { return _PitchEnvVelocitySens; }
            set
            {
                _PitchEnvVelocitySens = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0117)]
        public byte PitchEnvTime01VelocitySens
        {
            get { return _PitchEnvTime01VelocitySens; }
            set
            {
                _PitchEnvTime01VelocitySens = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0118)]
        public byte PitchEnvTime04VelocitySens
        {
            get { return _PitchEnvTime04VelocitySens; }
            set
            {
                _PitchEnvTime04VelocitySens = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0119)]
        public byte PitchEnvTime01
        {
            get { return _PitchEnvTime01; }
            set
            {
                _PitchEnvTime01 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x011A)]
        public byte PitchEnvTime02
        {
            get { return _PitchEnvTime02; }
            set
            {
                _PitchEnvTime02 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x011B)]
        public byte PitchEnvTime03
        {
            get { return _PitchEnvTime03; }
            set
            {
                _PitchEnvTime03 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x011C)]
        public byte PitchEnvTime04
        {
            get { return _PitchEnvTime04; }
            set
            {
                _PitchEnvTime04 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x011D)]
        public byte PitchEnvLevel00
        {
            get { return _PitchEnvLevel00; }
            set
            {
                _PitchEnvLevel00 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x011E)]
        public byte PitchEnvLevel01
        {
            get { return _PitchEnvLevel01; }
            set
            {
                _PitchEnvLevel01 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x011F)]
        public byte PitchEnvLevel02
        {
            get { return _PitchEnvLevel02; }
            set
            {
                _PitchEnvLevel02 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0120)]
        public byte PitchEnvLevel03
        {
            get { return _PitchEnvLevel03; }
            set
            {
                _PitchEnvLevel03 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0121)]
        public byte PitchEnvLevel04
        {
            get { return _PitchEnvLevel04; }
            set
            {
                _PitchEnvLevel04 = value;
                NotifyPropertyChanged();

            }
        }


        #endregion

        #region Properties: TVF

        [Offset(0x0122)]
        public IntegraTVFFilterType TVFFilterType
        {
            get { return _TVFFilterType; }
            set
            {
                _TVFFilterType = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0123)]
        public byte TVFCutoffFrequency
        {
            get { return _TVFCutoffFrequency; }
            set
            {
                _TVFCutoffFrequency = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0124)]
        public IntegraVelocityCurve TVFCutoffVelocityCurve
        {
            get { return _TVFCutoffVelocityCurve; }
            set
            {
                _TVFCutoffVelocityCurve = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0125)]
        public byte TVFCutoffVelocitySens
        {
            get { return _TVFCutoffVelocitySens; }
            set
            {
                _TVFCutoffVelocitySens = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0126)]
        public byte TVFResonance
        {
            get { return _TVFResonance; }
            set
            {
                _TVFResonance = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0127)]
        public byte TVFResonanceVelocitySens
        {
            get { return _TVFResonanceVelocitySens; }
            set
            {
                _TVFResonanceVelocitySens = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0128)]
        public byte TVFEnvDepth
        {
            get { return _TVFEnvDepth; }
            set
            {
                _TVFEnvDepth = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0129)]
        public IntegraVelocityCurve TVFEnvVelocityCurveType
        {
            get { return _TVFEnvVelocityCurveType; }
            set
            {
                _TVFEnvVelocityCurveType = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x012A)]
        public byte TVFEnvVelocitySens
        {
            get { return _TVFEnvVelocitySens; }
            set
            {
                _TVFEnvVelocitySens = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x012B)]
        public byte TVFEnvTime01VelocitySens
        {
            get { return _TVFEnvTime01VelocitySens; }
            set
            {
                _TVFEnvTime01VelocitySens = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x012C)]
        public byte TVFEnvTime04VelocitySens
        {
            get { return _TVFEnvTime04VelocitySens; }
            set
            {
                _TVFEnvTime04VelocitySens = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x012D)]
        public byte TVFEnvTime01
        {
            get { return _TVFEnvTime01; }
            set
            {
                _TVFEnvTime01 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x012E)]
        public byte TVFEnvTime02
        {
            get { return _TVFEnvTime02; }
            set
            {
                _TVFEnvTime02 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x012F)]
        public byte TVFEnvTime03
        {
            get { return _TVFEnvTime03; }
            set
            {
                _TVFEnvTime03 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0130)]
        public byte TVFEnvTime04
        {
            get { return _TVFEnvTime04; }
            set
            {
                _TVFEnvTime04 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0131)]
        public byte TVFEnvLevel00
        {
            get { return _TVFEnvLevel00; }
            set
            {
                _TVFEnvLevel00 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0132)]
        public byte TVFEnvLevel01
        {
            get { return _TVFEnvLevel01; }
            set
            {
                _TVFEnvLevel01 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0133)]
        public byte TVFEnvLevel02
        {
            get { return _TVFEnvLevel02; }
            set
            {
                _TVFEnvLevel02 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0134)]
        public byte TVFEnvLevel03
        {
            get { return _TVFEnvLevel03; }
            set
            {
                _TVFEnvLevel03 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0135)]
        public byte TVFEnvLevel04
        {
            get { return _TVFEnvLevel04; }
            set
            {
                _TVFEnvLevel04 = value;
                NotifyPropertyChanged();

            }
        }


        #endregion

        #region Properties: TVA

        [Offset(0x0136)]
        public IntegraVelocityCurve TVALevelVelocityCurve
        {
            get { return _TVALevelVelocityCurve; }
            set
            {
                _TVALevelVelocityCurve = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0137)]
        public byte TVALevelVelocitySens
        {
            get { return _TVALevelVelocitySens; }
            set
            {
                _TVALevelVelocitySens = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0138)]
        public byte TVAEnvTime01VelocitySens
        {
            get { return _TVAEnvTime01VelocitySens; }
            set
            {
                _TVAEnvTime01VelocitySens = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0139)]
        public byte TVAEnvTime04VelocitySens
        {
            get { return _TVAEnvTime04VelocitySens; }
            set
            {
                _TVAEnvTime04VelocitySens = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x013A)]
        public byte TVAEnvTime01
        {
            get { return _TVAEnvTime01; }
            set
            {
                _TVAEnvTime01 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x013B)]
        public byte TVAEnvTime02
        {
            get { return _TVAEnvTime02; }
            set
            {
                _TVAEnvTime02 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x013C)]
        public byte TVAEnvTime03
        {
            get { return _TVAEnvTime03; }
            set
            {
                _TVAEnvTime03 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x013D)]
        public byte TVAEnvTime04
        {
            get { return _TVAEnvTime04; }
            set
            {
                _TVAEnvTime04 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x013E)]
        public byte TVAEnvLevel01
        {
            get { return _TVAEnvLevel01; }
            set
            {
                _TVAEnvLevel01 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x013F)]
        public byte TVAEnvLevel02
        {
            get { return _TVAEnvLevel02; }
            set
            {
                _TVAEnvLevel02 = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0140)]
        public byte TVAEnvLevel03
        {
            get { return _TVAEnvLevel03; }
            set
            {
                _TVAEnvLevel03 = value;
                NotifyPropertyChanged();

            }
        }


        #endregion

        #region Properties: Misc

        [Offset(0x0141)]
        public IntegraSwitch OneShotMode
        {
            get { return _OneShotMode; }
            set
            {
                _OneShotMode = value;
                NotifyPropertyChanged();

            }
        }

        #endregion

        #endregion
    }
}
