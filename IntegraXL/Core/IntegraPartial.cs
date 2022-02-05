using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{
    public abstract class IntegraPartial<T> : IntegraModel<T>
    {
        protected IntegraPartial(Integra device, Parts part) : base(device)
        {
            Debug.Print($"[{nameof(IntegraPartial<T>)}] Constructor<{GetType().Name}>({part})");
            Address += (int)part << 8;
            Part = part;
        }

        public Parts Part { get; }

        protected internal override int GetUID()
        {
            return base.GetUID() | (int)Part << 8;
        }
    }
}
