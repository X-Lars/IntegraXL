using IntegraXL.Core;

namespace IntegraXL.File
{

    public struct TemporaryToneFile
    {
        public string Name;
        public byte[] Model;
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

            for (int i = 0; i < 16; i++)
            {
                Midis[i] = new byte[1];
            }

            Parts = new byte[IntegraConstants.PART_COUNT][];

            for (int i = 0; i < 16; i++)
            {
                Parts[i] = new byte[77];
            }

            PartEQs = new byte[IntegraConstants.PART_COUNT][];

            for (int i = 0; i < 16; i++)
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
