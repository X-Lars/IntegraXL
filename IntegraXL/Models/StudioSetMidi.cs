using IntegraXL.Core;
using System.Diagnostics.CodeAnalysis;

namespace IntegraXL.Models
{
    [Integra(0x18001000, 0x00001000)]
    public sealed class StudioSetMidis : IntegraPartialCollection<StudioSetMidi>
    {
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "The class is created by reflection.")]
        private StudioSetMidis(Integra device) : base(device) { }
    }

    /// <summary>
    /// Defines the INTEGRA-7 studio set MIDI model.
    /// </summary>
    [Integra(0x18001000, 0x00000001)]
    public sealed class StudioSetMidi : IntegraPartial<StudioSetMidi>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private bool _PhaseLock;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="StudioSetMidi"/> instance.
        /// </summary>
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "The class is created by reflection.")]
        private StudioSetMidi(Integra device, Parts part) : base(device, part) { }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public bool PhaseLock
        {
            get { return _PhaseLock; }
            set
            {
                if (_PhaseLock != value)
                {
                    _PhaseLock = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Overrides: Model

        internal override bool Initialize(byte[] data)
        {
            _PhaseLock = Convert.ToBoolean(data[0]);
            NotifyPropertyChanged(string.Empty);
            return IsInitialized = true;
        }

        #endregion
    }
}
