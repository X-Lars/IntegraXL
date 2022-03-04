using IntegraXL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Models
{
    [Integra(0x00000000, 0x00000050)]
    public class PCMSynthToneCommon : IntegraModel<PCMSynthToneCommon>
    {
        [Offset(0x0000)] byte[] _ToneName = new byte[12];
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



        public PCMSynthToneCommon(PCMSynthTone pcmSynthtone) : base(pcmSynthtone.Device)
        {
            Address = pcmSynthtone.Address;
        }

        [Offset(0x0000)]
        public string ToneName
        {
            get { return Encoding.ASCII.GetString(_ToneName); }
            set
            {
                _ToneName = Encoding.ASCII.GetBytes(value);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000E)]
        public byte ToneLevel
        {
            get { return _ToneLevel; }
            set
            {
                _ToneLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000F)]
        public byte Pan
        {
            get { return _Pan; }
            set
            {
                _Pan = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0010)]
        public IntegraTonePriority Priority
        {
            get { return _Priority; }
            set
            {
                _Priority = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0011)]
        public byte CoarseTune
        {
            get { return _CoarseTune; }
            set
            {
                _CoarseTune = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0012)]
        public byte FineTune
        {
            get { return _FineTune; }
            set
            {
                _FineTune = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0013)]
        public byte OctaveShift
        {
            get { return _OctaveShift; }
            set
            {
                _OctaveShift = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0014)]
        public IntegraStretchTuneDepth StretchTuneDepth
        {
            get { return _StretchTuneDepth; }
            set
            {
                _StretchTuneDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0015)]
        public byte AnalogFeel
        {
            get { return _AnalogFeel; }
            set
            {
                _AnalogFeel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0016)]
        public IntegraMonyPolySwitch MonoPoly
        {
            get { return _MonoPoly; }
            set
            {
                _MonoPoly = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0017)] 
        public IntegraSwitch LegatoSwitch
        {
            get { return _LegatoSwitch; }
            set
            {
                _LegatoSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0018)]
        public IntegraSwitch LegatoRetrigger
        {
            get { return _LegatoRetrigger; }
            set
            {
                _LegatoRetrigger = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0019)]
        public IntegraSwitch PortamentoSwitch
        {
            get { return _PortamentoSwitch; }
            set
            {
                _PortamentoSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001A)]
        public IntegraPortamentoMode PortamentoMode
        {
            get { return _PortamentoMode; }
            set
            {
                _PortamentoMode = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x001B)]
        public IntegraPortamentoType PortamentoType
        {
            get { return _PortamentoType; }
            set
            {
                _PortamentoType = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001C)]
        public IntegraPortamentoStart PortamentoStart
        {
            get { return _PortamentoStart; }
            set
            {
                _PortamentoStart = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x001D)]
        public byte PortamentoTime
        {
            get { return _PortamentoTime; }
            set
            {
                _PortamentoTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0022)]
        public byte CutoffOffset
        {
            get { return _CutoffOffset; }
            set
            {
                _CutoffOffset = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0023)]
        public byte ResonanceOffset
        {
            get { return _ResonanceOffset; }
            set
            {
                _ResonanceOffset = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0024)]
        public byte AttackTimeOffset
        {
            get { return _AttackTimeOffset; }
            set
            {
                _AttackTimeOffset = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0025)]
        public byte ReleaseTimeOffset
        {
            get { return _ReleaseTimeOffset; }
            set
            {
                _ReleaseTimeOffset = value;
            }
        }

        [Offset(0x0026)]
        public byte VelocitySensOffset
        {
            get { return _VelocitySensOffset; }
            set
            {
                _VelocitySensOffset = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0028)]
        public IntegraSwitch PMTControlSwitch
        {
            get { return _PMTControlSwitch; }
            set
            {
                _PMTControlSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0029)]
        public byte PitchBendRangeUp
        {
            get { return _PitchBendRangeUp; }
            set
            {
                _PitchBendRangeUp = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002A)]
        public byte PitchBendRangeDown
        {
            get { return _PitchBendRangeDown; }
            set
            {
                _PitchBendRangeDown = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002B)]
        public IntegraMatrixControlSource MatrixControl01Source
        {
            get { return _MatrixControl01Source; }
            set
            {
                _MatrixControl01Source = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002C)]
        public IntegraMatrixControlDestination MatrixControl01Destination01
        {
            get { return _MatrixControl01Destination01; }
            set
            {
                _MatrixControl01Destination01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002D)]
        public byte MatrixControl01Sens01
        {
            get { return _MatrixControl01Sens01; }
            set
            {
                _MatrixControl01Sens01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002E)]
        public IntegraMatrixControlDestination MatrixControl01Destination02
        {
            get { return _MatrixControl01Destination02; }
            set
            {
                _MatrixControl01Destination02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002F)]
        public byte MatrixControl01Sens02
        {
            get { return _MatrixControl01Sens02; }
            set
            {
                _MatrixControl01Sens02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0030)]
        public IntegraMatrixControlDestination MatrixControl01Destination03
        {
            get { return _MatrixControl01Destination03; }
            set
            {
                _MatrixControl01Destination03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0031)] 
        public byte MatrixControl01Sens03 
        {
            get {return _MatrixControl01Sens03;} 
            set{_MatrixControl01Sens03 = value;
                NotifyPropertyChanged();}}
        [Offset(0x0032)]
        public IntegraMatrixControlDestination MatrixControl01Destination04
        {
            get { return _MatrixControl01Destination04; }
            set
            {
                _MatrixControl01Destination04 = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0033)]
        public byte MatrixControl01Sens04
        {
            get { return _MatrixControl01Sens04; }
            set
            {
                _MatrixControl01Sens04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0034)]
        public IntegraMatrixControlSource MatrixControl02Source
        {
            get { return _MatrixControl02Source; }
            set
            {
                _MatrixControl02Source = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0035)]
        public IntegraMatrixControlDestination MatrixControl02Destination01
        {
            get { return _MatrixControl02Destination01; }
            set
            {
                _MatrixControl02Destination01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0036)]
        public byte MatrixControl02Sens01
        {
            get { return _MatrixControl02Sens01; }
            set
            {
                _MatrixControl02Sens01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0037)]
        public IntegraMatrixControlDestination MatrixControl02Destination02
        {
            get { return _MatrixControl02Destination02; }
            set
            {
                _MatrixControl02Destination02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0038)]
        public byte MatrixControl02Sens02
        {
            get { return _MatrixControl02Sens02; }
            set
            {
                _MatrixControl02Sens02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0039)]
        public IntegraMatrixControlDestination MatrixControl02Destination03
        {
            get { return _MatrixControl02Destination03; }
            set
            {
                _MatrixControl02Destination03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003A)]
        public byte MatrixControl02Sens03
        {
            get { return _MatrixControl02Sens03; }
            set
            {
                _MatrixControl02Sens03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003B)]
        public IntegraMatrixControlDestination MatrixControl02Destination04
        {
            get { return _MatrixControl02Destination04; }
            set
            {
                _MatrixControl02Destination04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003C)]
        public byte MatrixControl02Sens04
        {
            get { return _MatrixControl02Sens04; }
            set
            {
                _MatrixControl02Sens04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003D)]
        public IntegraMatrixControlSource MatrixControl03Source
        {
            get { return _MatrixControl03Source; }
            set
            {
                _MatrixControl03Source = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003E)]
        public IntegraMatrixControlDestination MatrixControl03Destination01
        {
            get { return _MatrixControl03Destination01; }
            set
            {
                _MatrixControl03Destination01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003F)]
        public byte MatrixControl03Sens01
        {
            get { return _MatrixControl03Sens01; }
            set
            {
                _MatrixControl03Sens01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0040)]
        public IntegraMatrixControlDestination MatrixControl03Destination02
        {
            get { return _MatrixControl03Destination02; }
            set
            {
                _MatrixControl03Destination02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0041)]
        public byte MatrixControl03Sens02
        {
            get { return _MatrixControl03Sens02; }
            set
            {
                _MatrixControl03Sens02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0042)]
        public IntegraMatrixControlDestination MatrixControl03Destination03
        {
            get { return _MatrixControl03Destination03; }
            set
            {
                _MatrixControl03Destination03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0043)]
        public byte MatrixControl03Sens03
        {
            get { return _MatrixControl03Sens03; }
            set
            {
                _MatrixControl03Sens03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0044)]
        public IntegraMatrixControlDestination MatrixControl03Destination04
        {
            get { return _MatrixControl03Destination04; }
            set
            {
                _MatrixControl03Destination04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0045)]
        public byte MatrixControl03Sens04
        {
            get { return _MatrixControl03Sens04; }
            set
            {
                _MatrixControl03Sens04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0046)]
        public IntegraMatrixControlSource MatrixControl04Source
        {
            get { return _MatrixControl04Source; }
            set
            {
                _MatrixControl04Source = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0047)]
        public IntegraMatrixControlDestination MatrixControl04Destination01
        {
            get { return _MatrixControl04Destination01; }
            set
            {
                _MatrixControl04Destination01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0048)]
        public byte MatrixControl04Sens01
        {
            get { return _MatrixControl04Sens01; }
            set
            {
                _MatrixControl04Sens01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0049)]
        public IntegraMatrixControlDestination MatrixControl04Destination02
        {
            get { return _MatrixControl04Destination02; }
            set
            {
                _MatrixControl04Destination02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004A)]
        public byte MatrixControl04Sens02
        {
            get { return _MatrixControl04Sens02; }
            set
            {
                _MatrixControl04Sens02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004B)]
        public IntegraMatrixControlDestination MatrixControl04Destination03
        {
            get { return _MatrixControl04Destination03; }
            set
            {
                _MatrixControl04Destination03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004C)]
        public byte MatrixControl04Sens03
        {
            get { return _MatrixControl04Sens03; }
            set
            {
                _MatrixControl04Sens03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004D)]
        public IntegraMatrixControlDestination MatrixControl04Destination04
        {
            get { return _MatrixControl04Destination04; }
            set
            {
                _MatrixControl04Destination04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004E)]
        public byte MatrixControl04Sens04
        {
            get { return _MatrixControl04Sens04; }
            set
            {
                _MatrixControl04Sens04 = value;
                NotifyPropertyChanged();
            }
        }
    }
}
