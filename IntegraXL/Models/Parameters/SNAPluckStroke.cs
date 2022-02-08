using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks><i>
    /// INT: 073 Sitar
    /// ExSN1: 010 Sarangi
    /// </i></remarks>
    public sealed class SNASitar : IntegraSNAMapper
    {
        public SNASitar(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int ResonanceLevel
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public int TamburaLevel
        {
            get => this[1] - 64;
            set
            {
                this[1] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public int TamburaPitch
        {
            get => this[2] - 64;
            set
            {
                this[2] = (byte)(value.Clamp(-12, 12) + 64);
                NotifyPropertyChanged();
            }
        }
    }
    /// <remarks><i>
    /// ExSN1: 005 Tsugaru<br/>
    /// ExSN1: 006 Sansin<br/>
    /// </i></remarks>
    public sealed class SNAShamisen : IntegraSNAMapper
    {
        public SNAShamisen(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int ResonanceLevel
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public int BendDepth
        {
            get => this[1] - 64;
            set
            {
                this[1] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public IntegraSwitch BuzzKeySwitch
        {
            get => (IntegraSwitch)this[2];
            set
            {
                this[2] = (byte)(value);
                NotifyPropertyChanged();
            }
        }

        public VarShamisen Variation
        {
            get => (VarShamisen)this[3];
            set
            {
                this[3] = (byte)(value);
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<IntegraSwitch> Switch     => Enum.GetValues(typeof(IntegraSwitch)).Cast<IntegraSwitch>();
        public IEnumerable<VarShamisen>   Variations => Enum.GetValues(typeof(VarShamisen)).Cast<VarShamisen>();
    }

    public sealed class SNAKoto : IntegraSNAMapper
    {
        public SNAKoto(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int TremoloSpeed
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public IntegraSwitch GlissandoMode
        {
            get => (IntegraSwitch)this[1];
            set
            {
                this[1] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public SNAPlayScale PlayScale
        {
            get => (SNAPlayScale)this[2];
            set
            {
                this[2] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IntegraScaleKey ScaleKey
        {
            get => (IntegraScaleKey)this[3];
            set
            {
                this[3] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IntegraSwitch BuzzKeySwitch
        {
            get => (IntegraSwitch)this[4];
            set
            {
                this[4] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public VarKoto Variation
        {
            get => (VarKoto)this[5];
            set
            {
                this[5] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<SNAPlayScale>    PlayScales => Enum.GetValues(typeof(SNAPlayScale)).Cast<SNAPlayScale>();
        public IEnumerable<IntegraScaleKey> ScaleKeys  => Enum.GetValues(typeof(IntegraScaleKey)).Cast<IntegraScaleKey>();
        public IEnumerable<VarKoto>         Variations => Enum.GetValues(typeof(VarKoto)).Cast<VarKoto>();
    }

    /// <remarks><i>
    /// ExSN1: 008 Taishou Koto
    /// </i></remarks>  
    public sealed class SNATaishou : IntegraSNAMapper
    {
        public SNATaishou(SuperNATURALAcousticToneCommon provider) : base(provider)
        {
        }

        public int NoiseLevel
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public int TremoloSpeed
        {
            get => this[1] - 64;
            set
            {
                this[1] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }
    }

    /// <remarks><i>
    /// ExSN1: 009 Kalimba
    /// </i></remarks>  
    public sealed class SNAKalimba : IntegraSNAMapper
    {
        public SNAKalimba(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int ResonanceLevel
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public VarKalimba Variation
        {
            get => (VarKalimba)this[1];
            set
            {
                this[1] = (byte)(value);
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarKalimba> Variations => Enum.GetValues(typeof(VarKalimba)).Cast<VarKalimba>();
    }

}