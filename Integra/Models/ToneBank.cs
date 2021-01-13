using Integra.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
  
    public sealed class ToneBank<T> : IntegraBase<ToneBank<T>> where T: IntegraBaseToneBank, new()
    {
        public ToneBank() : base(0x0F000402)
        {
        }
    }

    public class SNAPresetToneBank : IntegraBaseToneBank { public SNAPresetToneBank() : base(0x59, 0x40, 256) { Name = "SuperNATURAL Acoustic Preset Tones"; } }
    public class SNDPresetToneBank : IntegraBaseToneBank { public SNDPresetToneBank() : base(0x58, 0x40, 26) { Name = "SuperNATURAL Preset Drum Kits"; } }
    public class SNSPresetToneBank : IntegraBaseToneBank { public SNSPresetToneBank() : base(0x5F, 0x40, 1109) { Name = "SuperNATURAL Synth Preset Tonebank"; } }

}
