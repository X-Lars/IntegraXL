using Integra.Core;
using MidiXL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class TemporaryTone : IntegraBase<TemporaryTone>
    {
        private IntegraToneTypes _Type;

        public TemporaryTone(IntegraParts part, IntegraToneTypes type) : base(0x19000000) 
        {
            // Offset the base address with the selected part
            // 0x19, 0x19, 0x19, 0x19, 0x20, ...
            Address += new byte[] { (byte)((int)part / 4), 0x00, 0x00, 0x00 };

            // 0x00, 0x20, 0x40, 0x60, 0x00, ...
            Address += new byte[] { 0x00, (byte)((int)part % 4 * 0x20), 0x00, 0x00 };


            Address += (uint)type;

            Type = type;
            Console.WriteLine($"[{nameof(TemporaryTone)}] {Address}");
        }

        public IntegraToneTypes Type
        {
            get { return _Type; }
            private set
            {
                if(_Type != value)
                {
                    _Type = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
