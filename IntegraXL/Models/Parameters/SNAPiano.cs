using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks><i>
    /// INT 001: Concert Grand<br/>
    /// INT 002: Grand Piano1<br/>
    /// INT 003: Grand Piano2<br/>
    /// INT 004: Grand Piano3<br/>
    /// INT 005: Mellow Piano<br/>
    /// INT 006: Bright Piano<br/>
    /// INT 007: Upright Piano<br/>
    /// INT 008: Concert Mono<br/>
    /// INT 009: Honky-tonk<br/>
    /// </i></remarks>
    public sealed class SNAPiano : IntegraSNAParameter
    {
        public SNAPiano(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int StringResonance
        {
            get { return this[0]; }
            set
            {
                this[0] = (byte)value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int KeyOffResonance
        {
            get { return this[1]; }
            set
            {
                this[1] = (byte)value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int HammerNoise
        {
            get { return this[2] - 64; }
            set
            {
                this[2] = (byte)(value.Clamp(-2, 2) + 64);
                NotifyPropertyChanged();
            }
        }
        public int StereoWidth
        {
            get { return this[3]; }
            set
            {
                this[3] = (byte)(value.Clamp(0, 63));
                NotifyPropertyChanged();
            }
        }

        public SNANuance Nuance
        {
            get { return (SNANuance)this[4]; }
            set
            {
                this[4] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public int ToneCharacter
        {
            get { return this[5] - 64; }
            set
            {
                this[5] = (byte)(value.Clamp(-5, 5) + 64);
                NotifyPropertyChanged();
            }
        }

        public static IEnumerable<SNANuance> Nuances
        {
            get { return Enum.GetValues(typeof(SNANuance)).Cast<SNANuance>(); }
        }
    }
}
