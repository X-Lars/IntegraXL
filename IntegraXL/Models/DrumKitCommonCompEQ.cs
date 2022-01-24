using IntegraXL.Core;
using System.Reflection;

namespace IntegraXL.Models
{
    [Integra(0x00000800, 0x00000054)]
    public class DrumKitCommonCompEQ : IntegraModel<DrumKitCommonCompEQ>
    {
        #region Fields: INTEGRA-7

        #region Fields: Compressor/EQ 01

        [Offset(0x0000)] IntegraSwitch _Comp01Switch;
        [Offset(0x0001)] byte _Comp01AttackTime;
        [Offset(0x0002)] byte _Comp01ReleaseTime;
        [Offset(0x0003)] byte _Comp01Threshold;
        [Offset(0x0004)] byte _Comp01Ratio;
        [Offset(0x0005)] byte _Comp01OutputGain;
        [Offset(0x0006)] IntegraSwitch _EQ01Switch;
        [Offset(0x0007)] IntegraLowFrequencies _EQ01LowFreq;
        [Offset(0x0008)] byte _EQ01LowGain;
        [Offset(0x0009)] IntegraMidFrequencies _EQ01MidFreq;
        [Offset(0x000A)] byte _EQ01MidGain;
        [Offset(0x000B)] IntegraMidQs _EQ01MidQ;
        [Offset(0x000C)] IntegraHighFrequencies _EQ01HighFreq;
        [Offset(0x000D)] byte _EQ01HighGain;

        #endregion

        #region Fields: Compressor/EQ 02

        [Offset(0x000E)] IntegraSwitch _Comp02Switch;
        [Offset(0x000F)] byte _Comp02AttackTime;
        [Offset(0x0010)] byte _Comp02ReleaseTime;
        [Offset(0x0011)] byte _Comp02Threshold;
        [Offset(0x0012)] byte _Comp02Ratio;
        [Offset(0x0013)] byte _Comp02OutputGain;
        [Offset(0x0014)] IntegraSwitch _EQ02Switch;
        [Offset(0x0015)] IntegraLowFrequencies _EQ02LowFreq;
        [Offset(0x0016)] byte _EQ02LowGain;
        [Offset(0x0017)] IntegraMidFrequencies _EQ02MidFreq;
        [Offset(0x0018)] byte _EQ02MidGain;
        [Offset(0x0019)] IntegraMidQs _EQ02MidQ;
        [Offset(0x001A)] IntegraHighFrequencies _EQ02HighFreq;
        [Offset(0x001B)] byte _EQ02HighGain;

        #endregion

        #region Fields: Compressor/EQ 03

        [Offset(0x001C)] IntegraSwitch _Comp03Switch;
        [Offset(0x001D)] byte _Comp03AttackTime;
        [Offset(0x001E)] byte _Comp03ReleaseTime;
        [Offset(0x001F)] byte _Comp03Threshold;
        [Offset(0x0020)] byte _Comp03Ratio;
        [Offset(0x0021)] byte _Comp03OutputGain;
        [Offset(0x0022)] IntegraSwitch _EQ03Switch;
        [Offset(0x0023)] IntegraLowFrequencies _EQ03LowFreq;
        [Offset(0x0024)] byte _EQ03LowGain;
        [Offset(0x0025)] IntegraMidFrequencies _EQ03MidFreq;
        [Offset(0x0026)] byte _EQ03MidGain;
        [Offset(0x0027)] IntegraMidQs _EQ03MidQ;
        [Offset(0x0028)] IntegraHighFrequencies _EQ03HighFreq;
        [Offset(0x0029)] byte _EQ03HighGain;

        #endregion

        #region Fields: Compressor/EQ 04

        [Offset(0x002A)] IntegraSwitch _Comp04Switch;
        [Offset(0x002B)] byte _Comp04AttackTime;
        [Offset(0x002C)] byte _Comp04ReleaseTime;
        [Offset(0x002D)] byte _Comp04Threshold;
        [Offset(0x002E)] byte _Comp04Ratio;
        [Offset(0x002F)] byte _Comp04OutputGain;
        [Offset(0x0030)] IntegraSwitch _EQ04Switch;
        [Offset(0x0031)] IntegraLowFrequencies _EQ04LowFreq;
        [Offset(0x0032)] byte _EQ04LowGain;
        [Offset(0x0033)] IntegraMidFrequencies _EQ04MidFreq;
        [Offset(0x0034)] byte _EQ04MidGain;
        [Offset(0x0035)] IntegraMidQs _EQ04MidQ;
        [Offset(0x0036)] IntegraHighFrequencies _EQ04HighFreq;
        [Offset(0x0037)] byte _EQ04HighGain;

        #endregion

        #region Fields: Compressor/EQ 05

        [Offset(0x0038)] IntegraSwitch _Comp05Switch;
        [Offset(0x0039)] byte _Comp05AttackTime;
        [Offset(0x003A)] byte _Comp05ReleaseTime;
        [Offset(0x003B)] byte _Comp05Threshold;
        [Offset(0x003C)] byte _Comp05Ratio;
        [Offset(0x003D)] byte _Comp05OutputGain;
        [Offset(0x003E)] IntegraSwitch _EQ05Switch;
        [Offset(0x003F)] IntegraLowFrequencies _EQ05LowFreq;
        [Offset(0x0040)] byte _EQ05LowGain;
        [Offset(0x0041)] IntegraMidFrequencies _EQ05MidFreq;
        [Offset(0x0042)] byte _EQ05MidGain;
        [Offset(0x0043)] IntegraMidQs _EQ05MidQ;
        [Offset(0x0044)] IntegraHighFrequencies _EQ05HighFreq;
        [Offset(0x0045)] byte _EQ05HighGain;

        #endregion

        #region Fields: Compressor/EQ 06

        [Offset(0x0046)] IntegraSwitch _Comp06Switch;
        [Offset(0x0047)] byte _Comp06AttackTime;
        [Offset(0x0048)] byte _Comp06ReleaseTime;
        [Offset(0x0049)] byte _Comp06Threshold;
        [Offset(0x004A)] byte _Comp06Ratio;
        [Offset(0x004B)] byte _Comp06OutputGain;
        [Offset(0x004C)] IntegraSwitch _EQ06Switch;
        [Offset(0x004D)] IntegraLowFrequencies _EQ06LowFreq;
        [Offset(0x004E)] byte _EQ06LowGain;
        [Offset(0x004F)] IntegraMidFrequencies _EQ06MidFreq;
        [Offset(0x0050)] byte _EQ06MidGain;
        [Offset(0x0051)] IntegraMidQs _EQ06MidQ;
        [Offset(0x0052)] IntegraHighFrequencies _EQ06HighFreq;
        [Offset(0x0053)] byte _EQ06HighGain;

        #endregion

        #endregion

        #region Constructor

        public DrumKitCommonCompEQ(IntegraModel drumKit) : base(drumKit.Device)
        {
            Address += drumKit.Address;
        }

        #endregion

        #region Properties: INTEGRA-7

        #region Properties: Compressor/EQ 01

        [Offset(0x0000)]
        public IntegraSwitch Comp01Switch
        {
            get { return _Comp01Switch; }
            set
            {
                _Comp01Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0001)]
        public byte Comp01AttackTime
        {
            get { return _Comp01AttackTime; }
            set
            {
                _Comp01AttackTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0002)]
        public byte Comp01ReleaseTime
        {
            get { return _Comp01ReleaseTime; }
            set
            {
                _Comp01ReleaseTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0003)]
        public byte Comp01Threshold
        {
            get { return _Comp01Threshold; }
            set
            {
                _Comp01Threshold = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0004)]
        public byte Comp01Ratio
        {
            get { return _Comp01Ratio; }
            set
            {
                _Comp01Ratio = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0005)]
        public byte Comp01OutputGain
        {
            get { return _Comp01OutputGain; }
            set
            {
                _Comp01OutputGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public IntegraSwitch EQ01Switch
        {
            get { return _EQ01Switch; }
            set
            {
                _EQ01Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0007)]
        public IntegraLowFrequencies EQ01LowFreq
        {
            get { return _EQ01LowFreq; }
            set
            {
                _EQ01LowFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0008)]
        public byte EQ01LowGain
        {
            get { return _EQ01LowGain; }
            set
            {
                _EQ01LowGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0009)]
        public IntegraMidFrequencies EQ01MidFreq
        {
            get { return _EQ01MidFreq; }
            set
            {
                _EQ01MidFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000A)]
        public byte EQ01MidGain
        {
            get { return _EQ01MidGain; }
            set
            {
                _EQ01MidGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000B)]
        public IntegraMidQs EQ01MidQ
        {
            get { return _EQ01MidQ; }
            set
            {
                _EQ01MidQ = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000C)]
        public IntegraHighFrequencies EQ01HighFreq
        {
            get { return _EQ01HighFreq; }
            set
            {
                _EQ01HighFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000D)]
        public byte EQ01HighGain
        {
            get { return _EQ01HighGain; }
            set
            {
                _EQ01HighGain = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties: Compressor/EQ 02

        [Offset(0x000E)]
        public IntegraSwitch Comp02Switch
        {
            get { return _Comp02Switch; }
            set
            {
                _Comp02Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000F)]
        public byte Comp02AttackTime
        {
            get { return _Comp02AttackTime; }
            set
            {
                _Comp02AttackTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0010)]
        public byte Comp02ReleaseTime
        {
            get { return _Comp02ReleaseTime; }
            set
            {
                _Comp02ReleaseTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0011)]
        public byte Comp02Threshold
        {
            get { return _Comp02Threshold; }
            set
            {
                _Comp02Threshold = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0012)]
        public byte Comp02Ratio
        {
            get { return _Comp02Ratio; }
            set
            {
                _Comp02Ratio = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0013)]
        public byte Comp02OutputGain
        {
            get { return _Comp02OutputGain; }
            set
            {
                _Comp02OutputGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0014)]
        public IntegraSwitch EQ02Switch
        {
            get { return _EQ02Switch; }
            set
            {
                _EQ02Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0015)]
        public IntegraLowFrequencies EQ02LowFreq
        {
            get { return _EQ02LowFreq; }
            set
            {
                _EQ02LowFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0016)]
        public byte EQ02LowGain
        {
            get { return _EQ02LowGain; }
            set
            {
                _EQ02LowGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0017)]
        public IntegraMidFrequencies EQ02MidFreq
        {
            get { return _EQ02MidFreq; }
            set
            {
                _EQ02MidFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0018)]
        public byte EQ02MidGain
        {
            get { return _EQ02MidGain; }
            set
            {
                _EQ02MidGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0019)]
        public IntegraMidQs EQ02MidQ
        {
            get { return _EQ02MidQ; }
            set
            {
                _EQ02MidQ = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001A)]
        public IntegraHighFrequencies EQ02HighFreq
        {
            get { return _EQ02HighFreq; }
            set
            {
                _EQ02HighFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001B)]
        public byte EQ02HighGain
        {
            get { return _EQ02HighGain; }
            set
            {
                _EQ02HighGain = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties: Compressor/EQ 03

        [Offset(0x001C)]
        public IntegraSwitch Comp03Switch
        {
            get { return _Comp03Switch; }
            set
            {
                _Comp03Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001D)]
        public byte Comp03AttackTime
        {
            get { return _Comp03AttackTime; }
            set
            {
                _Comp03AttackTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001E)]
        public byte Comp03ReleaseTime
        {
            get { return _Comp03ReleaseTime; }
            set
            {
                _Comp03ReleaseTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001F)]
        public byte Comp03Threshold
        {
            get { return _Comp03Threshold; }
            set
            {
                _Comp03Threshold = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0020)]
        public byte Comp03Ratio
        {
            get { return _Comp03Ratio; }
            set
            {
                _Comp03Ratio = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0021)]
        public byte Comp03OutputGain
        {
            get { return _Comp03OutputGain; }
            set
            {
                _Comp03OutputGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0022)]
        public IntegraSwitch EQ03Switch
        {
            get { return _EQ03Switch; }
            set
            {
                _EQ03Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0023)]
        public IntegraLowFrequencies EQ03LowFreq
        {
            get { return _EQ03LowFreq; }
            set
            {
                _EQ03LowFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0024)]
        public byte EQ03LowGain
        {
            get { return _EQ03LowGain; }
            set
            {
                _EQ03LowGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0025)]
        public IntegraMidFrequencies EQ03MidFreq
        {
            get { return _EQ03MidFreq; }
            set
            {
                _EQ03MidFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0026)]
        public byte EQ03MidGain
        {
            get { return _EQ03MidGain; }
            set
            {
                _EQ03MidGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0027)]
        public IntegraMidQs EQ03MidQ
        {
            get { return _EQ03MidQ; }
            set
            {
                _EQ03MidQ = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0028)]
        public IntegraHighFrequencies EQ03HighFreq
        {
            get { return _EQ03HighFreq; }
            set
            {
                _EQ03HighFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0029)]
        public byte EQ03HighGain
        {
            get { return _EQ03HighGain; }
            set
            {
                _EQ03HighGain = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties: Compressor/EQ 04

        [Offset(0x002A)]
        public IntegraSwitch Comp04Switch
        {
            get { return _Comp04Switch; }
            set
            {
                _Comp04Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002B)]
        public byte Comp04AttackTime
        {
            get { return _Comp04AttackTime; }
            set
            {
                _Comp04AttackTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002C)]
        public byte Comp04ReleaseTime
        {
            get { return _Comp04ReleaseTime; }
            set
            {
                _Comp04ReleaseTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002D)]
        public byte Comp04Threshold
        {
            get { return _Comp04Threshold; }
            set
            {
                _Comp04Threshold = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002E)]
        public byte Comp04Ratio
        {
            get { return _Comp04Ratio; }
            set
            {
                _Comp04Ratio = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x002F)]
        public byte Comp04OutputGain
        {
            get { return _Comp04OutputGain; }
            set
            {
                _Comp04OutputGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0030)]
        public IntegraSwitch EQ04Switch
        {
            get { return _EQ04Switch; }
            set
            {
                _EQ04Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0031)]
        public IntegraLowFrequencies EQ04LowFreq
        {
            get { return _EQ04LowFreq; }
            set
            {
                _EQ04LowFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0032)]
        public byte EQ04LowGain
        {
            get { return _EQ04LowGain; }
            set
            {
                _EQ04LowGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0033)]
        public IntegraMidFrequencies EQ04MidFreq
        {
            get { return _EQ04MidFreq; }
            set
            {
                _EQ04MidFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0034)]
        public byte EQ04MidGain
        {
            get { return _EQ04MidGain; }
            set
            {
                _EQ04MidGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0035)]
        public IntegraMidQs EQ04MidQ
        {
            get { return _EQ04MidQ; }
            set
            {
                _EQ04MidQ = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0036)]
        public IntegraHighFrequencies EQ04HighFreq
        {
            get { return _EQ04HighFreq; }
            set
            {
                _EQ04HighFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0037)]
        public byte EQ04HighGain
        {
            get { return _EQ04HighGain; }
            set
            {
                _EQ04HighGain = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties: Compressor/EQ 05

        [Offset(0x0038)]
        public IntegraSwitch Comp05Switch
        {
            get { return _Comp05Switch; }
            set
            {
                _Comp05Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0039)]
        public byte Comp05AttackTime
        {
            get { return _Comp05AttackTime; }
            set
            {
                _Comp05AttackTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003A)]
        public byte Comp05ReleaseTime
        {
            get { return _Comp05ReleaseTime; }
            set
            {
                _Comp05ReleaseTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003B)]
        public byte Comp05Threshold
        {
            get { return _Comp05Threshold; }
            set
            {
                _Comp05Threshold = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003C)]
        public byte Comp05Ratio
        {
            get { return _Comp05Ratio; }
            set
            {
                _Comp05Ratio = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003D)]
        public byte Comp05OutputGain
        {
            get { return _Comp05OutputGain; }
            set
            {
                _Comp05OutputGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003E)]
        public IntegraSwitch EQ05Switch
        {
            get { return _EQ05Switch; }
            set
            {
                _EQ05Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x003F)]
        public IntegraLowFrequencies EQ05LowFreq
        {
            get { return _EQ05LowFreq; }
            set
            {
                _EQ05LowFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0040)]
        public byte EQ05LowGain
        {
            get { return _EQ05LowGain; }
            set
            {
                _EQ05LowGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0041)]
        public IntegraMidFrequencies EQ05MidFreq
        {
            get { return _EQ05MidFreq; }
            set
            {
                _EQ05MidFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0042)]
        public byte EQ05MidGain
        {
            get { return _EQ05MidGain; }
            set
            {
                _EQ05MidGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0043)]
        public IntegraMidQs EQ05MidQ
        {
            get { return _EQ05MidQ; }
            set
            {
                _EQ05MidQ = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0044)]
        public IntegraHighFrequencies EQ05HighFreq
        {
            get { return _EQ05HighFreq; }
            set
            {
                _EQ05HighFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0045)]
        public byte EQ05HighGain
        {
            get { return _EQ05HighGain; }
            set
            {
                _EQ05HighGain = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties: Compressor/EQ 06

        [Offset(0x0046)]
        public IntegraSwitch Comp06Switch
        {
            get { return _Comp06Switch; }
            set
            {
                _Comp06Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0047)]
        public byte Comp06AttackTime
        {
            get { return _Comp06AttackTime; }
            set
            {
                _Comp06AttackTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0048)]
        public byte Comp06ReleaseTime
        {
            get { return _Comp06ReleaseTime; }
            set
            {
                _Comp06ReleaseTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0049)]
        public byte Comp06Threshold
        {
            get { return _Comp06Threshold; }
            set
            {
                _Comp06Threshold = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004A)]
        public byte Comp06Ratio
        {
            get { return _Comp06Ratio; }
            set
            {
                _Comp06Ratio = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004B)]
        public byte Comp06OutputGain
        {
            get { return _Comp06OutputGain; }
            set
            {
                _Comp06OutputGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004C)]
        public IntegraSwitch EQ06Switch
        {
            get { return _EQ06Switch; }
            set
            {
                _EQ06Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004D)]
        public IntegraLowFrequencies EQ06LowFreq
        {
            get { return _EQ06LowFreq; }
            set
            {
                _EQ06LowFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004E)]
        public byte EQ06LowGain
        {
            get { return _EQ06LowGain; }
            set
            {
                _EQ06LowGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x004F)]
        public IntegraMidFrequencies EQ06MidFreq
        {
            get { return _EQ06MidFreq; }
            set
            {
                _EQ06MidFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0050)]
        public byte EQ06MidGain
        {
            get { return _EQ06MidGain; }
            set
            {
                _EQ06MidGain = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0051)]
        public IntegraMidQs EQ06MidQ
        {
            get { return _EQ06MidQ; }
            set
            {
                _EQ06MidQ = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0052)]
        public IntegraHighFrequencies EQ06HighFreq
        {
            get { return _EQ06HighFreq; }
            set
            {
                _EQ06HighFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0053)]
        public byte EQ06HighGain
        {
            get { return _EQ06HighGain; }
            set
            {
                _EQ06HighGain = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #endregion
    }
}
