using IntegraXL.Core;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace IntegraXL.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 startup expansions model.
    /// </summary>
    /// <remarks>
    /// <b>Important</b><br/>
    /// <i>The physical device doesn't transmit changes.</i><br/>
    /// <i>Do not modify the startup settings during program execution.</i><br/>
    /// </remarks>
    [Integra(0x0F000011, 0x00000000)]
    public sealed class StartupExpansions : IntegraModel<StartupExpansions>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private readonly byte[] _Expansions = new byte[4];

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="StartupExpansions"/> instance.
        /// </summary>
        /// <param name="device">The device to connect the model.</param>
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "The class is created by reflection.")]
        private StartupExpansions(Integra device) : base(device) { }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public IntegraExpansions SlotA
        {
            get { return (IntegraExpansions)_Expansions[0]; }
            set
            {
                if (_Expansions[0] != (byte)value)
                {
                    _Expansions[0] = (byte)value;
                    SetStartup();
                }
            }
        }

        //[Offset(0x0001)] Captured by SlotA
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public IntegraExpansions SlotB
        {
            get { return (IntegraExpansions)_Expansions[1]; }
            set
            {
                if (_Expansions[1] != (byte)value)
                {
                    _Expansions[1] = (byte)value;
                    SetStartup();
                }
            }
        }

        //[Offset(0x0002)] Captured by SlotA
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public IntegraExpansions SlotC
        {
            get { return (IntegraExpansions)_Expansions[2]; }
            set
            {
                if (_Expansions[2] != (byte)value)
                {
                    _Expansions[2] = (byte)value;
                    SetStartup();
                }
            }
        }

        //[Offset(0x0003)] Captured by SlotA
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public IntegraExpansions SlotD
        {
            get { return (IntegraExpansions)_Expansions[3]; }
            set
            {
                if (_Expansions[3] != (byte)value)
                {
                    _Expansions[3] = (byte)value;
                    SetStartup();
                }
            }
        }

        #endregion

        #region Overrides: Model

        /// <summary>
        /// Handles received system exclusive messages by exact address match only.
        /// </summary>
        /// <param name="sender">The device that raised the event.</param>
        /// <param name="e">The event's associated data.</param>
        /// <remarks><i>Override because the request doesn't represent the size of the model.</i></remarks>
        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            if (!IsInitialized)
                if (e.SystemExclusive.Address == Address)
                    Initialize(e.SystemExclusive.Data);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the expansions to load on startup.
        /// </summary>
        /// <param name="slotA">The startup expansion for slot A.</param>
        /// <param name="slotB">The startup expansion for slot B.</param>
        /// <param name="slotC">The startup expansion for slot C.</param>
        /// <param name="slotD">The startup expansion for slot D.</param>
        public void SetStartup(IntegraExpansions slotA, IntegraExpansions slotB, IntegraExpansions slotC, IntegraExpansions slotD)
        {
            _Expansions[0] = (byte)slotA;
            _Expansions[1] = (byte)slotB;
            _Expansions[2] = (byte)slotC;
            _Expansions[3] = (byte)slotD;

            SetStartup();
        }

        /// <summary>
        /// Sets the expansions to load on startup.
        /// </summary>
        private void SetStartup()
        {
            NotifyPropertyChanged(nameof(SlotA)); // Transmit changes for all slots
            NotifyPropertyChanged(string.Empty);
        }
       
        #endregion
    }
}
