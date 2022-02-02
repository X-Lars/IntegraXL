using IntegraXL.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace IntegraXL.Core
{
    /// <summary>
    /// Defines the possible <see cref="IntegraConnection"/> status values.
    /// </summary>
    public enum ConnectionStatus
    {
        /// <summary> Connection ready for data transmission. </summary>
        Connected = 0,

        /// <summary> Connection not ready. </summary>
        Disconnected = 1,

        /// <summary> Connection is validating. </summary>
        Validating = 2,

        /// <summary> Connection error. </summary>
        Error = 32,

        /// <summary> Connection not validated, initial state. </summary>
        Unknown = 64
    }

    /// <summary>
    /// Filters & delegates incoming and outgoing MIDI messages to and from the associated INTEGRA-7.
    /// </summary>
    public class IntegraConnection : INotifyPropertyChanged
    {
        #region Constants

        /// <summary>
        /// Defines the time to wait for an identity reply in milliseconds before the connection is timed out.
        /// </summary>
        private const int DEVICE_CONNECTION_TIMEOUT = 2000;

        #endregion

        #region Fields

        /// <summary>
        /// Tracks wheter the MIDI input and output devices are open.
        /// </summary>
        private bool _IsOpen;

        /// <summary>
        /// Reference to the MIDI output device associated with the connection.
        /// </summary>
        private IMIDIOutputDevice _MidiOutputDevice;

        /// <summary>
        /// Reference to the MIDI input device associated with the connection
        /// </summary>
        private IMIDIInputDevice _MidiInputDevice;

        /// <summary>
        /// Stores the connection status.
        /// </summary>
        private ConnectionStatus _ConnectionStatus;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when a system exclusive message is received.
        /// </summary>
        internal event EventHandler<IntegraSystemExclusiveEventArgs>? SystemExclusiveReceived;

        /// <summary>
        /// Event raised when the connection status is changed.
        /// </summary>
        public event EventHandler<IntegraConnectionStatusEventArgs>? ConnectionChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes a new INTEGRA-7 connection.
        /// </summary>
        /// <param name="midiOutputDevice">The MIDI output device to associate with the connection.</param>
        /// <param name="midiInputDevice">The MIDI input device to associate with the connection.</param>
        /// <param name="deviceID">The <i>zero based</i> device ID.</param>
        /// <exception cref="IntegraException">When the device ID is out of range.</exception>
        internal IntegraConnection(byte deviceID, IMIDIOutputDevice midiOutputDevice, IMIDIInputDevice midiInputDevice)
        {
            if (midiOutputDevice == null)
                throw new IntegraException($"[{nameof(IntegraConnection)}]\nMIDI Output device = NULL");

            if (midiInputDevice == null)
                throw new IntegraException($"[{nameof(IntegraConnection)}]\nMIDI Input device = NULL");

            ID = deviceID;

            _MidiOutputDevice = midiOutputDevice;
            _MidiInputDevice  = midiInputDevice;

            _ConnectionStatus = ConnectionStatus.Unknown;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the connection ID which equals the associated device ID.
        /// </summary>
        /// <remarks><i>The ID is zero based.</i></remarks>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public byte ID { get; }

        /// <summary>
        /// Gets wheter the connection is valid.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public bool IsConnected
        {
            get => _ConnectionStatus == ConnectionStatus.Connected;
        }

        /// <summary>
        /// Gets the connection status.
        /// </summary>
        /// <remarks><i>Raises the <see cref="ConnectionChanged"/> event.</i></remarks>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public ConnectionStatus Status
        {
            get => _ConnectionStatus;
            private set
            {
                if(_ConnectionStatus != value)
                {
                    Debug.Print($"[{nameof(IntegraConnection)} #{ID}] {nameof(Status)} = {value}");

                    _ConnectionStatus = value;
                    
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(IsConnected));

                    ConnectionChanged?.Invoke(this, new IntegraConnectionStatusEventArgs(value));
                }
            }
        }

        /// <summary>
        /// Gets the MIDI output device associated with the connection.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public IMIDIOutputDevice MidiOutputDevice
        {
            get => _MidiOutputDevice;
            set => throw new NotImplementedException($"Use {nameof(IntegraConnection)}.{nameof(SetMidiOutput)} or {nameof(IntegraConnection)}.{nameof(Change)} instead.");
        }

        /// <summary>
        /// Gets the MIDI input device associated with the connection.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public IMIDIInputDevice MidiInputDevice
        {
            get => _MidiInputDevice;
            set => throw new NotImplementedException($"Use {nameof(IntegraConnection)}.{nameof(SetMidiInput)} or {nameof(IntegraConnection)}.{nameof(Change)} instead.");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens the connection's MIDI input and output devices.
        /// </summary>
        /// <exception cref="IntegraException"></exception>
        internal void Open()
        {
            if (_IsOpen) return;

            try
            {
                _MidiOutputDevice.Open();
                _MidiInputDevice.Open();
                _MidiInputDevice.LongMessageReceived += LongMessageReceived;
                _MidiInputDevice.Start();
            }
            catch (Exception ex)
            {
                throw new IntegraException($"[{nameof(IntegraConnection)}.{nameof(Open)}]\nError opening connection #{ID}", ex);
            }

            _IsOpen = true;
        }

        /// <summary>
        /// Closes the connection's MIDI input and output devices.
        /// </summary>
        /// <exception cref="IntegraException"></exception>
        internal void Close()
        {
            if (!_IsOpen) return;

            Status = ConnectionStatus.Disconnected;

            try
            {
                _MidiInputDevice.Close();
                _MidiInputDevice.LongMessageReceived -= LongMessageReceived;
                _MidiOutputDevice.Close();
            }
            catch (Exception ex)
            {

                throw new IntegraException($"[{nameof(IntegraConnection)}.{nameof(Close)}]\nError closing connection #{ID}", ex);
            }

            _IsOpen = false;
        }

        /// <summary>
        /// Sends a device ID independent system exclusive message over the connection.
        /// </summary>
        /// <param name="syx">The raw system exclusive message.</param>
        /// <exception cref="IntegraException"></exception>
        internal void SendSystemExclusiveMessage(byte[] syx)
        {
            try
            {
                Debug.Print($"SX {string.Join(" ", syx.Select(x => string.Format("{0:X2}", x)))}");
                _MidiOutputDevice.SendLongMessage(syx);
            }
            catch (Exception ex)
            {
                throw new IntegraException($"[{nameof(IntegraConnection)}.{nameof(SendSystemExclusiveMessage)}]\nError sending system exclusive.\nConnection #{ID}", ex);
            }
        }

        /// <summary>
        /// Delegates and filters received long MIDI messages by the associated device ID.
        /// </summary>
        /// <param name="sender">The MIDI input device that raised the event.</param>
        /// <param name="e">The event data.</param>
        /// <exception cref="IntegraException"></exception>
        private void LongMessageReceived(object? sender, LongMessageEventArgs e)
        {
            // Check system exclusive status byte
            if (e.Data[0] != 0xF0)
            {
                Debug.Print($"Invalid system exclusive status byte.");
                return;
            }

            // Switch system exclusive message type
            switch (e.Data[1])
            {
                // System Exclusive Message
                case IntegraConstants.MIDI_MANUFACTURER_ID:

                    try
                    {
                        IntegraSystemExclusive systemExclusive = new (e.Data);

                        if (systemExclusive.DeviceID == ID)
                        {
                            Debug.Print($"RX {systemExclusive}");
                            SystemExclusiveReceived?.Invoke(this, new IntegraSystemExclusiveEventArgs(systemExclusive));
                        }
                        else
                        {
                            Debug.Print($"SKIP {systemExclusive}");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IntegraException($"[{nameof(IntegraConnection)}.{nameof(LongMessageReceived)}]\nInvalid system exclusive message.", ex);
                    }

                    break;

                // Universal Non Real Time Message
                case 0x7E:

                    // Check connection ID
                    if (e.Data[2] != ID)
                        return;

                    // Check general information, identity reply
                    if (e.Data[3] == 0x06 && e.Data[4] == 0x02)
                    {
                        // Check Roland manufacturer ID
                        if (e.Data[5] == 0x41)

                            // Check INTEGRA-7 device family code
                            if (e.Data[6] == 0x64 && e.Data[7] == 0x02)
                            {
                                Status = ConnectionStatus.Connected;
                            }
                    }
                    else
                    {
                        Debug.Print($"[{nameof(IntegraConnection)}.{nameof(LongMessageReceived)}]\nUnimplemented non real time message received.");
                    }

                    break;

                // Universal Real Time Message
                case 0x7F:
                    Debug.Print($"[{nameof(IntegraConnection)}.{nameof(LongMessageReceived)}]\nUnimplemented real time message received.");
                    break;

                default:
                    Debug.Print($"[{nameof(IntegraConnection)}.{nameof(LongMessageReceived)}]\nUndefined long message received.\n{e.Data[1].ToString("X2")}");
                    break;
            }
        }

        /// <summary>
        /// Changes the MIDI devices associateded with the connection and validates the modified connection.
        /// </summary>
        /// <param name="midiOutputDevice">The MIDI output device to associate with the connection.</param>
        /// <param name="midiInputDevice">The MIDI input device to associate with the connection.</param>
        /// <returns>An awaitable task that returns the connection status.</returns>
        public Task<ConnectionStatus> Change(IMIDIOutputDevice midiOutputDevice, IMIDIInputDevice midiInputDevice)
        {
            if (midiOutputDevice == null)
                throw new IntegraException($"[{nameof(IntegraConnection)}]\nMIDI Output device = NULL");

            if (midiInputDevice == null)
                throw new IntegraException($"[{nameof(IntegraConnection)}]\nMIDI Input device = NULL");

            Close();

            _MidiOutputDevice = midiOutputDevice;
            _MidiInputDevice  = midiInputDevice;

            Open();

            NotifyPropertyChanged(nameof(MidiOutputDevice));
            NotifyPropertyChanged(nameof(MidiInputDevice));

            return Invalidate();
        }

        /// <summary>
        /// Sets the MIDI output device associateded with the connection and validates the modified connection.
        /// </summary>
        /// <param name="midiOutputDevice">The MIDI output device to associate with the connection.</param>
        /// <returns>An awaitable task that returns the connection status.</returns>
        public Task<ConnectionStatus> SetMidiOutput(IMIDIOutputDevice midiOutputDevice)
        {
            if (midiOutputDevice == null)
                throw new IntegraException($"[{nameof(IntegraConnection)}]\nMIDI Output device = NULL");

            Close();

            _MidiOutputDevice = midiOutputDevice;

            Open();

            NotifyPropertyChanged(nameof(MidiOutputDevice));
            return Invalidate();
        }

        /// <summary>
        /// Sets the MIDI input device associateded with the connection and validates the modified connection.
        /// </summary>
        /// <param name="midiInputDevice">The MIDI input device to associate with the connection.</param>
        /// <returns>An awaitable task that returns the connection status.</returns>
        public Task<ConnectionStatus> SetMidiInput(IMIDIInputDevice midiInputDevice)
        {
            if (midiInputDevice == null)
                throw new IntegraException($"[{nameof(IntegraConnection)}]\nMIDI Input device = NULL");

            Close();

            _MidiInputDevice = midiInputDevice;

            Open();

            NotifyPropertyChanged(nameof(MidiInputDevice));

            return Invalidate();
        }

        /// <summary>
        /// Invalidates the connection by sending an identity request.
        /// </summary>
        /// <returns>An awaitable task that returns the connection status.</returns>
        public Task<ConnectionStatus> Invalidate()
        {
            Status = ConnectionStatus.Validating;

            int connectionTime = 0;
            int connectionResolution = DEVICE_CONNECTION_TIMEOUT / 100;

            Open();

            return Task<ConnectionStatus>.Factory.StartNew(() =>
            {
                try
                {
                    SendSystemExclusiveMessage(new byte[] { 0xF0, 0x7E, 0x7F, 0x06, 0x01, 0xF7 });
                }
                catch (Exception ex)
                {
                    return Status = ConnectionStatus.Error;
                }

                while (!IsConnected)
                {
                    connectionTime += connectionResolution;

                    int progress = 100 - (DEVICE_CONNECTION_TIMEOUT - connectionTime) / connectionResolution;

                    //TODO: Report progress
                    Thread.Sleep(connectionResolution);

                    if (connectionTime >= DEVICE_CONNECTION_TIMEOUT)
                    {
                        return Status = ConnectionStatus.Disconnected;
                    }
                }

                return Status = ConnectionStatus.Connected;
            });
        }

        #endregion

        #region Interfaces: INotifyPropertyChanged

        /// <summary>
        /// Event raised when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property, defaults to the caller member name if not specified.</param>
        public virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
