using Integra.Core;

namespace Integra.Models
{
    public sealed class StudioSets : IntegraBaseCollection<StudioSets, IntegraStudioSet>
    {
        public StudioSets() : base(0x0F000302, 0x55000040) { }
    }
}
