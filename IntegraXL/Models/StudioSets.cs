using IntegraXL.Core;
using IntegraXL.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Models
{
    [Integra(0x0F000302, 0x55000000, 64)]
    public class StudioSets : IntegraTemplateCollection<StudioSetTemplate>
    {
        internal StudioSets(Integra device) : base(device) 
        { 
        }
    }
}
