using MidiXL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    public class IntegraException : Exception
    {
        public IntegraException(string message) : base(message) { }

        public IntegraException(string message, MidiDeviceException innerException) : base(message, innerException) { }
        
    }
}
