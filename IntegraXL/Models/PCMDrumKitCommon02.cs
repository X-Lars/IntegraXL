using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x00020000, 0x00000032)]
    public class PCMDrumKitCommon02 : IntegraModel<PCMDrumKitCommon02>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private byte[] _RESERVED01 = new byte[16];
        [Offset(0x0010)] private IntegraPCMDrumKitPhrase _PhraseNumber;
        [Offset(0x0012)] private byte[] _RESERVED02 = new byte[31];
        [Offset(0x0031)] private IntegraSwitch _TFXSwitch;

        #endregion

        #region Constructor

        internal PCMDrumKitCommon02(PCMDrumKit parent) : base(parent.Device)
        {
            Address += parent.Address;
        }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0010)]
        public IntegraPCMDrumKitPhrase PhraseNumber
        {
            get => _PhraseNumber;
            set
            {
                if (_PhraseNumber != value)
                {
                    _PhraseNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0031)]
        public IntegraSwitch TFXSwitch
        {
            get => _TFXSwitch;
            set
            {
                if (_TFXSwitch != value)
                {
                    _TFXSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
    }
}