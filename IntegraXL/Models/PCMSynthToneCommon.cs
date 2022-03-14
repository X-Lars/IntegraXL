using IntegraXL.Core;
using IntegraXL.Extensions;
using System.Text;

namespace IntegraXL.Models
{
    [Integra(0x00000000, 0x00000050)]
    public class PCMSynthToneCommon : IntegraModel<PCMSynthToneCommon>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private byte[] _ToneName = new byte[12];
        [Offset(0x000C)] byte[] RESERVED01 = new byte[2];
        [Offset(0x000E)] byte _ToneLevel;
        [Offset(0x000F)] byte _Pan;
        [Offset(0x0010)] IntegraTonePriority _Priority;
        [Offset(0x0011)] byte _CoarseTune;
        [Offset(0x0012)] byte _FineTune;
        [Offset(0x0013)] byte _OctaveShift;
        [Offset(0x0014)] IntegraStretchTuneDepth _StretchTuneDepth;
        [Offset(0x0015)] byte _AnalogFeel;
        [Offset(0x0016)] IntegraMonyPolySwitch _MonoPoly;
        [Offset(0x0017)] IntegraSwitch _LegatoSwitch;
        [Offset(0x0018)] IntegraSwitch _LegatoRetrigger;
        [Offset(0x0019)] IntegraSwitch _PortamentoSwitch;
        [Offset(0x001A)] IntegraPortamentoMode _PortamentoMode;
        [Offset(0x001B)] IntegraPortamentoType _PortamentoType;
        [Offset(0x001C)] IntegraPortamentoStart _PortamentoStart;
        [Offset(0x001D)] byte _PortamentoTime;
        [Offset(0x001E)] byte[] RESERVED02 = new byte[4];
        [Offset(0x0022)] byte _CutoffOffset;
        [Offset(0x0023)] byte _ResonanceOffset;
        [Offset(0x0024)] byte _AttackTimeOffset;
        [Offset(0x0025)] byte _ReleaseTimeOffset;
        [Offset(0x0026)] byte _VelocitySensOffset;
        [Offset(0x0027)] byte RESERVED03;
        [Offset(0x0028)] IntegraSwitch _PMTControlSwitch;
        [Offset(0x0029)] byte _PitchBendRangeUp;
        [Offset(0x002A)] byte _PitchBendRangeDown;
        [Offset(0x002B)] IntegraMatrixControlSource _MatrixControl01Source;
        [Offset(0x002C)] IntegraMatrixControlDestination _MatrixControl01Destination01;
        [Offset(0x002D)] byte _MatrixControl01Sens01;
        [Offset(0x002E)] IntegraMatrixControlDestination _MatrixControl01Destination02;
        [Offset(0x002F)] byte _MatrixControl01Sens02;
        [Offset(0x0030)] IntegraMatrixControlDestination _MatrixControl01Destination03;
        [Offset(0x0031)] byte _MatrixControl01Sens03;
        [Offset(0x0032)] IntegraMatrixControlDestination _MatrixControl01Destination04;
        [Offset(0x0033)] byte _MatrixControl01Sens04;
        [Offset(0x0034)] IntegraMatrixControlSource _MatrixControl02Source;
        [Offset(0x0035)] IntegraMatrixControlDestination _MatrixControl02Destination01;
        [Offset(0x0036)] byte _MatrixControl02Sens01;
        [Offset(0x0037)] IntegraMatrixControlDestination _MatrixControl02Destination02;
        [Offset(0x0038)] byte _MatrixControl02Sens02;
        [Offset(0x0039)] IntegraMatrixControlDestination _MatrixControl02Destination03;
        [Offset(0x003A)] byte _MatrixControl02Sens03;
        [Offset(0x003B)] IntegraMatrixControlDestination _MatrixControl02Destination04;
        [Offset(0x003C)] byte _MatrixControl02Sens04;
        [Offset(0x003D)] IntegraMatrixControlSource _MatrixControl03Source;
        [Offset(0x003E)] IntegraMatrixControlDestination _MatrixControl03Destination01;
        [Offset(0x003F)] byte _MatrixControl03Sens01;
        [Offset(0x0040)] IntegraMatrixControlDestination _MatrixControl03Destination02;
        [Offset(0x0041)] byte _MatrixControl03Sens02;
        [Offset(0x0042)] IntegraMatrixControlDestination _MatrixControl03Destination03;
        [Offset(0x0043)] byte _MatrixControl03Sens03;
        [Offset(0x0044)] IntegraMatrixControlDestination _MatrixControl03Destination04;
        [Offset(0x0045)] byte _MatrixControl03Sens04;
        [Offset(0x0046)] IntegraMatrixControlSource _MatrixControl04Source;
        [Offset(0x0047)] IntegraMatrixControlDestination _MatrixControl04Destination01;
        [Offset(0x0048)] byte _MatrixControl04Sens01;
        [Offset(0x0049)] IntegraMatrixControlDestination _MatrixControl04Destination02;
        [Offset(0x004A)] byte _MatrixControl04Sens02;
        [Offset(0x004B)] IntegraMatrixControlDestination _MatrixControl04Destination03;
        [Offset(0x004C)] byte _MatrixControl04Sens03;
        [Offset(0x004D)] IntegraMatrixControlDestination _MatrixControl04Destination04;
        [Offset(0x004E)] byte _MatrixControl04Sens04;
        [Offset(0x004F)] byte RESERVED04;

        #endregion

        #region Constructor

        public PCMSynthToneCommon(PCMSynthTone synthTone) : base(synthTone.Device)
        {
            Address = synthTone.Address;
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

        [Offset(0x000E)]
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

        [Offset(0x000F)]
        public byte Pan
        {
            get => _Pan;
            set
            {
                if (_Pan != value)
                {
                    _Pan = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0010)]
        public IntegraTonePriority Priority
        {
            get => _Priority;
            set
            {
                if (_Priority != value)
                {
                    _Priority = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0011)]
        public int CoarseTune
        {
            get => _CoarseTune.Deserialize(64);
            set
            {
                if (CoarseTune != value)
                {
                    _CoarseTune = value.Serialize(64).Clamp(16, 112);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0012)]
        public int FineTune
        {
            get => _FineTune.Deserialize(64);
            set
            {
                if (FineTune != value)
                {
                    _FineTune = value.Serialize(64).Clamp(14, 114);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0013)]
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

        [Offset(0x0014)]
        public IntegraStretchTuneDepth StretchTuneDepth
        {
            get => _StretchTuneDepth;
            set
            {
                if (_StretchTuneDepth != value)
                {
                    _StretchTuneDepth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0015)]
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

        [Offset(0x0016)]
        public IntegraMonyPolySwitch MonoPoly
        {
            get => _MonoPoly;
            set
            {
                if (_MonoPoly != value)
                {
                    _MonoPoly = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0017)] 
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

        [Offset(0x0018)]
        public IntegraSwitch LegatoRetrigger
        {
            get => _LegatoRetrigger;
            set
            {
                if (_LegatoRetrigger != value)
                {
                    _LegatoRetrigger = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #region Portamento

        [Offset(0x0019)]
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

        [Offset(0x001A)]
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
        [Offset(0x001B)]
        public IntegraPortamentoType PortamentoType
        {
            get => _PortamentoType;
            set
            {
                if (_PortamentoType != value)
                {
                    _PortamentoType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001C)]
        public IntegraPortamentoStart PortamentoStart
        {
            get => _PortamentoStart;
            set
            {
                if (_PortamentoStart != value)
                {
                    _PortamentoStart = value;
                    NotifyPropertyChanged();
                }
            }
        }
        [Offset(0x001D)]
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

        #endregion

        #region Offset

        [Offset(0x0022)]
        public int CutoffOffset
        {
            get => _CutoffOffset.Deserialize(64);
            set
            {
                if (CutoffOffset != value)
                {
                    _CutoffOffset = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0023)]
        public int ResonanceOffset
        {
            get => _ResonanceOffset.Deserialize(64);
            set
            {
                if (ResonanceOffset != value)
                {
                    _ResonanceOffset = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0024)]
        public int AttackTimeOffset
        {
            get => _AttackTimeOffset.Deserialize(64);
            set
            {
                if (AttackTimeOffset != value)
                {
                    _AttackTimeOffset = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0025)]
        public int ReleaseTimeOffset
        {
            get => _ReleaseTimeOffset.Deserialize(64);
            set
            {
                if (ReleaseTimeOffset != value)
                {
                    _ReleaseTimeOffset = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0026)]
        public int VelocitySensOffset
        {
            get => _VelocitySensOffset.Deserialize(64);
            set
            {
                if (VelocitySensOffset != value)
                {
                    _VelocitySensOffset = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        [Offset(0x0028)]
        public IntegraSwitch PMTControlSwitch
        {
            get => _PMTControlSwitch;
            set
            {
                if (_PMTControlSwitch != value)
                {
                    _PMTControlSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0029)]
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

        [Offset(0x002A)]
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

        #region Matrix Control 01

        [Offset(0x002B)]
        public IntegraMatrixControlSource MatrixControl01Source
        {
            get => _MatrixControl01Source;
            set
            {
                if (_MatrixControl01Source != value)
                {
                    _MatrixControl01Source = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002C)]
        public IntegraMatrixControlDestination MatrixControl01Destination01
        {
            get => _MatrixControl01Destination01;
            set
            {
                if (_MatrixControl01Destination01 != value)
                {
                    _MatrixControl01Destination01 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002D)]
        public int MatrixControl01Sens01
        {
            get => _MatrixControl01Sens01.Deserialize(64); 
            set
            {
                if (MatrixControl01Sens01 != value)
                {
                    _MatrixControl01Sens01 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002E)]
        public IntegraMatrixControlDestination MatrixControl01Destination02
        {
            get => _MatrixControl01Destination02;
            set
            {
                if (_MatrixControl01Destination02 != value)
                {
                    _MatrixControl01Destination02 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002F)]
        public int MatrixControl01Sens02
        {
            get => _MatrixControl01Sens02.Deserialize(64);
            set
            {
                if (MatrixControl01Sens02 != value)
                {
                    _MatrixControl01Sens02 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0030)]
        public IntegraMatrixControlDestination MatrixControl01Destination03
        {
            get => _MatrixControl01Destination03;
            set
            {
                if (_MatrixControl01Destination03 != value)
                {
                    _MatrixControl01Destination03 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0031)] 
        public int MatrixControl01Sens03
        {
            get => _MatrixControl01Sens03.Deserialize(64);
            set
            {
                if (MatrixControl01Sens03 != value)
                {
                    _MatrixControl01Sens03 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0032)]
        public IntegraMatrixControlDestination MatrixControl01Destination04
        {
            get => _MatrixControl01Destination04;
            set
            {
                if (_MatrixControl01Destination04 != value)
                {
                    _MatrixControl01Destination04 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0033)]
        public int MatrixControl01Sens04
        {
            get => _MatrixControl01Sens04.Deserialize(64);
            set
            {
                if (MatrixControl01Sens04 != value)
                {
                    _MatrixControl01Sens04 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Matrix Control 02

        [Offset(0x0034)]
        public IntegraMatrixControlSource MatrixControl02Source
        {
            get => _MatrixControl02Source;
            set
            {
                if (_MatrixControl02Source != value)
                {
                    _MatrixControl02Source = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0035)]
        public IntegraMatrixControlDestination MatrixControl02Destination01
        {
            get => _MatrixControl02Destination01;
            set
            {
                if (_MatrixControl02Destination01 != value)
                {
                    _MatrixControl02Destination01 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0036)]
        public int MatrixControl02Sens01
        {
            get => _MatrixControl02Sens01.Deserialize(64);
            set
            {
                if (MatrixControl02Sens01 != value)
                {
                    _MatrixControl02Sens01 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0037)]
        public IntegraMatrixControlDestination MatrixControl02Destination02
        {
            get => _MatrixControl02Destination02;
            set
            {
                if (_MatrixControl02Destination02 != value)
                {
                    _MatrixControl02Destination02 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0038)]
        public int MatrixControl02Sens02
        {
            get => _MatrixControl02Sens02.Deserialize(64);
            set
            {
                if (MatrixControl02Sens02 != value)
                {
                    _MatrixControl02Sens02 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0039)]
        public IntegraMatrixControlDestination MatrixControl02Destination03
        {
            get => _MatrixControl02Destination03;
            set
            {
                if (_MatrixControl02Destination03 != value)
                {
                    _MatrixControl02Destination03 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003A)]
        public int MatrixControl02Sens03
        {
            get => _MatrixControl02Sens03.Deserialize(64);
            set
            {
                if (MatrixControl02Sens03 != value)
                {
                    _MatrixControl02Sens03 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003B)]
        public IntegraMatrixControlDestination MatrixControl02Destination04
        {
            get => _MatrixControl02Destination04;
            set
            {
                if (_MatrixControl02Destination04 != value)
                {
                    _MatrixControl02Destination04 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003C)]
        public int MatrixControl02Sens04
        {
            get => _MatrixControl02Sens04.Deserialize(64);
            set
            {
                if (MatrixControl02Sens04 != value)
                {
                    _MatrixControl02Sens04 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Matrix Control 03

        [Offset(0x003D)]
        public IntegraMatrixControlSource MatrixControl03Source
        {
            get => _MatrixControl03Source;
            set
            {
                if (_MatrixControl03Source != value)
                {
                    _MatrixControl03Source = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003E)]
        public IntegraMatrixControlDestination MatrixControl03Destination01
        {
            get => _MatrixControl03Destination01;
            set
            {
                if (_MatrixControl03Destination01 != value)
                {
                    _MatrixControl03Destination01 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003F)]
        public int MatrixControl03Sens01
        {
            get => _MatrixControl03Sens01.Deserialize(64);
            set
            {
                if (MatrixControl03Sens01 != value)
                {
                    _MatrixControl03Sens01 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0040)]
        public IntegraMatrixControlDestination MatrixControl03Destination02
        {
            get => _MatrixControl03Destination02;
            set
            {
                if (_MatrixControl03Destination02 != value)
                {
                    _MatrixControl03Destination02 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0041)]
        public int MatrixControl03Sens02
        {
            get => _MatrixControl03Sens02.Deserialize(64);
            set
            {
                if (MatrixControl03Sens02 != value)
                {
                    _MatrixControl03Sens02 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0042)]
        public IntegraMatrixControlDestination MatrixControl03Destination03
        {
            get => _MatrixControl03Destination03;
            set
            {
                if (_MatrixControl03Destination03 != value)
                {
                    _MatrixControl03Destination03 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0043)]
        public int MatrixControl03Sens03
        {
            get => _MatrixControl03Sens03.Deserialize(64);
            set
            {
                if (MatrixControl03Sens03 != value)
                {
                    _MatrixControl03Sens03 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0044)]
        public IntegraMatrixControlDestination MatrixControl03Destination04
        {
            get => _MatrixControl03Destination04;
            set
            {
                if (_MatrixControl03Destination04 != value)
                {
                    _MatrixControl03Destination04 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0045)]
        public int MatrixControl03Sens04
        {
            get => _MatrixControl03Sens04.Deserialize(64);
            set
            {
                if (MatrixControl03Sens04 != value)
                {
                    _MatrixControl03Sens04 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Matrix Control 04

        [Offset(0x0046)]
        public IntegraMatrixControlSource MatrixControl04Source
        {
            get => _MatrixControl04Source;
            set
            {
                if (_MatrixControl04Source != value)
                {
                    _MatrixControl04Source = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0047)]
        public IntegraMatrixControlDestination MatrixControl04Destination01
        {
            get => _MatrixControl04Destination01;
            set
            {
                if (_MatrixControl04Destination01 != value)
                {
                    _MatrixControl04Destination01 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0048)]
        public int MatrixControl04Sens01
        {
            get => _MatrixControl04Sens01.Deserialize(64);
            set
            {
                if (MatrixControl04Sens01 != value)
                {
                    _MatrixControl04Sens01 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0049)]
        public IntegraMatrixControlDestination MatrixControl04Destination02
        {
            get => _MatrixControl04Destination02;
            set
            {
                if (_MatrixControl04Destination02 != value)
                {
                    _MatrixControl04Destination02 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004A)]
        public int MatrixControl04Sens02
        {
            get => _MatrixControl04Sens02.Deserialize(64);
            set
            {
                if (MatrixControl04Sens02 != value)
                {
                    _MatrixControl04Sens02 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004B)]
        public IntegraMatrixControlDestination MatrixControl04Destination03
        {
            get => _MatrixControl04Destination03;
            set
            {
                if (_MatrixControl04Destination03 != value)
                {
                    _MatrixControl04Destination03 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004C)]
        public int MatrixControl04Sens03
        {
            get => _MatrixControl04Sens03.Deserialize(64);
            set
            {
                if (MatrixControl04Sens03 != value)
                {
                    _MatrixControl04Sens03 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004D)]
        public IntegraMatrixControlDestination MatrixControl04Destination04
        {
            get => _MatrixControl04Destination04;
            set
            {
                if (_MatrixControl04Destination04 != value)
                {
                    _MatrixControl04Destination04 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004E)]
        public int MatrixControl04Sens04
        {
            get => _MatrixControl04Sens04.Deserialize(64);
            set
            {
                if (MatrixControl04Sens04 != value)
                {
                    _MatrixControl04Sens04 = value.Serialize(64).Clamp(1, 127);
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
