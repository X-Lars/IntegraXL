using IntegraXL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Models
{
    [Integra(0x00000000, 0x00000012)]
    public class PCMDrumKitCommon : IntegraModel
    {
        [Offset(0x0000)] byte[] _KitName = new byte[12];
        [Offset(0x000C)] byte _KitLevel;
        
        public PCMDrumKitCommon(PCMDrumKit drumKit) : base(drumKit.Device)
        {
            Address = drumKit.Address;
        }

        [Offset(0x0000)]
        public string KitName
        {
            get { return Encoding.ASCII.GetString(_KitName); }
            set
            {
                _KitName = Encoding.ASCII.GetBytes(value);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000C)]
        public byte KitLevel
        {
            get { return _KitLevel; }
            set
            {
                _KitLevel = value;
                NotifyPropertyChanged();
            }
        }
    }
}
