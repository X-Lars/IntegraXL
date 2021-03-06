using Integra.Core.Interfaces;
using Integra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    public class IntegraTone : IntegraDataTemplate<IntegraTone>, IIntegraAddressable, INotifyPropertyChanged
    {
        public IntegraTone()
        {

        }

        private IntegraTone(int id, byte[] data)
        {
            ID = id;
            MSB = data[0];
            LSB = data[1];
            PC = data[2];
            Category = (IntegraToneCategories)data[3];
            Name = Encoding.ASCII.GetString(data, 5, 12);
        }

        public IntegraTone(byte msb, byte lsb, byte pc)
        {
            MSB = msb;
            LSB = lsb;
            PC = pc;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public byte MSB { get; set; }
        public byte LSB { get; set; }
        public byte PC { get; set; }
        public IntegraToneCategories Category { get; set; }

        public virtual IntegraToneBanks ToneBank
        {
            get { return this.ToneBank(); }
        }

        public virtual IntegraExpansions Expansion
        {
            get { return this.GetExpansion(); }
        }
    }
}
