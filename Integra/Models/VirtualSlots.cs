using Integra.Common;
using Integra.Core;
using MidiXL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integra.Models
{
    public sealed class VirtualSlots : IntegraBase<VirtualSlots>
    {
        #region Constants

        /// <summary>
        /// An <see cref="uint"/> representing the address to use to retreive the currently loaded INTEGRA-7 expansions.
        /// </summary>
        private const uint LOADED_EXPANSIONS = 0x0F000010;

        /// <summary>
        /// An <see cref="uint"/> representing the address used to store the INTEGRA-7 start up expansions.
        /// </summary>
        private const uint STARTUP_EXPANSIONS = 0x0F000011;

        /// <summary>
        /// An <see cref="uint"/> representing the address to use to load and unload INTEGRA-7 expansions.
        /// </summary>
        private const uint LOAD_EXPANSIONS = 0x0F003000;

        /// <summary>
        /// An <see cref="uint"/> representing the address received from the INTEGRA-7 to notify that the expansions are loading.
        /// </summary>
        private const uint START_LOADING_EXPANSIONS = 0x0F003001;

        /// <summary>
        /// An <see cref="uint"/> representing the address received from the INTEGRA-7 to notify that the expansions are finished loading.
        /// </summary>
        private const uint FINISHED_LOADING_EXPANSIONS = 0x0F003002;

        #endregion

        #region Fields

        private IntegraExpansions _SlotA;
        private IntegraExpansions _SlotB;
        private IntegraExpansions _SlotC;
        private IntegraExpansions _SlotD;

        /// <summary>
        /// Track if the expansion consumes all four slots.
        /// </summary>
        /// <remarks>Only the INTEGRA-7 <see cref="IntegraExpansions.Exp19"/> uses all four slots.</remarks>
        private bool _AllSlotsUsed;

        /// <summary>
        /// Stores the initial expansions before changes are made.
        /// </summary>
        private IntegraExpansions[] _InitialExpansions = new IntegraExpansions[4];

        /// <summary>
        /// Stores the selected expansions when an expansion is selected that uses all four slots, to be able to restore their values.
        /// </summary>
        private IntegraExpansions[] _BackupExpansions = new IntegraExpansions[4];

        /// <summary>
        /// Stores the actual selected expansions.
        /// </summary>
        private IntegraExpansions[] _ActualExpansions = new IntegraExpansions[4];

        #endregion

        #region Events

        //public event EventHandler<EventArgs> Loading;
        //public event EventHandler<EventArgs> Complete;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initialize a new <see cref="VirtualSlots"/> instance.
        /// </summary>
        public VirtualSlots() : base(LOADED_EXPANSIONS, 0x00000000)
        {
            Debug.Print($"[{nameof(VirtualSlots)}]");

            Name = "Virtual Slots";
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets a bindable <see cref="ICommand"/> to load the currently selected expansions.
        /// </summary>
        [Bindable(BindableSupport.Yes)]
        public ICommand LoadCommand
        {
            get { return new UICommand(new Action<object>(Load)); }
        }

        /// <summary>
        /// Gets a bindable <see cref="ICommand"/> to unload all expansions.
        /// </summary>
        [Bindable(BindableSupport.Yes)]
        public ICommand UnloadCommand
        {
            get { return new UICommand(new Action<object>(Unload)); }
        }

        /// <summary>
        /// Gets a bindable <see cref="ICommand"/> to set the currently selected expansions as default on start up.
        /// </summary>
        [Bindable(BindableSupport.Yes)]
        public ICommand SetStartupCommand
        {
            get { return new UICommand(new Action<object>(SetStartup)); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets whether the expansion with the specified index is loaded.
        /// </summary>
        /// <param name="expansion">An <see cref="IntegraExpansions"/> specifying the expansion to check.</param>
        /// <returns>A <see cref="bool"/> containing true if the <paramref name="expansion"/> is loaded.</returns>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public bool this[IntegraExpansions expansion]
        {
            get { return IsLoaded(expansion); }
        }

        /// <summary>
        /// Get or sets the expansion used in slot A.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public IntegraExpansions SlotA
        {
            get { return _SlotA; }
            set
            {
                if (_SlotA == value)
                    return;

                InvalidateDuplicateExpansions(value);
                InvalidateSlots(value);

                _SlotA = _ActualExpansions[0] = value;

                NotifyPropertyChanged(nameof(SlotA), false);
                NotifyPropertyChanged(nameof(SlotAIsDirty), false);
            }
        }

        /// <summary>
        /// Get or sets the expansion used in slot B.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public IntegraExpansions SlotB
        {
            get { return _SlotB; }
            set
            {
                if (_SlotB == value)
                    return;

                InvalidateDuplicateExpansions(value);
                InvalidateSlots(value);

                _SlotB = _ActualExpansions[1] = value;

                NotifyPropertyChanged(nameof(SlotB), false);
                NotifyPropertyChanged(nameof(SlotBIsDirty), false);
            }
        }

        /// <summary>
        /// Get or sets the expansion used in slot C.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public IntegraExpansions SlotC
        {
            get { return _SlotC; }
            set
            {
                if (_SlotC == value)
                    return;

                InvalidateDuplicateExpansions(value);
                InvalidateSlots(value);

                _SlotC = _ActualExpansions[2] = value;

                NotifyPropertyChanged(nameof(SlotC), false);
                NotifyPropertyChanged(nameof(SlotCIsDirty), false);
            }
        }

        /// <summary>
        /// Get or sets the expansion used in slot D.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public IntegraExpansions SlotD
        {
            get { return _SlotD; }
            set
            {
                if (_SlotD == value)
                    return;

                InvalidateDuplicateExpansions(value);
                InvalidateSlots(value);

                _SlotD = _ActualExpansions[3] = value;

                NotifyPropertyChanged(nameof(SlotD), false);
                NotifyPropertyChanged(nameof(SlotDIsDirty), false);
            }
        }

        /// <summary>
        /// Gets whether slot A has unsaved changes.
        /// </summary>
        /// <remarks>Can be used to mark slots in the UI.</remarks>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public bool SlotAIsDirty
        {
            get { return _SlotA != _InitialExpansions[0]; }
        }

        /// <summary>
        /// Gets whether slot B has unsaved changes.
        /// </summary>
        /// <remarks>Can be used to mark slots in the UI.</remarks>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public bool SlotBIsDirty
        {
            get { return _SlotB != _InitialExpansions[1]; }
        }

        /// <summary>
        /// Gets whether slot C has unsaved changes.
        /// </summary>
        /// <remarks>Can be used to mark slots in the UI.</remarks>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public bool SlotCIsDirty
        {
            get { return _SlotC != _InitialExpansions[2]; }
        }

        /// <summary>
        /// Gets whether slot D has unsaved changes.
        /// </summary>
        /// <remarks>Can be used to mark slots in the UI.</remarks>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public bool SlotDIsDirty
        {
            get { return _SlotD != _InitialExpansions[3]; }
        }

        /// <summary>
        /// Gets if the specified expansion is loaded.
        /// </summary>
        /// <param name="expansion">The expansion to check.</param>
        /// <returns>True if the specified expansion is loaded, false otherwise.</returns>
        public bool IsLoaded(IntegraExpansions expansion)
        {
            if (SlotA == expansion && !SlotAIsDirty)
                return true;

            if (SlotB == expansion && !SlotBIsDirty)
                return true;

            if (SlotC == expansion && !SlotCIsDirty)
                return true;

            if (SlotD == expansion && !SlotDIsDirty)
                return true;

            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Request the INTEGRA-7 to load the current selection of expansions.
        /// </summary>
        /// <param name="sender">The caller of the method.</param>
        private void Load(object sender)
        {
            if (!SlotAIsDirty && !SlotBIsDirty && !SlotCIsDirty && !SlotDIsDirty)
                return;

            IntegraSystemExclusive systemExclusive = new IntegraSystemExclusive(LOAD_EXPANSIONS, new byte[] { (byte)_SlotA, (byte)_SlotB, (byte)_SlotC, (byte)_SlotD });

            Device.Instance.SendSystemExclusive(systemExclusive);

            // After loading expansions set the initial expansion values to the actual ones
            _InitialExpansions[0] = SlotA;
            _InitialExpansions[1] = SlotB;
            _InitialExpansions[2] = SlotC;
            _InitialExpansions[3] = SlotD;

            NotifyPropertyChanged(string.Empty, false);
            NotifyPropertyChanged("Item[]");
        }

        /// <summary>
        /// Request the Integra to unload all expansions.
        /// </summary>
        /// <param name="sender">The caller of the method.</param>
        private void Unload(object sender)
        {
            if (SlotA == IntegraExpansions.Off && SlotB == IntegraExpansions.Off && SlotC == IntegraExpansions.Off && SlotD == IntegraExpansions.Off)
                return;

            IntegraSystemExclusive systemExclusive = new IntegraSystemExclusive(LOAD_EXPANSIONS, 0x00000000);

            Device.Instance.SendSystemExclusive(systemExclusive);

            _InitialExpansions[0] = SlotA = IntegraExpansions.Off;
            _InitialExpansions[1] = SlotB = IntegraExpansions.Off;
            _InitialExpansions[2] = SlotC = IntegraExpansions.Off;
            _InitialExpansions[3] = SlotD = IntegraExpansions.Off;

            // Notifies is dirty for all slots false
            NotifyPropertyChanged(string.Empty, false);
            NotifyPropertyChanged("Item[]");
        }

        /// <summary>
        /// Request the Integra to set the currently selected expansions as default startup expansions.
        /// </summary>
        /// <param name="sender">The caller of the method.</param>
        private void SetStartup(object sender)
        {
            IntegraSystemExclusive systemExclusive = new IntegraSystemExclusive(STARTUP_EXPANSIONS, 0x00, new byte[] { (byte)_SlotA, (byte)_SlotB, (byte)_SlotC, (byte)_SlotD });

            Device.Instance.SendSystemExclusive(systemExclusive);
        }

        /// <summary>
        /// Invalidates any slot to <see cref="IntegraExpansions.Off"/> when it contains the currently selected expansion to prevent duplicates.
        /// </summary>
        /// <param name="expansion">The expansion to invalidate the slots against.</param>
        private void InvalidateDuplicateExpansions(IntegraExpansions expansion)
        {
            // If the expansion is alread in another slot, turn that slot off
            if (expansion != IntegraExpansions.Off || expansion != IntegraExpansions.Exp19)
            {
                if (expansion == _SlotA)
                    SlotA = IntegraExpansions.Off;

                if (expansion == _SlotB)
                    SlotB = IntegraExpansions.Off;

                if (expansion == _SlotC)
                    SlotC = IntegraExpansions.Off;

                if (expansion == _SlotD)
                    SlotD = IntegraExpansions.Off;
            }
        }

        /// <summary>
        /// Invalidates any slot if <see cref="IntegraExpansions.Exp19"/> is or previously was selected.
        /// On selection, slot data is saved, on deselection the values are restored.
        /// </summary>
        /// <param name="expansion">The expansion to invalidate the slots against.</param>
        private void InvalidateSlots(IntegraExpansions expansion)
        {
            if (expansion == IntegraExpansions.Exp19)
            {
                // The selected expansion uses all four slots, save the current values to be able to restore them
                Array.Copy(_ActualExpansions, _BackupExpansions, 4);

                // Set all slots to the same expansion, use the backing field and not the property to prevent recursion
                _SlotA = _SlotB = _SlotC = _SlotD = IntegraExpansions.Exp19;

                _AllSlotsUsed = true;

                NotifyPropertyChanged(string.Empty, false);
                NotifyPropertyChanged("Item[]");
            }
            else if (_AllSlotsUsed)
            {
                // The previous expansion used all four slots, restore the backup data
                Array.Copy(_BackupExpansions, _ActualExpansions, 4);

                // Set all slots to the same expansion, use the backing field and not the property to prevent recursion
                _SlotA = _BackupExpansions[0];
                _SlotB = _BackupExpansions[1];
                _SlotC = _BackupExpansions[2];
                _SlotD = _BackupExpansions[3];

                _AllSlotsUsed = false;

                NotifyPropertyChanged(string.Empty, false);
                NotifyPropertyChanged("Item[]");

                // Prevent restoring of a duplicate expansion
                InvalidateDuplicateExpansions(expansion);
            }
        }
        #endregion

        #region Overrides

        internal override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            if (syx.Address == this.Address)
            {
                Initialize(syx.Data);
            }
            else if (syx.Address == START_LOADING_EXPANSIONS)
            {
                Device.Instance.ReportInit(this, new StatusMessage("Loading Expansions", "Please wait...", 0, "Loading"));

                //Loading?.Invoke(this, new EventArgs());
            }
            else if (syx.Address == FINISHED_LOADING_EXPANSIONS)
            {
                Device.Instance.ReportComplete(this, new StatusMessage("Loading Expansions", "Loaded", 100, "Done"));

                // Raise notify property changed for the indexer property
                NotifyPropertyChanged("Item[]", false);

                //Complete?.Invoke(this, new EventArgs());
            }
            else if (syx.Address == STARTUP_EXPANSIONS)
            {

            }
        }

        protected override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                _InitialExpansions[0] = _ActualExpansions[0] = _SlotA = (IntegraExpansions)data[0x00];

                if (_SlotA == IntegraExpansions.Exp19)
                {
                    _InitialExpansions[1] = _ActualExpansions[1] = _SlotB = _SlotA;
                    _InitialExpansions[2] = _ActualExpansions[2] = _SlotC = _SlotA;
                    _InitialExpansions[3] = _ActualExpansions[3] = _SlotD = _SlotA;

                    _AllSlotsUsed = true;
                }
                else
                {
                    _InitialExpansions[1] = _ActualExpansions[1] = _SlotB = (IntegraExpansions)data[0x01];
                    _InitialExpansions[2] = _ActualExpansions[2] = _SlotC = (IntegraExpansions)data[0x02];
                    _InitialExpansions[3] = _ActualExpansions[3] = _SlotD = (IntegraExpansions)data[0x03];
                }

                NotifyPropertyChanged(string.Empty, false);

                IsInitialized = true;
            }

            return IsInitialized;
        }


        #endregion

        #region Enumerations

        /// <summary>
        /// Provides the enumerated list of available <see cref="Expansions"/> to the UI.
        /// </summary>
        public IEnumerable<IntegraExpansions> Expansions
        {
            get { return Enum.GetValues(typeof(IntegraExpansions)).Cast<IntegraExpansions>(); }
        }

        #endregion
    }
}

