using IntegraXL.Templates;
using System.Reflection;

namespace IntegraXL.Core
{
    internal class IntegraWaveformLookup
    {
        public static WaveformTemplate Template(IntegraWaveFormTypes type, IntegraWaveFormBanks bank, int id)
        {
            List<WaveformTemplate> templates = Templates(type, bank);

            return templates.Where(x => x.ID == id).FirstOrDefault();
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
                    WaveformTemplate template = LoadWaveform(reader.ReadLine());

                    if (template.Type != type)
                        continue;

                    if (template.Bank != bank)
                        continue;

                    waveforms.Add(template);
                }
            }

            return waveforms;
        }

        public static WaveformTemplate LoadWaveform(string line)
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
