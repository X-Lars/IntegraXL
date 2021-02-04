using Integra.Core;
using Integra.Core.Interfaces;
using System.Text;

namespace Integra.Models
{
    public class SuperNATURALDrumKitCommon :IntegraBase<SuperNATURALDrumKitCommon>, IIntegraPartial
    {
        private IntegraParts _Part;

        [Offset(0x0000)] byte[] _KitName = new byte[12];
        [Offset(0x0010)] byte _KitLevel;
        [Offset(0x0011)] byte _AmbienceLevel;
        [Offset(0x0012)] byte _PhraseNumber;
        [Offset(0x0013)] IntegraSwitch _TFXSwitch;

        public SuperNATURALDrumKitCommon(IntegraAddress address, IntegraParts part) : base(address, 0x00000014)
        {
            Part = part;
            Name = "SuperNATURAL Acoustic Drum Kit Common";
        }

        public IntegraParts Part
        {
            get { return _Part; }
            set
            {
                if (Part != value)
                {
                    _Part = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0000)]
        public string KitName
        {
            get { return Encoding.ASCII.GetString(_KitName); }
            set
            {
                _KitName = Encoding.ASCII.GetBytes(value);
                NotifyPropertyChanged();
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
    }
}
