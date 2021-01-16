using Integra.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
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
        [Description("Auto Pan")]
        AutoPan = 17,
        [Description("")] Slicer = 18,
        [Description("Rotary 1")]
        Rotary1 = 19,
        [Description("Rotary 2")]
        Rotary2 = 20,
        [Description("Rotary 3")]
        Rotary3 = 21,
        [Description("")] Chorus = 22,
        [Description("")] Flanger = 23,
        [Description("Step Flanger")]
        StepFlanger = 24,
        [Description("Hexa-Chorus")]
        HexaChorus = 25,
        [Description("Tremolo Chorus")]
        TremoloChorus = 26,
        [Description("Space-D")]
        SpaceD = 27,
        [Description("")] Overdrive = 28,
        [Description("")] Distortion = 29,
        [Description("Guitar Amp Simulator")]
        GuitarAmpSimulator = 30,
        [Description("")] Compressor = 31,
        [Description("")] Limiter = 32,
        [Description("")] Gate = 33,
        [Description("")] Delay = 34,
        [Description("Modulation Delay")]
        ModulationDelay = 35,
        [Description("3 Tap Pan Delay")]
        TapPanDelay3 = 36,
        [Description("4 Tap Pan Delay")]
        TapPanDelay4 = 37,
        [Description("Multi Tap Delay")]
        MultiTapDelay = 38,
        [Description("Reverse Delay")]
        ReverseDelay = 39,
        [Description("Time Control Delay")]
        TimeControlDelay = 40,
        [Description("LOFI Compressor")]
        LoFiCompressor = 41,
        [Description("Bit Crasher")]
        BitCrasher = 42,
        [Description("Pitch Shifter")]
        PitchShifter = 43,
        [Description("2 Voice Pitch Shifter")]
        VoicePitchShifter = 44,
        [Description("Overdrive -> Chorus")]
        OverdriveChorus = 45,
        [Description("Overdrive -> Flanger")]
        OverdriveFlanger = 46,
        [Description("Overdrive -> Delay")]
        OverdriveDelay = 47,
        [Description("Distortion -> Chorus")]
        DistortionChorus = 48,
        [Description("Distortion -> Flanger")]
        DistortionFlanger = 49,
        [Description("Distortion -> Delay")]
        DistortionDelay = 50,
        [Description("Overdrive / Distortion Touch Wah")]   ODDSTouchWah        = 51,
        [Description("Overdrive / Distortion Auto Wah")]
        ODDSAutoWah = 52,
        [Description("Guitar Amp Simulator -> Chorus")]
        GuitarAmpSimChorus = 53,
        [Description("Guitar Amp Simulator -> Flanger")]    GuitarAmpSimFlanger = 54,
        [Description("Guitar Amp Simulator -> Phaser")]
        GuitarAmpSimPhaser = 55,
        [Description("Guitar Amp Simulator -> Delay")]
        GuitarAmpSimDelay = 56,
        [Description("EP Amp Simulator -> Tremolo")]
        EPAmpSimTremolo = 57,
        [Description("EP Amp Simulator -> Chorus")]
        EPAmpSimChorus = 58,
        [Description("EP Amp Simulator -> Flanger")]
        EPAmpSimFlanger = 59,
        [Description("EP Amp Simulator -> Phaser")]
        EPAmpSimPhaser = 60,
        [Description("EP Amp Simulator -> Delay")]
        EPAmpSimDelay = 61,
        [Description("Enhancer -> Chorus")]
        EnhancerChorus = 62,
        [Description("Enhancer -> Flanger")]
        EnhancerFlanger = 63,
        [Description("Enhancer -> Delay")]
        EnhancerDelay = 64,
        [Description("Chorus -> Delay")]
        ChorusDelay = 65,
        [Description("Flanger -> Delay")]
        FlangerDelay = 66,
        [Description("Chorus -> Flanger")]
        ChorusFlanger = 67
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
    public enum IntegraMFXControlAssigns
    {
        [Description("Off")] Off,
        [Description("01")] Channel1,
        [Description("02")] Channel2,
        [Description("03")] Channel3,
        [Description("04")] Channel4,
        [Description("05")] Channel5,
        [Description("06")] Channel6,
        [Description("07")] Channel7,
        [Description("08")] Channel8,
        [Description("09")] Channel9,
        [Description("10")] Channel10,
        [Description("11")] Channel11,
        [Description("12")] Channel12,
        [Description("13")] Channel13,
        [Description("14")] Channel14,
        [Description("15")] Channel15,
        [Description("16")] Channel16,
    }

    [TypeConverter(typeof(DescriptionConverter))]
    public enum IntegraLowFrequencies : int
    {
        [Description("200 [Hz]")] Hz200 = 0,
        [Description("400 [Hz]")] Hz400 = 1
    }
}
