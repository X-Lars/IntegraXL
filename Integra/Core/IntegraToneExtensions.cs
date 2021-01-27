using Integra.Core.Interfaces;
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
        public static IntegraToneBanks ToneBank<T>(this T value) where T : IIntegraAddressable
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

        public static IntegraExpansions GetExpansion<T>(this T instance) where T : IIntegraAddressable
        {
            switch (instance.MSB)
            {
                case 0x5C: // SRX Drum Kit
                case 0x5D: // SRX Tone

                    switch(instance.LSB)
                    {
                        case 0x00: return IntegraExpansions.Exp01;
                        case 0x01: return IntegraExpansions.Exp02;
                        case 0x02: return IntegraExpansions.Exp03;
                        case 0x03: return IntegraExpansions.Exp04;

                        case 0x04:
                        case 0x05:
                        case 0x06: return IntegraExpansions.Exp05;

                        case 0x07:
                        case 0x08:
                        case 0x09:
                        case 0x0A: return IntegraExpansions.Exp06;

                        case 0x0B:
                        case 0x0C:
                        case 0x0D:
                        case 0x0E: return IntegraExpansions.Exp07;

                        case 0x0F:
                        case 0x10:
                        case 0x11:
                        case 0x12: return IntegraExpansions.Exp08;

                        case 0x13:
                        case 0x14:
                        case 0x15:
                        case 0x16: return IntegraExpansions.Exp09;

                        case 0x17: return IntegraExpansions.Exp10;
                        case 0x18: return IntegraExpansions.Exp11;
                        case 0x1A: return IntegraExpansions.Exp12;
                    }

                    return IntegraExpansions.Off;

                case 0x59:

                    // ExSN Tone
                    switch(instance.LSB)
                    {
                        case 0x60: return IntegraExpansions.Exp13;
                        case 0x61: return IntegraExpansions.Exp14;
                        case 0x62: return IntegraExpansions.Exp15;
                        case 0x63: return IntegraExpansions.Exp16;
                        case 0x64: return IntegraExpansions.Exp17;
                        case 0x65: return IntegraExpansions.Exp18;
                    }

                    return IntegraExpansions.Off;

                case 0x60:
                case 0x61:

                    return IntegraExpansions.Exp19;

                default:
                    return IntegraExpansions.Off;
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
                case 0x00:
                    // NOT INITIALIZED YET
                    return IntegraToneTypes.SuperNATURALAcousticTone;

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
