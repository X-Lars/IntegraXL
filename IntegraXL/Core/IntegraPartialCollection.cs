using IntegraXL.Extensions;
using System.Diagnostics;
using System.Reflection;

namespace IntegraXL.Core
{
    /// <summary>
    /// Defines a strongly typed base collection for <see cref="IntegraPartial"/> derived models.
    /// </summary>
    /// <typeparam name="TPartial">An <see cref="IntegraPartial"/> derived type.</typeparam>
    public abstract class IntegraPartialCollection<TPartial> : IntegraCollection<TPartial> where TPartial : IntegraPartial<TPartial>
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraPartialCollection{TPartial}"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the collection.</param>
        /// <remarks>
        /// <i>Requires the collection's items to be derived from <see cref="IntegraPartial"/>.</i><br/>
        /// <i>Requires the <see cref="IntegraAttribute.Address"/> to specify the address of the first partial.</i><br/>
        /// <i>Requires the <see cref="IntegraAttribute.Request"/> to specify the request to initialize all partials.</i>
        /// </remarks>
        internal IntegraPartialCollection(Integra device, bool connect = true) : base(device, false)
        {
            IntegraAttribute? attribute = GetType().GetCustomAttribute<IntegraAttribute>();

            Debug.Assert(attribute != null);

            Requests.Add(new IntegraRequest(attribute.Request));

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                var item = device.CreateChildModel<TPartial>((Parts)i);

                Debug.Assert(item != null);
                
                Add(item);
            }

            if(connect)
                Connect();
        }

        #endregion

        #region Overrides: Model

        internal override void RequestInitialization()
        {
            foreach (var request in Requests)
            {
                IntegraSystemExclusive systemExclusive = new(Address, request);
                Device.TransmitSystemExclusive(systemExclusive);
            }
        }

        /// <summary>
        /// Gets whether the collection is initialized.
        /// </summary>
        /// <remarks><i>Only returns true if all partials are initialized.</i></remarks>
        public override bool IsInitialized 
        { 
            get => Collection.All(x => x.IsInitialized); 
            internal protected set => base.IsInitialized = value; 
        }

        /// <summary>
        /// Handles received system exclusive messages.
        /// </summary>
        /// <param name="sender">The device that raised the event.</param>
        /// <param name="e">The system exclusive message data.</param>
        /// <remarks><i>Defaults to do nothing.</i></remarks>
        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e) 
        { 
            if(e.SystemExclusive.Address.InRange(this.First().Address, this.Last().Address))
            {
                Device.ReportProgress(this, Collection.Where(x => x.IsInitialized).Count(), Size - 1, e.SystemExclusive.Address.GetStudioSetPart());
            }
        }

        /// <summary>
        /// Initializes the collection with received system exclusive data.
        /// </summary>
        /// <param name="data">The system exclusive data to initialize the collection.</param>
        /// <returns>True if the collection is initialized.</returns>
        /// <remarks><i>Defaults to do nothing.</i></remarks>
        internal override bool Initialize(byte[] data)
        {
            return IsInitialized;
        }

        /// <summary>
        /// Gets a hash code based on the collection's address.
        /// </summary>
        /// <returns>A hash code for the collection.</returns>
        /// <remarks><i>The LSB is maxed out to specify a collection.</i></remarks>
        internal protected override int GetUID()
        {
            // Base hash conflicts with the first element of the collection
            return base.GetUID() | 0xFF;
        }

        #endregion
    }
}
