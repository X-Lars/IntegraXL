using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{

    public static class IntegraConstants
    {
        /// <summary>
        /// The size of an INTEGRA-7 addres in <see cref="byte"/>s.
        /// </summary>
        public const int ADDRESS_SIZE = 4;

        /// <summary>
        /// The maximum MIDI <see cref="byte"/> value.
        /// </summary>
        public const byte MIDI_MAX_VALUE = 0x7F;

        /// <summary>
        /// The size of an INTEGRA-7 system exclusive header in <see cref="byte"/>s.
        /// </summary>
        public const int SYSTEM_EXCLUSIVE_HEADER_SIZE = 7;
    }
}
