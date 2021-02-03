using Integra.Core.Interfaces;
using System;

namespace Integra.Models.MFX
{
    /// <summary>
    /// Provides validation for the <see cref="StudioSetCommonChorus"/> chorus type parameters.
    /// </summary>
    /// <remarks>
    /// 00: Filter Type <br/>
    /// 01: Cutoff Freq <br/>
    /// 02: Pre Delay <br/>
    /// 03: Rate Switch (Hz / Note)<br/>
    /// 04: Rate (Hz) <br/>
    /// 05: Rate (Note) <br/>
    /// 06: Depth <br/>
    /// 07: Phase <br/>
    /// 08: Feedback <br/>
    /// </remarks>
    public class CommonChorus : IToneMFXModel
    {
        public double Get(int index, double value)
        {
            switch(index)
            {
                case 4:
                    return value * 0.05;

                default:
                    return value;
            }
        }

        public int Set(int index, double value)
        {
            switch(index)
            {
                case 4:
                    
                    return (int)Math.Round(value / 0.05);

                default:
                    return (int)value;
            }
        }
    }
}
