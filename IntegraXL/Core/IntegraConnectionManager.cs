using IntegraXL.Interfaces;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace IntegraXL.Core
{
    /// <summary>
    /// Manages creation and tracking of all INTEGRA-7 connections
    /// </summary>
    /// <remarks>
    /// <b>IMPORTANT</b><br/>
    /// <i>The INTEGRA-7 doesn't transmit device ID changes, do not change the device ID on the physical device when running.</i><br/>
    /// <i>This results in unresponsive models with corrupt data.</i>
    /// </remarks>
    public static class IntegraConnectionManager
    {
        #region Fields

        /// <summary>
        /// Stores a references to all connections.
        /// </summary>
        private static ConcurrentDictionary<int, IntegraConnection> _Connections = new();

        #endregion

        #region Properties

        /// <summary>
        /// Provides a list of all available connections.
        /// </summary>
        /// <remarks><i>Connection property changes are noticed.</i></remarks>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public static readonly ObservableCollection<IntegraConnection> Connections = new();

        #endregion

        #region Methods

        /// <summary>
        /// Gets the connection for the specified device ID.
        /// </summary>
        /// <param name="deviceID">The <i>zero based</i> INTEGRA-7 device ID.</param>
        /// <returns>The existing connection or null.</returns>
        /// <exception cref="IntegraException"></exception>
        public static IntegraConnection? GetConnection(byte deviceID)
        {
            if (deviceID < 16 || deviceID > 31)
                throw new IntegraException($"[{nameof(IntegraConnectionManager)}]\nDevice ID {deviceID} out of range. [16..31]");

            if (_Connections.TryGetValue(deviceID, out IntegraConnection? existingConnection))
            {
                return existingConnection;
            }

            return null;
        }

        /// <summary>
        /// Creates a new managed connection.
        /// </summary>
        /// <param name="deviceID">The <i>zero based</i> INTEGRA-7 device ID.</param>
        /// <param name="midiOutputDevice">The MIDI output device to associate with the connection.</param>
        /// <param name="midiInputDevice">The MIDI input device to associate with the connection.</param>
        /// <param name="progress">The optional progress report method.</param>
        /// <returns>The newly created connection.</returns>
        /// <exception cref="IntegraException"></exception>
        public static IntegraConnection CreateConnection(byte deviceID, IMIDIOutputDevice midiOutputDevice, IMIDIInputDevice midiInputDevice, IProgress<int>? progress = null)
        {
            if(deviceID < 16 || deviceID > 31)
                throw new IntegraException($"[{nameof(IntegraConnectionManager)}]\nDevice ID {deviceID} out of range. [16..31]");

            if (_Connections.ContainsKey(deviceID))
                throw new IntegraException($"[{nameof(IntegraConnectionManager)}]\nA connection for #{deviceID} already exists.");

            IntegraConnection connection = new (deviceID, midiOutputDevice, midiInputDevice, progress);

            if (_Connections.TryAdd(deviceID, connection))
            {
                Debug.Print($"[{nameof(IntegraConnectionManager)}.{nameof(CreateConnection)}] #{deviceID} | SX: {midiOutputDevice.Name} | RX: {midiInputDevice.Name}");

                // Raises property changed event for the property in the static collection
                connection.ConnectionChanged += (s, e) => Connections.First(x => x == s).NotifyPropertyChanged(string.Empty);
                
                Connections.Clear();

                foreach(var item in _Connections.Values.OrderBy(x => x.ID))
                {
                    Connections.Add(item);
                }

                return connection;
            }

            throw new IntegraException($"[{nameof(IntegraConnectionManager)}]\nUnable to add connection #{deviceID}.");
        }

        #endregion
    }
}
