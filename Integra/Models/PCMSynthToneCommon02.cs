using Integra.Core;
using Integra.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integra.Core.Interfaces;

namespace Integra.Models
{
    public class PCMSynthToneCommon02 : IntegraBase<PCMSynthToneCommon02>, IIntegraPartial
    {
        private IntegraParts _Part;

        [Offset(0x0010)] byte _ToneCategory;
        [Offset(0x0013)] byte _PhraseOctaveShift;
        [Offset(0x0033)] IntegraSwitch _TFXSwitch;
        [Offset(0x0038)] int _PhraseNumber;

        public PCMSynthToneCommon02(IntegraAddress address, IntegraParts part) : base(address, 0x0000003C)
        {
            Name = "PCM Synth Tone Common 2";
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
            get { return _PhraseNumber.GetIntegraValue(); }
            set
            {
                _PhraseNumber = value.SetIntegraValue();
            }
        }
    }
}
