using Integra.Core.Interfaces;

namespace Integra.Models.MFX
{
    /// <summary>
    /// Defines the default common MFX setting.
    /// </summary>
    public class CommonOff : IToneMFXModel
    {
        public double Get(int index, double value)
        {
            return value;
        }

        public int Set(int index, double value)
        {
            return (int)value;
        }
    }
}
