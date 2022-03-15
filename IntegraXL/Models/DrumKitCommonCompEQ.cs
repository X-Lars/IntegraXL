using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks><i>Shared by the <see cref="SuperNATURALDrumKit"/> and <see cref="PCMDrumKit"/> models.</i></remarks>
    [Integra(0x00000800, 0x00000054)]
    public class DrumKitCommonCompEQ : IntegraModel<DrumKitCommonCompEQ>
    {
        #region Fields: INTEGRA-7

        #region Fields: Compressor/EQ 01

        [Offset(0x0000)] IntegraSwitch _Comp1Switch;
        [Offset(0x0001)] IntegraCompAttackTime _Comp1AttackTime;
        [Offset(0x0002)] IntegraCompReleaseTime _Comp1ReleaseTime;
        [Offset(0x0003)] byte _Comp1Threshold;
        [Offset(0x0004)] IntegraCompRatio _Comp1Ratio;
        [Offset(0x0005)] byte _Comp1OutputGain;
        [Offset(0x0006)] IntegraSwitch _EQ1Switch;
        [Offset(0x0007)] IntegraLowFrequencies _EQ1LowFreq;
        [Offset(0x0008)] byte _EQ1LowGain;
        [Offset(0x0009)] IntegraMidFrequencies _EQ1MidFreq;
        [Offset(0x000A)] byte _EQ1MidGain;
        [Offset(0x000B)] IntegraMidQs _EQ1MidQ;
        [Offset(0x000C)] IntegraHighFrequencies _EQ1HighFreq;
        [Offset(0x000D)] byte _EQ1HighGain;

        #endregion

        #region Fields: Compressor/EQ 02

        [Offset(0x000E)] IntegraSwitch _Comp2Switch;
        [Offset(0x000F)] IntegraCompAttackTime _Comp2AttackTime;
        [Offset(0x0010)] IntegraCompReleaseTime _Comp2ReleaseTime;
        [Offset(0x0011)] byte _Comp2Threshold;
        [Offset(0x0012)] IntegraCompRatio _Comp2Ratio;
        [Offset(0x0013)] byte _Comp2OutputGain;
        [Offset(0x0014)] IntegraSwitch _EQ2Switch;
        [Offset(0x0015)] IntegraLowFrequencies _EQ2LowFreq;
        [Offset(0x0016)] byte _EQ2LowGain;
        [Offset(0x0017)] IntegraMidFrequencies _EQ2MidFreq;
        [Offset(0x0018)] byte _EQ2MidGain;
        [Offset(0x0019)] IntegraMidQs _EQ2MidQ;
        [Offset(0x001A)] IntegraHighFrequencies _EQ2HighFreq;
        [Offset(0x001B)] byte _EQ2HighGain;

        #endregion

        #region Fields: Compressor/EQ 03

        [Offset(0x001C)] IntegraSwitch _Comp3Switch;
        [Offset(0x001D)] IntegraCompAttackTime _Comp3AttackTime;
        [Offset(0x001E)] IntegraCompReleaseTime _Comp3ReleaseTime;
        [Offset(0x001F)] byte _Comp3Threshold;
        [Offset(0x0020)] IntegraCompRatio _Comp3Ratio;
        [Offset(0x0021)] byte _Comp3OutputGain;
        [Offset(0x0022)] IntegraSwitch _EQ3Switch;
        [Offset(0x0023)] IntegraLowFrequencies _EQ3LowFreq;
        [Offset(0x0024)] byte _EQ3LowGain;
        [Offset(0x0025)] IntegraMidFrequencies _EQ3MidFreq;
        [Offset(0x0026)] byte _EQ3MidGain;
        [Offset(0x0027)] IntegraMidQs _EQ3MidQ;
        [Offset(0x0028)] IntegraHighFrequencies _EQ3HighFreq;
        [Offset(0x0029)] byte _EQ3HighGain;

        #endregion

        #region Fields: Compressor/EQ 04

        [Offset(0x002A)] IntegraSwitch _Comp4Switch;
        [Offset(0x002B)] IntegraCompAttackTime _Comp4AttackTime;
        [Offset(0x002C)] IntegraCompReleaseTime _Comp4ReleaseTime;
        [Offset(0x002D)] byte _Comp4Threshold;
        [Offset(0x002E)] IntegraCompRatio _Comp4Ratio;
        [Offset(0x002F)] byte _Comp4OutputGain;
        [Offset(0x0030)] IntegraSwitch _EQ4Switch;
        [Offset(0x0031)] IntegraLowFrequencies _EQ4LowFreq;
        [Offset(0x0032)] byte _EQ4LowGain;
        [Offset(0x0033)] IntegraMidFrequencies _EQ4MidFreq;
        [Offset(0x0034)] byte _EQ4MidGain;
        [Offset(0x0035)] IntegraMidQs _EQ4MidQ;
        [Offset(0x0036)] IntegraHighFrequencies _EQ4HighFreq;
        [Offset(0x0037)] byte _EQ4HighGain;

        #endregion

        #region Fields: Compressor/EQ 05

        [Offset(0x0038)] IntegraSwitch _Comp5Switch;
        [Offset(0x0039)] IntegraCompAttackTime _Comp5AttackTime;
        [Offset(0x003A)] IntegraCompReleaseTime _Comp5ReleaseTime;
        [Offset(0x003B)] byte _Comp5Threshold;
        [Offset(0x003C)] IntegraCompRatio _Comp5Ratio;
        [Offset(0x003D)] byte _Comp5OutputGain;
        [Offset(0x003E)] IntegraSwitch _EQ5Switch;
        [Offset(0x003F)] IntegraLowFrequencies _EQ5LowFreq;
        [Offset(0x0040)] byte _EQ5LowGain;
        [Offset(0x0041)] IntegraMidFrequencies _EQ5MidFreq;
        [Offset(0x0042)] byte _EQ5MidGain;
        [Offset(0x0043)] IntegraMidQs _EQ5MidQ;
        [Offset(0x0044)] IntegraHighFrequencies _EQ5HighFreq;
        [Offset(0x0045)] byte _EQ5HighGain;

        #endregion

        #region Fields: Compressor/EQ 06

        [Offset(0x0046)] IntegraSwitch _Comp6Switch;
        [Offset(0x0047)] IntegraCompAttackTime _Comp6AttackTime;
        [Offset(0x0048)] IntegraCompReleaseTime _Comp6ReleaseTime;
        [Offset(0x0049)] byte _Comp6Threshold;
        [Offset(0x004A)] IntegraCompRatio _Comp6Ratio;
        [Offset(0x004B)] byte _Comp6OutputGain;
        [Offset(0x004C)] IntegraSwitch _EQ6Switch;
        [Offset(0x004D)] IntegraLowFrequencies _EQ6LowFreq;
        [Offset(0x004E)] byte _EQ6LowGain;
        [Offset(0x004F)] IntegraMidFrequencies _EQ6MidFreq;
        [Offset(0x0050)] byte _EQ6MidGain;
        [Offset(0x0051)] IntegraMidQs _EQ6MidQ;
        [Offset(0x0052)] IntegraHighFrequencies _EQ6HighFreq;
        [Offset(0x0053)] byte _EQ6HighGain;

        #endregion

        #endregion

        #region Constructor

        internal DrumKitCommonCompEQ(IntegraModel drumKit) : base(drumKit.Device)
        {
            Address += drumKit.Address;
        }

        #endregion

        #region Properties: INTEGRA-7

        #region Properties: Compressor/EQ 01

        [Offset(0x0000)]
        public IntegraSwitch Comp1Switch
        {
            get => _Comp1Switch;
            set
            {
                if (_Comp1Switch != value)
                {
                    _Comp1Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0001)]
        public IntegraCompAttackTime Comp1AttackTime
        {
            get => _Comp1AttackTime;
            set
            {
                if (_Comp1AttackTime != value)
                {
                    _Comp1AttackTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0002)]
        public IntegraCompReleaseTime Comp1ReleaseTime
        {
            get => _Comp1ReleaseTime; set
            {
                if (_Comp1ReleaseTime != value)
                {
                    _Comp1ReleaseTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0003)]
        public byte Comp1Threshold
        {
            get => _Comp1Threshold;
            set
            {
                if (_Comp1Threshold != value)
                {
                    _Comp1Threshold = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0004)]
        public IntegraCompRatio Comp1Ratio
        {
            get => _Comp1Ratio; 
            set
            {
                if (_Comp1Ratio != value)
                {
                    _Comp1Ratio = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0005)]
        public byte Comp1OutputGain
        {
            get => _Comp1OutputGain;
            set
            {
                if (_Comp1OutputGain != value)
                {
                    _Comp1OutputGain = value.Clamp(0, 24);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public IntegraSwitch EQ1Switch
        {
            get => _EQ1Switch; 
            set
            {
                if (_EQ1Switch != value)
                {
                    _EQ1Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0007)]
        public IntegraLowFrequencies EQ1LowFreq
        {
            get => _EQ1LowFreq; 
            set
            {
                if (_EQ1LowFreq != value)
                {
                    _EQ1LowFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0008)]
        public int EQ1LowGain
        {
            get => _EQ1LowGain.Deserialize(15); 
            set
            {
                if (_EQ1LowGain != value)
                {
                    _EQ1LowGain = value.Clamp(-15, 15).Serialize(15);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0009)]
        public IntegraMidFrequencies EQ1MidFreq
        {
            get => _EQ1MidFreq; 
            set
            {
                if (_EQ1MidFreq != value)
                {
                    _EQ1MidFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000A)]
        public int EQ1MidGain
        {
            get => _EQ1MidGain.Deserialize(15); 
            set
            {
                if (_EQ1MidGain != value)
                {
                    _EQ1MidGain = value.Clamp(-15, 15).Serialize(15);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000B)]
        public IntegraMidQs EQ1MidQ
        {
            get => _EQ1MidQ; 
            set
            {
                if (_EQ1MidQ != value)
                {
                    _EQ1MidQ = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public IntegraHighFrequencies EQ1HighFreq
        {
            get => _EQ1HighFreq; 
            set
            {
                if (_EQ1HighFreq != value)
                {
                    _EQ1HighFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000D)]
        public int EQ1HighGain
        {
            get => _EQ1HighGain.Deserialize(15); 
            set
            {
                if (_EQ1HighGain != value)
                {
                    _EQ1HighGain = value.Clamp(-15, 15).Serialize(15);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: Compressor/EQ 02

        [Offset(0x000E)]
        public IntegraSwitch Comp2Switch
        {
            get => _Comp2Switch; set
            {
                if (_Comp2Switch != value)
                {
                    _Comp2Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000F)]
        public IntegraCompAttackTime Comp2AttackTime
        {
            get => _Comp2AttackTime; set
            {
                if (_Comp2AttackTime != value)
                {
                    _Comp2AttackTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0010)]
        public IntegraCompReleaseTime Comp2ReleaseTime
        {
            get => _Comp2ReleaseTime; set
            {
                if (_Comp2ReleaseTime != value)
                {
                    _Comp2ReleaseTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0011)]
        public byte Comp2Threshold
        {
            get => _Comp2Threshold; set
            {
                if (_Comp2Threshold != value)
                {
                    _Comp2Threshold = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0012)]
        public IntegraCompRatio Comp2Ratio
        {
            get => _Comp2Ratio; set
            {
                if (_Comp2Ratio != value)
                {
                    _Comp2Ratio = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0013)]
        public byte Comp2OutputGain
        {
            get => _Comp2OutputGain; set
            {
                if (_Comp2OutputGain != value)
                {
                    _Comp2OutputGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0014)]
        public IntegraSwitch EQ2Switch
        {
            get => _EQ2Switch; set
            {
                if (_EQ2Switch != value)
                {
                    _EQ2Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0015)]
        public IntegraLowFrequencies EQ2LowFreq
        {
            get => _EQ2LowFreq; set
            {
                if (_EQ2LowFreq != value)
                {
                    _EQ2LowFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0016)]
        public byte EQ2LowGain
        {
            get => _EQ2LowGain; set
            {
                if (_EQ2LowGain != value)
                {
                    _EQ2LowGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0017)]
        public IntegraMidFrequencies EQ2MidFreq
        {
            get => _EQ2MidFreq; set
            {
                if (_EQ2MidFreq != value)
                {
                    _EQ2MidFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0018)]
        public byte EQ2MidGain
        {
            get => _EQ2MidGain; set
            {
                if (_EQ2MidGain != value)
                {
                    _EQ2MidGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0019)]
        public IntegraMidQs EQ2MidQ
        {
            get => _EQ2MidQ; set
            {
                if (_EQ2MidQ != value)
                {
                    _EQ2MidQ = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001A)]
        public IntegraHighFrequencies EQ2HighFreq
        {
            get => _EQ2HighFreq; set
            {
                if (_EQ2HighFreq != value)
                {
                    _EQ2HighFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001B)]
        public byte EQ2HighGain
        {
            get => _EQ2HighGain; set
            {
                if (_EQ2HighGain != value)
                {
                    _EQ2HighGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: Compressor/EQ 03

        [Offset(0x001C)]
        public IntegraSwitch Comp3Switch
        {
            get => _Comp3Switch; set
            {
                if (_Comp3Switch != value)
                {
                    _Comp3Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001D)]
        public IntegraCompAttackTime Comp3AttackTime
        {
            get => _Comp3AttackTime; set
            {
                if (_Comp3AttackTime != value)
                {
                    _Comp3AttackTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001E)]
        public IntegraCompReleaseTime Comp3ReleaseTime
        {
            get => _Comp3ReleaseTime; set
            {
                if (_Comp3ReleaseTime != value)
                {
                    _Comp3ReleaseTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001F)]
        public byte Comp3Threshold
        {
            get => _Comp3Threshold; set
            {
                if (_Comp3Threshold != value)
                {
                    _Comp3Threshold = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0020)]
        public IntegraCompRatio Comp3Ratio
        {
            get => _Comp3Ratio; set
            {
                if (_Comp3Ratio != value)
                {
                    _Comp3Ratio = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0021)]
        public byte Comp3OutputGain
        {
            get => _Comp3OutputGain; set
            {
                if (_Comp3OutputGain != value)
                {
                    _Comp3OutputGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0022)]
        public IntegraSwitch EQ3Switch
        {
            get => _EQ3Switch; set
            {
                if (_EQ3Switch != value)
                {
                    _EQ3Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0023)]
        public IntegraLowFrequencies EQ3LowFreq
        {
            get => _EQ3LowFreq; set
            {
                if (_EQ3LowFreq != value)
                {
                    _EQ3LowFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0024)]
        public byte EQ3LowGain
        {
            get => _EQ3LowGain; set
            {
                if (_EQ3LowGain != value)
                {
                    _EQ3LowGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0025)]
        public IntegraMidFrequencies EQ3MidFreq
        {
            get => _EQ3MidFreq; set
            {
                if (_EQ3MidFreq != value)
                {
                    _EQ3MidFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0026)]
        public byte EQ3MidGain
        {
            get => _EQ3MidGain; set
            {
                if (_EQ3MidGain != value)
                {
                    _EQ3MidGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0027)]
        public IntegraMidQs EQ3MidQ
        {
            get => _EQ3MidQ; set
            {
                if (_EQ3MidQ != value)
                {
                    _EQ3MidQ = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0028)]
        public IntegraHighFrequencies EQ3HighFreq
        {
            get => _EQ3HighFreq; set
            {
                if (_EQ3HighFreq != value)
                {
                    _EQ3HighFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0029)]
        public byte EQ3HighGain
        {
            get => _EQ3HighGain; set
            {
                if (_EQ3HighGain != value)
                {
                    _EQ3HighGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: Compressor/EQ 04

        [Offset(0x002A)]
        public IntegraSwitch Comp4Switch
        {
            get => _Comp4Switch; set
            {
                if (_Comp4Switch != value)
                {
                    _Comp4Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002B)]
        public IntegraCompAttackTime Comp4AttackTime
        {
            get => _Comp4AttackTime; set
            {
                if (_Comp4AttackTime != value)
                {
                    _Comp4AttackTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002C)]
        public IntegraCompReleaseTime Comp4ReleaseTime
        {
            get => _Comp4ReleaseTime; set
            {
                if (_Comp4ReleaseTime != value)
                {
                    _Comp4ReleaseTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002D)]
        public byte Comp4Threshold
        {
            get => _Comp4Threshold; set
            {
                if (_Comp4Threshold != value)
                {
                    _Comp4Threshold = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002E)]
        public IntegraCompRatio Comp4Ratio
        {
            get => _Comp4Ratio; set
            {
                if (_Comp4Ratio != value)
                {
                    _Comp4Ratio = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x002F)]
        public byte Comp4OutputGain
        {
            get => _Comp4OutputGain; set
            {
                if (_Comp4OutputGain != value)
                {
                    _Comp4OutputGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0030)]
        public IntegraSwitch EQ4Switch
        {
            get => _EQ4Switch; set
            {
                if (_EQ4Switch != value)
                {
                    _EQ4Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0031)]
        public IntegraLowFrequencies EQ4LowFreq
        {
            get => _EQ4LowFreq; set
            {
                if (_EQ4LowFreq != value)
                {
                    _EQ4LowFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0032)]
        public byte EQ4LowGain
        {
            get => _EQ4LowGain; set
            {
                if (_EQ4LowGain != value)
                {
                    _EQ4LowGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0033)]
        public IntegraMidFrequencies EQ4MidFreq
        {
            get => _EQ4MidFreq; set
            {
                if (_EQ4MidFreq != value)
                {
                    _EQ4MidFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0034)]
        public byte EQ4MidGain
        {
            get => _EQ4MidGain; set
            {
                if (_EQ4MidGain != value)
                {
                    _EQ4MidGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0035)]
        public IntegraMidQs EQ4MidQ
        {
            get => _EQ4MidQ; set
            {
                if (_EQ4MidQ != value)
                {
                    _EQ4MidQ = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0036)]
        public IntegraHighFrequencies EQ4HighFreq
        {
            get => _EQ4HighFreq; set
            {
                if (_EQ4HighFreq != value)
                {
                    _EQ4HighFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0037)]
        public byte EQ4HighGain
        {
            get => _EQ4HighGain; set
            {
                if (_EQ4HighGain != value)
                {
                    _EQ4HighGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: Compressor/EQ 05

        [Offset(0x0038)]
        public IntegraSwitch Comp5Switch
        {
            get => _Comp5Switch; set
            {
                if (_Comp5Switch != value)
                {
                    _Comp5Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0039)]
        public IntegraCompAttackTime Comp5AttackTime
        {
            get => _Comp5AttackTime; set
            {
                if (_Comp5AttackTime != value)
                {
                    _Comp5AttackTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003A)]
        public IntegraCompReleaseTime Comp5ReleaseTime
        {
            get => _Comp5ReleaseTime; set
            {
                if (_Comp5ReleaseTime != value)
                {
                    _Comp5ReleaseTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003B)]
        public byte Comp5Threshold
        {
            get => _Comp5Threshold; set
            {
                if (_Comp5Threshold != value)
                {
                    _Comp5Threshold = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003C)]
        public IntegraCompRatio Comp5Ratio
        {
            get => _Comp5Ratio; set
            {
                if (_Comp5Ratio != value)
                {
                    _Comp5Ratio = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003D)]
        public byte Comp5OutputGain
        {
            get => _Comp5OutputGain; set
            {
                if (_Comp5OutputGain != value)
                {
                    _Comp5OutputGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003E)]
        public IntegraSwitch EQ5Switch
        {
            get => _EQ5Switch; set
            {
                if (_EQ5Switch != value)
                {
                    _EQ5Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x003F)]
        public IntegraLowFrequencies EQ5LowFreq
        {
            get => _EQ5LowFreq; set
            {
                if (_EQ5LowFreq != value)
                {
                    _EQ5LowFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0040)]
        public byte EQ5LowGain
        {
            get => _EQ5LowGain; set
            {
                if (_EQ5LowGain != value)
                {
                    _EQ5LowGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0041)]
        public IntegraMidFrequencies EQ5MidFreq
        {
            get => _EQ5MidFreq; set
            {
                if (_EQ5MidFreq != value)
                {
                    _EQ5MidFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0042)]
        public byte EQ5MidGain
        {
            get => _EQ5MidGain; set
            {
                if (_EQ5MidGain != value)
                {
                    _EQ5MidGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0043)]
        public IntegraMidQs EQ5MidQ
        {
            get => _EQ5MidQ; set
            {
                if (_EQ5MidQ != value)
                {
                    _EQ5MidQ = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0044)]
        public IntegraHighFrequencies EQ5HighFreq
        {
            get => _EQ5HighFreq; set
            {
                if (_EQ5HighFreq != value)
                {
                    _EQ5HighFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0045)]
        public byte EQ5HighGain
        {
            get => _EQ5HighGain; set
            {
                if (_EQ5HighGain != value)
                {
                    _EQ5HighGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: Compressor/EQ 06

        [Offset(0x0046)]
        public IntegraSwitch Comp6Switch
        {
            get => _Comp6Switch; set
            {
                if (_Comp6Switch != value)
                {
                    _Comp6Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0047)]
        public IntegraCompAttackTime Comp6AttackTime
        {
            get => _Comp6AttackTime; set
            {
                if (_Comp6AttackTime != value)
                {
                    _Comp6AttackTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0048)]
        public IntegraCompReleaseTime Comp6ReleaseTime
        {
            get => _Comp6ReleaseTime; set
            {
                if (_Comp6ReleaseTime != value)
                {
                    _Comp6ReleaseTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0049)]
        public byte Comp6Threshold
        {
            get => _Comp6Threshold; set
            {
                if (_Comp6Threshold != value)
                {
                    _Comp6Threshold = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004A)]
        public IntegraCompRatio Comp6Ratio
        {
            get => _Comp6Ratio; set
            {
                if (_Comp6Ratio != value)
                {
                    _Comp6Ratio = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004B)]
        public byte Comp6OutputGain
        {
            get => _Comp6OutputGain; set
            {
                if (_Comp6OutputGain != value)
                {
                    _Comp6OutputGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004C)]
        public IntegraSwitch EQ6Switch
        {
            get => _EQ6Switch; set
            {
                if (_EQ6Switch != value)
                {
                    _EQ6Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004D)]
        public IntegraLowFrequencies EQ6LowFreq
        {
            get => _EQ6LowFreq; set
            {
                if (_EQ6LowFreq != value)
                {
                    _EQ6LowFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004E)]
        public byte EQ6LowGain
        {
            get => _EQ6LowGain; set
            {
                if (_EQ6LowGain != value)
                {
                    _EQ6LowGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x004F)]
        public IntegraMidFrequencies EQ6MidFreq
        {
            get => _EQ6MidFreq; set
            {
                if (_EQ6MidFreq != value)
                {
                    _EQ6MidFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0050)]
        public byte EQ6MidGain
        {
            get => _EQ6MidGain; set
            {
                if (_EQ6MidGain != value)
                {
                    _EQ6MidGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0051)]
        public IntegraMidQs EQ6MidQ
        {
            get => _EQ6MidQ; set
            {
                if (_EQ6MidQ != value)
                {
                    _EQ6MidQ = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0052)]
        public IntegraHighFrequencies EQ6HighFreq
        {
            get => _EQ6HighFreq; set
            {
                if (_EQ6HighFreq != value)
                {
                    _EQ6HighFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0053)]
        public byte EQ6HighGain
        {
            get => _EQ6HighGain; set
            {
                if (_EQ6HighGain != value)
                {
                    _EQ6HighGain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #endregion
    }
}
