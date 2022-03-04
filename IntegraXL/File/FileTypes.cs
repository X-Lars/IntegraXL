using IntegraXL.Core;

namespace IntegraXL.File
{
    public static class FileTypes
    {
        public static readonly string STUDIO_SET_FILE_EXT = "i7s";
        public static readonly string TEMPORARY_TONE_FILE_EXT = "i7t";

        public struct TemporaryToneFile
        {
            public TemporaryToneFile()
            {
                ToneType = 0;

                PCMSynthToneCommon = new byte[80];
                PMT = new byte[41];
                PCMSynthTonePartials = new byte[IntegraConstants.PCM_PARTIAL_COUNT][];

                for (int i = 0; i < IntegraConstants.PCM_PARTIAL_COUNT; i++)
                {
                    PCMSynthTonePartials[i] = new byte[154];
                }

                PCMSynthToneCommon2 = new byte[60];

                SuperNATURALSynthToneCommon = new byte[64];
                SuperNATURALSynthTonePartials = new byte[IntegraConstants.SNS_PARTIAL_COUNT][];

                for (int i = 0; i < IntegraConstants.SNS_PARTIAL_COUNT; i++)
                {
                    SuperNATURALSynthTonePartials[i] = new byte[61];
                }

                SuperNATURALAcousticToneCommon = new byte[70];

                SuperNATURALDrumKitCommon = new byte[20];
                SuperNATURALDrumKitCommonCompEQ = new byte[84];
                SuperNATURALDrumKitNotes = new byte[IntegraConstants.SND_NOTE_COUNT][];

                for (int i = 0; i < IntegraConstants.SND_NOTE_COUNT; i++)
                {
                    SuperNATURALDrumKitNotes[i] = new byte[19];
                }

                PCMDrumKitCommon = new byte[18];
                PCMDrumKitCommonCompEQ = new byte[84];
                PCMDrumKitNotes = new byte[IntegraConstants.PCM_NOTE_COUNT][];

                for (int i = 0; i < IntegraConstants.PCM_NOTE_COUNT; i++)
                {
                    PCMDrumKitNotes[i] = new byte[194];
                }

                PCMDrumKitCommon2 = new byte[50];

                MFX = new byte[145];
            }

            public byte ToneType;

            public byte[] PCMSynthToneCommon;
            public byte[] PMT;
            public byte[][] PCMSynthTonePartials;
            public byte[] PCMSynthToneCommon2;

            public byte[] SuperNATURALSynthToneCommon;
            public byte[][] SuperNATURALSynthTonePartials;

            public byte[] SuperNATURALAcousticToneCommon;

            public byte[] SuperNATURALDrumKitCommon;
            public byte[] SuperNATURALDrumKitCommonCompEQ;
            public byte[][] SuperNATURALDrumKitNotes;

            public byte[] PCMDrumKitCommon;
            public byte[] PCMDrumKitCommonCompEQ;
            public byte[][] PCMDrumKitNotes;
            public byte[] PCMDrumKitCommon2;

            public byte[] MFX;
        }

        public struct StudioSetFile
        {
            public StudioSetFile()
            {
                Common = new byte[84];
                CommonChorus = new byte[84];
                CommonReverb = new byte[99];
                MotionalSurround = new byte[16];
                MasterEQ = new byte[7];

                Midis = new byte[IntegraConstants.PART_COUNT][];

                for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
                {
                    Midis[i] = new byte[1];
                }

                Parts = new byte[IntegraConstants.PART_COUNT][];

                for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
                {
                    Parts[i] = new byte[77];
                }

                PartEQs = new byte[IntegraConstants.PART_COUNT][];

                for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
                {
                    PartEQs[i] = new byte[8];
                }
            }

            public byte[] Common;
            public byte[] CommonChorus;
            public byte[] CommonReverb;
            public byte[] MotionalSurround;
            public byte[] MasterEQ;
            public byte[][] Midis;
            public byte[][] Parts;
            public byte[][] PartEQs;
        }

    }
}
