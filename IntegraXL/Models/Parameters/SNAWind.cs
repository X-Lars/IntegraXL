using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    public abstract class SNAWindBase : IntegraSNAParameter
    {
        public SNAWindBase(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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

    public abstract class SNAWindBaseExt : SNAWindBase
    {
        public SNAWindBaseExt(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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

        public IEnumerable<SNAPlayScale> PlayScales => Enum.GetValues(typeof(SNAPlayScale)).Cast<SNAPlayScale>();
        public IEnumerable<IntegraScaleKey> ScaleKeys => Enum.GetValues(typeof(IntegraScaleKey)).Cast<IntegraScaleKey>();
    }

    /// <remarks><i>
    /// INT: 066 Oboe<br/>
    /// INT: 067 Bassoon<br/>
    /// INT: 068 Clarinet<br/>
    /// INT: 069 Piccolo<br/>
    /// INT: 070 Flute<br/>
    /// ExSN2: 005 English Horn<br/>
    /// ExSN2: 006 Bass Clarinet<br/>
    /// </i>Off | Staccato </remarks>
    public sealed class SNAWind1 : SNAWindBaseExt
    {
        public SNAWind1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBrass1 Variation
        {
            get => (VarBrass1)this[4];
            set
            {
                this[4] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarBrass1> Variations => Enum.GetValues(typeof(VarBrass1)).Cast<VarBrass1>();
    }

    /// <remarks><i>
    /// INT: 071 Pan Flute<br/>
    /// </i>Off | Staccato | Flutter</remarks>
    public sealed class SNAWind2 : SNAWindBaseExt
    {
        public SNAWind2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarWind2 Variation
        {
            get => (VarWind2)this[4];
            set
            {
                this[4] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarWind2> Variations => Enum.GetValues(typeof(VarWind2)).Cast<VarWind2>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks><i>
    /// INT: 074 Uilleann Pipes<br/>
    /// INT: 075 Bag Pipes<br/>
    /// </i></remarks>
    public sealed class SNAPipes : IntegraSNAParameter
    {
        public SNAPipes(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int DroneLevel
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public int DronePitch
        {
            get => this[1] - 64;
            set
            {
                this[1] = (byte)(value.Clamp(-12, 12) + 64);
                NotifyPropertyChanged();
            }
        }

        public VarPipes Variation
        {
            get => (VarPipes)this[2];
            set
            {
                this[2] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarPipes> Variations => Enum.GetValues(typeof(VarPipes)).Cast<VarPipes>();
    }

    /// <remarks><i>
    /// INT: 072 Shakuhachi<br/>
    /// ExSN1: 004 Ryuteki<br/><br/>
    /// ExSN2: 012 Ocarina SopC<br/>
    /// ExSN2: 013 Ocarina SopF<br/>
    /// ExSN2: 014 Ocarina Alto<br/>
    /// ExSN2: 015 Ocarina Bass<br/>    
    /// </i>Off | Staccato | Ornament</remarks>
    public sealed class SNAWind3 : SNAWindBase
    {
        public SNAWind3(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarStrings2 Variation
        {
            get => (VarStrings2)this[2];
            set
            {
                this[2] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarStrings2> Variations => Enum.GetValues(typeof(VarStrings2)).Cast<VarStrings2>();
    }

    /// <remarks><i>
    /// ExSN1: 003 Tin Whistle<br/>
    /// </i></remarks>
    public sealed class SNAWind4 : SNAWindBase
    {
        public SNAWind4(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarWind3 Variation
        {
            get => (VarWind3)this[2];
            set
            {
                this[2] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarWind3> Variations => Enum.GetValues(typeof(VarWind3)).Cast<VarWind3>();
    }

    /// <remarks><i>
    /// ExSN2: 008 Soprano Recorder</br>
    /// ExSN2: 009 Alto Recorder</br>
    /// ExSN2: 0010 Tenor Recorder</br>
    /// ExSN2: 0011 Bass Recorder</br>
    /// </i></remarks>
    public sealed class SNAWind5 : SNAWindBase
    {
        public SNAWind5(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBrass1 Variation
        {
            get => (VarBrass1)this[2];
            set
            {
                this[2] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarBrass1> Variations => Enum.GetValues(typeof(VarBrass1)).Cast<VarBrass1>();

    }

    /// <remarks><i>
    /// INT: 062 Soprano Sax 2<br/>
    /// INT: 063 Alto Sax 2<br/>
    /// INT: 064 Tenor Sax 2<br/>
    /// INT: 065 Baritone Sax 2<br/>
    /// ExSN2: 001 Soprano Sax<br/>
    /// ExSN2: 002 Alto Sax<br/>
    /// ExSN2: 003 Tenor Sax<br/>
    /// ExSN2: 004 Baritone Sax<br/>
    /// </i></remarks>
    public sealed class SNASax : SNAWindBaseExt
    {
        public SNASax(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public SNAPortaGliss PortaGliss
        {
            get => (SNAPortaGliss)this[5];
            set
            {
                this[5] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public VarWind3 Variation
        {
            get => (VarWind3)this[5];
            set
            {
                this[5] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<SNAPortaGliss> Portas     => Enum.GetValues(typeof(SNAPortaGliss)).Cast<SNAPortaGliss>();
        public IEnumerable<VarWind4>      Variations => Enum.GetValues(typeof(VarWind4)).Cast<VarWind4>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks><i>
    /// INT: 055 London Choir<br/>
    /// INT: 056 Boys Choir
    /// </i></remarks>
    public sealed class SNAChoir : IntegraSNAParameter
    {
        public SNAChoir(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public IntegraSwitch HoldLegatoMode
        {
            get => (IntegraSwitch)this[0];
            set
            {
                this[0] = (byte)value;
                NotifyPropertyChanged();
            }
        }
        public VarChoir Variation
        {
            get { return (VarChoir)this[1]; }
            set
            {
                this[1] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<IntegraSwitch> HoldLegatoModes => Enum.GetValues(typeof(IntegraSwitch)).Cast<IntegraSwitch>();
        public IEnumerable<VarChoir> Variations => Enum.GetValues(typeof(VarChoir)).Cast<VarChoir>();
    }
}
