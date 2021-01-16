using Integra.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class SuperNATURALSynthTone : IntegraBase<SuperNATURALSynthTone>, IToneMFX
    {
        private ToneMFX _MFX;

        public SuperNATURALSynthTone(IntegraAddress address) : base(address)
        {
            Name = "SuperNATURAL Synth Tone";
            _MFX = new ToneMFX(address);
        }

        public ToneMFX MFX
        {
            get { return _MFX; }
        }
    }
}
