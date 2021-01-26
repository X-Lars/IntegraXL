using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    ///
    public sealed class IntegraStudioSet : IntegraDataTemplate<IntegraStudioSet>
    {
        private uint _ID;
        private string _Name;
        private byte _MSB;
        private byte _LSB;
        private byte _PC;

        private IntegraStudioSet(uint id, byte[] data) : base(id, data)
        {
           

            ID = id;
            Name = Encoding.ASCII.GetString(data, 5, 16);
            MSB = data[0];
            LSB = data[1];
            PC = data[2];
           
        }

        public bool IsEditable
        {
            get { return PC > 0x0F; }
        }

        public uint ID
        {
            get { return _ID; }
            set { _ID = value; NotifyPropertyChanged(); }
        }


        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(); }
        }


        public byte MSB
        {
            get { return _MSB; }
            set { _MSB = value; NotifyPropertyChanged(); }
        }


        public byte LSB
        {
            get { return _LSB; }
            set { _LSB = value; NotifyPropertyChanged(); }
        }

        public byte PC
        {
            get { return _PC; }
            set { _PC = value; NotifyPropertyChanged(); }
        }
       
    }
}
