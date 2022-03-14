using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models.Parameters
{
    #region Bass

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="NoiseLevel"/><br/>
    /// </remarks>
    public abstract class SNABassCommon : IntegraSNAMapper
    {
        public SNABassCommon(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (NoiseLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNABassCommon.NoiseLevel"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Staccato, Harmonics)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 041 Acoustic Bass<br/>
    /// INT: 044 Fretless Bass<br/>
    /// ExSN3: 006 Acoustic Bass 2<br/>
    /// </remarks>
    public sealed class SNABass1 : SNABassCommon
    {
        public SNABass1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBass1 Variation
        {
            get => (VarBass1)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNABassCommon.NoiseLevel"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Slap, Harmonics)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 042 Fingered Bass<br/>
    /// ExSN3: 007 Fingered Bass 2<br/>
    /// </remarks>
    public sealed class SNABass2 : SNABassCommon
    {
        public SNABass2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBass2 Variation
        {
            get => (VarBass2)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNABassCommon.NoiseLevel"/><br/>
    /// - [9] <see cref="Variation"/> (Off, BridgeMute, Harmonics)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 043 Picked Bass<br/>
    /// ExSN3: 008 Picked Bass 2<br/>
    /// </remarks>
    public sealed class SNABass3 : SNABassCommon
    {
        public SNABass3(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBass3 Variation
        {
            get => (VarBass3)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    #endregion

    #region Bell Mallet

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="Hardness"/><br/>
    /// - [1] <see cref="RollSpeed"/><br/>
    /// </remarks>
    public abstract class SNABellMalletCommon : IntegraSNAMapper
    {
        public SNABellMalletCommon(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int Hardness
        {
            get => this[0].Deserialize(64);
            set
            {
                if (Hardness != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int RollSpeed
        {
            get => this[1].Deserialize(64);
            set
            {
                if (RollSpeed != value)
                {
                    this[1] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNABellMalletCommon.Hardness"/><br/>
    /// - [1] <see cref="SNABellMalletCommon.RollSpeed"/><br/>
    /// - [2] <see cref="Variation"/> (Off, DeadStroke)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 024 Glockenspiel<br/>
    /// INT: 026 Marimba<br/>
    /// INT: 027 Xylophone<br/>
    /// INT: 028 Tubular Bells<br/>
    /// </remarks>
    public sealed class SNABellMallet1 : SNABellMalletCommon
    {
        public SNABellMallet1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBellMallet1 Variation
        {
            get => (VarBellMallet1)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNABellMalletCommon.Hardness"/><br/>
    /// - [1] <see cref="SNABellMalletCommon.RollSpeed"/><br/>
    /// - [2] <see cref="Variation"/> (Off, DeadStroke, Tremolo)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 025 Vibraphone<br/>
    /// </remarks>
    public sealed class SNABellMallet2 : SNABellMalletCommon
    {
        public SNABellMallet2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBellMallet2 Variation
        {
            get => (VarBellMallet2)this[3];
            set
            {
                if (Variation != value)
                {
                    this[3] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="ResonanceLevel"/><br/>
    /// - [1] <see cref="RollSpeed"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Mute, Tremolo)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// ExSN1: 001 Santoor<br/>
    /// ExSN1: 002 Yang Chin<br/>
    /// </remarks>
    public sealed class SNABellMallet3 : IntegraSNAMapper
    {
        public SNABellMallet3(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int ResonanceLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (ResonanceLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int RollSpeed
        {
            get => this[1].Deserialize(64);
            set
            {
                if (RollSpeed != value)
                {
                    this[1] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public VarBellMallet3 Variation
        {
            get => (VarBellMallet3)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    #endregion

    #region Brass

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="NoiseLevel"/><br/>
    /// - [2] <see cref="GrowlSens"/><br/>
    /// </remarks>
    public abstract class SNABrassCommon : IntegraSNAMapper
    {
        public SNABrassCommon(SuperNATURALAcousticToneCommon provider) : base(provider) { }
        
        public int NoiseLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (NoiseLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }

        public byte GrowlSens
        {
            get => this[2];
            set
            {
                if (this[2] != value)
                {
                    this[2] = value.Clamp();
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNABrassCommon.NoiseLevel"/><br/>
    /// - [2] <see cref="SNABrassCommon.GrowlSens"/> (Off, DeadStroke)<br/>
    /// - [9] <see cref="Variation"/> (Off, Staccato)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 061 French Horn<br/>
    /// ExSN5: 007 Tuba<br/>
    /// ExSN5: 010 French Horn 2<br/>
    /// ExSN5: 011 Mute French Horn<br/>
    /// </remarks>
    public sealed class SNABrass1 : SNABrassCommon
    {
        public SNABrass1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBrass1 Variation
        {
            get => (VarBrass1)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNABrassCommon.NoiseLevel"/><br/>
    /// - [2] <see cref="SNABrassCommon.GrowlSens"/> (Off, DeadStroke)<br/>
    /// - [9] <see cref="Variation"/> (Off, Staccato, Fall)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
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
    /// </remarks>
    public sealed class SNABrass2 : SNABrassCommon
    {
        public SNABrass2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBrass2 Variation
        {
            get => (VarBrass2)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="NoiseLevel"/><br/>
    /// - [1] <see cref="CrescendoDepth"/><br/>
    /// - [2] <see cref="GrowlSens"/> (Off, DeadStroke)<br/>
    /// - [9] <see cref="Variation"/> (Off, Staccato, Fall)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// ExSN5: 004 Mariachi Tp<br/>
    /// </remarks>
    public sealed class SNABrass3 : IntegraSNAMapper
    {
        public SNABrass3(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (NoiseLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }

        public int CrescendoDepth
        {
            get => this[1].Deserialize(64);
            set
            {
                if (CrescendoDepth != value)
                {
                    this[1] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }

        public byte GrowlSens
        {
            get => this[2];
            set
            {
                if (this[2] != value)
                {
                    this[2] = value.Clamp();
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }

        public VarBrass2 Variation
        {
            get => (VarBrass2)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    #endregion

    #region Guitar

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="NoiseLevel"/><br/>
    /// - [1] <see cref="StrumSpeed"/><br/>
    /// - [3] <see cref="StrumMode"/> (Off, On)<br/>
    /// </remarks>
    public abstract class SNAGuitarCommon : IntegraSNAMapper
    {
        public SNAGuitarCommon(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (NoiseLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int StrumSpeed
        {
            get => this[1].Deserialize(64);
            set
            {
                if (StrumSpeed != value)
                {
                    this[1] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraSwitch StrumMode
        {
            get => (IntegraSwitch)this[3];
            set
            {
                if (StrumMode != value)
                {
                    this[3] = (byte)(value);
                    NotifyPropertyChanged();
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [1] <see cref="StrumSpeed"/><br/>
    /// - [3] <see cref="StrumMode"/> (Off, On)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// ExSN4: 001 Ukulele<br/>
    /// </remarks>
    public sealed class SNAUkelele : IntegraSNAMapper
    {
        public SNAUkelele(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int StrumSpeed
        {
            get => this[1].Deserialize(64);
            set
            {
                if (StrumSpeed != value)
                {
                    this[1] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraSwitch StrumMode
        {
            get => (IntegraSwitch)this[3];
            set
            {
                if (StrumMode != value)
                {
                    this[3] = (byte)(value);
                    NotifyPropertyChanged();
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="NoiseLevel"/><br/>
    /// - [1] <see cref="TremoloSpeed"/><br/>
    /// - [3] <see cref="StrumMode"/> (Off, On)<br/>
    /// - [9] <see cref="Variation"/> (Off, Mute, Harmonics)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// ExSN4: 004 Mandolin<br/>
    /// </remarks>
    public sealed class SNAMandolin : IntegraSNAMapper
    {
        public SNAMandolin(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (NoiseLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int TremoloSpeed
        {
            get => this[1].Deserialize(64);
            set
            {
                if (TremoloSpeed != value)
                {
                    this[1] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraSwitch StrumMode
        {
            get => (IntegraSwitch)this[3];
            set
            {
                if (StrumMode != value)
                {
                    this[3] = (byte)(value);
                    NotifyPropertyChanged();
                }
            }
        }

        public VarGuitar1 Variation
        {
            get => (VarGuitar1)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAGuitarCommon.NoiseLevel"/><br/>
    /// - [1] <see cref="SNAGuitarCommon.StrumSpeed"/><br/>
    /// - [2] <see cref="SNAGuitarCommon.StrumMode"/> (Off, On)<br/>
    /// - [9] <see cref="Variation"/> (Off, Mute, Harmonics)<br/>
    /// - [12] <see cref="SubStringTune"/><br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// ExSN4: 003 12th Steel Gtr<br/>
    /// </remarks>
    public sealed class SNAGuitar4 : SNAGuitarCommon
    {
        public SNAGuitar4(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarGuitar1 Variation
        {
            get => (VarGuitar1)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }

        public int SubStringTune
        {
            get => this[12].Deserialize(64);
            set
            {
                if (SubStringTune != value)
                {
                    this[12] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAGuitarCommon.NoiseLevel"/><br/>
    /// - [1] <see cref="SNAGuitarCommon.StrumSpeed"/><br/>
    /// - [3] <see cref="SNAGuitarCommon.StrumMode"/> (Off, On)<br/>
    /// - [9] <see cref="Variation"/> (Off, Mute, Harmonics)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 034 Nylon Guitar<br/>
    /// INT: 036 SteelStr Guitar<br/>
    /// ExSN4: 002 Nylon Guitar 2<br/>
    /// ExSN4: 003 12th Steel Gtr<br/>
    /// ExSN4: 005 SteelFing Guitar<br/>
    /// ExSN4: 006 SteelStr Guitar2<br/>
    /// </remarks>
    public sealed class SNAGuitar1 : SNAGuitarCommon
    {
        public SNAGuitar1(SuperNATURALAcousticToneCommon provider) : base(provider) { }
        
        public VarGuitar1 Variation
        {
            get => (VarGuitar1)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAGuitarCommon.NoiseLevel"/><br/>
    /// - [1] <see cref="SNAGuitarCommon.StrumSpeed"/><br/>
    /// - [3] <see cref="SNAGuitarCommon.StrumMode"/> (Off, On)<br/>
    /// - [9] <see cref="Variation"/> (Off, Rasugueado, Harmonics)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 035 Flamenco Guitar<br/>
    /// </remarks>
    public sealed class SNAGuitar2 : SNAGuitarCommon
    {
        public SNAGuitar2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarGuitar2 Variation
        {
            get => (VarGuitar2)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAGuitarCommon.NoiseLevel"/><br/>
    /// - [1] <see cref="SNAGuitarCommon.StrumSpeed"/><br/>
    /// - [3] <see cref="SNAGuitarCommon.StrumMode"/> (Off, On)<br/>
    /// - [9] <see cref="Variation"/> (Off, FingerPicking, OctaveTone)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// ExSN3: 001 TC Guitar w/Fing<br/>
    /// ExSN3: 002 335Guitar w/Fing<br/>
    /// </remarks>
    public sealed class SNAGuitar3 : SNAGuitarCommon
    {
        public SNAGuitar3(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarGuitar3 Variation
        {
            get => (VarGuitar3)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    #region Electric

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAGuitarCommon.NoiseLevel"/><br/>
    /// - [1] <see cref="SNAGuitarCommon.StrumSpeed"/><br/>
    /// - [3] <see cref="SNAGuitarCommon.StrumMode"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Mute, Harmonics)<br/>
    /// - [10] <see cref="PickingHarmonics"/><br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 038 ST Guitar Half<br/>
    /// INT: 039 ST Guitar Front<br/>
    /// INT: 040 TC Guitar Rear<br/>
    /// ExSN3: 003 LP Guitar Rear<br/>
    /// ExSN3: 004 LP Guitar Front<br/>
    /// ExSN3: 005 335 Guitar Half<br/>
    /// </remarks>
    public class SNAElectricGuitar : SNAGuitarCommon
    {
        public SNAElectricGuitar(SuperNATURALAcousticToneCommon provider) : base(provider) { }
        
        public VarGuitar1 Variation
        {
            get => (VarGuitar1)this[9];
            set
            {
                if (Variation != value)
                {
                    this[4] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }

        public IntegraSwitch PickingHarmonics
        {
            get => (IntegraSwitch)this[10];
            set
            {
                if (PickingHarmonics != value)
                {
                    this[10] = (byte)(value);
                    NotifyPropertyChanged();
                }
            }
        }
    }

    #endregion

    #endregion

    #region Harmonica

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="NoiseLevel"/><br/>
    /// - [2] <see cref="GrowlSens"/><br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 032 Harmonica
    /// </remarks>
    public sealed class SNAHarmonica : IntegraSNAMapper
    {
        public SNAHarmonica(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (NoiseLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public byte GrowlSens
        {
            get => this[2];
            set
            {
                if (this[2] != value)
                {
                    this[2] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }
    }

    #endregion

    #region Harp

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [3] <see cref="GlissandoMode"/><br/>
    /// - [6] <see cref="PlayScale"/><br/>
    /// - [7] <see cref="ScaleKey"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Nail)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 051 Harp
    /// </remarks>
    public sealed class SNAHarp : IntegraSNAMapper
    {
        public SNAHarp(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public IntegraSwitch GlissandoMode
        {
            
            get => (IntegraSwitch)this[3];
            set
            {
                if (GlissandoMode != value)
                {
                    this[3] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public SNAPlayScale PlayScale
        {
            get => (SNAPlayScale)this[6];
            set
            {
                if (PlayScale != value)
                {
                    this[6] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraScaleKey ScaleKey
        {
            get => (IntegraScaleKey)this[7];
            set
            {
                if (ScaleKey != value)
                {
                    this[7] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public VarHarp Variation
        {
            get => (VarHarp)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    #endregion

    #region Keys

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="NoiseLevel"/><br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// <i>
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
            get => this[0].Deserialize(64);
            set
            {
                if (NoiseLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }
    }

    #endregion

    #region Organ

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="HarmonicBar16"/><br/>
    /// - [1] <see cref="HarmonicBar513"/><br/>
    /// - [2] <see cref="HarmonicBar8"/><br/>
    /// - [3] <see cref="HarmonicBar4"/><br/>
    /// - [4] <see cref="HarmonicBar223"/><br/>
    /// - [5] <see cref="HarmonicBar2"/><br/>
    /// - [6] <see cref="HarmonicBar135"/><br/>
    /// - [7] <see cref="HarmonicBar113"/><br/>
    /// - [8] <see cref="HarmonicBar1"/><br/>
    /// - [9] <see cref="PercSwitch"/><br/>
    /// - [10] <see cref="PercHarmonic"/><br/>
    /// - [11] <see cref="PercSlow"/><br/>
    /// - [12] <see cref="KeyOnClickLevel"/><br/>
    /// - [13] <see cref="KeyOffClickLevel"/><br/>
    /// - [14] <see cref="PercSoftLevel"/><br/>
    /// - [15] <see cref="PercNormLevel"/><br/>
    /// - [16] <see cref="PercSlowTime"/><br/>
    /// - [17] <see cref="PercFastTime"/><br/>
    /// - [18] <see cref="PercRechargeTime"/><br/>
    /// - [19] <see cref="PercHarmonicBarLevel"/><br/>
    /// - [20] <see cref="PercSoft"/><br/>
    /// - [21] <see cref="Leakage"/><br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// <i>
    /// INT: 029 TW Organ
    /// </i></remarks>
    public sealed class SNAOrgan : IntegraSNAMapper
    {
        public SNAOrgan(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public byte HarmonicBar16  { get => this[0]; set { if(this[0] != value){ this[0] = value.Clamp(0, 8); NotifyPropertyChanged();} } }
        public byte HarmonicBar513 { get => this[1]; set { if(this[1] != value){ this[1] = value.Clamp(0, 8); NotifyPropertyChanged();} } }
        public byte HarmonicBar8   { get => this[2]; set { if(this[2] != value){ this[2] = value.Clamp(0, 8); NotifyPropertyChanged();} } }
        public byte HarmonicBar4   { get => this[3]; set { if(this[3] != value){ this[3] = value.Clamp(0, 8); NotifyPropertyChanged();} } }
        public byte HarmonicBar223 { get => this[4]; set { if(this[4] != value){ this[4] = value.Clamp(0, 8); NotifyPropertyChanged();} } }
        public byte HarmonicBar2   { get => this[5]; set { if(this[5] != value){ this[5] = value.Clamp(0, 8); NotifyPropertyChanged();} } }
        public byte HarmonicBar135 { get => this[6]; set { if(this[6] != value){ this[6] = value.Clamp(0, 8); NotifyPropertyChanged();} } }
        public byte HarmonicBar113 { get => this[7]; set { if(this[7] != value){ this[7] = value.Clamp(0, 8); NotifyPropertyChanged();} } }
        public byte HarmonicBar1   { get => this[8]; set { if(this[8] != value){ this[8] = value.Clamp(0, 8); NotifyPropertyChanged(); } } }

        // 2B
        public IntegraSwitch PercSwitch
        {
            get => (IntegraSwitch)this[9];
            set
            {
                if (PercSwitch != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        // 2C
        public SNAPercussionHarmonic PercHarmonic
        {
            get => (SNAPercussionHarmonic)this[10];
            set
            {
                if (PercHarmonic != value)
                {
                    this[10] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        // 2D
        public SNAPercussionSlow PercSlow
        {
            get => (SNAPercussionSlow)this[11];
            set
            {
                if (PercSlow != value)
                {
                    this[11] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        // 2E
        public byte KeyOnClickLevel
        {
            get => this[12];
            set
            {
                if (this[12] != value)
                {
                    this[12] = value.Clamp(0, 31);
                    NotifyPropertyChanged();
                }
            }
        }

        // 2F
        public byte KeyOffClickLevel
        {
            get => this[13];
            set
            {
                if (this[13] != value)
                {
                    this[13] = value.Clamp(0, 31);
                    NotifyPropertyChanged();
                }
            }
        }
        
        // 30
        public byte PercSoftLevel
        {
            get => this[14];
            set
            {
                if (this[14] != value)
                {
                    this[14] = value.Clamp(0, 15);
                    NotifyPropertyChanged();
                }
            }
        }

        //31
        public byte PercNormLevel
        {
            get => this[15];
            set
            {
                if (this[15] != value)
                {
                    this[15] = value.Clamp(0, 15);
                    NotifyPropertyChanged();
                }
            }
        }

        // 32
        public byte PercSlowTime
        {
            get => this[16];
            set
            {
                if (this[16] != value)
                {
                    this[16] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        // 33
        public byte PercFastTime
        {
            get => this[17];
            set
            {
                if (this[17] != value)
                {
                    this[17] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        // 34
        public byte PercRechargeTime
        {
            get => this[18];
            set
            {
                if (this[18] != value)
                {
                    this[18] = value.Clamp(0, 15);
                    NotifyPropertyChanged();
                }
            }
        }

        //35
        public byte PercHarmonicBarLevel
        {
            get => this[19];
            set
            {
                if (this[19] != value)
                {
                    this[19] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public SNAPercussionSoft PercSoft
        {
            get => (SNAPercussionSoft)this[20];
            set
            {
                if (PercSoft != value)
                {
                    this[20] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public byte Leakage 
        { 
            get => this[21]; 
            set 
            {
                if (this[21] != value)
                {
                    this[21] = value.Clamp();
                    NotifyPropertyChanged();
                }
            } 
        }
    }

    #endregion

    #region Percussion

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [1] <see cref="RollSpeed"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Flam, AccentRoll)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 052 Timpani
    /// </remarks>
    public sealed class SNATimpani : IntegraSNAMapper
    {
        public SNATimpani(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int RollSpeed
        {
            get => this[1].Deserialize(64);
            set
            {
                if (RollSpeed != value)
                {
                    this[1] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public VarTimpani Variation
        {
            get => (VarTimpani)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="ResonanceLevel"/><br/>
    /// - [1] <see cref="RollSpeed"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Mute)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 077 Steel Drums
    /// </remarks>
    public sealed class SNASteelDrums : IntegraSNAMapper
    {
        public SNASteelDrums(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int ResonanceLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (ResonanceLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int RollSpeed
        {
            get => this[1].Deserialize(64);
            set
            {
                if (RollSpeed != value)
                {
                    this[1] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public VarSteelDrum Variation
        {
            get => (VarSteelDrum)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    #endregion

    #region Piano

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="StringResonance"/><br/>
    /// - [1] <see cref="KeyOffResonance"/><br/>
    /// - [2] <see cref="HammerNoise"/><br/>
    /// - [3] <see cref="StereoWidth"/><br/>
    /// - [4] <see cref="Nuance"/><br/>
    /// - [5] <see cref="ToneCharacter"/><br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT 001: Concert Grand<br/>
    /// INT 002: Grand Piano1<br/>
    /// INT 003: Grand Piano2<br/>
    /// INT 004: Grand Piano3<br/>
    /// INT 005: Mellow Piano<br/>
    /// INT 006: Bright Piano<br/>
    /// INT 007: Upright Piano<br/>
    /// INT 009: Honky-tonk<br/>
    /// </remarks>
    public class SNAPiano : IntegraSNAMapper
    {
        public SNAPiano(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public byte StringResonance
        {
            get => this[0];
            set
            {
                if (this[0] != value)
                {
                    this[0] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public byte KeyOffResonance
        {
            get => this[1];
            set
            {
                if (this[1] != value)
                {
                    this[1] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int HammerNoise
        {
            get => this[2].Deserialize(64);
            set
            {
                if (HammerNoise != value)
                {
                    this[2] = value.Serialize(64).Clamp(62, 66);
                    NotifyPropertyChanged();
                }
            }
        }

        public byte StereoWidth
        {
            get => this[3];
            set
            {
                if (this[3] != value)
                {
                    this[3] = value.Clamp(0, 63);
                    NotifyPropertyChanged();
                }
            }
        }

        public SNANuance Nuance
        {
            get => (SNANuance)this[4];
            set
            {
                if (Nuance != value)
                {
                    this[4] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int ToneCharacter
        {
            get => this[5].Deserialize(64);
            set
            {
                if (ToneCharacter != value)
                {
                    this[5] = value.Serialize(64).Clamp(59, 69);
                    NotifyPropertyChanged();
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="StringResonance"/><br/>
    /// - [1] <see cref="KeyOffResonance"/><br/>
    /// - [2] <see cref="HammerNoise"/><br/>
    /// - [3] <see cref="StereoWidth"/><br/>
    /// - [5] <see cref="ToneCharacter"/><br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT 008: Concert Mono<br/>
    /// </remarks>
    public sealed class SNAPianoMono : IntegraSNAMapper
    {
        public SNAPianoMono(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public byte StringResonance
        {
            get => this[0];
            set
            {
                if (this[0] != value)
                {
                    this[0] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public byte KeyOffResonance
        {
            get => this[1];
            set
            {
                if (this[1] != value)
                {
                    this[1] = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int HammerNoise
        {
            get => this[2].Deserialize(64);
            set
            {
                if (HammerNoise != value)
                {
                    this[2] = value.Serialize(64).Clamp(62, 66);
                    NotifyPropertyChanged();
                }
            }
        }

        public byte StereoWidth
        {
            get => this[3];
            set
            {
                if (this[3] != value)
                {
                    this[3] = value.Clamp(0, 63);
                    NotifyPropertyChanged();
                }
            }
        }

        public int ToneCharacter
        {
            get => this[5].Deserialize(64);
            set
            {
                if (ToneCharacter != value)
                {
                    this[5] = value.Serialize(64).Clamp(59, 69);
                    NotifyPropertyChanged();
                }
            }
        }
    }

    #endregion

    #region Pluck Stroke

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="ResonanceLevel"/><br/>
    /// - [4] <see cref="TamburaLevel"/><br/>
    /// - [5] <see cref="TamburaPitch"/><br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 073 Sitar
    /// ExSN1: 010 Sarangi
    /// </remarks>
    public sealed class SNASitar : IntegraSNAMapper
    {
        public SNASitar(SuperNATURALAcousticToneCommon provider) : base(provider) { }
        
        public int ResonanceLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (ResonanceLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int TamburaLevel
        {
            get => this[4].Deserialize(64);
            set
            {
                if (TamburaLevel != value)
                {
                    this[4] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int TamburaPitch
        {
            get => this[5].Deserialize(64);
            set
            {
                if (TamburaPitch != value)
                {
                    this[5] = value.Serialize(64).Clamp(52, 76);
                    NotifyPropertyChanged();
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="ResonanceLevel"/><br/>
    /// - [1] <see cref="BendDepth"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Strum, UpPicking, AutoBend)<br/>
    /// - [11] <see cref="BuzzKeySwitch"/><br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// ExSN1: 005 Tsugaru<br/>
    /// ExSN1: 007 Sansin<br/>
    /// </i></remarks>
    public sealed class SNAShamisen : IntegraSNAMapper
    {
        public SNAShamisen(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int ResonanceLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (ResonanceLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int BendDepth
        {
            get => this[1].Deserialize(64);
            set
            {
                if (BendDepth != value)
                {
                    this[1] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        
        public VarShamisen Variation
        {
            get => (VarShamisen)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)(value);
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }

        public IntegraSwitch BuzzKeySwitch
        {
            get => (IntegraSwitch)this[11];
            set
            {
                if (BuzzKeySwitch != value)
                {
                    this[11] = (byte)(value);
                    NotifyPropertyChanged();
                }
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [1] <see cref="TremoloSpeed"/><br/>
    /// - [3] <see cref="GlissandoMode"/><br/>
    /// - [6] <see cref="PlayScale"/><br/>
    /// - [7] <see cref="ScaleKey"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Tremolo, Ornament)<br/>
    /// - [11] <see cref="BuzzKeySwitch"/><br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// ExSN1: 007 Koto
    /// </remarks>
    public sealed class SNAKoto1 : IntegraSNAMapper
    {
        public SNAKoto1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int TremoloSpeed
        {
            get => this[1].Deserialize(64);
            set
            {
                if (TremoloSpeed != value)
                {
                    this[1] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraSwitch GlissandoMode
        {
            get => (IntegraSwitch)this[3];
            set
            {
                if (GlissandoMode != value)
                {
                    this[3] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public SNAKotoPlayScale PlayScale
        {
            get => (SNAKotoPlayScale)this[6];
            set
            {
                if (PlayScale != value)
                {
                    this[6] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraScaleKey ScaleKey
        {
            get => (IntegraScaleKey)this[7];
            set
            {
                if (ScaleKey != value)
                {
                    this[7] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        

        public VarKoto Variation
        {
            get => (VarKoto)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }

        public IntegraSwitch BuzzKeySwitch
        {
            get => (IntegraSwitch)this[11];
            set
            {
                if (BuzzKeySwitch != value)
                {
                    this[11] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="NoiseLevel"/><br/>
    /// - [1] <see cref="TremoloSpeed"/><br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// ExSN1: 008 Taishou Koto
    /// </remarks>  
    public sealed class SNAKoto2 : IntegraSNAMapper
    {
        public SNAKoto2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (NoiseLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int TremoloSpeed
        {
            get => this[1].Deserialize(64);
            set
            {
                if (TremoloSpeed != value)
                {
                    this[1] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="ResonanceLevel"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Buzz)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// ExSN1: 009 Kalimba
    /// </remarks>  
    public sealed class SNAKalimba : IntegraSNAMapper
    {
        public SNAKalimba(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int ResonanceLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (ResonanceLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public VarKalimba Variation
        {
            get => (VarKalimba)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)(value);
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    #endregion

    #region Strings

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="NoiseLevel"/><br/>
    /// </remarks>
    public abstract class SNAStringCommon : IntegraSNAMapper
    {
        public SNAStringCommon(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (NoiseLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAStringCommon.NoiseLevel"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Staccato, Pizzicato, Tremolo)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 045 Violin<br/>
    /// INT: 046 Violin 2<br/>
    /// INT: 047 Viola<br/>
    /// INT: 048 Cello<br/>
    /// INT: 049 Cello 2<br/>
    /// INT: 050 Contrabass<br/>
    /// </remarks>
    public sealed class SNAStrings1 : SNAStringCommon
    {
        public SNAStrings1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarStrings1 Variation
        {
            get => (VarStrings1)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAStringCommon.NoiseLevel"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Staccato, Ornament)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 076 Erhu
    /// </i></remarks>
    public sealed class SNAStrings2 : SNAStringCommon
    {
        public SNAStrings2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarStrings2 Variation
        {
            get => (VarStrings2)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [3] <see cref="HoldLegatoMode"/> (Off, On)<br/>
    /// - [9] <see cref="Variation"/> (Off, Staccato, Pizzicato, Tremolo)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 053 Strings<br/>
    /// INT: 054 Marcato Strings<br/>
    /// </i></remarks>
    public sealed class SNAStrings3 : IntegraSNAMapper
    {
        public SNAStrings3(SuperNATURALAcousticToneCommon provider) : base(provider) { }
        
        public IntegraSwitch HoldLegatoMode
        {
            get => (IntegraSwitch)this[3];
            set
            {
                if (HoldLegatoMode != value)
                {
                    this[3] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }
        public VarStrings1 Variation
        {
            get => (VarStrings1)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    #endregion

    #region Wind

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="NoiseLevel"/><br/>
    /// - [2] <see cref="GrowlSens"/><br/>
    /// <br/>
    /// </remarks>  
    public abstract class SNAWindCommon : IntegraSNAMapper
    {
        public SNAWindCommon(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int NoiseLevel
        {
            get => this[0].Deserialize(64);
            set
            {
                if (NoiseLevel != value)
                {
                    this[0] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }
        
        public byte GrowlSens
        {
            get => this[2];
            set
            {
                if (this[2] != value)
                {
                    this[2] = (byte)(value.Clamp());
                    NotifyPropertyChanged();
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAWindCommon.NoiseLevel"/><br/>
    /// - [2] <see cref="SNAWindCommon.GrowlSens"/> (Off, Buzz)<br/>
    /// - [6] <see cref="PlayScale"/><br/>
    /// - [7] <see cref="ScaleKey"/><br/>
    /// <br/>
    /// </remarks>  
    public abstract class SNAWindCommonExtended : SNAWindCommon
    {
        public SNAWindCommonExtended(SuperNATURALAcousticToneCommon provider) : base(provider) { }
        
        public SNAPlayScale PlayScale
        {
            get => (SNAPlayScale)this[6];
            set
            {
                if (PlayScale != value)
                {
                    this[6] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraScaleKey ScaleKey
        {
            get => (IntegraScaleKey)this[7];
            set
            {
                if (ScaleKey != value)
                {
                    this[7] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAWindCommon.NoiseLevel"/><br/>
    /// - [2] <see cref="SNAWindCommon.GrowlSens"/> (Off, Buzz)<br/>
    /// - [6] <see cref="SNAWindCommonExtended.PlayScale"/><br/>
    /// - [7] <see cref="SNAWindCommonExtended.ScaleKey"/><br/>
    /// - [8] <see cref="Glide"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Staccato)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 066 Oboe<br/>
    /// INT: 067 Bassoon<br/>
    /// INT: 068 Clarinet<br/>
    /// INT: 069 Piccolo<br/>
    /// INT: 070 Flute<br/>
    /// ExSN2: 005 English Horn<br/>
    /// ExSN2: 006 Bass Clarinet<br/>
    /// </remarks>
    public sealed class SNAWind1 : SNAWindCommonExtended
    {
        public SNAWind1(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public SNAPortaGliss Glide
        {
            get => (SNAPortaGliss)this[8];
            set
            {
                if (Glide != value)
                {
                    this[8] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public VarBrass1 Variation
        {
            get => (VarBrass1)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAWindCommon.NoiseLevel"/><br/>
    /// - [2] <see cref="SNAWindCommon.GrowlSens"/> (Off, Buzz)<br/>
    /// - [8] <see cref="Variation"/> (Off, Staccato, Flutter)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 071 Pan Flute<br/>
    /// </remarks>
    public sealed class SNAPanFlute : SNAWindCommon
    {
        public SNAPanFlute(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarWind2 Variation
        {
            get => (VarWind2)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [4] <see cref="DroneLevel"/><br/>
    /// - [5] <see cref="DronePitch"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Ornament)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 074 Uilleann Pipes<br/>
    /// INT: 075 Bag Pipes<br/>
    /// </remarks>
    public sealed class SNAPipes : IntegraSNAMapper
    {
        public SNAPipes(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public int DroneLevel
        {
            get => this[4].Deserialize(64);
            set
            {
                if (DroneLevel != value)
                {
                    this[4] = value.Serialize(64).Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        public int DronePitch
        {
            get => this[5].Deserialize(64);
            set
            {
                if (DronePitch != value)
                {
                    this[5] = value.Serialize(64).Clamp(52, 76);
                    NotifyPropertyChanged();
                }
            }
        }

        public VarPipes Variation
        {
            get => (VarPipes)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAWindCommon.NoiseLevel"/><br/>
    /// - [2] <see cref="SNAWindCommon.GrowlSens"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Staccato, Ornament)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 072 Shakuhachi<br/>
    /// ExSN1: 004 Ryuteki<br/>
    /// ExSN2: 012 Ocarina SopC<br/>
    /// ExSN2: 013 Ocarina SopF<br/>
    /// ExSN2: 014 Ocarina Alto<br/>
    /// ExSN2: 015 Ocarina Bass<br/>    
    /// </remarks>
    public sealed class SNAWind2 : SNAWindCommon
    {
        public SNAWind2(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarStrings2 Variation
        {
            get => (VarStrings2)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAWindCommon.NoiseLevel"/><br/>
    /// - [2] <see cref="SNAWindCommon.GrowlSens"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Cut, Ornament)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// ExSN1: 003 Tin Whistle<br/>
    /// </remarks>
    public sealed class SNAWhistle : SNAWindCommon
    {
        public SNAWhistle(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarWind3 Variation
        {
            get => (VarWind3)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAWindCommon.NoiseLevel"/><br/>
    /// - [2] <see cref="SNAWindCommon.GrowlSens"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Staccato)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// ExSN2: 008 Soprano Recorder<br/>
    /// ExSN2: 009 Alto Recorder<br/>
    /// ExSN2: 0010 Tenor Recorder<br/>
    /// ExSN2: 0011 Bass Recorder<br/>
    /// </remarks>
    public sealed class SNARecorder : SNAWindCommon
    {
        public SNARecorder(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public VarBrass1 Variation
        {
            get => (VarBrass1)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [0] <see cref="SNAWindCommon.NoiseLevel"/><br/>
    /// - [2] <see cref="SNAWindCommon.GrowlSens"/> (Off, Buzz)<br/>
    /// - [6] <see cref="SNAWindCommonExtended.PlayScale"/><br/>
    /// - [7] <see cref="SNAWindCommonExtended.ScaleKey"/><br/>
    /// - [8] <see cref="Glide"/><br/>
    /// - [9] <see cref="Variation"/> (Off, Staccato, Fall, SubTone)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 062 Soprano Sax 2<br/>
    /// INT: 063 Alto Sax 2<br/>
    /// INT: 064 Tenor Sax 2<br/>
    /// INT: 065 Baritone Sax 2<br/>
    /// ExSN2: 001 Soprano Sax<br/>
    /// ExSN2: 002 Alto Sax<br/>
    /// ExSN2: 003 Tenor Sax<br/>
    /// ExSN2: 004 Baritone Sax<br/>
    /// </remarks>
    public sealed class SNASax : SNAWindCommonExtended
    {
        public SNASax(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public SNAPortaGliss Glide
        {
            get => (SNAPortaGliss)this[8];
            set
            {
                if (Glide != value)
                {
                    this[8] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public VarWind4 Variation
        {
            get => (VarWind4)this[9];
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <b>Parameter Index</b><br/>
    /// - [3] <see cref="HoldLegatoMode"/><br/>
    /// - [9] <see cref="Variation"/> (Off, VoiceWoo)<br/>
    /// <br/>
    /// <b>Instruments</b><br/>
    /// INT: 055 London Choir<br/>
    /// INT: 056 Boys Choir
    /// </remarks>
    public sealed class SNAChoir : IntegraSNAMapper
    {
        public SNAChoir(SuperNATURALAcousticToneCommon provider) : base(provider) { }

        public IntegraSwitch HoldLegatoMode
        {
            get => (IntegraSwitch)this[3];
            set
            {
                if (HoldLegatoMode != value)
                {
                    this[3] = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }
        public VarChoir Variation
        {
            get { return (VarChoir)this[9]; }
            set
            {
                if (Variation != value)
                {
                    this[9] = (byte)value;
                    NotifyPropertyChanged();
                }
                else
                {
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }
    }

    #endregion
}
