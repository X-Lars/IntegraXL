using System.Diagnostics;
using System.Reflection;

namespace IntegraXL.Core
{
    /// <summary>
    /// Defines a strongly typed base collection for <see cref="IntegraTemplate"/> derived templates.
    /// </summary>
    /// <typeparam name="TTemplate">The template type specifier.</typeparam>
    public abstract class IntegraTemplateCollection<TTemplate> : IntegraCollection<TTemplate> where TTemplate : IntegraTemplate
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraTemplateCollection{TTemplate}"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the collection.</param>
        /// <remarks>
        /// <i>Requires the collection's items to be derived from <see cref="IntegraTemplate"/>.</i><br/>
        /// <i>Requires the <see cref="IntegraAttribute.Address"/> to specify the INTEGRA-7 function address.</i><br/>
        /// <i>Requires the <see cref="IntegraAttribute.Request"/> to specify the offset to the first template.</i>
        /// <i>Requires the <see cref="IntegraAttribute.Size"/> to specify the number of items.</i>
        /// </remarks>
        internal protected IntegraTemplateCollection(Integra device) : base(device) 
        {
            IntegraAttribute? attribute = GetType().GetCustomAttribute<IntegraAttribute>();

            Debug.Assert(attribute != null);

            for (int size = Size, i = 0; size > 0; size -= 0x40, i++)
            {
                // request[0] = 0x57, 0x00, 0x00, 0x40;   0.. 63
                // request[1] = 0x57, 0x00, 0x40, 0x40;  64..127
                // request[2] = 0x57, 0x01, 0x00, 0x40; 128..191
                // request[3] = 0x57, 0x01, 0x40, 0x40; 192..255

                IntegraRequest request = new IntegraRequest(attribute.Request);

                request[1] += (byte)(i / 2);
                request[2]  = (byte)(i % 2 * 0x40);
                request[3]  = (byte)(size > 0x40 ? 0x40 : size);

                Requests.Add(request);
            }
        }

        #endregion

        #region Overrides: Model

        /// <summary>
        /// Handles received system exclusive messages matched to the exact address and length.
        /// </summary>
        /// <param name="sender">The device that raised the event.</param>
        /// <param name="e">The system exclusive message data.</param>
        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            if (e.SystemExclusive.Address == Address)
            {
                if (e.SystemExclusive.Data.Length == _ItemSize)
                {
                    // Skip the empty message of 0x00 bytes received between requests
                    if (e.SystemExclusive.Data[0] == 0x00)
                        return;

                    if (Initialize(e.SystemExclusive.Data))
                    {
                        // Collection Initialized
                    }
                }
            }
        }

        /// <summary>
        /// Creates and initializes a template from the received system exclusive data and adds it to the collection.
        /// </summary>
        /// <param name="data">The system exclusive data to create and initialize new templates.</param>
        /// <returns>True if the collection is initialized based on the collection count.</returns>
        protected override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                var item = Activator.CreateInstance(typeof(TTemplate), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { Collection.Count + 1, data }, null);

                Debug.Assert(item != null);

                Add((TTemplate)item);

                if (Collection.Count == Size)
                    IsInitialized = true;
            }

            return IsInitialized;
        }

        /// <summary>
        /// Gets a hash code based on the collection function address.
        /// </summary>
        /// <returns>A hash code for the collection.</returns>
        /// <remarks>
        /// <i>The first two bytes of the offset represent the bank select MSB and LSB.</i><br/>
        /// <i>The last two bytes are maxed out to specify a template collection.</i><br/>
        /// </remarks>
        protected internal override int GetUID()
        {
            return Requests[0] | 0x0000FFFF;
        }

        #endregion
    }
}
