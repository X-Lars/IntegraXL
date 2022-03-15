using IntegraXL.Core;
using IntegraXL.Extensions;
using System.Text;

namespace IntegraXL.Models
{
    [Integra(0x00000000, 0x00000040)]
    public class SuperNATURALSynthToneCommon : IntegraModel<SuperNATURALSynthToneCommon>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] byte[] _ToneName = new byte[12];
        [Offset(0x000C)] byte _ToneLevel;
        [Offset(0x000D)] byte[] RESERVED01 = new byte[5];
        [Offset(0x0012)] IntegraSwitch _PortamentoSwitch;
        [Offset(0x0013)] byte _PortamentoTime;
        [Offset(0x0014)] IntegraSwitch _MonoSwitch;
        [Offset(0x0015)] byte _OctaveShift;
        [Offset(0x0016)] byte _PitchBendRangeUp;
        [Offset(0x0017)] byte _PitchBendRangeDown;
        [Offset(0x0018)] byte RESERVED02;
        [Offset(0x0019)] IntegraSwitch _Partial01Switch;
        [Offset(0x001A)] IntegraSwitch _Partial01Select;
        [Offset(0x001B)] IntegraSwitch _Partial02Switch;
        [Offset(0x001C)] IntegraSwitch _Partial02Select;
        [Offset(0x001D)] IntegraSwitch _Partial03Switch;
        [Offset(0x001E)] IntegraSwitch _Partial03Select;
        [Offset(0x001F)] IntegraRingSwitch _RingSwitch;
        [Offset(0x0020)] IntegraSwitch _TFXSwitch;
        [Offset(0x0021)] byte[] RESERVED03 = new byte[13];
        [Offset(0x002E)] IntegraSwitch _UnisonSwitch;
        [Offset(0x002F)] byte[] RESERVED04 = new byte[2];
        [Offset(0x0031)] IntegraPortamentoMode _PortamentoMode;
        [Offset(0x0032)] IntegraSwitch _LegatoSwitch;
        [Offset(0x0033)] byte RESERVED05;
        [Offset(0x0034)] byte _AnalogFeel;
        [Offset(0x0035)] byte _WaveShape;
        [Offset(0x0036)] IntegraTemporaryToneCategories _Category;
        [Offset(0x0037)] IntegraSynthPhrase _PhraseNumber;
        [Offset(0x003B)] byte _PhraseOctaveShift;
        [Offset(0x003C)] IntegraUnisonSize _UnisonSize;
        [Offset(0x003D)] byte[] RESERVED06 = new byte[3];

        #endregion

        #region Constructor

        public SuperNATURALSynthToneCommon(SuperNATURALSynthTone tone) : base(tone.Device) 
        {
            Address = tone.Address;
        }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public string ToneName
        {
            get => Encoding.ASCII.GetString(_ToneName, 0, 12);
            set
            {
                if (ToneName != value)
                {
                    if (string.IsNullOrEmpty(value))
                        return;

                    _ToneName = Encoding.ASCII.GetBytes(value.FixedLength(_ToneName.Length));

                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public byte ToneLevel
        {
            get => _ToneLevel;
            set
            {
                if (_ToneLevel != value)
                {
                    _ToneLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0012)]
        public IntegraSwitch PortamentoSwitch
        {
            get => _PortamentoSwitch;
            set
            {
                if (_PortamentoSwitch != value)
                {
                    _PortamentoSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0013)]
        public byte PortamentoTime
        {
            get => _PortamentoTime;
            set 
            {
                if (_PortamentoTime != value)
                {
                    _PortamentoTime = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0014)]
        public IntegraSwitch MonoSwitch
        {
            get => _MonoSwitch;
            set
            {
                if (_MonoSwitch != value)
                {
                    _MonoSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0015)]
        public int OctaveShift
        {
            get => _OctaveShift.Deserialize(64);
            set
            {
                if (OctaveShift != value)
                {
                    _OctaveShift = value.Serialize(64).Clamp(61, 67);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0016)]
        public byte PitchBendRangeUp
        {
            get => _PitchBendRangeUp;
            set
            {
                if (_PitchBendRangeUp != value)
                {
                    _PitchBendRangeUp = value.Clamp(0, 48);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0017)]
        public byte PitchBendRangeDown
        {
            get => _PitchBendRangeDown;
            set
            {
                if (_PitchBendRangeDown != value)
                {
                    _PitchBendRangeDown = value.Clamp(0, 48);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0019)]
        public IntegraSwitch Partial01Switch
        {
            get { return _Partial01Switch; }
            set
            {
                _Partial01Switch = value;
                NotifyPropertyChanged();
            }
        }
        
        [Offset(0x001A)]
        public IntegraSwitch Partial01Select
        {
            get { return _Partial01Select; }
            set
            {
                _Partial01Select = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001B)]
        public IntegraSwitch Partial02Switch
        {
            get { return _Partial02Switch; }
            set
            {
                _Partial02Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001C)]
        public IntegraSwitch Partial02Select
        {
            get { return _Partial02Select; }
            set
            {
                _Partial02Select = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x001D)]
        public IntegraSwitch Partial03Switch
        {
            get { return _Partial03Switch; }
            set
            {
                _Partial03Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001E)]
        public IntegraSwitch Partial03Select
        {
            get { return _Partial03Select; }
            set
            {
                _Partial03Select = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001F)]
        public IntegraRingSwitch RingSwitch
        {
            get => _RingSwitch;
            set
            {
                if (_RingSwitch != value)
                {
                    _RingSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0020)]
        public IntegraSwitch TFXSwitch
        {
            get => _TFXSwitch;
            set
            {
                if (_TFXSwitch != value)
                {
                    _TFXSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002E)]
        public IntegraSwitch UnisonSwitch
        {
            get => _UnisonSwitch;
            set
            {
                if (_UnisonSwitch != value)
                {
                    _UnisonSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0031)]
        public IntegraPortamentoMode PortamentoMode
        {
            get => _PortamentoMode;
            set
            {
                if (_PortamentoMode != value)
                {
                    _PortamentoMode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0032)]
        public IntegraSwitch LegatoSwitch
        {
            get => _LegatoSwitch;
            set
            {
                if (_LegatoSwitch != value)
                {
                    _LegatoSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0034)]
        public byte AnalogFeel
        {
            get => _AnalogFeel;
            set
            {
                if (_AnalogFeel != value)
                {
                    _AnalogFeel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0035)]
        public byte WaveShape
        {
            get => _WaveShape;
            set
            {
                if (_WaveShape != value)
                {
                    _WaveShape = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0036)]
        public IntegraTemporaryToneCategories Category
        {
            get => _Category;
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0037)]
        public IntegraSynthPhrase PhraseNumber
        {
            get => _PhraseNumber;
            set
            {
                if (_PhraseNumber != value)
                {
                    _PhraseNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003B)]
        public int PhraseOctaveShift
        {
            get => _PhraseOctaveShift.Deserialize(64);
            set
            {
                if (PhraseOctaveShift != value)
                {
                    _PhraseOctaveShift = value.Serialize(64).Clamp(61, 67);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003C)]
        public IntegraUnisonSize UnisonSize
        {
            get => _UnisonSize;
            set
            {
                if (_UnisonSize != value)
                {
                    _UnisonSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
    }
}
