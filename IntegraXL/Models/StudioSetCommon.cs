using IntegraXL.Core;
using System.Text;

namespace IntegraXL.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 studio set common model.
    /// </summary>
    /// <remarks><i>18 00 00 00</i></remarks>
    [Integra(0x18000000, 0x00000054)]
    public sealed class StudioSetCommon : IntegraModel<StudioSetCommon>
    {
        #region Fields

        [Offset(0x0000)] private byte[] _NameData = new byte[16];
        [Offset(0x0018)] private byte _VoiceReserve01;
        [Offset(0x0019)] private byte _VoiceReserve02;
        [Offset(0x001A)] private byte _VoiceReserve03;
        [Offset(0x001B)] private byte _VoiceReserve04;
        [Offset(0x001C)] private byte _VoiceReserve05;
        [Offset(0x001D)] private byte _VoiceReserve06;
        [Offset(0x001E)] private byte _VoiceReserve07;
        [Offset(0x001F)] private byte _VoiceReserve08;
        [Offset(0x0020)] private byte _VoiceReserve09;
        [Offset(0x0021)] private byte _VoiceReserve10;
        [Offset(0x0022)] private byte _VoiceReserve11;
        [Offset(0x0023)] private byte _VoiceReserve12;
        [Offset(0x0024)] private byte _VoiceReserve13;
        [Offset(0x0025)] private byte _VoiceReserve14;
        [Offset(0x0026)] private byte _VoiceReserve15;
        [Offset(0x0027)] private byte _VoiceReserve16;
        [Offset(0x0039)] private IntegraControlSources _ToneControl01;
        [Offset(0x003A)] private IntegraControlSources _ToneControl02;
        [Offset(0x003B)] private IntegraControlSources _ToneControl03;
        [Offset(0x003C)] private IntegraControlSources _ToneControl04;
        [Offset(0x003D)] private byte[] _StudioSetTempoData = new byte[2];
        [Offset(0x003F)] private byte _SoloPart;
        [Offset(0x0040)] private bool _ReverbSwitch;
        [Offset(0x0041)] private bool _ChorusSwitch;
        [Offset(0x0042)] private bool _MasterEQSwitch;
        [Offset(0x0043)] private bool _DrumCompEQSwitch;
        [Offset(0x0044)] private Parts _DrumCompEQPart;
        [Offset(0x0045)] private IntegraOutputAssigns _DrumCompEQOutputAssign01;
        [Offset(0x0046)] private IntegraOutputAssigns _DrumCompEQOutputAssign02;
        [Offset(0x0047)] private IntegraOutputAssigns _DrumCompEQOutputAssign03;
        [Offset(0x0048)] private IntegraOutputAssigns _DrumCompEQOutputAssign04;
        [Offset(0x0049)] private IntegraOutputAssigns _DrumCompEQOutputAssign05;
        [Offset(0x004A)] private IntegraOutputAssigns _DrumCompEQOutputAssign06;
        [Offset(0x004C)] private byte _ExtPartLevel;
        [Offset(0x004D)] private byte _ExtPartChorusSendLevel;
        [Offset(0x004E)] private byte _ExtPartReverbSendLevel;
        [Offset(0x004F)] private bool _ExtPartMuteSwitch;

        #endregion

        #region Constructor

        private StudioSetCommon(Integra device) : base(device) { }

        #endregion

        #region Properties

        [Offset(0x0000)]
        public new string Name
        {
            get
            {
                // Convert the backing field byte array to string
                return Encoding.ASCII.GetString(_NameData, 0, 16);
            }
            set
            {
                if (Name != value)
                {
                    if (value == null)
                        return;

                    // Copy the string to the backing field byte array
                    Array.Copy(Encoding.ASCII.GetBytes(value), 0, _NameData, 0, 16);

                    NotifyPropertyChanged();
                }
            }
        }

        #region Properties: Voice Reserves

        [Offset(0x0018)]
        public byte VoiceReserve01
        {
            get { return _VoiceReserve01; }
            set
            {
                _VoiceReserve01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0019)]
        public byte VoiceReserve02
        {
            get { return _VoiceReserve02; }
            set
            {
                _VoiceReserve02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001A)]
        public byte VoiceReserve03
        {
            get { return _VoiceReserve03; }
            set
            {
                _VoiceReserve03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001B)]
        public byte VoiceReserve04
        {
            get { return _VoiceReserve04; }
            set
            {
                _VoiceReserve04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001C)]
        public byte VoiceReserve05
        {
            get { return _VoiceReserve05; }
            set
            {
                _VoiceReserve05 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001D)]
        public byte VoiceReserve06
        {
            get { return _VoiceReserve06; }
            set
            {
                _VoiceReserve06 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001E)]
        public byte VoiceReserve07
        {
            get { return _VoiceReserve07; }
            set
            {
                _VoiceReserve07 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001F)]
        public byte VoiceReserve08
        {
            get { return _VoiceReserve08; }
            set
            {
                _VoiceReserve08 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0020)]
        public byte VoiceReserve09
        {
            get { return _VoiceReserve09; }
            set
            {
                _VoiceReserve09 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0021)]
        public byte VoiceReserve10
        {
            get { return _VoiceReserve10; }
            set
            {
                _VoiceReserve10 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0022)]
        public byte VoiceReserve11
        {
            get { return _VoiceReserve11; }
            set
            {
                _VoiceReserve11 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0023)]
        public byte VoiceReserve12
        {
            get { return _VoiceReserve12; }
            set
            {
                _VoiceReserve12 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0024)]
        public byte VoiceReserve13
        {
            get { return _VoiceReserve13; }
            set
            {
                _VoiceReserve13 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0025)]
        public byte VoiceReserve14
        {
            get { return _VoiceReserve14; }
            set
            {
                _VoiceReserve14 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0026)]
        public byte VoiceReserve15
        {
            get { return _VoiceReserve15; }
            set
            {
                _VoiceReserve15 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0027)]
        public byte VoiceReserve16
        {
            get { return _VoiceReserve16; }
            set
            {
                _VoiceReserve16 = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #region Properties: Tone Control Sources

        [Offset(0x0039)]
        public IntegraControlSources ToneControlSource01
        {
            get { return _ToneControl01; }
            set
            {
                _ToneControl01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003A)]
        public IntegraControlSources ToneControlSource02
        {
            get { return _ToneControl02; }
            set
            {
                _ToneControl02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003B)]
        public IntegraControlSources ToneControlSource03
        {
            get { return _ToneControl03; }
            set
            {
                _ToneControl03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003C)]
        public IntegraControlSources ToneControlSource04
        {
            get { return _ToneControl04; }
            set
            {
                _ToneControl04 = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        [Offset(0x003D)]
        public short Tempo
        {
            get
            {
                // Convert the backing field byte array to short
                return (short)((_StudioSetTempoData[0] & 0x0F) << 4 | (_StudioSetTempoData[1] & 0x0F));
            }
            set
            {
                if (Tempo != value)
                {
                    // Copy the short to the backing field byte array
                    value = Math.Min(value, (short)250);
                    value = Math.Max(value, (short)20);

                    _StudioSetTempoData[0] = (byte)((value >> 4) & 0x0F);
                    _StudioSetTempoData[1] = (byte)((value) & 0x0F);

                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003F)]
        public byte SoloPart
        {
            get { return _SoloPart; }
            set
            {
                _SoloPart = value;
                NotifyPropertyChanged();
            }
        }

        #region Properties: Switches

        [Offset(0x0040)]
        public bool ReverbSwitch
        {
            get { return _ReverbSwitch; }
            set
            {
                _ReverbSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0041)]
        public bool ChorusSwitch
        {
            get { return _ChorusSwitch; }
            set
            {
                _ChorusSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0042)]
        public bool MasterEQSwitch
        {
            get { return _MasterEQSwitch; }
            set
            {
                _MasterEQSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0043)]
        public bool DrumCompEQSwitch
        {
            get { return _DrumCompEQSwitch; }
            set
            {
                _DrumCompEQSwitch = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        [Offset(0x0044)]
        public Parts DrumCompEQPart
        {
            get { return _DrumCompEQPart; }
            set
            {
                _DrumCompEQPart = value;
                NotifyPropertyChanged();
            }
        }

        #region Properties: Drum Compressor + EQ Output Assign

        [Offset(0x0045)]
        public IntegraOutputAssigns DrumCompEQOutputAssign01
        {
            get { return _DrumCompEQOutputAssign01; }
            set
            {
                _DrumCompEQOutputAssign01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0046)]
        public IntegraOutputAssigns DrumCompEQOutputAssign02
        {
            get { return _DrumCompEQOutputAssign02; }
            set
            {
                _DrumCompEQOutputAssign02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0047)]
        public IntegraOutputAssigns DrumCompEQOutputAssign03
        {
            get { return _DrumCompEQOutputAssign03; }
            set
            {
                _DrumCompEQOutputAssign03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0048)]
        public IntegraOutputAssigns DrumCompEQOutputAssign04
        {
            get { return _DrumCompEQOutputAssign04; }
            set
            {
                _DrumCompEQOutputAssign04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0049)]
        public IntegraOutputAssigns DrumCompEQOutputAssign05
        {
            get { return _DrumCompEQOutputAssign05; }
            set
            {
                _DrumCompEQOutputAssign05 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004A)]
        public IntegraOutputAssigns DrumCompEQOutputAssign06
        {
            get { return _DrumCompEQOutputAssign06; }
            set
            {
                _DrumCompEQOutputAssign06 = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties: External Part

        [Offset(0x004C)]
        public byte ExtPartLevel
        {
            get { return _ExtPartLevel; }
            set
            {
                _ExtPartLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004D)]
        public byte ExtPartChorusSendLevel
        {
            get { return _ExtPartChorusSendLevel; }
            set
            {
                _ExtPartChorusSendLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004E)]
        public byte ExtPartReverbSendLevel
        {
            get { return _ExtPartReverbSendLevel; }
            set
            {
                _ExtPartReverbSendLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004F)]
        public bool ExtPartMuteSwitch
        {
            get { return _ExtPartMuteSwitch; }
            set
            {
                _ExtPartMuteSwitch = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region Methods

        ///// <summary>
        ///// Method to initialize the properties of the <see cref="StudioSetCommon"/> class by the received system exclusive data.
        ///// </summary>
        ///// <param name="data">A <see cref="byte[]"/> array containing the received system exclusive data part.</param>
        //protected override bool Initialize(byte[] data)
        //{
        //    return base.Initialize(data);
        //    //if(base.Initialize(data))
        //    //{
        //    //    _Name = Encoding.ASCII.GetString(data, 0, 16);
        //    //}

        //    //return true;

        //}

        //private void SetStudioSetName()
        //{
        //    _Name = Encoding.ASCII.GetString(_NameData, 0, 16);
        //}
        //private void SetStudioSetNameData()
        //{
        //    Array.Copy(Encoding.ASCII.GetBytes(_Name), 0, _NameData, 0, 16);
        //}

        //private void SetStudioSetTempo()
        //{
        //    _StudioSetTempo = (short)((_StudioSetTempoData[0] & 0x0F) << 4 | (_StudioSetTempoData[1] & 0x0F));
        //}

        //private void SetStudioSetTempoData()
        //{
        //    _StudioSetTempo = Math.Min(_StudioSetTempo, (short)250);
        //    _StudioSetTempo = Math.Max(_StudioSetTempo, (short)20);

        //    _StudioSetTempoData[0] = (byte)((_StudioSetTempo >> 4) & 0x0F);
        //    _StudioSetTempoData[1] = (byte)((_StudioSetTempo) & 0x0F);
        //}

        #endregion

        #region Enumerations

        public List<string> VoiceReserveValues
        {
            get { return IntegraVoiceReserves.Values; }
        }

        public List<string> SoloPartValues
        {
            get { return IntegraPartSelections.Values; }
        }

        public IEnumerable<IntegraControlSources> ToneControlSourceValues
        {
            get { return Enum.GetValues(typeof(IntegraControlSources)).Cast<IntegraControlSources>(); }
        }

        public IEnumerable<Parts> IntegraParts
        {
            get { return Enum.GetValues(typeof(Parts)).Cast<Parts>(); }
        }

        public IEnumerable<IntegraOutputAssigns> OutputAssigns
        {
            get { return Enum.GetValues(typeof(IntegraOutputAssigns)).Cast<IntegraOutputAssigns>(); }
        }

        #endregion

    }
}
