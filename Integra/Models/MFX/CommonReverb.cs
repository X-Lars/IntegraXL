using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models.MFX
{
    /// <summary>
    /// Provides validation for the <see cref="StudioSetCommonReverb"/> room type parameters.
    /// </summary>
    /// <remarks>
    /// 01: Pre Delay <br/>
    /// 02: Time <br/>
    /// 03: Density <br/>
    /// 04: Diffusion <br/>
    /// 05: LF Damp <br/>
    /// 06: HF Damp <br/>
    /// 07: Spread <br/>
    /// 08: Tone <br/>
    /// </remarks>
    public class CommonReverb : IToneMFXModel
    {
        public double Get(int index, double value)
        {
            switch(index)
            {
                case 2:
                    return value * 0.1;

                default:
                    return value;
            }
        }

        public int Set(int index, double value)
        {
            switch (index)
            {
                case 2:
                    return (int)Math.Round(value / 0.1);

                default:
                    return (int)value;
            }
        }
    }
}
