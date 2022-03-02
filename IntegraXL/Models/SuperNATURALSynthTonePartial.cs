using IntegraXL.Core;
using IntegraXL.Extensions;
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
        [Offset(0x0003)] byte _OSCPitch;
        [Offset(0x0004)] byte _OSCDetune;
        [Offset(0x0005)] byte _OSCPulseWidthModDepth;
        [Offset(0x0006)] byte _OSCPulseWidth;
        [Offset(0x0007)] byte _OSCPitchEnvAttack;
        [Offset(0x0008)] byte _OSCPitchEnvDecay;
        [Offset(0x0009)] byte _OSCPitchEnvDepth;

        #endregion

        #region Fields: Filter

        [Offset(0x000A)] IntegraFilterMode _FilterMode;
        [Offset(0x000B)] byte _FilterSlope;
        [Offset(0x000C)] byte _FilterCutoff;
        [Offset(0x000D)] byte _FilterCutoffKeyFollow;
        [Offset(0x000E)] byte _FilterEnvVelocitySens;
        [Offset(0x000F)] byte _FilterResonance;
        [Offset(0x0010)] byte _FilterEnvAttack;
        [Offset(0x0011)] byte _FilterEnvDecay;
        [Offset(0x0012)] byte _FilterEnvSustain;
        [Offset(0x0013)] byte _FilterEnvRelease;
        [Offset(0x0014)] byte _FilterEnvDepth;

        #endregion

        #region Fields: Amplifier

        [Offset(0x0015)] byte _AmpLevel;
        [Offset(0x0016)] byte _AmpLevelVelocitySens;
        [Offset(0x0017)] byte _AmpEnvAttack;
        [Offset(0x0018)] byte _AmpEnvDecay;
        [Offset(0x0019)] byte _AmpEnvSustain;
        [Offset(0x001A)] byte _AmpEnvRelease;
        [Offset(0x001B)] byte _AmpPan;

        #endregion

        #region Fields: LFO

        [Offset(0x001C)] IntegraLFOShape _LFOShape;
        [Offset(0x001D)] byte _LFORate;
        [Offset(0x001E)] IntegraSwitch _LFOTempoSyncSwitch;
        [Offset(0x001F)] byte _LFOTempoSyncNote;
        [Offset(0x0020)] byte _LFOFadeTime;
        [Offset(0x0021)] IntegraSwitch _LFOKeyTrigger;
        [Offset(0x0022)] byte _LFOPitchDepth;
        [Offset(0x0023)] byte _LFOFilterDepth;
        [Offset(0x0024)] byte _LFOAmpDepth;
        [Offset(0x0025)] byte _LFOPanDepth;

        #endregion

        #region Fields: Modulation

        [Offset(0x0026)] IntegraLFOShape _ModulationLFOShape;
        [Offset(0x0027)] byte _ModulationLFORate;
        [Offset(0x0028)] IntegraSwitch _ModulationLFOTempoSyncSwitch;
        [Offset(0x0029)] byte _ModulationLFOTempoSyncNote;
        [Offset(0x002A)] byte _OSCPulseWidthShift;
        [Offset(0x002C)] byte _ModulationLFOPitchDepth;
        [Offset(0x002D)] byte _ModulationLFOFilterDepth;
        [Offset(0x002E)] byte _ModulationLFOAmpDepth;
        [Offset(0x002F)] byte _ModulationLFOPanDepth;

        #endregion

        #region Fields: General

        [Offset(0x0030)] byte _CutoffAftertouchSens;
        [Offset(0x0031)] byte _LevelAftertouchSens;
        [Offset(0x0034)] byte _WaveGain;
        [Offset(0x0035)] int _WaveNumber;
        [Offset(0x0039)] byte _HPFCutoff;
        [Offset(0x003A)] byte _SuperSawDetune;
        [Offset(0x003B)] byte _ModulationLFORateControl;
        [Offset(0x003C)] byte _AmpLevelKeyFollow;

        #endregion

        #endregion

        #region Constructor

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "The class is created by reflection.")]
        private SuperNATURALSynthTonePartial(SuperNATURALSynthTone tone, int partial) : base(tone.Device) 
        {
            Address += tone.Address;
            Address += (partial) << 8;
            Partial  = (IntegraSNSynthToneParts)partial;
        }

        #endregion

        #region Properties

        public IntegraSNSynthToneParts Partial { get; private set; }

        #endregion

        #region Properties: INTEGRA-7

        #region Properties: Oscillator

        [Offset(0x0000)]
        public IntegraOSCWave OSCWave
        {
            get { return _OSCWave; }
            set
            {
                _OSCWave = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0001)]
        public IntegraOSCWaveVariation OSCWaveVariation
        {
            get { return _OSCWaveVariation; }
            set
            {
                _OSCWaveVariation = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0003)]
        public byte OSCPitch
        {
            get { return _OSCPitch; }
            set
            {
                _OSCPitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0004)]
        public byte OSCDetune
        {
            get { return _OSCDetune; }
            set
            {
                _OSCDetune = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0005)]
        public byte OSCPulseWidthModDepth
        {
            get { return _OSCPulseWidthModDepth; }
            set
            {
                _OSCPulseWidthModDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public byte OSCPulseWidth
        {
            get { return _OSCPulseWidth; }
            set
            {
                _OSCPulseWidth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0007)]
        public byte OSCPitchEnvAttack
        {
            get { return _OSCPitchEnvAttack; }
            set
            {
                _OSCPitchEnvAttack = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0008)]
        public byte OSCPitchEnvDecay
        {
            get { return _OSCPitchEnvDecay; }
            set
            {
                _OSCPitchEnvDecay = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0009)]
        public byte OSCPitchEnvDepth
        {
            get { return _OSCPitchEnvDepth; }
            set
            {
                _OSCPitchEnvDepth = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #region Properties: Filter

        [Offset(0x000A)]
        public IntegraFilterMode FilterMode
        {
            get { return _FilterMode; }
            set
            {
                _FilterMode = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000B)]
        public byte FilterSlope
        {
            get { return _FilterSlope; }
            set
            {
                _FilterSlope = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000C)]
        public byte FilterCutoff
        {
            get { return _FilterCutoff; }
            set
            {
                _FilterCutoff = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000D)]
        public byte FilterCutoffKeyFollow
        {
            get { return _FilterCutoffKeyFollow; }
            set
            {
                _FilterCutoffKeyFollow = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000E)]
        public byte FilterEnvVelocitySens
        {
            get { return _FilterEnvVelocitySens; }
            set
            {
                _FilterEnvVelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000F)]
        public byte FilterResonance
        {
            get { return _FilterResonance; }
            set
            {
                _FilterResonance = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0010)]
        public byte FilterEnvAttack
        {
            get { return _FilterEnvAttack; }
            set
            {
                _FilterEnvAttack = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0011)]
        public byte FilterEnvDecay
        {
            get { return _FilterEnvDecay; }
            set
            {
                _FilterEnvDecay = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0012)]
        public byte FilterEnvSustain
        {
            get { return _FilterEnvSustain; }
            set
            {
                _FilterEnvSustain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0013)]
        public byte FilterEnvRelease
        {
            get { return _FilterEnvRelease; }
            set
            {
                _FilterEnvRelease = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0014)]
        public byte FilterEnvDepth
        {
            get { return _FilterEnvDepth; }
            set
            {
                _FilterEnvDepth = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #region Properties: Amplifier

        [Offset(0x0015)]
        public byte AmpLevel
        {
            get { return _AmpLevel; }
            set
            {
                _AmpLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0016)]
        public byte AmpLevelVelocitySens
        {
            get { return _AmpLevelVelocitySens; }
            set
            {
                _AmpLevelVelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0017)]
        public byte AmpEnvAttack
        {
            get { return _AmpEnvAttack; }
            set
            {
                _AmpEnvAttack = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0018)]
        public byte AmpEnvDecay
        {
            get { return _AmpEnvDecay; }
            set
            {
                _AmpEnvDecay = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0019)]
        public byte AmpEnvSustain
        {
            get { return _AmpEnvSustain; }
            set
            {
                _AmpEnvSustain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001A)]
        public byte AmpEnvRelease
        {
            get { return _AmpEnvRelease; }
            set
            {
                _AmpEnvRelease = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001B)]
        public byte AmpPan
        {
            get { return _AmpPan; }
            set
            {
                _AmpPan = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #region Properties: LFO

        [Offset(0x001C)]
        public IntegraLFOShape LFOShape
        {
            get { return _LFOShape; }
            set
            {
                _LFOShape = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001D)]
        public byte LFORate
        {
            get { return _LFORate; }
            set
            {
                _LFORate = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001E)]
        public IntegraSwitch LFOTempoSyncSwitch
        {
            get { return _LFOTempoSyncSwitch; }
            set
            {
                _LFOTempoSyncSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001F)]
        public byte LFOTempoSyncNote
        {
            get { return _LFOTempoSyncNote; }
            set
            {
                _LFOTempoSyncNote = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0020)]
        public byte LFOFadeTime
        {
            get { return _LFOFadeTime; }
            set
            {
                _LFOFadeTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0021)]
        public IntegraSwitch LFOKeyTrigger
        {
            get { return _LFOKeyTrigger; }
            set
            {
                _LFOKeyTrigger = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0022)]
        public byte LFOPitchDepth
        {
            get { return _LFOPitchDepth; }
            set
            {
                _LFOPitchDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0023)]
        public byte LFOFilterDepth
        {
            get { return _LFOFilterDepth; }
            set
            {
                _LFOFilterDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0024)]
        public byte LFOAmpDepth
        {
            get { return _LFOAmpDepth; }
            set
            {
                _LFOAmpDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0025)]
        public byte LFOPanDepth
        {
            get { return _LFOPanDepth; }
            set
            {
                _LFOPanDepth = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #region Properties: Modulation

        [Offset(0x0026)]
        public IntegraLFOShape ModulationLFOShape
        {
            get { return _ModulationLFOShape; }
            set
            {
                _ModulationLFOShape = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0027)]
        public byte ModulationLFORate
        {
            get { return _ModulationLFORate; }
            set
            {
                _ModulationLFORate = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0028)]
        public IntegraSwitch ModulationLFOTempoSyncSwitch
        {
            get { return _ModulationLFOTempoSyncSwitch; }
            set
            {
                _ModulationLFOTempoSyncSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0029)]
        public byte ModulationLFOTempoSyncNote
        {
            get { return _ModulationLFOTempoSyncNote; }
            set
            {
                _ModulationLFOTempoSyncNote = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002A)]
        public byte OSCPulseWidthShift
        {
            get { return _OSCPulseWidthShift; }
            set
            {
                _OSCPulseWidthShift = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002C)]
        public byte ModulationLFOPitchDepth
        {
            get { return _ModulationLFOPitchDepth; }
            set
            {
                _ModulationLFOPitchDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002D)]
        public byte ModulationLFOFilterDepth
        {
            get { return _ModulationLFOFilterDepth; }
            set
            {
                _ModulationLFOFilterDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002E)]
        public byte ModulationLFOAmpDepth
        {
            get { return _ModulationLFOAmpDepth; }
            set
            {
                _ModulationLFOAmpDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002F)]
        public byte ModulationLFOPanDepth
        {
            get { return _ModulationLFOPanDepth; }
            set
            {
                _ModulationLFOPanDepth = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #region Properties: General

        [Offset(0x0030)]
        public byte CutoffAftertouchSens
        {
            get { return _CutoffAftertouchSens; }
            set
            {
                _CutoffAftertouchSens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0031)]
        public byte LevelAftertouchSens
        {
            get { return _LevelAftertouchSens; }
            set
            {
                _LevelAftertouchSens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0034)]
        public byte WaveGain
        {
            get { return _WaveGain; }
            set
            {
                _WaveGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0035)]
        public int WaveNumber
        {
            get { return _WaveNumber.ToMidi(); }
            set
            {
                _WaveNumber = value.SerializeInt();
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0039)]
        public byte HPFCutoff
        {
            get { return _HPFCutoff; }
            set
            {
                _HPFCutoff = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003A)]
        public byte SuperSawDetune
        {
            get { return _SuperSawDetune; }
            set
            {
                _SuperSawDetune = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003B)]
        public byte ModulationLFORateControl
        {
            get { return _ModulationLFORateControl; }
            set
            {
                _ModulationLFORateControl = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003C)]
        public byte AmpLevelKeyFollow
        {
            get { return _AmpLevelKeyFollow; }
            set
            {
                _AmpLevelKeyFollow = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #endregion

       
    }
}
