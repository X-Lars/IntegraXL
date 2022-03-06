using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models
{
    [Integra(0x02000000, 0x0000002F)]
    public class SystemCommon : IntegraModel<SystemCommon>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private readonly byte[] _MasterTuneData = new byte[4];
        [Offset(0x0004)] private byte _MasterKeyShift;
        [Offset(0x0005)] private byte _MasterLevel;
        [Offset(0x0006)] private IntegraSwitch _ScaleTuneSwitch;
        [Offset(0x0011)] private IntegraControlChannels _StudioSetControlChannel;
        [Offset(0x0020)] private IntegraControlSources _Control01Source;
        [Offset(0x0021)] private IntegraControlSources _Control02Source;
        [Offset(0x0022)] private IntegraControlSources _Control03Source;
        [Offset(0x0023)] private IntegraControlSources _Control04Source;
        [Offset(0x0024)] private IntegraTempoAssignSource _ControlSourceSelect;
        [Offset(0x0025)] private IntegraClockSource _SystemClockSource;
        [Offset(0x0026)] private readonly byte[] _SystemTempoData = new byte[2];
        [Offset(0x0028)] private IntegraTempoAssignSource _TempoAssignSource;
        [Offset(0x0029)] private IntegraSwitch _ReceiveProgramChange;
        [Offset(0x002A)] private IntegraSwitch _ReceiveBankSelect;
        [Offset(0x002B)] private IntegraSwitch _SurroundCenterSpeakerSwitch;
        [Offset(0x002C)] private IntegraSwitch _SurroundSubWooferSwitch;
        [Offset(0x002D)] private IntegraOutputMode _StereoOutputMode;

        private double _MasterTune;
        private int _SystemTempo;

        #endregion

        #region Constructor

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "The class is created by reflection.")]
        private SystemCommon(Integra device) : base(device) { }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public double MasterTune
        {
            get
            {
                // Ensures the property returns the correct data
                InvalidateMasterTune();

                return _MasterTune;
            }
            set
            {
                if (_MasterTune != value)
                {
                    // Invalidate the property value range
                    value = Math.Max(value, 415.3);
                    value = Math.Min(value, 466.2);

                    _MasterTune = value;

                    // Ensures the backing field contains the correct data
                    InvalidateMasterTuneData();

                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0004)]
        public int MasterKeyShift
        {
            get => _MasterKeyShift.Deserialize(64);
            set
            {
                if (MasterKeyShift != value)
                {
                    _MasterKeyShift = value.Serialize(64).Clamp(40, 88);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0005)]
        public byte MasterLevel
        {
            get => _MasterLevel;
            set
            {
                if (_MasterLevel != value)
                {
                    _MasterLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public IntegraSwitch ScaleTuneSwitch
        {
            get => _ScaleTuneSwitch;
            set
            {
                if (_ScaleTuneSwitch != value)
                {
                    _ScaleTuneSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0011)]
        public IntegraControlChannels StudioSetControlChannel
        {
            get => _StudioSetControlChannel;
            set
            {
                if (_StudioSetControlChannel != value)
                {
                    _StudioSetControlChannel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0020)]
        public IntegraControlSources Control01Source
        {
            get => _Control01Source;
            set
            {
                if (_Control01Source != value)
                {
                    _Control01Source = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0021)]
        public IntegraControlSources Control02Source
        {
            get => _Control02Source;
            set
            {
                if (_Control02Source != value)
                {
                    _Control02Source = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0022)]
        public IntegraControlSources Control03Source
        {
            get => _Control03Source;
            set
            {
                if (_Control03Source != value)
                {
                    _Control03Source = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0023)]
        public IntegraControlSources Control04Source
        {
            get => _Control04Source;
            set
            {
                if (_Control04Source != value)
                {
                    _Control04Source = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0024)]
        public IntegraTempoAssignSource ControlSourceSelect
        {
            get => _ControlSourceSelect;
            set
            {
                if (_ControlSourceSelect != value)
                {
                    _ControlSourceSelect = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0025)]
        public IntegraClockSource SystemClockSource
        {
            get => _SystemClockSource;
            set
            {
                if (_SystemClockSource != value)
                {
                    _SystemClockSource = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0026)]
        public int SystemTempo
        {
            get
            {
                // Ensures the property returns the correct data
                InvalidateSystemTempo();

                return _SystemTempo;
            }
            set
            {
                if (_SystemTempo != value)
                {
                    _SystemTempo = value.Clamp(20, 250);

                    // Ensures the backing field contains the correct data
                    InvalidateSystemTempoData();
                    NotifyPropertyChanged();
                }
            }
        }
        [Offset(0x0028)]
        public IntegraTempoAssignSource TempoAssignSource
        {
            get => _TempoAssignSource;
            set
            {
                if (_TempoAssignSource != value)
                {
                    _TempoAssignSource = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0029)]
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

        [Offset(0x002A)]
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

        [Offset(0x002B)]
        public IntegraSwitch SurroundCenterSpeakerSwitch
        {
            get => _SurroundCenterSpeakerSwitch;
            set
            {
                if (_SurroundCenterSpeakerSwitch != value)
                {
                    _SurroundCenterSpeakerSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002C)]
        public IntegraSwitch SurroundSubWooferSwitch
        {
            get => _SurroundSubWooferSwitch;
            set
            {
                if (_SurroundSubWooferSwitch != value)
                {
                    _SurroundSubWooferSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002D)]
        public IntegraOutputMode StereoOutputMode
        {
            get => _StereoOutputMode;
            set
            {
                if (_StereoOutputMode != value)
                {
                    _StereoOutputMode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Sets the <see cref="_MasterTune"/> field by converting the <see cref="_MasterTuneData"/> array to the respective herz value.
        /// </summary>
        private void InvalidateMasterTune()
        {
            // [MasterTune[4]]
            // 03    01    03    04
            //  |     |     |     |
            // 0F    0F    0F    0F
            //  |     |     |     |
            //  3 <<  1 <<  3 <<  4 = [int bytes] 31 34

            // Retreive lower 4 bits of each byte in the _MasterTuneData byte array and combine them into an integer
            int masterTuneBytes = ((_MasterTuneData[0] & 0x0F) << 12 | (_MasterTuneData[1] & 0x0F) << 8 | (_MasterTuneData[2] & 0x0F) << 4 | (_MasterTuneData[3] & 0x0F));

            // Convert bytes to cents
            double cents = ((double)masterTuneBytes - 1024) / 1000;

            // Convert cents to herz
            double herz = Math.Pow(2, (cents / 12)) * 440;

            //_MasterTune.Herz = result;
            _MasterTune = Math.Round(herz, 1);
        }

        /// <summary>
        /// Sets the <see cref="_MasterTuneData"/> array fields by converting the <see cref="_MasterTune"/> herz value to raw MIDI data.
        /// </summary>
        private void InvalidateMasterTuneData()
        {
            var result = 12 * (Math.Log(_MasterTune / 440) / Math.Log(2));
            int midi = (int)((result * 1000) + 1024);

            midi = Math.Min(midi, 2024);
            midi = Math.Max(midi, 24);

            _MasterTuneData[0] = (byte)((midi >> 12) & 0x0F);
            _MasterTuneData[1] = (byte)((midi >> 8) & 0x0F);
            _MasterTuneData[2] = (byte)((midi >> 4) & 0x0F);
            _MasterTuneData[3] = (byte)((midi & 0x0F));
        }

        /// <summary>
        /// Sets the <see cref="_SystemTempo"/> field by converting the <see cref="_SystemTempoData"/> array to the respective tempo value.
        /// </summary>
        private void InvalidateSystemTempo()
        {
            _SystemTempo = (byte)((_SystemTempoData[0] & 0x0F) << 4 | (_SystemTempoData[1] & 0x0F));
        }

        /// <summary>
        /// Sets the <see cref="_SystemTempoData"/> array fields by converting the <see cref="_SystemTempo"/> value to raw MIDI data.
        /// </summary>
        private void InvalidateSystemTempoData()
        {
            //_SystemTempo = Math.Min(_SystemTempo, 250);
            //_SystemTempo = Math.Max(_SystemTempo, 20);

            _SystemTempoData[0] = (byte)((_SystemTempo >> 4) & 0x0F);
            _SystemTempoData[1] = (byte)((_SystemTempo & 0x0F));
        }

        #endregion

        #region Enumerations

        //public IEnumerable<IntegraControlChannels> ControlChannelValues
        //{
        //    get { return Enum.GetValues(typeof(IntegraControlChannels)).Cast<IntegraControlChannels>(); }
        //}

        //public IEnumerable<IntegraControlSources> SystemControlSourceValues
        //{
        //    get { return Enum.GetValues(typeof(IntegraControlSources)).Cast<IntegraControlSources>(); }
        //}

        #endregion
    }
}
