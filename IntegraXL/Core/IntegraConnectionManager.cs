using IntegraXL.Interfaces;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace IntegraXL.Core
{
    public static class IntegraConnectionManager
    {
       
            #region Constants

            /// <summary>
            /// Defines the time in milliseconds to wait for a respons before the connection is considered lost.
            /// </summary>
            private const int DEVICE_CONNECTION_TIMEOUT = 1000;

            #endregion

            #region Fields

            /// <summary>
            /// Stores a references to all connections.
            /// </summary>
            private static ConcurrentDictionary<int, IntegraConnection> _Connections = new();

            /// <summary>
            /// Stores the device ID for filtering the universal non realtime messages.
            /// </summary>
            private static byte _DeviceID;

            /// <summary>
            /// Tracks wheter the connection is valid.
            /// </summary>
            private static bool _IsConnected = false;

            #endregion

            public static async Task<IntegraConnection> CreateConnection(IMIDIOutputDevice midiOutputDevice, IMIDIInputDevice midiInputDevice, byte deviceID = 16)
            {
                _DeviceID = deviceID;

                if (_Connections.TryGetValue(_DeviceID, out IntegraConnection existingConnection))
                {
                    return existingConnection;
                }

                IntegraConnection connection = new IntegraConnection(midiOutputDevice, midiInputDevice, _DeviceID);

                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    throw new IntegraException($"[{nameof(IntegraConnectionManager)}.{nameof(CreateConnection)}]\nError opening the MIDI output device.", e);
                }

                if (await InvalidateConnection(connection))
                {

                    if (_Connections.TryAdd(connection.ID, connection))
                    {
                        return connection;
                    }
                    else
                    {
                        throw new IntegraException($"[{nameof(IntegraConnectionManager)}.{nameof(CreateConnection)}]\nError adding connection.");
                    }
                }
                else
                {
                    try
                    {
                        connection.Close();
                    }
                    catch (Exception e)
                    {
                        throw new IntegraException($"[{nameof(IntegraConnectionManager)}.{nameof(CreateConnection)}]\nError closing the MIDI devices.", e);
                    }


                    return null;
                }
            }

            /// <summary>
            /// Invalidates the specified connection.
            /// </summary>
            /// <param name="connection">The connection to invalidate.</param>
            /// <returns>An awaitable task that returns true if the connection is valid.</returns>
            private static async Task<bool> InvalidateConnection(IntegraConnection connection)
            {
                Debug.Print($"{nameof(IntegraConnectionManager)}.{nameof(InvalidateConnection)} #{_DeviceID}");

                int connectionTime = 0;
                int connectionResolution = DEVICE_CONNECTION_TIMEOUT / 100;

                connection.IsOpen = false;

                await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        connection.SendSystemExclusiveMessage(new byte[] { 0xF0, 0x7E, 0x7F, 0x06, 0x01, 0xF7 });
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                    while (!connection.IsOpen)
                    {
                        connectionTime += connectionResolution;

                        int progress = 100 - (DEVICE_CONNECTION_TIMEOUT - connectionTime) / connectionResolution;

                        // TODO: Report progress
                        Thread.Sleep(connectionResolution);

                        if (connectionTime >= DEVICE_CONNECTION_TIMEOUT)
                        {
                            return false;
                        }
                    }

                    return true;
                });

                return connection.IsOpen;
            }

            /// <summary>
            /// Handles received MIDI long messages by filtering MIDI identity responses for the current device ID.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            /// <remarks><i>When an identity response is received for the current device ID the connection is considered valid.</i></remarks>
            private static void SystemExclusiveReceived(object sender, EventArgs e)
            {
                //Debug.Print($"{nameof(DeviceConnectionManager)}.{nameof(UniversalNonRealtimeReceived)} {string.Join(" ", (e.Data).Select(x => string.Format("{0:X2}", x)))}");
                _IsConnected = true;
                
                //// TODO: Replace endless loop
                //if (e.Data.Length == 15)
                //if (e.Data[0] == 0xF0) // System exclusive status
                //if (e.Data[1] == 0x7E) // Universal non-realtime ID
                //if (e.Data[2] == _DeviceID) // Device ID
                //if (e.Data[3] == 0x06) // General information
                //if (e.Data[4] == 0x02) // Identity reply
                //if (e.Data[5] == 0x41) // Roland ID
                //if (e.Data[6] == 0x64 && e.Data[7] == 0x02) // Device family code
                //if (e.Data[8] == 0x00 && e.Data[9] == 0x00) // Device family number code
                //{ 
                //    _IsConnected = true;
                //}
            }
        
    }
}
