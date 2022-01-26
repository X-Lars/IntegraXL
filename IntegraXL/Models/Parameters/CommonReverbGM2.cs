
using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks>
    /// 00: Character <br/>
    /// 03: Time <br/>
    /// </remarks>
    public sealed class CommonReverbGM2 : IntegraMFXParameter
    {
        public CommonReverbGM2(StudioSetCommonReverb provider) : base(provider) { }

        public int Character
        {
            get { return this[0]; }
            set
            {
                this[0] = value.Clamp(0, 5);
                NotifyPropertyChanged();
            }
        }

        public int Time
        {
            get { return this[3]; }
            set
            {
                this[3] = value.Clamp();
                NotifyPropertyChanged();
            }
        }

    }
}
