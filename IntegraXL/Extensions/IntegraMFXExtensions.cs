using IntegraXL.Core;
using IntegraXL.Models;
using IntegraXL.Models.Parameters;

namespace IntegraXL.Extensions
{
    internal static class IntegraMFXExtensions
    {
        /// <summary>
        /// Deserializes an MFX parameter.
        /// </summary>
        /// <param name="value">The value to deserialize.</param>
        /// <returns>The integer value of the MFX parameter.</returns>
        /// <remarks><i>
        /// - Removes the MFX parameter byte.<br/>
        /// </i></remarks>
        internal static int DeserializeMFX(this int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return ((bytes[1] & 0x0F) << 8 | (bytes[2] & 0x0F) << 4 | (bytes[3] & 0x0F));
        }

        /// <summary>
        /// Serializes a value to an MFX parameter.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The integer MFX parameter.</returns>
        /// <remarks><i>
        /// - Adds the MFX parameter byte.<br/>
        /// </i></remarks>
        internal static int SerializeMFX(this int value)
        {
            byte[] bytes = new byte[4];

            bytes[0] = 0x08; // Magic number
            bytes[1] = (byte)((value >> 8) & 0x0F);
            bytes[2] = (byte)((value >> 4) & 0x0F);
            bytes[3] = (byte)((value & 0x0F));

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }

        internal static int DeserializeFeedback(this int value)
        {
            // TODO: Check Clamp value
            return (value * 2) - 98;
        }

        internal static int SerializeFeedback(this int value)
        {
            // TODO: Check Clamp value
            return (value + 98) / 2;
        }

        /// <summary>
        /// Gets the list of controllable MFX parameters based on the <see cref="MFX.Type"/>.
        /// </summary>
        /// <param name="instance">The MFX model.</param>
        /// <returns>The list of controllable MFX parameters.</returns>
        internal static List<string> GetMFXControls(this MFX instance)
        {
            List<string> controls = new();

            controls.Add("Off");

            switch (instance.Type)
            {
                case IntegraMFXTypes.Equalizer:
                    controls.Add("Low Gain");
                    controls.Add("High Gain");
                    controls.Add("Level");
                    break;

                case IntegraMFXTypes.Spectrum:
                    controls.Add("Level");
                    break;

                case IntegraMFXTypes.LowBoost:
                    controls.Add("Boost Frequency");
                    controls.Add("Boost Gain");
                    break;

                case IntegraMFXTypes.StepFilter:
                    controls.Add("Rate");
                    controls.Add("Attack");
                    controls.Add("Filter Resonance");
                    controls.Add("Step Reset");
                    break;

                case IntegraMFXTypes.Enhancer:
                    controls.Add("Sens");
                    controls.Add("Mix");
                    break;

                case IntegraMFXTypes.AutoWah:
                    controls.Add("Manual");
                    controls.Add("Sens");
                    controls.Add("Rate");
                    controls.Add("Depth");
                    controls.Add("Phase");
                    break;

                case IntegraMFXTypes.Humanizer:
                    controls.Add("Drive");
                    controls.Add("Rate");
                    controls.Add("Depth");
                    controls.Add("Manual");
                    controls.Add("Pan");
                    break;

                case IntegraMFXTypes.SpeakerSimulator:
                    controls.Add("Mic Level");
                    controls.Add("Direct Level");
                    controls.Add("Level");
                    break;

                case IntegraMFXTypes.Phaser1:
                    controls.Add("Manual");
                    controls.Add("Rate");
                    controls.Add("Resonance");
                    controls.Add("Mix");
                    break;

                case IntegraMFXTypes.Phaser2:
                    controls.Add("Rate");
                    break;

                case IntegraMFXTypes.Phaser3:
                    controls.Add("Speed");
                    break;

                case IntegraMFXTypes.StepPhaser:
                    controls.Add("Manual");
                    controls.Add("Rate");
                    controls.Add("Resonance");
                    controls.Add("Step Rate");
                    controls.Add("Mix");
                    break;

                case IntegraMFXTypes.MultiStagePhaser:
                    controls.Add("Manual");
                    controls.Add("Rate");
                    controls.Add("Resonance");
                    controls.Add("Mix");
                    controls.Add("Pan");
                    break;

                case IntegraMFXTypes.InfinitePhaser:
                    controls.Add("Speed");
                    controls.Add("Resonance");
                    controls.Add("Mix");
                    controls.Add("Pan");
                    break;

                case IntegraMFXTypes.RingModulator:
                    controls.Add("Frequency");
                    controls.Add("Sens");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.Tremolo:
                case IntegraMFXTypes.AutoPan:
                    controls.Add("Rate");
                    controls.Add("Depth");
                    break;

                case IntegraMFXTypes.Slicer:
                    controls.Add("Rate");
                    controls.Add("Attack");
                    controls.Add("Shuffle");
                    controls.Add("Step Reset");
                    break;

                case IntegraMFXTypes.Rotary1:
                    controls.Add("Speed");
                    controls.Add("Level");
                    break;

                case IntegraMFXTypes.Rotary2:
                    controls.Add("Speed");
                    controls.Add("Brake");
                    controls.Add("Level");
                    break;

                case IntegraMFXTypes.Rotary3:
                    controls.Add("Speed");
                    controls.Add("Brake");
                    controls.Add("OD Gain");
                    controls.Add("OD Drive");
                    controls.Add("Level");
                    break;

                case IntegraMFXTypes.Chorus:
                case IntegraMFXTypes.HexaChorus:
                case IntegraMFXTypes.SpaceD:
                    controls.Add("Rate");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.Flanger:
                    controls.Add("Rate");
                    controls.Add("Feedback");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.StepFlanger:
                    controls.Add("Rate");
                    controls.Add("Feedback");
                    controls.Add("Step Rate");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.TremoloChorus:
                    controls.Add("Chorus Rate");
                    controls.Add("Tremolo Rate");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.Overdrive:
                case IntegraMFXTypes.Distortion:
                    controls.Add("Drive");
                    controls.Add("Tone");
                    controls.Add("Pan");
                    break;

                case IntegraMFXTypes.GuitarAmpSimulator:
                    controls.Add("Amp Volume");
                    controls.Add("Amp Master");
                    controls.Add("Pan");
                    controls.Add("Level");
                    break;

                case IntegraMFXTypes.Compressor:
                    controls.Add("Attack");
                    controls.Add("Threshold");
                    controls.Add("Level");
                    break;

                case IntegraMFXTypes.Limiter:
                    controls.Add("Release");
                    controls.Add("Threshold");
                    controls.Add("Level");
                    break;

                case IntegraMFXTypes.Gate:
                    controls.Add("Threshold");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.Delay:
                    controls.Add("Feedback");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.ModulationDelay:
                    controls.Add("Feedback");
                    controls.Add("Rate");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.TapPanDelay3:
                    controls.Add("Center Feedback");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.TapPanDelay4:
                case IntegraMFXTypes.MultiTapDelay:
                    controls.Add("Delay 1 Feedback");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.ReverseDelay:
                    controls.Add("Reverse Delay Feedback");
                    controls.Add("Delay 3 Feedback");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.TimeControlDelay:
                    controls.Add("Delay Time");
                    controls.Add("Feedback");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.LoFiCompressor:
                    controls.Add("Balance");
                    controls.Add("Level");
                    break;

                case IntegraMFXTypes.BitCrasher:
                    controls.Add("Sample Rate");
                    controls.Add("Bit Down");
                    controls.Add("Filter");
                    break;

                case IntegraMFXTypes.PitchShifter:
                    controls.Add("Pitch");
                    controls.Add("Feedback");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.VoicePitchShifter:
                    controls.Add("Pitch 1 Pitch");
                    controls.Add("Pitch 1 Feedback");
                    controls.Add("Pitch 1 Pan");
                    controls.Add("Pitch 2 Pitch");
                    controls.Add("Pitch 2 Feedback");
                    controls.Add("Pitch 2 Pan");
                    controls.Add("Balance");
                    break;

                case IntegraMFXTypes.OverdriveChorus:
                    controls.Add("Overdrive Drive");
                    controls.Add("Overdrive Pan");
                    controls.Add("Chorus Rate");
                    controls.Add("Chorus Balance");
                    break;

                case IntegraMFXTypes.OverdriveFlanger:
                    controls.Add("Overdrive Drive");
                    controls.Add("Overdrive Pan");
                    controls.Add("Flanger Rate");
                    controls.Add("Flanger Feedback");
                    controls.Add("Flanger Balance");
                    break;

                case IntegraMFXTypes.OverdriveDelay:
                    controls.Add("Overdrive Drive");
                    controls.Add("Overdrive Pan");
                    controls.Add("Delay Feedback");
                    controls.Add("Delay Balance");
                    break;

                case IntegraMFXTypes.DistortionChorus:
                    controls.Add("Distortion Drive");
                    controls.Add("Distortion Pan");
                    controls.Add("Chorus Rate");
                    controls.Add("Chorus Balance");
                    break;

                case IntegraMFXTypes.DistortionFlanger:
                    controls.Add("Distortion Drive");
                    controls.Add("Distortion Pan");
                    controls.Add("Flanger Rate");
                    controls.Add("Flanger Feedback");
                    controls.Add("Flanger Balance");
                    break;

                case IntegraMFXTypes.DistortionDelay:
                    controls.Add("Distortion Drive");
                    controls.Add("Distortion Pan");
                    controls.Add("Delay Feedback");
                    controls.Add("Delay Balance");
                    break;

                case IntegraMFXTypes.ODDSTouchWah:
                    controls.Add("Drive");
                    controls.Add("Tone");
                    controls.Add("Touch Wah Sens");
                    controls.Add("Touch Wah Manual");
                    controls.Add("Touch Wah Peak");
                    controls.Add("Touch Wah Balance");
                    break;

                case IntegraMFXTypes.ODDSAutoWah:
                    controls.Add("Drive");
                    controls.Add("Tone");
                    controls.Add("Auto Wah Manual");
                    controls.Add("Auto Wah Peak");
                    controls.Add("Auto Wah Rate");
                    controls.Add("Auto Wah Depth");
                    controls.Add("Auto Wah Balance");
                    break;

                case IntegraMFXTypes.GuitarAmpSimChorus:
                    controls.Add("Amp Volume");
                    controls.Add("Amp Master");
                    controls.Add("Chorus Switch");
                    controls.Add("Chorus Rate");
                    controls.Add("Chorus Depth");
                    controls.Add("Chorus Balance");
                    break;

                case IntegraMFXTypes.GuitarAmpSimFlanger:
                    controls.Add("Amp Volume");
                    controls.Add("Amp Master");
                    controls.Add("Flanger Switch");
                    controls.Add("Flanger Rate");
                    controls.Add("Flanger Depth");
                    controls.Add("Flanger Feedback");
                    controls.Add("Flanger Balance");
                    break;

                case IntegraMFXTypes.GuitarAmpSimPhaser:
                    controls.Add("Amp Volume");
                    controls.Add("Amp Master");
                    controls.Add("Phaser Switch");
                    controls.Add("Phaser Rate");
                    controls.Add("Phaser Manual");
                    controls.Add("Phaser Depth");
                    controls.Add("Phaser Resonance");
                    controls.Add("Phaser Mix");
                    break;

                case IntegraMFXTypes.GuitarAmpSimDelay:
                    controls.Add("Amp Volume");
                    controls.Add("Amp Master");
                    controls.Add("Delay Switch");
                    controls.Add("Delay Time");
                    controls.Add("Delay Feedback");
                    controls.Add("Delay Balance");
                    break;

                case IntegraMFXTypes.EPAmpSimTremolo:
                    controls.Add("Treble");
                    controls.Add("Bass");
                    controls.Add("Tremolo Switch");
                    controls.Add("Tremolo Rate");
                    controls.Add("Tremolo Depth");
                    break;

                case IntegraMFXTypes.EPAmpSimChorus:
                    controls.Add("Treble");
                    controls.Add("Bass");
                    controls.Add("Chorus Switch");
                    controls.Add("Chorus Rate");
                    controls.Add("Chorus Depth");
                    controls.Add("Chorus Balance");
                    break;

                case IntegraMFXTypes.EPAmpSimFlanger:
                    controls.Add("Treble");
                    controls.Add("Bass");
                    controls.Add("Flanger Switch");
                    controls.Add("Flanger Rate");
                    controls.Add("Flanger Depth");
                    controls.Add("Flanger Feedback");
                    controls.Add("Flanger Balance");
                    break;

                case IntegraMFXTypes.EPAmpSimPhaser:
                    controls.Add("Treble");
                    controls.Add("Bass");
                    controls.Add("Phaser Switch");
                    controls.Add("Phaser Rate");
                    controls.Add("Phaser Manual");
                    controls.Add("Phaser Depth");
                    controls.Add("Phaser Resonance");
                    controls.Add("Phaser Mix");
                    break;

                case IntegraMFXTypes.EPAmpSimDelay:
                    controls.Add("Treble");
                    controls.Add("Bass");
                    controls.Add("Delay Switch");
                    controls.Add("Delay Time");
                    controls.Add("Delay Feedback");
                    controls.Add("Delay Balance");
                    break;

                case IntegraMFXTypes.EnhancerChorus:
                    controls.Add("Enhancer Sens");
                    controls.Add("Enhancer Mix");
                    controls.Add("Chorus Rate");
                    controls.Add("Chorus Balance");
                    break;

                case IntegraMFXTypes.EnhancerFlanger:
                    controls.Add("Enhancer Sens");
                    controls.Add("Enhancer Mix");
                    controls.Add("Flanger Rate");
                    controls.Add("Flanger Feedback");
                    controls.Add("Flanger Balance");
                    break;

                case IntegraMFXTypes.EnhancerDelay:
                    controls.Add("Enhancer Sens");
                    controls.Add("Enhancer Mix");
                    controls.Add("Delay Feedback");
                    controls.Add("Delay Balance");
                    break;

                case IntegraMFXTypes.ChorusDelay:
                    controls.Add("Chorus Rate");
                    controls.Add("Chorus Balance");
                    controls.Add("Delay Feedback");
                    controls.Add("Delay Balance");
                    break;

                case IntegraMFXTypes.FlangerDelay:
                    controls.Add("Flanger Rate");
                    controls.Add("Flanger Feedback");
                    controls.Add("Flanger Balance");
                    controls.Add("Delay Feedback");
                    controls.Add("Delay Balance");
                    break;

                case IntegraMFXTypes.ChorusFlanger:
                    controls.Add("Chorus Rate");
                    controls.Add("Chorus Balance");
                    controls.Add("Flanger Rate");
                    controls.Add("Flanger Feedback");
                    controls.Add("Flanger Balance");
                    break;
            }

            return controls;
        }

        internal static void SetMFXProvider(this MFX instance)
        {
            switch(instance.Type)
            {
                case IntegraMFXTypes.Equalizer: instance.Parameters = new Equalizer(instance); break;

                default:
                    instance.Parameters = new Thru(instance);
                    break;
            }
        }
    }
}
