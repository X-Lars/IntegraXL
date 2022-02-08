using IntegraXL.Common;
using System.ComponentModel;

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
        [Description("Custom Individually")]                Custom      = 0x00,
        [Description("Equal Temprament")]                   Equal       = 0x01,
        [Description("Just Intonation (Major)")]            JustMaj     = 0x02,
        [Description("Just Intonation (Minor)")]            JustMin     = 0x03,
        [Description("Pythagorean Tuning")]                 Pythagore   = 0x04,
        [Description("Kirnberger (Type 3)")]                Kirnberge   = 0x05,
        [Description("Meantone Temprament")]                MeanTone    = 0x06,
        [Description("Werckmeister (Type 1, Number 3)")]    Werckmeis   = 0x07,
        [Description("Arabic Scale")]                       Arabic      = 0x08
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
        [Description("200 [Hz]")] Hz200 = 0x00,
        [Description("400 [Hz]")] Hz400 = 0x01
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraMidFrequencies : byte
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
        [Description("2000 [Hz]")] Hz2000 = 0x0A,
        [Description("2500 [Hz]")] Hz2500 = 0x0B,
        [Description("3150 [Hz]")] Hz3150 = 0x0C,
        [Description("4000 [Hz]")] Hz4000 = 0x0D,
        [Description("5000 [Hz]")] Hz5000 = 0x0E,
        [Description("6300 [Hz]")] Hz6300 = 0x0F,
        [Description("8000 [Hz]")] Hz8000 = 0x10
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
        [Description("2000 [Hz]")] Hz2000,
        [Description("4000 [Hz]")] Hz4000,
        [Description("8000 [Hz]")] Hz8000
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
        [Description("\U0001D162\U0001D16D")] _132D = 0x05,
        [Description("\U0001D161")]           _116  = 0x06,
        [Description("\U0001D160\U00002083")] _18T  = 0x07,
        [Description("\U0001D161\U0001D16D")] _116D = 0x08,
        [Description("\U0001D160")]           _18   = 0x09,
        [Description("\U0001D15F\U00002083")] _14T  = 0x0A,
        [Description("\U0001D161\U0001D16D")] _18D  = 0x0B,
        [Description("\U0001D15F")]           _14   = 0x0C,
        [Description("\U0001D15E\U00002083")] _12T  = 0x0D,
        [Description("\U0001D15F\U0001D16D")] _14D  = 0x0E,
        [Description("\U0001D15E")]           _12   = 0x0F,
        [Description("\U0001D15D\U00002083")] _1T   = 0x10,
        [Description("\U0001D15E\U0001D16D")] _12D  = 0x11,
        [Description("\U0001D15D")]           _1    = 0x12,
        [Description("\U0001D15C\U00002083")] _2T   = 0x13,
        [Description("\U0001D15D\U0001D16D")] _1D   = 0x14,
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

    //[TypeConverter(typeof(DescriptionConverter))]
    //public enum IntegraNoteRates : byte
    //{
    //    [Description("\uE014\uEB9E\U0000E833")] _164T = 0x00,
    //    [Description("")]  _164  = 0x01,
    //    [Description("r£")]  _132T = 0x02,
    //    [Description("r")]   _132  = 0x03,
    //    [Description("x£")]  _116T = 0x04,
    //    [Description("r.")]  _132D = 0x05,
    //    [Description("x")]   _116  = 0x06,
    //    [Description("e£")]  _18T  = 0x07,
    //    [Description("x.")]  _116D = 0x08,
    //    [Description("e")]   _18   = 0x09,
    //    [Description("q£")]  _14T  = 0x0A,
    //    [Description("e.")]  _18D  = 0x0B,
    //    [Description("q")]   _14   = 0x0C,
    //    [Description("h£")]  _12T  = 0x0D,
    //    [Description("q.")]  _14D  = 0x0E,
    //    [Description("h")]   _12   = 0x0F,
    //    [Description("w£")]  _1T   = 0x10,
    //    [Description("h.")]  _12D  = 0x11,
    //    [Description("w")]   _1    = 0x12,
    //    [Description("W£")]  _2T   = 0x13,
    //    [Description("w.")]  _1D   = 0x14,
    //    [Description("W")]   _2    = 0x15,
    //}

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
        [Description("Unassigned")]             Unassigned          = 0x00,
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

    #endregion

    #region PCMSynthToneCommon

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
}
