using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x00020000, 0x00000032)]
    public class PCMDrumKitCommon02 : IntegraModel<PCMDrumKitCommon02>
    {
        [Offset(0x0000)] byte[] RESERVED01 = new byte[16];
        [Offset(0x0010)] byte[] _PhraseNumber = new byte[2];
        [Offset(0x0012)] byte[] RESERVED02 = new byte[31];
        [Offset(0x0031)] IntegraSwitch _TFXSwitch;

        public PCMDrumKitCommon02(PCMDrumKit drumKit) : base(drumKit.Device)
        {
            Address += drumKit.Address;
        }

        //[Offset(0x0010)]
        //public short PhraseNumber
        //{
        //    get { return _PhraseNumber.DeserializeShort(); }
        //    set
        //    {
        //        _PhraseNumber = value.SerializeShort();
        //        NotifyPropertyChanged();
        //    }
        //}

        [Offset(0x0031)]
        public IntegraSwitch TFXSwitch
        {
            get { return _TFXSwitch; }
            set
            {
                _TFXSwitch = value;
                NotifyPropertyChanged();
            }
        }
    }
}
