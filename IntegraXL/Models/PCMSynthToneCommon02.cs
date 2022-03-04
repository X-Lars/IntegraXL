using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models
{
    [Integra(0x00003000, 0x0000003C)]
    public class PCMSynthToneCommon02 : IntegraModel<PCMSynthToneCommon02>
    {
        [Offset(0x0000)] byte[] RESERVED01 = new byte[16];
        [Offset(0x0010)] byte _ToneCategory;
        [Offset(0x0013)] byte _PhraseOctaveShift;
        [Offset(0x0014)] byte[] RESERVED02 = new byte[31];
        [Offset(0x0033)] IntegraSwitch _TFXSwitch;
        [Offset(0x0034)] byte[] RESERVED03 = new byte[4];
        [Offset(0x0038)] int _PhraseNumber;

        public PCMSynthToneCommon02(PCMSynthTone pcmSynthTone) : base(pcmSynthTone.Device)
        {
            Address += pcmSynthTone.Address;
        }

        [Offset(0x0010)]
        public byte ToneCategory
        {
            get { return _ToneCategory; }
            set
            {
                _ToneCategory = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0013)]
        public byte PhraseOctaveShift
        {
            get { return _PhraseOctaveShift; }
            set
            {
                _PhraseOctaveShift = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0033)]
        public IntegraSwitch TFXSwitch
        {
            get { return _TFXSwitch; }
            set
            {
                _TFXSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0038)]
        public int PhraseNumber
        {
            get { return _PhraseNumber.ToMidi(); }
            set
            {
                _PhraseNumber = value.SerializeInt();
            }
        }
    }
}
