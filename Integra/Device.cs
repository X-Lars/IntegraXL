using Integra.Core;
using Integra.Models;
using MidiXL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ToolsXL.Config;

namespace Integra
{
    [Flags]
    public enum DeviceStatusFlags : int
    {
        DEVICE_READY                   = 0,
        DEVICE_NO_MIDI_SETUP           = 1,
        DEVICE_NO_MIDI_OUTPUT_DEVICES  = 2,
        DEVICE_NO_MIDI_INPUT_DEVICES   = 4,
        DEVICE_NO_MIDI_OUTPUT_SELECTED = 8,
        DEVICE_NO_MIDI_INPUT_SELECTED  = 16,
        DEVICE_NOT_CONNECTED           = 32,
        DEVICE_CONNECTION_LOST         = 64
        // TODO: number of devices changed (device disconnected or new device id)
    }

    public sealed class Device : INotifyPropertyChanged
    {
        #region Constants

        /// <summary>
        /// Defines the time in milliseconds to wait for a response before the connection is considered lost.
        /// </summary>
        private const int DEVICE_CONNECTION_TIMEOUT = 1000;

        /// <summary>
        /// Defines the aproximate MIDI latency in milliseconds.
        /// </summary>
        private const int DEVICE_LATENCY = 50;

        #endregion

        #region Fields

        /// <summary>
        /// Stores the <b>Singleton</b> device instance.
        /// </summary>
        private static Device _Instance = null;

        /// <summary>
        /// Stores a reference to the selected <see cref="MidiOutputDevice"/>.
        /// </summary>
        private MidiOutputDevice _MidiOutputDevice;

        /// <summary>
        /// Stores a reference to the selected <see cref="MidiInputDevice"/>.
        /// </summary>
        private MidiInputDevice _MidiInputDevice;

        /// <summary>
        /// Stores the connection state of the <see cref="Device"/>.
        /// </summary>
        internal static bool _IsConnected = false;

        /// <summary>
        /// Stores whether <see cref="Initialize"/> has been called.
        /// </summary>
        private bool _IsInitialized = false;

        /// <summary>
        /// Stores the device status.
        /// </summary>
        /// <remarks><i>The device status is initiated with <see cref="DeviceStatusFlags.DEVICE_NO_MIDI_SETUP"/>.</i></remarks>
        private static DeviceStatus _Status = new DeviceStatus(DeviceStatusFlags.DEVICE_NO_MIDI_SETUP);

        /// <summary>
        /// Stores all executing tasks.
        /// </summary>
        private static List<Task> _Tasks = new List<Task>();

        /// <summary>
        /// Tracks the state of the <see cref="TaskManager"/>.
        /// </summary>
        private bool _IsTaskRunning = false;


        // TODO: Device count changes
        private int _MidiOutputDeviceCount = 0;
        // TODO: Device count changes
        private int _MidiInputDeviceCount = 0;

        /// <summary>
        /// Lock for thread safety.
        /// </summary>
        private static readonly object _Lock = new object();

        

        #region Fields: INTEGRA-7

        /// <summary>
        /// Stores a reference to the INTEGRA-7 setup data structure.
        /// </summary>
        private Setup _Setup = new Setup();

        private StudioSet _StudioSet = new StudioSet();

        /// <summary>
        /// Stores a reference to the INTEGRA-7 studio set collection.
        /// </summary>
        private StudioSets _StudioSets = new StudioSets();

        /// <summary>
        /// Stores a reference to the currently selected INTEGRA-7 tone.
        /// </summary>
        private IntegraTone _SelectedTone = new IntegraTone();

        
        #endregion

        #endregion

        #region Events

        /// <summary>
        /// Provides the signature for device operation events.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the class that raised the event.</param>
        /// <param name="e">An <see cref="IntegraOperationEventArgs"/> containing event data.</param>
        public delegate void DeviceOperationEventHandler(object sender, IntegraOperationEventArgs e);

        /// <summary>
        /// Provides the signature for device status events.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the class that raised the event.</param>
        /// <param name="e">An <see cref="IntegraStatusEventArgs"/> containing event data.</param>
        public delegate void DeviceStatusEventHandler(object sender, IntegraStatusEventArgs e);

        /// <summary>
        /// Raised when the <see cref="Device"/> starts an operation.
        /// </summary>
        public event DeviceOperationEventHandler OperationStart;

        /// <summary>
        /// Raised when the <see cref="Device"/> operation progresses.
        /// </summary>
        public event DeviceOperationEventHandler OperationProgress;

        /// <summary>
        /// Raised when the <see cref="Device"/> finished an operation.
        /// </summary>
        public event DeviceOperationEventHandler OperationComplete;

        /// <summary>
        /// Raised when the <see cref="Status"/> is changed. <b>Must be subscribed to before any call to <see cref="Instance"/>!</b>
        /// </summary>
        /// <remarks><i>Event is static to be able to subscribe to the event before the Singleton instance is created and auto initialized.</i></remarks>
        public static event DeviceStatusEventHandler StatusChanged;

        /// <summary>
        /// Stores the synchronization context of instantiating class.
        /// </summary>
        private static readonly SynchronizationContext _UIContext = SynchronizationContext.Current;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="Device"/>.
        /// </summary>
        private Device() 
        {
            Debug.Print($"[{nameof(Device)}]");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <b>Singleton</b> <see cref="Device"/> instance.
        /// </summary>
        /// <remarks><i>This is the external access point to all INTEGRA-7 functionality.</i></remarks>
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
                            if (StatusChanged == null)
                                throw new ApplicationException($"[{nameof(Device)}] Initialization handler not hooked up to the application!\nUse Device.OnInitialization += <...Event Handler...>");
                            
                            _Instance.Initialize();
                        }
                    }
                }

                return _Instance;
            }
        }

        /// <summary>
        /// Gets the synchronization context of the <see cref="Device"/>.
        /// </summary>
        internal static SynchronizationContext UIContext
        {
            get { return _UIContext; }
        }

        /// <summary>
        /// Gets the device status.
        /// </summary>
        public DeviceStatus Status
        {
            get { return _Status; }
            private set
            {
                _Status = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the device status flags.
        /// </summary>
        public DeviceStatusFlags Flags
        {
            get { return _Status.Flags; }
            private set
            {
                if(_Status.Flags != value)
                {
                    _Status.Flags = value;
                    NotifyPropertyChanged();

                    Debug.Print($"[{nameof(Device)}.{nameof(Status)}] {_Status.Flags}");
                }
            }
        }

        /// <summary>
        /// Gets wheter the <see cref="Device"/> is connected.
        /// </summary>
        public bool IsConnected
        {
            get { return _IsConnected; }
            private set
            {
                if (IsConnected != value)
                {
                    _IsConnected = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        /// <summary>
        /// Gets the available <see cref="MidiOutputDevice"/>s.
        /// </summary>
        public ReadOnlyCollection<MidiOutputDevice> MidiOutputDevices => DeviceManager.MidiOutputDevices;

        /// <summary>
        /// Gets the available <see cref="MidiInputDevice"/>s.
        /// </summary>
        public ReadOnlyCollection<MidiInputDevice> MidiInputDevices => DeviceManager.MidiInputDevices;

        /// <summary>
        /// Gets or sets the selected <see cref="MidiOutputDevice"/>.
        /// </summary>
        public MidiOutputDevice MidiOutputDevice
        {
            get { return _MidiOutputDevice; }
            set
            {
                // Device not changed
                if (_MidiOutputDevice == value)
                    return;

                // Delete the current device
                if (_MidiOutputDevice != null)
                {
                    // TODO: _MidiOutputDevice.Close() causes problems
                    _MidiOutputDevice = null;
                }

                // No device selected
                if (value == null)
                {
                    InvalidateDeviceStatus();
                    return;
                }

                try
                {
                    _MidiOutputDevice = value;
                    _MidiOutputDevice.Open();
                }
                catch (MidiOutputDeviceException exception)
                {

                    throw new IntegraException("An error occured while selecting the MIDI output device.", exception);
                }

                NotifyPropertyChanged();

                // Only invalidate the device status if connection is possible
                if(MidiInputDevice != null && _IsInitialized)
                    InvalidateDeviceStatus();
            }
        }

        /// <summary>
        /// Gets or sets the selected <see cref="MidiInputDevice"/>.
        /// </summary>
        public MidiInputDevice MidiInputDevice
        {
            get { return _MidiInputDevice; }
            set
            {
                if (_MidiInputDevice == value)
                    return;

                // Delete the current device
                if (_MidiInputDevice != null)
                {
                    _MidiInputDevice.SystemExclusiveReceived -= SystemExclusiveReceived;
                    _MidiInputDevice.UniversalNonRealTimeReceived -= UniversalNonRealTimeReceived;
                    // TODO: _MidiInputDevice.Close() causes problems
                    _MidiInputDevice = null;
                }

                if(value == null)
                {
                    InvalidateDeviceStatus();
                    return;
                }

                try
                {
                    _MidiInputDevice = value;
                    _MidiInputDevice.Open();
                    _MidiInputDevice.Start();
                }
                catch (MidiOutputDeviceException exception)
                {
                    throw new IntegraException("An error occured while selecting the MIDI input device.", exception);
                }

                _MidiInputDevice.SystemExclusiveReceived += SystemExclusiveReceived;
                _MidiInputDevice.UniversalNonRealTimeReceived += UniversalNonRealTimeReceived;

                NotifyPropertyChanged();

                // Only invalidate the device status if connection is possible
                if (MidiOutputDevice != null && _IsInitialized)
                    InvalidateDeviceStatus();
            }
        }

        #region Properties: INTEGRA-7

        /// <summary>
        /// Gets the INTEGRA-7 setup.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public Setup Setup
        {
            get { return _Setup; }

        }

        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public StudioSet StudioSet
        {
            get { return _StudioSet; }
        }

        /// <summary>
        /// Gets the collection of INTEGRA-7 studio sets.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public StudioSets StudioSets
        {
            get { return _StudioSets; }
        }

        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public IntegraTone SelectedTone
        {
            get { return _SelectedTone; }
            set
            {
                _SelectedTone = value;
                Tone = new Tone(_SelectedTone);
                // TODO: Set actual tone
                NotifyPropertyChanged();
            }
        }

        public Tone _Tone;
        public Tone Tone
        {
            get
            {
                if(_Tone == null)
                {
                    _Tone = new Tone(StudioSet.Part[(int)SelectedPart].ToneBankSelectMSB, StudioSet.Part[(int)SelectedPart].ToneBankSelectLSB, StudioSet.Part[(int)SelectedPart].ToneProgramNumber);
                }

                return _Tone;
            }

            set
            {
                if(_Tone != value)
                {
                    _Tone = value;

                    MidiOutputDevice.Send(new ControlChangeMessage((MidiChannels)(int)StudioSet.Part[(int)SelectedPart].ReceiveChannel, 0, _Tone.MSB));
                    MidiOutputDevice.Send(new ControlChangeMessage((MidiChannels)(int)StudioSet.Part[(int)SelectedPart].ReceiveChannel, 32, _Tone.LSB));
                    MidiOutputDevice.Send(new ProgramChangeMessage((MidiChannels)(int)StudioSet.Part[(int)SelectedPart].ReceiveChannel, _Tone.PC));

                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraParts SelectedPart { get; set; } = IntegraParts.Part01;

        #endregion

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the <see cref="MidiInputDevice.UniversalNonRealTimeReceived"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="UniversalNonRealTimeMessageEventArgs"/> containing event data.</param>
        private void UniversalNonRealTimeReceived(object sender, UniversalNonRealTimeMessageEventArgs e)
        {
            Debug.Print($"[{nameof(Device)}.{nameof(UniversalNonRealTimeReceived)}] {string.Join(" ", e.Message.Data.Select(x => string.Format("{0:X2}", x)))}");

            IsConnected = true;
        }

        /// <summary>
        /// Handles the <see cref="MidiInputDevice.SystemExclusiveReceived"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="SystemExclusiveMessageEventArgs"/> containing event data.</param>
        private void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            Debug.Print($"[{nameof(Device)}.{nameof(SystemExclusiveReceived)}] {string.Join(" ", e.Message.Data.Select(x => string.Format("{0:X2}", x)))}");
        }

        #endregion

        #region Methods

        internal void SendSystemExclusive(IntegraSystemExclusive syx)
        {
            Debug.Print($"[{nameof(Device)}.{nameof(SendSystemExclusive)}] {syx}");
            lock (MidiOutputDevice)
            {
                MidiOutputDevice.Send(new SystemExclusiveMessage(syx));
                Thread.Sleep(DEVICE_LATENCY);
            }
        }

        /// <summary>
        /// Initializes the <see cref="Device"/>.
        /// </summary>
        public void Initialize()
        {
            Debug.Print($"[{nameof(Device)}.{nameof(Initialize)}]");

            // Check for initial status to load devices from the configuration
            if (!_IsInitialized)
            {
                DeviceStatusFlags flags = DeviceStatusFlags.DEVICE_READY;

                // Load MIDI configuration from App.config
                int midiOutputDeviceID = Config<IntegraConfiguration>.Get.MidiOutputDeviceID;
                int midiInputDeviceID = Config<IntegraConfiguration>.Get.MidiInputDeviceID;

                // Set device count to track device changes
                _MidiOutputDeviceCount = MidiOutputDevices.Count;
                _MidiInputDeviceCount = MidiInputDevices.Count;

                // Invalidate device count
                if (_MidiOutputDeviceCount == 0)
                    flags |= DeviceStatusFlags.DEVICE_NO_MIDI_OUTPUT_DEVICES;

                if (_MidiInputDeviceCount == 0)
                    flags |= DeviceStatusFlags.DEVICE_NO_MIDI_INPUT_DEVICES;

                // Try set the MIDI output device
                if (_MidiOutputDeviceCount - 1 >= midiOutputDeviceID)
                {
                    MidiOutputDevice = DeviceManager.GetMidiOutputDevice(midiOutputDeviceID);
                }
                else
                {
                    flags |= DeviceStatusFlags.DEVICE_NO_MIDI_OUTPUT_SELECTED;
                }

                // Try set the MIDI input device
                if (_MidiInputDeviceCount - 1 >= midiInputDeviceID)
                {
                    MidiInputDevice = DeviceManager.GetMidiInputDevice(midiInputDeviceID);
                }
                else
                {
                    flags |= DeviceStatusFlags.DEVICE_NO_MIDI_INPUT_SELECTED;
                }

                if(flags != DeviceStatusFlags.DEVICE_READY)
                {
                    flags |= DeviceStatusFlags.DEVICE_NO_MIDI_SETUP;
                }

                _IsInitialized = true;
            }

            InvalidateDeviceStatus();
        }

        /// <summary>
        /// Invalidates the device status flags.
        /// </summary>
        private async void InvalidateDeviceStatus()
        {
            Debug.Print($"[{nameof(Device)}.{nameof(InvalidateDeviceStatus)}]");

            DeviceStatusFlags flags = DeviceStatusFlags.DEVICE_READY;

            // TODO: Check device count changes

            // Invalidate device count
            if (MidiOutputDevices.Count == 0)
                flags |= DeviceStatusFlags.DEVICE_NO_MIDI_OUTPUT_DEVICES;

            if (MidiInputDevices.Count == 0)
                flags |= DeviceStatusFlags.DEVICE_NO_MIDI_INPUT_DEVICES;

            // Invalidate device selection
            if (MidiOutputDevice == null)
                flags |= DeviceStatusFlags.DEVICE_NO_MIDI_OUTPUT_SELECTED;

            if (MidiInputDevice == null)
                flags |= DeviceStatusFlags.DEVICE_NO_MIDI_INPUT_SELECTED;

            // Invalidate initialization
            if (flags != DeviceStatusFlags.DEVICE_READY)
                flags |= DeviceStatusFlags.DEVICE_NO_MIDI_SETUP;

            // Invalidate connection
            if(flags != DeviceStatusFlags.DEVICE_READY)
            {
                if (IsConnected)
                    flags |= DeviceStatusFlags.DEVICE_CONNECTION_LOST;

                flags |= DeviceStatusFlags.DEVICE_NOT_CONNECTED;

                IsConnected = false;
            }
            else
            {
                bool connectionLost = IsConnected;

                await ConnectionValidation();

                if (IsConnected)
                {
                    flags = DeviceStatusFlags.DEVICE_READY;

                    // Connection is valid, store the configuration
                    Config<IntegraConfiguration>.Set.MidiOutputDeviceID = MidiOutputDevice.ID;
                    Config<IntegraConfiguration>.Set.MidiInputDeviceID = MidiInputDevice.ID;
                }
                else
                {
                    if (connectionLost)
                        flags |= DeviceStatusFlags.DEVICE_CONNECTION_LOST;

                    flags |= DeviceStatusFlags.DEVICE_NOT_CONNECTED;

                    // Connection is invalid, reset the configuration
                    Config<IntegraConfiguration>.Set.MidiOutputDeviceID = -1;
                    Config<IntegraConfiguration>.Set.MidiInputDeviceID = -1;
                }
            }

            if (Flags != flags)
            {
                Flags = flags;
                StatusChanged?.Invoke(this, new IntegraStatusEventArgs(Flags));
            }
        }

        /// <summary>
        /// Validates the connection to the INTEGRA-7 by sending an identity request.
        /// </summary>
        /// <returns>An awaitable <see cref="Task{TResult}"/> containing true if the INTEGRA-7 responded the request.</returns>
        private async Task<bool> ConnectionValidation()
        {
            IsConnected = false;

            Task<bool> connectionValidation = new Task<bool>(() =>
            {
                int connectionTimeOut = 0;

                // Report the initialization message
                ReportProgress(new StatusMessage("Validating Connection", "Please wait...", 0, "Connecting"));

                // Send the identity request
                MidiOutputDevice.Send(new UniversalNonRealTimeMessage(new byte[] { 0xF0, 0x7E, 0x7F, 0x06, 0x01, 0xF7 }));

                while (!IsConnected)
                {
                    Thread.Sleep(DEVICE_LATENCY);
                    connectionTimeOut += DEVICE_LATENCY;

                    // Report the progress message
                    ReportProgress(new StatusMessage("Validating Connection", "Please wait...", 100d / DEVICE_CONNECTION_TIMEOUT * connectionTimeOut, "Connecting"));

                    if (connectionTimeOut > DEVICE_CONNECTION_TIMEOUT)
                        return false;
                }

                return true;

            }, TaskCreationOptions.LongRunning);

            // Start the task using the task manager to keep track of running tasks
            StartTask(connectionValidation);
            
            await connectionValidation;

            // Report the finalization message
            if (IsConnected)
            {
                ReportProgress(new StatusMessage("Validating Connection", "Connection successful", 100, "Done"));
            }
            else
            {
                ReportProgress(new StatusMessage("Validating Connection", "Connection timeout", 100d, "Connection Error"));
            }

            return IsConnected;
        }

        /// <summary>
        /// Request initialization for an INTEGRA-7 data structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataStructure"></param>
        internal async void Initialize<T>(IntegraBase<T> dataStructure) where T : IntegraBase<T>
        {
            // Ensure the INTEGRA-7 is connected before starting initialization
            while (!_IsConnected)
            {
                //await Task.Delay(DEVICE_LATENCY);
                Thread.Sleep(DEVICE_CONNECTION_TIMEOUT);
            }

            Task<bool> initialization = new Task<bool>(() =>
            {
                // Hookup the system exclusive event handler to the INTEGRA-7 data structure
                MidiInputDevice.SystemExclusiveReceived += dataStructure.SystemExclusiveReceived;

                // Send all requests contained inside the data structure
                for (int i = 0; i < dataStructure.Requests.Count; i++)
                {
                    lock (MidiOutputDevice)
                    {
                        MidiOutputDevice.Send(new SystemExclusiveMessage(new IntegraSystemExclusive(dataStructure.Address, dataStructure.Requests[i])));
                        Thread.Sleep(DEVICE_LATENCY);
                    }
                }

                while (!dataStructure.IsInitialized)
                {
                    // Allow the data struture to report progress
                    Thread.Sleep(DEVICE_LATENCY);
                }

                return true;
            });

            // Start the task using the task manager to keep track of running tasks
            StartTask(initialization);

            await initialization;
        }

        /// <summary>
        /// Adds the provided <paramref name="task"/> to the task list and starts the <see cref="TaskManager"/> if not already running.
        /// </summary>
        /// <param name="task">The <see cref="Task"/> to start.</param>
        private async void StartTask(Task task)
        {
            lock (_Tasks)
                _Tasks.Add(task);

            task.Start();

            if (!_IsTaskRunning)
                await TaskManager();
        }

        /// <summary>
        /// Manages any running tasks and raises the <see cref="OperationStart"/> event upon invokation and the <see cref="OperationComplete"/> event when no tasks left.
        /// </summary>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        private async Task TaskManager()
        {
            lock (_Tasks)
                _IsTaskRunning = true;

            // Report the initialization message
            _UIContext.Send(o => OperationStart?.Invoke(this, new IntegraOperationEventArgs(Status)), null);

            while (_Tasks.Any())
            {
                Task task = await Task.WhenAny(_Tasks.ToArray());

                lock (_Tasks)
                    _Tasks.Remove(task);

                // Let UI update
                await Task.Delay(DEVICE_LATENCY);
            }

            lock (_Tasks)
                _IsTaskRunning = false;

            // Report the finalization message
            _UIContext.Send(o => OperationComplete?.Invoke(this, new IntegraOperationEventArgs(Status)), null);
        }
 
        /// <summary>
        /// Reports task progress to the UI.
        /// </summary>
        /// <param name="progress">A <see cref="StatusMessage"/> containing the progress to report.</param>
        /// <remarks><i>Only effective if a task is started using the <see cref="StartTask(Task)"/> method and the <see cref="TaskManager"/> is running.</i></remarks>
        internal void ReportProgress(StatusMessage progress)
        {
            Status.Update(progress);

            _UIContext.Send(o => OperationProgress?.Invoke(this, new IntegraOperationEventArgs(Status)), null);
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

    public class StatusMessage
    {
        public StatusMessage(string action, string message, double progress, string status)
        {
            Action = action;
            Message = message;
            Progress = progress;
            Text = status;
        }

        public StatusMessage(double progress) : this(null, null, progress, null) { }
        public StatusMessage(double progress, string status) : this(null, null, progress, status) { }

        public string Action { get; internal set; }
        public string Message { get; internal set; }
        public double Progress { get; internal set; }
        public string Text { get; internal set; }
    }

   
    /// <summary>
    /// Extends a <see cref="DeviceStatusFlags"/> enumeration with additional operator functionality for addition, removal and comparison.
    /// </summary>
    public class DeviceStatus : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Stores the device status flags.
        /// </summary>
        private static DeviceStatusFlags _Flags;

        private StatusMessage _StatusMessage;

        #endregion

        #region Constructor

        /// <summary>
        /// Private constructor used for operator overloading.
        /// </summary>
        internal DeviceStatus(DeviceStatusFlags flags)
        {
            _StatusMessage = new StatusMessage(string.Empty, string.Empty, 0, "Ready");
            Flags = flags;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the device status flags.
        /// </summary>
        public DeviceStatusFlags Flags
        {
            get { return _Flags; }
            internal set 
            {
                if (_Flags != value)
                {
                    _Flags = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Action
        {
            get { return _StatusMessage.Action; }
            internal set 
            {
                if (_StatusMessage.Action != value)
                {
                    _StatusMessage.Action = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Message
        {
            get { return _StatusMessage.Message; }
            internal set 
            {
                if (_StatusMessage.Message != value)
                {
                    _StatusMessage.Message = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double Progress 
        { 
            get { return _StatusMessage.Progress; }
            internal set
            {
                if (_StatusMessage.Progress != value)
                {
                    _StatusMessage.Progress = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public string Text
        {
            get { return _StatusMessage.Text; }
            internal set
            {
                if (_StatusMessage.Text != value)
                {
                    _StatusMessage.Text = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Methods

        internal void Update() { Init(string.Empty, string.Empty, 0, string.Empty); }

        internal void Update(StatusMessage deviceMessage)
        {
            if (deviceMessage.Action != null)
                Action = deviceMessage.Action;

            if (deviceMessage.Message != null)
                Message = deviceMessage.Message;

            Progress = deviceMessage.Progress;

            if (deviceMessage.Text != null)
                Text = deviceMessage.Text;
        }

        internal void Init(string action, string message, double progress, string text)
        {
            Action = action;
            Message = message;
            Progress = progress;
            Text = text;
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
