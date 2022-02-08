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
        internal SuperNATURALDrumKitNotes(SuperNATURALDrumKit drumKit) : base(drumKit.Device)
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
            // TODO: ? Collections are disconnected after initialization so it's safe to only match the address range
            if(e.SystemExclusive.Address.InRange(this.First().Address, this.Last().Address))
            {
                Device.ReportProgress(this, Collection.Where(x => x.IsInitialized).Count(), Size - 1, e.SystemExclusive.Address.GetTemporaryTonePart());
            }
        }

        protected override bool Initialize(byte[] data)
        {
            throw new NotImplementedException();
        }
    }

    [Integra(0x00001000, 0x00000013)]
    public class SuperNATURALDrumKitNote : IntegraModel<SuperNATURALDrumKitNote>
    {
        private int _Note;

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
            Note = note;
        }

        #endregion

        public int Note
        {
            get { return _Note; }
            set
            {
                _Note = value;
                NotifyPropertyChanged();
            }
        }

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public int InstNumber
        {
            get { return _InstNumber.DeserializeInt(); }
            set
            {
                _InstNumber = value.SerializeInt();
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0004)]
        public byte NoteLevel
        {
            get { return _NoteLevel; }
            set
            {
                _NoteLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0005)]
        public byte Pan
        {
            get { return _Pan; }
            set
            {
                _Pan = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public byte ChorusSendLevel
        {
            get { return _ChorusSendLevel; }
            set
            {
                _ChorusSendLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0007)]
        public byte ReverbSendLevel
        {
            get { return _ReverbSendLevel; }
            set
            {
                _ReverbSendLevel = value;
                NotifyPropertyChanged();

            }
        }

        [Offset(0x0008)]
        public int Tune
        {
            // TODO: Calulate tune values -1200 / 1200 (8 / 248)
            get { return _Tune.DeserializeInt(); }
            set
            {
                _Tune = value.SerializeInt();
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000C)]
        public byte Attack
        {
            get { return _Attack; }
            set
            {
                _Attack = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000D)] 
        public byte Decay
        {
            get { return _Decay; }
            set
            {
                _Decay = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000E)] 
        public byte Brilliance
        {
            get { return _Brilliance; }
            set
            {
                _Brilliance = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000F)] 
        public IntegraNoteVariation Variation
        {
            get { return _Variation; }
            set
            {
                _Variation = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0010)] 
        public byte DynamicRange
        {
            get { return _DynamicRange; }
            set
            {
                _DynamicRange = value;
                NotifyPropertyChanged();
            }
        }
        [Offset(0x0011)] 
        public byte StereoWidth
        {
            get { return _StereoWidth; }
            set
            {
                _StereoWidth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0012)] 
        public IntegraNoteOutputAssign OutputAssign
        {
            get { return _OutputAssign; }
            set
            {
                _OutputAssign = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
