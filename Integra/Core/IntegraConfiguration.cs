using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Integra.Core
{
    /// <summary>
    /// Defines the structure for configuration of the <see cref="Device"/>.
    /// </summary>
    public class IntegraConfiguration : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Stores the ID of the selected MIDI output device.
        /// </summary>
        private int _MidiOutputDeviceID = -1;

        /// <summary>
        /// Stores the ID of the selected MIDI input device.
        /// </summary>
        private int _MidiInputDeviceID = -1;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ID of the selected MIDI output device.
        /// </summary>
        public int MidiOutputDeviceID
        {
            get { return _MidiOutputDeviceID; }
            set 
            {
                if (_MidiOutputDeviceID == value) 
                    return;

                _MidiOutputDeviceID = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the ID of the selected MIDI input device.
        /// </summary>
        public int MidiInputDeviceID
        {
            get { return _MidiInputDeviceID; }
            set 
            {
                if (_MidiInputDeviceID == value) 
                    return;

                _MidiInputDeviceID = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region INotifyPropertyChanged

        /// <summary>
        /// Event raised when a property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the specified property.
        /// </summary>
        /// <param name="propertyName">A <see cref="string"/> containing the name of the property that is changed.</param>
        /// <remarks><i>If no property name is specified, the actual name of the property in code is used.</i></remarks>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
