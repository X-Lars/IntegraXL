using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models
{
    [Integra(0x00003000, 0x0000003C)]
    public class PCMSynthToneCommon02 : IntegraModel<PCMSynthToneCommon02>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] byte[] RESERVED01 = new byte[16];
        [Offset(0x0010)] IntegraTemporaryToneCategories _ToneCategory;
        [Offset(0x0011)] byte[] ToCheck = new byte[2];
        [Offset(0x0013)] byte _PhraseOctaveShift;
        [Offset(0x0014)] byte[] RESERVED02 = new byte[31];
        [Offset(0x0033)] IntegraSwitch _TFXSwitch;
        [Offset(0x0034)] byte[] RESERVED03 = new byte[4];
        [Offset(0x0038)] IntegraSynthPhrase _PhraseNumber;

        #endregion

        #region Constructor

        internal PCMSynthToneCommon02(PCMSynthTone pcmSynthTone) : base(pcmSynthTone.Device)
        {
            Address += pcmSynthTone.Address;
        }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0010)]
        public IntegraTemporaryToneCategories ToneCategory
        {
            get => _ToneCategory;
            set
            {
                if (_ToneCategory != value)
                {
                    _ToneCategory = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0013)]
        public int PhraseOctaveShift
        {
            get => _PhraseOctaveShift.Deserialize(64);
            set
            {
                if (PhraseOctaveShift != value)
                {
                    _PhraseOctaveShift = value.Serialize(64).Clamp(61, 67);
                    NotifyPropertyChanged();
                }
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
        public IntegraSynthPhrase PhraseNumber
        {
            get => _PhraseNumber;
            set
            {
                if(_PhraseNumber != value)
                {
                    _PhraseNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
    }
}
