using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks>
    /// 00: Pre LPF <br/>
    /// 01: Level <br/>
    /// 02: Feedback <br/>
    /// 03: Delay <br/>
    /// 04: Rate <br/>
    /// 05: Depth <br/>
    /// 06: Reverb Send Level<br/>
    /// </remarks>
    public sealed class CommonChorusGM2 : IntegraMFXParameter
    {
        public CommonChorusGM2(StudioSetCommonChorus provider) : base(provider) { }

        public int PreLPF
        {
            get { return this[0]; }
            set
            {
                this[0] = value.Clamp(0, 7);
                NotifyPropertyChanged();
            }
        }

        public int Level
        {
            get { return this[1]; }
            set
            {
                this[1] = value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int Feedback
        {
            get { return this[2]; }
            set
            {
                this[2] = value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int Delay
        {
            get { return this[3]; }
            set
            {
                this[3] = value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int Rate
        {
            get { return this[4]; }
            set
            {
                this[4] = value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int Depth
        {
            get { return this[5]; }
            set
            {
                this[5] = value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int ReverbSendLevel
        {
            get { return this[6]; }
            set
            {
                this[6] = value.Clamp();
                NotifyPropertyChanged();
            }
        }
    }
}
