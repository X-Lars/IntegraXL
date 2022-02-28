using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models
{
    [Integra(0x00001000, 0x00000029)]
    public class PCMSynthTonePMT : IntegraModel<PCMSynthTonePMT>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] IntegraStructureType _StructureType12;
        [Offset(0x0001)] byte _Booster12;
        [Offset(0x0002)] IntegraStructureType _Structuretype34;
        [Offset(0x0003)] byte _Booster34;
        [Offset(0x0004)] IntegraVelocityControl _VelocityControl;
        [Offset(0x0005)] IntegraSwitch _PMT1Switch;
        [Offset(0x0006)] IntegraKeyRange _PMT1KeyboardRangeLower;
        [Offset(0x0007)] IntegraKeyRange _PMT1KeyboardRangeUpper;
        [Offset(0x0008)] byte _PMT1KeyboardFadeWidthLower;
        [Offset(0x0009)] byte _PMT1KeyboardFadeWidthUpper;
        [Offset(0x000A)] byte _PMT1VelocityRangeLower;
        [Offset(0x000B)] byte _PMT1VelocityRangeUpper;
        [Offset(0x000C)] byte _PMT1VelocityFadeWidthLower;
        [Offset(0x000D)] byte _PMT1VelocityFadeWidthUpper;
        [Offset(0x000E)] IntegraSwitch _PMT2Switch;
        [Offset(0x000F)] IntegraKeyRange _PMT2KeyboardRangeLower;
        [Offset(0x0010)] IntegraKeyRange _PMT2KeyboardRangeUpper;
        [Offset(0x0011)] byte _PMT2KeyboardFadeWidthLower;
        [Offset(0x0012)] byte _PMT2KeyboardFadeWidthUpper;
        [Offset(0x0013)] byte _PMT2VelocityRangeLower;
        [Offset(0x0014)] byte _PMT2VelocityRangeUpper;
        [Offset(0x0015)] byte _PMT2VelocityFadeWidthLower;
        [Offset(0x0016)] byte _PMT2VelocityFadeWidthUpper;
        [Offset(0x0017)] IntegraSwitch _PMT3Switch;
        [Offset(0x0018)] IntegraKeyRange _PMT3KeyboardRangeLower;
        [Offset(0x0019)] IntegraKeyRange _PMT3KeyboardRangeUpper;
        [Offset(0x001A)] byte _PMT3KeyboardFadeWidthLower;
        [Offset(0x001B)] byte _PMT3KeyboardFadeWidthUpper;
        [Offset(0x001C)] byte _PMT3VelocityRangeLower;
        [Offset(0x001D)] byte _PMT3VelocityRangeUpper;
        [Offset(0x001E)] byte _PMT3VelocityFadeWidthLower;
        [Offset(0x001F)] byte _PMT3VelocityFadeWidthUpper;
        [Offset(0x0020)] IntegraSwitch _PMT4Switch;
        [Offset(0x0021)] IntegraKeyRange _PMT4KeyboardRangeLower;
        [Offset(0x0022)] IntegraKeyRange _PMT4KeyboardRangeUpper;
        [Offset(0x0023)] byte _PMT4KeyboardFadeWidthLower;
        [Offset(0x0024)] byte _PMT4KeyboardFadeWidthUpper;
        [Offset(0x0025)] byte _PMT4VelocityRangeLower;
        [Offset(0x0026)] byte _PMT4VelocityRangeUpper;
        [Offset(0x0027)] byte _PMT4VelocityFadeWidthLower;
        [Offset(0x0028)] byte _PMT4VelocityFadeWidthUpper;

        #endregion

        #region Constructor

        public PCMSynthTonePMT(PCMSynthTone synthTone) : base(synthTone.Device)
        {
            Address += synthTone.Address;
        }

        #endregion

        #region Properties: Common

        [Offset(0x0000)]
        public IntegraStructureType StructureType12
        {
            get => _StructureType12;
            set
            {
                if (_StructureType12 != value)
                {
                    _StructureType12 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0001)]
        public int Booster12
        {
            get => _Booster12.Deserialize(0, 6);
            set
            {
                if (_Booster12 != value)
                {
                    _Booster12 = value.Serialize(0, 6).Clamp(0, 3);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0002)]
        public IntegraStructureType StructureType34
        {
            get => _Structuretype34;
            set
            {
                if (_Structuretype34 != value)
                {
                    _Structuretype34 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0003)]
        public int Booster34
        {
            get => _Booster34.Deserialize(0, 6);
            set
            {
                if (_Booster34 != value)
                {
                    _Booster34 = value.Serialize(0, 6).Clamp(0, 3);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0004)]
        public IntegraVelocityControl VelocityControl
        {
            get => _VelocityControl;
            set
            {
                if (_VelocityControl != value)
                {
                    _VelocityControl = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: PMT 1

        [Offset(0x0005)]
        public IntegraSwitch PMT1Switch
        {
            get => _PMT1Switch;
            set
            {
                if (_PMT1Switch != value)
                {
                    _PMT1Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public IntegraKeyRange PMT1KeyboardRangeLower
        {
            get => _PMT1KeyboardRangeLower;
            set
            {
                if (_PMT1KeyboardRangeLower != value)
                {
                    _PMT1KeyboardRangeLower = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0007)]
        public IntegraKeyRange PMT1KeyboardRangeUpper
        {
            get => _PMT1KeyboardRangeUpper;
            set
            {
                if (_PMT1KeyboardRangeUpper != value)
                {
                    _PMT1KeyboardRangeUpper = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0008)]
        public byte PMT1KeyboardFadeWidthLower
        {
            get => _PMT1KeyboardFadeWidthLower;
            set
            {
                if (_PMT1KeyboardFadeWidthLower != value)
                {
                    _PMT1KeyboardFadeWidthLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0009)]
        public byte PMT1KeyboardFadeWidthUpper
        {
            get => _PMT1KeyboardFadeWidthUpper;
            set
            {
                if (_PMT1KeyboardFadeWidthUpper != value)
                {
                    _PMT1KeyboardFadeWidthUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000A)]
        public byte PMT1VelocityRangeLower
        {
            get => _PMT1VelocityRangeLower;
            set
            {
                if (_PMT1VelocityRangeLower != value)
                {
                    _PMT1VelocityRangeLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000B)]
        public byte PMT1VelocityRangeUpper
        {
            get => _PMT1VelocityRangeUpper;
            set
            {
                if (_PMT1VelocityRangeUpper != value)
                {
                    _PMT1VelocityRangeUpper = value.Clamp(); ;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public byte PMT1VelocityFadeWidthLower
        {
            get => _PMT1VelocityFadeWidthLower;
            set
            {
                if (_PMT1VelocityFadeWidthLower != value)
                {
                    _PMT1VelocityFadeWidthLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000D)]
        public byte PMT1VelocityFadeWidthUpper
        {
            get => _PMT1VelocityFadeWidthUpper;
            set
            {
                if (_PMT1VelocityFadeWidthUpper != value)
                {
                    _PMT1VelocityFadeWidthUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: PMT 2

        [Offset(0x000E)]
        public IntegraSwitch PMT2Switch
        {
            get => _PMT2Switch;
            set
            {
                if (_PMT2Switch != value)
                {
                    _PMT2Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000F)]
        public IntegraKeyRange PMT2KeyboardRangeLower
        {
            get => _PMT2KeyboardRangeLower;
            set
            {
                if (_PMT2KeyboardRangeLower != value)
                {
                    _PMT2KeyboardRangeLower = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0010)]
        public IntegraKeyRange PMT2KeyboardRangeUpper
        {
            get => _PMT2KeyboardRangeUpper;
            set
            {
                if (_PMT2KeyboardRangeUpper != value)
                {
                    _PMT2KeyboardRangeUpper = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0011)]
        public byte PMT2KeyboardFadeWidthLower
        {
            get => _PMT2KeyboardFadeWidthLower;
            set
            {
                if (_PMT2KeyboardFadeWidthLower != value)
                {
                    _PMT2KeyboardFadeWidthLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0012)]
        public byte PMT2KeyboardFadeWidthUpper
        {
            get => _PMT2KeyboardFadeWidthUpper;
            set
            {
                if (_PMT2KeyboardFadeWidthUpper != value)
                {
                    _PMT2KeyboardFadeWidthUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0013)]
        public byte PMT2VelocityRangeLower
        {
            get => _PMT2VelocityRangeLower;
            set
            {
                if (_PMT2VelocityRangeLower != value)
                {
                    _PMT2VelocityRangeLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0014)]
        public byte PMT2VelocityRangeUpper
        {
            get => _PMT2VelocityRangeUpper;
            set
            {
                if (_PMT2VelocityRangeUpper != value)
                {
                    _PMT2VelocityRangeUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0015)]
        public byte PMT2VelocityFadeWidthLower
        {
            get => _PMT2VelocityFadeWidthLower;
            set
            {
                if (_PMT2VelocityFadeWidthLower != value)
                {
                    _PMT2VelocityFadeWidthLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0016)]
        public byte PMT2VelocityFadeWidthUpper
        {
            get => _PMT2VelocityFadeWidthUpper;
            set
            {
                if (_PMT2VelocityFadeWidthUpper != value)
                {
                    _PMT2VelocityFadeWidthUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: PMT 3

        [Offset(0x0017)]
        public IntegraSwitch PMT3Switch
        {
            get => _PMT3Switch;
            set
            {
                if (_PMT3Switch != value)
                {
                    _PMT3Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }
        [Offset(0x0018)]
        public IntegraKeyRange PMT3KeyboardRangeLower
        {
            get => _PMT3KeyboardRangeLower;
            set
            {
                if (_PMT3KeyboardRangeLower != value)
                {
                    _PMT3KeyboardRangeLower = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0019)]
        public IntegraKeyRange PMT3KeyboardRangeUpper
        {
            get => _PMT3KeyboardRangeUpper;
            set
            {
                if (_PMT3KeyboardRangeUpper != value)
                {
                    _PMT3KeyboardRangeUpper = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001A)]
        public byte PMT3KeyboardFadeWidthLower
        {
            get => _PMT3KeyboardFadeWidthLower;
            set
            {
                if (_PMT3KeyboardFadeWidthLower != value)
                {
                    _PMT3KeyboardFadeWidthLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001B)]
        public byte PMT3KeyboardFadeWidthUpper
        {
            get => _PMT3KeyboardFadeWidthUpper;
            set
            {
                if (_PMT3KeyboardFadeWidthUpper != value)
                {
                    _PMT3KeyboardFadeWidthUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001C)]
        public byte PMT3VelocityRangeLower
        {
            get => _PMT3VelocityRangeLower;
            set
            {
                if (_PMT3VelocityRangeLower != value)
                {
                    _PMT3VelocityRangeLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001D)]
        public byte PMT3VelocityRangeUpper
        {
            get => _PMT3VelocityRangeUpper;
            set
            {
                if (_PMT3VelocityRangeUpper != value)
                {
                    _PMT3VelocityRangeUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001E)]
        public byte PMT3VelocityFadeWidthLower
        {
            get => _PMT3VelocityFadeWidthLower;
            set
            {
                if (_PMT3VelocityFadeWidthLower != value)
                {
                    _PMT3VelocityFadeWidthLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001F)]
        public byte PMT3VelocityFadeWidthUpper
        {
            get => _PMT3VelocityFadeWidthUpper;
            set
            {
                if (_PMT3VelocityFadeWidthUpper != value)
                {
                    _PMT3VelocityFadeWidthUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties: PMT 4

        [Offset(0x0020)]
        public IntegraSwitch PMT4Switch
        {
            get => _PMT4Switch;
            set
            {
                if (_PMT4Switch != value)
                {
                    _PMT4Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0021)]
        public IntegraKeyRange PMT4KeyboardRangeLower
        {
            get => _PMT4KeyboardRangeLower;
            set
            {
                if (_PMT4KeyboardRangeLower != value)
                {
                    _PMT4KeyboardRangeLower = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0022)]
        public IntegraKeyRange PMT4KeyboardRangeUpper
        {
            get => _PMT4KeyboardRangeUpper;
            set
            {
                if (_PMT4KeyboardRangeUpper != value)
                {
                    _PMT4KeyboardRangeUpper = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0023)]
        public byte PMT4KeyboardFadeWidthLower
        {
            get => _PMT4KeyboardFadeWidthLower;
            set
            {
                if (_PMT4KeyboardFadeWidthLower != value)
                {
                    _PMT4KeyboardFadeWidthLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0024)]
        public byte PMT4KeyboardFadeWidthUpper
        {
            get => _PMT4KeyboardFadeWidthUpper;
            set
            {
                if (_PMT4KeyboardFadeWidthUpper != value)
                {
                    _PMT4KeyboardFadeWidthUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0025)]
        public byte PMT4VelocityRangeLower
        {
            get => _PMT4VelocityRangeLower;
            set
            {
                if (_PMT4VelocityRangeLower != value)
                {
                    _PMT4VelocityRangeLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0026)]
        public byte PMT4VelocityRangeUpper
        {
            get => _PMT4VelocityRangeUpper;
            set
            {
                if (_PMT4VelocityRangeUpper != value)
                {
                    _PMT4VelocityRangeUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0027)]
        public byte PMT4VelocityFadeWidthLower
        {
            get => _PMT4VelocityFadeWidthLower;
            set
            {
                if (_PMT4VelocityFadeWidthLower != value)
                {
                    _PMT4VelocityFadeWidthLower = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0028)]
        public byte PMT4VelocityFadeWidthUpper
        {
            get => _PMT4VelocityFadeWidthUpper;
            set
            {
                if (_PMT4VelocityFadeWidthUpper != value)
                {
                    _PMT4VelocityFadeWidthUpper = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
    }
}
