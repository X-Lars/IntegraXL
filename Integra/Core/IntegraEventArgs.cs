using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    /// <summary>
    /// Extends the <see cref="EventArgs"/> class with <see cref="StatusFlags"/> and <see cref="Message"/> properties.
    /// </summary>
    public class IntegraEventArgs : EventArgs
    {
        /// <summary>
        /// Creates and initializes a <see cref="IntegraEventArgs"/> with the specified values.
        /// </summary>
        /// <param name="flags"></param>
        /// <param name="message"></param>
        public IntegraEventArgs(DeviceStatusFlags flags, string message)
        {
            this.StatusFlags = flags;
            this.Message = message;
        }

        /// <summary>
        /// Gets the event's associated status flags.
        /// </summary>
        public DeviceStatusFlags StatusFlags { get; }

        /// <summary>
        /// Gets the message if provided.
        /// </summary>
        public string Message { get; }
    }
}
