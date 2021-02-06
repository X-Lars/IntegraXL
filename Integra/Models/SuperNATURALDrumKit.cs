using Integra.Core;
using Integra.Core.Interfaces;

namespace Integra.Models
{
    public class SuperNATURALDrumKit : IntegraBase<SuperNATURALDrumKit>, IIntegraPartial
    {
        private IntegraParts _Part;
        private SuperNATURALDrumKitCommon _Common;
        private DrumKitCommonCompEQ _CompEQ;
        private IntegraBaseSuperNATURALDrumKitNotes<SuperNATURALDrumKitNote> _Notes;
        public SuperNATURALDrumKit(IntegraAddress address, IntegraParts part) : base(address)
        {
            Name = "SuperNATURAL Drum Kit";
            Part = part;

            Common = new SuperNATURALDrumKitCommon(address, part);
            CompEQ = new DrumKitCommonCompEQ(address, part);
            _Notes = new IntegraBaseSuperNATURALDrumKitNotes<SuperNATURALDrumKitNote>(address, part);
        }

        public IntegraParts Part
        {
            get { return _Part; }
            set
            {
                if (_Part != value)
                {
                    _Part = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public SuperNATURALDrumKitCommon Common
        {
            get { return _Common; }
            set
            {
                _Common = value;
                NotifyPropertyChanged();
            }
        }

        public DrumKitCommonCompEQ CompEQ
        {
            get { return _CompEQ; }
            set
            {
                if (_CompEQ != value)
                {
                    _CompEQ = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraBaseSuperNATURALDrumKitNotes<SuperNATURALDrumKitNote> Notes
        {
            get { return _Notes; }
        }
    }
}
