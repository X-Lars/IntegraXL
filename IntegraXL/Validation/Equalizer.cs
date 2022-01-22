using IntegraXL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Validation
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
    public sealed class Equalizer : IntegraMFXValidator
    {
        public override double Get(int index, int value)
        {
            double result = base.Get(index, value);

            switch (index)
            {
                case 1: // Low  Gain
                case 3: // Mid1 Gain
                case 6: // Mid2 Gain
                case 9: // High Gain
                    return result - 15;
            }

            return result;
        }

        public override int Set(int index, double value)
        {
            switch (index)
            {
                case 0: // Low Freq
                    value = Invalidate<IntegraLowFrequencies>(value);
                    break;

                case 1: // Low  Gain
                case 3: // Mid1 Gain
                case 6: // Mid2 Gain
                case 9: // High Gain
                    value = Invalidate(value, -15, 15) + 15;
                    break;

                case 2: // Mid1 Freq
                case 5: // Mid2 Freq
                    value = Invalidate<IntegraMidFrequencies>(value);
                    break;

                case 4: // Mid1 Q
                case 7: // Mid2 Q
                    value = Invalidate<IntegraMidQs>(value);
                    break;

                case 8: // High Freq
                    value = Invalidate<IntegraHighFrequencies>(value);
                    break;

                case 10: // Level
                    value = Invalidate(value, 0, 127);
                    break;
            }

            return base.Set(index, value);
        }
    }
}
