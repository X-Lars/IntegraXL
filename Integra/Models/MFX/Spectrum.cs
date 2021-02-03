using Integra.Core;
using Integra.Common;
using Integra.Core.Interfaces;

namespace Integra.Models.MFX
{
    /// <summary>
    /// Provides validation for the <see cref="Spectrum"/> MFX parameters.
    /// </summary>
    /// <remarks>
    /// 00: Band 1 (250  Hz) <br/>
    /// 01: Band 2 (500  Hz) <br/>
    /// 02: Band 3 (1000 Hz) <br/>
    /// 03: Band 4 (1250 Hz) <br/>
    /// 04: Band 5 (2000 Hz) <br/>
    /// 05: Band 6 (3150 Hz) <br/>
    /// 06: Band 7 (4000 Hz) <br/>
    /// 07: Band 8 (8000 Hz) <br/>
    /// 08: Q <br/>
    /// 09: Level <br/>
    /// </remarks>
    public class Spectrum : IToneMFXModel
    {
        public double Get(int index, double value)
        {
            switch (index)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    return value - 15;
                    
                default:
                    return value;
            }
        }

        public int Set(int index, double value)
        {
            switch (index)
            {
                case 0: 
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    return value.InvalidateRange(-15, 15) + 15;

                case 8:
                    return value.InvalidateRange<IntegraMidQs>();

                case 9:
                    return value.InvalidateRange(0, 127);

                default:
                    return (int)value;
            }
        }
    }
}
