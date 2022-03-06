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
            ushort bank = (ushort)((instance.MSB << 8) | instance.LSB);

            // 0x5D11 ERROR
            // Check for single addressable tone banks
            // The low nibble can vary for big tone banks

            if(Enum.IsDefined(typeof(IntegraToneBanks), (ushort)(bank & 0xFF00)))
            {
                if(Enum.IsDefined(typeof(IntegraToneBanks), bank))
                {
                    return (IntegraToneBanks)bank;
                }
                else
                {
                    byte i = (byte)(instance.LSB & 0x0F);
                    ushort b = (ushort)(bank & 0xFFF0);
                    //for (; i >= 0; i--)
                    //{
                    //    if (Enum.IsDefined(typeof(IntegraToneBanks), (ushort)(b | i)))
                    //        return (IntegraToneBanks)(b | i);
                    //}

                    i = instance.LSB;
                    b = (ushort)(bank & 0xFF00);

                    for (; i >= 0; i--)
                    {
                        if (Enum.IsDefined(typeof(IntegraToneBanks), (ushort)(b | i)))
                            return (IntegraToneBanks)(b | i);
                    }

                    throw new ArgumentOutOfRangeException($"[{nameof(IntegraToneExtensions)}.{nameof(ToneBank)}] 0x{instance.MSB:X2}{instance.MSB:X2}");
                    
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException($"[{nameof(IntegraToneExtensions)}.{nameof(ToneBank)}] 0x{instance.MSB:X2}{instance.MSB:X2}");
            }
        }

        /// <summary>
        /// Gets the expansion based on the MSB and LSB of the bank select.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance providing the MSB and LSB.</param>
        /// <returns>The associated expansion.</returns>
        public static IntegraExpansions GetExpansion<T>(this T instance) where T : IBankSelect
        {
            switch (instance.MSB)
            {
                case 0x5C:
                case 0x5D:

                    switch (instance.LSB)
                    {
                        case 0x00: return IntegraExpansions.SRX01;
                        case 0x01: return IntegraExpansions.SRX02;
                        case 0x02: return IntegraExpansions.SRX03;
                        case 0x03: return IntegraExpansions.SRX04;

                        case 0x04:
                        case 0x05:
                        case 0x06: return IntegraExpansions.SRX05;

                        case 0x07:
                        case 0x08:
                        case 0x09:
                        case 0x0A: return IntegraExpansions.SRX06;

                        case 0x0B:
                        case 0x0C:
                        case 0x0D:
                        case 0x0E: return IntegraExpansions.SRX07;

                        case 0x0F:
                        case 0x10:
                        case 0x11:
                        case 0x12: return IntegraExpansions.SRX08;

                        case 0x13:
                        case 0x14:
                        case 0x15:
                        case 0x16: return IntegraExpansions.SRX09;

                        case 0x17: return IntegraExpansions.SRX10;
                        case 0x18: return IntegraExpansions.SRX11;
                        case 0x1A: return IntegraExpansions.SRX12;
                    }

                    return IntegraExpansions.Off;

                case 0x59:

                    switch (instance.LSB)
                    {
                        case 0x60: return IntegraExpansions.ExSN01;
                        case 0x61: return IntegraExpansions.ExSN02;
                        case 0x62: return IntegraExpansions.ExSN03;
                        case 0x63: return IntegraExpansions.ExSN04;
                        case 0x64: return IntegraExpansions.ExSN05;
                        case 0x65: return IntegraExpansions.ExSN06;
                    }

                    return IntegraExpansions.Off;

                case 0x60:
                case 0x61:

                    return IntegraExpansions.ExPCM;

                default:
                    return IntegraExpansions.Off;
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

                    // TODO: Tone type can be undefined, tone ID 0000 Piano 1 when error
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

        /// <summary>
        /// Gets wheter the tone is editable by the user.
        /// </summary>
        /// <param name="instance">The most significant byte of the tone's request.</param>
        /// <returns>True if the tone is editable.</returns>
        public static bool IsEditable(this IBankSelect instance)
        {
            switch (instance.MSB)
            {
                // ExPCM Expansion
                case 0x60:
                case 0x61:

                // GM2
                case 0x78:
                case 0x79:
                    return false;

                default:
                    return true;
            }
        }
    }
}
