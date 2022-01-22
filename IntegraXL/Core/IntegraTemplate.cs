using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{
    public abstract class IntegraTemplate { }

    /// <summary>
    /// Base class for all immutable INTEGRA-7 data structures like studio sets, tones and waveforms.
    /// </summary>
    /// <typeparam name="T">Type specifying the data structure.</typeparam>
    public abstract class IntegraTemplate<T> : IntegraTemplate where T : IntegraTemplate<T>
    {
        protected IntegraTemplate(int id, byte[] data)
        {
            ID = id;
        }

        public int ID { get; }
    }
}
