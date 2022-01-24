using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x18001000, 0x00001000)]
    public sealed class StudioSetMidis : IntegraPartialCollection<StudioSetMidi>
    {
        private StudioSetMidis(Integra device) : base(device) { }
    }

    /// <summary>
    /// Defines the INTEGRA-7 studio set MIDI model.
    /// </summary>
    [Integra(0x18001000, 0x00000001)]
    public class StudioSetMidi : IntegraPartial<StudioSetMidi>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private bool _PhaseLock;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of INTEGRA-7 studio set MIDI model.
        /// </summary>
        private StudioSetMidi(Integra device, Parts part) : base(device, part) { }
        

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

        protected override bool Initialize(byte[] data)
        {
            _PhaseLock = Convert.ToBoolean(data[0]);
            NotifyPropertyChanged(string.Empty);
            return IsInitialized = true;
        }

        #endregion
    }
}
