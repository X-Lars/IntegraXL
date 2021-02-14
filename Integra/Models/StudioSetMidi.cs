using Integra.Core;
using Integra.Core.Interfaces;
using MidiXL;
using System;
using System.ComponentModel;

namespace Integra.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 MIDI data structure.
    /// </summary>
    /// <remarks><i>18 00 10 00 - 18 00 1F 00</i></remarks>
    public class StudioSetMidi : IntegraBase<StudioSetMidi>, IIntegraPartial, INotifyPropertyChanged, IIntegraStudioSetPartial
    {
        #region Fields

        [Offset(0x0000)] private bool _PhaseLock;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new disconnected <see cref="StudioSetMidi"/> instance.
        /// </summary>
        /// <remarks><i>Default constructor for dynamic instance creation.</i></remarks>
        public StudioSetMidi() { }

        /// <summary>
        /// Creates and initializes a new connected <see cref="StudioSetMidi"/> instance.
        /// </summary>
        /// <param name="address">The address of data structure.</param>
        /// <param name="request">The request to initialize the data structure.</param>
        public StudioSetMidi(IntegraAddress address, IntegraRequest request) : base(address, request)
        {
            Part = (IntegraParts)((address & 0x00000F00) >> 8);
        }

        #endregion

        #region Properties

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public bool PhaseLock
        {
            get { return _PhaseLock; }
            set
            {
                _PhaseLock = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region IIntegraPartial
        
        public IntegraParts Part { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Overridden because the length of data is always one and the base skips exact matches when the data structure is already initialized.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event data.</param>
        protected override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            if (syx.Address == Address)
            {

                if (!IsInitialized)
                {
                    if (Initialize(syx.Data))
                    {
                        Device.Instance.ReportProgress(this, new StatusMessage($"Initializing Studio Set MIDI {Part}", "Initialized", 100, "Done"));
                    }
                    else
                    {
                        Device.Instance.ReportProgress(this, new StatusMessage($"Initializing Studio Set MIDI {Part}", "Please wait...", 100, "Initializing"));
                    }
                }
                else
                {
                    InitializeField(syx);
                }
            }
        }

        internal override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                Part = (IntegraParts)((Address & 0x00000F00) >> 8);

                _PhaseLock = Convert.ToBoolean(data[0x00]);
                //NotifyPropertyChanged(string.Empty, false);
                IsInitialized = true;
            }

            return IsInitialized;
        }

        #endregion
    }
}
