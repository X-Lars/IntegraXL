using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Providers
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
    public sealed class CommonChorusGM2 : IntegraMFXProvider
    {
        public CommonChorusGM2(StudioSetCommonChorus provider) : base(provider) { }

        public int PreLPF
        {
            get => this[0];
            set
            {
                if (this[0] != value)
                {
                    this[0] = value.Clamp(0, 7);
                    NotifyPropertyChanged();
                }
            }
        }

        public int Level
        {
            get => this[1];
            set
            {
                if (this[1] != value)
                { 
                    this[1] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int Feedback
        {
            get => this[2];
            set
            {
                if (this[2] != value)
                {
                    this[2] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int Delay
        {
            get => this[3];
            set
            {
                if (this[3] != value)
                {
                    this[3] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int Rate
        {
            get => this[4];
            set
            {
                if (this[4] != value)
                {
                    this[4] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int Depth
        {
            get => this[5];
            set
            {
                if (this[5] != value)
                {
                    this[5] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int ReverbSendLevel
        {
            get => this[6];
            set
            {
                if (this[6] != value)
                {
                    this[6] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
