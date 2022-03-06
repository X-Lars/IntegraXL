using IntegraXL.Core;
using IntegraXL.Extensions;
using System.Text;

namespace IntegraXL.Models
{
    [Integra(0x00000000, 0x00000014)]
    public class SuperNATURALDrumKitCommon : IntegraModel<SuperNATURALDrumKitCommon>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] byte[] _KitName = new byte[12];
        [Offset(0x000C)] byte[] RESERVED01 = new byte[4];
        [Offset(0x0010)] byte _KitLevel;
        [Offset(0x0011)] byte _AmbienceLevel;
        [Offset(0x0012)] byte _PhraseNumber;
        [Offset(0x0013)] IntegraSwitch _TFXSwitch;

        #endregion

        #region Constructor

        public SuperNATURALDrumKitCommon(SuperNATURALDrumKit drumKit) : base(drumKit.Device) 
        {
            Address = drumKit.Address;
        }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public string KitName
        {
            get { return Encoding.ASCII.GetString(_KitName); }
            set
            {
                if (KitName != value)
                {
                    if (string.IsNullOrEmpty(value))
                        return;

                    _KitName = Encoding.ASCII.GetBytes(value.Clamp(_KitName.Length));

                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0010)]
        public byte KitLevel
        {
            get { return _KitLevel; }
            set
            {
                _KitLevel = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0011)]
        public byte AmbienceLevel
        {
            get { return _AmbienceLevel; }
            set
            {
                _AmbienceLevel = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0012)]
        public byte PhraseNumber
        {
            get { return _PhraseNumber; }
            set
            {
                _PhraseNumber = value;
            }
        }

        [Offset(0x0013)]
        public IntegraSwitch TFXSwitch
        {
            get { return _TFXSwitch; }
            set
            {
                _TFXSwitch = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
