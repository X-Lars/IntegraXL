using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks><i>
    /// INT: 029 TW Organ
    /// </i></remarks>
    public sealed class SNAOrgan : IntegraSNAMapper
    {
        public SNAOrgan(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int HarmonicBar16  { get => this[0]; set { this[0] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar513 { get => this[1]; set { this[1] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar8   { get => this[2]; set { this[2] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar4   { get => this[3]; set { this[3] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar223 { get => this[4]; set { this[4] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar2   { get => this[5]; set { this[5] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar135 { get => this[6]; set { this[6] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar113 { get => this[7]; set { this[7] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar1   { get => this[8]; set { this[8] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }

        public int Leakage { get => this[9]; set { this[9] = (byte)value.Clamp(); NotifyPropertyChanged(); } }
        public IntegraSwitch PercSoftSwitch
        {
            get { return (IntegraSwitch)this[10]; }
            set 
            { 
                this[10] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public SNAPercussionSoft PercSoft
        {
            get { return (SNAPercussionSoft)this[11]; }
            set
            {
                this[11] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public int PercSoftlevel
        {
            get { return this[12]; }
            set
            {
                this[12] = (byte)value.Clamp(0, 15);
                NotifyPropertyChanged();
            }
        }

        public int PercNormlevel
        {
            get { return this[13]; }
            set
            {
                this[13] = (byte)value.Clamp(0, 15);
                NotifyPropertyChanged();
            }
        }

        public SNAPercussionSlow PercSlow
        {
            get { return (SNAPercussionSlow)this[14]; }
            set
            {
                this[14] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public int PercSlowTime
        {
            get { return this[15]; }
            set
            {
                this[15] = (byte)value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int PercFastTime
        {
            get { return this[16]; }
            set
            {
                this[16] = (byte)value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public SNAPercussionHarmonic PercHarmonic
        {
            get { return (SNAPercussionHarmonic)this[17]; }
            set
            {
                this[17] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public int PercRechargeTime
        {
            get { return this[18]; }
            set
            {
                this[18] = (byte)value.Clamp(0, 15);
                NotifyPropertyChanged();
            }
        }

        public int PercHarmonicBarLevl
        {
            get { return this[19]; }
            set
            {
                this[19] = (byte)value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int KeyOnClickLevel
        {
            get { return this[20]; }
            set
            {
                this[20] = (byte)value.Clamp(0, 31);
                NotifyPropertyChanged();
            }
        }

        public int KeyOffClickLevel
        {
            get { return this[21]; }
            set
            {
                this[21] = (byte)value.Clamp(0, 31);
                NotifyPropertyChanged();
            }
        }

    }
}
