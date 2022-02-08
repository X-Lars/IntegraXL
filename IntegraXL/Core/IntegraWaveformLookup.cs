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
            List<WaveformTemplate> templates = new();
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream("IntegraXL.Resources.WaveForms.csv");
            //StreamReader reader = new (myStream);
            using (StreamReader reader = new (myStream))
            {
                while (!reader.EndOfStream)
                {
                    WaveformTemplate template = Read(reader.ReadLine());

                    if (template.Type != type)
                        continue;

                    if (template.Bank != bank)
                        continue;

                    templates.Add(template);
                }
            }

            return templates;
            //return reader.ReadToEnd((@"IntegraXL.Resources.WaveForms.csv").Select(x => Read(x)).Where(x => x.Type == type).Where(x => x.Bank == bank).ToList();
        }

        public static WaveformTemplate Read(string line)
        {
            string[] values = line.Split('|');

            WaveformTemplate template = new WaveformTemplate();

            template.Type = (IntegraWaveFormTypes)Convert.ToInt32(values[0]);
            template.Bank = (IntegraWaveFormBanks)Convert.ToInt32(values[1]);
            template.ID   = Convert.ToInt32(values[2]);
            template.Name = values[3];

            return template;
        }
    }
}
