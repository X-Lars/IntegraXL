using Integra.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class StudioSet
    {
        StudioSetMidi[] _StudioSetMidi = new StudioSetMidi[16];
        StudioSetPart[] _StudioSetPart = new StudioSetPart[16];

        public StudioSet()
        {
            for (int i = 0; i < 16; i++)
            {
                _StudioSetPart[i] = new StudioSetPart((IntegraParts)i);
            }

            for (int i = 0; i < 16; i++)
            {
                _StudioSetMidi[i] = new StudioSetMidi((IntegraParts)i);
            }
        }

        public StudioSetMidi[] Midi
        {
            get { return _StudioSetMidi; }
        }

        public StudioSetPart[] Part
        {
            get { return _StudioSetPart; }
        }
    }
}
