using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models.MFX
{
    /// <summary>
    /// Provides validation for the <see cref="StudioSetCommonReverb"/> GM2 type parameters.
    /// </summary>
    /// <remarks>
    /// 00: Character <br/>
    /// 03: Time <br/>
    /// </remarks>
    public class CommonReverbGM2 : IToneMFXModel
    {
        public double Get(int index, double value)
        {
            switch (index)
            {
                default:
                    return value;
            }
        }

        public int Set(int index, double value)
        {
            switch (index)
            {
                default:
                    return (int)value;
            }
        }
    }
}
