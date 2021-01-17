using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models.MFX
{
    public class Thru : IToneMFXModel
    {
        public int GetValue(int index, int value)
        {
            return value;
        }

        public int SetValue(int index, int value)
        {
            return value;
        }
    }
}
