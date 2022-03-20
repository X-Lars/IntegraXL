using IntegraXL.Core;
using System.Diagnostics;

namespace IntegraXL.Templates
{
    internal class ScaleTuneTemplate
    {
        internal ScaleTuneTemplate() 
        { 
           
        }

        internal ScaleTuneTemplate(IntegraScaleTuneTypes type, IntegraScaleTuneKeys key, byte[] values)
        {
            Debug.Assert(values.Length == 12);

            Type   = type;
            Key    = key;
            Values = values;
        }

        public IntegraScaleTuneTypes Type { get; internal set; } = IntegraScaleTuneTypes.Custom;
        public IntegraScaleTuneKeys Key { get; internal set; }   = IntegraScaleTuneKeys.C;

        public byte[] Values { get; } = new byte[12] { 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64 };
    }
}
