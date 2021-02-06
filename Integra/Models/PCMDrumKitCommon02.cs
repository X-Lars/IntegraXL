using Integra.Core;
using Integra.Common;
using Integra.Core.Interfaces;

namespace Integra.Models
{
    public class PCMDrumKitCommon02 : IntegraBase<PCMDrumKitCommon02>, IIntegraPartial
    {
        private IntegraParts _Part;

        [Offset(0x0010)] byte[] _PhraseNumber = new byte[2];
        [Offset(0x0031)] IntegraSwitch _TFXSwitch;

        public PCMDrumKitCommon02(IntegraAddress address, IntegraParts part) : base(address + 0x00020000, 0x00000032)
        {
            Name = "PCM Drum Kit Common 2";
            Part = part;
        }


        public IntegraParts Part
        {
            get { return _Part; }
            set
            {
                if(_Part != value)
                {
                    _Part = value;
                    NotifyPropertyChanged();
                }
            }
        }
        [Offset(0x0010)]
        public short PhraseNumber
        {
            get { return _PhraseNumber.GetShort(); }
            set
            {
                _PhraseNumber = value.GetBytes();
                NotifyPropertyChanged();
            }
        }

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
