using IntegraXL.Core;

namespace IntegraXL.Templates
{
    public sealed class WaveformTemplate
    {
        internal WaveformTemplate()
        {

        }

        internal WaveformTemplate(IntegraWaveFormTypes type, IntegraWaveFormBanks bank, int id, string name) 
        {
            Type = type;
            Bank = bank;
            ID = id;
            Name = name;
        }

        public IntegraWaveFormTypes Type { get; internal set; }
        public IntegraWaveFormBanks Bank { get; internal set; }
        public int ID { get; internal set; }
        public string Name { get; internal set; } = "---";
    }
}
