using Integra.Core.Interfaces;

namespace Integra.Models.MFX
{
    public class Equalizer : IToneMFXModel
    {

        public int GetValue(int index, int value)
        {
            switch (index)
            {
                case 0:
                    return value;
                case 1:
                    return value - 15;
                    
                default:
                    return value;
            }
        }

        public int SetValue(int index, int value)
        {
            switch (index)
            {
                case 0:
                    return value;
                case 1:
                    return value + 15;
                default:
                    return value;
            }
        }

    }
}
