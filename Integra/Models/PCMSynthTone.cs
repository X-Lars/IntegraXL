using Integra.Core;
using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class PCMSynthTone : IntegraBase<PCMSynthTone>, IToneMFX
    {
        private ToneMFX _MFX;

        public PCMSynthTone(IntegraAddress address) : base(address)
        {
            Name = "PCM Synth Tone";
            _MFX = new ToneMFX(address);
        }

        public ToneMFX MFX
        {
            get { return _MFX; }
        }
    }
}
