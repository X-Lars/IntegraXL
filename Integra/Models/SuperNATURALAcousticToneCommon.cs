using Integra.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class SuperNATURALAcousticToneCommon : IntegraBase<SuperNATURALAcousticToneCommon>
    {
        public SuperNATURALAcousticToneCommon(IntegraAddress address) : base(address, 0x00000046)
        {
            Name = "SuperNATURAL Acoustic Tone Common";
            Console.WriteLine($"[{nameof(SuperNATURALAcousticToneCommon)}] {Address}");
        }

    }
}
