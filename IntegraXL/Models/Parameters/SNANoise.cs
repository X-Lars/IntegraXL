using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks><i>
    /// INT: 010 Pure Vintage EP1<br/>
    /// INT: 011 Pure Vintage EP2<br/>
    /// INT: 012 Pure Wurly<br/>
    /// INT: 013 Pure Vintage EP3<br/>
    /// INT: 014 Old Hammer EP<br/>
    /// INT: 015 Dyno Piano<br/>
    /// INT: 016 Clav CB Flat<br/>
    /// INT: 017 Clav CA Flat<br/>
    /// INT: 018 Clav CB Medium<br/>
    /// INT: 019 Clav CA Medium<br/>
    /// INT: 020 Clav CB Brillia<br/>
    /// INT: 021 Clav CA Brillia<br/>
    /// INT: 022 Clav CB Combo<br/>
    /// INT: 023 Clav CA Combo<br/>
    /// INT: 030 French Accordion<br/>
    /// INT: 031 Italian Accordion<br/>
    /// INT: 033 Bandoneon<br/>
    /// </i></remarks>
    public sealed class SNANoise : IntegraSNAMapper
    {
        public SNANoise(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        { 
            get => this[0] - 64; 
            set 
            { 
                this[0] = (byte)(value.Clamp(-64, 63) + 64); 
                NotifyPropertyChanged(); 
            } 
        }
    }

    /// <remarks><i>
    /// INT: 032 Harmonica
    /// </i></remarks>
    public sealed class SNAHarmonica : IntegraSNAMapper
    {
        public SNAHarmonica(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public int GrowlSens
        {
            get => this[1];
            set
            {
                this[1] = (byte)(value.Clamp());
                NotifyPropertyChanged();
            }
        }
    }
}