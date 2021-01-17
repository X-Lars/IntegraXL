using Integra.Models;

namespace Integra.Core.Interfaces
{
    /// <summary>
    /// Ensures tone types provide type independent access to the tone's MFX data structure.
    /// </summary>
    /// <remarks><i>Bound to <see cref="SuperNATURALAcousticTone"/>, <see cref="SuperNATURALSynthTone"/>, <see cref="SuperNATURALDrumKit"/>
    /// <see cref="PCMSynthTone"/> and <see cref="PCMDrumKit"/>.</i></remarks>
    public interface IToneMFX
    {
        ToneMFX MFX { get; }
    }

    /// <summary>
    /// Ensures the <see cref="ToneMFX"/> class can invalidate its parameter values.
    /// </summary>
    /// <remarks><i>Bound to all classes in the <see cref="Integra.Models.MFX"/> namespace.</i></remarks>
    public interface IToneMFXModel
    {
        int GetValue(int index, int value);
        int SetValue(int index, int value);
    }
}
