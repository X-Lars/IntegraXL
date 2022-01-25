using IntegraXL.Core;
using IntegraXL.Interfaces;
using IntegraXL.Models.Parameters;
using System.Diagnostics;

namespace IntegraXL.Models
{
    [Integra(0x18000600, 0x00000063)]
    public class StudioSetCommonReverb : IntegraModel<StudioSetCommonReverb>, IParameterProvider
    {
        //IntegraMFXValidator _Validator = new CommonOff();

        [Offset(0x0000)] private IntegraReverbTypes _Type;
        [Offset(0x0001)] private byte _ReverbLevel;
        [Offset(0x0002)] private IntegraStudioSetCommonOutputAssigns _OutputAssign;
        [Offset(0x0003)] private int[] _Parameters = new int[24];

        private StudioSetCommonReverb(Integra device) : base(device) { }

        [Offset(0x0000)]
        public IntegraReverbTypes Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0001)]
        public byte ReverbLevel
        {
            get { return _ReverbLevel; }
            set
            {
                _ReverbLevel = value;
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
        public int this[int index]
        {
            get
            {
                return _Parameters[index];
            }
            set
            {
                _Parameters[index] = value;
                NotifyPropertyChanged("Item", index);
                //NotifyPropertyChanged(nameof(P));
                //if (_Validator.Get(index, _Parameters[index]) != value)
                //{
                //    _Parameters[index] = _Validator.Set(index, value);
                //    NotifyPropertyChanged("Item[]", index);
                //}
            }
        }

        public IntegraParameter Parameter { get; set; }
        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            //base.SystemExclusiveReceived(sender, e);

            if (e.SystemExclusive.Address == Address)
            {
                if (e.SystemExclusive.Data.Length == Size)
                {
                    Debug.Print("*** COMMON REVERB: Full ***");
                    Initialize(e.SystemExclusive.Data);
                }
                //else
                //{
                //    IntegraAddress offset = new IntegraAddress(0x00000111);
                //    if (e.SystemExclusive.Address.InRange(Address, (int)(Address + offset)))
                //    {
                //        Debug.Print("*** COMMON REVERB: Parameters ***");
                //        // Parameter data received
                //        ReceivedProperty(e.SystemExclusive);
                //    }
                //}
            }
            else if (e.SystemExclusive.Address.InRange(Address, Address + Size))
            {
                Debug.Print("*** COMMON REVERB: Parameters ***");
                ReceivedProperty(e.SystemExclusive);
                InitProperty(e.SystemExclusive);
            }

        }

        private void InitProperty(IntegraSystemExclusive e)
        {
            Debug.Assert(e.Data.Length % 4 == 0);
            Debug.Assert(e.Data.Length == 4);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(e.Data);

            int value = BitConverter.ToInt32(e.Data, 0);
            int index = (e.Address - Address) / 4;

            _Parameters[index] = value;

            NotifyPropertyChanged(string.Empty);
        }

        protected override bool Initialize(byte[] data)
        {
            base.Initialize(data);

            SetValidation();

            return IsInitialized;
        }

        /// <summary>
        /// Sets the MFX model to use for parameter conversion and validation.
        /// </summary>
        /// <param name="type">An <see cref="IntegraMFXTypes"/> specifying the model to bind.</param>
        private void SetValidation()
        {
            switch (Type)
            {
                case IntegraReverbTypes.Room1:
                case IntegraReverbTypes.Room2:
                case IntegraReverbTypes.Hall1:
                case IntegraReverbTypes.Hall2:
                case IntegraReverbTypes.Plate:
                    Parameter = new CommonReverb(this);
                    break;

                case IntegraReverbTypes.GM2:
                    Parameter = new CommonReverbGM2(this);
                    break;

                default:
                    Parameter = new CommonReverbOff(this);
                    break;
            }
        }

        public virtual IEnumerable<IntegraReverbTypes> ReverbTypes
        {
            get { return Enum.GetValues(typeof(IntegraReverbTypes)).Cast<IntegraReverbTypes>(); }
        }

        public virtual IEnumerable<IntegraStudioSetCommonOutputAssigns> OutputAssigns
        {
            get { return Enum.GetValues(typeof(IntegraStudioSetCommonOutputAssigns)).Cast<IntegraStudioSetCommonOutputAssigns>(); }
        }
    }
}
