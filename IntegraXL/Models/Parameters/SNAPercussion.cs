using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <remarks><i>
    /// INT: 052 Timpani
    /// </i></remarks>
    public sealed class SNATimpani : IntegraSNAMapper
    {
        public SNATimpani(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int RollSpeed
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public VarTimpani Variation
        {
            get { return (VarTimpani)this[1]; }
            set
            {
                this[1] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarTimpani> Variations
        {
            get { return Enum.GetValues(typeof(VarTimpani)).Cast<VarTimpani>(); }
        }

    }

    /// <remarks><i>
    /// INT: 077 Steel Drums
    /// </i></remarks>
    public sealed class SNASteelDrums : IntegraSNAMapper
    {
        public SNASteelDrums(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int ResonanceLevel
        {
            get => this[0] - 64;
            set
            {
                this[0] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public int RollSpeed
        {
            get => this[1] - 64;
            set
            {
                this[1] = (byte)(value.Clamp(-64, 63) + 64);
                NotifyPropertyChanged();
            }
        }

        public VarSteelDrum Variation
        {
            get { return (VarSteelDrum)this[2]; }
            set
            {
                this[2] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarSteelDrum> Variations
        {
            get { return Enum.GetValues(typeof(VarSteelDrum)).Cast<VarSteelDrum>(); }
        }
    }

    ///// <remarks><i>
    ///// INT: 045 Violin<br/>
    ///// INT: 046 Violin 2<br/>
    ///// INT: 047 Viola<br/>
    ///// INT: 048 Cello<br/>
    ///// INT: 049 Cello 2<br/>
    ///// INT: 050 Contrabass<br/>
    ///// </i></remarks>
    //public sealed class SNAStrings1 : SNAStringsBase
    //{
    //    public SNAStrings1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

    //    public VarStrings1 Variation
    //    {
    //        get { return (VarStrings1)this[1]; }
    //        set
    //        {
    //            this[1] = (byte)value;
    //            NotifyPropertyChanged();
    //        }
    //    }

    //    public IEnumerable<VarStrings1> Variations
    //    {
    //        get { return Enum.GetValues(typeof(VarStrings1)).Cast<VarStrings1>(); }
    //    }

    //}

    ///// <remarks><i>
    ///// INT: 076 Erhu
    ///// </i></remarks>
    //public sealed class SNAStrings2 : SNAStringsBase
    //{
    //    public SNAStrings2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

    //    public VarStrings2 Variation
    //    {
    //        get { return (VarStrings2)this[1]; }
    //        set
    //        {
    //            this[1] = (byte)value;
    //            NotifyPropertyChanged();
    //        }
    //    }

    //    public IEnumerable<VarStrings2> Variations
    //    {
    //        get { return Enum.GetValues(typeof(VarStrings2)).Cast<VarStrings2>(); }
    //    }

    //}

    ///// <remarks><i>
    ///// INT: 053 Strings<br/>
    ///// INT: 054 Marcato Strings<br/>
    ///// </i></remarks>
    //public sealed class SNAStrings3 : IntegraSNAParameter
    //{
    //    public SNAStrings3(SuperNATURALAcousticToneCommon provider) : base(provider) { }

    //    public IntegraSwitch HoldLegatoMode
    //    {
    //        get => (IntegraSwitch)this[0];
    //        set
    //        {
    //            this[0] = (byte)value;
    //            NotifyPropertyChanged();
    //        }
    //    }
    //    public VarStrings1 Variation
    //    {
    //        get { return (VarStrings1)this[1]; }
    //        set
    //        {
    //            this[1] = (byte)value;
    //            NotifyPropertyChanged();
    //        }
    //    }

    //    public IEnumerable<IntegraSwitch> HoldLegatoModes => Enum.GetValues(typeof(IntegraSwitch)).Cast<IntegraSwitch>();
    //    public IEnumerable<VarStrings1>   Variations      => Enum.GetValues(typeof(VarStrings1)).Cast<VarStrings1>();
    //}

}