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
        double Get(int index, double value);

        /// <summary>
        /// Converts an <see cref="int"/> value to an INTEGRA-7 parameter value.
        /// </summary>
        /// <param name="index">The index of the value in the <see cref="ToneMFX._Parameters"/> array.</param>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted <paramref name="value"/>.</returns>
        int Set(int index, double value);
    }

    /// <summary>
    /// Ensures classes provide recursive data access.
    /// </summary>
    public interface IIntegraDataClass
    {
        int ID { get; }

        /// <summary>
        /// Stores the instance in the database.
        /// </summary>
        void Insert();

        /// <summary>
        /// Loads the the instance with the specified <paramref name="id"/> from the database.
        /// </summary>
        /// <param name="id">The ID of the instance to load.</param>
        void Select(int id);

        /// <summary>
        /// Removes the referenced instance from the database.
        /// </summary>
        /// <param name="id">The ID of the instance to delete.</param>
        void Delete();

        /// <summary>
        /// Updates the referenced instance in the database.
        /// </summary>
        void Update();

        /// <summary>
        /// Deletes all instances from the database.
        /// </summary>
        void Truncate();
    }

    /// <summary>
    /// Defines an interface for classes that are instantiated per part.
    /// </summary>
    /// <remarks><i>Used by the data access layer insert and select methods to dynamically add the part column.</i></remarks>
    public interface IIntegraPartial
    {
        IntegraParts Part { get; set; }
    }

    /// <summary>
    /// Defines an interface for classes that are associated with a synth tone partial.
    /// </summary>
    /// <remarks><i>Used by the data access layer insert and select methods to dynamically add the partial column.</i></remarks>
    public interface IIntegraSynthTonePartial
    {
        IntegraSynthTonePartials Partial { get; set; }
    }

    public interface IIntegraDrumKitPartial
    {
        int Note { get; set; }
    }

    public interface IIntegraAddressable
    {
        byte MSB { get; }
        byte LSB { get; }
        byte PC { get; }
    }
}
