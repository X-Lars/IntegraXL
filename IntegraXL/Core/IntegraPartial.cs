using System.Diagnostics;

namespace IntegraXL.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
