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
            Address += (int)part << 8;
            Part = part;
        }

        public Parts Part { get; }

        protected internal override int GetModelHash()
        {
            return base.GetModelHash() | (int)Part << 8;
        }
    }
}
