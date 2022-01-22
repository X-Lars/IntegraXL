using IntegraXL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 startup expansions model.
    /// </summary>
    /// <remarks>
    /// <b><i>The model is unable to catch changes made on the physical device.</i></b>
    /// </remarks>
    [Integra(0x0F000011, 0x00000000)]
    public sealed class StartupExpansions : IntegraModel
    {
        #region Fields

        [Offset(0x0000)] private byte[] _Expansions = new byte[4];

        #endregion

        #region Constructor

        private StartupExpansions(Integra device) : base(device) { }

        #endregion

        #region Properties

        [Offset(0x0000)]
        public IntegraExpansions SlotA
        {
            get { return (IntegraExpansions)_Expansions[0]; }
            set
            {
                _Expansions[0] = (byte)value;
                SetStartup();
            }
        }

        public IntegraExpansions SlotB
        {
            get { return (IntegraExpansions)_Expansions[1]; }
            set
            {
                _Expansions[1] = (byte)value;
                SetStartup();
            }
        }

        public IntegraExpansions SlotC
        {
            get { return (IntegraExpansions)_Expansions[2]; }
            set
            {
                _Expansions[2] = (byte)value;
                SetStartup();
            }
        }

        public IntegraExpansions SlotD
        {
            get { return (IntegraExpansions)_Expansions[3]; }
            set
            {
                _Expansions[3] = (byte)value;
                SetStartup();
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Handles system exclusive events by exact matching the address only.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks><i>Override because the request doesn't represent the size of the model.</i></remarks>
        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            if (!IsInitialized)
                if (e.SystemExclusive.Address == Address)
                    Initialize(e.SystemExclusive.Data);
        }

        /// <summary>
        /// Sets the expansions loaded at startup.
        /// </summary>
        /// <param name="slotA">The default expansion for slot A.</param>
        /// <param name="slotB">The default expansion for slot B.</param>
        /// <param name="slotC">The default expansion for slot C.</param>
        /// <param name="slotD">The default expansion for slot D.</param>
        public void SetStartup(IntegraExpansions slotA, IntegraExpansions slotB, IntegraExpansions slotC, IntegraExpansions slotD)
        {
            _Expansions[0] = (byte)slotA;
            _Expansions[1] = (byte)slotB;
            _Expansions[2] = (byte)slotC;
            _Expansions[3] = (byte)slotD;

            NotifyPropertyChanged(nameof(SlotA));
            NotifyPropertyChanged(string.Empty);
        }

        /// <summary>
        /// Sets the expansions load at startup.
        /// </summary>
        private void SetStartup()
        {
            NotifyPropertyChanged(nameof(SlotA));
            NotifyPropertyChanged(string.Empty);
        }
       
        #endregion
    }
}
