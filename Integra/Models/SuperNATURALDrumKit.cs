using Integra.Core;
using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class SuperNATURALDrumKit : IntegraBase<SuperNATURALDrumKit>//, IToneMFX
    {
        //private ToneMFX _MFX;

        public SuperNATURALDrumKit(IntegraAddress address) : base(address)
        {
            Name = "SuperNATURAL Drum Kit";
            //_MFX = new ToneMFX(address);
        }

        //public ToneMFX MFX
        //{
        //    get { return _MFX; }
        //}
    }
}
