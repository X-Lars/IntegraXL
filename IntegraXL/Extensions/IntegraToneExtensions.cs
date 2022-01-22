using IntegraXL.Core;
using IntegraXL.Interfaces;
using IntegraXL.Models;

namespace IntegraXL.Extensions
{
    /// <summary>
    /// Defines extension methods to get additional immutable tone data.
    /// </summary>
    public static class IntegraToneExtensions
    {
        /// <summary>
        /// Gets the tone bank of an <see cref="IBankSelect"/> implementing class.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IntegraToneBanks ToneBank(this IBankSelect instance)
        {
            ushort bank = (ushort)((instance.MSB << 8) + instance.LSB);

            // Check for single addressable tone banks
            // The low nibble can vary for a long tone banks
            if (Enum.IsDefined(typeof(IntegraToneBanks), (ushort)(bank & 0xFFF0)))
            {
                // Check for tone banks that multi address range tone banks
                // 
                if (Enum.IsDefined(typeof(IntegraToneBanks), bank))
                {
                    // Get the tone bank based on the full address
                    return (IntegraToneBanks)bank;
                }
                else
                {
                    return (IntegraToneBanks)(bank & 0xFFF0);
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException($"[{nameof(IntegraToneExtensions)}.{nameof(ToneBank)}]");
                //return IntegraToneBanks.Undefined;
            }
        }

        /// <summary>
        /// Gets the tone's type defined by the <see cref="IntegraToneTypes"/> enumeration.
        /// </summary>
        /// <param name="instance">The <see cref="IBankSelect"/> implementing instance.</param>
        /// <returns>The tone's type based on the <see cref="IBankSelect"/> interface.</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static IntegraToneTypes ToneType(this IBankSelect instance)
        {
            return ToneType(instance.MSB);
        }

        /// <summary>
        /// Gets the tone's type defined by the <see cref="IntegraToneTypes"/> enumeration.
        /// </summary>
        /// <param name="msb">The MSB of the tone.</param>
        /// <returns>A tone type based the <paramref name="msb"/> value.</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static IntegraToneTypes ToneType(this byte msb)
        {
            switch (msb)
            {
                case 0x56:
                case 0x5C: // SRX
                case 0x60:
                case 0x78:
                    return IntegraToneTypes.PCMDrumkit;

                case 0x57:
                case 0x5D: // SRX
                case 0x61:
                case 0x79:
                    return IntegraToneTypes.PCMSynthTone;

                case 0x58:
                    return IntegraToneTypes.SuperNATURALDrumkit;

                case 0x59:
                    return IntegraToneTypes.SuperNATURALAcousticTone;

                case 0x5F:
                    return IntegraToneTypes.SuperNATURALSynthTone;

                default:
                    throw new ArgumentOutOfRangeException($"[{nameof(IntegraToneExtensions)}.{nameof(ToneType)}]");
            }
        }

        /// <summary>
        /// Gets the model type of an <see cref="IntegraToneBanks"/> enumeration value.
        /// </summary>
        /// <param name="tonebank">The tone bank to get the model type.</param>
        /// <returns>A tone bank model type.</returns>
        public static Type? ToneBankType(this IntegraToneBanks tonebank)
        {
            return tonebank switch
            {
                IntegraToneBanks.SNAPresetTone => typeof(SNAPresetToneBank),
                IntegraToneBanks.SNSPresetTone => typeof(SNSPresetToneBank),
                IntegraToneBanks.SNDPresetDrum => typeof(SNDPresetToneBank),
                IntegraToneBanks.PCMPresetTone => typeof(PCMPresetToneBank),
                IntegraToneBanks.PCMPresetDrum => typeof(PCMPresetDrumKits),
                IntegraToneBanks.SNAUserTone   => typeof(SNAUserToneBank),
                IntegraToneBanks.SNSUserTone   => typeof(SNSUserToneBank),
                IntegraToneBanks.SNDUserDrum   => typeof(SNDUserToneBank),
                IntegraToneBanks.PCMUserTone   => typeof(PCMUserToneBank),
                IntegraToneBanks.PCMUserDrum   => typeof(PCMUserDrumKits),
                IntegraToneBanks.ExSn01Tone    => typeof(ExSN01ToneBank),
                IntegraToneBanks.ExSN02Tone    => typeof(ExSN02ToneBank),
                IntegraToneBanks.ExSN03Tone    => typeof(ExSN03ToneBank),
                IntegraToneBanks.ExSN04Tone    => typeof(ExSN04ToneBank),
                IntegraToneBanks.ExSN05Tone    => typeof(ExSN05ToneBank),
                IntegraToneBanks.ExSN06Drum    => typeof(ExSN06ToneBank),
                IntegraToneBanks.SRX01Tone     => typeof(SRX01ToneBank),
                IntegraToneBanks.SRX02Tone     => typeof(SRX02ToneBank),
                IntegraToneBanks.SRX03Tone     => typeof(SRX03ToneBank),
                IntegraToneBanks.SRX04Tone     => typeof(SRX04ToneBank),
                IntegraToneBanks.SRX05Tone     => typeof(SRX05ToneBank),
                IntegraToneBanks.SRX06Tone     => typeof(SRX06ToneBank),
                IntegraToneBanks.SRX07Tone     => typeof(SRX07ToneBank),
                IntegraToneBanks.SRX08Tone     => typeof(SRX08ToneBank),
                IntegraToneBanks.SRX09Tone     => typeof(SRX09ToneBank),
                IntegraToneBanks.SRX10Tone     => typeof(SRX10ToneBank),
                IntegraToneBanks.SRX11Tone     => typeof(SRX11ToneBank),
                IntegraToneBanks.SRX12Tone     => typeof(SRX12ToneBank),
                IntegraToneBanks.SRX01Drum     => typeof(SRX01DrumKits),
                IntegraToneBanks.SRX03Drum     => typeof(SRX03DrumKits),
                IntegraToneBanks.SRX05Drum     => typeof(SRX05DrumKits),
                IntegraToneBanks.SRX06Drum     => typeof(SRX06DrumKits),
                IntegraToneBanks.SRX07Drum     => typeof(SRX07DrumKits),
                IntegraToneBanks.SRX08Drum     => typeof(SRX08DrumKits),
                IntegraToneBanks.SRX09Drum     => typeof(SRX09DrumKits),
                IntegraToneBanks.ExPCMTone     => typeof(ExPCMTonebank),
                IntegraToneBanks.ExPCMDrum     => typeof(ExPCMDrumKits),
                IntegraToneBanks.GM2Tone       => typeof(GM2ToneBank),
                IntegraToneBanks.GM2Drum       => typeof(GM2DrumKits),
                _ => null,
            };
        }
    }
}
