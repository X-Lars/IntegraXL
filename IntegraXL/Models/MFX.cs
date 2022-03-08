using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using IntegraXL.Models.Parameters;
using System.ComponentModel;

namespace IntegraXL.Models
{
    [Integra(0x00000200, 0x00000111, 145)]
    public sealed class MFX : IntegraModel<MFX>, IParameterProvider<int>
    {
        private List<string> _Controls = new ();

        #region Fields: INTEGRA-7

        [Offset(0x0000)] private IntegraMFXTypes _Type;
        [Offset(0x0001)] private readonly byte RESERVED01;
        [Offset(0x0002)] private byte _ChorusSendLevel;
        [Offset(0x0003)] private byte _ReverbSendLevel;
        [Offset(0x0004)] private readonly byte RESERVED02;
        [Offset(0x0005)] private IntegraMFXControlSources _ControlSource1;
        [Offset(0x0006)] private byte _ControlSens1;
        [Offset(0x0007)] private IntegraMFXControlSources _ControlSource2;
        [Offset(0x0008)] private byte _ControlSens2;
        [Offset(0x0009)] private IntegraMFXControlSources _ControlSource3;
        [Offset(0x000A)] private byte _ControlSens3;
        [Offset(0x000B)] private IntegraMFXControlSources _ControlSource4;
        [Offset(0x000C)] private byte _ControlSens4;
        [Offset(0x000D)] private byte _ControlAssign1;
        [Offset(0x000E)] private byte _ControlAssign2;
        [Offset(0x000F)] private byte _ControlAssign3;
        [Offset(0x0010)] private byte _ControlAssign4;
        [Offset(0x0011)] private readonly int[] _Parameters = new int[32];

        #endregion

        #region Constructor

        public MFX(TemporaryTone temporaryTone) : base(temporaryTone.Device)
        {

            IsEditable = temporaryTone.IsEditable;

            if(!IsEditable)
            {
                IsInitialized = true;
                Type = IntegraMFXTypes.Thru;
                //this.SetMFXProvider();
                //_Controls = this.GetMFXControls();
                SetParameterProvider();
            }

            PropertyChanged += MFXPropertyChanged;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets wheter the MFX is editable.
        /// </summary>
        public bool IsEditable { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Controls => _Controls;

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public IntegraMFXTypes Type
        {
            get => _Type;
            set
            {
                if (_Type != value)
                {
                    _Type = value;

                    NotifyPropertyChanged();
                    //_Controls = this.GetMFXControls();
                    ReinitializeAsync();
                }
            }
        }

        [Offset(0x0002)]
        public byte ChorusSendLevel
        {
            get => _ChorusSendLevel;
            set
            {
                if (_ChorusSendLevel != value)
                {
                    _ChorusSendLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0003)]
        public byte ReverbSendLevel
        {
            get => _ReverbSendLevel;
            set
            {
                if (_ReverbSendLevel != value)
                {
                    _ReverbSendLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0005)]
        public IntegraMFXControlSources ControlSource1
        {
            get => _ControlSource1;
            set
            {
                if (_ControlSource1 != value)
                {
                    _ControlSource1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public int ControlSens1
        {
            get => _ControlSens1.Deserialize(64);
            set
            {
                if (ControlSens1 != value)
                {
                    _ControlSens1 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0007)]
        public IntegraMFXControlSources ControlSource2
        {
            get => _ControlSource2;
            set
            {
                if (_ControlSource2 != value)
                {
                    _ControlSource2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0008)]
        public int ControlSens2
        {
            get => _ControlSens2.Deserialize(64);
            set
            {
                if (ControlSens2 != value)
                {
                    _ControlSens2 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0009)]
        public IntegraMFXControlSources ControlSource3
        {
            get => _ControlSource3;
            set
            {
                if (_ControlSource3 != value)
                {
                    _ControlSource3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000A)]
        public int ControlSens3
        {
            get => _ControlSens3.Deserialize(64);
            set
            {
                if (ControlSens3 != value)
                {
                    _ControlSens3 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000B)]
        public IntegraMFXControlSources ControlSource4
        {
            get => _ControlSource4;
            set
            {
                if (_ControlSource4 != value)
                {
                    _ControlSource4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public int ControlSens4
        {
            get => _ControlSens4.Deserialize(64);
            set
            {
                if (ControlSens4 != value)
                {
                    _ControlSens4 = value.Serialize(64).Clamp(1, 127);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000D)]
        public byte ControlAssign1
        {
            get => _ControlAssign1;
            set
            {
                if (_ControlAssign1 != value)
                {
                    _ControlAssign1 = value.Clamp(0, (byte)_Controls.Count);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000E)]
        public byte ControlAssign2
        {
            get => _ControlAssign2;
            set
            {
                if (_ControlAssign2 != value)
                {
                    _ControlAssign2 = value.Clamp(0, (byte)_Controls.Count);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000F)]
        public byte ControlAssign3
        {
            get => _ControlAssign3;
            set
            {
                if (_ControlAssign3 != value)
                {
                    _ControlAssign3 = value.Clamp(0, (byte)_Controls.Count);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0010)]
        public byte ControlAssign4
        {
            get => _ControlAssign4;
            set
            {
                if (_ControlAssign4 != value)
                {
                    _ControlAssign4 = value.Clamp(0, (byte)_Controls.Count);
                    NotifyPropertyChanged();
                }
            }
        }

        // IParameterProvider
        [Offset(0x0011)]
        public int this[int index]
        {
            // TODO: Access only via parameter provider
            get => _Parameters[index];

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

        #region Methods

        /// <summary>
        /// Event raised when the MFX type is changed.
        /// </summary>
        public event EventHandler<IntegraTypeChangedEventArgs>? TypeChanged;

        /// <summary>
        /// Gets the MFX parameters.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public IntegraParameterMapper<int>? Parameters { get; internal set; }

        /// <summary>
        /// Sets the parameter provider based on the <see cref="Type"/>.
        /// </summary>
        private void SetParameterProvider()
        {
            // TODO: Use extension method
            //this.SetMFXProvider();

            switch(Type)
            {
                case IntegraMFXTypes.Equalizer: Parameters = new Equalizer(this); break;
                    // TODO: Remove default? No validation?
                default: 
                    Parameters = new Thru(this);
                    break;
            }

            _Controls = this.GetMFXControls();

            //NotifyPropertyChanged(nameof(Controls));
            NotifyPropertyChanged(string.Empty);
        }

        #endregion

        #region Overrides: Model

        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            base.SystemExclusiveReceived(sender, e);

            //if (e.SystemExclusive.Address == Address)
            //{
            //    if (e.SystemExclusive.Data.Length == Size)
            //    {
            //        //Debug.Print("*** MFX: Full ***");
            //        Initialize(e.SystemExclusive.Data);
            //    }
            //    else
            //    {
            //        IntegraAddress offset = new IntegraAddress(0x00000111);
            //        if (e.SystemExclusive.Address.InRange(Address, (int)(Address + offset)))
            //        {
            //            //Debug.Print("*** MFX: Parameters ***");
            //            // Parameter data received
            //            ReceivedProperty(e.SystemExclusive);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Initializes the model with data.
        /// </summary>
        /// <param name="data">The data to initialize the model.</param>
        /// <returns>True if the model is initialized.</returns>
        /// <remarks><i>Sets the MFX parameter provider after initialization.</i></remarks>
        internal override bool Initialize(byte[] data)
        {
            if (base.Initialize(data))
            {
                SetParameterProvider();
            }
            
            return IsInitialized;
        }

        private void MFXPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Type))
            {
                SetParameterProvider();
            }
        }

        internal protected override int GetUID()
        {
            // TODO: Check uniqueness
            return (int)(base.GetUID() | 0xFFF00F00);
        }

        #endregion

        #region Enumerations

        public static IEnumerable<IntegraMFXTypes> Types => Enum.GetValues(typeof(IntegraMFXTypes)).Cast<IntegraMFXTypes>(); 

        #endregion
    }
}
