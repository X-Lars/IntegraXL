using IntegraXL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Models.Providers
{
    public sealed class MFXThru : IntegraMFXProvider
    {
        public MFXThru(MFX provider) : base(provider) { }
    }
}
