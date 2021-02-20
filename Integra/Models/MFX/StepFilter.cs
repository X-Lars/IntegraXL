using Integra.Core;
using Integra.Common;
using Integra.Core.Interfaces;

namespace Integra.Models.MFX
{
    /// <summary>
    /// Provides validation for the <see cref="StepFilter"/> MFX parameters.
    /// </summary>
    /// <remarks>
    /// 00: Step 01 <br/>
    /// 01: Step 02 <br/>
    /// 02: Step 03 <br/>
    /// 03: Step 04 <br/>
    /// 04: Step 05 <br/>
    /// 05: Step 06 <br/>
    /// 06: Step 07 <br/>
    /// 07: Step 08 <br/>
    /// 08: Step 09 <br/>
    /// 09: Step 10 <br/>
    /// 10: Step 11 <br/>
    /// 11: Step 12 <br/>
    /// 12: Step 13 <br/>
    /// 13: Step 14 <br/>
    /// 14: Step 15 <br/>
    /// 15: Step 16 <br/>
    /// 16: Rate Switch <br/>
    /// 17: Rate <br/>
    /// 18: Attack <br/>
    /// 19: Filter Type <br/>
    /// 20: Filter Slope <br/>
    /// 21: Filter Resonance <br/>
    /// 22: Filter Gain <br/>
    /// 23: Level <br/>
    /// </remarks>
    public class StepFilter : IToneMFXModel
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
                case 0: 
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:

                    return value.InvalidateRange(0, 127);

                default:
                    return (int)value;
            }
        }
    }
}
