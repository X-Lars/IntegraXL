using Integra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    public static class IntegraToneExtensions
    {
        /// <summary>
        /// Gets the tone bank the tone resides in.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IntegraToneBanks ToneBank<T>(this T value) where T : Tone
        {
            if (Enum.IsDefined(typeof(IntegraToneBanks), (ushort)(((value.MSB << 8) + value.LSB) & 0xFFF0)))
            {
                if (Enum.IsDefined(typeof(IntegraToneBanks), (ushort)((value.MSB << 8) + value.LSB)))
                {
                    return (IntegraToneBanks)((value.MSB << 8) + value.LSB);
                }
                else
                {
                    return (IntegraToneBanks)(((value.MSB << 8) + value.LSB) & 0xFFF0);
                }
            }
            else
            {
                return IntegraToneBanks.Undefined;
            }
        }

        public static bool IsUserTone<T>(this T value) where T : Tone
        {
            switch (value.MSB)
            {
                case 0x56: // PCM Drumkit
                case 0x57: // PCM Tone
                case 0x58: // SuperNATURAL Drumkit
                case 0x59: // SuperNATURAL Acoustic Tone
                case 0x5F: // SuperNATURAL Synth Tone

                    bool result = value.LSB < 0x40 ? true : false;

                    return result;

                default:

                    return false;

            }
        }

        public static bool IsExpansion<T>(this T value) where T : Tone
        {
            switch (value.MSB)
            {
                case 0x5C: // SRX Drum Kit
                case 0x5D: // SRX Tone
                case 0x60: // ExPCM Drum Kit
                case 0x61: // ExPCM Tone
                    return true;

                case 0x59: // SuperNATURAL Acoustic Tone

                    // ExSN
                    return value.LSB >= 0x60 ? true : false;

                default:
                    return false;
            }
        }

        public static IntegraToneTypes Type<T>(this T value) where T : Tone
        {
            switch (value.MSB)
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
                    throw new ArgumentOutOfRangeException(nameof(value.MSB));
            }
        }

        public static IntegraToneTypes Type(byte msb)
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
                    throw new ArgumentOutOfRangeException(nameof(msb));
            }
        }
    }
}
