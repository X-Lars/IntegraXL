using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{
    public class IntegraModelQueue : Queue<IntegraModel> 
    {
        public IntegraModelQueue() : base() { }
        public IntegraModelQueue(IEnumerable<IntegraModel> models) : base(models) { }
    }
}
