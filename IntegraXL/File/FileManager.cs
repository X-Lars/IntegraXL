
namespace IntegraXL.File
{
    public static class FileManager
    {
        public static StudioSetFile Create()
        {
            return new StudioSetFile();
        }


        public static StudioSetFile Load(byte[] data)
        {
            StudioSetFile file = new();

            MemoryStream stream = new(data);
            BinaryReader reader = new(stream);

            file.Common = reader.ReadBytes(file.Common.Length);
            file.CommonChorus = reader.ReadBytes(file.CommonChorus.Length);
            file.CommonReverb = reader.ReadBytes(file.CommonReverb.Length);
            file.MotionalSurround = reader.ReadBytes(file.MotionalSurround.Length);
            file.MasterEQ = reader.ReadBytes(file.MasterEQ.Length);

            for (int i = 0; i < 16; i++)
            {
                file.Midis[i] = reader.ReadBytes(file.Midis[i].Length);
            }

            for (int i = 0; i < 16; i++)
            {
                file.Parts[i] = reader.ReadBytes(file.Parts[i].Length);
            }

            for (int i = 0; i < 16; i++)
            {
                file.PartEQs[i] = reader.ReadBytes(file.PartEQs[i].Length);
            }

            return file;
        }


        public static byte[] Save(StudioSetFile file)
        {
            MemoryStream stream = new();
            BinaryWriter writer = new(stream);

            writer.Write(file.Common);
            writer.Write(file.CommonChorus);
            writer.Write(file.CommonReverb);
            writer.Write(file.MotionalSurround);
            writer.Write(file.MasterEQ);

            for (int i = 0; i < 16; i++)
            {
                writer.Write(file.Midis[i]);
            }

            for (int i = 0; i < 16; i++)
            {
                writer.Write(file.Parts[i]);
            }

            for (int i = 0; i < 16; i++)
            {
                writer.Write(file.PartEQs[i]);
            }

            return stream.ToArray();
        }
    }
}
