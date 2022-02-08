using IntegraXL.Core;
using IntegraXL.Extensions;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace IntegraXL.Models
{
    [Integra(0x01000000, 0x00000038)]
    public sealed class Setup : IntegraModel<Setup>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private IntegraSoundModes _SoundMode;
        [Offset(0x0004)] private byte _StudioSetMSB;
        [Offset(0x0005)] private byte _StudioSetLSB;
        [Offset(0x0006)] private byte _StudioSetPC;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="Setup"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the model.</param>
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "The class is created by reflection.")]
        private Setup(Integra device) : base(device) { }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public IntegraSoundModes SoundMode
        {
            get { return _SoundMode; }
            internal set
            {
                if (_SoundMode != value)
                {
                    _SoundMode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0004)]
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public byte StudioSetMSB
        {
            get { return _StudioSetMSB; }
            internal set
            {
                if (_StudioSetMSB != value)
                {
                    _StudioSetMSB = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0005)]
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public byte StudioSetLSB
        {
            get { return _StudioSetLSB; }
            internal set
            {
                if (_StudioSetLSB != value)
                {
                    _StudioSetLSB = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public byte StudioSetPC
        {
            get { return _StudioSetPC; }
            set
            {
                if (_StudioSetPC != value)
                {
                    _StudioSetPC = value.Clamp(0, 63);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
    }
}
