using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    /// <summary>
    /// Extends the <see cref="EventArgs"/> class with <see cref="Status"/> and <see cref="StatusFlags"/> properties.
    /// </summary>
    public class IntegraEventArgs : EventArgs
    {
        /// <summary>
        /// Creates and initializes a <see cref="IntegraEventArgs"/> with the specified values.
        /// </summary>
        /// <param name="status"></param>
        public IntegraEventArgs(DeviceStatus status)
        {
            Action = status.Action;
            Message = status.Message;
            Progress = status.Progress;
            StatusFlags = status.Flags;
            StatusText = status.Text;
        }

        /// <summary>
        /// Gets the event's associated status flags.
        /// </summary>
        public DeviceStatusFlags StatusFlags { get; }
        public string Action { get; }
        public double Progress { get; }
        public string Message { get; }
        public string StatusText { get; }
    }
}
