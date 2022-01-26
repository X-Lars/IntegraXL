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
    public sealed class CommonChorus : IntegraMFXParameter
    {
        public CommonChorus(StudioSetCommonChorus provider) : base(provider) { }

        public IntegraChorusFilterTypes FilterType
        {
            get { return (IntegraChorusFilterTypes)this[0]; }
            set
            {
                this[0] = (int)value;
                NotifyPropertyChanged();
            }
        }

        public IntegraMidFrequencies CutoffFreq
        {
            get { return (IntegraMidFrequencies)this[1]; }
            set
            {
                this[1] = (int)value;
                NotifyPropertyChanged();
            }
        }

        public int PreDelay
        {
            get { return this[2]; }
            set
            {
                // TODO: Clamp
                this[2] = value;
                NotifyPropertyChanged();
            }
        }

        public IntegraRateNoteSwitch RateSwitch
        {
            get { return (IntegraRateNoteSwitch)this[3]; }
            set
            {
                this[3] = (int)value;
                NotifyPropertyChanged();
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
            get { return (IntegraNoteRates)this[5]; }
            set
            {
                this[5] = (int)value;
                NotifyPropertyChanged();
            }
        }

        public int Depth
        {
            get { return this[6]; }
            set
            {
                this[6] = value.Clamp();
                NotifyPropertyChanged();
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
            get { return this[8]; }
            set
            {
                this[8] = value.Clamp();
            }
        }

        public static IEnumerable<IntegraChorusFilterTypes> FilterTypes
        {
            get { return Enum.GetValues(typeof(IntegraChorusFilterTypes)).Cast<IntegraChorusFilterTypes>(); }
        }

        public static IEnumerable<IntegraMidFrequencies> CutoffFreqs
        {
            get { return Enum.GetValues(typeof(IntegraMidFrequencies)).Cast<IntegraMidFrequencies>(); }
        }

        public static List<string> PreDelays
        {
            get { return IntegraPreDelay.Values; }
        }

        public static IEnumerable<IntegraRateNoteSwitch> RateSwitchs
        {
            get { return Enum.GetValues(typeof(IntegraRateNoteSwitch)).Cast<IntegraRateNoteSwitch>(); }
        }

        public static IEnumerable<IntegraNoteRates> NoteRates
        {
            get { return Enum.GetValues(typeof(IntegraNoteRates)).Cast<IntegraNoteRates>(); }
        }
    }
}
