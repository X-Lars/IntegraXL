using IntegraXL.Core;
using System.Reflection;

namespace IntegraXL.Models
{
    // TODO: PMT Keyboard / Velocity Range intersection validation
    [Integra(0x00001000, 0x00000029)]
    public class PCMSynthTonePMT : IntegraModel<PCMSynthTonePMT>
    {
        [Offset(0x0000)] byte _StructureType12;
        [Offset(0x0001)] byte _Booster12;
        [Offset(0x0002)] byte _Structuretype34;
        [Offset(0x0003)] byte _Booster34;

        [Offset(0x0004)] IntegraVelocityControl _VelocityControl;

        [Offset(0x0005)] IntegraSwitch _PMT01Switch;
        [Offset(0x0006)] byte _PMT01KeyboardRangeLower;
        [Offset(0x0007)] byte _PMT01KeyboardRangeUpper;
        [Offset(0x0008)] byte _PMT01KeyboardFadeWidthLower;
        [Offset(0x0009)] byte _PMT01KeyboardFadeWidthUpper;
        [Offset(0x000A)] byte _PMT01VelocityRangeLower;
        [Offset(0x000B)] byte _PMT01VelocityRangeUpper;
        [Offset(0x000C)] byte _PMT01VelocityFadeWidthLower;
        [Offset(0x000D)] byte _PMT01VelocityFadeWidthUpper;
        [Offset(0x000E)] IntegraSwitch _PMT02Switch;
        [Offset(0x000F)] byte _PMT02KeyboardRangeLower;
        [Offset(0x0010)] byte _PMT02KeyboardRangeUpper;
        [Offset(0x0011)] byte _PMT02KeyboardFadeWidthLower;
        [Offset(0x0012)] byte _PMT02KeyboardFadeWidthUpper;
        [Offset(0x0013)] byte _PMT02VelocityRangeLower;
        [Offset(0x0014)] byte _PMT02VelocityRangeUpper;
        [Offset(0x0015)] byte _PMT02VelocityFadeWidthLower;
        [Offset(0x0016)] byte _PMT02VelocityFadeWidthUpper;
        [Offset(0x0017)] IntegraSwitch _PMT03Switch;
        [Offset(0x0018)] byte _PMT03KeyboardRangeLower;
        [Offset(0x0019)] byte _PMT03KeyboardRangeUpper;
        [Offset(0x001A)] byte _PMT03KeyboardFadeWidthLower;
        [Offset(0x001B)] byte _PMT03KeyboardFadeWidthUpper;
        [Offset(0x001C)] byte _PMT03VelocityRangeLower;
        [Offset(0x001D)] byte _PMT03VelocityRangeUpper;
        [Offset(0x001E)] byte _PMT03VelocityFadeWidthLower;
        [Offset(0x001F)] byte _PMT03VelocityFadeWidthUpper;
        [Offset(0x0020)] IntegraSwitch _PMT04Switch;
        [Offset(0x0021)] byte _PMT04KeyboardRangeLower;
        [Offset(0x0022)] byte _PMT04KeyboardRangeUpper;
        [Offset(0x0023)] byte _PMT04KeyboardFadeWidthLower;
        [Offset(0x0024)] byte _PMT04KeyboardFadeWidthUpper;
        [Offset(0x0025)] byte _PMT04VelocityRangeLower;
        [Offset(0x0026)] byte _PMT04VelocityRangeUpper;
        [Offset(0x0027)] byte _PMT04VelocityFadeWidthLower;
        [Offset(0x0028)] byte _PMT04VelocityFadeWidthUpper;

        //public PCMSynthTonePMT(IntegraAddress address) : base(address + 0x00001000, 0x00000029)
        public PCMSynthTonePMT(PCMSynthTone synthTone) : base(synthTone.Device)
        {
            Address = synthTone.Address;
            //Address += GetType().GetCustomAttribute<ModelAddress>().Address;
            //Size = GetType().GetCustomAttribute<ModelAddress>().Size;
        }

        [Offset(0x0000)]
        public byte StructureType12
        {
            get { return _StructureType12; }
            set
            {
                _StructureType12 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0001)]
        public byte Booster12
        {
            get { return _Booster12; }
            set
            {
                _Booster12 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0002)]
        public byte StructureType34
        {
            get { return _Structuretype34; }
            set
            {
                _Structuretype34 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0003)]
        public byte Booster34
        {
            get { return _Booster34; }
            set
            {
                _Booster34 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0004)]
        public IntegraVelocityControl VelocityControl
        {
            get { return _VelocityControl; }
            set
            {
                _VelocityControl = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0005)]
        public IntegraSwitch PMT01Switch
        {
            get { return _PMT01Switch; }
            set
            {
                _PMT01Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public byte PMT01KeyboardRangeLower
        {
            get { return _PMT01KeyboardRangeLower; }
            set
            {
                _PMT01KeyboardRangeLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0007)]
        public byte PMT01KeyboardRangeUpper
        {
            get { return _PMT01KeyboardRangeUpper; }
            set
            {
                _PMT01KeyboardRangeUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0008)]
        public byte PMT01KeyboardFadeWidthLower
        {
            get { return _PMT01KeyboardFadeWidthLower; }
            set
            {
                _PMT01KeyboardFadeWidthLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0009)]
        public byte PMT01KeyboardFadeWidthUpper
        {
            get { return _PMT01KeyboardFadeWidthUpper; }
            set
            {
                _PMT01KeyboardFadeWidthUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000A)]
        public byte PMT01VelocityRangeLower
        {
            get { return _PMT01VelocityRangeLower; }
            set
            {
                _PMT01VelocityRangeLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000B)]
        public byte PMT01VelocityRangeUpper
        {
            get { return _PMT01VelocityRangeUpper; }
            set
            {
                _PMT01VelocityRangeUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000C)]
        public byte PMT01VelocityFadeWidthLower
        {
            get { return _PMT01VelocityFadeWidthLower; }
            set
            {
                _PMT01VelocityFadeWidthLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000D)]
        public byte PMT01VelocityFadeWidthUpper
        {
            get { return _PMT01VelocityFadeWidthUpper; }
            set
            {
                _PMT01VelocityFadeWidthUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000E)]
        public IntegraSwitch PMT02Switch
        {
            get { return _PMT02Switch; }
            set
            {
                _PMT02Switch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000F)]
        public byte PMT02KeyboardRangeLower
        {
            get { return _PMT02KeyboardRangeLower; }
            set
            {
                _PMT02KeyboardRangeLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0010)]
        public byte PMT02KeyboardRangeUpper
        {
            get { return _PMT02KeyboardRangeUpper; }
            set
            {
                _PMT02KeyboardRangeUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0011)]
        public byte PMT02KeyboardFadeWidthLower
        {
            get { return _PMT02KeyboardFadeWidthLower; }
            set
            {
                _PMT02KeyboardFadeWidthLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0012)]
        public byte PMT02KeyboardFadeWidthUpper
        {
            get { return _PMT02KeyboardFadeWidthUpper; }
            set
            {
                _PMT02KeyboardFadeWidthUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0013)]
        public byte PMT02VelocityRangeLower
        {
            get { return _PMT02VelocityRangeLower; }
            set
            {
                _PMT02VelocityRangeLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0014)]
        public byte PMT02VelocityRangeUpper
        {
            get { return _PMT02VelocityRangeUpper; }
            set
            {
                _PMT02VelocityRangeUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0015)]
        public byte PMT02VelocityFadeWidthLower
        {
            get { return _PMT02VelocityFadeWidthLower; }
            set
            {
                _PMT02VelocityFadeWidthLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0016)]
        public byte PMT02VelocityFadeWidthUpper
        {
            get { return _PMT02VelocityFadeWidthUpper; }
            set
            {
                _PMT02VelocityFadeWidthUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0017)]
        public IntegraSwitch PMT03Switch
        {
            get { return _PMT03Switch; }
            set
            {
                _PMT03Switch = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0018)]
        public byte PMT03KeyboardRangeLower
        {
            get { return _PMT03KeyboardRangeLower; }
            set
            {
                _PMT03KeyboardRangeLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0019)]
        public byte PMT03KeyboardRangeUpper
        {
            get { return _PMT03KeyboardRangeUpper; }
            set
            {
                _PMT03KeyboardRangeUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001A)]
        public byte PMT03KeyboardFadeWidthLower
        {
            get { return _PMT03KeyboardFadeWidthLower; }
            set
            {
                _PMT03KeyboardFadeWidthLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001B)]
        public byte PMT03KeyboardFadeWidthUpper
        {
            get { return _PMT03KeyboardFadeWidthUpper; }
            set
            {
                _PMT03KeyboardFadeWidthUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001C)]
        public byte PMT03VelocityRangeLower
        {
            get { return _PMT03VelocityRangeLower; }
            set
            {
                _PMT03VelocityRangeLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001D)]
        public byte PMT03VelocityRangeUpper
        {
            get { return _PMT03VelocityRangeUpper; }
            set
            {
                _PMT03VelocityRangeUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001E)]
        public byte PMT03VelocityFadeWidthLower
        {
            get { return _PMT03VelocityFadeWidthLower; }
            set
            {
                _PMT03VelocityFadeWidthLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001F)]
        public byte PMT03VelocityFadeWidthUpper
        {
            get { return _PMT03VelocityFadeWidthUpper; }
            set
            {
                _PMT03VelocityFadeWidthUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0020)]
        public IntegraSwitch PMT04Switch
        {
            get { return _PMT04Switch; }
            set
            {
                _PMT04Switch =
                  value; NotifyPropertyChanged();
            }
        }

        [Offset(0x0021)]
        public byte PMT04KeyboardRangeLower
        {
            get { return _PMT04KeyboardRangeLower; }
            set
            {
                _PMT04KeyboardRangeLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0022)]
        public byte PMT04KeyboardRangeUpper
        {
            get { return _PMT04KeyboardRangeUpper; }
            set
            {
                _PMT04KeyboardRangeUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0023)]
        public byte PMT04KeyboardFadeWidthLower
        {
            get { return _PMT04KeyboardFadeWidthLower; }
            set
            {
                _PMT04KeyboardFadeWidthLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0024)]
        public byte PMT04KeyboardFadeWidthUpper
        {
            get { return _PMT04KeyboardFadeWidthUpper; }
            set
            {
                _PMT04KeyboardFadeWidthUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0025)]
        public byte PMT04VelocityRangeLower
        {
            get { return _PMT04VelocityRangeLower; }
            set
            {
                _PMT04VelocityRangeLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0026)]
        public byte PMT04VelocityRangeUpper
        {
            get { return _PMT04VelocityRangeUpper; }
            set
            {
                _PMT04VelocityRangeUpper = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0027)]
        public byte PMT04VelocityFadeWidthLower
        {
            get { return _PMT04VelocityFadeWidthLower; }
            set
            {
                _PMT04VelocityFadeWidthLower = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0028)]
        public byte PMT04VelocityFadeWidthUpper
        {
            get { return _PMT04VelocityFadeWidthUpper; }
            set
            {
                _PMT04VelocityFadeWidthUpper = value;
                NotifyPropertyChanged();
            }
        }
    }
}
