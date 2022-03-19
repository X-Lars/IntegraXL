using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 studio set master EQ model.
    /// </summary>
    [Integra(0x18000900, 0x00000007)]
    public class StudioSetMasterEQ : IntegraModel<StudioSetMasterEQ>
    {
        #region Fields

        [Offset(0x0000)] private IntegraLowFrequencies _EQLowFreq;
        [Offset(0x0001)] private byte _EQLowGain;
        [Offset(0x0002)] private IntegraMidFrequencies _EQMidFreq;
        [Offset(0x0003)] private byte _EQMidGain;
        [Offset(0x0004)] private IntegraMidQs _EQMidQ;
        [Offset(0x0005)] private IntegraHighFrequencies _EQHighFreq;
        [Offset(0x0006)] private byte _EQHighGain;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="StudioSetMasterEQ"/> instance.
        /// </summary>
        /// <param name="device">The <see cref="Integra"/> to connect the model.</param>
        private StudioSetMasterEQ(Integra device) : base(device) { }

        #endregion

        #region Properties

        [Offset(0x0000)]
        public IntegraLowFrequencies EQLowFreq
        {
            get => _EQLowFreq;
            set
            {
                if (_EQLowFreq != value)
                {
                    _EQLowFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0001)]
        public int EQLowGain
        {
            get => _EQLowGain.Deserialize(64);
            set
            {
                if (EQLowGain != value)
                {
                    _EQLowGain = value.Clamp(-15, 15).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0002)]
        public IntegraMidFrequencies EQMidFreq
        {
            get => _EQMidFreq;
            set
            {
                if (_EQMidFreq != value)
                {
                    _EQMidFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0003)]
        public int EQMidGain
        {
            get => _EQMidGain.Deserialize(64);
            set
            {
                if (EQMidGain != value)
                {
                    _EQMidGain = value.Clamp(-15, 15).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0004)]
        public IntegraMidQs EQMidQ
        {
            get => _EQMidQ;
            set
            {
                if (_EQMidQ != value)
                {
                    _EQMidQ = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0005)]
        public IntegraHighFrequencies EQHighFreq
        {
            get => _EQHighFreq;
            set
            {
                if (_EQHighFreq != value)
                {
                    _EQHighFreq = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public int EQHighGain
        {
            get => _EQHighGain.Deserialize(64);
            set
            {
                if (EQHighGain != value)
                {
                    _EQHighGain = value.Clamp(-15, 15).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
    }
}
