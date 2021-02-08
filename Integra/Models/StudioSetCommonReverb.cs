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
    public class StudioSetCommonReverb : IntegraBase<StudioSetCommonReverb>
    {
        IToneMFXModel _Model = new CommonOff();

        [Offset(0x0000)] private IntegraReverbTypes _Type;
        [Offset(0x0001)] private byte _ReverbLevel;
        [Offset(0x0002)] private IntegraStudioSetCommonOutputAssigns _OutputAssign;
        [Offset(0x0003)] private int[] _Parameters = new int[24];

        public StudioSetCommonReverb() : base(0x18000600, 0x00000063) { }

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
        private void SetValidationModel(IntegraReverbTypes type)
        {
            switch (type)
            {
                case IntegraReverbTypes.Room1:
                case IntegraReverbTypes.Room2:
                case IntegraReverbTypes.Hall1:
                case IntegraReverbTypes.Hall2:
                case IntegraReverbTypes.Plate: 
                    _Model = new CommonReverb(); 
                    break;

                case IntegraReverbTypes.GM2:   
                    _Model = new CommonReverbGM2();   
                    break;

                default:
                    _Model = new CommonOff();
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
