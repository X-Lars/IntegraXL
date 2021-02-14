using Integra.Common;
using Integra.Core;
using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Integra.Models
{
    public class StudioSetPartEQ : IntegraBase<StudioSetPartEQ>, IIntegraPartial, INotifyPropertyChanged, IIntegraStudioSetPartial
    {
        #region Fields

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
        /// Creates a new disconnected <see cref="StudioSetPartEQ"/> instance.
        /// </summary>
        /// <remarks><i>Default constructor for dynamic instance creation.</i></remarks>
        public StudioSetPartEQ() { }

        /// <summary>
        /// Creates and initializes a new connected <see cref="StudioSetPartEQ"/> instance.
        /// </summary>
        /// <param name="address">The address of data structure.</param>
        /// <param name="request">The request to initialize the data structure.</param>
        public StudioSetPartEQ(IntegraAddress address, IntegraRequest request) : base(address, request)
        {
            Part = (IntegraParts)((address & 0x00000F00) >> 8);
        }

        #endregion

        #region Properties

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
        public byte EQLowGain
        {
            get { return _EQLowGain.Offset(-15); }
            set
            {
                _EQLowGain = value.Offset(15);
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
        public byte EQMidGain
        {
            get { return _EQMidGain.Offset(-15); }
            set
            {
                _EQMidGain = value.Offset(15);
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
        public byte EQHighGain
        {
            get { return _EQHighGain.Offset(-15); }
            set
            {
                _EQHighGain = value.Offset(15);
                NotifyPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region IIntegraPartial

        public IntegraParts Part { get; set; }

        #endregion

        #region Overrides

        #endregion

        public virtual IEnumerable<IntegraLowFrequencies> LowFrequencyValues
        {
            get { return Enum.GetValues(typeof(IntegraLowFrequencies)).Cast<IntegraLowFrequencies>(); }
        }

        public virtual IEnumerable<IntegraMidFrequencies> MidFrequencyValues
        {
            get { return Enum.GetValues(typeof(IntegraMidFrequencies)).Cast<IntegraMidFrequencies>(); }
        }

        public virtual IEnumerable<IntegraMidQs> MidQValues
        {
            get { return Enum.GetValues(typeof(IntegraMidQs)).Cast<IntegraMidQs>(); }
        }

        public virtual IEnumerable<IntegraHighFrequencies> HighFrequencyValues
        {
            get { return Enum.GetValues(typeof(IntegraHighFrequencies)).Cast<IntegraHighFrequencies>(); }
        }
    }
}
