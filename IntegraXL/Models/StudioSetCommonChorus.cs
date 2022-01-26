using IntegraXL.Core;
using IntegraXL.Interfaces;
using IntegraXL.Models.Parameters;
using System.Diagnostics;

namespace IntegraXL.Models
{
    [Integra(0x18000400, 0x00000054)]
    public class StudioSetCommonChorus : IntegraModel<StudioSetCommonChorus>, IParameterProvider
    {
        #region Properties: INTEGRA-7

        [Offset(0x0000)] private IntegraChorusTypes _Type;
        [Offset(0x0001)] private byte _ChorusLevel;
        [Offset(0x0002)] private IntegraStudioSetCommonOutputAssigns _OutputAssign;
        [Offset(0x0003)] private IntegraChorusOutputSelections _OutputSelect;
        [Offset(0x0004)] private readonly int[] _Parameters = new int[20];

        #endregion

        #region Constructor

#pragma warning disable IDE0051 // Remove unused private members
        private StudioSetCommonChorus(Integra device) : base(device) { }
#pragma warning restore IDE0051 // Remove unused private members

        #endregion

        #region Properties

        public IntegraParameter? Parameter { get; set; }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public IntegraChorusTypes Type
        {
            get { return _Type; }
            set
            {
                _Type = value;

                NotifyPropertyChanged();
                Reinitialize();
            }
        }

        [Offset(0x0001)]
        public byte ChorusLevel
        {
            get { return _ChorusLevel; }
            set
            {
                _ChorusLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0002)]
        public IntegraStudioSetCommonOutputAssigns OutputAssign
        {
            get { return _OutputAssign; }
            set
            {
                _OutputAssign = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0003)]
        public IntegraChorusOutputSelections OutputSelect
        {
            get { return _OutputSelect; }
            set
            {
                _OutputSelect = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0004)]
        public int this[int index]
        {
            get
            {
                return _Parameters[index];
            }
            set
            {
                if(_Parameters[index] != value)
                {
                    _Parameters[index] = value;
                    NotifyPropertyChanged("Item", index);
                }
            }
        }

        #endregion

        #region Methods

        private void ReceivedParameter(IntegraSystemExclusive e)
        {
            Debug.Assert(e.Data.Length == 4);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(e.Data);

            int value = BitConverter.ToInt32(e.Data, 0);
            int index = (e.Address - Address - 4) / 4;
            Debug.Print(index.ToString());
            _Parameters[index] = value;

            NotifyPropertyChanged(string.Empty);
        }

        private void SetParameterProvider()
        {
            switch(Type)
            {
                case IntegraChorusTypes.Chorus:    Parameter = new CommonChorus(this); break;
                case IntegraChorusTypes.Delay:     Parameter = new CommonDelay(this); break;
                case IntegraChorusTypes.GM2Chorus: Parameter = new CommonChorusGM2(this); break;

                default:
                    Parameter = new CommonChorusOff(this);
                    break;
            };

            NotifyPropertyChanged(string.Empty);
        }

        #endregion

        #region Overrides: Model

        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            if (e.SystemExclusive.Address == Address)
            {
                // Always initialize, when the address is received the type has changed
                Initialize(e.SystemExclusive.Data);
            }
            else if (e.SystemExclusive.Address.InRange(Address, Address + Size))
            {
                if (e.SystemExclusive.Address - Address >= 0x00000004)
                {
                    ReceivedParameter(e.SystemExclusive);
                }
                else
                {
                    ReceivedProperty(e.SystemExclusive);
                }

            }
        }

        protected override bool Initialize(byte[] data)
        {
            IsInitialized = false;

            base.Initialize(data);

            SetParameterProvider();

            return IsInitialized = true;
        }

        #endregion

        #region Enumerations

        public static IEnumerable<IntegraChorusTypes> Types
        {
            get { return Enum.GetValues(typeof(IntegraChorusTypes)).Cast<IntegraChorusTypes>(); }
        }

        public static IEnumerable<IntegraStudioSetCommonOutputAssigns> OutputAssigns
        {
            get { return Enum.GetValues(typeof(IntegraStudioSetCommonOutputAssigns)).Cast<IntegraStudioSetCommonOutputAssigns>(); }
        }

        public static IEnumerable<IntegraChorusOutputSelections> OutputSelects
        {
            get { return Enum.GetValues(typeof(IntegraChorusOutputSelections)).Cast<IntegraChorusOutputSelections>(); }
        }

        #endregion
    }
}
