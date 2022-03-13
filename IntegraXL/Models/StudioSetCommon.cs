using IntegraXL.Core;
using IntegraXL.Extensions;
using System.Diagnostics.CodeAnalysis;
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

        [Offset(0x0010)] private readonly byte[] RESERVED1 = new byte[8];

        [Offset(0x0018)] private byte _VoiceReserve1;
        [Offset(0x0019)] private byte _VoiceReserve2;
        [Offset(0x001A)] private byte _VoiceReserve3;
        [Offset(0x001B)] private byte _VoiceReserve4;
        [Offset(0x001C)] private byte _VoiceReserve5;
        [Offset(0x001D)] private byte _VoiceReserve6;
        [Offset(0x001E)] private byte _VoiceReserve7;
        [Offset(0x001F)] private byte _VoiceReserve8;
        [Offset(0x0020)] private byte _VoiceReserve9;
        [Offset(0x0021)] private byte _VoiceReserve10;
        [Offset(0x0022)] private byte _VoiceReserve11;
        [Offset(0x0023)] private byte _VoiceReserve12;
        [Offset(0x0024)] private byte _VoiceReserve13;
        [Offset(0x0025)] private byte _VoiceReserve14;
        [Offset(0x0026)] private byte _VoiceReserve15;
        [Offset(0x0027)] private byte _VoiceReserve16;

        [Offset(0x0028)] private readonly byte[] RESERVED2 = new byte[17];

        [Offset(0x0039)] private IntegraControlSources _ToneControl1;
        [Offset(0x003A)] private IntegraControlSources _ToneControl2;
        [Offset(0x003B)] private IntegraControlSources _ToneControl3;
        [Offset(0x003C)] private IntegraControlSources _ToneControl4;
        [Offset(0x003D)] private byte[] _Tempo = new byte[2];
        [Offset(0x003F)] private SoloParts _SoloPart;
        [Offset(0x0040)] private IntegraSwitch _ReverbSwitch;
        [Offset(0x0041)] private IntegraSwitch _ChorusSwitch;
        [Offset(0x0042)] private IntegraSwitch _MasterEQSwitch;
        [Offset(0x0043)] private IntegraSwitch _DrumCompEQSwitch;
        [Offset(0x0044)] private Parts _DrumCompEQPart;
        [Offset(0x0045)] private IntegraOutputAssigns _DrumCompEQOutputAssign1;
        [Offset(0x0046)] private IntegraOutputAssigns _DrumCompEQOutputAssign2;
        [Offset(0x0047)] private IntegraOutputAssigns _DrumCompEQOutputAssign3;
        [Offset(0x0048)] private IntegraOutputAssigns _DrumCompEQOutputAssign4;
        [Offset(0x0049)] private IntegraOutputAssigns _DrumCompEQOutputAssign5;
        [Offset(0x004A)] private IntegraOutputAssigns _DrumCompEQOutputAssign6;

        [Offset(0x004B)] private readonly byte RESERVED3;

        [Offset(0x004C)] private byte _ExtPartLevel;
        [Offset(0x004D)] private byte _ExtPartChorusSendLevel;
        [Offset(0x004E)] private byte _ExtPartReverbSendLevel;
        [Offset(0x004F)] private IntegraSwitch _ExtPartMuteSwitch;

        [Offset(0x0050)] private readonly byte[] _Reserved04 = new byte[4];

        #endregion

        #region Constructor

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Class is created by reflection")]
        private StudioSetCommon(Integra device) : base(device) { }

        #endregion

        #region Properties

        [Offset(0x0000)]
        public new string Name
        {
            // Convert the backing field byte array to string
            get => Encoding.ASCII.GetString(_NameData, 0, _NameData.Length);
            set
            {
                if (Name != value)
                {
                    if (string.IsNullOrEmpty(value))
                        return;

                    _NameData = Encoding.ASCII.GetBytes(value.Clamp(_NameData.Length));

                    NotifyPropertyChanged();
                }
            }
        }

        #region Properties: Voice Reserves

        [Offset(0x0018)]
        public byte VoiceReserve1
        {
            get => _VoiceReserve1;
            set
            {
                if (_VoiceReserve1 != value)
                {
                    _VoiceReserve1 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0019)]
        public byte VoiceReserve2
        {
            get => _VoiceReserve2;
            set
            {
                if (_VoiceReserve2 != value)
                {
                    _VoiceReserve2 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001A)]
        public byte VoiceReserve3
        {
            get => _VoiceReserve3;
            set
            {
                if (_VoiceReserve3 != value)
                {
                    _VoiceReserve3 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001B)]
        public byte VoiceReserve4
        {
            get => _VoiceReserve4;
            set
            {
                if (_VoiceReserve4 != value)
                {
                    _VoiceReserve4 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001C)]
        public byte VoiceReserve5
        {
            get => _VoiceReserve5;
            set
            {
                if (_VoiceReserve5 != value)
                {
                    _VoiceReserve5 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001D)]
        public byte VoiceReserve6
        {
            get => _VoiceReserve6;
            set
            {
                if (_VoiceReserve6 != value)
                {
                    _VoiceReserve6 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001E)]
        public byte VoiceReserve7
        {
            get => _VoiceReserve7;
            set
            {
                if (_VoiceReserve7 != value)
                {
                    _VoiceReserve7 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001F)]
        public byte VoiceReserve8
        {
            get => _VoiceReserve8;
            set
            {
                if (_VoiceReserve8 != value)
                {
                    _VoiceReserve8 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0020)]
        public byte VoiceReserve9
        {
            get => _VoiceReserve9;
            set
            {
                if (_VoiceReserve9 != value)
                {
                    _VoiceReserve9 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0021)]
        public byte VoiceReserve10
        {
            get => _VoiceReserve10;
            set
            {
                if (_VoiceReserve10 != value)
                {
                    _VoiceReserve10 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0022)]
        public byte VoiceReserve11
        {
            get => _VoiceReserve11;
            set
            {
                if (_VoiceReserve11 != value)
                {
                    _VoiceReserve11 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0023)]
        public byte VoiceReserve12
        {
            get => _VoiceReserve12;
            set
            {
                if (_VoiceReserve12 != value)
                {
                    _VoiceReserve12 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0024)]
        public byte VoiceReserve13
        {
            get => _VoiceReserve13;
            set
            {
                if (_VoiceReserve13 != value)
                {
                    _VoiceReserve13 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0025)]
        public byte VoiceReserve14
        {
            get => _VoiceReserve14;
            set
            {
                if (_VoiceReserve14 != value)
                {
                    _VoiceReserve14 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0026)]
        public byte VoiceReserve15
        {
            get => _VoiceReserve15;
            set
            {
                if (_VoiceReserve15 != value)
                {
                    _VoiceReserve15 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0027)]
        public byte VoiceReserve16
        {
            get => _VoiceReserve16;
            set
            {
                if (_VoiceReserve16 != value)
                {
                    _VoiceReserve16 = value.Clamp(0, 64);
                    NotifyPropertyChanged();
                }
            }
        }


        #endregion

        #region Properties: Tone Control Sources

        [Offset(0x0039)]
        public IntegraControlSources ToneControlSource1
        {
            get => _ToneControl1;
            set
            {
                if (_ToneControl1 != value)
                {
                    _ToneControl1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003A)]
        public IntegraControlSources ToneControlSource2
        {
            get => _ToneControl2;
            set
            {
                if (_ToneControl2 != value)
                {
                    _ToneControl2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003B)]
        public IntegraControlSources ToneControlSource3
        {
            get => _ToneControl3;
            set
            {
                if (_ToneControl3 != value)
                {
                    _ToneControl3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003C)]
        public IntegraControlSources ToneControlSource4
        {
            get => _ToneControl4;
            set
            {
                if (_ToneControl4 != value)
                {
                    _ToneControl4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        [Offset(0x003D)]
        public short Tempo
        {
            get => _Tempo.Deserialize();
            set
            {
                if(Tempo != value)
                {
                    _Tempo = value.Serialize(20, 250);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003F)]
        public SoloParts SoloPart
        {
            get => _SoloPart;
            set
            {
                if (_SoloPart != value)
                {
                    _SoloPart = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #region Properties: Switches

        [Offset(0x0040)]
        public IntegraSwitch ReverbSwitch
        {
            get => _ReverbSwitch;
            set
            {
                if (_ReverbSwitch != value)
                {
                    _ReverbSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0041)]
        public IntegraSwitch ChorusSwitch
        {
            get => _ChorusSwitch;
            set
            {
                if (_ChorusSwitch != value)
                {
                    _ChorusSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0042)]
        public IntegraSwitch MasterEQSwitch
        {
            get => _MasterEQSwitch;
            set
            {
                if (_MasterEQSwitch != value)
                {
                    _MasterEQSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0043)]
        public IntegraSwitch DrumCompEQSwitch
        {
            get => _DrumCompEQSwitch;
            set
            {
                if (_DrumCompEQSwitch != value)
                {
                    _DrumCompEQSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        [Offset(0x0044)]
        public Parts DrumCompEQPart
        {
            get => _DrumCompEQPart;
            set
            {
                if (_DrumCompEQPart != value)
                {
                    _DrumCompEQPart = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #region Properties: Drum Compressor + EQ Output Assign

        [Offset(0x0045)]
        public IntegraOutputAssigns DrumCompEQOutputAssign1
        {
            get => _DrumCompEQOutputAssign1;
            set
            {
                if (_DrumCompEQOutputAssign1 != value)
                {
                    _DrumCompEQOutputAssign1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0046)]
        public IntegraOutputAssigns DrumCompEQOutputAssign2
        {
            get => _DrumCompEQOutputAssign2;
            set
            {
                if (_DrumCompEQOutputAssign2 != value)
                {
                    _DrumCompEQOutputAssign2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0047)]
        public IntegraOutputAssigns DrumCompEQOutputAssign3
        {
            get => _DrumCompEQOutputAssign3;
            set
            {
                if (_DrumCompEQOutputAssign3 != value)
                {
                    _DrumCompEQOutputAssign3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0048)]
        public IntegraOutputAssigns DrumCompEQOutputAssign4
        {
            get => _DrumCompEQOutputAssign4;
            set
            {
                if (_DrumCompEQOutputAssign4 != value)
                {
                    _DrumCompEQOutputAssign4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0049)]
        public IntegraOutputAssigns DrumCompEQOutputAssign5
        {
            get => _DrumCompEQOutputAssign5;
            set
            {
                if (_DrumCompEQOutputAssign5 != value)
                {
                    _DrumCompEQOutputAssign5 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004A)]
        public IntegraOutputAssigns DrumCompEQOutputAssign6
        {
            get => _DrumCompEQOutputAssign6;
            set
            {
                if (_DrumCompEQOutputAssign6 != value)
                {
                    _DrumCompEQOutputAssign6 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: External Part

        [Offset(0x004C)]
        public byte ExtPartLevel
        {
            get => _ExtPartLevel;
            set
            {
                if (_ExtPartLevel != value)
                {
                    _ExtPartLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004D)]
        public byte ExtPartChorusSendLevel
        {
            get => _ExtPartChorusSendLevel;
            set
            {
                if (_ExtPartChorusSendLevel != value)
                {
                    _ExtPartChorusSendLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004E)]
        public byte ExtPartReverbSendLevel
        {
            get => _ExtPartReverbSendLevel;
            set
            {
                if (_ExtPartReverbSendLevel != value)
                {
                    _ExtPartReverbSendLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004F)]
        public IntegraSwitch ExtPartMuteSwitch
        {
            get => _ExtPartMuteSwitch;
            set
            {
                if (_ExtPartMuteSwitch != value)
                {
                    _ExtPartMuteSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #endregion

        #region Enumerations

        public List<string> VoiceReserveValues
        {
            get { return IntegraVoiceReserves.Values; }
        }

        #endregion

    }
}
