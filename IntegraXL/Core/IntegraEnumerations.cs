using IntegraXL.Common;
using System.ComponentModel;
using System.Diagnostics;

namespace IntegraXL.Core
{
    #region Custom

    public class IntegraPreDelay : Enumeration
    {
        public static new List<string> Values
        {
            get
            {
                List<string> values = new();

                for (int i = 0; i < 50; i++)
                {
                    values.Add((((double)i / 10)).ToString("0.0"));
                }

                for (int i = 0; i < 10; i++)
                {
                    values.Add((5.0 + i * 0.5).ToString("0.0"));
                }

                for (int i = 0; i < 40; i++)
                {
                    values.Add((10.0 + i).ToString("0.0"));
                }
                for (int i = 0; i < 26; i++)
                {
                    values.Add((50.0 + (i * 2)).ToString("0.0"));
                }

                return values;
            }
        }
    }

    public class IntegraVoiceReserves : Enumeration
    {
        public new static List<string> Values
        {
            get
            {
                List<string> values = new();

                for (int i = 0; i < 64; i++)
                {
                    values.Add(i.ToString());
                }

                values.Add("Full");

                return values;
            }
        }
    }

    public class IntegraPartSelections : Enumeration
    {
        public new static List<string> Values
        {
            get
            {
                List<string> values = new();

                values.Add("Off");

                for (int i = 1; i < 17; i++)
                {
                    values.Add($"Part {i}");
                }

                return values;
            }
        }
    }

    public class IntegraExtendedRate : Enumeration
    {
        public new static List<string> Values
        {
            get
            {
                List<string> values = new();

                for (int i = 0; i < 128; i++)
                {
                    values.Add($"{i}");
                }

                for (int i = 0; i < 22; i++)
                {
                    IntegraNoteRates rate = (IntegraNoteRates)i;
                    object? value = TypeDescriptor.GetConverter(rate).ConvertTo(rate, typeof(string));
                    Debug.Assert(value != null);
                    values.Add((string)value);
                }

                return values;
            }
        }
    }

    public class IntegraPan : Enumeration
    {
        public new static List<string> Values
        {
            get
            {
                List<string> values = new();

                for (int i = 64; i > 0; i--)
                {
                    values.Add($"L {i}");
                }

                values.Add($"0");

                for (int i = 1; i < 64; i++)
                {
                    values.Add($"{i} R");
                }

                return values;
            }
        }
    }

    #endregion

    #region Core

    [TypeConverter(typeof(DescriptionConverter))]
    public enum Parts : byte
    {
        [Description("Part 1")]  Part01 = 0x00,
        [Description("Part 2")]  Part02 = 0x01,
        [Description("Part 3")]  Part03 = 0x02,
        [Description("Part 4")]  Part04 = 0x03,
        [Description("Part 5")]  Part05 = 0x04,
        [Description("Part 6")]  Part06 = 0x05,
        [Description("Part 7")]  Part07 = 0x06,
        [Description("Part 8")]  Part08 = 0x07,
        [Description("Part 9")]  Part09 = 0x08,
        [Description("Part 10")] Part10 = 0x09,
        [Description("Part 11")] Part11 = 0x0A,
        [Description("Part 12")] Part12 = 0x0B,
        [Description("Part 13")] Part13 = 0x0C,
        [Description("Part 14")] Part14 = 0x0D,
        [Description("Part 15")] Part15 = 0x0E,
        [Description("Part 16")] Part16 = 0x0F
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraChannels : byte
    {
        [Description("Channel 1")]  Channel01 = 0x00,
        [Description("Channel 2")]  Channel02 = 0x01,
        [Description("Channel 3")]  Channel03 = 0x02,
        [Description("Channel 4")]  Channel04 = 0x03,
        [Description("Channel 5")]  Channel05 = 0x04,
        [Description("Channel 6")]  Channel06 = 0x05,
        [Description("Channel 7")]  Channel07 = 0x06,
        [Description("Channel 8")]  Channel08 = 0x07,
        [Description("Channel 9")]  Channel09 = 0x08,
        [Description("Channel 10")] Channel10 = 0x09,
        [Description("Channel 11")] Channel11 = 0x0A,
        [Description("Channel 12")] Channel12 = 0x0B,
        [Description("Channel 13")] Channel13 = 0x0C,
        [Description("Channel 14")] Channel14 = 0x0D,
        [Description("Channel 15")] Channel15 = 0x0E,
        [Description("Channel 16")] Channel16 = 0x0F
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraToneTypes : uint
    {
        [Description("PCM Synth Tone")]             PCMSynthTone                = 0x00000000,
        [Description("SuperNATURAL Synth Tone")]    SuperNATURALSynthTone       = 0x00010000,
        [Description("SuperNATURAL Acoustic Tone")] SuperNATURALAcousticTone    = 0x00020000,
        [Description("SuperNATURAL Drum Kit")]      SuperNATURALDrumkit         = 0x00030000,
        [Description("PCM Drum Kit")]               PCMDrumkit                  = 0x00100000,
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraExpansions : byte
    {
        [Description("Off")]                                    Off    = 0x00,
        [Description("SRX 01: Dynamic Drums")]                  SRX01  = 0x01,
        [Description("SRX 02: Concert Piano")]                  SRX02  = 0x02,
        [Description("SRX 03: Studio SRX")]                     SRX03  = 0x03,
        [Description("SRX 04: Symphonique Strings")]            SRX04  = 0x04,
        [Description("SRX 05: Supreme Dance")]                  SRX05  = 0x05,
        [Description("SRX 06: Complete Orchestra")]             SRX06  = 0x06,
        [Description("SRX 07: Ultimate Keys")]                  SRX07  = 0x07,
        [Description("SRX 08: Platinum Trax")]                  SRX08  = 0x08,
        [Description("SRX 09: World Collection")]               SRX09  = 0x09,
        [Description("SRX 10: Big Brass Ensemble")]             SRX10  = 0x0A,
        [Description("SRX 11: Complete Piano")]                 SRX11  = 0x0B,
        [Description("SRX 12: Classic Electric Pianos")]        SRX12  = 0x0C,
        [Description("ExSN 01: SuperNATURAL Ethnic")]           ExSN01 = 0x0D,
        [Description("ExSN 02: SuperNATURAL Woodwinds")]        ExSN02 = 0x0E,
        [Description("ExSN 03: SuperNATURAL Session")]          ExSN03 = 0x0F,
        [Description("ExSN 04: SuperNATURAL Acoustic Guitar")]  ExSN04 = 0x10,
        [Description("ExSN 05: SuperNATURAL Brass")]            ExSN05 = 0x11,
        [Description("ExSN 06: SuperNATURAL SFX")]              ExSN06 = 0x12,
        [Description("ExPCM: HQ GM2 + HQ PCM Sound")]           ExPCM  = 0x13
    }

    #endregion

    #region Waveforms

    public enum IntegraWaveFormBanks : int
    {
        INT    = 0,
        SRX01  = 1,
        SRX02  = 2,
        SRX03  = 3,
        SRX04  = 4,
        SRX05  = 5,
        SRX06  = 6,
        SRX07  = 7,
        SRX08  = 8,
        SRX09  = 9,
        SRX10  = 10,
        SRX11  = 11,
        SRX12  = 12,
        EXSN01 = 21,
        EXSN02 = 22,
        EXSN03 = 23,
        EXSN04 = 24,
        EXSN05 = 25,
        EXSN06 = 26
    }


    public enum IntegraWaveFormTypes : int
    {
        PCM = 0,
        SNA = 1,
        SND = 2,
        SNS = 3
    }

    #endregion

    #region Virtual Slots

    public enum VirtualSlotsState : uint
    {
        LoadStart    = 0x0F003001,
        LoadComplete = 0x0F003002
    }

    #endregion

    #region Common
    
    public enum IntegraSwitch : byte
    {
        Off = 0x00,
        On  = 0x01
    }

    public enum IntegraRateNoteSwitch
    {
        Hz   = 0,
        Note = 1
    }

    public enum IntegraRateMSecSwitch
    {
        MSec = 0,
        Note = 1
    }


    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraControlChannels : byte
    {
        [Description("1")]   Channel01,
        [Description("2")]   Channel02,
        [Description("3")]   Channel03,
        [Description("4")]   Channel04,
        [Description("5")]   Channel05,
        [Description("6")]   Channel06,
        [Description("7")]   Channel07,
        [Description("8")]   Channel08,
        [Description("9")]   Channel09,
        [Description("10")]  Channel10,
        [Description("11")]  Channel11,
        [Description("12")]  Channel12,
        [Description("13")]  Channel13,
        [Description("14")]  Channel14,
        [Description("15")]  Channel15,
        [Description("16")]  Channel16,
        [Description("Off")] Off
    }

    #endregion

    #region StudioSetCommon

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraControlSources : byte
    {
        [Description("Off")]
        Off = 0,
        [Description("CC01: Modulation")]
        CC01 = 1,
        [Description("CC02: Breath")]
        CC02 = 2,
        CC03 = 3,
        [Description("CC04: Foot Type")]
        CC04 = 4,
        [Description("CC05: Portamento Time")]
        CC05 = 5,
        [Description("CC06: Data Entry")]
        CC06 = 6,
        [Description("CC07: Volume")]
        CC07 = 7,
        CC08 = 8,
        CC09 = 9,
        [Description("CC10: Panpot")]
        CC10 = 10,
        [Description("CC11: Expression")]
        CC11 = 11,
        [Description("CC12: Motional Surround Control 1")]
        CC12 = 12,
        [Description("CC13: Motional Surround Control 2")]
        CC13 = 13,
        [Description("CC14: Motional Surround Control 3")]
        CC14 = 14,
        CC15 = 15,
        [Description("CC16: General Purpose Controller 1 (Tone Modify 1)")]
        CC16 = 16,
        [Description("CC17: General Purpose Controller 2 (Tone Modify 2)")]
        CC17 = 17,
        [Description("CC18: General Purpose Controller 3 (Tone Modify 3)")]
        CC18 = 18,
        [Description("CC19: General Purpose Controller 4 (Tone Modify 4)")]
        CC19 = 19,
        CC20 = 20,
        CC21 = 21,
        CC22 = 22,
        CC23 = 23,
        CC24 = 24,
        CC25 = 25,
        CC26 = 26,
        CC27 = 27,
        [Description("CC28: Motional Surround External Part Control 1")]
        CC28 = 28,
        [Description("CC29: Motional Surround External Part Control 2")]
        CC29 = 29,
        [Description("CC30: Motional Surround External Part Control 3")]
        CC30 = 30,
        CC31 = 31,
        CC33 = 33,
        CC34 = 34,
        CC35 = 35,
        CC36 = 36,
        CC37 = 37,
        [Description("CC38: Data Entry")]
        CC38 = 38,
        CC39 = 39,
        CC40 = 40,
        CC41 = 41,
        CC42 = 42,
        CC43 = 43,
        CC44 = 44,
        CC45 = 45,
        CC46 = 46,
        CC47 = 47,
        CC48 = 48,
        CC49 = 49,
        CC50 = 50,
        CC51 = 51,
        CC52 = 52,
        CC53 = 53,
        CC54 = 54,
        CC55 = 55,
        CC56 = 56,
        CC57 = 57,
        CC58 = 58,
        CC59 = 59,
        CC60 = 60,
        CC61 = 61,
        CC62 = 62,
        CC63 = 63,
        [Description("CC64: Hold 1")]
        CC64 = 64,
        [Description("CC65: Portamento")]
        CC65 = 65,
        [Description("CC66: Sostenuto")]
        CC66 = 66,
        [Description("CC67: Soft")]
        CC67 = 67,
        [Description("CC68: Legato Foot Switch")]
        CC68 = 68,
        [Description("CC69: Hold 2")]
        CC69 = 69,
        CC70 = 70,
        [Description("CC71: Resonance")]
        CC71 = 71,
        [Description("CC72: Release Time")]
        CC72 = 72,
        [Description("CC73: Attack Time")]
        CC73 = 73,
        [Description("CC74: Cutoff")]
        CC74 = 74,
        [Description("CC75: Decay Time")]
        CC75 = 75,
        [Description("CC76: Vibrato Rate")]
        CC76 = 76,
        [Description("CC77: Vibrato Depth")]
        CC77 = 77,
        [Description("CC78: Vibrato Delay")]
        CC78 = 78,
        CC79 = 79,
        [Description("CC80: General Purpose Controller 5 (Tone Variation 1)")]
        CC80 = 80,
        [Description("CC81: General Purpose Controller 6 (Tone Variation 2)")]
        CC81 = 81,
        [Description("CC82: General Purpose Controller 7 (Tone Variation 3)")]
        CC82 = 82,
        [Description("CC83: General Purpose Controller 8 (Tone Variation 4)")]
        CC83 = 83,
        [Description("CC84: Portamento Control")]
        CC84 = 84,
        CC85 = 85,
        CC86 = 86,
        CC87 = 87,
        CC88 = 88,
        CC89 = 89,
        CC90 = 90,
        [Description("CC91: General Purpose Effect 1 (Reverb Send Level)")]
        CC91 = 91,
        CC92 = 92,
        [Description("CC93: General Purpose Effect 3 (Chorus Send Level)")]
        CC93 = 93,
        CC94 = 94,
        CC95 = 95,
        [Description("Pitch Bend")]
        Bend = 96,
        [Description("Aftertouch")]
        Aft = 97
    }
    #endregion

    #region StudioSetPart

    public enum IntegraMonyPolySwitch : byte
    {
        Mono = 0x00,
        Poly = 0x01,
        Tone = 0x02
    }

    public enum IntegraToneSwitch : byte
    {
        Off  = 0x00,
        On   = 0x01,
        Tone = 0x02
    }

    public enum IntegraMuteSwitch : byte
    {
        Off = 0x00,
        Mute = 0x01
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraOutputAssigns : byte
    {
        [Description("Part")] OutputPart,
        [Description("A")]    OutputA,
        [Description("B")]    OutputB,
        [Description("C")]    OutputC,
        [Description("D")]    OutputD,
        [Description("1")]    Output01,
        [Description("2")]    Output02,
        [Description("3")]    Output03,
        [Description("4")]    Output04,
        [Description("5")]    Output05,
        [Description("6")]    Output06,
        [Description("7")]    Output07,
        [Description("8")]    Output08,
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraScaleTuneTypes : byte
    {
        [Description("Custom Individually")]                Custom       = 0x00,
        [Description("Equal Temprament")]                   Equal        = 0x01,
        [Description("Just Intonation (Major)")]            JustMajor    = 0x02,
        [Description("Just Intonation (Minor)")]            JustMinor    = 0x03,
        [Description("Pythagorean Tuning")]                 Pythagorean  = 0x04,
        [Description("Kirnberger (Type 3)")]                Kirnberger   = 0x05,
        [Description("Meantone Temprament")]                Meantone     = 0x06,
        [Description("Werckmeister (Type 1, Number 3)")]    Werckmeister = 0x07,
        [Description("Arabic Scale")]                       Arabic       = 0x08
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraScaleTuneKeys : byte
    {
        [Description("C")]  C       = 0x00,
        [Description("C#")] CSharp  = 0x01,
        [Description("D")]  D       = 0x02,
        [Description("D#")] DSharp  = 0x03,
        [Description("E")]  E       = 0x04,
        [Description("F")]  F       = 0x05,
        [Description("F#")] FSharp  = 0x06,
        [Description("G")]  G       = 0x07,
        [Description("G#")] GSharp  = 0x08,
        [Description("A")]  A       = 0x09,
        [Description("A#")] ASharp  = 0x0A,
        [Description("B")]  B       = 0x0B
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraVelocityCurveTypes : byte
    {
        [Description("Off")] Off  = 0x00,
        [Description("1")]   VC01 = 0x01,
        [Description("2")]   VC02 = 0x02,
        [Description("3")]   VC03 = 0x03,
        [Description("4")]   VC04 = 0x04
    }
    #endregion

    #region EQ

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraLowFrequencies : byte
    {
        [Description("200")] Hz200 = 0x00,
        [Description("400")] Hz400 = 0x01
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraMidFrequencies : byte
    {
        [Description("200")]  Hz200  = 0x00,
        [Description("250")]  Hz250  = 0x01,
        [Description("315")]  Hz315  = 0x02,
        [Description("400")]  Hz400  = 0x03,
        [Description("500")]  Hz500  = 0x04,
        [Description("630")]  Hz630  = 0x05,
        [Description("800")]  Hz800  = 0x06,
        [Description("1000")] Hz1000 = 0x07,
        [Description("1250")] Hz1250 = 0x08,
        [Description("1600")] Hz1600 = 0x09,
        [Description("2000")] Hz2000 = 0x0A,
        [Description("2500")] Hz2500 = 0x0B,
        [Description("3150")] Hz3150 = 0x0C,
        [Description("4000")] Hz4000 = 0x0D,
        [Description("5000")] Hz5000 = 0x0E,
        [Description("6300")] Hz6300 = 0x0F,
        [Description("8000")] Hz8000 = 0x10
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraMidQs : byte
    {
        [Description("0.5")] Q05,
        [Description("1.0")] Q10,
        [Description("2.0")] Q20,
        [Description("4.0")] Q40,
        [Description("8.0")] Q80
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraHighFrequencies : byte
    {
        [Description("2000")] Hz2000,
        [Description("4000")] Hz4000,
        [Description("8000")] Hz8000
    }

    #endregion

    #region Chorus

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraChorusTypes : byte
    {
        [Description("Off")]        Off       = 0x00,
        [Description("Chorus")]     Chorus    = 0x01,
        [Description("Delay")]      Delay     = 0x02,
        [Description("GM2 Chorus")] GM2Chorus = 0x03
    }

    public enum IntegraStudioSetCommonOutputAssigns : byte
    {
        A = 0x00,
        B = 0x01,
        C = 0x02,
        D = 0x03
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraChorusOutputSelections : byte
    {
        [Description("Main")]          Main       = 0x00,
        [Description("Reverb")]        Reverb     = 0x01,
        [Description("Main + Reverb")] MainReverb = 0x02
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraChorusFilterTypes : byte
    {
        [Description("Off")] Off = 0x00,
        [Description("LPF")] LPF = 0x01,
        [Description("HPF")] HPF = 0x02
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraDelayHFDamps : byte
    {
        [Description("200 [Hz]")]  Hz200  = 0x00,
        [Description("250 [Hz]")]  Hz250  = 0x01,
        [Description("315 [Hz]")]  Hz315  = 0x02,
        [Description("400 [Hz]")]  Hz400  = 0x03,
        [Description("500 [Hz]")]  Hz500  = 0x04,
        [Description("630 [Hz]")]  Hz630  = 0x05,
        [Description("800 [Hz]")]  Hz800  = 0x06,
        [Description("1000 [Hz]")] Hz1000 = 0x07,
        [Description("1250 [Hz]")] Hz1250 = 0x08,
        [Description("1600 [Hz]")] Hz1600 = 0x09,
        [Description("2000 [Hz]")] hz2000 = 0x0A,
        [Description("2500 [Hz]")] Hz2500 = 0x0B,
        [Description("3150 [Hz]")] Hz3150 = 0x0C,
        [Description("4000 [Hz]")] Hz4000 = 0x0D,
        [Description("5000 [Hz]")] Hz5000 = 0x0E,
        [Description("6300 [Hz]")] Hz6300 = 0x0F,
        [Description("8000 [Hz]")] Hz8000 = 0x10,
        [Description("Bypass")]    Bypass = 0x11
    }


    #endregion

    #region Reverb

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraReverbTypes : byte
    {
        [Description("Off")]        Off   = 0x00,
        [Description("Room 1")]     Room1 = 0x01,
        [Description("Room 2")]     Room2 = 0x02,
        [Description("Hall 1")]     Hall1 = 0x03,
        [Description("Hall 2")]     Hall2 = 0x04,
        [Description("Plate")]      Plate = 0x05,
        [Description("GM2 Reverb")] GM2   = 0x06
    }
    #endregion

    #region MotionalSurround

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraRoomTypes : byte
    {
        [Description("Room 1")] Room01 = 0x00,
        [Description("Room 2")] Room02 = 0x01,
        [Description("Hall 1")] Hall01 = 0x02,
        [Description("Hall 2")] Hall02 = 0x03
    }

    public enum IntegraRoomSizes : byte
    {
        Small  = 0x00,
        Medium = 0x01,
        Large  = 0x02,
    }

    #endregion

    #region Special

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraNoteRates : byte
    {
        [Description("\U0001D163\U00002083")] _164T = 0x00,
        [Description("\U0001D163")]           _164  = 0x01,
        [Description("\U0001D162\U00002083")] _132T = 0x02,
        [Description("\U0001D162")]           _132  = 0x03,
        [Description("\U0001D161\U00002083")] _116T = 0x04,
        [Description("\U0001D162.")]          _132D = 0x05,
        [Description("\U0001D161")]           _116  = 0x06,
        [Description("\U0001D160\U00002083")] _18T  = 0x07,
        [Description("\U0001D161.")]          _116D = 0x08,
        [Description("\U0001D160")]           _18   = 0x09,
        [Description("\U0001D15F\U00002083")] _14T  = 0x0A,
        [Description("\U0001D161.")]          _18D  = 0x0B,
        [Description("\U0001D15F")]           _14   = 0x0C,
        [Description("\U0001D15E\U00002083")] _12T  = 0x0D,
        [Description("\U0001D15F.")]          _14D  = 0x0E,
        [Description("\U0001D15E")]           _12   = 0x0F,
        [Description("\U0001D15D\U00002083")] _1T   = 0x10,
        [Description("\U0001D15E.")]          _12D  = 0x11,
        [Description("\U0001D15D")]           _1    = 0x12,
        [Description("\U0001D15C\U00002083")] _2T   = 0x13,
        [Description("\U0001D15D.")]          _1D   = 0x14,
        [Description("\U0001D15C")]           _2    = 0x15
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraScaleKey : byte
    {
        [Description("C")]           C     = 0x00,
        [Description("D\U0001D12D")] DFlat = 0x01,
        [Description("D")]           D     = 0x02,
        [Description("E\U0001D12D")] EFlat = 0x03,
        [Description("E")]           E     = 0x04,
        [Description("F")]           F     = 0x05,
        [Description("G\U0001D12D")] GFlat = 0x06,
        [Description("G")]           G     = 0x07,
        [Description("A\U0001D12D")] AFlat = 0x08,
        [Description("A")]           A     = 0x09,
        [Description("B\U0001D12D")] BFlat = 0x0A,
        [Description("B")]           B     = 0x0B,
    }

    #endregion

    #region Tone

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraToneBanks : ushort
    {
        //[Description("Undefined")]                                  Undefined       = 0x0000,
        [Description("PCM User Drum Kit")]                          PCMUserDrum     = 0x5600,
        [Description("PCM Preset Drum Kit")]                        PCMPresetDrum   = 0x5640,
        [Description("PCM User Tone")]                              PCMUserTone     = 0x5700,
        [Description("PCM Preset Tone")]                            PCMPresetTone   = 0x5740,
        [Description("SuperNATURAL User Drum Kit")]                 SNDUserDrum     = 0x5800,
        [Description("SuperNATURAL Preset Drum Kit")]               SNDPresetDrum   = 0x5840,
        [Description("ExSN-06 SuperNATURAL Expansion Drum Kit")]    ExSN06Drum      = 0x5865,
        [Description("SuperNATURAL Acoustic User Tone")]            SNAUserTone     = 0x5900,
        [Description("SuperNATURAL Acoustic Preset Tone")]          SNAPresetTone   = 0x5940,
        [Description("ExSN-01 SuperNATURAL Expansion Tone")]        ExSN01Tone      = 0x5960,
        [Description("ExSN-02 SuperNATURAL Expansion Tone")]        ExSN02Tone      = 0x5961,
        [Description("ExSN-03 SuperNATURAL Expansion Tone")]        ExSN03Tone      = 0x5962,
        [Description("ExSN-04 SuperNATURAL Expansion Tone")]        ExSN04Tone      = 0x5963,
        [Description("ExSN-05 SuperNATURAL Expansion Tone")]        ExSN05Tone      = 0x5964,
        [Description("SRX-01 PCM Expansion Drum Kit")]              SRX01Drum       = 0x5C00,
        [Description("SRX-03 PCM Expansion Drum Kit")]              SRX03Drum       = 0x5C02,
        [Description("SRX-05 PCM Expansion Drum Kit")]              SRX05Drum       = 0x5C04,
        [Description("SRX-06 PCM Expansion Drum Kit")]              SRX06Drum       = 0x5C07,
        [Description("SRX-07 PCM Expansion Drum Kit")]              SRX07Drum       = 0x5C0B,
        [Description("SRX-08 PCM Expansion Drum Kit")]              SRX08Drum       = 0x5C0F,
        [Description("SRX-09 PCM Expansion Drum Kit")]              SRX09Drum       = 0x5C13,
        [Description("SRX-01 PCM Expansion Tone")]                  SRX01Tone       = 0x5D00,
        [Description("SRX-02 PCM Expansion Tone")]                  SRX02Tone       = 0x5D01,
        [Description("SRX-03 PCM Expansion Tone")]                  SRX03Tone       = 0x5D02,
        [Description("SRX-04 PCM Expansion Tone")]                  SRX04Tone       = 0x5D03,
        [Description("SRX-05 PCM Expansion Tone")]                  SRX05Tone       = 0x5D04,
        [Description("SRX-06 PCM Expansion Tone")]                  SRX06Tone       = 0x5D07,
        [Description("SRX-07 PCM Expansion Tone")]                  SRX07Tone       = 0x5D0B,
        [Description("SRX-08 PCM Expansion Tone")]                  SRX08Tone       = 0x5D0F,
        [Description("SRX-09 PCM Expansion Tone")]                  SRX09Tone       = 0x5D13,
        [Description("SRX-10 PCM Expansion Tone")]                  SRX10Tone       = 0x5D17,
        [Description("SRX-11 PCM Expansion Tone")]                  SRX11Tone       = 0x5D18,
        [Description("SRX-12 PCM Expansion Tone")]                  SRX12Tone       = 0x5D1A,
        [Description("SuperNATURAL Synth User Tone")]               SNSUserTone     = 0x5F00,
        [Description("SuperNATURAL Synth Preset Tone")]             SNSPresetTone   = 0x5F40,
        [Description("ExPCM Expansion Drum Kit")]                   ExPCMDrum       = 0x6000,
        [Description("ExPCM Expansion Tone")]                       ExPCMTone       = 0x6100,
        [Description("GM2 Drum Kit")]                               GM2Drum         = 0x7800,
        [Description("GM2 Tone")]                                   GM2Tone         = 0x7900,
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraToneCategories : byte
    {
        [Description("No Assign")]              Unassigned          = 0x00,
        [Description("Acoustic Piano")]         AcousticPiano       = 0x01,
        [Description("Electric Piano")]         ElectricPiano       = 0x04,
        [Description("Organ")]                  Organ               = 0x06,
        [Description("Other Keys")]             OtherKeys           = 0x09,
        [Description("Accordion & Harmonica")]  AccordionHarmonica  = 0x0C,
        [Description("Bells & Mallet")]         BellMallet          = 0x0E,
        [Description("Acoustic Guitar")]        AcousticGuitar      = 0x10,
        [Description("Electric Guitar")]        ElectricGuitar      = 0x11,
        [Description("Distortion Guitar")]      DistortionGuitar    = 0x12,
        [Description("Acoustic Bass")]          AcousticBass        = 0x13,
        [Description("Electric Bass")]          ElectricBass        = 0x14,
        [Description("Synth Bass")]             SynthBass           = 0x15,
        [Description("Plucked & Strokes")]      PluckedStroke       = 0x16,
        [Description("Strings")]                Strings             = 0x17,
        [Description("Brass")]                  Brass               = 0x1A,
        [Description("Wind")]                   Wind                = 0x1C,
        [Description("Flute")]                  Flute               = 0x1D,
        [Description("Sax")]                    Sax                 = 0x1E,
        [Description("Recorder")]               Recorder            = 0x1F,
        [Description("Vox & Choir")]            VoxChoir            = 0x20,
        [Description("Synth Lead")]             SynthLead           = 0x22,
        [Description("Synth Brass")]            SynthBrass          = 0x23,
        [Description("Synth Pad Strings")]      SynthPadStrings     = 0x24,
        [Description("Synth Bell Pads")]        SynthBellpad        = 0x25,
        [Description("Synth Poly Keys")]        SynthPolyKey        = 0x26,
        [Description("FX")]                     FX                  = 0x27,
        [Description("Synth Sequence Pop")]     SynthSeqPop         = 0x28,
        [Description("Phrase")]                 Phrase              = 0x29,
        [Description("Pulstating")]             Pulstating          = 0x2A,
        [Description("Beat & Grooves")]         BeatGroove          = 0x2B,
        [Description("Hit")]                    Hit                 = 0x2C,
        [Description("Sound FX")]               SoundFX             = 0x2D,
        [Description("Drums")]                  Drums               = 0x2E,
        [Description("Percussion")]             Percussion          = 0x2F,
        [Description("Combination")]            Combination         = 0x30
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraSynthPhrase : int
    {
        [Description("")] NoAssign           = 0x00000000,
        [Description("")] Piano01            = 0x00000001,
        [Description("")] Piano02            = 0x00000002,
        [Description("")] Piano03            = 0x00000003,
        [Description("")] Piano04            = 0x00000004,
        [Description("")] Piano05            = 0x00000005,
        [Description("")] Piano06            = 0x00000006,
        [Description("")] Piano07            = 0x00000007,
        [Description("")] Piano08            = 0x00000008,
        [Description("")] Piano09            = 0x00000009,
        [Description("")] Piano10            = 0x0000000A,
        [Description("")] EPiano01           = 0x0000000B,
        [Description("")] EPiano02           = 0x0000000C,
        [Description("")] EPiano03           = 0x0000000D,
        [Description("")] EPiano04           = 0x0000000E,
        [Description("")] EPiano05           = 0x0000000F,
        [Description("")] EPiano06           = 0x00000100,
        [Description("")] EOrgan01           = 0x00000101,
        [Description("")] EOrgan02           = 0x00000102,
        [Description("")] EOrgan03           = 0x00000103,
        [Description("")] EOrgan04           = 0x00000104,
        [Description("")] EOrgan05           = 0x00000105,
        [Description("")] EOrgan06           = 0x00000106,
        [Description("")] EOrgan07           = 0x00000107,
        [Description("")] EOrgan08           = 0x00000108,
        [Description("")] EOrgan09           = 0x00000109,
        [Description("")] EOrgan10           = 0x0000010A,
        [Description("")] PipeOrgan01        = 0x0000010B,
        [Description("")] PipeOrgan02        = 0x0000010C,
        [Description("")] ReedOrgan          = 0x0000010D,
        [Description("")] Harpsichord01      = 0x0000010E,
        [Description("")] Harpsichord02      = 0x0000010F,
        [Description("")] Clav01             = 0x00000200,
        [Description("")] Clav02             = 0x00000201,
        [Description("")] Celesta            = 0x00000202,
        [Description("")] Accordiaon01       = 0x00000203,
        [Description("")] Accordiaon02       = 0x00000204,
        [Description("")] Harmonica          = 0x00000205,
        [Description("")] Bell01             = 0x00000206,
        [Description("")] MusicBox           = 0x00000207,
        [Description("")] Vibraphone01       = 0x00000208,
        [Description("")] Vibraphone02       = 0x00000209,
        [Description("")] Vibraphone03       = 0x0000020A,
        [Description("")] Vibraphone04       = 0x0000020B,
        [Description("")] Marimba01          = 0x0000020C,
        [Description("")] Marimba02          = 0x0000020D,
        [Description("")] Glockenspiel       = 0x0000020E,
        [Description("")] Xylophone01        = 0x0000020F,
        [Description("")] Xylophone02        = 0x00000300,
        [Description("")] Xylophone03        = 0x00000301,
        [Description("")] YangQin            = 0x00000302,
        [Description("")] Santur01           = 0x00000303,
        [Description("")] Santur02           = 0x00000304,
        [Description("")] SteelDrums         = 0x00000305,
        [Description("")] AcGuitar01         = 0x00000306,
        [Description("")] AcGuitar02         = 0x00000307,
        [Description("")] AcGuitar03         = 0x00000308,
        [Description("")] AcGuitar04         = 0x00000309,
        [Description("")] AcGuitar05         = 0x0000030A,
        [Description("")] Mandolin01         = 0x0000030B,
        [Description("")] Mandolin02         = 0x0000030C,
        [Description("")] Ukelele            = 0x0000030D,
        [Description("")] JazzGutiar01       = 0x0000030E,
        [Description("")] JazzGutiar02       = 0x0000030F,
        [Description("")] JazzGutiar03       = 0x00000400,
        [Description("")] EGuitar            = 0x00000401,
        [Description("")] MutedGuitar        = 0x00000402,
        [Description("")] PedalSteel         = 0x00000403,
        [Description("")] DistGuitar01       = 0x00000404,
        [Description("")] AcBass01           = 0x00000405,
        [Description("")] AcBass02           = 0x00000406,
        [Description("")] EBass01            = 0x00000407,
        [Description("")] EBass02            = 0x00000408,
        [Description("")] FretlessBass01     = 0x00000409,
        [Description("")] FretlessBass02     = 0x0000040A,
        [Description("")] FretlessBass03     = 0x0000040B,
        [Description("")] SlapBass01         = 0x0000040C,
        [Description("")] SlapBass02         = 0x0000040D,
        [Description("")] SynthBass01        = 0x0000040E,
        [Description("")] SynthBass02        = 0x0000040F,
        [Description("")] SynthBass03        = 0x00000500,
        [Description("")] SynthBass04        = 0x00000501,
        [Description("")] SynthBass05        = 0x00000502,
        [Description("")] SynthBass06        = 0x00000503,
        [Description("")] PluckedStroke      = 0x00000504,
        [Description("")] Banjo              = 0x00000505,
        [Description("")] Harp               = 0x00000506,
        [Description("")] Koto               = 0x00000507,
        [Description("")] Shamisen           = 0x00000508,
        [Description("")] Sitar              = 0x00000509,
        [Description("")] Violin01           = 0x0000050A,
        [Description("")] Violin02           = 0x0000050B,
        [Description("")] Fiddle             = 0x0000050C,
        [Description("")] Cello01            = 0x0000050D,
        [Description("")] Cello02            = 0x0000050E,
        [Description("")] Contrabass01       = 0x0000050F,
        [Description("")] Contrabass02       = 0x00000600,
        [Description("")] EnsembleStrins01   = 0x00000601,
        [Description("")] EnsembleStrins02   = 0x00000602,
        [Description("")] EnsembleStrins03   = 0x00000603,
        [Description("")] TremoloStrings     = 0x00000604,
        [Description("")] PizzicatoStrings01 = 0x00000605,
        [Description("")] PizzicatoStrings02 = 0x00000606,
        [Description("")] Orchestra01        = 0x00000607,
        [Description("")] Orchestra02        = 0x00000608,
        [Description("")] SoloBrass          = 0x00000609,
        [Description("")] Trumpet01          = 0x0000060A,
        [Description("")] Trumpet02          = 0x0000060B,
        [Description("")] MuteTrumpet        = 0x0000060C,
        [Description("")] Trombone           = 0x0000060D,
        [Description("")] FrenchHorn         = 0x0000060E,
        [Description("")] Tuba               = 0x0000060F,
        [Description("")] EnsembleBrass01    = 0x00000700,
        [Description("")] FrenchHornSection  = 0x00000701,
        [Description("")] Wind               = 0x00000702,
        [Description("")] Oboe               = 0x00000703,
        [Description("")] Clarinet           = 0x00000704,
        [Description("")] Bassoon            = 0x00000705,
        [Description("")] Bagpipe01          = 0x00000706,
        [Description("")] Bagpipe02          = 0x00000707,
        [Description("")] Shanai             = 0x00000708,
        [Description("")] Shakuhachi         = 0x00000709,
        [Description("")] Flute              = 0x0000070A,
        [Description("")] SopranoSax01       = 0x0000070B,
        [Description("")] SopranoSax02       = 0x0000070C,
        [Description("")] AltoSax01          = 0x0000070D,
        [Description("")] AltoSax02          = 0x0000070E,
        [Description("")] TenorSax01         = 0x0000070F,
        [Description("")] BaritoneSax        = 0x00000800,
        [Description("")] Recorder           = 0x00000801,
        [Description("")] VoxChoirs01        = 0x00000802,
        [Description("")] VoxChoirs02        = 0x00000803,
        [Description("")] Scat01             = 0x00000804,
        [Description("")] Scat02             = 0x00000805,
        [Description("")] SynthLead01        = 0x00000806,
        [Description("")] SynthLead02        = 0x00000807,
        [Description("")] SynthLead03        = 0x00000808,
        [Description("")] SynthLead04        = 0x00000809,
        [Description("")] SynthLead05        = 0x0000080A,
        [Description("")] SynthLead06        = 0x0000080B,
        [Description("")] SynthLead07        = 0x0000080C,
        [Description("")] SynthBrass01       = 0x0000080D,
        [Description("")] SynthBrass02       = 0x0000080E,
        [Description("")] SynthBrass03       = 0x0000080F,
        [Description("")] SynthBrass04       = 0x00000900,
        [Description("")] SynthPadStrings01  = 0x00000901,
        [Description("")] SynthPadStrings02  = 0x00000902,
        [Description("")] SynthPadStrings03  = 0x00000903,
        [Description("")] SynthBellPad01     = 0x00000904,
        [Description("")] SynthBellPad02     = 0x00000905,
        [Description("")] SynthBellPad03     = 0x00000906,
        [Description("")] SynthPolyKey01     = 0x00000907,
        [Description("")] SynthPolyKey02     = 0x00000908,
        [Description("")] SynthPolyKey03     = 0x00000909,
        [Description("")] SynthSeqPop01      = 0x0000090A,
        [Description("")] SynthSeqPop02      = 0x0000090B,
        [Description("")] Timpani01          = 0x0000090C,
        [Description("")] Timpani02          = 0x0000090D,
        [Description("")] Percussion         = 0x0000090E,
        [Description("")] SoundFX01          = 0x0000090F,
        [Description("")] SoundFX02          = 0x00000A00,
        [Description("")] SoundFX03          = 0x00000A01,
        [Description("")] Vibraphone05       = 0x00000A02,
        [Description("")] DistGuitar02       = 0x00000A03,
        [Description("")] DistGuitar03       = 0x00000A04,
        [Description("")] EBass03            = 0x00000A05,
        [Description("")] EBass04            = 0x00000A06,
        [Description("")] SynthBass07        = 0x00000A07,
        [Description("")] SynthBass08        = 0x00000A08,
        [Description("")] SynthBass09        = 0x00000A09,
        [Description("")] SynthBass10        = 0x00000A0A,
        [Description("")] SynthBass11        = 0x00000A0B,
        [Description("")] SynthBass12        = 0x00000A0C,
        [Description("")] Santur03           = 0x00000A0D,
        [Description("")] EnsembleBrass02    = 0x00000A0E,
        [Description("")] TenorSax02         = 0x00000A0F,
        [Description("")] TenorSax03         = 0x00000B00,
        [Description("")] PanPipe            = 0x00000B01,
        [Description("")] VoxChoirs03        = 0x00000B02,
        [Description("")] VoxChoirs04        = 0x00000B03,
        [Description("")] VoxChoirs05        = 0x00000B04,
        [Description("")] VoxChoirs06        = 0x00000B05,
        [Description("")] VoxChoirs07        = 0x00000B06,
        [Description("")] SynthLead08        = 0x00000B07,
        [Description("")] SynthPadStrings04  = 0x00000B08,
        [Description("")] SynthPadStrings05  = 0x00000B09,
        [Description("")] SynthBell01        = 0x00000B0A,
        [Description("")] SynthBell02        = 0x00000B0B,
        [Description("")] SynthBell03        = 0x00000B0C,
        [Description("")] SynthBell04        = 0x00000B0D,
        [Description("")] SynthBell05        = 0x00000B0E,
        [Description("")] SynthPolyKey04     = 0x00000B0F,
        [Description("")] SynthPolyKey05     = 0x00000C00,
        [Description("")] SynthPolyKey06     = 0x00000C01,
        [Description("")] SynthPolyKey07     = 0x00000C02,
        [Description("")] SynthPolyKey08     = 0x00000C03,
        [Description("")] SynthPolyKey09     = 0x00000C04,
        [Description("")] SynthPolyKey10     = 0x00000C05,
        [Description("")] Bell02             = 0x00000C06,
        [Description("")] Bell03             = 0x00000C07,
        [Description("")] SynthPolyKey11     = 0x00000C08,
        [Description("")] SynthPadStrings06  = 0x00000C09,
        [Description("")] SynthPadStrings07  = 0x00000C0A,
        [Description("")] SynthPadStrings08  = 0x00000C0B,
        [Description("")] SoundFX04          = 0x00000C0C,
        [Description("")] SoundFX05          = 0x00000C0D,
        [Description("")] XVAcPiano          = 0x00000C0E,
        [Description("")] XVElPiano          = 0x00000C0F,
        [Description("")] XVKeyboards        = 0x00000D00,
        [Description("")] XVBell             = 0x00000D01,
        [Description("")] XVMallet           = 0x00000D02,
        [Description("")] XVOrgan            = 0x00000D03,
        [Description("")] XVAccordion        = 0x00000D04,
        [Description("")] XVHarmonica        = 0x00000D05,
        [Description("")] XVAcGuitar         = 0x00000D06,
        [Description("")] XVElGuitar         = 0x00000D07,
        [Description("")] XVDistGuitar       = 0x00000D08,
        [Description("")] XVBass             = 0x00000D09,
        [Description("")] XVSynthBass        = 0x00000D0A,
        [Description("")] XVStrings          = 0x00000D0B,
        [Description("")] XVOrchestra        = 0x00000D0C,
        [Description("")] XVHitStab          = 0x00000D0D,
        [Description("")] XVWind             = 0x00000D0E,
        [Description("")] XVFlute            = 0x00000D0F,
        [Description("")] XVAcBrass          = 0x00000E00,
        [Description("")] XVSynthBrass       = 0x00000E01,
        [Description("")] XVSax              = 0x00000E02,
        [Description("")] XVHardLead         = 0x00000E03,
        [Description("")] XVSoftLead         = 0x00000E04,
        [Description("")] XVTechnoSynth      = 0x00000E05,
        [Description("")] XVPulsating        = 0x00000E06,
        [Description("")] XVSynthFX          = 0x00000E07,
        [Description("")] XVOtherSynth       = 0x00000E08,
        [Description("")] XVBrightPad        = 0x00000E09,
        [Description("")] XVSoftPad          = 0x00000E0A,
        [Description("")] XVVox              = 0x00000E0B,
        [Description("")] XVPlucked          = 0x00000E0C,
        [Description("")] XVEthnic           = 0x00000E0D,
        [Description("")] XVFretted          = 0x00000E0E,
        [Description("")] XVPercussion       = 0x00000E0F,
        [Description("")] XVSoundFX          = 0x00000F00,
        [Description("")] XVBeatGroove       = 0x00000F01,
        [Description("")] XVDrums            = 0x00000F02,
        [Description("")] XVCombination      = 0x00000F03,
    }

    #endregion

    #region System

    public enum IntegraClockSource : byte
    {
        MIDI = 0x00,
        USB  = 0x01
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraTempoAssignSource : byte
    {
        [Description("System")]     System    = 0x00,
        [Description("Studio Set")] StudioSet = 0x01
    }

    public enum IntegraOutputMode : byte
    {
        Speaker = 0x00,
        Phones  = 0x01
    }

    #endregion

    #region Setup

    public enum IntegraSoundModes : byte
    {
        Studio = 0x01,
        GM     = 0x02,
        GM2    = 0x03,
        GS     = 0x04
    }

    #endregion

    #region SuperNATURALSynthToneCommon

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraRingSwitch : byte
    {
        [Description("Off")] Off   = 0x00,
        [Description("---")] Empty = 0x01,
        [Description("On")]  On    = 0x02,
    }

    public enum IntegraPortamentoMode : byte
    {
        Normal = 0x00,
        Legato = 0x01
    }

    #endregion

    #region SuperNATURALAcousticTone Parameters

    [TypeConverter (typeof(DescriptionConverter))]
    public enum SNANuance : byte
    {
        [Description("Type 1")] Type1 = 0x00,
        [Description("Type 2")] Type2 = 0x01,
        [Description("Type 3")] Type3 = 0x02
    }

    public enum SNAPercussionSoft : byte
    {
        Normal = 0x00,
        Soft   = 0x01
    }

    public enum SNAPercussionSlow : byte
    {
        Fast = 0x00,
        Slow = 0x01
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum SNAPercussionHarmonic : byte
    {
        [Description("Second")] SND = 0x00,
        [Description("Third")] TRD = 0x01
    }

    public enum SNAPlayScale : byte
    {
        Chroma,
        Major,
        Minor,
        Seventh,
        Diminish,
        Whole,
        HarmonicMinor
    }

    public enum SNAPortaGliss : byte
    {
        Porta,
        Gliss
    }
    #endregion

    #region SuperNATURALAcousticTone Variations

    public enum VarBellMallet1 : byte
    {
        Off = 0x00,
        DeadStroke = 0x01
    }

    public enum VarBellMallet2 : byte
    {
        Off = 0x00,
        DeadStroke = 0x01,
        TremoloSw = 0x02
    }

    public enum VarBellMallet3 : byte
    {
        Off = 0x00,
        Mute = 0x01,
        Tremolo = 0x02
    }

    public enum VarGuitar1 : byte
    {
        Off = 0x00,
        Mute = 0x01,
        Harmonics = 0x02
    }

    public enum VarGuitar2 : byte
    {
        Off = 0x00,
        Rasugueado = 0x01,
        Harmonics = 0x02
    }

    public enum VarGuitar3 : byte
    {
        Off = 0x00,
        FingerPicking = 0x01,
        OctaveTone = 0x02
    }

    public enum VarBass1 : byte
    {
        Off = 0x00,
        Staccato = 0x01,
        Harmonics = 0x02
    }

    public enum VarBass2 : byte
    {
        Off = 0x00,
        Slap = 0x01,
        Harmonics = 0x02
    }

    public enum VarBass3 : byte
    {
        Off = 0x00,
        BridgeMute = 0x01,
        Harmonics = 0x02
    }

    public enum VarHarp : byte
    {
        Off = 0x00,
        Nail = 0x01
    }

    public enum VarShamisen : byte
    {
        Off = 0x00,
        Strum = 0x01,
        UpPicking = 0x02,
        AutoBend = 0x03
    }

    public enum VarKoto: byte
    {
        Off = 0x00,
        Tremolo = 0x01,
        Ornament = 0x02
    }

    public enum VarKalimba : byte
    {
        Off = 0x00,
        Buzz = 0x01
    }

    public enum VarStrings1 : byte
    {
        Off = 0x00,
        Staccato = 0x01,
        Pizzicato = 0x02,
        Tremolo = 0x03
    }

    public enum VarStrings2 : byte
    {
        Off = 0x00,
        Staccato = 0x01,
        Ornament = 0x02
    }

    public enum VarBrass1 : byte
    {
        Off = 0x00,
        Staccato = 0x01,
    }

    public enum VarBrass2 : byte
    {
        Off = 0x00,
        Staccato = 0x01,
        Fall = 0x02
    }

    public enum VarPipes : byte
    {
        Off = 0x00,
        Ornament = 0x02
    }

    public enum VarWind2 : byte
    {
        Off = 0x00,
        Staccato = 0x01,
        Flutter = 0x02
    }

    public enum VarWind3 : byte
    {
        Off = 0x00,
        Cut = 0x01,
        Ornament = 0x02
    }

    public enum VarWind4 : byte
    {
        Off,
        Staccato,
        Fall,
        SubTone
    }

    public enum VarChoir : byte
    {
        Off,
        VoiceWoo
    }

    public enum VarTimpani : byte
    {
        Off,
        Flam,
        AccentRoll
    }

    public enum VarSteelDrum: byte
    {
        Off,
        Mute
    }

    #endregion

    #region SuperNATURAL DrumKit

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraNoteVariation : byte
    {
        // TODO: Enumeration descriptions
        [Description("Off")] Off  = 0x00,
        [Description("1")] Flam01 = 0x01,
        [Description("2")] Flam02 = 0x02,
        [Description("3")] Flam03 = 0x03,
        [Description("1")] Buzz01 = 0x04,
        [Description("2")] Buzz02 = 0x05,
        [Description("3")] Buzz03 = 0x06,
        [Description("3")] Roll   = 0x07,
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraNoteOutputAssign : byte
    {
        // TODO: Enumeration descriptions
        [Description("Off")] Part     = 0x00,
        [Description("1")]   CompEQ01 = 0x01,
        [Description("2")]   CompEQ02 = 0x02,
        [Description("3")]   CompEQ03 = 0x03,
        [Description("1")]   CompEQ04 = 0x04,
        [Description("2")]   CompEQ05 = 0x05,
        [Description("3")]   CompEQ06 = 0x06,
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraSNDNoteIndex : byte
    {
        [Description("1 E\U0001D12D")] E1Flat,
        [Description("1 E")]           E1,
        [Description("1 F")]           F1,
        [Description("1 F\U0001D130")] F1Sharp,
        [Description("1 G")]           G1,
        [Description("1 G\U0001D130")] G1Sharp,
        [Description("1 A")]           A1,
        [Description("1 B\U0001D12D")] B1Flat,
        [Description("1 B")]           B1,
        [Description("2 C")]           C2,
        [Description("2 C\U0001D130")] C2Sharp,
        [Description("2 D")]           D2,
        [Description("2 E\U0001D12D")] E2Flat,
        [Description("2 E")]           E2,
        [Description("2 F")]           F2,
        [Description("2 F\U0001D130")] F2Sharp,
        [Description("2 G")]           G2,
        [Description("2 G\U0001D130")] G2Sharp,
        [Description("2 A")]           A2,
        [Description("2 B\U0001D12D")] B2Flat,
        [Description("2 B")]           B2,
        [Description("2 C")]           C3,
        [Description("3 C\U0001D130")] C3Sharp,
        [Description("3 D")]           D3,
        [Description("3 E\U0001D12D")] E3Flat,
        [Description("3 E")]           E3,
        [Description("3 F")]           F3,
        [Description("3 F\U0001D130")] F3Sharp,
        [Description("3 G")]           G3,
        [Description("3 G\U0001D130")] G3Sharp,
        [Description("3 A")]           A3,
        [Description("3 B\U0001D12D")] B3Flat,
        [Description("3 B")]           B3,
        [Description("4 C")]           C4,
        [Description("4 C\U0001D130")] C4Sharp,
        [Description("4 D")]           D4,
        [Description("4 E\U0001D12D")] E4Flat,
        [Description("4 E")]           E4,
        [Description("4 F")]           F4,
        [Description("4 F\U0001D130")] F4Sharp,
        [Description("4 G")]           G4,
        [Description("4 G\U0001D130")] G4Sharp,
        [Description("4 A")]           A4,
        [Description("4 B\U0001D12D")] B4Flat,
        [Description("4 B")]           B4,
        [Description("5 C")]           C5,
        [Description("5 C\U0001D130")] C5Sharp,
        [Description("5 D")]           D5,
        [Description("5 E\U0001D12D")] E5Flat,
        [Description("5 E")]           E5,
        [Description("5 F")]           F5,
        [Description("5 F\U0001D130")] F5Sharp,
        [Description("5 G")]           G5,
        [Description("5 G\U0001D130")] G5Sharp,
        [Description("5 A")]           A5,
        [Description("5 B\U0001D12D")] B5Flat,
        [Description("5 B")]           B5,
        [Description("6 C")]           C6,
        [Description("6 C\U0001D130")] C6Sharp,
        [Description("6 D")]           D6,
        [Description("6 E\U0001D12D")] E6Flat,
        [Description("6 E")]           E6
    }

    #endregion

    #region PCMSynthToneCommon

    public enum IntegraStructureType : byte
    {
        Type1  = 0x00,
        Type2  = 0x01,
        Type3  = 0x02,
        Type4  = 0x03,
        Type5  = 0x04,
        Type6  = 0x05,
        Type7  = 0x06,
        Type8  = 0x07,
        Type9  = 0x08,
        Type10 = 0x09

    }
    public enum IntegraTonePriority : byte
    {
        Last    = 0x00,
        Loudest = 0x01
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraStretchTuneDepth : byte
    {
        [Description("Off")] Off     = 0x00,
        [Description("1")]   Depth01 = 0x01,
        [Description("2")]   Depth02 = 0x02,
        [Description("3")]   Depth03 = 0x03
    }

    public enum IntegraPortamentoType : byte
    {
        Rate = 0x00,
        Time = 0x01
    }

    public enum IntegraPortamentoStart : byte
    {
        Pitch = 0x00,
        Note  = 0x01
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraMatrixControlSource : byte
    {
        [Description("Off")]
        Off = 0,
        [Description("CC01: Modulation")]
        CC01 = 1,
        [Description("CC02: Breath")]
        CC02 = 2,
        CC03 = 3,
        [Description("CC04: Foot Type")]
        CC04 = 4,
        [Description("CC05: Portamento Time")]
        CC05 = 5,
        [Description("CC06: Data Entry")]
        CC06 = 6,
        [Description("CC07: Volume")]
        CC07 = 7,
        CC08 = 8,
        CC09 = 9,
        [Description("CC10: Panpot")]
        CC10 = 10,
        [Description("CC11: Expression")]
        CC11 = 11,
        [Description("CC12: Motional Surround Control 1")]
        CC12 = 12,
        [Description("CC13: Motional Surround Control 2")]
        CC13 = 13,
        [Description("CC14: Motional Surround Control 3")]
        CC14 = 14,
        CC15 = 15,
        [Description("CC16: General Purpose Controller 1 (Tone Modify 1)")]
        CC16 = 16,
        [Description("CC17: General Purpose Controller 2 (Tone Modify 2)")]
        CC17 = 17,
        [Description("CC18: General Purpose Controller 3 (Tone Modify 3)")]
        CC18 = 18,
        [Description("CC19: General Purpose Controller 4 (Tone Modify 4)")]
        CC19 = 19,
        CC20 = 20,
        CC21 = 21,
        CC22 = 22,
        CC23 = 23,
        CC24 = 24,
        CC25 = 25,
        CC26 = 26,
        CC27 = 27,
        [Description("CC28: Motional Surround External Part Control 1")]
        CC28 = 28,
        [Description("CC29: Motional Surround External Part Control 2")]
        CC29 = 29,
        [Description("CC30: Motional Surround External Part Control 3")]
        CC30 = 30,
        CC31 = 31,
        CC33 = 33,
        CC34 = 34,
        CC35 = 35,
        CC36 = 36,
        CC37 = 37,
        [Description("CC38: Data Entry")]
        CC38 = 38,
        CC39 = 39,
        CC40 = 40,
        CC41 = 41,
        CC42 = 42,
        CC43 = 43,
        CC44 = 44,
        CC45 = 45,
        CC46 = 46,
        CC47 = 47,
        CC48 = 48,
        CC49 = 49,
        CC50 = 50,
        CC51 = 51,
        CC52 = 52,
        CC53 = 53,
        CC54 = 54,
        CC55 = 55,
        CC56 = 56,
        CC57 = 57,
        CC58 = 58,
        CC59 = 59,
        CC60 = 60,
        CC61 = 61,
        CC62 = 62,
        CC63 = 63,
        [Description("CC64: Hold 1")]
        CC64 = 64,
        [Description("CC65: Portamento")]
        CC65 = 65,
        [Description("CC66: Sostenuto")]
        CC66 = 66,
        [Description("CC67: Soft")]
        CC67 = 67,
        [Description("CC68: Legato Foot Switch")]
        CC68 = 68,
        [Description("CC69: Hold 2")]
        CC69 = 69,
        CC70 = 70,
        [Description("CC71: Resonance")]
        CC71 = 71,
        [Description("CC72: Release Time")]
        CC72 = 72,
        [Description("CC73: Attack Time")]
        CC73 = 73,
        [Description("CC74: Cutoff")]
        CC74 = 74,
        [Description("CC75: Decay Time")]
        CC75 = 75,
        [Description("CC76: Vibrato Rate")]
        CC76 = 76,
        [Description("CC77: Vibrato Depth")]
        CC77 = 77,
        [Description("CC78: Vibrato Delay")]
        CC78 = 78,
        CC79 = 79,
        [Description("CC80: General Purpose Controller 5 (Tone Variation 1)")]
        CC80 = 80,
        [Description("CC81: General Purpose Controller 6 (Tone Variation 2)")]
        CC81 = 81,
        [Description("CC82: General Purpose Controller 7 (Tone Variation 3)")]
        CC82 = 82,
        [Description("CC83: General Purpose Controller 8 (Tone Variation 4)")]
        CC83 = 83,
        [Description("CC84: Portamento Control")]
        CC84 = 84,
        CC85 = 85,
        CC86 = 86,
        CC87 = 87,
        CC88 = 88,
        CC89 = 89,
        CC90 = 90,
        [Description("CC91: General Purpose Effect 1 (Reverb Send Level)")]
        CC91 = 91,
        CC92 = 92,
        [Description("CC93: General Purpose Effect 3 (Chorus Send Level)")]
        CC93 = 93,
        CC94 = 94,
        CC95 = 95,
        [Description("Pitch Bend")]
        Bend = 96,
        [Description("Aftertouch")]
        Aft = 97,
        [Description("System Control 1")]
        Sys01 = 98,
        [Description("System Control 2")]
        Sys02 = 99,
        [Description("System Control 3")]
        Sys03 = 100,
        [Description("System Control 4")]
        Sys04 = 101,
        Velocity = 102,
        [Description("Key Follow")]
        KeyFollow = 103,
        Tempo = 104,
        [Description("LFO 1")]
        LFO01 = 105,
        [Description("LFO 2")]
        LFO02 = 106,
        [Description("Pitch Envelope")]
        Pitch = 107,
        [Description("TVF Envelope")]
        TVF = 108,
        [Description("TVA Envelope")]
        TVA = 109,
    }

    public enum IntegraMatrixControlDestination : byte
    {
        Off      = 0,
        PCH      = 1,
        CUT      = 2,
        RES      = 3,
        LEV      = 4,
        PAN      = 5,
        DRY      = 6,
        CHO      = 7,
        REV      = 8,
        PITLFO1  = 9,
        PITLFO2  = 10,
        TVFLFO1  = 11,
        TVFLFO2  = 12,
        TVALFO1  = 13,
        TFALFO2  = 14,
        PANLFO1  = 15,
        PANLFO2  = 16,
        RATELFO1 = 17,
        RATELFO2 = 18,
        PITATK   = 19,
        PITDCY   = 20,
        PITREL   = 21,
        TVFATK   = 22,
        TVFDCY   = 23,
        TVFREL   = 24,
        TVAATK   = 25,
        TVADCY   = 26,
        TVAREL   = 27,
        PMT      = 28,
        FXM      = 29,
        RES01    = 30,
        RES02    = 31,
        RES03    = 32,
        RES04    = 33
    }

    public enum IntegraWaveGroupType : byte
    {
        INT        = 0x00,
        SRX        = 0x01,
        Reserved01 = 0x02,
        Reserved02 = 0x03
    }

    public enum IntegraControlSwitch : byte
    {
        Off     = 0x00,
        On      = 0x01,
        Reverse = 0x02
    }

    public enum IntegraTVFFilterType : byte
    {
        OFF   = 0x00,
        LPF   = 0x01,
        BPF   = 0x02,
        HPF   = 0x03,
        PKG   = 0x04,
        LPF2  = 0x05,
        LPF3  = 0x06
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraVelocityCurve : byte
    {
        [Description("Fixed")] Fixed   = 0x00,
        [Description("1")]     Curve01 = 0x01,
        [Description("2")]     Curve02 = 0x02,
        [Description("3")]     Curve03 = 0x03,
        [Description("4")]     Curve04 = 0x04,
        [Description("5")]     Curve05 = 0x05,
        [Description("6")]     Curve06 = 0x06,
        [Description("7")]     Curve07 = 0x07
    }

    public enum IntegraEnvelopeMode : byte
    {
        NoSustain = 0x00,
        Sustain   = 0x01
    }

    public enum IntegraVelocityControl : byte
    {
        Off    = 0x00,
        On     = 0x01,
        Random = 0x02,
        Cycle  = 0x03
    }

    #endregion

    #region PCMSynthTonePartial

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraPCMSynthToneParts : byte
    {
        [Description("1")] Partial01 = 0x00,
        [Description("2")] Partial02 = 0x01,
        [Description("3")] Partial03 = 0x02,
        [Description("4")] Partial04 = 0x03,
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraDelayMode : byte
    {
        [Description("")] Normal       = 0x00,
        [Description("")] Hold         = 0x01,
        [Description("")] KeyOffNormal = 0x02,
        [Description("")] KeyOffDecay  = 0x03
    }

    
    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraBiasDirection : byte
    {
        [Description("Lower")]         Lower      = 0x00,
        [Description("Upper")]         Upper      = 0x01,
        [Description("Lower & Upper")] LowerUpper = 0x02,
        [Description("All")]           All        = 0x03
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraLFOWaveform : byte
    {
        [Description("")] SIN         = 0x00,
        [Description("")] TRI         = 0x01,
        [Description("")] SAWUP       = 0x02,
        [Description("")] SAWDOWN     = 0x03,
        [Description("")] SQR         = 0x04,
        [Description("")] RND         = 0x05,
        [Description("")] BENDUP      = 0x06,
        [Description("")] BENDDOWN    = 0x07,
        [Description("")] TRP         = 0x08,
        [Description("")] SH          = 0x09,
        [Description("")] CHS         = 0x0A,
        [Description("")] VSIN        = 0x0B,
        [Description("")] STEP        = 0x0C
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraLFOFadeMode : byte
    {
        [Description("ON <")]  ONIN    = 0x00,
        [Description("ON >")]  ONOUT   = 0x01,
        [Description("OFF <")] OFFIN   = 0x02,
        [Description("OFF >")] OFFOUT  = 0x03
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraStepLFOType : byte
    {
        [Description("Type 1")] Type1 = 0x00,
        [Description("Type 2")] Type2 = 0x01
    }


    #endregion

    #region PCMDrumKitPartial

    public enum IntegraAssignType : byte
    {
        Multi   = 0x00,
        Single  = 0x01
    }

    public enum IntegraPartialOutputAssign : byte
    {
        Part     = 0x00,
        CompEQ01 = 0x01,
        CompEQ02 = 0x02,
        CompEQ03 = 0x03,
        CompEQ04 = 0x04,
        CompEQ05 = 0x05,
        CompEQ06 = 0x06
    }

    public enum IntegraPartialVelocityControl : byte
    {
        Off    = 0x00,
        On     = 0x01,
        Random = 0x02,
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraPCMNoteIndex : byte
    {   [Description("0 A")]           A0,
        [Description("0 B\U0001D12D")] B0Flat,
        [Description("0 B")]           B0,
        [Description("1 C")]           C1,
        [Description("1 C\U0001D130")] C1Sharp,
        [Description("1 D")]           D1,
        [Description("1 E\U0001D12D")] E1Flat,
        [Description("1 E")]           E1,
        [Description("1 F")]           F1,
        [Description("1 F\U0001D130")] F1Sharp,
        [Description("1 G")]           G1,
        [Description("1 G\U0001D130")] G1Sharp,
        [Description("1 A")]           A1,
        [Description("1 B\U0001D12D")] B1Flat,
        [Description("1 B")]           B1,
        [Description("2 C")]           C2,
        [Description("2 C\U0001D130")] C2Sharp,
        [Description("2 D")]           D2,
        [Description("2 E\U0001D12D")] E2Flat,
        [Description("2 E")]           E2,
        [Description("2 F")]           F2,
        [Description("2 F\U0001D130")] F2Sharp,
        [Description("2 G")]           G2,
        [Description("2 G\U0001D130")] G2Sharp,
        [Description("2 A")]           A2,
        [Description("2 B\U0001D12D")] B2Flat,
        [Description("2 B")]           B2,
        [Description("2 C")]           C3,
        [Description("3 C\U0001D130")] C3Sharp,
        [Description("3 D")]           D3,
        [Description("3 E\U0001D12D")] E3Flat,
        [Description("3 E")]           E3,
        [Description("3 F")]           F3,
        [Description("3 F\U0001D130")] F3Sharp,
        [Description("3 G")]           G3,
        [Description("3 G\U0001D130")] G3Sharp,
        [Description("3 A")]           A3,
        [Description("3 B\U0001D12D")] B3Flat,
        [Description("3 B")]           B3,
        [Description("4 C")]           C4,
        [Description("4 C\U0001D130")] C4Sharp,
        [Description("4 D")]           D4,
        [Description("4 E\U0001D12D")] E4Flat,
        [Description("4 E")]           E4,
        [Description("4 F")]           F4,
        [Description("4 F\U0001D130")] F4Sharp,
        [Description("4 G")]           G4,
        [Description("4 G\U0001D130")] G4Sharp,
        [Description("4 A")]           A4,
        [Description("4 B\U0001D12D")] B4Flat,
        [Description("4 B")]           B4,
        [Description("5 C")]           C5,
        [Description("5 C\U0001D130")] C5Sharp,
        [Description("5 D")]           D5,
        [Description("5 E\U0001D12D")] E5Flat,
        [Description("5 E")]           E5,
        [Description("5 F")]           F5,
        [Description("5 F\U0001D130")] F5Sharp,
        [Description("5 G")]           G5,
        [Description("5 G\U0001D130")] G5Sharp,
        [Description("5 A")]           A5,
        [Description("5 B\U0001D12D")] B5Flat,
        [Description("5 B")]           B5,
        [Description("6 C")]           C6,
        [Description("6 C\U0001D130")] C6Sharp,
        [Description("6 D")]           D6,
        [Description("6 E\U0001D12D")] E6Flat,
        [Description("6 E")]           E6,
        [Description("6 F")]           F6,
        [Description("6 F\U0001D130")] F6Sharp,
        [Description("6 G")]           G6,
        [Description("6 G\U0001D130")] G6Sharp,
        [Description("6 A")]           A6,
        [Description("6 B\U0001D12D")] B6Flat,
        [Description("6 B")]           B6,
        [Description("7 C")]           C7,
        [Description("7 C\U0001D130")] C7Sharp,
        [Description("7 D")]           D7,
        [Description("7 E\U0001D12D")] E7Flat,
        [Description("7 E")]           E7,
        [Description("7 F")]           F7,
        [Description("7 F\U0001D130")] F7Sharp,
        [Description("7 G")]           G7,
        [Description("7 G\U0001D130")] G7Sharp,
        [Description("7 A")]           A7,
        [Description("7 B\U0001D12D")] B7Flat,
        [Description("7 B")]           B7,
        [Description("8 C")]           C8
    }

    #endregion

    #region MFX

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraMFXTypes : byte
    {
        [Description("Thru")]                               Thru                = 0x00,
        [Description("Equalizer")]                          Equalizer           = 0x01,
        [Description("Spectrum")]                           Spectrum            = 0x02,
        [Description("Low Boost")]                          LowBoost            = 0x03,
        [Description("Step Filter")]                        StepFilter          = 0x04,
        [Description("Enhancer")]                           Enhancer            = 0x05,
        [Description("Auto Wah")]                           AutoWah             = 0x06,
        [Description("Humanizer")]                          Humanizer           = 0x07,
        [Description("Speaker Simulator")]                  SpeakerSimulator    = 0x08,
        [Description("Phaser 1")]                           Phaser1             = 0x09,
        [Description("Phaser 2")]                           Phaser2             = 0x0A,
        [Description("Phaser 3")]                           Phaser3             = 0x0B,
        [Description("Step Phaser")]                        StepPhaser          = 0x0C,
        [Description("Multi Stage Phaser")]                 MultiStagePhaser    = 0x0D,
        [Description("Infinite Phaser")]                    InfinitePhaser      = 0x0E,
        [Description("Ring Modulator")]                     RingModulator       = 0x0F,
        [Description("Tremolo")]                            Tremolo             = 0x10,
        [Description("Auto Pan")]                           AutoPan             = 0x11,
        [Description("Slicer")]                             Slicer              = 0x12,
        [Description("Rotary 1")]                           Rotary1             = 0x13,
        [Description("Rotary 2")]                           Rotary2             = 0x14,
        [Description("Rotary 3")]                           Rotary3             = 0x15,
        [Description("Chorus")]                             Chorus              = 0x16,
        [Description("Flanger")]                            Flanger             = 0x17,
        [Description("Step Flanger")]                       StepFlanger         = 0x18,
        [Description("Hexa-Chorus")]                        HexaChorus          = 0x19,
        [Description("Tremolo Chorus")]                     TremoloChorus       = 0x1A,
        [Description("Space-D")]                            SpaceD = 27,
        [Description("Overdrive")]                          Overdrive = 28,
        [Description("Distortion")]                         Distortion = 29,
        [Description("Guitar Amp Simulator")]               GuitarAmpSimulator = 30,
        [Description("Compressor")]                         Compressor = 31,
        [Description("Limiter")]                            Limiter = 32,
        [Description("Gate")]                               Gate = 33,
        [Description("Delay")]                              Delay = 34,
        [Description("Modulation Delay")]                   ModulationDelay = 35,
        [Description("3 Tap Pan Delay")]                    TapPanDelay3 = 36,
        [Description("4 Tap Pan Delay")]                    TapPanDelay4 = 37,
        [Description("Multi Tap Delay")]                    MultiTapDelay = 38,
        [Description("Reverse Delay")]                      ReverseDelay = 39,
        [Description("Time Control Delay")]                 TimeControlDelay = 40,
        [Description("LOFI Compressor")]                    LoFiCompressor = 41,
        [Description("Bit Crasher")]                        BitCrasher = 42,
        [Description("Pitch Shifter")]                      PitchShifter = 43,
        [Description("2 Voice Pitch Shifter")]              VoicePitchShifter = 44,
        [Description("Overdrive -> Chorus")]                OverdriveChorus = 45,
        [Description("Overdrive -> Flanger")]               OverdriveFlanger = 46,
        [Description("Overdrive -> Delay")]                 OverdriveDelay = 47,
        [Description("Distortion -> Chorus")]               DistortionChorus = 48,
        [Description("Distortion -> Flanger")]              DistortionFlanger = 49,
        [Description("Distortion -> Delay")]                DistortionDelay = 50,
        [Description("Overdrive / Distortion Touch Wah")]   ODDSTouchWah        = 51,
        [Description("Overdrive / Distortion Auto Wah")]    ODDSAutoWah = 52,
        [Description("Guitar Amp Simulator -> Chorus")]     GuitarAmpSimChorus = 53,
        [Description("Guitar Amp Simulator -> Flanger")]    GuitarAmpSimFlanger = 54,
        [Description("Guitar Amp Simulator -> Phaser")]     GuitarAmpSimPhaser = 55,
        [Description("Guitar Amp Simulator -> Delay")]      GuitarAmpSimDelay = 56,
        [Description("EP Amp Simulator -> Tremolo")]        EPAmpSimTremolo = 57,
        [Description("EP Amp Simulator -> Chorus")]         EPAmpSimChorus = 58,
        [Description("EP Amp Simulator -> Flanger")]        EPAmpSimFlanger = 59,
        [Description("EP Amp Simulator -> Phaser")]         EPAmpSimPhaser = 60,
        [Description("EP Amp Simulator -> Delay")]          EPAmpSimDelay = 61,
        [Description("Enhancer -> Chorus")]                 EnhancerChorus = 62,
        [Description("Enhancer -> Flanger")]                EnhancerFlanger = 63,
        [Description("Enhancer -> Delay")]                  EnhancerDelay = 64,
        [Description("Chorus -> Delay")]                    ChorusDelay = 65,
        [Description("Flanger -> Delay")]                   FlangerDelay = 66,
        [Description("Chorus -> Flanger")]                  ChorusFlanger = 67
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraMFXControlSources : byte
    {
        [Description("Off")]
        Off = 0,
        [Description("CC01: Modulation")]
        CC01 = 1,
        [Description("CC02: Breath")]
        CC02 = 2,
        CC03 = 3,
        [Description("CC04: Foot Type")]
        CC04 = 4,
        [Description("CC05: Portamento Time")]
        CC05 = 5,
        [Description("CC06: Data Entry")]
        CC06 = 6,
        [Description("CC07: Volume")]
        CC07 = 7,
        CC08 = 8,
        CC09 = 9,
        [Description("CC10: Panpot")]
        CC10 = 10,
        [Description("CC11: Expression")]
        CC11 = 11,
        [Description("CC12: Motional Surround Control 1")]
        CC12 = 12,
        [Description("CC13: Motional Surround Control 2")]
        CC13 = 13,
        [Description("CC14: Motional Surround Control 3")]
        CC14 = 14,
        CC15 = 15,
        [Description("CC16: General Purpose Controller 1 (Tone Modify 1)")]
        CC16 = 16,
        [Description("CC17: General Purpose Controller 2 (Tone Modify 2)")]
        CC17 = 17,
        [Description("CC18: General Purpose Controller 3 (Tone Modify 3)")]
        CC18 = 18,
        [Description("CC19: General Purpose Controller 4 (Tone Modify 4)")]
        CC19 = 19,
        CC20 = 20,
        CC21 = 21,
        CC22 = 22,
        CC23 = 23,
        CC24 = 24,
        CC25 = 25,
        CC26 = 26,
        CC27 = 27,
        [Description("CC28: Motional Surround External Part Control 1")]
        CC28 = 28,
        [Description("CC29: Motional Surround External Part Control 2")]
        CC29 = 29,
        [Description("CC30: Motional Surround External Part Control 3")]
        CC30 = 30,
        CC31 = 31,
        CC33 = 33,
        CC34 = 34,
        CC35 = 35,
        CC36 = 36,
        CC37 = 37,
        [Description("CC38: Data Entry")]
        CC38 = 38,
        CC39 = 39,
        CC40 = 40,
        CC41 = 41,
        CC42 = 42,
        CC43 = 43,
        CC44 = 44,
        CC45 = 45,
        CC46 = 46,
        CC47 = 47,
        CC48 = 48,
        CC49 = 49,
        CC50 = 50,
        CC51 = 51,
        CC52 = 52,
        CC53 = 53,
        CC54 = 54,
        CC55 = 55,
        CC56 = 56,
        CC57 = 57,
        CC58 = 58,
        CC59 = 59,
        CC60 = 60,
        CC61 = 61,
        CC62 = 62,
        CC63 = 63,
        [Description("CC64: Hold 1")]
        CC64 = 64,
        [Description("CC65: Portamento")]
        CC65 = 65,
        [Description("CC66: Sostenuto")]
        CC66 = 66,
        [Description("CC67: Soft")]
        CC67 = 67,
        [Description("CC68: Legato Foot Switch")]
        CC68 = 68,
        [Description("CC69: Hold 2")]
        CC69 = 69,
        CC70 = 70,
        [Description("CC71: Resonance")]
        CC71 = 71,
        [Description("CC72: Release Time")]
        CC72 = 72,
        [Description("CC73: Attack Time")]
        CC73 = 73,
        [Description("CC74: Cutoff")]
        CC74 = 74,
        [Description("CC75: Decay Time")]
        CC75 = 75,
        [Description("CC76: Vibrato Rate")]
        CC76 = 76,
        [Description("CC77: Vibrato Depth")]
        CC77 = 77,
        [Description("CC78: Vibrato Delay")]
        CC78 = 78,
        CC79 = 79,
        [Description("CC80: General Purpose Controller 5 (Tone Variation 1)")]
        CC80 = 80,
        [Description("CC81: General Purpose Controller 6 (Tone Variation 2)")]
        CC81 = 81,
        [Description("CC82: General Purpose Controller 7 (Tone Variation 3)")]
        CC82 = 82,
        [Description("CC83: General Purpose Controller 8 (Tone Variation 4)")]
        CC83 = 83,
        [Description("CC84: Portamento Control")]
        CC84 = 84,
        CC85 = 85,
        CC86 = 86,
        CC87 = 87,
        CC88 = 88,
        CC89 = 89,
        CC90 = 90,
        [Description("CC91: General Purpose Effect 1 (Reverb Send Level)")]
        CC91 = 91,
        CC92 = 92,
        [Description("CC93: General Purpose Effect 3 (Chorus Send Level)")]
        CC93 = 93,
        CC94 = 94,
        CC95 = 95,
        [Description("Pitch Bend")]
        Bend = 96,
        [Description("Aftertouch")]
        Aft = 97,
        [Description("System 1")]
        Sys01 = 98,
        [Description("System 2")]
        Sys02 = 99,
        [Description("System 3")]
        Sys03 = 100,
        [Description("System 4")]
        Sys04 = 101,
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraMFXControlAssigns : byte
    {
        [Description("Off")] Off,
        [Description("01")]  Channel1,
        [Description("02")]  Channel2,
        [Description("03")]  Channel3,
        [Description("04")]  Channel4,
        [Description("05")]  Channel5,
        [Description("06")]  Channel6,
        [Description("07")]  Channel7,
        [Description("08")]  Channel8,
        [Description("09")]  Channel9,
        [Description("10")]  Channel10,
        [Description("11")]  Channel11,
        [Description("12")]  Channel12,
        [Description("13")]  Channel13,
        [Description("14")]  Channel14,
        [Description("15")]  Channel15,
        [Description("16")]  Channel16,
    }

    #endregion

    #region SuperNATURALSynthTonePartial

    public enum IntegraSNSynthToneParts : int
    {
        [Description("Partial 01")] Partial01 = 0x00002000,
        [Description("Partial 02")] Partial02 = 0x00002100,
        [Description("Partial 03")] Partial03 = 0x00002200
    }

    public enum IntegraOSCWave : byte
    {
        SAW      = 0x00,
        SQR      = 0x01,
        PWSQR    = 0x02,
        TRI      = 0x03,
        SINE     = 0x04,
        NOISE    = 0x05,
        SUPERSAW = 0x06,
        PCM      = 0x07
    }

    public enum IntegraOSCWaveVariation : byte
    {
        A = 0x00,
        B = 0x01,
        C = 0x02
    }

    public enum IntegraFilterMode : byte
    {
        BYPASS      = 0x00,
        LPF         = 0x01,
        HPF         = 0x02,
        BPF         = 0x03,
        PKG         = 0x04,
        LPF02       = 0x05,
        LPF03       = 0x06,
        LPF04       = 0x07
    }

    public enum IntegraLFOShape : byte
    {
        TRI     = 0x00,
        SIN     = 0x01,
        SAW     = 0x02,
        SQR     = 0x03,
        SH      = 0x04,
        RND     = 0x05
    }

    #endregion

    #region Misc

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraKeyRange : byte
    { 
        [Description("C -")]           C_  = 0x00,
        [Description("C#-")] C_S = 0x01,
        [Description("D -")]           D_  = 0x02,
        [Description("Eb-")] E_F = 0x03,
        [Description("E -")]           E_  = 0x04,
        [Description("F -")]           F_  = 0x05,
        [Description("F#-")] F_S = 0x06,
        [Description("G -")]           G_  = 0x07,
        [Description("G#-")] G_S = 0x08,
        [Description("A-")]           A_  = 0x09,
        [Description("Bb-")] B_F = 0x0A,
        [Description("B-")]           B_  = 0x0B,
        [Description("C0")]           C0  = 0x0C,
        [Description("C\U0001D1300")] C0S = 0x0D,
        [Description("D0")]           D0  = 0x0E,
        [Description("E\U0001D12D0")] E0F = 0x0F,
        [Description("E0")]           E0  = 0x10,
        [Description("F0")]           F0  = 0x11,
        [Description("F\U0001D1300")] F0S = 0x12,
        [Description("G0")]           G0  = 0x13,
        [Description("G\U0001D1300")] G0S = 0x14,
        [Description("A0")]           A0  = 0x15,
        [Description("B\U0001D12D0")] B0F = 0x16,
        [Description("B0")]           B0  = 0x17,
        [Description("C1")]           C1  = 0x18,
        [Description("C\U0001D1301")] C1S = 0x19,
        [Description("D1")]           D1  = 0x1A,
        [Description("E\U0001D12D1")] E1F = 0x1B,
        [Description("E1")]           E1  = 0x1C,
        [Description("F1")]           F1  = 0x1D,
        [Description("F\U0001D1301")] F1S = 0x1E,
        [Description("G1")]           G1  = 0x1F,
        [Description("G\U0001D1301")] G1S = 0x20,
        [Description("A1")]           A1  = 0x21,
        [Description("B\U0001D12D1")] B1F = 0x22,
        [Description("B1")]           B1  = 0x23,
        [Description("C2")]           C2  = 0x24,
        [Description("C\U0001D1302")] C2S = 0x25,
        [Description("D2")]           D2  = 0x26,
        [Description("E\U0001D12D2")] E2F = 0x27,
        [Description("E2")] E2    = 0x28,
        [Description("F2")] F2    = 0x29,
        [Description("F\U0001D1302")] FS2   = 0x2A,
        [Description("G2")] G2    = 0x2B,
        [Description("G\U0001D1302")] GS2   = 0x2C,
        [Description("A2")] A2    = 0x2D,
        [Description("B\U0001D12D2")] BF2   = 0x2E,
        [Description("B2")] B2    = 0x2F,
        [Description("C3")] C3    = 0x30,
        [Description("C\U0001D1303")] CS3   = 0x31,
        [Description("D3")] D3    = 0x32,
        [Description("E\U0001D12D3")] EF3   = 0x33,
        [Description("E3")] E3    = 0x34,
        [Description("F3")] F3    = 0x35,
        [Description("F\U0001D1303")] FS3   = 0x36,
        [Description("G3")] G3    = 0x37,
        [Description("G\U0001D1303")] GS3   = 0x38,
        [Description("A3")] A3    = 0x39,
        [Description("B\U0001D12D3")] BF3   = 0x3A,
        [Description("B3")] B3    = 0x3B,
        [Description("C4")] C4    = 0x3C,
        [Description("C\U0001D1304")] CS4   = 0x3D,
        [Description("D4")] D4    = 0x3E,
        [Description("E\U0001D12D4")] EF4   = 0x3F,
        [Description("E4")] E4    = 0x40,
        [Description("F4")] F4    = 0x41,
        [Description("F\U0001D1304")] FS4   = 0x42,
        [Description("G4")] G4    = 0x43,
        [Description("G\U0001D1304")] GS4   = 0x44,
        [Description("A4")] A4    = 0x45,
        [Description("B\U0001D12D4")] BF4   = 0x46,
        [Description("B4")] B4    = 0x47,
        [Description("C5")] C5    = 0x48,
        [Description("C\U0001D1305")] CS5   = 0x49,
        [Description("D5")] D5    = 0x4A,
        [Description("E\U0001D12D5")] EF5   = 0x4B,
        [Description("E5")] E5    = 0x4C,
        [Description("F5")] F5    = 0x4D,
        [Description("F\U0001D1305")] FS5   = 0x4E,
        [Description("G5")] G5    = 0x4F,
        [Description("G\U0001D1305")] GS5   = 0x50,
        [Description("A5")] A5    = 0x51,
        [Description("B\U0001D12D5")] BF5   = 0x52,
        [Description("B5")] B5    = 0x53,
        [Description("C6")] C6    = 0x54,
        [Description("C\U0001D1306")] CS6   = 0x55,
        [Description("D6")] D6    = 0x56,
        [Description("E\U0001D12D6")] EF6   = 0x57,
        [Description("E6")] E6    = 0x58,
        [Description("F6")] F6    = 0x59,
        [Description("F\U0001D1306")] FS6   = 0x5A,
        [Description("G6")] G6    = 0x5B,
        [Description("G\U0001D1306")] GS6   = 0x5C,
        [Description("A6")] A6    = 0x5D,
        [Description("B\U0001D12D6")] BF6   = 0x5E,
        [Description("B6")] B6    = 0x5F,
        [Description("C7")] C7    = 0x60,
        [Description("C\U0001D1307")] CS7   = 0x61,
        [Description("D7")] D7    = 0x62,
        [Description("E\U0001D12D7")] EF7   = 0x63,
        [Description("E7")] E7    = 0x64,
        [Description("F7")] F7    = 0x65,
        [Description("F\U0001D1307")] FS7   = 0x66,
        [Description("G7")] G7    = 0x67,
        [Description("G\U0001D1307")] GS7   = 0x68,
        [Description("A7")] A7    = 0x69,
        [Description("B\U0001D12D7")] BF7   = 0x6A,
        [Description("B7")] B7    = 0x6B,
        [Description("C8")] C8    = 0x6C,
        [Description("C\U0001D1308")] CS8   = 0x6D,
        [Description("D8")] D8    = 0x6E,
        [Description("E\U0001D12D8")] EF8   = 0x6F,
        [Description("E8")] E8    = 0x70,
        [Description("F8")] F8    = 0x71,
        [Description("F\U0001D1308")] FS8   = 0x72,
        [Description("G8")] G8    = 0x73,
        [Description("G\U0001D1308")] GS8   = 0x74,
        [Description("A8")] A8    = 0x75,
        [Description("B\U0001D12D8")] BF8   = 0x76,
        [Description("B8")] B8    = 0x77,
        [Description("C9")] C9    = 0x78,
        [Description("C\U0001D1309")] CS9   = 0x79,
        [Description("D9")] D9    = 0x7A,
        [Description("E\U0001D12D9")] EF9   = 0x7B,
        [Description("E9")] E9    = 0x7C,
        [Description("F9")] F9    = 0x7D,
        [Description("F\U0001D1309")] FS9   = 0x7E,
        [Description("G9")] G9    = 0x7F,
    }

    #endregion
}
