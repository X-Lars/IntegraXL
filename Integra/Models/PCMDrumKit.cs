using Integra.Core;
using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class PCMDrumKit : IntegraBase<PCMDrumKit>, IIntegraPartial
    {
        private IntegraParts _Part;
        private PCMDrumKitCommon _Common;

        public PCMDrumKit(IntegraAddress address, IntegraParts part) : base(address)
        {
            Name = "PCM Drum Kit";
            Part = part;
            Common = new PCMDrumKitCommon(address, part);
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

        public PCMDrumKitCommon Common
        {
            get { return _Common; }
            set
            {
                if(_Common != value)
                {
                    _Common = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
    }
}
