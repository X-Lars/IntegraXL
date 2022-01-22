using IntegraXL.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegraXL.Models
{
    [Integra(0x18000400, 0x00000054)]
    public class StudioSetCommonChorus : IntegraModel
    {
        //IntegraMFXValidator _Validator = new CommonOff();

        [Offset(0x0000)] private IntegraChorusTypes _Type;
        [Offset(0x0001)] private byte _ChorusLevel;
        [Offset(0x0002)] private IntegraStudioSetCommonOutputAssigns _OutputAssign;
        [Offset(0x0003)] private IntegraChorusOutputSelections _OutputSelect;
        [Offset(0x0004)] private int[] _Parameters = new int[20];

        private StudioSetCommonChorus(Integra device) : base(device) { }

        [Offset(0x0000)]
        public IntegraChorusTypes Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                NotifyPropertyChanged();
                //Initialize();
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

        //[Offset(0x0004)]
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
        private void SetValidation(IntegraChorusTypes type)
        {
            //switch (type)
            //{
            //    case IntegraChorusTypes.Chorus:    _Validator = new CommonChorus();      break;
            //    case IntegraChorusTypes.Delay:     _Validator = new CommonChorusDelay(); break;
            //    case IntegraChorusTypes.GM2Chorus: _Validator = new CommonChorusGM2();   break;

            //    default:
            //        _Validator = new CommonOff();
            //        break;
            //}
        }

        #region Enumerations


        public virtual IEnumerable<IntegraChorusTypes> ChorusTypes
        {
            get { return Enum.GetValues(typeof(IntegraChorusTypes)).Cast<IntegraChorusTypes>(); }
        }

        public virtual IEnumerable<IntegraChorusOutputSelections> ChorusOutputs
        {
            get { return Enum.GetValues(typeof(IntegraChorusOutputSelections)).Cast<IntegraChorusOutputSelections>(); }
        }

        public virtual IEnumerable<IntegraStudioSetCommonOutputAssigns> OutputAssigns
        {
            get { return Enum.GetValues(typeof(IntegraStudioSetCommonOutputAssigns)).Cast<IntegraStudioSetCommonOutputAssigns>(); }
        }

        public virtual List<string> PreDelayValues
        {
            get { return IntegraPreDelay.Values; }
        }

        public virtual IEnumerable<IntegraChorusFilterTypes> ChorusFilterTypes
        {
            get { return Enum.GetValues(typeof(IntegraChorusFilterTypes)).Cast<IntegraChorusFilterTypes>(); }
        }

        public virtual IEnumerable<IntegraMidFrequencies> ChorusCutoffFrequencies
        {
            get { return Enum.GetValues(typeof(IntegraMidFrequencies)).Cast<IntegraMidFrequencies>(); }
        }

        public virtual IEnumerable<IntegraDelayHFDamps> HFDamps
        {
            get { return Enum.GetValues(typeof(IntegraDelayHFDamps)).Cast<IntegraDelayHFDamps>(); }
        }

        public virtual IEnumerable<IntegraNoteRates> NoteRates
        {
            get { return Enum.GetValues(typeof(IntegraNoteRates)).Cast<IntegraNoteRates>(); }
        }

        #endregion
    }
}
