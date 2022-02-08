using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks><i>
    /// INT: 041 Acoustic Bass<br/>
    /// INT: 042 Fingered Bass<br/>
    /// INT: 043 Picked Bass<br/>
    /// INT: 044 Fretless Bass<br/>
    /// ExSN3: 006 Acoustic Bass 2<br/>
    /// ExSN3: 007 Fingered Bass 2<br/>
    /// ExSN3: 008 Picked Bass 2<br/>
    /// </i></remarks>
    public abstract class SNABassBase : IntegraSNAMapper
    {
        public SNABassBase(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp() + 64);
                NotifyPropertyChanged();
            }
        }

        
    }

    /// <remarks><i>
    /// INT: 041 Acoustic Bass<br/>
    /// INT: 044 Fretless Bass<br/>
    /// ExSN3: 006 Acoustic Bass 2<br/>
    /// </i></remarks>
    public sealed class SNABass1 : SNABassBase
    {
        public SNABass1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBass1 Variation
        {
            get { return (VarBass1)this[1]; }
            set
            {
                this[1] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarBass1> Variations
        {
            get { return Enum.GetValues(typeof(VarBass1)).Cast<VarBass1>(); }
        }
    }

    /// <remarks><i>
    /// INT: 042 Fingered Bass<br/>
    /// ExSN3: 007 Fingered Bass 2<br/>
    /// </i></remarks>
    public sealed class SNABass2 : SNABassBase
    {
        public SNABass2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBass2 Variation
        {
            get { return (VarBass2)this[1]; }
            set
            {
                this[1] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarBass2> Variations
        {
            get { return Enum.GetValues(typeof(VarBass2)).Cast<VarBass2>(); }
        }
    }

    /// <remarks><i>
    /// INT: 043 Picked Bass<br/>
    /// ExSN3: 008 Picked Bass 2<br/>
    /// </i></remarks>
    public sealed class SNABass3 : SNABassBase
    {
        public SNABass3(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBass3 Variation
        {
            get { return (VarBass3)this[1]; }
            set
            {
                this[1] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarBass3> Variations
        {
            get { return Enum.GetValues(typeof(VarBass3)).Cast<VarBass3>(); }
        }
    }
}