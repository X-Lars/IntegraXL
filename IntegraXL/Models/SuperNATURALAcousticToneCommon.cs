using IntegraXL.Core;
using IntegraXL.Extensions;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace IntegraXL.Models
{
    [Integra(0x00000000, 0x00000046)]
    public sealed class SuperNATURALAcousticToneCommon : IntegraModel<SuperNATURALAcousticToneCommon>
    {
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

        internal SuperNATURALAcousticToneCommon(SuperNATURALAcousticTone tone) : base(tone.Device) 
        {
            Address = tone.Address;
            //Size = GetType().GetCustomAttribute<ModelAddress>().Size;
            //Debug.Print($"SuperNATURALAcousticToneCommon {Address}");
            //Device = device;
            //Initialize();
        }

        [Offset(0x0000)]
        public string ToneName
        {
            get { return Encoding.ASCII.GetString(_ToneName, 0, 12); }
            set
            {
                if (ToneName != value)
                {
                    if (string.IsNullOrEmpty(value))
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
                if (_ToneLevel != value)
                {
                    _ToneLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }
        [Offset(0x0011)]
        public IntegraMonyPolySwitch MonoPoly
        {
            get { return _MonoPoly; }
            set
            {
                if (_MonoPoly != value)
                {
                    _MonoPoly = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0012)]
        public byte PortamentoTimeOffset
        {
            get { return _PortamentTimeOffset; }
            set
            {
                if (_PortamentTimeOffset != value)
                {
                    _PortamentTimeOffset = value;
                    NotifyPropertyChanged();
                }
            }
        }


        [Offset(0x0013)]
        public byte CutoffOffset
        {
            get { return _CutoffOffset; }
            set
            {
                if (_CutoffOffset != value)
                {
                    _CutoffOffset = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0014)]
        public byte ResonanceOffset
        {
            get { return _ResonanceOffset; }
            set
            {
                if (_ResonanceOffset != value)
                {
                    _ResonanceOffset = value;
                    NotifyPropertyChanged();
                }
                
            }
        }

        [Offset(0x0015)]
        public byte AttackTimeOffset
        {
            get { return _AttackTimeOffset; }
            set
            {
                if (_AttackTimeOffset != value)
                {
                    _AttackTimeOffset = value;
                    NotifyPropertyChanged();
                }
            }
        }
        [Offset(0x0016)]
        public byte ReleaseTimeOffset
        {
            get { return _ReleaseTimeOffset; }
            set
            {
                if (_ReleaseTimeOffset != value)
                {
                    _ReleaseTimeOffset = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0017)]
        public byte VibratoRate
        {
            get { return _VibratoRate; }
            set
            {
                if (_VibratoRate != value)
                {
                    _VibratoRate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0018)]
        public byte VibratoDepth
        {
            get { return _VibratoDepth; }
            set
            {
                if (_VibratoDepth != value)
                {
                    _VibratoDepth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0019)]
        public byte VibratoDelay
        {
            get { return _VibratoDelay; }
            set
            {
                if (_VibratoDelay != value)
                {
                    _VibratoDelay = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001A)]
        public byte OctaveShift
        {
            get { return _OctaveShift; }
            set
            {
                if (_OctaveShift != value)
                {
                    _OctaveShift = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001B)]
        public byte Category
        {
            get { return _Category; }
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // TODO:
        //[Offset(0x001C)]
        //public short PhraseNumber
        //{
        //    get { return _PhraseNumber.DeserializeShort(); }
        //    set
        //    {
        //        if (_PhraseNumber.DeserializeShort() != value)
        //        {
        //            _PhraseNumber = value.SerializeShort();
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        [Offset(0x001E)]
        public byte PhraseOctaveShift
        {
            get { return _PhraseOctaveShift; }
            set
            {
                if (_PhraseOctaveShift != value)
                {
                    _PhraseOctaveShift = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x001F)]
        public IntegraSwitch TFXSwitch
        {
            get { return _TFXSwitch; }
            set
            {
                if (_TFXSwitch != value)
                {
                    _TFXSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0020)]
        public byte InstVariation
        {
            get { return _InstVariation; }
            set
            {
                if (_InstVariation != value)
                {
                    _InstVariation = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0021)]
        public byte InstNumber
        {
            get { return _InstNumber; }
            set
            {
                if (_InstNumber != value)
                {
                    _InstNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0022)]
        public byte this[int index]
        {
            get { return _ModifyParameter[index]; }
            set
            {
                if (_ModifyParameter[index] != value)
                {
                    _ModifyParameter[index] = value;
                    NotifyPropertyChanged("Item[]", index);
                }
            }
        }
    }
}
