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
        [Offset(0x0036)] byte _ToneCategory;
        [Offset(0x0037)] int _PhraseNumber;
        [Offset(0x003B)] byte _PhraseOctaveShift;
        [Offset(0x003C)] byte _UnisonSize;
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
            get { return Encoding.ASCII.GetString(_ToneName); }
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
            get { return _ToneLevel; }
            set
            {
                _ToneLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0012)]
        public IntegraSwitch PortamentoSwitch
        {
            get { return _PortamentoSwitch; }
            set
            {
                _PortamentoSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0013)]
        public  byte PortamentoTime
        {
            get { return _PortamentoTime; }
            set 
            { 
                _PortamentoTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0014)]
        public IntegraSwitch MonoSwitch
        {
            get { return _MonoSwitch; }
            set
            {
                _MonoSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0015)]
        public byte OctaveShift
        {
            get { return _OctaveShift; }
            set
            {
                _OctaveShift = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0016)]
        public byte PitchBendRangeUp
        {
            get { return _PitchBendRangeUp; }
            set
            {
                _PitchBendRangeUp = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0017)]
        public byte PitchBendRangeDown
        {
            get { return _PitchBendRangeDown; }
            set
            {
                _PitchBendRangeDown = value;
                NotifyPropertyChanged();
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
            get { return _RingSwitch; }
            set
            {
                _RingSwitch = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0020)]
        public IntegraSwitch TFXSwitch
        {
            get { return _TFXSwitch; }
            set
            {
                _TFXSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002E)]
        public IntegraSwitch UnisonSwitch
        {
            get { return _UnisonSwitch; }
            set
            {
                _UnisonSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0031)]
        public IntegraPortamentoMode PortamentoMode
        {
            get { return _PortamentoMode; }
            set
            {
                _PortamentoMode = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0032)]
        public IntegraSwitch LegatoSwitch
        {
            get { return _LegatoSwitch; }
            set
            {
                _LegatoSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0034)]
        public byte AnalogFeel
        {
            get { return _AnalogFeel; }
            set
            {
                _AnalogFeel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0035)]
        public byte WaveShape
        {
            get { return _WaveShape; }
            set
            {
                _WaveShape = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0036)]
        public byte ToneCategory
        {
            get { return _ToneCategory; }
            set
            {
                _ToneCategory = value;
                NotifyPropertyChanged();
            }
        }

        //[Offset(0x0037)]
        //public int PhraseNumber
        //{
        //    get
        //    {
        //        return _PhraseNumber.DeserializeInt();
        //    }
        //    set
        //    {
        //        _PhraseNumber = value.SerializeInt();
        //        NotifyPropertyChanged();
        //    }
        //}

        [Offset(0x003B)]
        public byte PhraseOctaveShift
        {
            get { return _PhraseOctaveShift; }
            set
            {
                _PhraseOctaveShift = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003C)]
        public byte UnisonSize
        {
            get { return _UnisonSize; }
            set
            {
                _UnisonSize = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
