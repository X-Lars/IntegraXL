using IntegraXL.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegraXL.Models
{
    [Integra(0x18000600, 0x00000063)]
    public class StudioSetCommonReverb : IntegraModel
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

        //[Offset(0x0003)]
        //public double this[int index]
        //{
        //    get
        //    {
        //        return _Validator.GetMFXParameter(index, _Parameters[index]);
        //    }
        //    set
        //    {
        //        if (_Validator.GetMFXParameter(index, _Parameters[index]) != value)
        //        {
        //            _Parameters[index] = _Validator.SetMFXParameter(index, value);
        //            NotifyPropertyChanged("Item[]", index);
        //        }
        //    }
        //}


        protected override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                base.Initialize(data);

                SetValidation(Type);
            }

            return IsInitialized;
        }

        /// <summary>
        /// Sets the MFX model to use for parameter conversion and validation.
        /// </summary>
        /// <param name="type">An <see cref="IntegraMFXTypes"/> specifying the model to bind.</param>
        private void SetValidation(IntegraReverbTypes type)
        {
            //switch (type)
            //{
            //    case IntegraReverbTypes.Room1:
            //    case IntegraReverbTypes.Room2:
            //    case IntegraReverbTypes.Hall1:
            //    case IntegraReverbTypes.Hall2:
            //    case IntegraReverbTypes.Plate: 
            //        _Validator = new CommonReverb(); 
            //        break;

            //    case IntegraReverbTypes.GM2:   
            //        _Validator = new CommonReverbGM2();   
            //        break;

            //    default:
            //        _Validator = new CommonOff();
            //        break;
            //}
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
