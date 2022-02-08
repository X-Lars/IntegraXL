using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    #region Bass

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
    public sealed class SNABassStaccatoHarmonics : SNABassBase
    {
        public SNABassStaccatoHarmonics(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    public sealed class SNABassSlapHarmonics : SNABassBase
    {
        public SNABassSlapHarmonics(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    public sealed class SNABassBridgeMuteHarmonics : SNABassBase
    {
        public SNABassBridgeMuteHarmonics(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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

    #endregion

    #region Bell Mallet

    /// <remarks><i>
    /// INT: 024 Glockenspiel<br/>
    /// INT: 025 Vibraphone<br/>
    /// INT: 026 Marimba<br/>
    /// INT: 027 Xylophone<br/>
    /// INT: 028 Tubular Bells<br/>
    /// ExSN1: 001 Santoor<br/>
    /// ExSN1: 002 Yang Chin<br/>
    /// </i></remarks>
    public abstract class SNABellMallet : IntegraSNAMapper
    {
        public SNABellMallet(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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

    /// <remarks><i>
    /// INT: 024 Glockenspiel<br/>
    /// INT: 026 Marimba<br/>
    /// INT: 027 Xylophone<br/>
    /// INT: 028 Tubular Bells<br/>
    /// </i></remarks>
    public sealed class SNABellMalletDeadStroke : SNABellMallet
    {
        public SNABellMalletDeadStroke(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    public sealed class SNABellMalletDeadStrokTremoloSwitch : SNABellMallet
    {
        public SNABellMalletDeadStrokTremoloSwitch(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    public sealed class SNABellMalletMuteTremolo : SNABellMallet
    {
        public SNABellMalletMuteTremolo(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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

    #region Brass

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
    public abstract class SNABrassBase : IntegraSNAMapper
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
    /// INT: 061 French Horn<br/>
    /// ExSN5: 007 Tuba<br/>
    /// ExSN5: 0010 French Horn 2<br/>
    /// ExSN5: 0011 Mute French Horn<br/>
    /// </i></remarks>
    public sealed class SNABrassStaccato : SNABrassBase
    {
        public SNABrassStaccato(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    public sealed class SNABrassStaccatoFall : SNABrassBase
    {
        public SNABrassStaccatoFall(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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

    #endregion

    #region Guitar

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
    public sealed class SNAGuitarMuteHarmonics : SNAGuitarBaseExt
    {
        public SNAGuitarMuteHarmonics(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    public sealed class SNAFlamencoGuitar : SNAGuitarBaseExt
    {
        public SNAFlamencoGuitar(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    public sealed class SNAGuitarFingerPickingOctaveTone : SNAGuitarBaseExt
    {
        public SNAGuitarFingerPickingOctaveTone(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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

    #region Electric

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
    public sealed class SNAElectricGuitarMuteHarmonics : SNAElectricGuitarBase
    {
        public SNAElectricGuitarMuteHarmonics(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    public sealed class SNAElectricGuitarFingerPickingOctaveTone : SNAElectricGuitarBase
    {
        public SNAElectricGuitarFingerPickingOctaveTone(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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

    #endregion

    #endregion

    #region Harmonica

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

    #endregion

    #region Harp

    /// <remarks><i>
    /// INT: 051 Harp
    /// </i></remarks>
    public sealed class SNAHarp : IntegraSNAMapper
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

        public IEnumerable<SNAPlayScale> PlayScales => Enum.GetValues(typeof(SNAPlayScale)).Cast<SNAPlayScale>();
        public IEnumerable<IntegraScaleKey> ScaleKeys => Enum.GetValues(typeof(IntegraScaleKey)).Cast<IntegraScaleKey>();
        public IEnumerable<VarHarp> Variations => Enum.GetValues(typeof(VarHarp)).Cast<VarHarp>();
    }

    #endregion

    #region Keys

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
    public sealed class SNAKeys : IntegraSNAMapper
    {
        public SNAKeys(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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

    #endregion

    #region Organ

    /// <summary>
    /// 
    /// </summary>
    /// <remarks><i>
    /// INT: 029 TW Organ
    /// </i></remarks>
    public sealed class SNAOrgan : IntegraSNAMapper
    {
        public SNAOrgan(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int HarmonicBar16 { get => this[0]; set { this[0] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar513 { get => this[1]; set { this[1] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar8 { get => this[2]; set { this[2] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar4 { get => this[3]; set { this[3] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar223 { get => this[4]; set { this[4] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar2 { get => this[5]; set { this[5] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar135 { get => this[6]; set { this[6] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar113 { get => this[7]; set { this[7] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }
        public int HarmonicBar1 { get => this[8]; set { this[8] = (byte)value.Clamp(0, 8); NotifyPropertyChanged(); } }

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

    #endregion

    #region Percussion

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

    #endregion

    #region Piano

    /// <remarks><i>
    /// INT 001: Concert Grand<br/>
    /// INT 002: Grand Piano1<br/>
    /// INT 003: Grand Piano2<br/>
    /// INT 004: Grand Piano3<br/>
    /// INT 005: Mellow Piano<br/>
    /// INT 006: Bright Piano<br/>
    /// INT 007: Upright Piano<br/>
    /// INT 008: Concert Mono<br/>
    /// INT 009: Honky-tonk<br/>
    /// </i></remarks>
    public sealed class SNAPiano : IntegraSNAMapper
    {
        public SNAPiano(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int StringResonance
        {
            get { return this[0]; }
            set
            {
                this[0] = (byte)value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int KeyOffResonance
        {
            get { return this[1]; }
            set
            {
                this[1] = (byte)value.Clamp();
                NotifyPropertyChanged();
            }
        }

        public int HammerNoise
        {
            get { return this[2] - 64; }
            set
            {
                this[2] = (byte)(value.Clamp(-2, 2) + 64);
                NotifyPropertyChanged();
            }
        }
        public int StereoWidth
        {
            get { return this[3]; }
            set
            {
                this[3] = (byte)(value.Clamp(0, 63));
                NotifyPropertyChanged();
            }
        }

        public SNANuance Nuance
        {
            get { return (SNANuance)this[4]; }
            set
            {
                this[4] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public int ToneCharacter
        {
            get { return this[5] - 64; }
            set
            {
                this[5] = (byte)(value.Clamp(-5, 5) + 64);
                NotifyPropertyChanged();
            }
        }

        public static IEnumerable<SNANuance> Nuances
        {
            get { return Enum.GetValues(typeof(SNANuance)).Cast<SNANuance>(); }
        }
    }

    #endregion

    #region Pluck Stroke

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

        public IEnumerable<IntegraSwitch> Switch => Enum.GetValues(typeof(IntegraSwitch)).Cast<IntegraSwitch>();
        public IEnumerable<VarShamisen> Variations => Enum.GetValues(typeof(VarShamisen)).Cast<VarShamisen>();
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

        public IEnumerable<SNAPlayScale> PlayScales => Enum.GetValues(typeof(SNAPlayScale)).Cast<SNAPlayScale>();
        public IEnumerable<IntegraScaleKey> ScaleKeys => Enum.GetValues(typeof(IntegraScaleKey)).Cast<IntegraScaleKey>();
        public IEnumerable<VarKoto> Variations => Enum.GetValues(typeof(VarKoto)).Cast<VarKoto>();
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

    #endregion

    #region Strings

    /// <remarks><i>
    /// INT: 045 Violin<br/>
    /// INT: 046 Violin 2<br/>
    /// INT: 047 Viola<br/>
    /// INT: 048 Cello<br/>
    /// INT: 049 Cello 2<br/>
    /// INT: 050 Contrabass<br/>
    /// </i></remarks>
    public abstract class SNAStringsBase : IntegraSNAMapper
    {
        public SNAStringsBase(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    /// INT: 045 Violin<br/>
    /// INT: 046 Violin 2<br/>
    /// INT: 047 Viola<br/>
    /// INT: 048 Cello<br/>
    /// INT: 049 Cello 2<br/>
    /// INT: 050 Contrabass<br/>
    /// </i></remarks>
    public sealed class SNAStringsStaccatoPizzicatoTremolo : SNAStringsBase
    {
        public SNAStringsStaccatoPizzicatoTremolo(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarStrings1 Variation
        {
            get { return (VarStrings1)this[1]; }
            set
            {
                this[1] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarStrings1> Variations
        {
            get { return Enum.GetValues(typeof(VarStrings1)).Cast<VarStrings1>(); }
        }

    }

    /// <remarks><i>
    /// INT: 076 Erhu
    /// </i></remarks>
    public sealed class SNAStringsStaccatoOrnament : SNAStringsBase
    {
        public SNAStringsStaccatoOrnament(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarStrings2 Variation
        {
            get { return (VarStrings2)this[1]; }
            set
            {
                this[1] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<VarStrings2> Variations
        {
            get { return Enum.GetValues(typeof(VarStrings2)).Cast<VarStrings2>(); }
        }

    }

    /// <remarks><i>
    /// INT: 053 Strings<br/>
    /// INT: 054 Marcato Strings<br/>
    /// </i></remarks>
    public sealed class SNAStringsExt : IntegraSNAMapper
    {
        public SNAStringsExt(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public IntegraSwitch HoldLegatoMode
        {
            get => (IntegraSwitch)this[0];
            set
            {
                this[0] = (byte)value;
                NotifyPropertyChanged();
            }
        }
        public VarStrings1 Variation
        {
            get { return (VarStrings1)this[1]; }
            set
            {
                this[1] = (byte)value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<IntegraSwitch> HoldLegatoModes => Enum.GetValues(typeof(IntegraSwitch)).Cast<IntegraSwitch>();
        public IEnumerable<VarStrings1> Variations => Enum.GetValues(typeof(VarStrings1)).Cast<VarStrings1>();
    }

    #endregion

    #region Wind

    public abstract class SNAWindBase : IntegraSNAMapper
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
    public sealed class SNAWindStaccatoExt : SNAWindBaseExt
    {
        public SNAWindStaccatoExt(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    public sealed class SNAPanFlute : SNAWindBaseExt
    {
        public SNAPanFlute(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    public sealed class SNAPipes : IntegraSNAMapper
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
    public sealed class SNAWindStaccatoOrnament : SNAWindBase
    {
        public SNAWindStaccatoOrnament(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    public sealed class SNATinWhistle : SNAWindBase
    {
        public SNATinWhistle(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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
    public sealed class SNAWindStaccato : SNAWindBase
    {
        public SNAWindStaccato(SuperNATURALAcousticToneCommon provider) : base(provider) { }

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

        public IEnumerable<SNAPortaGliss> Portas => Enum.GetValues(typeof(SNAPortaGliss)).Cast<SNAPortaGliss>();
        public IEnumerable<VarWind4> Variations => Enum.GetValues(typeof(VarWind4)).Cast<VarWind4>();
    }

    /// <remarks><i>
    /// INT: 055 London Choir<br/>
    /// INT: 056 Boys Choir
    /// </i></remarks>
    public sealed class SNAChoir : IntegraSNAMapper
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

    #endregion
}
