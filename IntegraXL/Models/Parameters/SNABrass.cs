using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks><i>
    /// INT: 057 Trumpet<br/>
    /// INT: 058 Trombone<br/>
    /// INT: 059 Tb2 CupMute<br/>
    /// INT: 060 Mute Trumpet<br/>
    /// INT: 061 French Horn<br/>
    /// ExSN5: 001 Classical Trumpet<br/>
    /// ExSN5: 002 Frugal Horn<br/>
    /// ExSN5: 003 Trumpet 2<br/>
    /// ExSN5: 004 Mariachi Tp<br/>
    /// ExSN5: 005 Trombone 2<br/>
    /// ExSN5: 006 Bass Trombone<br/>
    /// ExSN5: 007 Tuba<br/>
    /// ExSN5: 008 StraightMute Tp<br/>
    /// ExSN5: 009 Cup Mute Trumpet<br/>
    /// ExSN5: 0010 French Horn 2<br/>
    /// ExSN5: 0011 Mute French Horn<br/>
    /// </i></remarks>
    public abstract class SNABrassBase : IntegraSNAParameter
    {
        public SNABrassBase(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public int CresendoDepth
        {
            get => this[1] - 64;
            set
            {
                this[1] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public int GrowlSens
        {
            get => this[2];
            set
            {
                this[2] = (byte)(value.Clamp());
                NotifyPropertyChanged();
            }
        }
    }


    /// <remarks><i>
    /// INT: 057 Trumpet<br/>
    /// INT: 058 Trombone<br/>
    /// INT: 059 Tb2 CupMute<br/>
    /// INT: 060 Mute Trumpet<br/>
    /// ExSN5: 001 Classical Trumpet<br/>
    /// ExSN5: 002 Frugal Horn<br/>
    /// ExSN5: 003 Trumpet 2<br/>
    /// ExSN5: 004 Mariachi Tp<br/>
    /// ExSN5: 005 Trombone 2<br/>
    /// ExSN5: 006 Bass Trombone<br/>
    /// ExSN5: 008 StraightMute Tp<br/>
    /// ExSN5: 009 Cup Mute Trumpet<br/>
    /// </i></remarks>
    public sealed class SNABrass2 : SNABrassBase
    {
        public SNABrass2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBrass2 Variation
        {
            get { return (VarBrass2)this[3]; }
            set
            {
                this[3] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarBrass2> Variations
        {
            get { return Enum.GetValues(typeof(VarBrass2)).Cast<VarBrass2>(); }
        }

    }

    /// <remarks><i>
    /// INT: 061 French Horn<br/>
    /// ExSN5: 007 Tuba<br/>
    /// ExSN5: 0010 French Horn 2<br/>
    /// ExSN5: 0011 Mute French Horn<br/>
    /// </i></remarks>
    public sealed class SNABrass1 : SNABrassBase
    {
        public SNABrass1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBrass1 Variation
        {
            get { return (VarBrass1)this[3]; }
            set
            {
                this[3] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarBrass1> Variations
        {
            get { return Enum.GetValues(typeof(VarBrass1)).Cast<VarBrass1>(); }
        }
    }

}