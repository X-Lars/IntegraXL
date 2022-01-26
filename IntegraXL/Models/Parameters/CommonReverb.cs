using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks>
    /// 01: Pre Delay <br/>
    /// 02: Time <br/>
    /// 03: Density <br/>
    /// 04: Diffusion <br/>
    /// 05: LF Damp <br/>
    /// 06: HF Damp <br/>
    /// 07: Spread <br/>
    /// 08: Tone <br/>
    /// </remarks>
    public sealed class CommonReverb : IntegraMFXParameter
    {
        public CommonReverb(StudioSetCommonReverb provider) : base(provider) { }

        public int PreDelay
        {
            get { return this[1]; }
            set 
            { 
                this[1] = value.Clamp(0, 100);
                NotifyPropertyChanged();
            }
        }

        // TODO: Clamp
        public double Time
        {
            get { return this[2] * 0.1; }
            set 
            { 
                this[2] = (int)Math.Round(value / 0.1);
                NotifyPropertyChanged();
            }
        }

        public int Density
        {
            get { return this[3]; }
            set
            {
                this[3] = value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int Diffusion
        {
            get { return this[4]; }
            set
            {
                this[4] = value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int LFDamp
        {
            get { return this[5]; }
            set
            {
                this[5] = value.Clamp(0, 100);
                NotifyPropertyChanged();
            }
        }

        public int HFDamp
        {
            get { return this[6]; }
            set
            {
                this[6] = value.Clamp(0, 100);
                NotifyPropertyChanged();
            }
        }

        public int Spread
        {
            get { return this[7]; }
            set
            {
                this[7] = value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int Tone
        {
            get { return this[8]; }
            set
            {
                this[8] = value.Clamp();
                NotifyPropertyChanged();
            }
        }
    }
}
