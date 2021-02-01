using Integra.Common;
using Integra.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class StudioSetMasterEQ : IntegraBase<StudioSetMasterEQ>
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
        public StudioSetMasterEQ() : base(0x18000900, 0x00000007) { }

        [Offset(0x0000)]
        public IntegraLowFrequencies EQLowFreq
        {
            get { return _EQLowFreq; }
            set
            {
                _EQLowFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0001)]
        public byte EQLowGain
        {
            get { return _EQLowGain.Offset(-15); }
            set
            {
                _EQLowGain = value.Offset(15);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0002)]
        public IntegraMidFrequencies EQMidFreq
        {
            get { return _EQMidFreq; }
            set
            {
                _EQMidFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0003)]
        public byte EQMidGain
        {
            get { return _EQMidGain.Offset(-15); }
            set
            {
                _EQMidGain = value.Offset(15);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0004)]
        public IntegraMidQs EQMidQ
        {
            get { return _EQMidQ; }
            set
            {
                _EQMidQ = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0005)]
        public IntegraHighFrequencies EQHighFreq
        {
            get { return _EQHighFreq; }
            set
            {
                _EQHighFreq = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public byte EQHighGain
        {
            get { return _EQHighGain.Offset(-15); }
            set
            {
                _EQHighGain = value.Offset(15);
                NotifyPropertyChanged();
            }
        }

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
