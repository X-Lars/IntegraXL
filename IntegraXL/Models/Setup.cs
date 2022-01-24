using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models
{
    [Integra(0x01000000, 0x00000038)]
    public sealed class Setup : IntegraModel<Setup>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private IntegraSoundModes _SoundMode;
        //[Offset(0x0004)] private byte[] _BankSelect = new byte[3];
        [Offset(0x0004)] private byte _StudioSetMSB;
        [Offset(0x0005)] private byte _StudioSetLSB;
        [Offset(0x0006)] private byte _StudioSetPC;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the INTEGRA-7 setup model.
        /// </summary>
        private Setup(Integra device) : base(device) { }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public IntegraSoundModes SoundMode
        {
            get { return _SoundMode; }
            internal set
            {
                if (_SoundMode != value)
                {
                    _SoundMode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //[Offset(0x0004)]
        //public IBankSelect BankSelect
        //{
        //    get { return new IntegraBankSelect(_BankSelect[0], _BankSelect[1], _BankSelect[2]); }
        //    internal set
        //    {
        //        if (BankSelect.Equals(value))
        //            return;

        //        _BankSelect[0] = value.MSB;
        //        _BankSelect[1] = value.LSB;
        //        _BankSelect[2] = value.PC.Serialize(0, 63);

        //        // Notifies the readonly propery change
        //        NotifyPropertyChanged(nameof(StudioSetMSB));
        //        NotifyPropertyChanged(nameof(StudioSetLSB));
        //        NotifyPropertyChanged(nameof(StudioSetPC));

        //        // Transmits the bank select change
        //        NotifyPropertyChanged();
        //    }
        //}

        //public byte StudioSetMSB => _BankSelect[0];

        //public byte StudioSetLSB => _BankSelect[1];

        ///// <summary>
        ///// Gets the program change of the current studio set.
        ///// </summary>
        //public byte StudioSetPC => _BankSelect[2];
        [Offset(0x0004)]
        public byte StudioSetMSB
        {
            get { return _StudioSetMSB; }
            internal set
            {
                if (_StudioSetMSB != value)
                {
                    _StudioSetMSB = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0005)]
        public byte StudioSetLSB
        {
            get { return _StudioSetLSB; }
            internal set
            {
                if (_StudioSetLSB != value)
                {
                    _StudioSetLSB = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public byte StudioSetPC
        {
            get { return _StudioSetPC; }
            set
            {
                if (_StudioSetPC != value)
                {
                    _StudioSetPC = value.Serialize(0, 63);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
    }
}
