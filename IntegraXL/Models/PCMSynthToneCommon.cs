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
        [Offset(0x002B)] IntegraMatrixControlSource _MatrixControl1Source;
        [Offset(0x002C)] IntegraMatrixControlDestination _MatrixControl1Destination1;
        [Offset(0x002D)] byte _MatrixControl1Sens1;
        [Offset(0x002E)] IntegraMatrixControlDestination _MatrixControl1Destination2;
        [Offset(0x002F)] byte _MatrixControl1Sens2;
        [Offset(0x0030)] IntegraMatrixControlDestination _MatrixControl1Destination3;
        [Offset(0x0031)] byte _MatrixControl1Sens3;
        [Offset(0x0032)] IntegraMatrixControlDestination _MatrixControl1Destination4;
        [Offset(0x0033)] byte _MatrixControl1Sens4;
        [Offset(0x0034)] IntegraMatrixControlSource _MatrixControl2Source;
        [Offset(0x0035)] IntegraMatrixControlDestination _MatrixControl2Destination1;
        [Offset(0x0036)] byte _MatrixControl2Sens1;
        [Offset(0x0037)] IntegraMatrixControlDestination _MatrixControl2Destination2;
        [Offset(0x0038)] byte _MatrixControl2Sens2;
        [Offset(0x0039)] IntegraMatrixControlDestination _MatrixControl2Destination3;
        [Offset(0x003A)] byte _MatrixControl2Sens3;
        [Offset(0x003B)] IntegraMatrixControlDestination _MatrixControl2Destination4;
        [Offset(0x003C)] byte _MatrixControl2Sens4;
        [Offset(0x003D)] IntegraMatrixControlSource _MatrixControl3Source;
        [Offset(0x003E)] IntegraMatrixControlDestination _MatrixControl3Destination1;
        [Offset(0x003F)] byte _MatrixControl3Sens1;
        [Offset(0x0040)] IntegraMatrixControlDestination _MatrixControl3Destination2;
        [Offset(0x0041)] byte _MatrixControl3Sens2;
        [Offset(0x0042)] IntegraMatrixControlDestination _MatrixControl3Destination3;
        [Offset(0x0043)] byte _MatrixControl3Sens3;
        [Offset(0x0044)] IntegraMatrixControlDestination _MatrixControl3Destination4;
        [Offset(0x0045)] byte _MatrixControl3Sens4;
        [Offset(0x0046)] IntegraMatrixControlSource _MatrixControl4Source;
        [Offset(0x0047)] IntegraMatrixControlDestination _MatrixControl4Destination1;
        [Offset(0x0048)] byte _MatrixControl4Sens1;
        [Offset(0x0049)] IntegraMatrixControlDestination _MatrixControl4Destination2;
        [Offset(0x004A)] byte _MatrixControl4Sens2;
        [Offset(0x004B)] IntegraMatrixControlDestination _MatrixControl4Destination3;
        [Offset(0x004C)] byte _MatrixControl4Sens3;
        [Offset(0x004D)] IntegraMatrixControlDestination _MatrixControl4Destination4;
        [Offset(0x004E)] byte _MatrixControl4Sens4;
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
        public IntegraMatrixControlSource MatrixControl1Source
        {
            get => _MatrixControl1Source;
            set
            {
                if (_MatrixControl1Source != value)
                {
                    _MatrixControl1Source = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002C)]
        public IntegraMatrixControlDestination MatrixControl1Destination1
        {
            get => _MatrixControl1Destination1;
            set
            {
                if (_MatrixControl1Destination1 != value)
                {
                    _MatrixControl1Destination1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002D)]
        public int MatrixControl1Sens1
        {
            get => _MatrixControl1Sens1.Deserialize(64); 
            set
            {
                if (MatrixControl1Sens1 != value)
                {
                    _MatrixControl1Sens1 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002E)]
        public IntegraMatrixControlDestination MatrixControl1Destination2
        {
            get => _MatrixControl1Destination2;
            set
            {
                if (_MatrixControl1Destination2 != value)
                {
                    _MatrixControl1Destination2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002F)]
        public int MatrixControl1Sens2
        {
            get => _MatrixControl1Sens2.Deserialize(64);
            set
            {
                if (MatrixControl1Sens2 != value)
                {
                    _MatrixControl1Sens2 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0030)]
        public IntegraMatrixControlDestination MatrixControl1Destination3
        {
            get => _MatrixControl1Destination3;
            set
            {
                if (_MatrixControl1Destination3 != value)
                {
                    _MatrixControl1Destination3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0031)] 
        public int MatrixControl1Sens3
        {
            get => _MatrixControl1Sens3.Deserialize(64);
            set
            {
                if (MatrixControl1Sens3 != value)
                {
                    _MatrixControl1Sens3 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0032)]
        public IntegraMatrixControlDestination MatrixControl1Destination4
        {
            get => _MatrixControl1Destination4;
            set
            {
                if (_MatrixControl1Destination4 != value)
                {
                    _MatrixControl1Destination4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0033)]
        public int MatrixControl1Sens4
        {
            get => _MatrixControl1Sens4.Deserialize(64);
            set
            {
                if (MatrixControl1Sens4 != value)
                {
                    _MatrixControl1Sens4 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Matrix Control 02

        [Offset(0x0034)]
        public IntegraMatrixControlSource MatrixControl2Source
        {
            get => _MatrixControl2Source;
            set
            {
                if (_MatrixControl2Source != value)
                {
                    _MatrixControl2Source = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0035)]
        public IntegraMatrixControlDestination MatrixControl2Destination1
        {
            get => _MatrixControl2Destination1;
            set
            {
                if (_MatrixControl2Destination1 != value)
                {
                    _MatrixControl2Destination1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0036)]
        public int MatrixControl2Sens1
        {
            get => _MatrixControl2Sens1.Deserialize(64);
            set
            {
                if (MatrixControl2Sens1 != value)
                {
                    _MatrixControl2Sens1 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0037)]
        public IntegraMatrixControlDestination MatrixControl2Destination2
        {
            get => _MatrixControl2Destination2;
            set
            {
                if (_MatrixControl2Destination2 != value)
                {
                    _MatrixControl2Destination2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0038)]
        public int MatrixControl2Sens2
        {
            get => _MatrixControl2Sens2.Deserialize(64);
            set
            {
                if (MatrixControl2Sens2 != value)
                {
                    _MatrixControl2Sens2 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0039)]
        public IntegraMatrixControlDestination MatrixControl2Destination3
        {
            get => _MatrixControl2Destination3;
            set
            {
                if (_MatrixControl2Destination3 != value)
                {
                    _MatrixControl2Destination3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003A)]
        public int MatrixControl2Sens3
        {
            get => _MatrixControl2Sens3.Deserialize(64);
            set
            {
                if (MatrixControl2Sens3 != value)
                {
                    _MatrixControl2Sens3 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003B)]
        public IntegraMatrixControlDestination MatrixControl2Destination4
        {
            get => _MatrixControl2Destination4;
            set
            {
                if (_MatrixControl2Destination4 != value)
                {
                    _MatrixControl2Destination4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003C)]
        public int MatrixControl2Sens4
        {
            get => _MatrixControl2Sens4.Deserialize(64);
            set
            {
                if (MatrixControl2Sens4 != value)
                {
                    _MatrixControl2Sens4 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Matrix Control 03

        [Offset(0x003D)]
        public IntegraMatrixControlSource MatrixControl3Source
        {
            get => _MatrixControl3Source;
            set
            {
                if (_MatrixControl3Source != value)
                {
                    _MatrixControl3Source = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003E)]
        public IntegraMatrixControlDestination MatrixControl3Destination1
        {
            get => _MatrixControl3Destination1;
            set
            {
                if (_MatrixControl3Destination1 != value)
                {
                    _MatrixControl3Destination1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003F)]
        public int MatrixControl3Sens1
        {
            get => _MatrixControl3Sens1.Deserialize(64);
            set
            {
                if (MatrixControl3Sens1 != value)
                {
                    _MatrixControl3Sens1 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0040)]
        public IntegraMatrixControlDestination MatrixControl3Destination2
        {
            get => _MatrixControl3Destination2;
            set
            {
                if (_MatrixControl3Destination2 != value)
                {
                    _MatrixControl3Destination2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0041)]
        public int MatrixControl3Sens2
        {
            get => _MatrixControl3Sens2.Deserialize(64);
            set
            {
                if (MatrixControl3Sens2 != value)
                {
                    _MatrixControl3Sens2 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0042)]
        public IntegraMatrixControlDestination MatrixControl3Destination3
        {
            get => _MatrixControl3Destination3;
            set
            {
                if (_MatrixControl3Destination3 != value)
                {
                    _MatrixControl3Destination3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0043)]
        public int MatrixControl3Sens3
        {
            get => _MatrixControl3Sens3.Deserialize(64);
            set
            {
                if (MatrixControl3Sens3 != value)
                {
                    _MatrixControl3Sens3 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0044)]
        public IntegraMatrixControlDestination MatrixControl3Destination4
        {
            get => _MatrixControl3Destination4;
            set
            {
                if (_MatrixControl3Destination4 != value)
                {
                    _MatrixControl3Destination4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0045)]
        public int MatrixControl3Sens4
        {
            get => _MatrixControl3Sens4.Deserialize(64);
            set
            {
                if (MatrixControl3Sens4 != value)
                {
                    _MatrixControl3Sens4 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Matrix Control 04

        [Offset(0x0046)]
        public IntegraMatrixControlSource MatrixControl4Source
        {
            get => _MatrixControl4Source;
            set
            {
                if (_MatrixControl4Source != value)
                {
                    _MatrixControl4Source = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0047)]
        public IntegraMatrixControlDestination MatrixControl4Destination1
        {
            get => _MatrixControl4Destination1;
            set
            {
                if (_MatrixControl4Destination1 != value)
                {
                    _MatrixControl4Destination1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0048)]
        public int MatrixControl4Sens1
        {
            get => _MatrixControl4Sens1.Deserialize(64);
            set
            {
                if (MatrixControl4Sens1 != value)
                {
                    _MatrixControl4Sens1 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0049)]
        public IntegraMatrixControlDestination MatrixControl4Destination2
        {
            get => _MatrixControl4Destination2;
            set
            {
                if (_MatrixControl4Destination2 != value)
                {
                    _MatrixControl4Destination2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004A)]
        public int MatrixControl4Sens2
        {
            get => _MatrixControl4Sens2.Deserialize(64);
            set
            {
                if (MatrixControl4Sens2 != value)
                {
                    _MatrixControl4Sens2 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004B)]
        public IntegraMatrixControlDestination MatrixControl4Destination3
        {
            get => _MatrixControl4Destination3;
            set
            {
                if (_MatrixControl4Destination3 != value)
                {
                    _MatrixControl4Destination3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004C)]
        public int MatrixControl4Sens3
        {
            get => _MatrixControl4Sens3.Deserialize(64);
            set
            {
                if (MatrixControl4Sens3 != value)
                {
                    _MatrixControl4Sens3 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004D)]
        public IntegraMatrixControlDestination MatrixControl4Destination4
        {
            get => _MatrixControl4Destination4;
            set
            {
                if (_MatrixControl4Destination4 != value)
                {
                    _MatrixControl4Destination4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004E)]
        public int MatrixControl4Sens4
        {
            get => _MatrixControl4Sens4.Deserialize(64);
            set
            {
                if (MatrixControl4Sens4 != value)
                {
                    _MatrixControl4Sens4 = value.Serialize(64).Clamp(1, 127);
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
