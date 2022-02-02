using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x02000000, 0x0000002F)]
    public class SystemCommon : IntegraModel<SystemCommon>
    {
        #region Fields

        [Offset(0x0000)] private byte[] _MasterTuneData = new byte[4];

        [Offset(0x0004)] private byte _MasterKeyShift;
        [Offset(0x0005)] private byte _MasterLevel;
        [Offset(0x0006)] private bool _ScaleTuneSwitch;
        [Offset(0x0011)] private IntegraControlChannels _StudioSetControlChannel;
        [Offset(0x0020)] private IntegraControlSources _SystemControl01Source;
        [Offset(0x0021)] private IntegraControlSources _SystemControl02Source;
        [Offset(0x0022)] private IntegraControlSources _SystemControl03Source;
        [Offset(0x0023)] private IntegraControlSources _SystemControl04Source;
        [Offset(0x0024)] private bool _ControlSourceSelect;
        [Offset(0x0025)] private bool _SystemClockSource;
        [Offset(0x0026)] private byte[] _SystemTempoData = new byte[2];

        [Offset(0x0028)] private bool _TempoAssignSource;
        [Offset(0x0029)] private bool _ReceiveProgramChange;
        [Offset(0x002A)] private bool _ReceiveBankSelect;
        [Offset(0x002B)] private bool _SurroundCenterSpeakerSwitch;
        [Offset(0x002C)] private bool _SurroundSubWooferSwitch;
        [Offset(0x002D)] private bool _StereoOutputMode;

        private double _MasterTune;
        private int _SystemTempo;

        #endregion

        #region Constructor

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
                if (_MasterTune == value)
                    return;

                // Invalidate the property value range
                value = Math.Max(value, 415.3);
                value = Math.Min(value, 466.2);

                _MasterTune = value;

                // Ensures the backing field contains the correct data
                InvalidateMasterTuneData();

                NotifyPropertyChanged();
            }
        }

        [Offset(0x0004)]
        public int MasterKeyShift
        {
            get { return (_MasterKeyShift - 64); }
            set
            {
                if (_MasterKeyShift == (value + 64))
                    return;

                _MasterKeyShift = (byte)(value + 64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0005)]
        public byte MasterLevel
        {
            get { return _MasterLevel; }
            set
            {
                if (_MasterLevel == value)
                    return;

                _MasterLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public bool ScaleTuneSwitch
        {
            get { return _ScaleTuneSwitch; }
            set
            {
                if (_ScaleTuneSwitch == value)
                    return;

                _ScaleTuneSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0011)]
        public IntegraControlChannels StudioSetControlChannel
        {
            get { return _StudioSetControlChannel; }
            set
            {
                if (_StudioSetControlChannel == value)
                    return;

                _StudioSetControlChannel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0020)]
        public IntegraControlSources SystemControl01Source
        {
            get { return _SystemControl01Source; }
            set
            {
                if (_SystemControl01Source == value)
                    return;

                _SystemControl01Source = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0021)]
        public IntegraControlSources SystemControl02Source
        {
            get { return _SystemControl02Source; }
            set
            {
                if (_SystemControl02Source == value)
                    return;

                _SystemControl02Source = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0022)]
        public IntegraControlSources SystemControl03Source
        {
            get { return _SystemControl03Source; }
            set
            {
                if (_SystemControl03Source == value)
                    return;

                _SystemControl03Source = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0023)]
        public IntegraControlSources SystemControl04Source
        {
            get { return _SystemControl04Source; }
            set
            {
                if (_SystemControl04Source == value)
                    return;

                _SystemControl04Source = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0024)]
        public bool ControlSourceSelect
        {
            get { return _ControlSourceSelect; }
            set
            {
                if (_ControlSourceSelect == value)
                    return;

                _ControlSourceSelect = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0025)]
        public bool SystemClockSource
        {
            get { return _SystemClockSource; }
            set
            {
                if (_SystemClockSource == value)
                    return;

                _SystemClockSource = value;
                NotifyPropertyChanged();
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
                if (_SystemTempo == value)
                    return;

                _SystemTempo = value;

                // Ensures the backing field contains the correct data
                InvalidateSystemTempoData();
                NotifyPropertyChanged();

            }
        }
        [Offset(0x0028)]
        public bool TempoAssignSource
        {
            get { return _TempoAssignSource; }
            set
            {
                if (_TempoAssignSource == value)
                    return;

                _TempoAssignSource = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0029)]
        public bool ReceiveProgramChange
        {
            get { return _ReceiveProgramChange; }
            set
            {
                if (_ReceiveProgramChange == value)
                    return;

                _ReceiveProgramChange = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002A)]
        public bool ReceiveBankSelect
        {
            get { return _ReceiveBankSelect; }
            set
            {
                if (_ReceiveBankSelect == value)
                    return;

                _ReceiveBankSelect = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002B)]
        public bool SurroundCenterSpeakerSwitch
        {
            get { return _SurroundCenterSpeakerSwitch; }
            set
            {
                if (_SurroundCenterSpeakerSwitch == value)
                    return;

                _SurroundCenterSpeakerSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002C)]
        public bool SurroundSubWooferSwitch
        {
            get { return _SurroundSubWooferSwitch; }
            set
            {
                if (_SurroundSubWooferSwitch == value)
                    return;

                _SurroundSubWooferSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002D)]
        public bool StereoOutputMode
        {
            get { return _StereoOutputMode; }
            set
            {
                if (_StereoOutputMode == value)
                    return;

                _StereoOutputMode = value;
                NotifyPropertyChanged();
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
            _SystemTempo = Math.Min(_SystemTempo, 250);
            _SystemTempo = Math.Max(_SystemTempo, 20);

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
