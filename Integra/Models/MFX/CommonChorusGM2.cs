using Integra.Core.Interfaces;
using System;

namespace Integra.Models.MFX
{
    /// <summary>
    /// Provides validation for the <see cref="StudioSetCommonChorus"/> GM2 chorus type parameters.
    /// </summary>
    /// <remarks>
    /// 00: Filter Type <br/>
    /// 01: Cutoff Freq <br/>
    /// 02: Pre Delay <br/>
    /// 03: Rate Switch <br/>
    /// 06: Depth <br/>
    /// 07: Phase <br/>
    /// 08: Feedback <br/>
    /// </remarks>
    public class CommonChorusGM2 : IToneMFXModel
    {
        public double Get(int index, double value)
        {
            switch(index)
            {
                default:
                    return value;
            }
        }

        public int Set(int index, double value)
        {
            switch(index)
            {
                default:
                    return (int)value;
            }
        }
    }
}
