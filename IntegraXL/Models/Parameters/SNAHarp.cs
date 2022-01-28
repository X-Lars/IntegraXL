using IntegraXL.Core;

namespace IntegraXL.Models.Parameters
{
    /// <remarks><i>
    /// INT: 051 Harp
    /// </i></remarks>
    public sealed class SNAHarp : IntegraSNAParameter
    {
        public SNAHarp(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public IntegraSwitch GlissandoMode
        {
            get => (IntegraSwitch)this[0];
            set
            {
                this[0] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public SNAPlayScale PlayScale
        {
            get => (SNAPlayScale)this[1];
            set
            {
                this[1] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IntegraScaleKey ScaleKey
        {
            get => (IntegraScaleKey)this[2];
            set
            {
                this[2] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public VarHarp Variation
        {
            get => (VarHarp)this[3];
            set
            {
                this[3] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<SNAPlayScale>    PlayScales => Enum.GetValues(typeof(SNAPlayScale)).Cast<SNAPlayScale>();
        public IEnumerable<IntegraScaleKey> ScaleKeys  => Enum.GetValues(typeof(IntegraScaleKey)).Cast<IntegraScaleKey>();
        public IEnumerable<VarHarp>         Variations => Enum.GetValues(typeof(VarHarp)).Cast<VarHarp>();
    }
}
