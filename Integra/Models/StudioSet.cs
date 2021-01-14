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

        public StudioSet()
        {
            for (int i = 0; i < 16; i++)
            {
                StudioSetMidi midi = new StudioSetMidi((IntegraParts)i);
                _StudioSetMidi[i] = midi;
            }
        }

        public StudioSetMidi[] Midi
        {
            get { return _StudioSetMidi; }
        }
    }
}
