using Integra.Core;
using Integra.Core.Interfaces;

namespace Integra.Models
{
    public class PCMDrumKit : IntegraBase<PCMDrumKit>, IIntegraPartial
    {
        private IntegraParts _Part;

        private PCMDrumKitCommon _Common;
        private DrumKitCommonCompEQ _CompEQ;
        private PCMDrumKitCommon02 _Common02;

        public PCMDrumKit(IntegraAddress address, IntegraParts part) : base(address)
        {
            Name = "PCM Drum Kit";
            Part = part;
            Common = new PCMDrumKitCommon(address, part);
            CompEQ = new DrumKitCommonCompEQ(address, part);
            Common02 = new PCMDrumKitCommon02(address, part);
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
        
        public DrumKitCommonCompEQ CompEQ
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

        public PCMDrumKitCommon02 Common02
        {
            get { return _Common02; }
            set
            {
                if(_Common02 != value)
                {
                    _Common02 = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
