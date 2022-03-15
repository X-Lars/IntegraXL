using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.Templates;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace IntegraXL.Models
{
    [Integra(0x00002000, 0x00001000, 3)]
    public sealed class SuperNATURALSynthTonePartials : IntegraCollection<SuperNATURALSynthTonePartial>
    {
        #region Constructor

        /// <summary>
        /// Creates a new uninitialized MIDI enabled partial collection.
        /// </summary>
        /// <param name="device">The device for data transmission.</param>
        internal SuperNATURALSynthTonePartials(SuperNATURALSynthTone tone) : base(tone.Device, false)
        {
            Address = tone.Address;

            IntegraRequest request = new(Attribute.Request);

            Requests.Add(request);

            for (int i = 0; i < Size; i++)
            {
                SuperNATURALSynthTonePartial? partial = Activator.CreateInstance(typeof(SuperNATURALSynthTonePartial), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { tone, i }, null) as SuperNATURALSynthTonePartial;

                Debug.Assert(partial != null);

                Add(partial);
            }

            Connect();
        }

        #endregion

        #region Overrides: IntegraModel

        public override bool IsInitialized
        {
            get => Collection.All(x => x.IsInitialized);
        }

        /// <summary>
        /// Handles system exclusive events that match the partial collection address after masking the partial byte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks><i>If the partial collection is initialized, the event handler will be released.</i></remarks>
        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            if (!IsInitialized)
            {
                if(e.SystemExclusive.Address.InRange(this.First().Address, this.Last().Address))
                {
                    Device.ReportProgress(this, Collection.Where(x => x.IsInitialized).Count(), Size , e.SystemExclusive.Address.GetTemporaryTonePart());
                }
            }
        }

        internal override bool Initialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


    [Integra(0x00002000, 0x0000003D)]
    public class SuperNATURALSynthTonePartial : IntegraModel<SuperNATURALSynthTonePartial>
    {
        #region Fields: INTEGRA-7

        #region Fields: Oscillator

        [Offset(0x0000)] IntegraOSCWave _OSCWave;
        [Offset(0x0001)] IntegraOSCWaveVariation _OSCWaveVariation;

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Field exist to match the model's structure size.")]
        [Offset(0x0002)] private readonly byte RESERVED01;

        [Offset(0x0003)] private byte _OSCPitch;
        [Offset(0x0004)] private byte _OSCDetune;
        [Offset(0x0005)] private byte _OSCPulseWidthModDepth;
        [Offset(0x0006)] private byte _OSCPulseWidth;
        [Offset(0x0007)] private byte _OSCPitchEnvAttack;
        [Offset(0x0008)] private byte _OSCPitchEnvDecay;
        [Offset(0x0009)] private byte _OSCPitchEnvDepth;

        #endregion

        #region Fields: Filter

        [Offset(0x000A)] private IntegraFilterMode _FilterMode;
        [Offset(0x000B)] private IntegraFilterSlope _FilterSlope;
        [Offset(0x000C)] private byte _FilterCutoff;
        [Offset(0x000D)] private byte _FilterCutoffKeyFollow;
        [Offset(0x000E)] private byte _FilterEnvVelocitySens;
        [Offset(0x000F)] private byte _FilterResonance;
        [Offset(0x0010)] private byte _FilterEnvAttack;
        [Offset(0x0011)] private byte _FilterEnvDecay;
        [Offset(0x0012)] private byte _FilterEnvSustain;
        [Offset(0x0013)] private byte _FilterEnvRelease;
        [Offset(0x0014)] private byte _FilterEnvDepth;

        #endregion

        #region Fields: Amplifier

        [Offset(0x0015)] private byte _AmpLevel;
        [Offset(0x0016)] private byte _AmpLevelVelocitySens;
        [Offset(0x0017)] private byte _AmpEnvAttack;
        [Offset(0x0018)] private byte _AmpEnvDecay;
        [Offset(0x0019)] private byte _AmpEnvSustain;
        [Offset(0x001A)] private byte _AmpEnvRelease;
        [Offset(0x001B)] private byte _AmpPan;

        #endregion

        #region Fields: LFO

        [Offset(0x001C)] IntegraLFOShape _LFOShape;
        [Offset(0x001D)] byte _LFORate;
        [Offset(0x001E)] IntegraSwitch _LFOTempoSyncSwitch;
        [Offset(0x001F)] IntegraTempoSyncNote _LFOTempoSyncNote;
        [Offset(0x0020)] byte _LFOFadeTime;
        [Offset(0x0021)] IntegraSwitch _LFOKeyTrigger;
        [Offset(0x0022)] byte _LFOPitchDepth;
        [Offset(0x0023)] byte _LFOFilterDepth;
        [Offset(0x0024)] byte _LFOAmpDepth;
        [Offset(0x0025)] byte _LFOPanDepth;

        #endregion

        #region Fields: Modulation

        [Offset(0x0026)] private IntegraLFOShape _ModLFOShape;
        [Offset(0x0027)] private byte _ModLFORate;
        [Offset(0x0028)] private IntegraSwitch _ModLFOTempoSyncSwitch;
        [Offset(0x0029)] private IntegraTempoSyncNote _ModLFOTempoSyncNote;
        [Offset(0x002A)] private byte _OSCPulseWidthShift;
        [Offset(0x002B)] private readonly byte RESERVED02;
        [Offset(0x002C)] private byte _ModLFOPitchDepth;
        [Offset(0x002D)] private byte _ModLFOFilterDepth;
        [Offset(0x002E)] private byte _ModLFOAmpDepth;
        [Offset(0x002F)] private byte _ModLFOPanDepth;

        #endregion

        #region Fields: General

        [Offset(0x0030)] private byte _CutoffAftertouchSens;
        [Offset(0x0031)] private byte _LevelAftertouchSens;
        [Offset(0x0032)] private readonly byte[] RESERVED03 = new byte[2];
        [Offset(0x0034)] private byte _WaveGain;
        [Offset(0x0035)] private int _WaveNumber;
        [Offset(0x0039)] private byte _HPFCutoff;
        [Offset(0x003A)] private byte _SuperSawDetune;
        [Offset(0x003B)] private byte _ModLFORateControl;
        [Offset(0x003C)] private byte _AmpLevelKeyFollow;

        #endregion

        #endregion

        #region Constructor

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "The class is created by reflection.")]
        private SuperNATURALSynthTonePartial(SuperNATURALSynthTone tone, int partial) : base(tone.Device) 
        {
            Address += tone.Address;
            Address += (partial) << 8;

            Debug.Print($"{Address}");

            Index = partial;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected partial.
        /// </summary>
        public IntegraSNSynthToneParts Partial => (IntegraSNSynthToneParts)Index;
        
        /// <summary>
        /// Gets the index of the partial.
        /// </summary>
        public int Index { get; }

        public WaveformTemplate WaveForm
        {
            get
            {
                // TODO: Default template for non PCM wave
                return IntegraWaveformLookup.Template(IntegraWaveFormTypes.SNS, IntegraWaveFormBanks.INT, WaveNumber);
            }
        }


        #endregion

        #region Properties: INTEGRA-7

        #region Properties: Oscillator

        [Offset(0x0000)]
        public IntegraOSCWave OSCWave
        {
            get => _OSCWave;
            set
            {
                if (_OSCWave != value)
                {
                    _OSCWave = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0001)]
        public IntegraOSCWaveVariation OSCWaveVariation
        {
            get => _OSCWaveVariation;
            set
            {
                if (_OSCWaveVariation != value)
                {
                    _OSCWaveVariation = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0003)]
        public int OSCPitch
        {
            get => _OSCPitch.Deserialize(64);
            set
            {
                if (OSCPitch != value)
                {
                    _OSCPitch = value.Serialize(64).Clamp(40, 88);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0004)]
        public int OSCDetune
        {
            get => _OSCDetune.Deserialize(64);
            set
            {
                if (OSCDetune != value)
                {
                    _OSCDetune = value.Serialize(64).Clamp(14, 114);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0005)]
        public byte OSCPulseWidthModDepth
        {
            get => _OSCPulseWidthModDepth;
            set
            {
                if (_OSCPulseWidthModDepth != value)
                {
                    _OSCPulseWidthModDepth = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public byte OSCPulseWidth
        {
            get => _OSCPulseWidth;
            set
            {
                if (_OSCPulseWidth != value)
                {
                    _OSCPulseWidth = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0007)]
        public byte OSCPitchEnvAttack
        {
            get => _OSCPitchEnvAttack;
            set
            {
                if (_OSCPitchEnvAttack != value)
                {
                    _OSCPitchEnvAttack = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0008)]
        public byte OSCPitchEnvDecay
        {
            get => _OSCPitchEnvDecay;
            set
            {
                if (_OSCPitchEnvDecay != value)
                {
                    _OSCPitchEnvDecay = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0009)]
        public int OSCPitchEnvDepth
        {
            get => _OSCPitchEnvDepth.Deserialize(64);
            set
            {
                if (OSCPitchEnvDepth != value)
                {
                    _OSCPitchEnvDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: Filter

        [Offset(0x000A)]
        public IntegraFilterMode FilterMode
        {
            get => _FilterMode;
            set
            {
                if (_FilterMode != value)
                {
                    _FilterMode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000B)]
        public IntegraFilterSlope FilterSlope
        {
            get => _FilterSlope;
            set
            {
                if (_FilterSlope != value)
                {
                    _FilterSlope = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public byte FilterCutoff
        {
            get => _FilterCutoff;
            set
            {
                if (_FilterCutoff != value)
                {
                    _FilterCutoff = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000D)]
        public int FilterCutoffKeyFollow
        {
            get => _FilterCutoffKeyFollow.Deserialize(64, 10);
            set
            {
                if (FilterCutoffKeyFollow != value)
                {
                    _FilterCutoffKeyFollow = value.Serialize(64, 10).Clamp(54, 74);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000E)]
        public int FilterEnvVelocitySens
        {
            get => _FilterEnvVelocitySens.Deserialize(64);
            set
            {
                if (FilterEnvVelocitySens != value)
                {
                    _FilterEnvVelocitySens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000F)]
        public byte FilterResonance
        {
            get => _FilterResonance;
            set
            {
                if (_FilterResonance != value)
                {
                    _FilterResonance = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0010)]
        public byte FilterEnvAttack
        {
            get => _FilterEnvAttack;
            set
            {
                if (_FilterEnvAttack != value)
                {
                    _FilterEnvAttack = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0011)]
        public byte FilterEnvDecay
        {
            get => _FilterEnvDecay;
            set
            {
                if (_FilterEnvDecay != value)
                {
                    _FilterEnvDecay = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0012)]
        public byte FilterEnvSustain
        {
            get => _FilterEnvSustain;
            set
            {
                if (_FilterEnvSustain != value)
                {
                    _FilterEnvSustain = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0013)]
        public byte FilterEnvRelease
        {
            get => _FilterEnvRelease;
            set
            {
                if (_FilterEnvRelease != value)
                {
                    _FilterEnvRelease = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0014)]
        public int FilterEnvDepth
        {
            get => _FilterEnvDepth.Deserialize(64);
            set
            {
                if (FilterEnvDepth != value)
                {
                    _FilterEnvDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: Amplifier

        [Offset(0x0015)]
        public byte AmpLevel
        {
            get => _AmpLevel;
            set
            {
                if (_AmpLevel != value)
                {
                    _AmpLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0016)]
        public int AmpLevelVelocitySens
        {
            get => _AmpLevelVelocitySens.Deserialize(64);
            set
            {
                if (AmpLevelVelocitySens != value)
                {
                    _AmpLevelVelocitySens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0017)]
        public byte AmpEnvAttack
        {
            get => _AmpEnvAttack;
            set
            {
                if (_AmpEnvAttack != value)
                {
                    _AmpEnvAttack = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0018)]
        public byte AmpEnvDecay
        {
            get => _AmpEnvDecay;
            set
            {
                if (_AmpEnvDecay != value)
                {
                    _AmpEnvDecay = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0019)]
        public byte AmpEnvSustain
        {
            get => _AmpEnvSustain;
            set
            {
                if (_AmpEnvSustain != value)
                {
                    _AmpEnvSustain = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001A)]
        public byte AmpEnvRelease
        {
            get => _AmpEnvRelease;
            set
            {
                if (_AmpEnvRelease != value)
                {
                    _AmpEnvRelease = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001B)]
        public byte AmpPan
        {
            get => _AmpPan;
            set
            {
                if (_AmpPan != value)
                {
                    _AmpPan = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: LFO

        [Offset(0x001C)]
        public IntegraLFOShape LFOShape
        {
            get => _LFOShape;
            set
            {
                if (_LFOShape != value)
                {
                    _LFOShape = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001D)]
        public byte LFORate
        {
            get => _LFORate;
            set
            {
                if (_LFORate != value)
                {
                    _LFORate = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001E)]
        public IntegraSwitch LFOTempoSyncSwitch
        {
            get => _LFOTempoSyncSwitch;
            set
            {
                if (_LFOTempoSyncSwitch != value)
                {
                    _LFOTempoSyncSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001F)]
        public IntegraTempoSyncNote LFOTempoSyncNote
        {
            get => _LFOTempoSyncNote;
            set
            {
                if (_LFOTempoSyncNote != value)
                {
                    _LFOTempoSyncNote = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0020)]
        public byte LFOFadeTime
        {
            get => _LFOFadeTime;
            set
            {
                if (_LFOFadeTime != value)
                {
                    _LFOFadeTime = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0021)]
        public IntegraSwitch LFOKeyTrigger
        {
            get => _LFOKeyTrigger;
            set
            {
                if (_LFOKeyTrigger != value)
                {
                    _LFOKeyTrigger = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0022)]
        public int LFOPitchDepth
        {
            get => _LFOPitchDepth.Deserialize(64);
            set
            {
                if (LFOPitchDepth != value)
                {
                    _LFOPitchDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0023)]
        public int LFOFilterDepth
        {
            get => _LFOFilterDepth.Deserialize(64);
            set
            {
                if (LFOFilterDepth != value)
                {
                    _LFOFilterDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0024)]
        public int LFOAmpDepth
        {
            get => _LFOAmpDepth.Deserialize(64);
            set
            {
                if (LFOAmpDepth != value)
                {
                    _LFOAmpDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0025)]
        public int LFOPanDepth
        {
            get => _LFOPanDepth.Deserialize(64);
            set
            {
                if (LFOPanDepth != value)
                {
                    _LFOPanDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: Modulation

        [Offset(0x0026)]
        public IntegraLFOShape ModLFOShape
        {
            get => _ModLFOShape;
            set
            {
                if (_ModLFOShape != value)
                {
                    _ModLFOShape = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0027)]
        public byte ModLFORate
        {
            get => _ModLFORate;
            set
            {
                if (_ModLFORate != value)
                {
                    _ModLFORate = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0028)]
        public IntegraSwitch ModLFOTempoSyncSwitch
        {
            get => _ModLFOTempoSyncSwitch;
            set
            {
                if (_ModLFOTempoSyncSwitch != value)
                {
                    _ModLFOTempoSyncSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0029)]
        public IntegraTempoSyncNote ModLFOTempoSyncNote
        {
            get => _ModLFOTempoSyncNote;
            set
            {
                if (_ModLFOTempoSyncNote != value)
                {
                    _ModLFOTempoSyncNote = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002A)]
        public byte OSCPulseWidthShift
        {
            get => _OSCPulseWidthShift;
            set
            {
                if (_OSCPulseWidthShift != value)
                {
                    _OSCPulseWidthShift = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002C)]
        public int ModLFOPitchDepth
        {
            get => _ModLFOPitchDepth.Deserialize(64);
            set
            {
                if (ModLFOPitchDepth != value)
                {
                    _ModLFOPitchDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002D)]
        public int ModLFOFilterDepth
        {
            get => _ModLFOFilterDepth.Deserialize(64);
            set
            {
                if (ModLFOFilterDepth != value)
                {
                    _ModLFOFilterDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002E)]
        public int ModLFOAmpDepth
        {
            get => _ModLFOAmpDepth.Deserialize(64);
            set
            {
                if (ModLFOAmpDepth != value)
                {
                    _ModLFOAmpDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002F)]
        public int ModLFOPanDepth
        {
            get => _ModLFOPanDepth.Deserialize(64);
            set
            {
                if (ModLFOPanDepth != value)
                {
                    _ModLFOPanDepth = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }


        #endregion

        #region Properties: General

        [Offset(0x0030)]
        public int CutoffAftertouchSens
        {
            get => _CutoffAftertouchSens.Deserialize(64);
            set
            {
                if (CutoffAftertouchSens != value)
                {
                    _CutoffAftertouchSens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0031)]
        public int LevelAftertouchSens
        {
            get => _LevelAftertouchSens.Deserialize(64);
            set
            {
                if (LevelAftertouchSens != value)
                {
                    _LevelAftertouchSens = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
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
        public int WaveNumber
        {
            get => _WaveNumber.Deserialize();
            set
            {
                if (WaveNumber != value)
                {
                    _WaveNumber = value.Clamp(0, IntegraConstants.WAVE_COUNT_SNS).SerializeInt();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0039)]
        public byte HPFCutoff
        {
            get => _HPFCutoff;
            set
            {
                if (_HPFCutoff != value)
                {
                    _HPFCutoff = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003A)]
        public byte SuperSawDetune
        {
            get => _SuperSawDetune;
            set
            {
                if (_SuperSawDetune != value)
                {
                    _SuperSawDetune = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003B)]
        public int ModLFORateControl
        {
            get => _ModLFORateControl.Deserialize(64);
            set
            {
                if (ModLFORateControl != value)
                {
                    _ModLFORateControl = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003C)]
        public int AmpLevelKeyFollow
        {
            get => _AmpLevelKeyFollow.Deserialize(64, 10);
            set
            {
                if (AmpLevelKeyFollow != value)
                {
                    _AmpLevelKeyFollow = value.Serialize(64, 10).Clamp(54, 74);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #endregion

        #region Enumerations

        public List<string> PanValues
        {
            get { return IntegraPan.Values; }
        }

        #endregion
    }
}
