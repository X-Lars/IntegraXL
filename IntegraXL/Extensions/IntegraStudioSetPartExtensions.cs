using IntegraXL.Core;
using IntegraXL.Models;
using IntegraXL.Templates;
using System.Diagnostics;
using System.Reflection;

namespace IntegraXL.Extensions
{
    public static class IntegraStudioSetPartExtensions
    {
        private static List<ScaleTuneTemplate> _Templates = new();

        public static void SetScaleTune(this StudioSetPart instance)
        {
            if (instance.ScaleTuneType == IntegraScaleTuneTypes.Custom)
                return;

            ScaleTuneTemplate template = GetTemplate(instance.ScaleTuneType, instance.ScaleTuneKey);

            instance.ScaleTuneC      = template.Values[0];
            instance.ScaleTuneCSharp = template.Values[1];
            instance.ScaleTuneD      = template.Values[2];
            instance.ScaleTuneDSharp = template.Values[3];
            instance.ScaleTuneE      = template.Values[4];
            instance.ScaleTuneF      = template.Values[5];
            instance.ScaleTuneFSharp = template.Values[6];
            instance.ScaleTuneG      = template.Values[7];
            instance.ScaleTuneGSharp = template.Values[8];
            instance.ScaleTuneA      = template.Values[9];
            instance.ScaleTuneASharp = template.Values[10];
            instance.ScaleTuneB      = template.Values[11];
        }

        internal static ScaleTuneTemplate GetTemplate(this StudioSetPart instance)
        {
            if (_Templates.Count == 0)
                LoadScaleTuneTemplates();

            // Type: Custom = 0, Equal = 1, ..., ..., Arabic = 8
            // Key : C = 0, C# = 1, ..., ... B = 11

            return _Templates.First(x => x.Type == instance.ScaleTuneType && x.Key == instance.ScaleTuneKey);
        }

        internal static ScaleTuneTemplate GetTemplate(IntegraScaleTuneTypes type, IntegraScaleTuneKeys key)
        {
            if (_Templates.Count == 0)
                LoadScaleTuneTemplates();

            // Type: Custom = 0, Equal = 1, ..., ..., Arabic = 8
            // Key : C = 0, C# = 1, ..., ... B = 11

            return _Templates.First(x => x.Type == type && x.Key == key);
        }

        private static void LoadScaleTuneTemplates()
        {
            Debug.Print($"[{nameof(IntegraStudioSetPartExtensions)}.{nameof(LoadScaleTuneTemplates)}]");

            _Templates.Clear();

            Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("IntegraXL.Resources.ScaleTunePresets.csv");

            Debug.Assert(stream != null);

            using (StreamReader reader = new(stream))
            {
                while (!reader.EndOfStream)
                {
                    string[] values = reader.ReadLine()?.Split(IntegraConstants.CSV_DELIMITER) ??
                        throw new IntegraException($"[{nameof(IntegraStudioSetPartExtensions)}.{nameof(LoadScaleTuneTemplates)}()]\n" +
                                                   $"Unexpected end of resource.");

                    Debug.Assert(values.Length == 14);

                    ScaleTuneTemplate template = new();

                    template.Type = (IntegraScaleTuneTypes)Convert.ToByte(values[0]);
                    template.Key  = (IntegraScaleTuneKeys) Convert.ToByte(values[1]);

                    for (int i = 0; i < 12; i++)
                    {
                        template.Values[i] = Convert.ToByte(values[i + 2]);
                    }

                    _Templates.Add(template);
                }
            }
        }
    }
}
