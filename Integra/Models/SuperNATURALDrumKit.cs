using Integra.Core;
using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class SuperNATURALDrumKit : IntegraBase<SuperNATURALDrumKit>
    {
        private IntegraParts _Part;
        private SuperNATURALDrumKitCommon _Common;

        public SuperNATURALDrumKit(IntegraAddress address, IntegraParts part) : base(address)
        {
            Name = "SuperNATURAL Drum Kit";
            Part = part;

            Common = new SuperNATURALDrumKitCommon(address, part);
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
    }
}
