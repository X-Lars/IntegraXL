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
        /// <summary>
        /// Gets a reference to the MFX data structure of the tone.
        /// </summary>
        ToneMFX MFX { get; }
    }

    /// <summary>
    /// Ensures the <see cref="ToneMFX"/> class can invalidate its parameter values.
    /// </summary>
    /// <remarks><i>Bound to all classes in the <see cref="Integra.Models.MFX"/> namespace.</i></remarks>
    public interface IToneMFXModel
    {
        /// <summary>
        /// Converts an INTEGRA-7 parameter value to an <see cref="int"/> value.
        /// </summary>
        /// <param name="index">The index of the value in the <see cref="ToneMFX._Parameters"/> array.</param>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted <paramref name="value"/>.</returns>
        int Get(int index, int value);

        /// <summary>
        /// Converts an <see cref="int"/> value to an INTEGRA-7 parameter value.
        /// </summary>
        /// <param name="index">The index of the value in the <see cref="ToneMFX._Parameters"/> array.</param>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted <paramref name="value"/>.</returns>
        int Set(int index, int value);
    }
}
