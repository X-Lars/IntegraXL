using Integra.Core;
using Integra.Core.Interfaces;

namespace Integra.Models
{
    public class PCMDrumKit : IntegraBase<PCMDrumKit>, IIntegraPartial
    {
        private IntegraParts _Part;

        private PCMDrumKitCommon _Common;
        private PCMDrumKitCommonCompEQ _CompEQ;

        public PCMDrumKit(IntegraAddress address, IntegraParts part) : base(address)
        {
            Name = "PCM Drum Kit";
            Part = part;
            Common = new PCMDrumKitCommon(address, part);
            CompEQ = new PCMDrumKitCommonCompEQ(address, Part);
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
        
        public PCMDrumKitCommonCompEQ CompEQ
        {
            get { return _CompEQ; }
            set
            {
                if(_CompEQ != value)
                {
                    _CompEQ = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
