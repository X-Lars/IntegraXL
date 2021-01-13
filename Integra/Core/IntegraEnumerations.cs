using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    public enum IntegraSoundModes : byte
    {
        Studio  = 0x01,
        GM      = 0x02,
        GM2     = 0x03,
        GS      = 0x04
    }

    //[TypeConverter(typeof(EnumerationDescriptionConverter))]
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
