using Integra.Core;
using Integra.Common;
using System;
using System.Text;

namespace Integra.Models
{
    public class SuperNATURALAcousticToneCommon : IntegraBase<SuperNATURALAcousticToneCommon>
    {
        private IntegraParts _Part;

        [Offset(0x0000)] byte[] _ToneName = new byte[12];
        [Offset(0x0010)] byte _ToneLevel;
        [Offset(0x0011)] IntegraMonyPolySwitch _MonoPoly;
        [Offset(0x0012)] byte _PortamentTimeOffset;
        [Offset(0x0013)] byte _CutoffOffset;
        [Offset(0x0014)] byte _ResonanceOffset;
        [Offset(0x0015)] byte _AttackTimeOffset;
        [Offset(0x0016)] byte _ReleaseTimeOffset;
        [Offset(0x0017)] byte _VibratoRate;
        [Offset(0x0018)] byte _VibratoDepth;
        [Offset(0x0019)] byte _VibratoDelay;
        [Offset(0x001A)] byte _OctaveShift;
        [Offset(0x001B)] byte _Category;
        [Offset(0x001C)] byte[] _PhraseNumber = new byte[2];
        [Offset(0x001E)] byte _PhraseOctaveShift;
        [Offset(0x001F)] IntegraSwitch _TFXSwitch;
        [Offset(0x0020)] byte _InstVariation;
        [Offset(0x0021)] byte _InstNumber;
        [Offset(0x0022)] byte[] _ModifyParameter = new byte[32];

        public SuperNATURALAcousticToneCommon(IntegraAddress address, IntegraParts part) : base(address, 0x00000046)
        {
            Part = part;
            Name = "SuperNATURAL Acoustic Tone Common";
        }

        public IntegraParts Part
        {
            get { return _Part; }
            set
            {
                if(Part != value)
                {
                    _Part = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0000)]
        public string ToneName
        {
            get { return Encoding.ASCII.GetString(_ToneName, 0, 12); }
            set
            {
                if (ToneName != value)
                {
                    if (value == null)
                        return;

                    // Copy the string to the backing field byte array
                    Array.Copy(Encoding.ASCII.GetBytes(value), 0, _ToneName, 0, 12);
                    NotifyPropertyChanged();
                }
            }
        }
        [Offset(0x0010)]
        public byte ToneLevel
        {
            get { return _ToneLevel; }
            set
            {
                _ToneLevel = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0011)]
        public IntegraMonyPolySwitch MonoPoly
        {
            get { return _MonoPoly; }
            set
            {
                _MonoPoly = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0012)]
        public byte PortamentoTimeOffset
        {
            get { return _PortamentTimeOffset; }
            set
            {
                _PortamentTimeOffset = value;
                NotifyPropertyChanged();
            }
        }


        [Offset(0x0013)]
        public byte CutoffOffset
        {
            get { return _CutoffOffset; }
            set
            {
                _CutoffOffset = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0014)]
        public byte ResonanceOffset
        {
            get { return _ResonanceOffset; }
            set
            {
                _ResonanceOffset = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0015)]
        public byte AttackTimeOffset
        {
            get { return _AttackTimeOffset; }
            set
            {
                _AttackTimeOffset = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0016)]
        public byte ReleaseTimeOffset
        {
            get { return _ReleaseTimeOffset; }
            set
            {
                _ReleaseTimeOffset = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0017)]
        public byte VibratoRate
        {
            get { return _VibratoRate; }
            set
            {
                _VibratoRate = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0018)]
        public byte VibratoDepth
        {
            get { return _VibratoDepth; }
            set
            {
                _VibratoDepth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0019)]
        public byte VibratoDelay
        {
            get { return _VibratoDelay; }
            set
            {
                _VibratoDelay = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001A)]
        public byte OctaveShift
        {
            get { return _OctaveShift; }
            set
            {
                _OctaveShift = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001B)]
        public byte Category
        {
            get { return _Category; }
            set
            {
                _Category = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001C)]
        public short PhraseNumber
        {
            get { return _PhraseNumber.GetShort(); }
            set
            {
                _PhraseNumber = value.GetBytes();
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001E)]
        public byte PhraseOctaveShift
        {
            get { return _PhraseOctaveShift; }
            set
            {
                _PhraseOctaveShift = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x001F)]
        public IntegraSwitch TFXSwitch
        {
            get { return _TFXSwitch; }
            set
            {
                _TFXSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0020)]
        public byte InstVariation
        {
            get { return _InstVariation; }
            set
            {
                _InstVariation = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0021)]
        public byte InstNumber
        {
            get { return _InstNumber; }
            set
            {
                _InstNumber = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0022)]
        public byte this[int index]
        {
            get { return _ModifyParameter[index]; }
            set
            {
                _ModifyParameter[index] = value;
                NotifyIndexerPropertyChanged(index);
            }
        }
    }
}
