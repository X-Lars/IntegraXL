using IntegraXL.Core;
using IntegraXL.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Models
{
    [Integra(0x00000200, 0x00000111, 145)]
    public class MFX : IntegraModel
    {
        IntegraValidator? _Validator;

        #region Fields

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

        public MFX(TemporaryTone temporaryTone) : base(temporaryTone.Device)
        {
            //Address; += temporaryTone.Address;
            Debug.Print($"[{nameof(MFX)}] Contructor: {Address:X4}, 0x{Size:X4}");
        }

        #region Properties : INTEGRA-7

        [Offset(0x0000)]
        public IntegraMFXTypes Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    //SetValidator();
                    NotifyPropertyChanged();
                    //Initialize();
                }
            }
        }

        [Offset(0x0002)]
        public byte ChorusSendLevel
        {
            get { return _ChorusSendLevel; }
            set
            {
                if (_ChorusSendLevel != value)
                {
                    _ChorusSendLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0003)]
        public byte ReverbSendLevel
        {
            get { return _ReverbSendLevel; }
            set
            {
                if (_ReverbSendLevel != value)
                {
                    _ReverbSendLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0005)]
        public IntegraMFXControlSources ControlSource01
        {
            get { return _ControlSource01; }
            set
            {
                if (_ControlSource01 != value)
                {
                    _ControlSource01 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public byte ControlSense01
        {
            get { return _ControlSense01; }
            set
            {
                if (_ControlSense01 != value)
                {
                    _ControlSense01 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0007)]
        public IntegraMFXControlSources ControlSource02
        {
            get { return _ControlSource02; }
            set
            {
                if (_ControlSource02 != value)
                {
                    _ControlSource02 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0008)]
        public byte ControlSense02
        {
            get { return _ControlSense02; }
            set
            {
                if (_ControlSense02 != value)
                {
                    _ControlSense02 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0009)]
        public IntegraMFXControlSources ControlSource03
        {
            get { return _ControlSource03; }
            set
            {
                if (_ControlSource03 != value)
                {
                    _ControlSource03 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000A)]
        public byte ControlSense03
        {
            get { return _ControlSense03; }
            set
            {
                if (_ControlSense03 != value)
                {
                    _ControlSense03 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000B)]
        public IntegraMFXControlSources ControlSource04
        {
            get { return _ControlSource04; }
            set
            {
                if (_ControlSource04 != value)
                {
                    _ControlSource04 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public byte ControlSense04
        {
            get { return _ControlSense04; }
            set
            {
                if (_ControlSense04 != value)
                {
                    _ControlSense04 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000D)]
        public IntegraMFXControlAssigns ControlAssign01
        {
            get { return _ControlAssign01; }
            set
            {
                if (_ControlAssign01 != value)
                {
                    _ControlAssign01 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000E)]
        public IntegraMFXControlAssigns ControlAssign02
        {
            get { return _ControlAssign02; }
            set
            {
                if (_ControlAssign02 != value)
                {
                    _ControlAssign02 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000F)]
        public IntegraMFXControlAssigns ControlAssign03
        {
            get { return _ControlAssign03; }
            set
            {
                if (_ControlAssign03 != value)
                {
                    _ControlAssign03 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0010)]
        public IntegraMFXControlAssigns ControlAssign04
        {
            get { return _ControlAssign04; }
            set
            {
                if (_ControlAssign04 != value)
                {
                    _ControlAssign04 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0011)]
        public double this[int index]
        {
            get
            {
                return _Validator.Get(index, _Parameters[index]);
            }

            set
            {
                if (_Validator.Get(index, _Parameters[index]) != value)
                {
                    _Parameters[index] = _Validator.Set(index, value);

                    NotifyPropertyChanged("Item[]", index);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the parameter validator for the <see cref="Type"/>.
        /// </summary>
        private void SetValidator()
        {
            Debug.Print($"Set Validator: {Type}");
            switch(Type)
            {
                case IntegraMFXTypes.Equalizer: _Validator = new Equalizer(); break;

                    // TODO: Remove default? No validation?
                default: 
                    _Validator = new Thru();
                    break;
            }

            NotifyPropertyChanged(string.Empty);
        }

        #endregion

        #region Overrides: Model

        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            //base.SystemExclusiveReceived(sender, e);

            if (e.SystemExclusive.Address == Address)
            {
                if (e.SystemExclusive.Data.Length == Size)
                {
                    Debug.Print("***");
                    Initialize(e.SystemExclusive.Data);
                }
                else 
                {
                    IntegraAddress offset = new IntegraAddress(0x00000111);
                    if (e.SystemExclusive.Address.InRange(Address, (int)(Address + offset)))
                        // Parameter data received
                        ReceivedProperty(e.SystemExclusive);
                }
            }
            
        }
        /// <summary>
        /// Initializes the model with data.
        /// </summary>
        /// <param name="data">The data to initialize the model.</param>
        /// <returns>True if the model is initialized.</returns>
        /// <remarks><i>Sets the MFX parameter validator after initialization.</i></remarks>
        protected override bool Initialize(byte[] data)
        {
            // After initialization the MFX type
            if (base.Initialize(data))
            {
                SetValidator();
            }
            
            return IsInitialized;
        }

        protected internal override int GetModelHash()
        {
            // TODO: Check uniqueness
            return (int)(base.GetModelHash() | 0xFFF00F00);
            //return base.GetModelHash() & 0xFFF00F00;
        }

        #endregion
    }
}
