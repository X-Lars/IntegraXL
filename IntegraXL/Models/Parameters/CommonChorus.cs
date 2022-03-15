using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks>
    /// 01: Filter Type <br/>
    /// 02: Cutoff Freq <br/>
    /// 03: Pre Delay <br/>
    /// 04: Rate Switch (Hz / Note)<br/>
    /// 05: Rate (Hz) <br/>
    /// 06: Rate (Note) <br/>
    /// 07: Depth <br/>
    /// 08: Phase <br/>
    /// 09: Feedback <br/>
    /// </remarks>
    public sealed class CommonChorus : IntegraMFXMapper
    {
        public CommonChorus(StudioSetCommonChorus provider) : base(provider) { }

        public IntegraChorusFilterTypes FilterType
        {
            get => (IntegraChorusFilterTypes)this[0];
            set
            {
                if (FilterType != value)
                {
                    this[0] = (int)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraMidFrequencies CutoffFreq
        {
            get => (IntegraMidFrequencies)this[1];
            set
            {
                if (CutoffFreq != value)
                {
                    this[1] = (int)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int PreDelay
        {
            get => this[2];
            set
            {
                if (this[2] != value)
                {
                    // TODO: Clamp
                    this[2] = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraRateNoteSwitch RateSwitch
        {
            get => (IntegraRateNoteSwitch)this[3];
            set
            {
                if (RateSwitch != value)
                {
                    this[3] = (int)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double RateHz
        {
            get { return this[4] * 0.05; }
            set
            {
                // TODO: Clamp
                this[4] = (int)Math.Round(value / 0.05);
            }
        }

        public IntegraNoteRates RateNote
        {
            get => (IntegraNoteRates)this[5];
            set
            {
                if (RateNote != value)
                {
                    this[5] = (int)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Depth
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

        public int Phase
        {
            get { return this[7] * 2; }
            set
            {
                // TODO: Clamp
                this[7] = value / 2;
                NotifyPropertyChanged();
            }
        }

        public int Feedback
        {
            get => this[8];
            set
            {
                if (this[8] != value)
                {
                    this[8] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public static List<string> PreDelays
        {
            get { return IntegraPreDelay.Values; }
        }
    }
}
