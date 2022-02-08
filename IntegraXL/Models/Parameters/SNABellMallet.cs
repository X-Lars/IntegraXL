using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks><i>
    /// INT: 024 Glockenspiel<br/>
    /// INT: 025 Vibraphone<br/>
    /// INT: 026 Marimba<br/>
    /// INT: 027 Xylophone<br/>
    /// INT: 028 Tubular Bells<br/>
    /// ExSN1: 001 Santoor<br/>
    /// ExSN1: 002 Yang Chin<br/>
    /// </i></remarks>
    public abstract class SNABellMalletBase : IntegraSNAMapper
    {
        public SNABellMalletBase(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp() + 64);
                NotifyPropertyChanged();
            }
        }

        public int RollSpeed
        {
            get => this[1] - 64;
            set
            {
                this[1] = (byte)(value.Clamp() + 64);
                NotifyPropertyChanged();
            }
        }
    }

    #region Variations

    /// <remarks><i>
    /// INT: 024 Glockenspiel<br/>
    /// INT: 026 Marimba<br/>
    /// INT: 027 Xylophone<br/>
    /// INT: 028 Tubular Bells<br/>
    /// </i></remarks>
    public sealed class SNABellMallet1 : SNABellMalletBase
    {
        public SNABellMallet1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBellMallet1 Variation
        {
            get { return (VarBellMallet1)this[3]; }
            set
            {
                this[3] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarBellMallet1> Variations
        {
            get { return Enum.GetValues(typeof(VarBellMallet1)).Cast<VarBellMallet1>(); }
        }
    }

    /// <remarks><i>
    /// INT: 025 Vibraphone<br/>
    /// </i></remarks>
    public sealed class SNABellMallet2 : SNABellMalletBase
    {
        public SNABellMallet2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBellMallet2 Variation
        {
            get { return (VarBellMallet2)this[3]; }
            set
            {
                this[3] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarBellMallet2> Variations
        {
            get { return Enum.GetValues(typeof(VarBellMallet2)).Cast<VarBellMallet2>(); }
        }

    }

    /// <remarks><i>
    /// ExSN1: 001 Santoor<br/>
    /// ExSN1: 002 Yang Chin<br/>
    /// </i></remarks>
    public sealed class SNABellMallet3 : SNABellMalletBase
    {
        public SNABellMallet3(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBellMallet3 Variation
        {
            get { return (VarBellMallet3)this[3]; }
            set
            {
                this[3] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarBellMallet3> Variations
        {
            get { return Enum.GetValues(typeof(VarBellMallet3)).Cast<VarBellMallet3>(); }
        }

    }

    #endregion
}