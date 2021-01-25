using Integra.Core;
using Integra.Core.Interfaces;
using MidiXL;
using System;

namespace Integra.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 MIDI data structure.
    /// </summary>
    /// <remarks><i>Address 01 00 00 00</i></remarks>
    public class StudioSetMidi : IntegraBase<StudioSetMidi>, IIntegraPartial
    {
        #region Fields

        [Offset(0x0000)] private bool _PhaseLock;

        #endregion

        private IntegraParts _Part;

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="StudioSetMidi"/> instance.
        /// </summary>
        /// <remarks><i>Default constructor for dynamic instance creation.</i></remarks>
        public StudioSetMidi() { }

        public StudioSetMidi(IntegraAddress address, IntegraRequest request) : base(address, request)
        {
            _Part = (IntegraParts)((address & 0x00000F00) >> 8);
            Initialize();
        }

        #endregion

        #region Properties

        public IntegraParts Part
        {
            get { return _Part; }
        }

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

        #region Overrides

        internal override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            if (syx.Address == Address)
            {

                if (!IsInitialized)
                {
                    if (Initialize(syx.Data))
                    {
                        Device.Instance.ReportProgress(this, new StatusMessage($"Initializing Studio Set MIDI {_Part}", "Initialized", 100, "Done"));
                    }
                    else
                    {
                        Device.Instance.ReportProgress(this, new StatusMessage($"Initializing Studio Set MIDI {_Part}", "Please wait...", 100, "Initializing"));
                    }
                }
                else
                {
                    InitializeField(syx);
                }
            }
        }

        protected override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                DebugPrint();
                _Part = (IntegraParts)((Address & 0x00000F00) >> 8);

                _PhaseLock = Convert.ToBoolean(data[0x00]);
                NotifyPropertyChanged(string.Empty);
                IsInitialized = true;
            }

            return IsInitialized;
        }

        #endregion
    }

}
