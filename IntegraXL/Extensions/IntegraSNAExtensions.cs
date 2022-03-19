using IntegraXL.Core;
using IntegraXL.Models;
using IntegraXL.Models.Providers;

namespace IntegraXL.Extensions
{
    public static class IntegraSNAExtensions
    {

        public static IntegraSNAInstruments GetInstrument(this SuperNATURALAcousticToneCommon instance)
        {
            
            short value = 0;
            value += (short)(instance.InstVariation << 8);
            value += (byte)(instance.InstNumber);
            int index = 0;

            switch(value)
            {
                case 0x4000: index =   0; break;
                case 0x4001: index =   1; break;
                case 0x4002: index =   2; break;
                case 0x4003: index =   3; break;
                case 0x4004: index =   4; break;
                case 0x4005: index =   5; break;
                case 0x4006: index =   6; break;
                case 0x4007: index =   7; break;
                case 0x4008: index =   8; break;
                case 0x0004: index =   9; break;
                case 0x0104: index =  10; break;
                case 0x0204: index =  11; break;
                case 0x0304: index =  12; break;
                case 0x0604: index =  13; break;
                case 0x0704: index =  14; break;
                case 0x0007: index =  15; break;
                case 0x0107: index =  16; break;
                case 0x0207: index =  17; break;
                case 0x0307: index =  18; break;
                case 0x0407: index =  19; break;
                case 0x0507: index =  20; break;
                case 0x0607: index =  21; break;
                case 0x0707: index =  22; break;
                case 0x0009: index =  23; break;
                case 0x000B: index =  24; break;
                case 0x000C: index =  25; break;
                case 0x000D: index =  26; break;
                case 0x000E: index =  27; break;
                case 0x4100: index =  28; break;
                case 0x0015: index =  29; break;
                case 0x0115: index =  30; break;
                case 0x0016: index =  31; break;
                case 0x0017: index =  32; break;
                case 0x0018: index =  33; break;
                case 0x0118: index =  34; break;
                case 0x0019: index =  35; break;
                case 0x001A: index =  36; break;
                case 0x001B: index =  37; break;
                case 0x011B: index =  38; break;
                case 0x021B: index =  39; break;
                case 0x0020: index =  40; break;
                case 0x0021: index =  41; break;
                case 0x0022: index =  42; break;
                case 0x0023: index =  43; break;
                case 0x0028: index =  44; break;
                case 0x0128: index =  45; break;
                case 0x0029: index =  46; break;
                case 0x002A: index =  47; break;
                case 0x012A: index =  48; break;
                case 0x002B: index =  49; break;
                case 0x002E: index =  50; break;
                case 0x002F: index =  51; break;
                case 0x0030: index =  52; break;
                case 0x0130: index =  53; break;
                case 0x0034: index =  54; break;
                case 0x0134: index =  55; break;
                case 0x0038: index =  56; break;
                case 0x0039: index =  57; break;
                case 0x0339: index =  58; break;
                case 0x003B: index =  59; break;
                case 0x003C: index =  60; break;
                case 0x0140: index =  61; break;
                case 0x0141: index =  62; break;
                case 0x0242: index =  63; break;
                case 0x0143: index =  64; break;
                case 0x0044: index =  65; break;
                case 0x0046: index =  66; break;
                case 0x0047: index =  67; break;
                case 0x0048: index =  68; break;
                case 0x0049: index =  69; break;
                case 0x004B: index =  70; break;
                case 0x004D: index =  71; break;
                case 0x0068: index =  72; break;
                case 0x006D: index =  73; break;
                case 0x016D: index =  74; break;
                case 0x016E: index =  75; break;
                case 0x0072: index =  76; break;
                case 0x000F: index =  77; break;
                case 0x012E: index =  78; break;
                case 0x014B: index =  79; break;
                case 0x014D: index =  80; break;
                case 0x006A: index =  81; break;
                case 0x016A: index =  82; break;
                case 0x006B: index =  83; break;
                case 0x016B: index =  84; break;
                case 0x006C: index =  85; break;
                case 0x026E: index =  86; break;
                case 0x0040: index =  87; break;
                case 0x0041: index =  88; break;
                case 0x0042: index =  89; break;
                case 0x0043: index =  90; break;
                case 0x0045: index =  91; break;
                case 0x0147: index =  92; break;
                case 0x0149: index =  93; break;
                case 0x004A: index =  94; break;
                case 0x014A: index =  95; break;
                case 0x024A: index =  96; break;
                case 0x034A: index =  97; break;
                case 0x004F: index =  98; break;
                case 0x014F: index =  99; break;
                case 0x024F: index = 100; break;
                case 0x034F: index = 101; break;
                case 0x011A: index = 102; break;
                case 0x021A: index = 103; break;
                case 0x031B: index = 104; break;
                case 0x041B: index = 105; break;
                case 0x051B: index = 106; break;
                case 0x0120: index = 107; break;
                case 0x0121: index = 108; break;
                case 0x0122: index = 109; break;
                case 0x0218: index = 110; break;
                case 0x0318: index = 111; break;
                case 0x0119: index = 112; break;
                case 0x0219: index = 113; break;
                case 0x0319: index = 114; break;
                case 0x0419: index = 115; break;
                case 0x0138: index = 116; break;
                case 0x0238: index = 117; break;
                case 0x0338: index = 118; break;
                case 0x0438: index = 119; break;
                case 0x0139: index = 120; break;
                case 0x0239: index = 121; break;
                case 0x003A: index = 122; break;
                case 0x013B: index = 123; break;
                case 0x023B: index = 124; break;
                case 0x013C: index = 125; break;
                case 0x023C: index = 126; break;
            }

            return (IntegraSNAInstruments)index;
        }

        public static void SetInstrument(this SuperNATURALAcousticToneCommon instance, IntegraSNAInstruments instrument)
        {
            switch((int)instrument)
            {
                case   0: instance.InstVariation = 0x40; instance.InstNumber = 0x00; break;//0x4000
                case   1: instance.InstVariation = 0x40; instance.InstNumber = 0x01; break;//0x4001
                case   2: instance.InstVariation = 0x40; instance.InstNumber = 0x02; break;//0x4002
                case   3: instance.InstVariation = 0x40; instance.InstNumber = 0x03; break;//0x4003
                case   4: instance.InstVariation = 0x40; instance.InstNumber = 0x04; break;//0x4004
                case   5: instance.InstVariation = 0x40; instance.InstNumber = 0x05; break;//0x4005
                case   6: instance.InstVariation = 0x40; instance.InstNumber = 0x06; break;//0x4006
                case   7: instance.InstVariation = 0x40; instance.InstNumber = 0x07; break;//0x4007
                case   8: instance.InstVariation = 0x40; instance.InstNumber = 0x08; break;//0x4008
                case   9: instance.InstVariation = 0x00; instance.InstNumber = 0x04; break;//0x0004
                case  10: instance.InstVariation = 0x01; instance.InstNumber = 0x04; break;//0x0104
                case  11: instance.InstVariation = 0x02; instance.InstNumber = 0x04; break;//0x0204
                case  12: instance.InstVariation = 0x03; instance.InstNumber = 0x04; break;//0x0304
                case  13: instance.InstVariation = 0x06; instance.InstNumber = 0x04; break;//0x0604
                case  14: instance.InstVariation = 0x07; instance.InstNumber = 0x04; break;//0x0704
                case  15: instance.InstVariation = 0x00; instance.InstNumber = 0x07; break;//0x0007
                case  16: instance.InstVariation = 0x01; instance.InstNumber = 0x07; break;//0x0107
                case  17: instance.InstVariation = 0x02; instance.InstNumber = 0x07; break;//0x0207
                case  18: instance.InstVariation = 0x03; instance.InstNumber = 0x07; break;//0x0307
                case  19: instance.InstVariation = 0x04; instance.InstNumber = 0x07; break;//0x0407
                case  20: instance.InstVariation = 0x05; instance.InstNumber = 0x07; break;//0x0507
                case  21: instance.InstVariation = 0x06; instance.InstNumber = 0x07; break;//0x0607
                case  22: instance.InstVariation = 0x07; instance.InstNumber = 0x07; break;//0x0707
                case  23: instance.InstVariation = 0x00; instance.InstNumber = 0x09; break;//0x0009
                case  24: instance.InstVariation = 0x00; instance.InstNumber = 0x0B; break;//0x000B
                case  25: instance.InstVariation = 0x00; instance.InstNumber = 0x0C; break;//0x000C
                case  26: instance.InstVariation = 0x00; instance.InstNumber = 0x0D; break;//0x000D
                case  27: instance.InstVariation = 0x00; instance.InstNumber = 0x0E; break;//0x000E
                case  28: instance.InstVariation = 0x41; instance.InstNumber = 0x00; break;//0x4100
                case  29: instance.InstVariation = 0x00; instance.InstNumber = 0x15; break;//0x0015
                case  30: instance.InstVariation = 0x01; instance.InstNumber = 0x15; break;//0x0115
                case  31: instance.InstVariation = 0x00; instance.InstNumber = 0x16; break;//0x0016
                case  32: instance.InstVariation = 0x00; instance.InstNumber = 0x17; break;//0x0017
                case  33: instance.InstVariation = 0x00; instance.InstNumber = 0x18; break;//0x0018
                case  34: instance.InstVariation = 0x01; instance.InstNumber = 0x18; break;//0x0118
                case  35: instance.InstVariation = 0x00; instance.InstNumber = 0x19; break;//0x0019
                case  36: instance.InstVariation = 0x00; instance.InstNumber = 0x1A; break;//0x001A
                case  37: instance.InstVariation = 0x00; instance.InstNumber = 0x1B; break;//0x001B
                case  38: instance.InstVariation = 0x01; instance.InstNumber = 0x1B; break;//0x011B
                case  39: instance.InstVariation = 0x02; instance.InstNumber = 0x1B; break;//0x021B
                case  40: instance.InstVariation = 0x00; instance.InstNumber = 0x20; break;//0x0020
                case  41: instance.InstVariation = 0x00; instance.InstNumber = 0x21; break;//0x0021
                case  42: instance.InstVariation = 0x00; instance.InstNumber = 0x22; break;//0x0022
                case  43: instance.InstVariation = 0x00; instance.InstNumber = 0x23; break;//0x0023
                case  44: instance.InstVariation = 0x00; instance.InstNumber = 0x28; break;//0x0028
                case  45: instance.InstVariation = 0x01; instance.InstNumber = 0x28; break;//0x0128
                case  46: instance.InstVariation = 0x00; instance.InstNumber = 0x29; break;//0x0029
                case  47: instance.InstVariation = 0x00; instance.InstNumber = 0x2A; break;//0x002A
                case  48: instance.InstVariation = 0x01; instance.InstNumber = 0x2A; break;//0x012A
                case  49: instance.InstVariation = 0x00; instance.InstNumber = 0x2B; break;//0x002B
                case  50: instance.InstVariation = 0x00; instance.InstNumber = 0x2E; break;//0x002E
                case  51: instance.InstVariation = 0x00; instance.InstNumber = 0x2F; break;//0x002F
                case  52: instance.InstVariation = 0x00; instance.InstNumber = 0x30; break;//0x0030
                case  53: instance.InstVariation = 0x01; instance.InstNumber = 0x30; break;//0x0130
                case  54: instance.InstVariation = 0x00; instance.InstNumber = 0x34; break;//0x0034
                case  55: instance.InstVariation = 0x01; instance.InstNumber = 0x34; break;//0x0134
                case  56: instance.InstVariation = 0x00; instance.InstNumber = 0x38; break;//0x0038
                case  57: instance.InstVariation = 0x00; instance.InstNumber = 0x39; break;//0x0039
                case  58: instance.InstVariation = 0x03; instance.InstNumber = 0x39; break;//0x0339
                case  59: instance.InstVariation = 0x00; instance.InstNumber = 0x3B; break;//0x003B
                case  60: instance.InstVariation = 0x00; instance.InstNumber = 0x3C; break;//0x003C
                case  61: instance.InstVariation = 0x01; instance.InstNumber = 0x40; break;//0x0140
                case  62: instance.InstVariation = 0x01; instance.InstNumber = 0x41; break;//0x0141
                case  63: instance.InstVariation = 0x02; instance.InstNumber = 0x42; break;//0x0242
                case  64: instance.InstVariation = 0x01; instance.InstNumber = 0x43; break;//0x0143
                case  65: instance.InstVariation = 0x00; instance.InstNumber = 0x44; break;//0x0044
                case  66: instance.InstVariation = 0x00; instance.InstNumber = 0x46; break;//0x0046
                case  67: instance.InstVariation = 0x00; instance.InstNumber = 0x47; break;//0x0047
                case  68: instance.InstVariation = 0x00; instance.InstNumber = 0x48; break;//0x0048
                case  69: instance.InstVariation = 0x00; instance.InstNumber = 0x49; break;//0x0049
                case  70: instance.InstVariation = 0x00; instance.InstNumber = 0x4B; break;//0x004B
                case  71: instance.InstVariation = 0x00; instance.InstNumber = 0x4D; break;//0x004D
                case  72: instance.InstVariation = 0x00; instance.InstNumber = 0x68; break;//0x0068
                case  73: instance.InstVariation = 0x00; instance.InstNumber = 0x6D; break;//0x006D
                case  74: instance.InstVariation = 0x01; instance.InstNumber = 0x6D; break;//0x016D
                case  75: instance.InstVariation = 0x01; instance.InstNumber = 0x6E; break;//0x016E
                case  76: instance.InstVariation = 0x00; instance.InstNumber = 0x72; break;//0x0072
                case  77: instance.InstVariation = 0x00; instance.InstNumber = 0x0F; break;//0x000F
                case  78: instance.InstVariation = 0x01; instance.InstNumber = 0x2E; break;//0x012E
                case  79: instance.InstVariation = 0x01; instance.InstNumber = 0x4B; break;//0x014B
                case  80: instance.InstVariation = 0x01; instance.InstNumber = 0x4D; break;//0x014D
                case  81: instance.InstVariation = 0x00; instance.InstNumber = 0x6A; break;//0x006A
                case  82: instance.InstVariation = 0x01; instance.InstNumber = 0x6A; break;//0x016A
                case  83: instance.InstVariation = 0x00; instance.InstNumber = 0x6B; break;//0x006B
                case  84: instance.InstVariation = 0x01; instance.InstNumber = 0x6B; break;//0x016B
                case  85: instance.InstVariation = 0x00; instance.InstNumber = 0x6C; break;//0x006C
                case  86: instance.InstVariation = 0x02; instance.InstNumber = 0x6E; break;//0x026E
                case  87: instance.InstVariation = 0x00; instance.InstNumber = 0x40; break;//0x0040
                case  88: instance.InstVariation = 0x00; instance.InstNumber = 0x41; break;//0x0041
                case  89: instance.InstVariation = 0x00; instance.InstNumber = 0x42; break;//0x0042
                case  90: instance.InstVariation = 0x00; instance.InstNumber = 0x43; break;//0x0043
                case  91: instance.InstVariation = 0x00; instance.InstNumber = 0x45; break;//0x0045
                case  92: instance.InstVariation = 0x01; instance.InstNumber = 0x47; break;//0x0147
                case  93: instance.InstVariation = 0x01; instance.InstNumber = 0x49; break;//0x0149
                case  94: instance.InstVariation = 0x00; instance.InstNumber = 0x4A; break;//0x004A
                case  95: instance.InstVariation = 0x01; instance.InstNumber = 0x4A; break;//0x014A
                case  96: instance.InstVariation = 0x02; instance.InstNumber = 0x4A; break;//0x024A
                case  97: instance.InstVariation = 0x03; instance.InstNumber = 0x4A; break;//0x034A
                case  98: instance.InstVariation = 0x00; instance.InstNumber = 0x4F; break;//0x004F
                case  99: instance.InstVariation = 0x01; instance.InstNumber = 0x4F; break;//0x014F
                case 100: instance.InstVariation = 0x02; instance.InstNumber = 0x4F; break;//0x024F
                case 101: instance.InstVariation = 0x03; instance.InstNumber = 0x4F; break;//0x034F
                case 102: instance.InstVariation = 0x01; instance.InstNumber = 0x1A; break;//0x011A
                case 103: instance.InstVariation = 0x02; instance.InstNumber = 0x1A; break;//0x021A
                case 104: instance.InstVariation = 0x03; instance.InstNumber = 0x1B; break;//0x031B
                case 105: instance.InstVariation = 0x04; instance.InstNumber = 0x1B; break;//0x041B
                case 106: instance.InstVariation = 0x05; instance.InstNumber = 0x1B; break;//0x051B
                case 107: instance.InstVariation = 0x01; instance.InstNumber = 0x20; break;//0x0120
                case 108: instance.InstVariation = 0x01; instance.InstNumber = 0x21; break;//0x0121
                case 109: instance.InstVariation = 0x01; instance.InstNumber = 0x22; break;//0x0122
                    // ExSN04
                case 110: instance.InstVariation = 0x02; instance.InstNumber = 0x18; break;//0x0218
                case 111: instance.InstVariation = 0x03; instance.InstNumber = 0x18; break;//0x0318
                case 112: instance.InstVariation = 0x01; instance.InstNumber = 0x19; break;//0x0119
                case 113: instance.InstVariation = 0x02; instance.InstNumber = 0x19; break;//0x0219
                case 114: instance.InstVariation = 0x03; instance.InstNumber = 0x19; break;//0x0319
                case 115: instance.InstVariation = 0x04; instance.InstNumber = 0x19; break;//0x0419
                    // ExSN05
                case 116: instance.InstVariation = 0x01; instance.InstNumber = 0x38; break;//0x0138
                case 117: instance.InstVariation = 0x02; instance.InstNumber = 0x38; break;//0x0238
                case 118: instance.InstVariation = 0x03; instance.InstNumber = 0x38; break;//0x0338
                case 119: instance.InstVariation = 0x04; instance.InstNumber = 0x38; break;//0x0438
                case 120: instance.InstVariation = 0x01; instance.InstNumber = 0x39; break;//0x0139
                case 121: instance.InstVariation = 0x02; instance.InstNumber = 0x39; break;//0x0239
                case 122: instance.InstVariation = 0x00; instance.InstNumber = 0x3A; break;//0x003A
                case 123: instance.InstVariation = 0x01; instance.InstNumber = 0x3B; break;//0x013B
                case 124: instance.InstVariation = 0x02; instance.InstNumber = 0x3B; break;//0x023B
                case 125: instance.InstVariation = 0x01; instance.InstNumber = 0x3C; break;//0x013C
                case 126: instance.InstVariation = 0x02; instance.InstNumber = 0x3C; break;//0x023C
            }
        }

        public static IntegraParameterProvider<byte> NewGetParameterType(this SuperNATURALAcousticToneCommon instance)
        {
            switch(instance.Instrument)
            {
                // INT
                case IntegraSNAInstruments.ConcertGrand:
                case IntegraSNAInstruments.GrandPiano1:
                case IntegraSNAInstruments.GrandPiano2:
                case IntegraSNAInstruments.GrandPiano3:
                case IntegraSNAInstruments.MellowPiano:
                case IntegraSNAInstruments.BrightPiano:
                case IntegraSNAInstruments.UprightPiano:
                case IntegraSNAInstruments.HonkyTonk:
                    return new SNAPiano(instance);

                case IntegraSNAInstruments.ConcertMono:
                    return new SNAPianoMono(instance);

                case IntegraSNAInstruments.PureVintageEP1:
                case IntegraSNAInstruments.PureVintageEP2:
                case IntegraSNAInstruments.PureVintageEP3:
                case IntegraSNAInstruments.PureWurly:
                case IntegraSNAInstruments.OldHammerEP:
                case IntegraSNAInstruments.DynoPiano:
                case IntegraSNAInstruments.ClavCBFlat:
                case IntegraSNAInstruments.ClavCAFlat:
                case IntegraSNAInstruments.ClavCBMedium:
                case IntegraSNAInstruments.ClavCAMedium:
                case IntegraSNAInstruments.ClavCBBrillia:
                case IntegraSNAInstruments.ClavCABrillia:
                case IntegraSNAInstruments.ClavCBCombo:
                case IntegraSNAInstruments.ClavCACombo:
                case IntegraSNAInstruments.FrenchAccordion:
                case IntegraSNAInstruments.ItalianAccordion:
                case IntegraSNAInstruments.Bandoneon:
                    return new SNAKeys(instance);

                case IntegraSNAInstruments.Glockenspiel:
                case IntegraSNAInstruments.Marimba:
                case IntegraSNAInstruments.Xylophone:
                case IntegraSNAInstruments.TubularBells:
                    return new SNABellMallet1(instance);

                case IntegraSNAInstruments.Vibraphone:
                    return new SNABellMallet2(instance);

                case IntegraSNAInstruments.TWOrgan:
                    return new SNAOrgan(instance);

                case IntegraSNAInstruments.Harmonica:
                    return new SNAHarmonica(instance);

                case IntegraSNAInstruments.NylonGuitar:
                case IntegraSNAInstruments.NylonGuitar2:
                case IntegraSNAInstruments.SteelStrGuitar:
                case IntegraSNAInstruments.SteelStrGuitar2:
                case IntegraSNAInstruments.SteelFingGuitar:
                    return new SNAGuitar1(instance);

                case IntegraSNAInstruments.FlamencoGuitar:
                    return new SNAGuitar2(instance);

                case IntegraSNAInstruments.TCGuitarFing:
                case IntegraSNAInstruments.Guitar335Fing:
                case IntegraSNAInstruments.JazzGuitar:
                    return new SNAGuitar3(instance);

                case IntegraSNAInstruments.Steel12ThGtr:
                    return new SNAGuitar4(instance);

                case IntegraSNAInstruments.STGuitarHalf:
                case IntegraSNAInstruments.STGuitarFront:
                case IntegraSNAInstruments.TCGuitarRear:
                case IntegraSNAInstruments.LPGuitarFront:
                case IntegraSNAInstruments.LPGuitarRear:
                case IntegraSNAInstruments.Guitar335Half:
                    return new SNAElectricGuitar(instance);

                // ExSN01
                case IntegraSNAInstruments.Santoor:
                case IntegraSNAInstruments.YangChin:
                    return new SNABellMallet3(instance);

                case IntegraSNAInstruments.Ukelele:
                    return new SNAUkelele(instance);

                case IntegraSNAInstruments.Mandolin:
                    return new SNAMandolin(instance);

                case IntegraSNAInstruments.AcousticBass:
                case IntegraSNAInstruments.AcousticBass2:
                case IntegraSNAInstruments.FretlessBass:
                    return new SNABass1(instance);

                case IntegraSNAInstruments.FingeredBass:
                case IntegraSNAInstruments.FingeredBass2:
                    return new SNABass2(instance);

                case IntegraSNAInstruments.PickedBass:
                case IntegraSNAInstruments.PickedBass2:
                    return new SNABass3(instance);

                case IntegraSNAInstruments.FrenchHorn:
                case IntegraSNAInstruments.FrenchHorn2:
                case IntegraSNAInstruments.Tuba:
                case IntegraSNAInstruments.MuteFrenchHorn:
                    return new SNABrass1(instance);

                case IntegraSNAInstruments.Trumpet:
                case IntegraSNAInstruments.Trumpet2:
                case IntegraSNAInstruments.ClassicalTrumpet:
                case IntegraSNAInstruments.MuteTrumpet:
                case IntegraSNAInstruments.StraightMuteTp:
                case IntegraSNAInstruments.CupMuteTrumpet:
                case IntegraSNAInstruments.Trombone:
                case IntegraSNAInstruments.Trombone2:
                case IntegraSNAInstruments.Tb2CupMute:
                case IntegraSNAInstruments.BassTrombone:
                case IntegraSNAInstruments.FlugelHorn:
                    return new SNABrass2(instance);

                case IntegraSNAInstruments.MariachiTp:
                    return new SNABrass3(instance);

                case IntegraSNAInstruments.Harp:
                    return new SNAHarp(instance);

                case IntegraSNAInstruments.Timpani:
                    return new SNATimpani(instance);

                case IntegraSNAInstruments.SteelDrums:
                    return new SNASteelDrums(instance);

                case IntegraSNAInstruments.Sitar:
                case IntegraSNAInstruments.Sarangi:
                    return new SNASitar(instance);

                case IntegraSNAInstruments.Tsugaru:
                case IntegraSNAInstruments.Sansin:
                    return new SNAShamisen(instance);

                case IntegraSNAInstruments.Koto:
                    return new SNAKoto1(instance);

                case IntegraSNAInstruments.TaishouKoto:
                    return new SNAKoto2(instance);

                case IntegraSNAInstruments.Kalimba:
                    return new SNAKalimba(instance);

                case IntegraSNAInstruments.Violin:
                case IntegraSNAInstruments.Violin2:
                case IntegraSNAInstruments.Viola:
                case IntegraSNAInstruments.Cello:
                case IntegraSNAInstruments.Cello2:
                case IntegraSNAInstruments.Contrabass:
                    return new SNAStrings1(instance);

                case IntegraSNAInstruments.Erhu:
                    return new SNAStrings2(instance);

                case IntegraSNAInstruments.Strings:
                case IntegraSNAInstruments.MarcatoStrings:
                    return new SNAStrings3(instance);

                case IntegraSNAInstruments.Oboe:
                case IntegraSNAInstruments.Bassoon:
                case IntegraSNAInstruments.Clarinet:
                case IntegraSNAInstruments.Piccolo:
                case IntegraSNAInstruments.Flute:
                case IntegraSNAInstruments.Flute2:
                case IntegraSNAInstruments.EnglishHorn:
                case IntegraSNAInstruments.BassClarinet:
                    return new SNAWind1(instance);

                case IntegraSNAInstruments.PanFlute:
                    return new SNAPanFlute(instance);

                case IntegraSNAInstruments.UilleannPipes:
                case IntegraSNAInstruments.BagPipes:
                    return new SNAPipes(instance);

                case IntegraSNAInstruments.Shakuhachi:
                case IntegraSNAInstruments.Ryuteki:
                case IntegraSNAInstruments.OcarinaSopF:
                case IntegraSNAInstruments.OcarinaSopC:
                case IntegraSNAInstruments.OcarinaAlto:
                case IntegraSNAInstruments.OcarinaBass:
                    return new SNAWind2(instance);

                case IntegraSNAInstruments.TinWhistle:
                    return new SNAWhistle(instance);

                case IntegraSNAInstruments.AltoRecorder:
                case IntegraSNAInstruments.BassRecorder:
                case IntegraSNAInstruments.SopranoRecorder:
                case IntegraSNAInstruments.TenorRecorder:
                    return new SNARecorder(instance);

                case IntegraSNAInstruments.SopranoSax:
                case IntegraSNAInstruments.SopranoSax2:
                case IntegraSNAInstruments.AltoSax:
                case IntegraSNAInstruments.AltoSax2:
                case IntegraSNAInstruments.TenorSax:
                case IntegraSNAInstruments.TenorSax2:
                case IntegraSNAInstruments.BaritoneSax:
                case IntegraSNAInstruments.BaritoneSax2:
                    return new SNASax(instance);

                case IntegraSNAInstruments.LondonChoir:
                case IntegraSNAInstruments.BoysChoir:
                    return new SNAChoir(instance);

                default:
                    return null;
            }
        }

        public static IntegraParameterProvider<byte> GetParameterType(this SuperNATURALAcousticToneCommon instance)
        {

            if (instance.InstVariation == 64)
            {
                return new SNAPiano(instance);

                // INT 001   Concert Grand
                // INT 002   Grand Piano1
                // INT 003   Grand Piano2
                // INT 004   Grand Piano3
                // INT 005   Mellow Piano
                // INT 006   Bright Piano
                // INT 007   Upright Piano
                // INT 008   Concert Mono
                // INT 009   Honkey - Tonk

            }

            if (((instance.InstNumber == 4 || instance.InstNumber == 7) && instance.InstVariation != 64) || (instance.InstNumber == 21 || instance.InstNumber == 23))
            {
                return new SNAKeys(instance);

                // INT 010 Pure Vintage EP1
                // INT 011 Pure Vintage EP2
                // INT 012 Pure Wurly
                // INT 013 Pure Vintage EP3
                // INT 014 Old Hammer EP
                // INT 015 Dyno Piano
                // INT 016 Clav CB Flat
                // INT 017 Clav CA Flat
                // INT 018 Clav CB Medium
                // INT 019 Clav CA Medium
                // INT 020 Clav CB Brillia
                // INT 021 Clav CA Brillia
                // INT 022 Clav CB Combo
                // INT 023 Clav CA Combo
                // INT 031 ItalianAccordion
                // INT 030 French Accordion
                // INT 033 Bandoneon

            }

            if (instance.InstNumber >= 40 || instance.InstNumber <= 43)
            {
                // INT 045 Violin
                // INT 046 Violin 2
                // INT 047 Viola
                // INT 048 Cello
                // INT 049 Cello 2
                // INT 050 Contrabass

                return new SNAStrings1(instance);
            }

            if (instance.InstNumber >= 68 && instance.InstNumber <= 73)
            {
                // INT 066 Oboe
                // INT 067 Bassoon
                // INT 068 Clarinet
                // INT 069 Piccolo
                // INT 070 Flute
                // ExSN2 005 English Horn
                // ExSN2 006 Bass Clarinet
                // ExSN2 007 Flute2

                return new SNAWind1(instance);
            }

            if (instance.InstVariation == 65)
            {
                return new SNAOrgan(instance);

                // INT 029 TW Organ

            }

            switch (instance.InstNumber)
            {
                case 9:
                case 12:
                case 13:
                case 14:
                    // INT 024 Glockenspiel
                    // INT 026 Marimba
                    // INT 027 Xylophone
                    // INT 028 Tubular Bells
                    return new SNABellMallet1(instance);

                case 11:
                    // INT 025 Vibraphone
                    return new SNABellMallet2(instance);

                case 15:
                    // ExSN1 001 Santoor
                    return new SNABellMallet3(instance);

                case 22:
                    // INT 032 Harmonica
                    return new SNAHarmonica(instance);

                case 24:

                    // INT 034 Nylon Guitar
                    // ExSN4 002 Nylon Guitar 2
                    if (instance.InstVariation == 0 || instance.InstVariation == 3)
                        return new SNAGuitar1(instance);

                    // INT 035 Flamenco Guitar
                    // ExSN4 001 Ukulele
                    return instance.InstVariation == 1 ? new SNAGuitar2(instance) : new SNAUkelele(instance);

                case 25:
                    if (instance.InstVariation == 2)
                        // ExSN4 004 Mandolin
                        return new SNAMandolin(instance);

                    else if (instance.InstVariation == 3)
                        // ExSN4 005 SteelFing Guitar
                        return new SNAGuitar3(instance);
                    else
                        //INT 036 SteelStr Guitar
                        //ExSN4 003 12th Steel Gtr
                        //ExSN4 006 SteelStr Guitar2
                        return new SNAGuitar1(instance);

                case 26:
                    // INT 037Jazz Guitar
                    // ExSN3 001 TC Guitar w / Fing
                    // ExSN3 002 335Guitar w/ Fing

                    return instance.InstVariation == 0 ? new SNAElectricGuitar(instance) : new SNAGuitar3(instance);

                case 27:
                    // INT 038 ST Guitar Half
                    // INT 039 ST Guitar Front
                    // INT 040 TC Guitar Rear
                    // ExSN3 003 LP Guitar Rear
                    // ExSN3 004 LP Guitar Front
                    // ExSN3 005 335 Guitar Half

                    return new SNAElectricGuitar(instance);

                case 32:
                case 35:
                    // INT 041 Acoustic Bass
                    // INT 044 Fretless Bass
                    // ExSN3 006 Acoustic Bass 2

                    return new SNABass1(instance);

                case 33:
                    //INT 042 Fingered Bass
                    //ExSN3 007 Fingered Bass 2

                    return new SNABass2(instance);

                case 34:
                    // INT 043 Picked Bass
                    // ExSN3 008 Picked Bass 2

                    return new SNABass3(instance);

                case 46:
                    // INT 051 Harp
                    // ExSN1 002 Yang Chin
                    return instance.InstVariation == 0 ? new SNAHarp(instance) : new SNABellMallet3(instance);

                case 47:
                    // INT 052 Timpani
                    return new SNATimpani(instance);

                case 48:
                    // INT 053 Strings
                    // INT 054 Marcato Strings

                    return new SNAStrings3(instance);

                case 52:
                    // INT 055 London Choir
                    // INT 056 Boys Choir

                    return new SNAChoir(instance);

                case 56:
                case 57:
                case 59:
                    // INT 057 Trumpet
                    // INT 058 Trombone
                    // INT 059 Tb2 CupMute
                    // ExSN5 005 Trombone 2
                    // ExSN5 001 Classical Trumpet
                    // ExSN5 006 Bass Trombone
                    // ExSN5 002 Frugal Horn
                    // ExSN5 003 Trumpet 2
                    // ExSN5 004 Mariachi Tp

                    return new SNABrass2(instance);

                case 58:
                case 60:
                    // INT 061 French Horn
                    // ExSN5 007 Tuba
                    // ExSN5 010 French Horn 2
                    // ExSN5 011 Mute French Horn

                    return new SNABrass1(instance);

                case 64:
                case 65:
                case 66:
                case 67:
                    // INT 062 Soprano Sax 2
                    // INT 063 Alto Sax 2
                    // INT 064 Tenor Sax 2
                    // INT 065 Baritone Sax 2
                    // ExSN2 001 Soprano Sax
                    // ExSN2 002 Alto Sax
                    // ExSN2 003 Tenor Sax
                    // ExSN2 004 Baritone Sax

                    return new SNASax(instance);

                case 74:

                    // ExSN2 008 Soprano Recorder
                    // ExSN2 009 Alto Recorder
                    // ExSN2 010 Tenor Recorder
                    // ExSN2 011 Bass Recorder

                    return new SNARecorder(instance);

                case 75:
                    // INT 071 Panflute
                    // ExSN1 003 Tin Whistle

                    return instance.InstVariation == 0 ? new SNAPanFlute(instance) : new SNAWhistle(instance);

                case 77:
                case 79:
                    // INT 072 Shakuhachi
                    // ExSN1 004 Ryuteki
                    // ExSN2 012 Ocarina SopC
                    // ExSN2 013 Ocarina SopF
                    // ExSN2 014 Ocarina Alto
                    // ExSN2 015 Ocarina Bass

                    return new SNAWind2(instance);

                case 104:
                    // INT 073 Sitar
                    return new SNASitar(instance);

                case 106:
                    // ExSN1 005 Tsugaru
                    // ExSN1 006 Sansin

                    return new SNAShamisen(instance);

                case 107:
                    // ExSN1 007 Koto
                    // ExSN1 008 Taishou Koto

                    return instance.InstVariation == 0 ? new SNAKoto1(instance) : new SNAKoto2(instance);

                case 108:

                    // ExSN1 009 Kalimba
                    return new SNAKalimba(instance);

                case 109:
                    // INT 074 Uilleann Pipes
                    // INT 075 Bag Pipes

                    return new SNAPipes(instance);

                case 110:

                    // INT 076 Erhu
                    // ExSN1 010 Sarangi
                    return instance.InstVariation == 1 ? new SNAStrings2(instance) : new SNASitar(instance);

                case 114:
                    // INT 077 Steel Drums
                    return new SNASteelDrums(instance);

                default:

                    return null;
            }
        }
    }
}
