using IntegraXL.Core;
using IntegraXL.Extensions;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace IntegraXL.Models
{

    [Integra(0x00001000, 0x00004000, 62)]
    public class SuperNATURALDrumKitNotes : IntegraCollection<SuperNATURALDrumKitNote>
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="SuperNATURALDrumKitNotes"/> instance.
        /// </summary>
        /// <param name="drumKit">The <see cref="SuperNATURALDrumKit"/> to connect the collection.</param>
        internal SuperNATURALDrumKitNotes(SuperNATURALDrumKit drumKit) : base(drumKit.Device, false)
        {
            Address += drumKit.Address;

            IntegraRequest request = new(Attribute.Request);

            Requests.Add(request);

            for (int i = 0; i < Size; i++)
            {
                SuperNATURALDrumKitNote? note = Activator.CreateInstance(typeof(SuperNATURALDrumKitNote), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { drumKit, i }, null) as SuperNATURALDrumKitNote;

                Debug.Assert(note != null);

                Add(note);
            }

            Connect();
        }

        #endregion

        /// <summary>
        /// Gets whether the collection is initialized.
        /// </summary>
        /// <remarks><i>Only returns true if all partials are initialized.</i></remarks>
        public override bool IsInitialized
        {
            get => Collection.Last().IsInitialized;
        }

        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            // TODO: Make collection thread safe
            // TODO: ? Collections are disconnected after initialization so it's safe to only match the address range
            if(e.SystemExclusive.Address.InRange(Collection.First().Address, Collection.Last().Address))
            {
                Device.ReportProgress(this, Collection.Where(x => x.IsInitialized).Count(), Size, e.SystemExclusive.Address.GetTemporaryTonePart());
            }
        }

        internal override bool Initialize(byte[] data)
        {
            throw new NotImplementedException();
        }

    }

    [Integra(0x00001000, 0x00000013)]
    public class SuperNATURALDrumKitNote : IntegraModel<SuperNATURALDrumKitNote>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] int _InstNumber;
        [Offset(0x0004)] byte _NoteLevel;
        [Offset(0x0005)] byte _Pan;
        [Offset(0x0006)] byte _ChorusSendLevel;
        [Offset(0x0007)] byte _ReverbSendLevel;
        [Offset(0x0008)] int _Tune;
        [Offset(0x000C)] byte _Attack;
        [Offset(0x000D)] byte _Decay;
        [Offset(0x000E)] byte _Brilliance;
        [Offset(0x000F)] IntegraNoteVariation _Variation;
        [Offset(0x0010)] byte _DynamicRange;
        [Offset(0x0011)] byte _StereoWidth;
        [Offset(0x0012)] IntegraNoteOutputAssign _OutputAssign;

        #endregion

        #region Constructor

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "The class is created by reflection.")]
        private SuperNATURALDrumKitNote(SuperNATURALDrumKit drumKit, int note) : base(drumKit.Device)
        {
            Address += drumKit.Address;

            int lsb = (note % 16) << 8;
            int msb = (note / 16) << 12;

            int offset = (msb + lsb);

            Address += offset;

            Index = note;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected partial.
        /// </summary>
        public IntegraSNDNoteIndex Partial => (IntegraSNDNoteIndex)Index;

        /// <summary>
        /// Gets the index of the partial.
        /// </summary>
        public int Index { get; }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public IntegraSNDInstruments InstNumber
        {
            get => (IntegraSNDInstruments)_InstNumber.Deserialize();
            set
            {
                if (InstNumber != value)
                {
                    _InstNumber = ((int)value).SerializeInt();

                    Variation = IntegraNoteVariation.Off;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0004)]
        public byte NoteLevel
        {
            get => _NoteLevel;
            set
            {
                if (_NoteLevel != value)
                {
                    _NoteLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0005)]
        public byte Pan
        {
            get => _Pan;
            set
            {
                if (_Pan != value)
                {
                    _Pan = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public byte ChorusSendLevel
        {
            get => _ChorusSendLevel;
            set
            {
                if (_ChorusSendLevel != value)
                {
                    _ChorusSendLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0007)]
        public byte ReverbSendLevel
        {
            get => _ReverbSendLevel;
            set
            {
                if (_ReverbSendLevel != value)
                {
                    _ReverbSendLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0008)]
        public int Tune
        {
            // TODO: Calulate tune values -1200 / 1200 (8 / 248)
            get => _Tune.DeserializeInt(128, 10);
            set
            {
                if (Tune != value)
                {
                    _Tune = value.Clamp(-1200, 1200).SerializeInt(128, 10);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public byte Attack
        {
            get => _Attack;
            set
            {
                if (_Attack != value)
                {
                    _Attack = value.Clamp(0, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000D)] 
        public int Decay
        {
            get => _Decay.Deserialize(64);
            set
            {
                if (Decay != value)
                {
                    _Decay = value.Clamp(-63, 0).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000E)] 
        public int Brilliance
        {
            get => _Brilliance.Deserialize(64);
            set
            {
                if (Brilliance != value)
                {
                    _Brilliance = value.Clamp(-15, 12).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000F)] 
        public IntegraNoteVariation Variation
        {
            get => _Variation;
            set
            {
                if (_Variation != value)
                {
                    if ((byte)value > this.MaxVariation())
                        return;

                    _Variation = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0010)] 
        public byte DynamicRange
        {
            get => _DynamicRange;
            set
            {
                if (_DynamicRange != value)
                {
                    _DynamicRange = value.Clamp(0, 63);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0011)] 
        public byte StereoWidth
        {
            get => _StereoWidth;
            set
            {
                if (_StereoWidth != value)
                {
                    _StereoWidth = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0012)] 
        public IntegraNoteOutputAssign OutputAssign
        {
            get => _OutputAssign;
            set
            {
                if (_OutputAssign != value)
                {
                    _OutputAssign = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        public static List<string> PanValues
        {
            get { return IntegraPan.Values; }
        }
    }
}
