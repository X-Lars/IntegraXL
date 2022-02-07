using IntegraXL.Core;
using IntegraXL.Models;
using IntegraXL.Models.Parameters;

namespace IntegraXL.Extensions
{
    public static class IntegraSNAParameterExtensions
    {
        public static IntegraParameterMapper<byte> GetParameterType(this SuperNATURALAcousticToneCommon instance)
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
                return new SNANoise(instance);

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

            if(instance.InstNumber >= 40 || instance.InstNumber <= 43)
            {
                // INT 045 Violin
                // INT 046 Violin 2
                // INT 047 Viola
                // INT 048 Cello
                // INT 049 Cello 2
                // INT 050 Contrabass

                return new SNAStrings1(instance);
            }

            if(instance.InstNumber >= 68 && instance.InstNumber <=73)
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

            switch(instance.InstNumber)
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
                    return instance.InstVariation == 1 ? new SNAGuitar2(instance) : new SNAUkele(instance);

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

                    return instance.InstVariation == 0 ? new SNAElectricGuitar2(instance) : new SNAGuitar3(instance);

                case 27:
                    // INT 038 ST Guitar Half
                    // INT 039 ST Guitar Front
                    // INT 040 TC Guitar Rear
                    // ExSN3 003 LP Guitar Rear
                    // ExSN3 004 LP Guitar Front
                    // ExSN3 005 335 Guitar Half

                    return new SNAElectricGuitar1(instance);

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

                    return new SNAWind5(instance);

                case 75:
                    // INT 071 Panflute
                    // ExSN1 003 Tin Whistle

                    return instance.InstVariation == 0 ? new SNAWind2(instance) : new SNAWind4(instance);

                case 77:
                case 79:
                    // INT 072 Shakuhachi
                    // ExSN1 004 Ryuteki
                    // ExSN2 012 Ocarina SopC
                    // ExSN2 013 Ocarina SopF
                    // ExSN2 014 Ocarina Alto
                    // ExSN2 015 Ocarina Bass

                    return new SNAWind3(instance);

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

                    return instance.InstVariation == 0 ? new SNAKoto(instance) : new SNATaishou(instance);

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
