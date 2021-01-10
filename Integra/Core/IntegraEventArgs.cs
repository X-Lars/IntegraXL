using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsXL;

namespace Integra.Core
{
    /// <summary>
    /// Extends the <see cref="EventArgs"/> class with a <see cref="DeviceFlags"/> property.
    /// </summary>
    public class IntegraStatusEventArgs : EventArgs
    {
        /// <summary>
        /// Creates and initializes a new <see cref="IntegraStatusEventArgs"/> instance with the specified <see cref="DeviceStatusFlags"/>.
        /// </summary>
        /// <param name="deviceFlags">A <see cref="DeviceStatusFlags"/> representing the device status.</param>
        public IntegraStatusEventArgs(DeviceStatusFlags deviceFlags) : base()
        {
            DeviceFlags = deviceFlags;
        }

        /// <summary>
        /// Gets the device flags associated with the event.
        /// </summary>
        public DeviceStatusFlags DeviceFlags { get; }
    }

    /// <summary>
    /// Extends the <see cref="EventArgs"/> class with 
    /// </summary>
    public class IntegraOperationEventArgs : EventArgs
    {
        /// <summary>
        /// Creates and initializes a <see cref="IntegraOperationEventArgs"/> with the specified values.
        /// </summary>
        /// <param name="status"></param>
        public IntegraOperationEventArgs(DeviceStatus status)
        {
            Action = status.Action;
            Message = status.Message;
            Progress = status.Progress;
            Text = status.Text;
        }

        public string Action { get; }
        public double Progress { get; }
        public string Message { get; }
        public string Text { get; }
    }
}
