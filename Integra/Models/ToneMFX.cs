using Integra.Common;
using Integra.Core;
using Integra.Core.Interfaces;
using Integra.Models.MFX;
using MidiXL;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Integra.Models
{
    /// <summary>
    /// Defines the tone MFX structure for all INTEGRA-7 tone types.
    /// </summary>
    public sealed class ToneMFX : IntegraBase<ToneMFX>
    {
        #region Fields

        /// <summary>
        /// Stores the MFX model associated with the <see cref="Type"/>.
        /// </summary>
        private IToneMFXModel _Model = new Thru();

        [Offset(0x0000)] private IntegraMFXTypes _Type;
        [Offset(0x0002)] private byte _ChorusSendLevel;
        [Offset(0x0003)] private byte _ReverbSendLevel;
        [Offset(0x0005)] private IntegraMFXControlSources _ControlSource01;
        [Offset(0x0006)] private byte _ControlSense01;
        [Offset(0x0007)] private IntegraMFXControlSources _ControlSource02;
        [Offset(0x0008)] private byte _ControlSense02;
        [Offset(0x0009)] private IntegraMFXControlSources _ControlSource03;
        [Offset(0x000A)] private byte _ControlSense03;
        [Offset(0x000B)] private IntegraMFXControlSources _ControlSource04;
        [Offset(0x000C)] private byte _ControlSense04;
        [Offset(0x000D)] private IntegraMFXControlAssigns _ControlAssign01;
        [Offset(0x000E)] private IntegraMFXControlAssigns _ControlAssign02;
        [Offset(0x000F)] private IntegraMFXControlAssigns _ControlAssign03;
        [Offset(0x0010)] private IntegraMFXControlAssigns _ControlAssign04;
        [Offset(0x0011)] private int[] _Parameters = new int[32];

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes a new <see cref="ToneMFX"/> instance.
        /// </summary>
        /// <param name="address">The <see cref="IntegraAddress"/> of the parent INTEGRA-7 tone structure.</param>
        public ToneMFX(IntegraAddress address) : base(address + 0x00000200, 0x00000111)
        {
            Debug.Print($"[{nameof(ToneMFX)}] {Address}");

            Name = "Tone MFX";
        }

        #endregion

        #region Properties : INTEGRA-7

        [Offset(0x0000)]
        public IntegraMFXTypes Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                SetModel(value);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0002)]
        public byte ChorusSendLevel
        {
            get { return _ChorusSendLevel; }
            set
            {
                _ChorusSendLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0003)]
        public byte ReverbSendLevel
        {
            get { return _ReverbSendLevel; }
            set
            {
                _ReverbSendLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0005)]
        public IntegraMFXControlSources ControlSource01
        {
            get { return _ControlSource01; }
            set
            {
                _ControlSource01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public byte ControlSense01
        {
            get { return _ControlSense01; }
            set
            {
                _ControlSense01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0007)]
        public IntegraMFXControlSources ControlSource02
        {
            get { return _ControlSource02; }
            set
            {
                _ControlSource02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0008)]
        public byte ControlSense02
        {
            get { return _ControlSense02; }
            set
            {
                _ControlSense02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0009)]
        public IntegraMFXControlSources ControlSource03
        {
            get { return _ControlSource03; }
            set
            {
                _ControlSource03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000A)]
        public byte ControlSense03
        {
            get { return _ControlSense03; }
            set
            {
                _ControlSense03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000B)]
        public IntegraMFXControlSources ControlSource04
        {
            get { return _ControlSource04; }
            set
            {
                _ControlSource04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000C)]
        public byte ControlSense04
        {
            get { return _ControlSense04; }
            set
            {
                _ControlSense04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000D)]
        public IntegraMFXControlAssigns ControlAssign01
        {
            get { return _ControlAssign01; }
            set
            {
                _ControlAssign01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000E)]
        public IntegraMFXControlAssigns ControlAssign02
        {
            get { return _ControlAssign02; }
            set
            {
                _ControlAssign02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000F)]
        public IntegraMFXControlAssigns ControlAssign03
        {
            get { return _ControlAssign03; }
            set
            {
                _ControlAssign03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0010)]
        public IntegraMFXControlAssigns ControlAssign04
        {
            get { return _ControlAssign04; }
            set
            {
                _ControlAssign04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0011)]
        public int this[int index]
        {
            get
            {
                //return _Model.GetParameter(index, GetData(_Parameters[index]));
                return _Model.Get(index, _Parameters[index].ConvertFromIntegraParameter());
            }

            set
            {
                //_Parameters[index] = SetData(_Model.SetParameter(index, value));
                _Parameters[index] = _Model.Set(index, value).ConvertToIntegraParameter();
                NotifyIndexerPropertyChanged(index);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the MFX model to use for parameter conversion and validation.
        /// </summary>
        /// <param name="type">An <see cref="IntegraMFXTypes"/> specifying the model to bind.</param>
        private void SetModel(IntegraMFXTypes type)
        {
            switch (type)
            {
                case IntegraMFXTypes.Equalizer: _Model = new Equalizer(); break;
                default:
                    _Model = new Thru();
                    break;
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Overrides the base for MFX specific system exclusive filtering.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">A <see cref="SystemExclusiveMessageEventArgs"/> containing event data.</param>
        internal override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            if (!IsInitialized)
            {
                if (syx.Address == Address)
                {
                    if (Initialize(syx.Data))
                        Device.Instance.ReportProgress(new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
                }
                

            }
            else
            {
                if ((syx.Address & 0xFFFFF000) == (Address & 0xFFFFF000))
                {
                    if((syx.Address & 0x00000F00).IsInRange(0x00000200, 0x00000300))
                    {
                        InitializeField(syx);
                    }
                }
            }
        }

        /// <summary>
        /// Overrides the base to include binding of the MFX model on initialization.
        /// </summary>
        /// <param name="data">A <see cref="byte"/>[] containing the <see cref="IntegraSystemExclusive.Data"/> to initialize the data structure.</param>
        /// <returns>A <see cref="bool"/> containing true if the data structure is initialized.</returns>
        protected override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                base.Initialize(data);

                SetModel(Type);
            }

            return IsInitialized;
        }

        #endregion;

        #region Enumerations

        
        #endregion
    }
}
