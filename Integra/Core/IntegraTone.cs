using Integra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    public sealed class IntegraTone : IntegraDataTemplate<IntegraStudioSet>
    {
        public IntegraTone()
        {

        }

        internal IntegraTone(uint id, byte[] data)
        {
            ID = id;
            MSB = data[0];
            LSB = data[1];
            PC = data[2];
            Category = (IntegraToneCategories)data[3];
            Name = Encoding.ASCII.GetString(data, 5, 16);
        }

        public IntegraTone(byte msb, byte lsb, byte pc)
        {
            MSB = msb;
            LSB = lsb;
            PC = pc;
        }

        public uint ID { get; set; }
        public string Name { get; set; }
        public byte MSB { get; set; }
        public byte LSB { get; set; }
        public byte PC { get; set; }
        public IntegraToneCategories Category { get; set; }
    }
}
