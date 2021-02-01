using Integra.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class SuperNATURALAcousticToneMFX : IntegraBase<SuperNATURALAcousticToneMFX>
    {
        [Offset(0x0000)] private IntegraMFXTypes _MFXType;
        [Offset(0x0011)] private int[] _Parameters = new int[32];

        public SuperNATURALAcousticToneMFX(IntegraAddress address) : base(address + 0x00000200, 0x00000111)
        {
            Name = "SuperNATURAL Acoustic Tone MFX";
        }

        [Offset(0x0000)]
        public IntegraMFXTypes Type
        {
            get { return _MFXType; }
            set
            {
                _MFXType = value;
                NotifyPropertyChanged();
            }
        }

        public int this[int index]
        {
            get { return _Parameters[index]; }
        }
    }
}
