using Integra.Core;
using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class PCMDrumKit : IntegraBase<PCMDrumKit>, IToneMFX
    {
        private ToneMFX _MFX;

        public PCMDrumKit(IntegraAddress address) : base(address)
        {
            Name = "PCM Drum Kit";

            _MFX = new ToneMFX(address);
        }

        public ToneMFX MFX
        {
            get { return _MFX; }
        }
    }
}
