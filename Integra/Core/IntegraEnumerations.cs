using Integra.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Integra.Core
{


    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraNoteRates : byte
    {
        [Description("\uE014\uEB9E\U0000E833")] _164T = 0x00,
        [Description("")]  _164  = 0x01,
        [Description("r£")] _132T = 0x02,
        [Description("r")]  _132  = 0x03,
        [Description("x£")] _116T = 0x04,
        [Description("r.")]  _132D = 0x05,
        [Description("x")]  _116  = 0x06,
        [Description("e£")] _18T  = 0x07,
        [Description("x.")]  _116D = 0x08,
        [Description("e")]  _18   = 0x09,
        [Description("q£")] _14T  = 0x0A,
        [Description("e.")]  _18D  = 0x0B,
        [Description("q")]  _14   = 0x0C,
        [Description("h£")] _12T  = 0x0D,
        [Description("q.")]  _14D  = 0x0E,
        [Description("h")]  _12   = 0x0F,
        [Description("w£")] _1T   = 0x10,
        [Description("h.")]  _12D  = 0x11,
        [Description("w")]  _1    = 0x12,
        [Description("W£")] _2T   = 0x13,
        [Description("w.")]  _1D   = 0x14,
        [Description("W")]  _2    = 0x15,
    }

    
    public enum IntegraWaveFormType : int
    {
        INT = 0,
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
        SNS = 20,
        SNA = 21,
        SND = 22
    }

    #region Custom Build Enumerations

    public class IntegraSNDNotes : Enumeration
    {
        public new static List<string> Values
        {
            get
            {
                List<string> values = new List<string>();

                for (int i = 1; i < 7;)
                {
                    values.Add($"Eb{i}");
                    values.Add($"E{i}");
                    if (i == 6)
                        break;
                    values.Add($"F{i}");
                    values.Add($"F#{i}");
                    values.Add($"G{i}");
                    values.Add($"G#{i}");
                    values.Add($"A{i}");
                    values.Add($"Bb{i}");
                    values.Add($"B{i}");
                    i++;
                    values.Add($"C{i}");
                    values.Add($"C#{i}");
                    values.Add($"D{i}");
                }

                return values;
            }
        }
    }

    public class IntegraPitchBendRange : Enumeration
    {
        public new static List<string> Values
        {
            get
            {
                List<string> values = new List<string>();

                for (int i = 0; i < 25; i++)
                {
                    values.Add(i.ToString());
                }

                values.Add("Tone");

                return values;
            }
        }
    }

    public class IntegraPortamentoTime : Enumeration
    {
        public new static List<string> Values
        {
            get
            {
                List<string> values = new List<string>();

                for (int i = 0; i < 128; i++)
                {
                    values.Add(i.ToString());
                }

                values.Add("Tone");

                return values;
            }
        }
    }

    public class IntegraPCMDrumKitNotes : Enumeration
    {
        public new static List<string> Values
        {
            get
            {
                List<string> values = new List<string>();

                for (int i = 0; i < 9;)
                {
                    values.Add($"A{i}");
                    values.Add($"Bb{i}");
                    values.Add($"B{i}");
                    i++;
                    values.Add($"C{i}");
                    if (i == 8)
                        break;
                    values.Add($"C#{i}");
                    values.Add($"D{i}");
                    values.Add($"Eb{i}");
                    values.Add($"E{i}");
                    values.Add($"F{i}");
                    values.Add($"F#{i}");
                    values.Add($"G{i}");
                    values.Add($"G#{i}");
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
                List<string> values = new List<string>();

                for (int i = 0; i < 64; i++)
                {
                    values.Add(i.ToString());
                }

                values.Add("Full");

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
                List<string> values = new List<string>();

                for (int i = 0; i < 64; i++)
                {
                    values.Add($"L {Math.Abs(0 - 64 + i).ToString("00")}");
                }

                values.Add("0");

                for (int i = 1; i < 64; i++)
                {
                    values.Add($"R {i.ToString("00")}");
                }

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
                List<string> values = new List<string>();

                values.Add("Off");

                for (int i = 1; i < 17; i++)
                {
                    values.Add($"Part {i.ToString("00")}");
                }

                return values;
            }
        }
    }

    public class IntegraPreDelay : Enumeration
    {
        public new static List<string> Values
        {
            get
            {
                List<string> values = new List<string>();

                for (int i = 0; i < 50; i++)
                {
                    values.Add((((double)i / 10)).ToString("0.0 [ms]"));
                }

                for (int i = 0; i < 10; i++)
                {
                    values.Add((5.0 + i * 0.5).ToString("0.0 [ms]"));
                }

                for (int i = 0; i < 40; i++)
                {
                    values.Add((10.0 + i).ToString("0.0 [ms]"));
                }
                for (int i = 0; i < 26; i++)
                {
                    values.Add((50.0 + (i * 2)).ToString("0.0 [ms]"));
                }

                return values;
            }
        }
    }

    #endregion

    public enum IntegraSwitch : byte
    {
        Off = 0x00,
        On = 0x01
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraChannels : byte
    {
        [Description("Channel 01")] Channel01 = 0x00,
        [Description("Channel 02")] Channel02 = 0x01,
        [Description("Channel 03")] Channel03 = 0x02,
        [Description("Channel 04")] Channel04 = 0x03,
        [Description("Channel 05")] Channel05 = 0x04,
        [Description("Channel 06")] Channel06 = 0x05,
        [Description("Channel 07")] Channel07 = 0x06,
        [Description("Channel 08")] Channel08 = 0x07,
        [Description("Channel 09")] Channel09 = 0x08,
        [Description("Channel 10")] Channel10 = 0x09,
        [Description("Channel 11")] Channel11 = 0x0A,
        [Description("Channel 12")] Channel12 = 0x0B,
        [Description("Channel 13")] Channel13 = 0x0C,
        [Description("Channel 14")] Channel14 = 0x0D,
        [Description("Channel 15")] Channel15 = 0x0E,
        [Description("Channel 16")] Channel16 = 0x0F
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraControlChannels : byte
    {
        [Description("01")]  Channel01,
        [Description("02")]  Channel02,
        [Description("03")]  Channel03,
        [Description("04")]  Channel04,
        [Description("05")]  Channel05,
        [Description("06")]  Channel06,
        [Description("07")]  Channel07,
        [Description("08")]  Channel08,
        [Description("09")]  Channel09,
        [Description("10")]  Channel10,
        [Description("11")]  Channel11,
        [Description("12")]  Channel12,
        [Description("13")]  Channel13,
        [Description("14")]  Channel14,
        [Description("15")]  Channel15,
        [Description("16")]  Channel16,
        [Description("Off")] Off
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraParts : byte
    {
        [Description("Part 01")] Part01 = 0x00,
        [Description("Part 02")] Part02 = 0x01,
        [Description("Part 03")] Part03 = 0x02,
        [Description("Part 04")] Part04 = 0x03,
        [Description("Part 05")] Part05 = 0x04,
        [Description("Part 06")] Part06 = 0x05,
        [Description("Part 07")] Part07 = 0x06,
        [Description("Part 08")] Part08 = 0x07,
        [Description("Part 09")] Part09 = 0x08,
        [Description("Part 10")] Part10 = 0x09,
        [Description("Part 11")] Part11 = 0x0A,
        [Description("Part 12")] Part12 = 0x0B,
        [Description("Part 13")] Part13 = 0x0C,
        [Description("Part 14")] Part14 = 0x0D,
        [Description("Part 15")] Part15 = 0x0E,
        [Description("Part 16")] Part16 = 0x0F
    }

    public enum IntegraSynthTonePartials : byte
    {
        Partial01 = 0x00,
        Partial02 = 0x01,
        Partial03 = 0x02,
        Partial04 = 0x03
    }

    public enum IntegraSoundModes : byte
    {
        Studio  = 0x01,
        GM      = 0x02,
        GM2     = 0x03,
        GS      = 0x04
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraToneTypes : uint
    {
        [Description("PCM Synth Tone")]             PCMSynthTone                = 0x00000000,
        [Description("SuperNATURAL Synth Tone")]    SuperNATURALSynthTone       = 0x00010000,
        [Description("SuperNATURAL Acoustic Tone")] SuperNATURALAcousticTone    = 0x00020000,
        [Description("SuperNATURAL Drum Kit")]      SuperNATURALDrumkit         = 0x00030000,
        [Description("PCM Drum Kit")]               PCMDrumkit                  = 0x00100000
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraToneBanks : ushort
    {
        [Description("Undefined")]                                  Undefined       = 0x0000,
        [Description("PCM User Drum Kit")]                          PCMUserDrum     = 0x5600,
        [Description("PCM Preset Drum Kit")]                        PCMPresetDrum   = 0x5640,
        [Description("PCM User Tone")]                              PCMUserTone     = 0x5700,
        [Description("PCM Preset Tone")]                            PCMPresetTone   = 0x5740,
        [Description("SuperNATURAL User Drum Kit")]                 SNDuser         = 0x5800,
        [Description("SuperNATURAL Preset Drum Kit")]               SNDPreset       = 0x5840,
        [Description("ExSN-06 SuperNATURAL Expansion Drum Kit")]    Exp18Drum       = 0x5865,
        [Description("SuperNATURAL Acoustic User Tone")]            SNAUser         = 0x5900,
        [Description("SuperNATURAL Acoustic Preset Tone")]          SNAPreset       = 0x5940,
        [Description("ExSN-01 SuperNATURAL Expansion Tone")]        Exp13Tone       = 0x5960,
        [Description("ExSN-02 SuperNATURAL Expansion Tone")]        Exp14Tone       = 0x5961,
        [Description("ExSN-03 SuperNATURAL Expansion Tone")]        Exp15Tone       = 0x5962,
        [Description("ExSN-04 SuperNATURAL Expansion Tone")]        Exp16Tone       = 0x5963,
        [Description("ExSN-05 SuperNATURAL Expansion Tone")]        Exp17Tone       = 0x5964,
        [Description("SRX-01 PCM Expansion Drum Kit")]              Exp01Drum       = 0x5C00,
        [Description("SRX-03 PCM Expansion Drum Kit")]              Exp03Drum       = 0x5C02,
        [Description("SRX-05 PCM Expansion Drum Kit")]              Exp05Drum       = 0x5C04,
        [Description("SRX-06 PCM Expansion Drum Kit")]              Exp06Drum       = 0x5C07,
        [Description("SRX-07 PCM Expansion Drum Kit")]              Exp07Drum       = 0x5C0B,
        [Description("SRX-08 PCM Expansion Drum Kit")]              Exp08Drum       = 0x5C0F,
        [Description("SRX-09 PCM Expansion Drum Kit")]              Exp09Drum       = 0x5C13,
        [Description("SRX-01 PCM Expansion Tone")]                  Exp01Tone       = 0x5D00,
        [Description("SRX-02 PCM Expansion Tone")]                  Exp02Tone       = 0x5D01,
        [Description("SRX-03 PCM Expansion Tone")]                  Exp03Tone       = 0x5D02,
        [Description("SRX-04 PCM Expansion Tone")]                  Exp04Tone       = 0x5D03,
        [Description("SRX-05 PCM Expansion Tone")]                  Exp05Tone       = 0x5D04,
        [Description("SRX-06 PCM Expansion Tone")]                  Exp06Tone       = 0x5D07,
        [Description("SRX-07 PCM Expansion Tone")]                  Exp07Tone       = 0x5D0B,
        [Description("SRX-08 PCM Expansion Tone")]                  Exp08Tone       = 0x5D0F,
        [Description("SRX-09 PCM Expansion Tone")]                  Exp09Tone       = 0x5D13,
        [Description("SRX-10 PCM Expansion Tone")]                  Exp10Tone       = 0x5D17,
        [Description("SRX-11 PCM Expansion Tone")]                  Exp11Tone       = 0x5D18,
        [Description("SRX-12 PCM Expansion Tone")]                  Exp12Tone       = 0x5D1A,
        [Description("SuperNATURAL Synth User Tone")]               SNSuser         = 0x5F00,
        [Description("SuperNATURAL Synth Preset Tone")]             SNSPreset       = 0x5F40,
        [Description("ExPCM PCM Expansion Drum Kit")]               Exp19Drum       = 0x6000,
        [Description("ExPCM PCM Expansion Tone")]                   Exp19Tone       = 0x6100,
        [Description("GM2 Drum Kit")]                               GM2Drum         = 0x7800,
        [Description("GM2 Tone")]                                   GM2Tone         = 0x7900,
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraExpansions : byte
    {
        [Description("Off")]                                    Off   = 0x00,
        [Description("SRX 01: Dynamic Drums")]                  Exp01 = 0x01,
        [Description("SRX 02: Concert Piano")]                  Exp02 = 0x02,
        [Description("SRX 03: Studio SRX")]                     Exp03 = 0x03,
        [Description("SRX 04: Symphonique Strings")]            Exp04 = 0x04,
        [Description("SRX 05: Supreme Dance")]                  Exp05 = 0x05,
        [Description("SRX 06: Complete Orchestra")]             Exp06 = 0x06,
        [Description("SRX 07: Ultimate Keys")]                  Exp07 = 0x07,
        [Description("SRX 08: Platinum Trax")]                  Exp08 = 0x08,
        [Description("SRX 09: World Collection")]               Exp09 = 0x09,
        [Description("SRX 10: Big Brass Ensemble")]             Exp10 = 0x0A,
        [Description("SRX 11: Complete Piano")]                 Exp11 = 0x0B,
        [Description("SRX 12: Classic Electric Pianos")]        Exp12 = 0x0C,
        [Description("ExSN 01: SuperNATURAL Ethnic")]           Exp13 = 0x0D,
        [Description("ExSN 02: SuperNATURAL Woodwinds")]        Exp14 = 0x0E,
        [Description("ExSN 03: SuperNATURAL Session")]          Exp15 = 0x0F,
        [Description("ExSN 04: SuperNATURAL Acoustic Guitar")]  Exp16 = 0x10,
        [Description("ExSN 05: SuperNATURAL Brass")]            Exp17 = 0x11,
        [Description("ExSN 06: SuperNATURAL SFX")]              Exp18 = 0x12,
        [Description("ExPCM: HQ GM2 + HQ PCM Sound")]           Exp19 = 0x13
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
    public enum IntegraOutputAssigns : byte
    {
        [Description("Part")] OutputPart,
        [Description("A")] OutputA,
        [Description("B")] OutputB,
        [Description("C")] OutputC,
        [Description("D")] OutputD,
        [Description("1")] Output01,
        [Description("2")] Output02,
        [Description("3")] Output03,
        [Description("4")] Output04,
        [Description("5")] Output05,
        [Description("6")] Output06,
        [Description("7")] Output07,
        [Description("8")] Output08,
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

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraMatrixControlDestination : byte
    {
        Off = 0,
        PCH = 1,
        CUT = 2,
        RES = 3,
        LEV = 4,
        PAN = 5,
        DRY = 6,
        CHO = 7,
        REV = 8,
        PITLFO1 = 9,
        PITLFO2 = 10,
        TVFLFO1 = 11,
        TVFLFO2 = 12,
        TVALFO1 = 13,
        TFALFO2 = 14,
        PANLFO1 = 15,
        PANLFO2 = 16,
        RATELFO1 = 17,
        RATELFO2 = 18,
        PITATK = 19,
        PITDCY = 20,
        PITREL = 21,
        TVFATK = 22,
        TVFDCY = 23,
        TVFREL = 24,
        TVAATK = 25,
        TVADCY = 26,
        TVAREL = 27,
        PMT =  28,
        FXM = 29,
        RES01 = 30,
        RES02 = 31,
        RES03 = 32,
        RES04 = 33
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
        [Description("2000 [Hz]")] hz2000 = 0x0A,
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
        [Description("2000 [Hz]")] hz2000,
        [Description("4000 [Hz]")] Hz4000,
        [Description("8000 [Hz]")] Hz8000
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraChorusTypes : byte
    {
        [Description("Off")]        Off       = 0x00,
        [Description("Chorus")]     Chorus    = 0x01,
        [Description("Delay")]      Delay     = 0x02,
        [Description("GM2 Chorus")] GM2Chorus = 0x03
    }

    [TypeConverter(typeof(DescriptionConverter))]
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

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraVelocityCurveTypes : byte
    {
        [Description("Off")] Off  = 0x00,
        [Description("1")]   VC01 = 0x01,
        [Description("2")]   VC02 = 0x02,
        [Description("3")]   VC03 = 0x03,
        [Description("4")]   VC04 = 0x04
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

    public enum IntegraMuteSwitch : byte
    {
        Off  = 0x00,
        Mute = 0x01
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
        [Description("Off")] Part = 0x00,
        [Description("1")] CompEQ01 = 0x01,
        [Description("2")] CompEQ02 = 0x02,
        [Description("3")] CompEQ03 = 0x03,
        [Description("1")] CompEQ04 = 0x04,
        [Description("2")] CompEQ05 = 0x05,
        [Description("3")] CompEQ06 = 0x06,
    }

    public class IntegraKeyboardRange : Enumeration
    {
        public static new List<string> Values
        {
            get
            {
                List<string> values = new List<string>();

                int counter;
                string suffix;

                for (int i = 0; i < 128; i++)
                {
                    counter = (int)Math.Floor(i / 12d);

                    if (counter == 0)
                        suffix = "-";
                    else
                        suffix = (counter - 1).ToString();

                    switch (i % 12)
                    {
                        case 0: values.Add("C " + suffix); break;
                        case 1: values.Add("C#" + suffix); break;
                        case 2: values.Add("D " + suffix); break;
                        case 3: values.Add("Eb" + suffix); break;
                        case 4: values.Add("E " + suffix); break;
                        case 5: values.Add("F " + suffix); break;
                        case 6: values.Add("F#" + suffix); break;
                        case 7: values.Add("G " + suffix); break;
                        case 8: values.Add("G#" + suffix); break;
                        case 9: values.Add("A " + suffix); break;
                        case 10: values.Add("Bb" + suffix); break;
                        case 11: values.Add("B " + suffix); break;
                    }
                }

                return values;
            }
        }
    }

    public enum IntegraVelocityControl : byte
    {
        Off    = 0x00,
        On     = 0x01,
        Random = 0x02,
        Cycle  = 0x03
    }

    #region PCM Synth Tone Partial

    public enum IntegraEnvelopeMode : byte
    {
        NoSustain = 0x00,
        Sustain   = 0x01
    }

    public enum IntegraDelayMode : byte
    {
        Normal       = 0x00,
        Hold         = 0x01,
        KeyOffNormal = 0x02,
        KeyOffDecay  = 0x03
    }

    public enum IntegraControlSwitch : byte
    {
        Off     = 0x00,
        On      = 0x01,
        Reverse = 0x02
    }

    public enum IntegraWaveGroupType : byte
    {
        INT        = 0x00,
        SRX        = 0x01,
        Reserved01 = 0x02,
        Reserved02 = 0x03
    }

    public enum IntegraTVFFilterType : byte
    {
        OFF   = 0x00,
        LPF   = 0x01,
        BPF   = 0x02,
        HPF   = 0x03,
        PKG   = 0x04,
        LPF02 = 0x05,
        LPF03 = 0x06
    }

    public enum IntegraVelocityCurve : byte
    {
        Fixed   = 0x00,
        Curve01 = 0x01,
        Curve02 = 0x02,
        Curve03 = 0x03,
        Curve04 = 0x04,
        Curve05 = 0x05,
        Curve06 = 0x06,
        Curve07 = 0x07,
    }

    public enum IntegraBiasDirection : byte
    {
        Lower      = 0x00,
        Upper      = 0x01,
        LowerUpper = 0x02,
        All        = 0x03
    }

    public enum IntegraLFOWaveform : byte
    {
        SIN         = 0x00,
        TRI         = 0x01,
        SAWUP       = 0x02,
        SAWDOWN     = 0x03,
        SQR         = 0x04,
        RND         = 0x05,
        BENDUP      = 0x06,
        BENDDOWN    = 0x07,
        TRP         = 0x08,
        SH          = 0x09,
        CHS         = 0x0A,
        VSIN        = 0x0B,
        STEP        = 0x0C
    }

    public enum IntegraLFOFadeMode : byte
    {
        ONIN    = 0x00,
        ONOUT   = 0x01,
        OFFIN   = 0x02,
        OFFOUT  = 0x03
    }

    #endregion

    #region SuperNATURAL Synth Tone Partial

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

    #region PCM Drum Kit Partial

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

    #endregion

    #region MFX

    #region MFX: Low Boost

    public enum IntegraBoostFrequency : byte
    {
        [Description("50 [Hz]")]   Hz50    = 0x00,
        [Description("56 [Hz]")]   Hz56    = 0x01,
        [Description("63 [Hz]")]   Hz63    = 0x02,
        [Description("71 [Hz]")]   Hz71    = 0x03,
        [Description("80 [Hz]")]   Hz80    = 0x04,
        [Description("90 [Hz]")]   Hz90    = 0x05,
        [Description("100 [Hz]")]  Hz100   = 0x06,
        [Description("112 [Hz]")]  Hz112   = 0x07,
        [Description("125 [Hz]")]  Hz125   = 0x08
    }

    public enum IntegraBoostWidth : byte
    {
        Wide    = 0x00,
        Mid     = 0x01,
        Narrow  = 0x02
    }
    #endregion

    #region MFX: Step Filter

    public enum IntegraStepFilterType : byte
    {
        LPF     = 0x00,
        BPF     = 0x01,
        HPF     = 0x02,
        NOTCH   = 0x03
    }

    public enum IntegraFilterSlope : byte
    {
        [Description("-12 dB")] _12 = 0x00,
        [Description("-24 dB")] _24 = 0x01,
        [Description("-36 dB")] _36 = 0x02
    }

    #endregion

    #endregion
}
