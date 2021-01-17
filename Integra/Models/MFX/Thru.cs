using Integra.Core.Interfaces;

namespace Integra.Models.MFX
{
    /// <summary>
    /// Defines the default MFX setting.
    /// </summary>
    public class Thru : IToneMFXModel
    {
        public int Get(int index, int value)
        {
            return value;
        }

        public int Set(int index, int value)
        {
            return value;
        }
    }
}
