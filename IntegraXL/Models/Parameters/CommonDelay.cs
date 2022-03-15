using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks>
    /// 00: Left Switch (ms / Note) <br/>
    /// 01: Delay Left (ms) <br/>
    /// 02: Delay Left (Note) <br/>
    /// 03: Right Switch (ms / Note) <br/>
    /// 04: Delay Right (ms) <br/>
    /// 05: Delay Right (Note) <br/>
    /// 06: Center Switch (ms / Note) <br/>
    /// 07: Delay Center (ms) <br/>
    /// 08: Delay Center (Note) <br/>
    /// 09: Center Feedback <br/>
    /// 10: HF Damp <br/>
    /// 11: Left Level <br/>
    /// 12: Right Level <br/>
    /// 13: Center Level <br/>
    /// </remarks>
    public sealed class CommonDelay : IntegraMFXMapper
    {
        public CommonDelay(StudioSetCommonChorus provider) : base(provider) { }

        public IntegraRateMSecSwitch LeftSwitch
        {
            get => (IntegraRateMSecSwitch)this[0];
            set
            {
                if (LeftSwitch != value)
                {
                    this[0] = (int)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int LeftMSec
        {
            get { return this[1]; }
            set
            {
                this[1] = value.Clamp(0, 1000);
                NotifyPropertyChanged();
            }
        }

        public IntegraNoteRates LeftNote
        {
            get => (IntegraNoteRates)this[2];
            set
            {
                if (LeftNote != value)
                {
                    this[2] = (int)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraRateMSecSwitch RightSwitch
        {
            get => (IntegraRateMSecSwitch)this[3];
            set
            {
                if (RightSwitch != value)
                {
                    this[3] = (int)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int RightMSec
        {
            get { return this[4]; }
            set
            {
                this[4] = value.Clamp(0, 1000);
                NotifyPropertyChanged();
            }
        }

        public IntegraNoteRates RightNote
        {
            get => (IntegraNoteRates)this[5];
            set
            {
                if (RightNote != value)
                {
                    this[5] = (int)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraRateMSecSwitch CenterSwitch
        {
            get => (IntegraRateMSecSwitch)this[6];
            set
            {
                if (CenterSwitch != value)
                {
                    this[6] = (int)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int CenterMSec
        {
            get { return this[7]; }
            set
            {
                this[7] = value.Clamp(0, 1000);
                NotifyPropertyChanged();
            }
        }

        public IntegraNoteRates CenterNote
        {
            get => (IntegraNoteRates)this[8];
            set
            {
                if (CenterNote != value)
                {
                    this[8] = (int)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int CenterFeedback
        {
            get { return this[9].DeserializeFeedback(); }
            set
            {
                this[9] = value.SerializeFeedback();
                NotifyPropertyChanged();
            }
        }

        public IntegraDelayHFDamps HFDamp
        {
            get => (IntegraDelayHFDamps)this[10];
            set
            {
                if (HFDamp != value)
                {
                    this[10] = (int)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int LeftLevel
        {
            get => this[11];
            set
            {
                if (this[11] != value)
                {
                    this[11] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int RightLevel
        {
            get => this[12];
            set
            {
                if (this[12] != value)
                {
                    this[12] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int CenterLevel
        {
            get => this[13];
            set
            {
                if (this[13] != value)
                {
                    this[13] = value.Clamp();
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
