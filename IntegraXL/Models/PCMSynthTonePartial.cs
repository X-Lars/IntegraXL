using IntegraXL.Core;
using System.Diagnostics;
using IntegraXL.Extensions;
using System.Reflection;

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
            get => Collection.Last().IsInitialized;
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

        protected override bool Initialize(byte[] data)
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
        [Offset(0x000F)] byte _ChorusSendLevel;
        [Offset(0x0010)] byte _ReverbSendLevel;
        [Offset(0x0012)] IntegraSwitch _ReceiveBender;
        [Offset(0x0013)] IntegraSwitch _ReceiveExpression;
        [Offset(0x0014)] IntegraSwitch _ReceiveHold;
        [Offset(0x0016)] IntegraSwitch _RedamperSwitch;
        [Offset(0x0017)] IntegraControlSwitch _Control01Switch01;
        [Offset(0x0018)] IntegraControlSwitch _Control01Switch02;
        [Offset(0x0019)] IntegraControlSwitch _Control01Switch03;
        [Offset(0x001A)] IntegraControlSwitch _Control01Switch04;
        [Offset(0x001B)] IntegraControlSwitch _Control02Switch01;
        [Offset(0x001C)] IntegraControlSwitch _Control02Switch02;
        [Offset(0x001D)] IntegraControlSwitch _Control02Switch03;
        [Offset(0x001E)] IntegraControlSwitch _Control02Switch04;
        [Offset(0x001F)] IntegraControlSwitch _Control03Switch01;
        [Offset(0x0020)] IntegraControlSwitch _Control03Switch02;
        [Offset(0x0021)] IntegraControlSwitch _Control03Switch03;
        [Offset(0x0022)] IntegraControlSwitch _Control03Switch04;
        [Offset(0x0023)] IntegraControlSwitch _Control04Switch01;
        [Offset(0x0024)] IntegraControlSwitch _Control04Switch02;
        [Offset(0x0025)] IntegraControlSwitch _Control04Switch03;
        [Offset(0x0026)] IntegraControlSwitch _Control04Switch04;

        #endregion

        #region Fields: Waveform

        [Offset(0x0027)] IntegraWaveGroupType _WaveGroupType;
        [Offset(0x0028)] int _WaveGroupID;
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
        [Offset(0x003C)] byte _PitchEnvTime01VelocitySens;
        [Offset(0x003D)] byte _PitchEnvTime04VelocitySens;
        [Offset(0x003E)] byte _PitchEnvTimeKeyFollow;
        [Offset(0x003F)] byte _PitchEnvTime01;
        [Offset(0x0040)] byte _PitchEnvTime02;
        [Offset(0x0041)] byte _PitchEnvTime03;
        [Offset(0x0042)] byte _PitchEnvTime04;
        [Offset(0x0043)] byte _PitchEnvLevel00;
        [Offset(0x0044)] byte _PitchEnvLevel01;
        [Offset(0x0045)] byte _PitchEnvLevel02;
        [Offset(0x0046)] byte _PitchEnvLevel03;
        [Offset(0x0047)] byte _PitchEnvLevel04;

        #endregion

        #region Fields: TVF

        [Offset(0x0048)] IntegraTVFFilterType _TVFFilterType;
        [Offset(0x0049)] byte _TVFCutoffFrequency;
        [Offset(0x004A)] byte _TVFCutoffKeyFollow;
        [Offset(0x004B)] IntegraVelocityCurve _TVFCutoffVelocityCurve;
        [Offset(0x004C)] byte _TVFCutoffVelocitySens;
        [Offset(0x004E)] byte _TVFResonance;
        [Offset(0x004D)] byte _TVFResonanceVelocitySens;
        [Offset(0x004F)] byte _TVFEnvDepth;
        [Offset(0x0050)] IntegraVelocityCurve _TVFEnvVelocityCurve;
        [Offset(0x0051)] byte _TVFEnvVelocitySens;
        [Offset(0x0052)] byte _TVFEnvTime01VelocitySens;
        [Offset(0x0053)] byte _TVFEnvTime04VelocitySens;
        [Offset(0x0054)] byte _TVFEnvTimeKeyFollow;
        [Offset(0x0055)] byte _TVFTime01;
        [Offset(0x0056)] byte _TVFTime02;
        [Offset(0x0057)] byte _TVFTime03;
        [Offset(0x0058)] byte _TVFTime04;
        [Offset(0x0059)] byte _TVFLevel00;
        [Offset(0x005A)] byte _TVFLevel01;
        [Offset(0x005B)] byte _TVFLevel02;
        [Offset(0x005C)] byte _TVFLevel03;
        [Offset(0x005D)] byte _TVFLevel04;

        #endregion

        #region Fields: TVA

        [Offset(0x005E)] byte _BiasLevel;
        [Offset(0x005F)] byte _BiasPosition;
        [Offset(0x0060)] IntegraBiasDirection _BiasDirection;
        [Offset(0x0061)] IntegraVelocityCurve _TVALevelVelocityCurve;
        [Offset(0x0062)] byte _TVALevelVelocitySens;
        [Offset(0x0063)] byte _TVAEnvTime01VelocitySens;
        [Offset(0x0064)] byte _TVAEnvTime04VelocitySens;
        [Offset(0x0065)] byte _TVAEnvTimeKeyFollow;
        [Offset(0x0066)] byte _TVAEnvTime01;
        [Offset(0x0067)] byte _TVAEnvTime02;
        [Offset(0x0068)] byte _TVAEnvTime03;
        [Offset(0x0069)] byte _TVAEnvTime04;
        [Offset(0x006A)] byte _TVAEnvLevel01;
        [Offset(0x006B)] byte _TVAEnvLevel02;
        [Offset(0x006C)] byte _TVAEnvLevel03;

        #endregion

        #region Fields: LFO 01

        [Offset(0x006D)] IntegraLFOWaveform _LFO01WaveForm;
        [Offset(0x006E)] byte[] _LFO01Rate = new byte[2];
        [Offset(0x0070)] byte _LFO01Offset;
        [Offset(0x0071)] byte _LFO01Detune;
        [Offset(0x0072)] byte _LFO01DelayTime;
        [Offset(0x0073)] byte _LFO01DelayTimeKeyFollow;
        [Offset(0x0074)] IntegraLFOFadeMode _LFO01FadeMode;
        [Offset(0x0075)] byte _LFO01FadeTime;
        [Offset(0x0076)] IntegraSwitch _LFO01KeyTrigger;
        [Offset(0x0077)] byte _LFO01PitchDepth;
        [Offset(0x0078)] byte _LFO01TVFDepth;
        [Offset(0x0079)] byte _LFO01TVADepth;
        [Offset(0x007A)] byte _LFO01PanDepth;

        #endregion

        #region Fields: LFO 02

        [Offset(0x007B)] IntegraLFOWaveform _LFO02WaveForm;
        [Offset(0x007C)] byte[] _LFO02Rate = new byte[2];
        [Offset(0x007E)] byte _LFO02Offset;
        [Offset(0x007F)] byte _LFO02Detune;
        [Offset(0x0100)] byte _LFO02DelayTime;
        [Offset(0x0101)] byte _LFO02DelayTimeKeyFollow;
        [Offset(0x0102)] IntegraLFOFadeMode _LFO02FadeMode;
        [Offset(0x0103)] byte _LFO02FadeTime;
        [Offset(0x0104)] IntegraSwitch _LFO02KeyTrigger;
        [Offset(0x0105)] byte _LFO02PitchDepth;
        [Offset(0x0106)] byte _LFO02TVFDepth;
        [Offset(0x0107)] byte _LFO02TVADepth;
        [Offset(0x0108)] byte _LFO02PanDepth;

        #endregion

        #region Fields: LFO Step Sequencer

        [Offset(0x0109)] IntegraSwitch _LFOStepType;
        [Offset(0x010A)] byte _LFOStep01;
        [Offset(0x010B)] byte _LFOStep02;
        [Offset(0x010C)] byte _LFOStep03;
        [Offset(0x010D)] byte _LFOStep04;
        [Offset(0x010E)] byte _LFOStep05;
        [Offset(0x010F)] byte _LFOStep06;
        [Offset(0x0110)] byte _LFOStep07;
        [Offset(0x0111)] byte _LFOStep08;
        [Offset(0x0112)] byte _LFOStep09;
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
            Partial = partial;
        }

        #endregion

        #region Properties

        public int Partial { get; }

        #endregion

        #region Properties: INTEGRA-7

        #region Properties: General

        [Offset(0x0000)]
        public byte PartialLevel
        {
            get { return _PartialLevel; }
            set
            {
                _PartialLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0001)]
        public byte CoarseTune
        {
            get { return _CoarseTune; }
            set
            {
                _CoarseTune = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0002)]
        public byte FineTune
        {
            get { return _FineTune; }
            set
            {
                _FineTune = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0003)]
        public byte RandomPitchDepth
        {
            get { return _RandomPitchDepth; }
            set
            {
                _RandomPitchDepth = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0004)]
        public byte Pan
        {
            get { return _Pan; }
            set
            {
                _Pan = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0005)]
        public byte PanKeyFollow
        {
            get { return _PanKeyFollow; }
            set
            {
                _PanKeyFollow = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0006)]
        public byte RandomPanDepth
        {
            get { return _RandomPanDepth; }
            set
            {
                _RandomPanDepth = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0007)]
        public byte AlternatePanDepth
        {
            get { return _AlternatePanDepth; }
            set
            {
                _AlternatePanDepth = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0008)]
        public IntegraEnvelopeMode EnvMode
        {
            get { return _EnvMode; }
            set
            {
                _EnvMode = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0009)]
        public IntegraDelayMode DelayMode
        {
            get { return _DelayMode; }
            set
            {
                _DelayMode = value;
                NotifyPropertyChanged();
            }
        }

        //[Offset(0x000A)]
        //public short DelayTime
        //{
        //    get { return _DelayTime.DeserializeShort(); }
        //    set
        //    {
        //        _DelayTime = value.SerializeShort();
        //        NotifyPropertyChanged();
        //    }
        //}

        [Offset(0x000C)]
        public byte OutputLevel
        {
            get { return _OutputLevel; }
            set
            {
                _OutputLevel = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x000F)]
        public byte ChorusSendLevel
        {
            get { return _ChorusSendLevel; }
            set
            {
                _ChorusSendLevel = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0010)]
        public byte ReverbSendLevel
        {
            get { return _ReverbSendLevel; }
            set
            {
                _ReverbSendLevel = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0012)]
        public IntegraSwitch ReceiveBender
        {
            get { return _ReceiveBender; }
            set
            {
                _ReceiveBender = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0013)]
        public IntegraSwitch ReceiveExpression
        {
            get { return _ReceiveExpression; }
            set
            {
                _ReceiveExpression = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0014)]
        public IntegraSwitch ReceiveHold
        {
            get { return _ReceiveHold; }
            set
            {
                _ReceiveHold = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0016)]
        public IntegraSwitch RedamperSwitch
        {
            get { return _RedamperSwitch; }
            set
            {
                _RedamperSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0017)]
        public IntegraControlSwitch Control01Switch01
        {
            get { return _Control01Switch01; }
            set
            {
                _Control01Switch01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0018)]
        public IntegraControlSwitch Control01Switch02
        {
            get { return _Control01Switch02; }
            set
            {
                _Control01Switch02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0019)]
        public IntegraControlSwitch Control01Switch03
        {
            get { return _Control01Switch03; }
            set
            {
                _Control01Switch03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001A)]
        public IntegraControlSwitch Control01Switch04
        {
            get { return _Control01Switch04; }
            set
            {
                _Control01Switch04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001B)]
        public IntegraControlSwitch Control02Switch01
        {
            get { return _Control02Switch01; }
            set
            {
                _Control02Switch01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001C)]
        public IntegraControlSwitch Control02Switch02
        {
            get { return _Control02Switch02; }
            set
            {
                _Control02Switch02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001D)]
        public IntegraControlSwitch Control02Switch03
        {
            get { return _Control02Switch03; }
            set
            {
                _Control02Switch03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001E)]
        public IntegraControlSwitch Control02Switch04
        {
            get { return _Control02Switch04; }
            set
            {
                _Control02Switch04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001F)]
        public IntegraControlSwitch Control03Switch01
        {
            get { return _Control03Switch01; }
            set
            {
                _Control03Switch01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0020)]
        public IntegraControlSwitch Control03Switch02
        {
            get { return _Control03Switch02; }
            set
            {
                _Control03Switch02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0021)]
        public IntegraControlSwitch Control03Switch03
        {
            get { return _Control03Switch03; }
            set
            {
                _Control03Switch03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0022)]
        public IntegraControlSwitch Control03Switch04
        {
            get { return _Control03Switch04; }
            set
            {
                _Control03Switch04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0023)]
        public IntegraControlSwitch Control04Switch01
        {
            get { return _Control04Switch01; }
            set
            {
                _Control04Switch01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0024)]
        public IntegraControlSwitch Control04Switch02
        {
            get { return _Control04Switch02; }
            set
            {
                _Control04Switch02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0025)]
        public IntegraControlSwitch Control04Switch03
        {
            get { return _Control04Switch03; }
            set
            {
                _Control04Switch03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0026)]
        public IntegraControlSwitch Control04Switch04
        {
            get { return _Control04Switch04; }
            set
            {
                _Control04Switch04 = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties: Waveform

        [Offset(0x0027)]
        public IntegraWaveGroupType WaveGroupType
        {
            get { return _WaveGroupType; }
            set
            {
                _WaveGroupType = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0028)]
        public int WaveGroupID
        {
            get { return _WaveGroupID; }
            set
            {
                _WaveGroupID = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002C)]
        public int WaveNumberL
        {
            get { return _WaveNumberL; }
            set
            {
                _WaveNumberL = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0030)]
        public int WaveNumberR
        {
            get { return _WaveNumberR; }
            set
            {
                _WaveNumberR = value;
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
        public IntegraSwitch WaveFXMSwitch
        {
            get { return _WaveFXMSwitch; }
            set
            {
                _WaveFXMSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0036)]
        public byte WaveFXMColor
        {
            get { return _WaveFXMColor; }
            set
            {
                _WaveFXMColor = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0037)]
        public byte WaveFXMDepth
        {
            get { return _WaveFXMDepth; }
            set
            {
                _WaveFXMDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0038)]
        public IntegraSwitch WaveTempoSync
        {
            get { return _WaveTempoSync; }
            set
            {
                _WaveTempoSync = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0039)]
        public byte WavePitchKeyFollow
        {
            get { return _WavePitchKeyFollow; }
            set
            {
                _WavePitchKeyFollow = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties: Pitch Envelope

        [Offset(0x003A)]
        public byte PitchEnvDepth
        {
            get { return _PitchEnvDepth; }
            set
            {
                _PitchEnvDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003B)]
        public byte PitchEnvVelocitySens
        {
            get { return _PitchEnvVelocitySens; }
            set
            {
                _PitchEnvVelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003C)]
        public byte PitchEnvTime01VelocitySens
        {
            get { return _PitchEnvTime01VelocitySens; }
            set
            {
                _PitchEnvTime01VelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003D)]
        public byte PitchEnvTime04VelocitySens
        {
            get { return _PitchEnvTime04VelocitySens; }
            set
            {
                _PitchEnvTime04VelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003E)]
        public byte PitchEnvTimeKeyFollow
        {
            get { return _PitchEnvTimeKeyFollow; }
            set
            {
                _PitchEnvTimeKeyFollow = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003F)]
        public byte PitchEnvTime01
        {
            get { return _PitchEnvTime01; }
            set
            {
                _PitchEnvTime01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0040)]
        public byte PitchEnvTime02
        {
            get { return _PitchEnvTime02; }
            set
            {
                _PitchEnvTime02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0041)]
        public byte PitchEnvTime03
        {
            get { return _PitchEnvTime03; }
            set
            {
                _PitchEnvTime03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0042)]
        public byte PitchEnvTime04
        {
            get { return _PitchEnvTime04; }
            set
            {
                _PitchEnvTime04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0043)]
        public byte PitchEnvLevel00
        {
            get { return _PitchEnvLevel00; }
            set
            {
                _PitchEnvLevel00 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0044)]
        public byte PitchEnvLevel01
        {
            get { return _PitchEnvLevel01; }
            set
            {
                _PitchEnvLevel01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0045)]
        public byte PitchEnvLevel02
        {
            get { return _PitchEnvLevel02; }
            set
            {
                _PitchEnvLevel02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0046)]
        public byte PitchEnvLevel03
        {
            get { return _PitchEnvLevel03; }
            set
            {
                _PitchEnvLevel03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0047)]
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

        [Offset(0x0048)]
        public IntegraTVFFilterType TVFFilterType
        {
            get { return _TVFFilterType; }
            set
            {
                _TVFFilterType = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0049)]
        public byte TVFCutoffFrequency
        {
            get { return _TVFCutoffFrequency; }
            set
            {
                _TVFCutoffFrequency = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004A)]
        public byte TVFCutoffKeyFollow
        {
            get { return _TVFCutoffKeyFollow; }
            set
            {
                _TVFCutoffKeyFollow = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004B)]
        public IntegraVelocityCurve TVFCutoffVelocityCurve
        {
            get { return _TVFCutoffVelocityCurve; }
            set
            {
                _TVFCutoffVelocityCurve = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004C)]
        public byte TVFCutoffVelocitySens
        {
            get { return _TVFCutoffVelocitySens; }
            set
            {
                _TVFCutoffVelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004E)]
        public byte TVFResonance
        {
            get { return _TVFResonance; }
            set
            {
                _TVFResonance = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004D)]
        public byte TVFResonanceVelocitySens
        {
            get { return _TVFResonanceVelocitySens; }
            set
            {
                _TVFResonanceVelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004F)]
        public byte TVFEnvDepth
        {
            get { return _TVFEnvDepth; }
            set
            {
                _TVFEnvDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0050)]
        public IntegraVelocityCurve TVFEnvVelocityCurve
        {
            get { return _TVFEnvVelocityCurve; }
            set
            {
                _TVFEnvVelocityCurve = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0051)]
        public byte TVFEnvVelocitySens
        {
            get { return _TVFEnvVelocitySens; }
            set
            {
                _TVFEnvVelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0052)]
        public byte TVFEnvTime01VelocitySens
        {
            get { return _TVFEnvTime01VelocitySens; }
            set
            {
                _TVFEnvTime01VelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0053)]
        public byte TVFEnvTime04VelocitySens
        {
            get { return _TVFEnvTime04VelocitySens; }
            set
            {
                _TVFEnvTime04VelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0054)]
        public byte TVFEnvTimeKeyFollow
        {
            get { return _TVFEnvTimeKeyFollow; }
            set
            {
                _TVFEnvTimeKeyFollow = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0055)]
        public byte TVFTime01
        {
            get { return _TVFTime01; }
            set
            {
                _TVFTime01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0056)]
        public byte TVFTime02
        {
            get { return _TVFTime02; }
            set
            {
                _TVFTime02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0057)]
        public byte TVFTime03
        {
            get { return _TVFTime03; }
            set
            {
                _TVFTime03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0058)]
        public byte TVFTime04
        {
            get { return _TVFTime04; }
            set
            {
                _TVFTime04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0059)]
        public byte TVFLevel00
        {
            get { return _TVFLevel00; }
            set
            {
                _TVFLevel00 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x005A)]
        public byte TVFLevel01
        {
            get { return _TVFLevel01; }
            set
            {
                _TVFLevel01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x005B)]
        public byte TVFLevel02
        {
            get { return _TVFLevel02; }
            set
            {
                _TVFLevel02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x005C)]
        public byte TVFLevel03
        {
            get { return _TVFLevel03; }
            set
            {
                _TVFLevel03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x005D)]
        public byte TVFLevel04
        {
            get { return _TVFLevel04; }
            set
            {
                _TVFLevel04 = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties: TVA

        [Offset(0x005E)]
        public byte BiasLevel
        {
            get { return _BiasLevel; }
            set
            {
                _BiasLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x005F)]
        public byte BiasPosition
        {
            get { return _BiasPosition; }
            set
            {
                _BiasPosition = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0060)]
        public IntegraBiasDirection BiasDirection
        {
            get { return _BiasDirection; }
            set
            {
                _BiasDirection = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0061)]
        public IntegraVelocityCurve TVALevelVelocityCurve
        {
            get { return _TVALevelVelocityCurve; }
            set
            {
                _TVALevelVelocityCurve = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0062)]
        public byte TVALevelVelocitySens
        {
            get { return _TVALevelVelocitySens; }
            set
            {
                _TVALevelVelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0063)]
        public byte TVAEnvTime01VelocitySens
        {
            get { return _TVAEnvTime01VelocitySens; }
            set
            {
                _TVAEnvTime01VelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0064)]
        public byte TVAEnvTime04VelocitySens
        {
            get { return _TVAEnvTime04VelocitySens; }
            set
            {
                _TVAEnvTime04VelocitySens = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0065)]
        public byte TVAEnvTimeKeyFollow
        {
            get { return _TVAEnvTimeKeyFollow; }
            set
            {
                _TVAEnvTimeKeyFollow = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0066)]
        public byte TVAEnvTime01
        {
            get { return _TVAEnvTime01; }
            set
            {
                _TVAEnvTime01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0067)]
        public byte TVAEnvTime02
        {
            get { return _TVAEnvTime02; }
            set
            {
                _TVAEnvTime02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0068)]
        public byte TVAEnvTime03
        {
            get { return _TVAEnvTime03; }
            set
            {
                _TVAEnvTime03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0069)]
        public byte TVAEnvTime04
        {
            get { return _TVAEnvTime04; }
            set
            {
                _TVAEnvTime04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x006A)]
        public byte TVAEnvLevel01
        {
            get { return _TVAEnvLevel01; }
            set
            {
                _TVAEnvLevel01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x006B)]
        public byte TVAEnvLevel02
        {
            get { return _TVAEnvLevel02; }
            set
            {
                _TVAEnvLevel02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x006C)]
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

        #region Properties: LFO 01

        [Offset(0x006D)]
        public IntegraLFOWaveform LFO01WaveForm
        {
            get { return _LFO01WaveForm; }
            set
            {
                _LFO01WaveForm = value;
                NotifyPropertyChanged();
            }
        }

        //[Offset(0x006E)]
        //public short LFO01Rate
        //{
        //    get { return _LFO01Rate.DeserializeShort(); }
        //    set
        //    {
        //        _LFO01Rate = value.SerializeShort(); ;
        //        NotifyPropertyChanged();
        //    }
        //}

        [Offset(0x0070)]
        public byte LFO01Offset
        {
            get { return _LFO01Offset; }
            set
            {
                _LFO01Offset = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0071)]
        public byte LFO01Detune
        {
            get { return _LFO01Detune; }
            set
            {
                _LFO01Detune = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0072)]
        public byte LFO01DelayTime
        {
            get { return _LFO01DelayTime; }
            set
            {
                _LFO01DelayTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0073)]
        public byte LFO01DelayTimeKeyFollow
        {
            get { return _LFO01DelayTimeKeyFollow; }
            set
            {
                _LFO01DelayTimeKeyFollow = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0074)]
        public IntegraLFOFadeMode LFO01FadeMode
        {
            get { return _LFO01FadeMode; }
            set
            {
                _LFO01FadeMode = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0075)]
        public byte LFO01FadeTime
        {
            get { return _LFO01FadeTime; }
            set
            {
                _LFO01FadeTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0076)]
        public IntegraSwitch LFO01KeyTrigger
        {
            get { return _LFO01KeyTrigger; }
            set
            {
                _LFO01KeyTrigger = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0077)]
        public byte LFO01PitchDepth
        {
            get { return _LFO01PitchDepth; }
            set
            {
                _LFO01PitchDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0078)]
        public byte LFO01TVFDepth
        {
            get { return _LFO01TVFDepth; }
            set
            {
                _LFO01TVFDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0079)]
        public byte LFO01TVADepth
        {
            get { return _LFO01TVADepth; }
            set
            {
                _LFO01TVADepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x007A)]
        public byte LFO01PanDepth
        {
            get { return _LFO01PanDepth; }
            set
            {
                _LFO01PanDepth = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties: LFO 02

        [Offset(0x007B)]
        public IntegraLFOWaveform LFO02WaveForm
        {
            get { return _LFO02WaveForm; }
            set
            {
                _LFO02WaveForm = value;
                NotifyPropertyChanged();
            }
        }

        //[Offset(0x007C)]
        //public short LFO02Rate
        //{
        //    get { return _LFO02Rate.DeserializeShort(); }
        //    set
        //    {
        //        _LFO02Rate = value.SerializeShort();
        //        NotifyPropertyChanged();
        //    }
        //}

        [Offset(0x007E)]
        public byte LFO02Offset
        {
            get { return _LFO02Offset; }
            set
            {
                _LFO02Offset = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x007F)]
        public byte LFO02Detune
        {
            get { return _LFO02Detune; }
            set
            {
                _LFO02Detune = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0100)]
        public byte LFO02DelayTime
        {
            get { return _LFO02DelayTime; }
            set
            {
                _LFO02DelayTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0101)]
        public byte LFO02DelayTimeKeyFollow
        {
            get { return _LFO02DelayTimeKeyFollow; }
            set
            {
                _LFO02DelayTimeKeyFollow = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0102)]
        public IntegraLFOFadeMode LFO02FadeMode
        {
            get { return _LFO02FadeMode; }
            set
            {
                _LFO02FadeMode = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0103)]
        public byte LFO02FadeTime
        {
            get { return _LFO02FadeTime; }
            set
            {
                _LFO02FadeTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0104)]
        public IntegraSwitch LFO02KeyTrigger
        {
            get { return _LFO02KeyTrigger; }
            set
            {
                _LFO02KeyTrigger = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0105)]
        public byte LFO02PitchDepth
        {
            get { return _LFO02PitchDepth; }
            set
            {
                _LFO02PitchDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0106)]
        public byte LFO02TVFDepth
        {
            get { return _LFO02TVFDepth; }
            set
            {
                _LFO02TVFDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0107)]
        public byte LFO02TVADepth
        {
            get { return _LFO02TVADepth; }
            set
            {
                _LFO02TVADepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0108)]
        public byte LFO02PanDepth
        {
            get { return _LFO02PanDepth; }
            set
            {
                _LFO02PanDepth = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties: LFO Step Sequencer

        [Offset(0x0109)]
        public IntegraSwitch LFOStepType
        {
            get { return _LFOStepType; }
            set
            {
                _LFOStepType = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x010A)]
        public byte LFOStep01
        {
            get { return _LFOStep01; }
            set
            {
                _LFOStep01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x010B)]
        public byte LFOStep02
        {
            get { return _LFOStep02; }
            set
            {
                _LFOStep02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x010C)]
        public byte LFOStep03
        {
            get { return _LFOStep03; }
            set
            {
                _LFOStep03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x010D)]
        public byte LFOStep04
        {
            get { return _LFOStep04; }
            set
            {
                _LFOStep04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x010E)]
        public byte LFOStep05
        {
            get { return _LFOStep05; }
            set
            {
                _LFOStep05 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x010F)]
        public byte LFOStep06
        {
            get { return _LFOStep06; }
            set
            {
                _LFOStep06 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0110)]
        public byte LFOStep07
        {
            get { return _LFOStep07; }
            set
            {
                _LFOStep07 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0111)]
        public byte LFOStep08
        {
            get { return _LFOStep08; }
            set
            {
                _LFOStep08 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0112)]
        public byte LFOStep09
        {
            get { return _LFOStep09; }
            set
            {
                _LFOStep09 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0113)]
        public byte LFOStep10
        {
            get { return _LFOStep10; }
            set
            {
                _LFOStep10 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0114)]
        public byte LFOStep11
        {
            get { return _LFOStep11; }
            set
            {
                _LFOStep11 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0115)]
        public byte LFOStep12
        {
            get { return _LFOStep12; }
            set
            {
                _LFOStep12 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0116)]
        public byte LFOStep13
        {
            get { return _LFOStep13; }
            set
            {
                _LFOStep13 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0117)]
        public byte LFOStep14
        {
            get { return _LFOStep14; }
            set
            {
                _LFOStep14 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0118)]
        public byte LFOStep15
        {
            get { return _LFOStep15; }
            set
            {
                _LFOStep15 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0119)]
        public byte LFOStep16
        {
            get { return _LFOStep16; }
            set
            {
                _LFOStep16 = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #endregion

    }
}