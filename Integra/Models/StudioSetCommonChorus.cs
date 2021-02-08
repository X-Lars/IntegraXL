using Integra.Common;
using Integra.Core;
using Integra.Core.Interfaces;
using Integra.Models.MFX;
using MidiXL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Integra.Models
{
    public class StudioSetCommonChorus : IntegraBase<StudioSetCommonChorus>
    {
        IToneMFXModel _Model = new CommonOff();

        [Offset(0x0000)] private IntegraChorusTypes _Type;
        [Offset(0x0001)] private byte _ChorusLevel;
        [Offset(0x0002)] private IntegraStudioSetCommonOutputAssigns _OutputAssign;
        [Offset(0x0003)] private IntegraChorusOutputSelections _OutputSelect;
        [Offset(0x0004)] private int[] _Parameters = new int[20];

        public StudioSetCommonChorus() : base(0x18000400, 0x00000054) { }

        [Offset(0x0000)]
        public IntegraChorusTypes Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                NotifyPropertyChanged();
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
        public double this[int index]
        {
            get 
            { 
                return _Model.Get(index, _Parameters[index].ConvertFromIntegraParameter());  
            }
            set
            {
                if (_Parameters[index] != _Model.Set(index, value).ConvertToIntegraParameter())
                {
                    _Parameters[index] = _Model.Set(index, value).ConvertToIntegraParameter();
                    NotifyIndexerPropertyChanged(index);
                }
            }
        }

        /// <summary>
        /// Overrides the base for MFX specific system exclusive filtering.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">A <see cref="SystemExclusiveMessageEventArgs"/> containing event data.</param>
        protected override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            if (!IsInitialized)
            {
                if (syx.Address == Address)
                {
                    if (Initialize(syx.Data))
                        Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
                }
            }
            else
            {
                if ((syx.Address & 0xFFFFFF00) == (Address & 0xFFFFFF00))
                {
                    InitializeField(syx);
                }
            }
        }

        internal override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                base.Initialize(data);

                SetValidationModel(Type);

                IsInitialized = true;
            }

            return IsInitialized;
        }

        /// <summary>
        /// Sets the MFX model to use for parameter conversion and validation.
        /// </summary>
        /// <param name="type">An <see cref="IntegraMFXTypes"/> specifying the model to bind.</param>
        private void SetValidationModel(IntegraChorusTypes type)
        {
            switch (type)
            {
                case IntegraChorusTypes.Chorus:    _Model = new CommonChorus();      break;
                case IntegraChorusTypes.Delay:     _Model = new CommonChorusDelay(); break;
                case IntegraChorusTypes.GM2Chorus: _Model = new CommonChorusGM2();   break;

                default:
                    _Model = new CommonOff();
                    break;
            }
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
