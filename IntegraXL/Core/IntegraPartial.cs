using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{
    public abstract class IntegraPartial : IntegraModel
    {
        protected IntegraPartial(Integra device, Parts part) : base(device)
        {
            Debug.Print($"[{nameof(IntegraPartial)}] Constructor<{GetType().Name}>({part})");
            Address += (uint)part << 8;
            Part = part;
        }

        public Parts Part { get; }

        protected internal override uint GetModelHash()
        {
            return base.GetModelHash() | (uint)Part << 8;
        }
    }
}
