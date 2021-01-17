using Integra.Core;
using Integra.Common;
using Integra.Core.Interfaces;

namespace Integra.Models.MFX
{
    /// <summary>
    /// Provides validation for the <see cref="Equalizer"/> MFX parameters.
    /// </summary>
    /// <remarks>
    /// 00: Low Freq <br/>
    /// 01: Low Gain <br/>
    /// 02: Mid1 Freq <br/>
    /// 03: Mid1 Gain <br/>
    /// 04: Mid1 Q <br/>
    /// 05: Mid2 Freq <br/>
    /// 06: Mid2 Gain <br/>
    /// 07: Mid2 Q <br/>
    /// 08: High Freq <br/>
    /// 09: High Gain <br/>
    /// 10: Level <br/>
    /// </remarks>
    public class Equalizer : IToneMFXModel
    {
        public int Get(int index, int value)
        {
            switch (index)
            {
                case 1:
                case 3:
                case 6:
                case 9:
                    return value - 15;
                    
                default:
                    return value;
            }
        }

        public int Set(int index, int value)
        {
            switch (index)
            {
                case 0: 
                    return value.InvalidateRange<IntegraLowFrequencies>();

                case 1: 
                case 3:
                case 6:
                case 9:
                    return value.InvalidateRange(-15, 15) + 15;

                case 2:
                case 5: 
                    return value.InvalidateRange<IntegraMidFrequencies>();

                case 4:
                case 7:
                    return value.InvalidateRange<IntegraMidQs>();

                case 8:
                    return value.InvalidateRange<IntegraHighFrequencies>();

                case 10:
                    return value.InvalidateRange(0, 127);

                default:
                    return value;
            }
        }
    }
}
