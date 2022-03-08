using IntegraXL.Templates;
using System.Reflection;

namespace IntegraXL.Core
{
    internal class IntegraWaveformLookup
    {

        public static WaveformTemplate Template(IntegraWaveFormTypes type, IntegraWaveFormBanks bank, int id)
        {
            // Type: PCM = 0, SNA = 1, SND = 2, SNS = 3
            // Bank: INT = 0, SRX01 = 1 .. SRX12 = 12, ExSN01 = 21 .. ExSN06 = 26

            return Templates(type, bank).FirstOrDefault(x => x.ID == id, new WaveformTemplate(type, bank, id, "---"));
        }

        public static List<WaveformTemplate> Templates(IntegraWaveFormTypes type, IntegraWaveFormBanks bank)
        {
            List<WaveformTemplate> waveforms = new();

            Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("IntegraXL.Resources.WaveForms.csv");

            if(stream == null)
                throw new IntegraException($"[{nameof(IntegraWaveformLookup)}.{nameof(Templates)}]\nUnable to load the waveforms resource.");


            using (StreamReader reader = new (stream))
            {
                while (!reader.EndOfStream)
                {
                    WaveformTemplate template = LoadWaveforms(reader.ReadLine());

                    if (template.Type != type)
                        continue;

                    if (template.Bank != bank)
                        continue;

                    waveforms.Add(template);
                }
            }

            return waveforms;
        }

        public static WaveformTemplate LoadWaveforms(string line)
        {
            string[] values = line.Split('|');

            WaveformTemplate template = new();

            template.Type = (IntegraWaveFormTypes)Convert.ToInt32(values[0]);
            template.Bank = (IntegraWaveFormBanks)Convert.ToInt32(values[1]);
            template.ID   = Convert.ToInt32(values[2]);
            template.Name = values[3];

            return template;
        }
    }
}
