using IntegraXL.Templates;

namespace IntegraXL.Core
{
    /// <summary>
    /// Base class for all tone banks.
    /// </summary>
    public abstract class IntegraToneBank : IntegraTemplateCollection<ToneTemplate>
    {
        /// <summary>
        /// Creates a new <see cref="IntegraToneBank"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the collection.</param>
        internal IntegraToneBank(Integra device) : base(device) { }

        /// <summary>
        /// Provides a user friendly <see cref="string"/> representation of the tone bank name.
        /// </summary>
        /// <returns>A user friendly <see cref="string"/> representation of the tone bank name.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
