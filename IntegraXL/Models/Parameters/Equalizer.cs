using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks>
    /// 00: Low Freq <br/>
    /// 01: Low Gain <br/>
    /// 02: Mid1 Freq <br/>
    /// 03: Mid1 Gain <br/>
    /// 04: Mid1 Q <br/>
    /// 05: Mid2 Freq <br/>
    /// 06: Mid2 Gain <br/>
    /// 07: Mid2 Q <br/>
    /// 08: High Freq <br/>
    /// 09: High Gain <br/>
    /// 10: Level <br/>
    /// </remarks>
    public sealed class Equalizer : IntegraMFXParameter
    {
        public Equalizer(MFX provider) : base(provider) { }

        public IntegraLowFrequencies LowFreq
        {
            get { return (IntegraLowFrequencies)this[0]; }
            set
            {
                this[0] = (int)value;
                NotifyPropertyChanged();
            }
        }

        public int LowGain
        {
            get { return this[1] - 15; }
            set
            {
                this[1] = value.Clamp(-15, 15) + 15;
                NotifyPropertyChanged();
            }
        }

        public IntegraMidFrequencies Mid1Freq
        {
            get { return (IntegraMidFrequencies)this[2]; }
            set
            {
                this[2] = (int)value;
            }
        }

        public int Mid1Gain
        {
            get { return this[3] - 15; }
            set
            {
                this[3] = value.Clamp(-15, 15) + 15;
                NotifyPropertyChanged();
            }
        }

        public IntegraMidQs Mid1Q
        {
            get { return (IntegraMidQs)this[4]; }
            set
            {
                this[4] = (int)value;
                NotifyPropertyChanged();
            }
        }

        public IntegraMidFrequencies Mid2Freq
        {
            get { return (IntegraMidFrequencies)(this[5]); }
            set
            {
                this[5] = (int)value;
                NotifyPropertyChanged();
            }
        }

        public int Mid2Gain
        {
            get { return this[6] - 15; }
            set
            {
                this[6] = value.Clamp(-15, 15) + 15;
                NotifyPropertyChanged();
            }
        }

        public IntegraMidQs Mid2Q
        {
            get { return (IntegraMidQs)this[7]; }
            set
            {
                this[7] = (int)value;
                NotifyPropertyChanged();
            }
        }

        public IntegraHighFrequencies HighFreq
        {
            get { return (IntegraHighFrequencies)this[8]; }
            set
            {
                this[8] = (int)value;
                NotifyPropertyChanged();
            }
        }

        public int HighGain
        {
            get { return this[9] - 15; }
            set
            {
                this[9] = value.Clamp(-15, 15) + 15;
                NotifyPropertyChanged();
            }
        }
        
        public int Level
        {
            get { return this[10]; }
            set
            {
                this[10] = value.Clamp();
            }
        }

        public IEnumerable<IntegraLowFrequencies> LowFrequencies
        {
            get { return Enum.GetValues(typeof(IntegraLowFrequencies)).Cast<IntegraLowFrequencies>(); }
        }

        public IEnumerable<IntegraMidFrequencies> MidFrequencies
        {
            get { return Enum.GetValues(typeof(IntegraMidFrequencies)).Cast<IntegraMidFrequencies>(); }
        }
        public IEnumerable<IntegraHighFrequencies> HighFrequencies
        {
            get { return Enum.GetValues(typeof(IntegraHighFrequencies)).Cast<IntegraHighFrequencies>(); }
        }

        public IEnumerable<IntegraMidQs> MidQs
        {
            get { return Enum.GetValues(typeof(IntegraMidQs)).Cast<IntegraMidQs>(); }
        }
    }
}
