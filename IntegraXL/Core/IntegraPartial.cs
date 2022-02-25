using System.Diagnostics;

namespace IntegraXL.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class IntegraPartial<TModel> : IntegraModel<TModel>
    {
        protected IntegraPartial(Integra device, Parts part, bool connect = true) : base(device, connect)
        {
            Debug.Print($"[{nameof(IntegraPartial<TModel>)}] Constructor<{GetType().Name}>({part})");
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
