using Integra.Core.Interfaces;
using System;
namespace Integra.Models.MFX
{
    /// <summary>
    /// Provides validation for the <see cref="StudioSetCommonChorus"/> delay type parameters.
    /// </summary>
    /// <remarks>
    /// 00: Left Switch (ms / Note) <br/>
    /// 01: Delay Left (ms) <br/>
    /// 02: Delay Left (Note) <br/>
    /// 03: Right Switch (ms / Note) <br/>
    /// 04: Delay Right (ms) <br/>
    /// 05: Delay Right (Note) <br/>
    /// 06: Center Switch (ms / Note) <br/>
    /// 07: Delay Center (ms) <br/>
    /// 08: Delay Center (Note) <br/>
    /// 09: Center Feedback <br/>
    /// 10: HF Damp <br/>
    /// 11: Left Level <br/>
    /// 12: Right Level <br/>
    /// 13: Center Level <br/>
    /// </remarks>
    public class CommonChorusDelay : IToneMFXModel
    {
        public double Get(int index, double value)
        {
            switch(index)
            {
                case 9:
                    return (value * 2) - 98;

                default:
                    return value;

            }
        }

        public int Set(int index, double value)
        {
            switch(index)
            {
                case 9:
                    return (int)Math.Round((value + 98) / 2);

                default:
                    return (int)value;
            }
        }
    }
}
