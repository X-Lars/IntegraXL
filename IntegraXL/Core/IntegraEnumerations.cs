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

    public class IntegraPitchDepths : Enumeration
    {
        public new static List<string> Values
        {
            get
            {
                List<string> values = new();

                // 0.. 9
                for (int i = 0; i < 10; i++)
                {
                    values.Add($"{i}");
                }

                // 10 .. 90
                for (int i = 1; i < 10; i++)
                {
                    values.Add($"{i * 10}");
                }

                for (int i = 1; i < 10; i++)
                {
                    values.Add($"{i * 100}");
                }

                values.Add($"1000");
                values.Add($"1100");
                values.Add($"1200");

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

    public enum IntegraPCMWaveGroups : int
    {
        SRX01 = 1,
        SRX02 = 2,
        SRX03 = 3,
        SRX04 = 4,
        SRX05 = 5,
        SRX06 = 6,
        SRX07 = 7,
        SRX08 = 8,
        SRX09 = 9,
        SRX10 = 10,
        SRX11 = 11,
        SRX12 = 12,
    }

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
    public enum SoloParts : byte
    {
        [Description("Off")]     Off    = 0x00,
        [Description("Part 1")]  Part1  = 0x01,
        [Description("Part 2")]  Part2  = 0x02,
        [Description("Part 3")]  Part3  = 0x03,
        [Description("Part 4")]  Part4  = 0x04,
        [Description("Part 5")]  Part5  = 0x05,
        [Description("Part 6")]  Part6  = 0x06,
        [Description("Part 7")]  Part7  = 0x07,
        [Description("Part 8")]  Part8  = 0x08,
        [Description("Part 9")]  Part9  = 0x09,
        [Description("Part 10")] Part10 = 0x0A,
        [Description("Part 11")] Part11 = 0x0B,
        [Description("Part 12")] Part12 = 0x0C,
        [Description("Part 13")] Part13 = 0x0D,
        [Description("Part 14")] Part14 = 0x0E,
        [Description("Part 15")] Part15 = 0x0F,
        [Description("Part 16")] Part16 = 0x10
    }

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
    public enum IntegraTemporaryToneCategories : byte
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
    public enum IntegraToneCategories : byte
    {
        [Description("No Assign")]              Unassigned          = 0x00,
        [Description("Acoustic Piano")]         AcousticPiano       = 0x01,
        [Description("Electric Piano")]         ElectricPiano       = 0x02,
        [Description("Organ")]                  Organ               = 0x03,
        [Description("Other Keys")]             OtherKeys           = 0x04,
        [Description("Accordion & Harmonica")]  AccordionHarmonica  = 0x05,
        [Description("Bells & Mallet")]         BellMallet          = 0x06,
        [Description("Acoustic Guitar")]        AcousticGuitar      = 0x07,
        [Description("Electric Guitar")]        ElectricGuitar      = 0x08,
        [Description("Distortion Guitar")]      DistortionGuitar    = 0x09,
        [Description("Acoustic Bass")]          AcousticBass        = 0x0A,
        [Description("Electric Bass")]          ElectricBass        = 0x0B,
        [Description("Synth Bass")]             SynthBass           = 0x0C,
        [Description("Plucked & Strokes")]      PluckedStroke       = 0x0D,
        [Description("Strings")]                Strings             = 0x0E,
        [Description("Brass")]                  Brass               = 0x0F,
        [Description("Wind")]                   Wind                = 0x10,
        [Description("Flute")]                  Flute               = 0x11,
        [Description("Sax")]                    Sax                 = 0x12,
        [Description("Recorder")]               Recorder            = 0x13,
        [Description("Vox & Choir")]            VoxChoir            = 0x14,
        [Description("Synth Lead")]             SynthLead           = 0x15,
        [Description("Synth Brass")]            SynthBrass          = 0x16,
        [Description("Synth Pad Strings")]      SynthPadStrings     = 0x17,
        [Description("Synth Bell Pads")]        SynthBellpad        = 0x18,
        [Description("Synth Poly Keys")]        SynthPolyKey        = 0x19,
        [Description("FX")]                     FX                  = 0x1A,
        [Description("Synth Sequence Pop")]     SynthSeqPop         = 0x1B,
        [Description("Phrase")]                 Phrase              = 0x1C,
        [Description("Pulstating")]             Pulstating          = 0x1D,
        [Description("Beat & Grooves")]         BeatGroove          = 0x1E,
        [Description("Hit")]                    Hit                 = 0x1F,
        [Description("Sound FX")]               SoundFX             = 0x20,
        [Description("Drums")]                  Drums               = 0x21,
        [Description("Percussion")]             Percussion          = 0x22,
        [Description("Combination")]            Combination         = 0x23
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraSNDPhrase : int
    {
        [Description("00: No Assign")]                NoAssign       = 0x00000000,
        [Description("01: Standard 01")]              Standard01     = 0x00000001,
        [Description("02: Jazz 01")]                  Jazz01         = 0x00000002,
        [Description("03: Brush 01")]                 Brush01        = 0x00000003,
        [Description("04: Orchestra 01")]             Orchestra01    = 0x00000004,
        [Description("05: Standard 02")]              Standard02     = 0x00000005,
        [Description("06: Standard 03")]              Standard03     = 0x00000006,
        [Description("07: Standard 03 + Percussion")] Standard03Perc = 0x00000007,
        [Description("08: Rock 01")]                  Rock01         = 0x00000008,
        [Description("09: Rock 01 + Percussion")]     Rock01Perc     = 0x00000009,
        [Description("10: Rock 02")]                  Rock02         = 0x0000000A,
        [Description("10: Rock 02 + Percussion")]     Rock02Perc     = 0x0000000B,
        [Description("10: Jazz 02")]                  Jazz02         = 0x0000000C,
        [Description("10: Jazz 02 + Percussion")]     Jazz02Perc     = 0x0000000D,
        [Description("10: Brush 02")]                 Brush02        = 0x0000000E,
        [Description("10: Brush 02 + Percussion")]    Brush02Perc    = 0x0000000F,
        [Description("10: Orchestra02")]              Orchestra02    = 0x00000100
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraPCMDrumKitPhrase : int
    {
        [Description("00: No Assign")]    NoAssign    = 0x00000000,
        [Description("01: Standard 01")]  Standard01  = 0x00000001,
        [Description("02: Standard 02")]  Standard02  = 0x00000002,
        [Description("03: Room 01")]      Room01      = 0x00000003,
        [Description("04: Room 02")]      Room02      = 0x00000004,
        [Description("05: Power 01")]     Power01     = 0x00000005,
        [Description("06: Power 02")]     Power02     = 0x00000006,
        [Description("07: Electric 01")]  Electric01  = 0x00000007,
        [Description("08: Electric 02")]  Electric02  = 0x00000008,
        [Description("09: Analog 01")]    Analog01    = 0x00000009,
        [Description("10: Analog 02")]    Analog02    = 0x0000000A,
        [Description("11: Jazz 01")]      Jazz01      = 0x0000000B,
        [Description("12: Jazz 02")]      Jazz02      = 0x0000000C,
        [Description("13: Brush 01")]     Brush01     = 0x0000000D,
        [Description("14: Brush 02")]     Brush02     = 0x0000000E,
        [Description("15: Orchestra 01")] Orchestra01 = 0x0000000F,
        [Description("16: Orchestra 02")] Orchestra02 = 0x00000100,
        [Description("17: SFX 01")]       SFX01       = 0x00000101,
        [Description("18: SFX 02")]       SFX02       = 0x00000102,
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraSNAPhrase : short
    {
        [Description("00: No Assign")]              NoAssign     = 0x0000,
        [Description("01: Acoustic Piano 01")]      AcPiano01    = 0x0001,
        [Description("02: Acoustic Piano 02")]      AcPiano02    = 0x0002,
        [Description("03: Electric Piano 01")]      EPiano01     = 0x0003,
        [Description("04: Electric Piano 02")]      EPiano02     = 0x0004,
        [Description("05: Clav")]                   Clav         = 0x0005,
        [Description("06: Glockenspiel")]           Glockenspiel = 0x0006,
        [Description("07: Vibraphone")]             Vibraphone   = 0x0007,
        [Description("08: Marimaba")]               Marimba      = 0x0008,
        [Description("09: Xylophone")]              Xylophone    = 0x0009,
        [Description("10: Tubular Bells")]          TubularBells = 0x000A,
        [Description("11: Santoor")]                Santoor      = 0x000B,
        [Description("12: Organ 01")]               Organ01      = 0x000C,
        [Description("13: Organ 02")]               Organ02      = 0x000D,
        [Description("14: Accordeon")]              Accordion    = 0x000E,
        [Description("15: Bandoneon")]              Bandoneon    = 0x000F,
        [Description("16: Harmonica")]              Harmonica    = 0x0100,
        [Description("17: Nylon Guitar")]           NylonGuitar  = 0x0101,
        [Description("18: Flamenco Guitar")]        FlamencoGt   = 0x0102,
        [Description("19: Ukelele")]                Ukelele      = 0x0103,
        [Description("20: Steel String Guitar 01")] SteelStrGt01 = 0x0104,
        [Description("21: Steel String Guitar 02")] SteelStrGt02 = 0x0105,
        [Description("22: Steel String Guitar 03")] SteelStrGt03 = 0x0106,
        [Description("23: Mandolin")]               Mandolin     = 0x0107,
        [Description("24: Jazz Guitar")]            JazzGuitar   = 0x0108,
        [Description("25: Electric Guitar 01")]     EGuitar01    = 0x0109,
        [Description("26: Electric Guitar 02")]     EGuitar02    = 0x010A,
        [Description("27: Electric Guitar 03")]     EGuitar03    = 0x010B,
        [Description("28: Electric Guitar 04")]     EGuitar04    = 0x010C,
        [Description("29: Acoustic Bass")]          AcBass       = 0x010D,
        [Description("30: Fingered Bass")]          FingeredBass = 0x010E,
        [Description("31: Picked Bass")]            PickedBass   = 0x010F,
        [Description("32: Fretless Bass")]          FretlessBass = 0x0200,
        [Description("33: Violin")]                 Violin       = 0x0201,
        [Description("34: Viola")]                  Viola        = 0x0202,
        [Description("35: Cello")]                  Cello        = 0x0203,
        [Description("36: Contrabass")]             Contrabass   = 0x0204,
        [Description("37: Solo Pizzicato")]         SoloPizz     = 0x0205,
        [Description("38: Harp")]                   Harp         = 0x0206,
        [Description("39: Yang Chin")]              YangChin     = 0x0207,
        [Description("40: Timpani")]                Timpani      = 0x0208,
        [Description("41: Strings")]                Strings      = 0x0209,
        [Description("42: Strings Pizzicato")]      StringsPizz  = 0x020A,
        [Description("43: Choir")]                  Choir        = 0x020B,
        [Description("44: Trumpet 01")]             Trumpet01    = 0x020C,
        [Description("45: Trumpet 02")]             Trumpet02    = 0x020D,
        [Description("46: Trumpet 03")]             Trumpet03    = 0x020E,
        [Description("47: Trumpet 04")]             Trumpet04    = 0x020F,
        [Description("48: Trombone")]               Trombone     = 0x0300,
        [Description("49: Tuba")]                   Tuba         = 0x0301,
        [Description("50: Muted Trumpet")]          MuteTrumpet  = 0x0302,
        [Description("51: French Horn")]            FrenchHorn   = 0x0303,
        [Description("52: Soprano Sax")]            SopranoSax   = 0x0304,
        [Description("53: Alto Sax")]               AltoSax      = 0x0305,
        [Description("54: Tenor Sax")]              TenorSax     = 0x0306,
        [Description("55: Baritone Sax")]           BaritoneSax  = 0x0307,
        [Description("56: Oboe")]                   Oboe         = 0x0308,
        [Description("57: English Horn")]           EnglishHorn  = 0x0309,
        [Description("58: Bassoon")]                Bassoon      = 0x030A,
        [Description("59: Clarinet 01")]            Clarinet01   = 0x030B,
        [Description("60: Clarinet 02")]            Clarinet02   = 0x030C,
        [Description("61: Piccolo")]                Piccolo      = 0x030D,
        [Description("62: Flute")]                  Flute        = 0x030E,
        [Description("63: Recorder")]               Recorder     = 0x030F,
        [Description("64: Pan Flute")]              PanFlute     = 0x0400,
        [Description("65: Tin Wistle")]             TinWhistle   = 0x0401,
        [Description("66: Shakuhachi")]             Shakuhachi   = 0x0402,
        [Description("67: Ryuteki")]                Ryuteki      = 0x0403,
        [Description("68: Ocarina")]                Ocarina      = 0x0404,
        [Description("69: Sitar")]                  Sitar        = 0x0405,
        [Description("70: Tsugaru")]                Tsugaru      = 0x0406,
        [Description("71: Sansin")]                 Sansin       = 0x0407,
        [Description("72: Koto")]                   Koto         = 0x0408,
        [Description("73: Taisho Koto")]            TaishoKoto   = 0x0409,
        [Description("74: Kalimba")]                Kalimba      = 0x040A,
        [Description("75: Pipes 01")]               Pipes01      = 0x040B,
        [Description("76: Pipes 02")]               Pipes02      = 0x040C,
        [Description("77: Erhu")]                   Erhu         = 0x040D,
        [Description("78: Sarangi")]                Sarangi      = 0x040E,
        [Description("79: Steel Drums")]            SteelDrums   = 0x040F,
        [Description("80: One Shot")]               OneShot      = 0x0500,
        [Description("81: Gliss")]                  Gliss        = 0x0501,
        [Description("82: Wah Clav")]               WahClav      = 0x0502,
        [Description("83: Rasgueado")]              Rasgueado    = 0x0503,
        [Description("84: Octave Jazz Guitar")]     OctJazzGt    = 0x0504,
        [Description("85: Wah Cut Guitar")]         WahCutGt     = 0x0505,
        [Description("86: Wah OD Guitar")]          WahODGt      = 0x0506,
        [Description("87: Wah DS Guitar")]          WahDSGt      = 0x0507
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraSynthPhrase : int
    {
        [Description("000: No Assing")]              NoAssign           = 0x00000000,
        [Description("001: Piano 01")]               Piano01            = 0x00000001,
        [Description("002: Piano 02")]               Piano02            = 0x00000002,
        [Description("003: Piano 03")]               Piano03            = 0x00000003,
        [Description("004: Piano 04")]               Piano04            = 0x00000004,
        [Description("005: Piano 05")]               Piano05            = 0x00000005,
        [Description("006: Piano 06")]               Piano06            = 0x00000006,
        [Description("007: Piano 07")]               Piano07            = 0x00000007,
        [Description("008: Piano 08")]               Piano08            = 0x00000008,
        [Description("009: Piano 09")]               Piano09            = 0x00000009,
        [Description("010: Piano 10")]               Piano10            = 0x0000000A,
        [Description("011: Electric Piano 01")]      EPiano01           = 0x0000000B,
        [Description("012: Electric Piano 02")]      EPiano02           = 0x0000000C,
        [Description("013: Electric Piano 03")]      EPiano03           = 0x0000000D,
        [Description("014: Electric Piano 04")]      EPiano04           = 0x0000000E,
        [Description("015: Electric Piano 05")]      EPiano05           = 0x0000000F,
        [Description("016: Electric Piano 06")]      EPiano06           = 0x00000100,
        [Description("017: Electric Organ 01")]      EOrgan01           = 0x00000101,
        [Description("018: Electric Organ 02")]      EOrgan02           = 0x00000102,
        [Description("019: Electric Organ 03")]      EOrgan03           = 0x00000103,
        [Description("020: Electric Organ 04")]      EOrgan04           = 0x00000104,
        [Description("021: Electric Organ 05")]      EOrgan05           = 0x00000105,
        [Description("022: Electric Organ 06")]      EOrgan06           = 0x00000106,
        [Description("023: Electric Organ 07")]      EOrgan07           = 0x00000107,
        [Description("024: Electric Organ 08")]      EOrgan08           = 0x00000108,
        [Description("025: Electric Organ 09")]      EOrgan09           = 0x00000109,
        [Description("026: Electric Organ 10")]      EOrgan10           = 0x0000010A,
        [Description("027: Pipe Organ 01")]          PipeOrgan01        = 0x0000010B,
        [Description("028: Pipe Organ 02")]          PipeOrgan02        = 0x0000010C,
        [Description("029: Reed Organ")]             ReedOrgan          = 0x0000010D,
        [Description("030: Harpsichord 01")]         Harpsichord01      = 0x0000010E,
        [Description("031: Harpsichord 02")]         Harpsichord02      = 0x0000010F,
        [Description("032: Clave 01")]               Clav01             = 0x00000200,
        [Description("033: Clave 02")]               Clav02             = 0x00000201,
        [Description("034: Celesta")]                Celesta            = 0x00000202,
        [Description("035: Accordion 01")]           Accordion01        = 0x00000203,
        [Description("036: Accordion 02")]           Accordion02        = 0x00000204,
        [Description("037: Harmonica")]              Harmonica          = 0x00000205,
        [Description("038: Bell 01")]                Bell01             = 0x00000206,
        [Description("039: Music Box")]              MusicBox           = 0x00000207,
        [Description("040: Vibraphone 01")]          Vibraphone01       = 0x00000208,
        [Description("041: Vibraphone 02")]          Vibraphone02       = 0x00000209,
        [Description("042: Vibraphone 03")]          Vibraphone03       = 0x0000020A,
        [Description("043: Vibraphone 04")]          Vibraphone04       = 0x0000020B,
        [Description("044: Marimba 01")]             Marimba01          = 0x0000020C,
        [Description("045: Marimba 02")]             Marimba02          = 0x0000020D,
        [Description("046: Glockenspiel")]           Glockenspiel       = 0x0000020E,
        [Description("047: Xylophone 01")]           Xylophone01        = 0x0000020F,
        [Description("048: Xylophone 02")]           Xylophone02        = 0x00000300,
        [Description("049: Xylophone 03")]           Xylophone03        = 0x00000301,
        [Description("050: Yang Qin")]               YangQin            = 0x00000302,
        [Description("051: Santur 01")]              Santur01           = 0x00000303,
        [Description("052: Santur 02")]              Santur02           = 0x00000304,
        [Description("053: Steel Drums")]            SteelDrums         = 0x00000305,
        [Description("054: Acoustic Guitar 01")]     AcGuitar01         = 0x00000306,
        [Description("055: Acoustic Guitar 02")]     AcGuitar02         = 0x00000307,
        [Description("056: Acoustic Guitar 03")]     AcGuitar03         = 0x00000308,
        [Description("057: Acoustic Guitar 04")]     AcGuitar04         = 0x00000309,
        [Description("058: Acoustic Guitar 05")]     AcGuitar05         = 0x0000030A,
        [Description("059: Mandolin 01")]            Mandolin01         = 0x0000030B,
        [Description("060: Mandolin 02")]            Mandolin02         = 0x0000030C,
        [Description("061: Ukelele")]                Ukelele            = 0x0000030D,
        [Description("062: Jazz Guitar 01")]         JazzGuitar01       = 0x0000030E,
        [Description("063: Jazz Guitar 02")]         JazzGuitar02       = 0x0000030F,
        [Description("064: Jazz Guitar 03")]         JazzGuitar03       = 0x00000400,
        [Description("065: Electric Guitar")]        EGuitar            = 0x00000401,
        [Description("066: Muted Guitar")]           MutedGuitar        = 0x00000402,
        [Description("067: Pedal Steel Guitar")]     PedalSteel         = 0x00000403,
        [Description("068: Distortion Guitar")]      DistGuitar01       = 0x00000404,
        [Description("069: Acoustic Bass 01")]       AcBass01           = 0x00000405,
        [Description("070: Acoustic Bass 02")]       AcBass02           = 0x00000406,
        [Description("071: Electric Bass 01")]       EBass01            = 0x00000407,
        [Description("072: Electric Bass 02")]       EBass02            = 0x00000408,
        [Description("073: Fretless Bass 01")]       FretlessBass01     = 0x00000409,
        [Description("074: Fretless Bass 02")]       FretlessBass02     = 0x0000040A,
        [Description("075: Fretless Bass 03")]       FretlessBass03     = 0x0000040B,
        [Description("076: Slap Bass 01")]           SlapBass01         = 0x0000040C,
        [Description("077: Slap Bass 02")]           SlapBass02         = 0x0000040D,
        [Description("078: Synth Bass 01")]          SynthBass01        = 0x0000040E,
        [Description("079: Synth Bass 02")]          SynthBass02        = 0x0000040F,
        [Description("080: Synth Bass 03")]          SynthBass03        = 0x00000500,
        [Description("081: Synth Bass 04")]          SynthBass04        = 0x00000501,
        [Description("082: Synth Bass 05")]          SynthBass05        = 0x00000502,
        [Description("083: Synth Bass 06")]          SynthBass06        = 0x00000503,
        [Description("084: Plucked / Stroke")]       PluckedStroke      = 0x00000504,
        [Description("085: Banjo")]                  Banjo              = 0x00000505,
        [Description("086: Harp")]                   Harp               = 0x00000506,
        [Description("087: Koto")]                   Koto               = 0x00000507,
        [Description("088: Shamisen")]               Shamisen           = 0x00000508,
        [Description("089: Sitar")]                  Sitar              = 0x00000509,
        [Description("090: Violin 01")]              Violin01           = 0x0000050A,
        [Description("091: Violin 02")]              Violin02           = 0x0000050B,
        [Description("092: Fiddle")]                 Fiddle             = 0x0000050C,
        [Description("093: Cello 01")]               Cello01            = 0x0000050D,
        [Description("094: Cello 02")]               Cello02            = 0x0000050E,
        [Description("095: Contrabass 01")]          Contrabass01       = 0x0000050F,
        [Description("096: Contrabass 02")]          Contrabass02       = 0x00000600,
        [Description("097: Ensemble Strings 01")]    EnsembleStrings01  = 0x00000601,
        [Description("098: Ensemble Strings 02")]    EnsembleStrings02  = 0x00000602,
        [Description("099: Ensemble Strings 03")]    EnsembleStrings03  = 0x00000603,
        [Description("100: Tremolo Strings")]        TremoloStrings     = 0x00000604,
        [Description("101: Pizzicato Strings 01")]   PizzicatoStrings01 = 0x00000605,
        [Description("102: Pizzicato Strings 02")]   PizzicatoStrings02 = 0x00000606,
        [Description("103: Orchestra 01")]           Orchestra01        = 0x00000607,
        [Description("104: Orchestra 02")]           Orchestra02        = 0x00000608,
        [Description("105: Solo Brass")]             SoloBrass          = 0x00000609,
        [Description("106: Trumpet 01")]             Trumpet01          = 0x0000060A,
        [Description("107: Trumpet 02")]             Trumpet02          = 0x0000060B,
        [Description("108: Muted Trumpet")]          MuteTrumpet        = 0x0000060C,
        [Description("109: Trombone")]               Trombone           = 0x0000060D,
        [Description("110: French Horn")]            FrenchHorn         = 0x0000060E,
        [Description("111: Tuba")]                   Tuba               = 0x0000060F,
        [Description("112: Ensemble Brass 01")]      EnsembleBrass01    = 0x00000700,
        [Description("113: French Horn Section")]    FrenchHornSection  = 0x00000701,
        [Description("114: Wind")]                   Wind               = 0x00000702,
        [Description("115: Oboe")]                   Oboe               = 0x00000703,
        [Description("116: Clarinet")]               Clarinet           = 0x00000704,
        [Description("117: Bassoon")]                Bassoon            = 0x00000705,
        [Description("118: Bagpipe 01")]             Bagpipe01          = 0x00000706,
        [Description("119: Bagpipe 02")]             Bagpipe02          = 0x00000707,
        [Description("120: Shanai")]                 Shanai             = 0x00000708,
        [Description("121: Shakuhachi")]             Shakuhachi         = 0x00000709,
        [Description("122: Flute")]                  Flute              = 0x0000070A,
        [Description("123: Soprano Sax 01")]         SopranoSax01       = 0x0000070B,
        [Description("124: Soprano Sax 02")]         SopranoSax02       = 0x0000070C,
        [Description("125: Alto Sax 01")]            AltoSax01          = 0x0000070D,
        [Description("126: Alto Sax 02")]            AltoSax02          = 0x0000070E,
        [Description("127: Tenor Sax 01")]           TenorSax01         = 0x0000070F,
        [Description("128: Baritone Sax")]           BaritoneSax        = 0x00000800,
        [Description("129: Recorder")]               Recorder           = 0x00000801,
        [Description("130: Vox / Choirs 01")]        VoxChoirs01        = 0x00000802,
        [Description("131: Vox / Choirs 02")]        VoxChoirs02        = 0x00000803,
        [Description("132: Scat 01")]                Scat01             = 0x00000804,
        [Description("133: Scat 02")]                Scat02             = 0x00000805,
        [Description("134: Synth Lead 01")]          SynthLead01        = 0x00000806,
        [Description("135: Synth Lead 02")]          SynthLead02        = 0x00000807,
        [Description("136: Synth Lead 03")]          SynthLead03        = 0x00000808,
        [Description("137: Synth Lead 04")]          SynthLead04        = 0x00000809,
        [Description("138: Synth Lead 05")]          SynthLead05        = 0x0000080A,
        [Description("139: Synth Lead 06")]          SynthLead06        = 0x0000080B,
        [Description("140: Synth Lead 07")]          SynthLead07        = 0x0000080C,
        [Description("141: Synth Brass 01")]         SynthBrass01       = 0x0000080D,
        [Description("142: Synth Brass 02")]         SynthBrass02       = 0x0000080E,
        [Description("143: Synth Brass 03")]         SynthBrass03       = 0x0000080F,
        [Description("144: Synth Brass 04")]         SynthBrass04       = 0x00000900,
        [Description("145: Synth Pad / Strings 01")] SynthPadStrings01  = 0x00000901,
        [Description("146: Synth Pad / Strings 02")] SynthPadStrings02  = 0x00000902,
        [Description("147: Synth Pad / Strings 03")] SynthPadStrings03  = 0x00000903,
        [Description("148: Synth Bell Pad 01")]      SynthBellPad01     = 0x00000904,
        [Description("149: Synth Bell Pad 02")]      SynthBellPad02     = 0x00000905,
        [Description("150: Synth Bell Pad 03")]      SynthBellPad03     = 0x00000906,
        [Description("151: Synth Poly Key 01")]      SynthPolyKey01     = 0x00000907,
        [Description("152: Synth Poly Key 02")]      SynthPolyKey02     = 0x00000908,
        [Description("153: Synth Poly Key 03")]      SynthPolyKey03     = 0x00000909,
        [Description("154: Synth Sequence Pop 01")]  SynthSeqPop01      = 0x0000090A,
        [Description("155: Synth Sequence Pop 02")]  SynthSeqPop02      = 0x0000090B,
        [Description("156: Timpani 01")]             Timpani01          = 0x0000090C,
        [Description("157: Timpani 02")]             Timpani02          = 0x0000090D,
        [Description("158: Percussion")]             Percussion         = 0x0000090E,
        [Description("159: Sound FX 01")]            SoundFX01          = 0x0000090F,
        [Description("160: Sound FX 02")]            SoundFX02          = 0x00000A00,
        [Description("161: Sound FX 03")]            SoundFX03          = 0x00000A01,
        [Description("162: Vibraphone 05")]          Vibraphone05       = 0x00000A02,
        [Description("163: Distortion Guitar 02")]   DistGuitar02       = 0x00000A03,
        [Description("164: Distortion Guitar 03")]   DistGuitar03       = 0x00000A04,
        [Description("165: Electric Bass 03")]       EBass03            = 0x00000A05,
        [Description("166: Electric Bass 04")]       EBass04            = 0x00000A06,
        [Description("167: Synth Bass 07")]          SynthBass07        = 0x00000A07,
        [Description("168: Synth Bass 08")]          SynthBass08        = 0x00000A08,
        [Description("169: Synth Bass 09")]          SynthBass09        = 0x00000A09,
        [Description("170: Synth Bass 10")]          SynthBass10        = 0x00000A0A,
        [Description("171: Synth Bass 11")]          SynthBass11        = 0x00000A0B,
        [Description("172: Synth Bass 12")]          SynthBass12        = 0x00000A0C,
        [Description("173: Santur 03")]              Santur03           = 0x00000A0D,
        [Description("174: Ensemble Brass 02")]      EnsembleBrass02    = 0x00000A0E,
        [Description("175: Tenor Sax 02")]           TenorSax02         = 0x00000A0F,
        [Description("176: Tenor Sax 03")]           TenorSax03         = 0x00000B00,
        [Description("177: Pan Pipe")]               PanPipe            = 0x00000B01,
        [Description("178: Vox / Choirs 03")]        VoxChoirs03        = 0x00000B02,
        [Description("179: Vox / Choirs 04")]        VoxChoirs04        = 0x00000B03,
        [Description("180: Vox / Choirs 05")]        VoxChoirs05        = 0x00000B04,
        [Description("181: Vox / Choirs 06")]        VoxChoirs06        = 0x00000B05,
        [Description("182: Vox / Choirs 07")]        VoxChoirs07        = 0x00000B06,
        [Description("183: Synth Lead 08")]          SynthLead08        = 0x00000B07,
        [Description("184: Synth Pad / Strings 04")] SynthPadStrings04  = 0x00000B08,
        [Description("185: Synth Pad / Strings 05")] SynthPadStrings05  = 0x00000B09,
        [Description("186: Synth Bell 01")]          SynthBell01        = 0x00000B0A,
        [Description("187: Synth Bell 02")]          SynthBell02        = 0x00000B0B,
        [Description("188: Synth Bell 03")]          SynthBell03        = 0x00000B0C,
        [Description("189: Synth Bell 04")]          SynthBell04        = 0x00000B0D,
        [Description("190: Synth Bell 05")]          SynthBell05        = 0x00000B0E,
        [Description("191: Synth Poly Key 04")]      SynthPolyKey04     = 0x00000B0F,
        [Description("192: Synth Poly Key 05")]      SynthPolyKey05     = 0x00000C00,
        [Description("193: Synth Poly Key 06")]      SynthPolyKey06     = 0x00000C01,
        [Description("194: Synth Poly Key 07")]      SynthPolyKey07     = 0x00000C02,
        [Description("195: Synth Poly Key 08")]      SynthPolyKey08     = 0x00000C03,
        [Description("196: Synth Poly Key 09")]      SynthPolyKey09     = 0x00000C04,
        [Description("197: Synth Poly Key 10")]      SynthPolyKey10     = 0x00000C05,
        [Description("198: Bell 02")]                Bell02             = 0x00000C06,
        [Description("199: Bell 03")]                Bell03             = 0x00000C07,
        [Description("200: Synth Poly Key 11")]      SynthPolyKey11     = 0x00000C08,
        [Description("201: Synth Pad / Strings 06")] SynthPadStrings06  = 0x00000C09,
        [Description("202: Synth Pad / Strings 07")] SynthPadStrings07  = 0x00000C0A,
        [Description("203: Synth Pad / Strings 08")] SynthPadStrings08  = 0x00000C0B,
        [Description("204: Sound FX 04")]            SoundFX04          = 0x00000C0C,
        [Description("205: Sound FX 05")]            SoundFX05          = 0x00000C0D,
        [Description("206: XV Acoustic Piano")]      XVAcPiano          = 0x00000C0E,
        [Description("207: XV Electric Piano")]      XVElPiano          = 0x00000C0F,
        [Description("208: XV Keyboards")]           XVKeyboards        = 0x00000D00,
        [Description("209: XV Bell")]                XVBell             = 0x00000D01,
        [Description("210: XV Mallet")]              XVMallet           = 0x00000D02,
        [Description("211: XV Organ")]               XVOrgan            = 0x00000D03,
        [Description("212: XV Accordion")]           XVAccordion        = 0x00000D04,
        [Description("213: XV Harmonica")]           XVHarmonica        = 0x00000D05,
        [Description("214: XV Acoustic Guitar")]     XVAcGuitar         = 0x00000D06,
        [Description("215: XV Electric Guitar")]     XVElGuitar         = 0x00000D07,
        [Description("216: XV Distortion Guitar")]   XVDistGuitar       = 0x00000D08,
        [Description("217: XV Bass")]                XVBass             = 0x00000D09,
        [Description("218: XV Synth Bass")]          XVSynthBass        = 0x00000D0A,
        [Description("219: XV Strings")]             XVStrings          = 0x00000D0B,
        [Description("220: XV Orchestra")]           XVOrchestra        = 0x00000D0C,
        [Description("221: XV Hit & Stab")]          XVHitStab          = 0x00000D0D,
        [Description("222: XV Wind")]                XVWind             = 0x00000D0E,
        [Description("223: XV Flute")]               XVFlute            = 0x00000D0F,
        [Description("224: XV Acoustic Brass")]      XVAcBrass          = 0x00000E00,
        [Description("225: XV Synth Brass")]         XVSynthBrass       = 0x00000E01,
        [Description("226: XV Sax")]                 XVSax              = 0x00000E02,
        [Description("227: XV Hard Lead")]           XVHardLead         = 0x00000E03,
        [Description("228: XV Soft Lead")]           XVSoftLead         = 0x00000E04,
        [Description("229: XV Techno Synth")]        XVTechnoSynth      = 0x00000E05,
        [Description("230: XV Pulsating")]           XVPulsating        = 0x00000E06,
        [Description("231: XV Synth FX")]            XVSynthFX          = 0x00000E07,
        [Description("232: XV Other Synth")]         XVOtherSynth       = 0x00000E08,
        [Description("233: XV Bright Pad")]          XVBrightPad        = 0x00000E09,
        [Description("234: XV Soft Pad")]            XVSoftPad          = 0x00000E0A,
        [Description("235: XV Vox")]                 XVVox              = 0x00000E0B,
        [Description("236: XV Plucked")]             XVPlucked          = 0x00000E0C,
        [Description("237: XV Ethnic")]              XVEthnic           = 0x00000E0D,
        [Description("238: XV Fretted")]             XVFretted          = 0x00000E0E,
        [Description("239: XV Percussion")]          XVPercussion       = 0x00000E0F,
        [Description("240: XV Sound FX")]            XVSoundFX          = 0x00000F00,
        [Description("241: XV Beat & Groove")]       XVBeatGroove       = 0x00000F01,
        [Description("242: XV Drums")]               XVDrums            = 0x00000F02,
        [Description("243: XV Combination")]         XVCombination      = 0x00000F03
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
    public enum IntegraSNAInstruments : int
    {
        [Description("INT 001: ")]   ConcertGrand     = 0,
        [Description("INT 002: ")]   GrandPiano1      = 1,
        [Description("INT 003: ")]   GrandPiano2      = 2,
        [Description("INT 004: ")]   GrandPiano3      = 3,
        [Description("INT 005: ")]   MellowPiano      = 4,
        [Description("INT 006: ")]   BrightPiano      = 5,
        [Description("INT 007: ")]   UprightPiano     = 6,
        [Description("INT 008: ")]   ConcertMono      = 7,
        [Description("INT 009: ")]   HonkyTonk        = 8,
        [Description("INT 010: ")]   PureVintageEP1   = 9,
        [Description("INT 011: ")]   PureVintageEP2   = 10,
        [Description("INT 012: ")]   PureWurly        = 11,
        [Description("INT 013: ")]   PureVintageEP3   = 12,
        [Description("INT 014: ")]   OldHammerEP      = 13,
        [Description("INT 015: ")]   DynoPiano        = 14,
        [Description("INT 016: ")]   ClavCBFlat       = 15,
        [Description("INT 017: ")]   ClavCAFlat       = 16,
        [Description("INT 018: ")]   ClavCBMedium     = 17,
        [Description("INT 019: ")]   ClavCAMedium     = 18,
        [Description("INT 020: ")]   ClavCBBrillia    = 19,
        [Description("INT 021: ")]   ClavCABrillia    = 20,
        [Description("INT 022: ")]   ClavCBCombo      = 21,
        [Description("INT 023: ")]   ClavCACombo      = 22,
        [Description("INT 024: ")]   Glockenspiel     = 23,
        [Description("INT 025: ")]   Vibraphone       = 24,
        [Description("INT 026: ")]   Marimba          = 25,
        [Description("INT 027: ")]   Xylophone        = 26,
        [Description("INT 028: ")]   TubularBells     = 27,
        [Description("INT 029: ")]   TWOrgan          = 28,
        [Description("INT 030: ")]   FrenchAccordion  = 29,
        [Description("INT 031: ")]   ItalianAccordion = 30,
        [Description("INT 032: ")]   Harmonica        = 31,
        [Description("INT 033: ")]   Bandoneon        = 32,
        [Description("INT 034: ")]   NylonGuitar      = 33,
        [Description("INT 035: ")]   FlamencoGuitar   = 34,
        [Description("INT 036: ")]   SteelStrGuitar   = 35,
        [Description("INT 037: ")]   JazzGuitar       = 36,
        [Description("INT 038: ")]   STGuitarHalf     = 37,
        [Description("INT 039: ")]   STGuitarFront    = 38,
        [Description("INT 040: ")]   TCGuitarRear     = 39,
        [Description("INT 041: ")]   AcousticBass     = 40,
        [Description("INT 042: ")]   FingeredBass     = 41,
        [Description("INT 043: ")]   PickedBass       = 42,
        [Description("INT 044: ")]   FretlessBass     = 43,
        [Description("INT 045: ")]   Violin           = 44,
        [Description("INT 046: ")]   Violin2          = 45,
        [Description("INT 047: ")]   Viola            = 46,
        [Description("INT 048: ")]   Cello            = 47,
        [Description("INT 049: ")]   Cello2           = 48,
        [Description("INT 050: ")]   Contrabass       = 49,
        [Description("INT 051: ")]   Harp             = 50,
        [Description("INT 052: ")]   Timpani          = 51,
        [Description("INT 053: ")]   Strings          = 52,
        [Description("INT 054: ")]   MarcatoStrings   = 53,
        [Description("INT 055: ")]   LondonChoir      = 54,
        [Description("INT 056: ")]   BoysChoir        = 55,
        [Description("INT 057: ")]   Trumpet          = 56,
        [Description("INT 058: ")]   Trombone         = 57,
        [Description("INT 059: ")]   Tb2CupMute       = 58,
        [Description("INT 060: ")]   MuteTrumpet      = 59,
        [Description("INT 061: ")]   FrenchHorn       = 60,
        [Description("INT 062: ")]   SopranoSax2      = 61,
        [Description("INT 063: ")]   AltoSax2         = 62,
        [Description("INT 064: ")]   TenorSax2        = 63,
        [Description("INT 065: ")]   BaritoneSax2     = 64,
        [Description("INT 066: ")]   Oboe             = 65,
        [Description("INT 067: ")]   Bassoon          = 66,
        [Description("INT 068: ")]   Clarinet         = 67,
        [Description("INT 069: ")]   Piccolo          = 68,
        [Description("INT 070: ")]   Flute            = 69,
        [Description("INT 071: ")]   PanFlute         = 70,
        [Description("INT 072: ")]   Shakuhachi       = 71,
        [Description("INT 073: ")]   Sitar            = 72,
        [Description("INT 074: ")]   UilleannPipes    = 73,
        [Description("INT 075: ")]   BagPipes         = 74,
        [Description("INT 076: ")]   Erhu             = 75,
        [Description("INT 077: ")]   SteelDrums       = 76,
        // ExSN01
        [Description("ExSN01 001:")] Santoor          = 77,
        [Description("ExSN01 002:")] YangChin         = 78,
        [Description("ExSN01 003:")] TinWhistle       = 79,
        [Description("ExSN01 004:")] Ryuteki          = 80,
        [Description("ExSN01 005:")] Tsugaru          = 81,
        [Description("ExSN01 006:")] Sansin           = 82,
        [Description("ExSN01 007:")] Koto             = 83,
        [Description("ExSN01 008:")] TaishouKoto      = 84,
        [Description("ExSN01 009:")] Kalimba          = 85,
        [Description("ExSN01 010:")] Sarangi          = 86,
        // ExSN02
        [Description("ExSN02 001:")] SopranoSax       = 87,
        [Description("ExSN02 002:")] AltoSax          = 88,
        [Description("ExSN02 003:")] TenorSax         = 89,
        [Description("ExSN02 004:")] BaritoneSax      = 90,
        [Description("ExSN02 005:")] EnglishHorn      = 91,
        [Description("ExSN02 006:")] BassClarinet     = 92,
        [Description("ExSN02 007:")] Flute2           = 93,
        [Description("ExSN02 008:")] SopranoRecorder  = 94,
        [Description("ExSN02 009:")] AltoRecorder     = 95,
        [Description("ExSN02 010:")] TenorRecorder    = 96,
        [Description("ExSN02 011:")] BassRecorder     = 97,
        [Description("ExSN02 012:")] OcarinaSopC      = 98,
        [Description("ExSN02 013:")] OcarinaSopF      = 99,
        [Description("ExSN02 014:")] OcarinaAlto      = 100,
        [Description("ExSN02 015:")] OcarinaBass      = 101,
        // ExSN03
        [Description("ExSN03 001:")] TCGuitarFing     = 102,
        [Description("ExSN03 002:")] Guitar335Fing    = 103,
        [Description("ExSN03 003:")] LPGuitarRear     = 104,
        [Description("ExSN03 004:")] LPGuitarFront    = 105,
        [Description("ExSN03 005:")] Guitar335Half    = 106,
        [Description("ExSN03 006:")] AcousticBass2    = 107,
        [Description("ExSN03 007:")] FingeredBass2    = 108,
        [Description("ExSN03 008:")] PickedBass2      = 109,
        // ExSN04
        [Description("ExSN04 001:")] Ukelele          = 110,
        [Description("ExSN04 002:")] NylonGuitar2     = 111,
        [Description("ExSN04 003:")] Steel12ThGtr     = 112,
        [Description("ExSN04 004:")] Mandolin         = 113,
        [Description("ExSN04 005:")] SteelFingGuitar  = 114,
        [Description("ExSN04 006:")] SteelStrGuitar2  = 115,
        // ExSN05
        [Description("ExSN05 001:")] ClassicalTrumpet = 116,
        [Description("ExSN05 002:")] FlugelHorn       = 117,
        [Description("ExSN05 003:")] Trumpet2         = 118,
        [Description("ExSN05 004:")] MariachiTp       = 119,
        [Description("ExSN05 005:")] Trombone2        = 120,
        [Description("ExSN05 006:")] BassTrombone     = 121,
        [Description("ExSN05 007:")] Tuba             = 122,
        [Description("ExSN05 008:")] StraightMuteTp   = 123,
        [Description("ExSN05 009:")] CupMuteTrumpet   = 124,
        [Description("ExSN05 010:")] FrenchHorn2      = 125,
        [Description("ExSN05 011:")] MuteFrenchHorn   = 126,
    }

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
        Whole
    }

    public enum SNAKotoPlayScale : byte
    {
        Chroma,
        Hira
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
        Rasgueado = 0x01,
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
        //Reserved01 = 0x02,
        //Reserved02 = 0x03
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
    {   
        [Description("0 A")]           A0,
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
        [Description("C -")] C_  = 0x00,
        [Description("C#-")] C_S = 0x01,
        [Description("D -")] D_  = 0x02,
        [Description("Eb-")] E_F = 0x03,
        [Description("E -")] E_  = 0x04,
        [Description("F -")] F_  = 0x05,
        [Description("F#-")] F_S = 0x06,
        [Description("G -")] G_  = 0x07,
        [Description("G#-")] G_S = 0x08,
        [Description("A -")] A_  = 0x09,
        [Description("Bb-")] B_F = 0x0A,
        [Description("B -")] B_  = 0x0B,
        [Description("C 0")] C0  = 0x0C,
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
