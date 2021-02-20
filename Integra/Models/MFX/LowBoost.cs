using Integra.Core;
using Integra.Common;
using Integra.Core.Interfaces;

namespace Integra.Models.MFX
{
    /// <summary>
    /// Provides validation for the <see cref="LowBoost"/> MFX parameters.
    /// </summary>
    /// <remarks>
    /// 00: Boost Frequency <br/>
    /// 01: Boost Gain <br/>
    /// 02: Boost Width <br/>
    /// 03: Low Gain <br/>
    /// 04: High Gain <br/>
    /// 05: Level <br/>
    /// </remarks>
    public class LowBoost : IToneMFXModel
    {
        public double Get(int index, double value)
        {
            switch (index)
            {
                case 3:
                case 4:
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
                    return value.InvalidateRange<IntegraBoostFrequency>();

                case 1:
                    return value.InvalidateRange(0, 12);

                case 2:
                    return value.InvalidateRange<IntegraBoostWidth>();
                
                case 3:
                case 4:
                    return value.InvalidateRange(-15, 15) + 15;

                case 5:
                    return value.InvalidateRange(0, 127);

                default:
                    return (int)value;
            }
        }
    }
}
