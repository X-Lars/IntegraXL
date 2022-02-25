using IntegraXL.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegraXL.Models
{
    [Integra(0x18005000, 0x00001000)]
    public sealed class StudioSetPartEQs : IntegraPartialCollection<StudioSetPartEQ>
    {
        private StudioSetPartEQs(Integra device) : base(device) { }
    }

    /// <summary>
    /// Defines the INTEGRA-7 studio set part EQ model.
    /// </summary>
    [Integra(0x18005000, 0x00000008)]
    public class StudioSetPartEQ : IntegraPartial<StudioSetPartEQ>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private bool _EQSwitch;
        [Offset(0x0001)] private IntegraLowFrequencies _EQLowFreq;
        [Offset(0x0002)] private byte _EQLowGain;
        [Offset(0x0003)] private IntegraMidFrequencies _EQMidFreq;
        [Offset(0x0004)] private byte _EQMidGain;
        [Offset(0x0005)] private IntegraMidQs _EQMidQ;
        [Offset(0x0006)] private IntegraHighFrequencies _EQHighFreq;
        [Offset(0x0007)] private byte _EQHighGain;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the INTEGRA-7 studio set part model.
        /// </summary>
        private StudioSetPartEQ(Integra device, Parts part) : base(device, part)  { }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public bool EQSwitch
        {
            get { return _EQSwitch; }
            set
            {
                _EQSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0001)]
        public IntegraLowFrequencies EQLowFreq
        {
            get { return _EQLowFreq; }
            set
            {
                _EQLowFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0002)]
        public int EQLowGain
        {
            get { return _EQLowGain - 15; }
            set
            {
                _EQLowGain = (byte)(value + 15);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0003)]
        public IntegraMidFrequencies EQMidFreq
        {
            get { return _EQMidFreq; }
            set
            {
                _EQMidFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0004)]
        public int EQMidGain
        {
            get { return _EQMidGain - 15; }
            set
            {
                _EQMidGain = (byte)(value + 15);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0005)]
        public IntegraMidQs EQMidQ
        {
            get { return _EQMidQ; }
            set
            {
                _EQMidQ = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public IntegraHighFrequencies EQHighFreq
        {
            get { return _EQHighFreq; }
            set
            {
                _EQHighFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0007)]
        public int EQHighGain
        {
            get { return _EQHighGain -15; }
            set
            {
                _EQHighGain = (byte)(value + 15);
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Enumerations

        public virtual IEnumerable<IntegraLowFrequencies> LowFrequencies
        {
            get { return Enum.GetValues(typeof(IntegraLowFrequencies)).Cast<IntegraLowFrequencies>(); }
        }

        public virtual IEnumerable<IntegraMidFrequencies> MidFrequencies
        {
            get { return Enum.GetValues(typeof(IntegraMidFrequencies)).Cast<IntegraMidFrequencies>(); }
        }

        public virtual IEnumerable<IntegraMidQs> MidQs
        {
            get { return Enum.GetValues(typeof(IntegraMidQs)).Cast<IntegraMidQs>(); }
        }

        public virtual IEnumerable<IntegraHighFrequencies> HighFrequencies
        {
            get { return Enum.GetValues(typeof(IntegraHighFrequencies)).Cast<IntegraHighFrequencies>(); }
        }

        #endregion
    }
}
