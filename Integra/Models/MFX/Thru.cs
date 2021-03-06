using Integra.Core.Interfaces;

namespace Integra.Models.MFX
{
    /// <summary>
    /// Defines the default MFX setting.
    /// </summary>
    public class Thru : IToneMFXModel
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
