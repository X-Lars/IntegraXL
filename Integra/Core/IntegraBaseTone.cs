using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    public abstract class IntegraBaseTone : IntegraBase<IntegraBaseTone>
    {
        public IntegraBaseTone(IntegraToneTypes type) : base(0x19000000)
        {
            // Offset the base address with the selected part
            // 0x19, 0x19, 0x19, 0x19, 0x20, ...
            Address += new byte[] { (byte)((int)Device.Instance.SelectedPart / 4), 0x00, 0x00, 0x00 };

            // 0x00, 0x20, 0x40, 0x60, 0x00, ...
            Address += new byte[] { 0x00, (byte)((int)Device.Instance.SelectedPart % 4 * 0x20), 0x00, 0x00 };


            Address += (uint)type;

            Console.WriteLine(Address);
        }
    }
}
