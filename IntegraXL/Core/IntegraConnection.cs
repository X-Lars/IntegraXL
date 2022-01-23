using IntegraXL.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{

    /// <summary>
    /// Filters & delegates incoming and outgoing MIDI messages to and from the associated INTEGRA-7.
    /// </summary>
    public class IntegraConnection
    {
        #region Fields

        /// <summary>
        /// Reference to the MIDI output device associated with the connection.
        /// </summary>
        private IMIDIOutputDevice _MidiOutputDevice;

        /// <summary>
        /// Reference to the MIDI input device associated with the connection
        /// </summary>
        private IMIDIInputDevice _MidiInputDevice;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when a system exclusive message is received.
        /// </summary>
        internal event EventHandler<IntegraSystemExclusiveEventArgs>? SystemExclusiveReceived;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes a new INTEGRA-7 connection.
        /// </summary>
        /// <param name="midiOutputDevice">The MIDI output device associated with the connection.</param>
        /// <param name="midiInputDevice">The MIDI input device associated with the connection.</param>
        /// <param name="deviceID">The <i>zero based</i> device ID.</param>
        /// <exception cref="IntegraException">When the device ID is out of range.</exception>
        internal IntegraConnection(IMIDIOutputDevice midiOutputDevice, IMIDIInputDevice midiInputDevice, byte deviceID)
        {
            if (deviceID < 16 || deviceID > 31)
                throw new IntegraException($"[{nameof(IntegraConnection)}]\nDevice ID out of range [16..31].");

            ID = deviceID;

            _MidiOutputDevice = midiOutputDevice;
            _MidiInputDevice = midiInputDevice;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the connection ID which equals the associated device ID.
        /// </summary>
        public byte ID { get; }

        /// <summary>
        /// Get or sets wheter the connection is open.
        /// </summary>
        internal bool IsOpen { get; set; } = false;

        #endregion

        #region Methods

        /// <summary>
        /// Opens the connection for MIDI input and output.
        /// </summary>
        /// <exception cref="IntegraException"></exception>
        internal void Open()
        {
            if (IsOpen) return;

            try
            {
                _MidiInputDevice.LongMessageReceived += LongMessageReceived;

                _MidiOutputDevice.Open();
                _MidiInputDevice.Open();
                _MidiInputDevice.Start();
            }
            catch (Exception ex)
            {
                throw new IntegraException($"[{nameof(IntegraConnection)}.{nameof(Open)}]\nError opening connection #{ID}", ex);
            }
        }

        /// <summary>
        /// Closes the connection for MIDI input and output.
        /// </summary>
        /// <exception cref="IntegraException"></exception>
        internal void Close()
        {
            if (!IsOpen) return;

            try
            {
                _MidiInputDevice.LongMessageReceived -= LongMessageReceived;

                _MidiInputDevice.Close();
                _MidiOutputDevice.Close();
            }
            catch (Exception ex)
            {

                throw new IntegraException($"[{nameof(IntegraConnection)}.{nameof(Open)}]\nError closing connection #{ID}", ex);
            }
        }

        /// <summary>
        /// Sends a device independent system exclusive message over the connection.
        /// </summary>
        /// <param name="data">The raw system exclusive message.</param>
        /// <exception cref="IntegraException"></exception>
        internal void SendSystemExclusiveMessage(byte[] syx)
        {
            try
            {
                _MidiOutputDevice.SendLongMessage(syx);
                Debug.Print($"SX {string.Join(" ", (syx).Select(x => string.Format("{0:X2}", x)))}");
            }
            catch(Exception ex)
            {
                throw new IntegraException($"[{nameof(IntegraConnection)}.{nameof(SendSystemExclusiveMessage)}]\nError sending system exclusive over connection #{ID}", ex);
            }
        }

        /// <summary>
        /// Delegates and filters received long MIDI messages for the assoc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="IntegraException"></exception>
        private void LongMessageReceived(object? sender, LongMessageEventArgs e)
        {
            // Check system exclusive status byte
            if (e.Data[0] != 0xF0)
                throw new IntegraException($"[{nameof(IntegraConnection)}.{nameof(LongMessageReceived)}]\nInvalid exclusive status byte.");

            // Switch system exclusive message type
            switch (e.Data[1])
            {
                // System Exclusive Message
                case IntegraConstants.MIDI_MANUFACTURER_ID:

                    //try
                    //{

                        IntegraSystemExclusive systemExclusive = new IntegraSystemExclusive(e.Data);


                        if (systemExclusive.DeviceID == ID)
                        {
                            Debug.Print($"RX {systemExclusive.ToString()}");
                            SystemExclusiveReceived?.Invoke(this, new IntegraSystemExclusiveEventArgs(systemExclusive));
                        }
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw new IntegraException($"[{nameof(IntegraConnection)}.{nameof(LongMessageReceived)}]\nInvalid system exclusive message.", ex);
                    //}

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
                                IsOpen = true;
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

        #endregion
    }
}
