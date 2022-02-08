using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks><i>
    /// INT: 034 Nylon Guitar<br/>
    /// INT: 035 Flamenco Guitar<br/>
    /// INT: 036 SteelStr Guitar<br/>
    /// ExSN3: 001 TC Guitar w/Fing<br/>
    /// ExSN3: 002 335Guitar w/Fing<br/>
    /// ExSN4: 001 Ukulele<br/>
    /// ExSN4: 002 Nylon Guitar 2<br/>
    /// ExSN4: 003 12th Steel Gtr<br/>
    /// ExSN4: 005 SteelFing Guitar<br/>
    /// ExSN4: 006 SteelStr Guitar2<br/>
    /// </i></remarks>
    public abstract class SNAGuitarBase : IntegraSNAMapper
    {
        public SNAGuitarBase(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp() + 64);
                NotifyPropertyChanged();
            }
        }

        public int StrumSpeed
        {
            get => this[1] - 64;
            set
            {
                this[1] = (byte)(value.Clamp() + 64);
                NotifyPropertyChanged();
            }
        }

        public IntegraSwitch StrumMode
        {
            get => (IntegraSwitch)this[2];
            set
            {
                this[2] = (byte)(value);
                NotifyPropertyChanged();
            }
        }
       
        public IEnumerable<IntegraSwitch> StrumModes
        {
            get { return Enum.GetValues(typeof(IntegraSwitch)).Cast<IntegraSwitch>(); }
        }
    }

    /// <remarks><i>
    /// ExSN4: 001 Ukulele<br/>
    /// </i></remarks>
    public sealed class SNAUkele : SNAGuitarBaseExt
    {
        public SNAUkele(SuperNATURALAcousticToneCommon provider) : base(provider) { }
    }

    /// <remarks><i>
    /// ExSN4: 004 Mandolin<br/>
    /// </i></remarks>
    public sealed class SNAMandolin : IntegraSNAMapper
    {
        public SNAMandolin(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp() + 64);
                NotifyPropertyChanged();
            }
        }

        public int TremoloSpeed
        {
            get => this[1] - 64;
            set
            {
                this[1] = (byte)(value.Clamp() + 64);
                NotifyPropertyChanged();
            }
        }

        public IntegraSwitch StrumMode
        {
            get => (IntegraSwitch)this[2];
            set
            {
                this[2] = (byte)(value);
                NotifyPropertyChanged();
            }
        }

        public VarGuitar1 Variation
        {
            get { return (VarGuitar1)this[3]; }
            set
            {
                this[3] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<IntegraSwitch> StrumModes
        {
            get { return Enum.GetValues(typeof(IntegraSwitch)).Cast<IntegraSwitch>(); }
        }

        public IEnumerable<VarGuitar1> Variations
        {
            get { return Enum.GetValues(typeof(VarGuitar1)).Cast<VarGuitar1>(); }
        }
    }

    /// <remarks><i>
    /// INT: 034 Nylon Guitar<br/>
    /// INT: 035 Flamenco Guitar<br/>
    /// INT: 036 SteelStr Guitar<br/>
    /// ExSN3: 001 TC Guitar w/Fing<br/>
    /// ExSN3: 002 335Guitar w/Fing<br/>
    /// ExSN4: 002 Nylon Guitar 2<br/>
    /// ExSN4: 003 12th Steel Gtr<br/>
    /// ExSN4: 005 SteelFing Guitar<br/>
    /// ExSN4: 006 SteelStr Guitar2<br/>
    /// </i></remarks>
    public abstract class SNAGuitarBaseExt : SNAGuitarBase
    {
        protected SNAGuitarBaseExt(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int SubStringTune
        {
            get => this[3] - 64;
            set
            {
                this[3] = (byte)(value.Clamp() + 64);
                NotifyPropertyChanged();
            }
        }
    }

    /// <remarks><i>
    /// INT: 034 Nylon Guitar<br/>
    /// INT: 036 SteelStr Guitar<br/>
    /// ExSN4: 002 Nylon Guitar 2<br/>
    /// ExSN4: 003 12th Steel Gtr<br/>
    /// ExSN4: 006 SteelStr Guitar2<br/>
    /// </i></remarks>
    public sealed class SNAGuitar1 : SNAGuitarBaseExt
    {
        public SNAGuitar1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarGuitar1 Variation
        {
            get { return (VarGuitar1)this[4]; }
            set
            {
                this[4] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarGuitar1> Variations
        {
            get { return Enum.GetValues(typeof(VarGuitar1)).Cast<VarGuitar1>(); }
        }

    }

    /// <remarks><i>
    /// INT: 035 Flamenco Guitar<br/>
    /// </i></remarks>
    public sealed class SNAGuitar2 : SNAGuitarBaseExt
    {
        public SNAGuitar2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarGuitar2 Variation
        {
            get { return (VarGuitar2)this[4]; }
            set
            {
                this[4] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarGuitar2> Variations
        {
            get { return Enum.GetValues(typeof(VarGuitar2)).Cast<VarGuitar2>(); }
        }

    }

    /// <remarks><i>
    /// ExSN3: 001 TC Guitar w/Fing<br/>
    /// ExSN3: 002 335Guitar w/Fing<br/>
    /// ExSN4: 005 SteelFing Guitar<br/>
    /// ExSN4: 006 SteelStr Guitar2<br/>
    /// </i></remarks>
    public sealed class SNAGuitar3 : SNAGuitarBaseExt
    {
        public SNAGuitar3(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarGuitar3 Variation
        {
            get { return (VarGuitar3)this[4]; }
            set
            {
                this[4] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarGuitar3> Variations
        {
            get { return Enum.GetValues(typeof(VarGuitar3)).Cast<VarGuitar3>(); }
        }

    }

    /// <remarks><i>
    /// INT: 037 Jazz Guitar<br/>
    /// INT: 038 ST Guitar Half<br/>
    /// INT: 039 ST Guitar Front<br/>
    /// INT: 040 TC Guitar Rear<br/>
    /// ExSN3: 003 LP Guitar Rear<br/>
    /// ExSN3: 004 LP Guitar Front<br/>
    /// ExSN3: 005 335 Guitar Half<br/>
    /// </i></remarks>
    public abstract class SNAElectricGuitarBase : SNAGuitarBase
    {
        protected SNAElectricGuitarBase(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public IntegraSwitch PickingHarmonics
        {
            get => (IntegraSwitch)this[3];
            set
            {
                this[3] = (byte)(value);
                NotifyPropertyChanged();
            }
        }
    }

    /// <remarks><i>
    /// INT: 038 ST Guitar Half<br/>
    /// INT: 039 ST Guitar Front<br/>
    /// INT: 040 TC Guitar Rear<br/>
    /// ExSN3: 003 LP Guitar Rear<br/>
    /// ExSN3: 004 LP Guitar Front<br/>
    /// ExSN3: 005 335 Guitar Half<br/>
    /// </i>Off | Mute | Harmonics</remarks>
    public sealed class SNAElectricGuitar1 : SNAElectricGuitarBase
    {
        public SNAElectricGuitar1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarGuitar1 Variation
        {
            get { return (VarGuitar1)this[4]; }
            set
            {
                this[4] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarGuitar1> Variations
        {
            get { return Enum.GetValues(typeof(VarGuitar1)).Cast<VarGuitar1>(); }
        }

    }

    /// <remarks><i>
    /// INT: 037 Jazz Guitar<br/>
    /// </i></remarks>
    public sealed class SNAElectricGuitar2 : SNAElectricGuitarBase
    {
        public SNAElectricGuitar2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarGuitar3 Variation
        {
            get { return (VarGuitar3)this[4]; }
            set
            {
                this[4] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarGuitar3> Variations
        {
            get { return Enum.GetValues(typeof(VarGuitar3)).Cast<VarGuitar3>(); }
        }

    }
}