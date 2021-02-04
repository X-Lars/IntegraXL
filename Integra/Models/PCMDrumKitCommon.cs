using Integra.Core;
using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class PCMDrumKitCommon : IntegraBase<PCMDrumKitCommon>, IIntegraPartial
    {
        private IntegraParts _Part;

        [Offset(0x0000)] byte[] _KitName = new byte[12];
        [Offset(0x000C)] byte _KitLevel;
        
        public PCMDrumKitCommon(IntegraAddress address, IntegraParts part) : base(address, 0x00000012)
        {
            Name = "PCM Drum Kit Common";
            Part = part;
        }

        public IntegraParts Part
        {
            get { return _Part; }
            set
            {
                if (Part != value)
                {
                    _Part = value;
                    NotifyPropertyChanged();
                }
            }
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
