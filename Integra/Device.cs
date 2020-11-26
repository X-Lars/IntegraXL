using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MidiXL;

namespace Integra
{
    public sealed class Device : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Stores the singleton device instance.
        /// </summary>
        private static Device _Instance = null;

        /// <summary>
        /// Lock for thread safety.
        /// </summary>
        private static readonly object _Lock = new object();
        private ObservableCollection<MidiOutputDevice> _MidiOutputDevices = new ObservableCollection<MidiOutputDevice>();
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
        /// Raised when the <see cref="Device"/> is initialized. <b>Must be subscribed to before any call to <see cref="Instance"/>!</b>
        /// </summary>
        /// <remarks><i>Event is static to be able to subscribe to the event before the Singleton instance is created and auto initialized.</i></remarks>
        public static event EventHandler OnInitialize;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets the Singleton <see cref="Device"/> instance.
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
                            if (OnInitialize == null)
                                throw new ApplicationException($"[{nameof(Device)}] Initialization handler not hooked up to the application!\nUse Device.OnInitialization += <'Event Handler'>");

                            _Instance.Initialize();
                        }
                    }
                }

                return _Instance;
            }
        }

        /// <summary>
        /// Gets the available <see cref="MidiOutputDevice"/>s.
        /// </summary>
        public ObservableCollection<MidiOutputDevice> MidiOutputDevices
        {
            get { return _MidiOutputDevices; }
            set
            {
                _MidiOutputDevices = value;
                NotifyPropertyChanged();
            }
        }

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
        }

        private void InitializeMidiOutputDeviceList()
        {
            if(DeviceManager.MidiOutputDevices.Count == 0)
                Console.WriteLine("No Mide output devices error");

            MidiOutputDevices.Clear();

            foreach(MidiOutputDevice device in DeviceManager.MidiOutputDevices)
            {
                MidiOutputDevices.Add(device);
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
