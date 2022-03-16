using IntegraXL.Core;
using IntegraXL.Extensions;
using System.Text;

namespace IntegraXL.Models
{
    [Integra(0x00000000, 0x00000012)]
    public class PCMDrumKitCommon : IntegraModel<PCMDrumKitCommon>
    {
        #region Properties: INTEGRA-7

        [Offset(0x0000)] byte[] _KitName = new byte[12];
        [Offset(0x000C)] byte _KitLevel;
        [Offset(0x000D)] byte[] RESERVED01 = new byte[5];

        #endregion

        #region Constructor

        public PCMDrumKitCommon(PCMDrumKit parent) : base(parent.Device)
        {
            Address = parent.Address;
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

                    _KitName = Encoding.ASCII.GetBytes(value.FixedLength(_KitName.Length));

                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public byte KitLevel
        {
            get { return _KitLevel; }
            set
            {
                _KitLevel = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
