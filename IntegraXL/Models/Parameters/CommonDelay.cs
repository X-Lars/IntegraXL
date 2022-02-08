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
            get { return (IntegraRateMSecSwitch)this[0]; }
            set
            {
                this[0] = (int)value;
                NotifyPropertyChanged();
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
            get { return (IntegraNoteRates)this[2]; }
            set
            {
                this[2] = (int)value;
                NotifyPropertyChanged();
            }
        }

        public IntegraRateMSecSwitch RightSwitch
        {
            get { return (IntegraRateMSecSwitch)this[3]; }
            set
            {
                this[3] = (int)value;
                NotifyPropertyChanged();
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
            get { return (IntegraNoteRates)this[5]; }
            set
            {
                this[5] = (int)value;
                NotifyPropertyChanged();
            }
        }

        public IntegraRateMSecSwitch CenterSwitch
        {
            get { return (IntegraRateMSecSwitch)this[6]; }
            set
            {
                this[6] = (int)value;
                NotifyPropertyChanged();
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
            get { return (IntegraNoteRates)this[8]; }
            set
            {
                this[8] = (int)value;
                NotifyPropertyChanged();
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
            get { return (IntegraDelayHFDamps)this[10]; }
            set
            {
                this[10] = (int)value;
                NotifyPropertyChanged();
            }
        }

        public int LeftLevel
        {
            get { return this[11]; }
            set
            {
                this[11] = value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int RightLevel
        {
            get { return this[12]; }
            set
            {
                this[12] = value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int CenterLevel
        {
            get { return this[13]; }
            set
            {
                this[13] = value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public static IEnumerable<IntegraRateMSecSwitch> RateSwitchs
        {
            get { return Enum.GetValues(typeof(IntegraRateMSecSwitch)).Cast<IntegraRateMSecSwitch>(); }
        }

        public static IEnumerable<IntegraNoteRates> Notes
        {
            get { return Enum.GetValues(typeof(IntegraNoteRates)).Cast<IntegraNoteRates>(); }
        }

        public static IEnumerable<IntegraDelayHFDamps> HFDamps
        {
            get { return Enum.GetValues(typeof(IntegraDelayHFDamps)).Cast<IntegraDelayHFDamps>(); }
        }

        public static List<string> PreDelays
        {
            get { return IntegraPreDelay.Values; }
        }
    }
}
