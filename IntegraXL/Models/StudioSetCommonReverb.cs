﻿using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using IntegraXL.Models.Providers;
using System.ComponentModel;
using System.Diagnostics;

namespace IntegraXL.Models
{
    [Integra(0x18000600, 0x00000063)]
    public class StudioSetCommonReverb : IntegraModel<StudioSetCommonReverb>, IParameterProvider<int>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private IntegraReverbTypes _Type;
        [Offset(0x0001)] private byte _ReverbLevel;
        [Offset(0x0002)] private IntegraStudioSetCommonOutputAssigns _OutputAssign;
        [Offset(0x0003)] private readonly int[] _Parameters = new int[24];

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
#pragma warning disable IDE0051 // Remove unused private members
        private StudioSetCommonReverb(Integra device) : base(device) { }
#pragma warning restore IDE0051 // Remove unused private members

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public IntegraReverbTypes Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;

                    NotifyPropertyChanged();
                    //ReinitializeAsync();
                }
            }
        }

        [Offset(0x0001)]
        public byte ReverbLevel
        {
            get { return _ReverbLevel; }
            set
            {
                if (_ReverbLevel != value)
                {
                    _ReverbLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0002)]
        public IntegraStudioSetCommonOutputAssigns OutputAssign
        {
            get { return _OutputAssign; }
            set
            {
                if (_OutputAssign != value)
                {
                    _OutputAssign = value;
                    NotifyPropertyChanged();
                }
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
                if (_Parameters[index] != value)
                {
                    _Parameters[index] = value;
                    NotifyPropertyChanged("Item", index);
                }
            }
        }

        #endregion

        #region Interfaces: IParameterProvider

        /// <summary>
        /// Event raised when the reverb type is changed.
        /// </summary>
        public event EventHandler<IntegraParametersChangedEventArgs>? ParametersChanged;

        /// <summary>
        /// Gets the reverb parameters.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public IntegraParameterProvider<int>? Parameters { get; private set; }

        #endregion

        #region Methods

        private void ReceivedParameter(IntegraSystemExclusive e)
        {
            Debug.Assert(e.Data.Length == 4);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(e.Data);

            int value = BitConverter.ToInt32(e.Data, 0);
            int index = (e.Address - Address - 3) / 4;

            _Parameters[index] = value;

            NotifyPropertyChanged(string.Empty);
        }

        private void SetParameterProvider()
        {
            switch(Type)
            {
                case IntegraReverbTypes.Room1:
                case IntegraReverbTypes.Room2: 
                case IntegraReverbTypes.Hall1: 
                case IntegraReverbTypes.Hall2:
                case IntegraReverbTypes.Plate:
                    Parameters = new CommonReverb(this); 
                    break;

                case IntegraReverbTypes.GM2:
                    Parameters = new CommonReverbGM2(this);
                    break;

                default:
                    Parameters = new CommonReverbOff(this);
                    break;
            }

            ParametersChanged?.Invoke(this, new IntegraParametersChangedEventArgs(Parameters.GetType()));
            NotifyPropertyChanged(string.Empty);
        }

        #endregion

        #region Overrides: Model

        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            if (e.SystemExclusive.Address == Address)
            {
                // Always initialize, the the first property offset = 0 and determines the type
                Initialize(e.SystemExclusive.Data);
            }
            else if (e.SystemExclusive.Address.InRange(Address, Address + Size))
            {
                if (e.SystemExclusive.Address - Address >= 0x00000003)
                {
                    ReceivedParameter(e.SystemExclusive);
                }
                else
                {
                    ReceivedProperty(e.SystemExclusive);
                }

            }

        }

        internal override bool Initialize(byte[] data)
        {
            IsInitialized = false;

            base.Initialize(data);

            SetParameterProvider();

            return IsInitialized = true;
        }

        #endregion
    }
}
