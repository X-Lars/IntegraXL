
namespace IntegraXL.Models.Parameters
{
    /// <remarks>
    /// 00: Character <br/>
    /// 03: Time <br/>
    /// </remarks>
    public sealed class CommonReverbGM2 : CommonReverbOff
    {
        public CommonReverbGM2(StudioSetCommonReverb provider) : base(provider) { }

        public byte Character
        {
            get { return (byte)this[0]; }
            set
            {
                this[0] = value;
                NotifyPropertyChanged();
            }
        }

        public byte Time
        {
            get { return (byte)this[3]; }
            set
            {
                this[3] = value;
                NotifyPropertyChanged();
            }
        }

    }
}
