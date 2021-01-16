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
        //StudioSetMidi[] _StudioSetMidi = new StudioSetMidi[16];
        StudioSetPart[] _StudioSetParts = new StudioSetPart[16];

        StudioSetPart _Part = new StudioSetPart(IntegraParts.Part01);
        public StudioSet()
        {

            for (int i = 0; i < 16; i++)
            {
                _StudioSetParts[i] = new StudioSetPart((IntegraParts)i);
            }

            //for (int i = 0; i < 16; i++)
            //{
            //    _StudioSetMidi[i] = new StudioSetMidi((IntegraParts)i);
            //}
        }


        //public StudioSetMidi[] Midi
        //{
        //    get { return _StudioSetMidi; }
        //}

        public StudioSetPart[] Parts
        {
            get { return _StudioSetParts; }
        }

        //public StudioSetPart Part
        //{
        //    get { return _Part; }
        //    set
        //    {
        //        if(_Part != value)
        //        {
        //            _Part = value;
        //        }
        //    }
        //}
    }
}
