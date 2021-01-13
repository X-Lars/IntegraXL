using Integra.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    /// <summary>
    /// Provides the INEGRA-7 setup data structure.
    /// </summary>
    public class Setup : IntegraBase<Setup>
    {
        #region Fields

        [Offset(0x0000)] private byte _SoundMode;
        [Offset(0x0004)] private byte _StudioSetMSB;
        [Offset(0x0005)] private byte _StudioSetLSB;
        [Offset(0x0006)] private byte _StudioSetPC;

        #endregion

        #region Constructor

        public Setup() : base(0x01000000, 0x00000038) { }

        #endregion

        #region Properties

        [Offset(0x0000)]
        public IntegraSoundModes SoundMode
        {
            get { return (IntegraSoundModes)_SoundMode; }
            set
            {
                _SoundMode = (byte)value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0004)]
        public byte StudioSetMSB
        {
            get { return _StudioSetMSB; }
            set
            {
                _StudioSetMSB = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0005)]
        public byte StudioSetLSB
        {
            get { return _StudioSetLSB; }
            set
            {
                _StudioSetLSB = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public byte StudioSetPC
        {
            get { return _StudioSetPC; }
            set
            {
                _StudioSetPC = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
