using IntegraXL.Core;

namespace IntegraXL.Validation
{
    /// <summary>
    /// Provides validation for the <see cref="CommonReverb"/> room type parameters.
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
    public class CommonReverb : IntegraMFXValidator
    {
        public override double Get(int index, int value)
        {
            double result = base.Get(index, value);

            switch (index)
            {
                case 2:
                    return result * 0.1;
            }

            return result;
        }

        public override int Set(int index, double value)
        {
            switch (index)
            {
                case 2:
                    value = (int)Math.Round(value / 0.1);
                    break;
            }

            return base.Set(index, value);

        }
    }
}
