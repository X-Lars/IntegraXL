using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Integra.Core;
using MidiXL;

namespace Integra
{
    [Flags]
    public enum DeviceStatusFlags : int
    {
        DEVICE_READY           = 0x00000000,
        DEVICE_NOT_INITIALIZED = 0x00000001,
        DEVICE_NO_MIDI_DEVICE  = 0x00000002,
    }

    public sealed class Device : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Stores the <b>Singleton</b> device instance.
        /// </summary>
        private static Device _Instance = null;

        /// <summary>
        /// Lock for thread safety.
        /// </summary>
        private static readonly object _Lock = new object();
        
        /// <summary>
        /// Stores a list of <see cref="MidiOutputDevice"/>s installed in the system.
        /// </summary>
        private ObservableCollection<MidiOutputDevice> _MidiOutputDevices = new ObservableCollection<MidiOutputDevice>();

        /// <summary>
        /// Stores a list of <see cref="MidiInputDevice"/>s installed in the system.
        /// </summary>
        private ObservableCollection<MidiInputDevice> _MidiInputDevices = new ObservableCollection<MidiInputDevice>();

        /// <summary>
        /// Stores the device status.
        /// </summary>
        private static DeviceStatus _Status;
        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="Device"/>.
        /// </summary>
        private Device() 
        {
            Debug.Print($"[{nameof(Device)}.Constructor]");
        }

        #endregion

        #region Events

        /// <summary>
        /// Provides the signature for device error events.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the class that raised the event.</param>
        /// <param name="e">An <see cref="IntegraErrorEventArgs"/> containing event data.</param>
        public delegate void DeviceErrorEventHandler(object sender, IntegraErrorEventArgs e);

        /// <summary>
        /// Raised when the <see cref="Device"/> is initialized. <b>Must be subscribed to before any call to <see cref="Instance"/>!</b>
        /// </summary>
        /// <remarks><i>Event is static to be able to subscribe to the event before the Singleton instance is created and auto initialized.</i></remarks>
        public static event EventHandler Initialized;

        /// <summary>
        /// Raised when an error occurs with the <see cref="Device"/>. <b>Must be subscribed to before any call to <see cref="Instance"/>!</b>
        /// </summary>
        /// <remarks><i>Event is static to be able to subscribe to the event before the Singleton instance is created and auto initialized.</i></remarks>
        public static event DeviceErrorEventHandler Error;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <b>Singleton</b> <see cref="Device"/> instance.
        /// </summary>
        /// <remarks><i>This is the external acces point to all INTEGRA-7 functionality.</i></remarks>
        public static Device Instance
        { 
            get
            {
                // Double checked locking for increased performance because locking is more expensive than null checking
                if (_Instance == null)
                {
                    lock(_Lock)
                    {
                        if(_Instance == null)
                        {
                            _Instance = new Device();

                            // Enforces the application to hook up the device listener before before calling the device
                            if (Initialized == null)
                                throw new ApplicationException($"[{nameof(Device)}] Initialization handler not hooked up to the application!\nUse Device.OnInitialization += <...Event Handler...>");

                            if (Error == null)
                                throw new ApplicationException($"[{nameof(Device)}] Error handler not hooked up to the application!\nUse Device.OnError += <...Event Handler...>");

                            // Sets the startup device status
                            _Status = DeviceStatusFlags.DEVICE_NOT_INITIALIZED;
                            
                            _Instance.Initialize();
                        }
                    }
                }

                return _Instance;
            }
        }

        /// <summary>
        /// Gets the device status.
        /// </summary>
        /// <remarks><i>Use the == and != operators in combination with the <see cref="DeviceStatusFlags"/> enumeration to check the device status.</i></remarks>
        public DeviceStatus Status
        {
            get { return _Status; }
        }

        /// <summary>
        /// Gets the available <see cref="MidiOutputDevice"/>s.
        /// </summary>
        public ReadOnlyCollection<MidiOutputDevice> MidiOutputDevices => DeviceManager.MidiOutputDevices;

        /// <summary>
        /// Gets the available <see cref="MidiInputDevice"/>s.
        /// </summary>
        public ReadOnlyCollection<MidiInputDevice> MidiInputDevices => DeviceManager.MidiInputDevices;

        #endregion

        #region Methods

        public void Initialize()
        {
            Console.WriteLine($"[{nameof(Device)}.{nameof(Initialize)}]");

            InitializeMidiOutputDeviceList();
            InitializeMidiInputDeviceList();
        }

        private void InitializeMidiOutputDeviceList()
        {
            Debug.Print($"[{nameof(Device)}.{nameof(InitializeMidiOutputDeviceList)}]");

            if (DeviceManager.MidiOutputDevices.Count == 0)
            {
                _Status += DeviceStatusFlags.DEVICE_NO_MIDI_DEVICE;
                Error?.Invoke(this, new IntegraErrorEventArgs(DeviceStatusFlags.DEVICE_NO_MIDI_DEVICE, "No MIDI output devices"));
                return;
            }

            _MidiOutputDevices.Clear();

            foreach (MidiOutputDevice device in DeviceManager.MidiOutputDevices)
            {
                _MidiOutputDevices.Add(device);
            }
        }

        private void InitializeMidiInputDeviceList()
        {
            Debug.Print($"[{nameof(Device)}.{nameof(InitializeMidiInputDeviceList)}]");

            if (DeviceManager.MidiInputDevices.Count == 0)
            {
                _Status += DeviceStatusFlags.DEVICE_NO_MIDI_DEVICE;
                Error.Invoke(this, new IntegraErrorEventArgs(DeviceStatusFlags.DEVICE_NO_MIDI_DEVICE, "No MIDI input devices."));
                return;
            }

            _MidiInputDevices.Clear();

            foreach(MidiInputDevice device in DeviceManager.MidiInputDevices)
            {
                _MidiInputDevices.Add(device);
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

    /// <summary>
    /// Extends a <see cref="DeviceStatusFlags"/> enumeration with additional operator functionality for addition, removal and comparison.
    /// </summary>
    public class DeviceStatus
    {
        #region Fields

        /// <summary>
        /// Stores the device status flags.
        /// </summary>
        private static DeviceStatusFlags _Flags;

        #endregion

        #region Constructor

        /// <summary>
        /// Private constructor used for operator overloading.
        /// </summary>
        /// <param name="flags">A <see cref="DeviceStatusFlags"/> to initialize the device status.</param>
        private DeviceStatus(DeviceStatusFlags flags = DeviceStatusFlags.DEVICE_NOT_INITIALIZED)
        {
            this.Flags = flags;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the device status flags.
        /// </summary>
        public DeviceStatusFlags Flags
        {
            get { return _Flags; }
            private set 
            {
                // TODO: Remove Test
                Debug.Print(value.ToString());

                _Flags = value; 
            }
        }

        #endregion
        
        #region Operator Overloading

        /// <summary>
        /// Overloads the assignment operator to create a new <see cref="DeviceStatus"/> initialized with the provided <see cref="DeviceStatusFlags"/>.
        /// </summary>
        /// <param name="flags">A <see cref="DeviceStatusFlags"/> to initialize the <see cref="DeviceStatus"/>.</param>
        public static implicit operator DeviceStatus(DeviceStatusFlags flags)
        {
            return new DeviceStatus(flags);
        }

        /// <summary>
        /// Overloads the addition operator to add a <see cref="DeviceStatusFlags"/> value to the <see cref="DeviceStatus"/>.
        /// </summary>
        /// <param name="status">The LHS <see cref="DeviceStatus"/> to add the specified flag to.</param>
        /// <param name="flags">The RHS <see cref="DeviceStatusFlags"/> to add.</param>
        /// <returns>The <see cref="DeviceStatus"/> with the specified flag set.</returns>
        public static DeviceStatus operator +(DeviceStatus status, DeviceStatusFlags flags)
        {
            status.Flags |= flags;

            return status;
        }

        /// <summary>
        /// Overloads the substraction operator to remove a <see cref="DeviceStatusFlags"/> value from the <see cref="DeviceStatus"/>.
        /// </summary>
        /// <param name="status">The LHS <see cref="DeviceStatus"/> to remove the specified flag from.</param>
        /// <param name="flags">The RHS <see cref="DeviceStatusFlags"/> to remove.</param>
        /// <returns>The <see cref="DeviceStatus"/> with the specified flag cleared.</returns>
        public static DeviceStatus operator -(DeviceStatus status, DeviceStatusFlags flags)
        {
            status.Flags &= ~flags;

            return status;
        }

        /// <summary>
        /// Overloads the equals operator to compare the <see cref="DeviceStatus"/> with a <see cref="DeviceStatusFlags"/>.
        /// </summary>
        /// <param name="status">The LHS <see cref="DeviceStatus"/> to compare.</param>
        /// <param name="flags">The RHS <see cref="DeviceStatusFlags"/> to compare to.</param>
        /// <returns>A <see cref="bool"/> containing true if the specified status flag is set, false otherwise.</returns>
        public static bool operator ==(DeviceStatus status, DeviceStatusFlags flags)
        {
            if (status.Flags == DeviceStatusFlags.DEVICE_READY)
                return status.Flags == DeviceStatusFlags.DEVICE_READY;

            return status.Flags.HasFlag(flags);
        }

        /// <summary>
        /// Overloads the not equals operator to compare the <see cref="DeviceStatus"/> with a <see cref="DeviceStatusFlags"/>.
        /// </summary>
        /// <param name="status">The LHS <see cref="DeviceStatus"/> to compare.</param>
        /// <param name="flags">The RHS <see cref="DeviceStatusFlags"/> to compare to.</param>
        /// <returns>A <see cref="bool"/> containing false if the specified status flag is set, false otherwise.</returns>
        public static bool operator !=(DeviceStatus status, DeviceStatusFlags flags)
        {
            if (status.Flags == DeviceStatusFlags.DEVICE_READY)
                return status.Flags != DeviceStatusFlags.DEVICE_READY;

            return !status.Flags.HasFlag(flags);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Determines whether the specified object is equal to the <see cref="DeviceStatus"/> object.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare the <see cref="DeviceStatus"/> object to.</param>
        /// <returns>A <see cref="bool"/> containing true if the specified object is equal to the <see cref="DeviceStatus"/> object, false otherwise.</returns>
        /// <remarks><i>Not overridden, used to suppress operator overloading warning.</i></remarks>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Creates a hash code for the <see cref="DeviceStatus"/> object.
        /// </summary>
        /// <returns>An <see cref="int"/> containing the hash code for the <see cref="DeviceStatus"/> object.</returns>
        /// <remarks><i>Not overridden, used to suppress operator overloading warning.</i></remarks>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
