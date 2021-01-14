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
}
