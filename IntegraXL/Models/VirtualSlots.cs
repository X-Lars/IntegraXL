using IntegraXL.Common;
using IntegraXL.Core;
using System.Windows.Input;

namespace IntegraXL.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 virtual slots model.
    /// </summary>
    /// <remarks>
    /// <b><i>The model is unable to catch changes made on the physical device.</i></b>
    /// </remarks>
    [Integra(0x0F000010, 0x00000000)]
    public sealed class VirtualSlots : IntegraModel
    {
        #region Constants

        /// <summary>
        /// Defines the address received when the expansions begin loading.
        /// </summary>
        private const uint VIRTUALSLOTS_LOADING = 0x0F003001;

        /// <summary>
        /// Defines the address received when the expansions finished loading.
        /// </summary>
        private const uint VIRTUALSLOTS_COMPLETE = 0x0F003002;

        #endregion

        #region Fields

        /// <summary>
        /// Stores the expansion of slot A.
        /// </summary>
        private IntegraExpansions _SlotA;

        /// <summary>
        /// Stores the expansion of slot B.
        /// </summary>
        private IntegraExpansions _SlotB;

        /// <summary>
        /// Stores the expansion of slot C.
        /// </summary>
        private IntegraExpansions _SlotC;

        /// <summary>
        /// Stores the expansion of slot D.
        /// </summary>
        private IntegraExpansions _SlotD;

        /// <summary>
        /// Stores the initial expansion selection.
        /// </summary>
        private IntegraExpansions[] _InitialExpansions = new IntegraExpansions[4];

        /// <summary>
        /// Stores the expansion selection for backup.
        /// </summary>
        private IntegraExpansions[] _BackupExpansions = new IntegraExpansions[4];

        /// <summary>
        /// Track if the current expansion consumes all four slots.
        /// </summary>
        /// <remarks>Only the INTEGRA-7 <see cref="IntegraExpansions.ExPCM"/> uses all four slots.</remarks>
        private bool _AllSlotsUsed;

        /// <summary>
        /// Tracks wheter the expansions are loading.
        /// </summary>
        private bool _IsLoading = false;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the expansions are loading or have completed loading.
        /// </summary>
        public event EventHandler VirtualSlotsLoading;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initialize a new <see cref="VirtualSlots"/> instance.
        /// </summary>
#pragma warning disable IDE0051 // Remove unused private members
        private VirtualSlots(Integra device) : base(device)
#pragma warning restore IDE0051 // Remove unused private members
        {
            StartupExpansions = device.CreateModel<StartupExpansions>();

            //InitializeStartupExpansions();
        }

        private async void InitializeStartupExpansions()
        {
            await Device.InitializeModel(StartupExpansions);
        }

        #endregion

        #region Commands

        #region Commands: Properties

        /// <summary>
        /// Provides a bindable command to load the current selection of expansions.
        /// </summary>
        public ICommand LoadCommand
        {
            get { return new UICommandAsync(Load, CanExecuteLoad, this); }
        }

        /// <summary>
        /// Provides a bindable command to unload all expansions.
        /// </summary>
        public ICommand UnloadCommand
        {
            get { return new UICommandAsync(Unload, CanExecuteUnload, this); }
        }

        /// <summary>
        /// Provides a bindable command to set the current selection as default on startup.
        /// </summary>
        public ICommand SetStartupCommand
        {
            get { return new UICommand(SetDefault); }
        }

        #endregion

        #region Commands: Validation

        /// <summary>
        /// Validates execution of the <see cref="LoadCommand"/>.
        /// </summary>
        /// <returns>True if the command can be executed, false otherwise.</returns>
        public bool CanExecuteLoad()
        {
            return SlotAIsDirty || SlotBIsDirty || SlotCIsDirty || SlotDIsDirty;
        }

        /// <summary>
        /// Validates execution of the <see cref="UnloadCommand"/>.
        /// </summary>
        /// <returns>True if the command can be executed, false otherwise.</returns>
        public bool CanExecuteUnload()
        {
            return _InitialExpansions[0] != IntegraExpansions.Off ||
                   _InitialExpansions[1] != IntegraExpansions.Off ||
                   _InitialExpansions[2] != IntegraExpansions.Off ||
                   _InitialExpansions[3] != IntegraExpansions.Off;
        }
       
        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets the startup expansions model.
        /// </summary>
        public StartupExpansions StartupExpansions { get; }

        /// <summary>
        /// Gets whether the expansion with the specified index is loaded.
        /// </summary>
        /// <param name="expansion">The expansion to check.</param>
        /// <returns>True if the expansion is loaded, false otherwise.</returns>
        public bool this[IntegraExpansions expansion]
        {
            get { return IsLoaded(expansion); }
        }

        /// <summary>
        /// Get or sets the expansion used in slot A.
        /// </summary>
        public IntegraExpansions SlotA
        {
            get { return _SlotA; }
            set
            {
                if (_SlotA != value)
                {
                    InvalidateExpansion(value);

                    _SlotA = value;

                    NotifyPropertyChanged(string.Empty);
                }
            }
        }

        /// <summary>
        /// Get or sets the expansion used in slot B.
        /// </summary>
        public IntegraExpansions SlotB
        {
            get { return _SlotB; }
            set
            {
                if (_SlotB == value)
                    return;

                InvalidateExpansion(value);

                _SlotB = value;

                NotifyPropertyChanged(string.Empty);
            }
        }

        /// <summary>
        /// Get or sets the expansion used in slot C.
        /// </summary>
        public IntegraExpansions SlotC
        {
            get { return _SlotC; }
            set
            {
                if (_SlotC == value)
                    return;

                InvalidateExpansion(value);

                _SlotC = value;

                NotifyPropertyChanged(string.Empty);
            }
        }

        /// <summary>
        /// Get or sets the expansion used in slot D.
        /// </summary>
        public IntegraExpansions SlotD
        {
            get { return _SlotD; }
            set
            {
                if (_SlotD == value)
                    return;

                InvalidateExpansion(value);

                _SlotD = value;

                NotifyPropertyChanged(string.Empty);
            }
        }

        /// <summary>
        /// Gets whether slot A has unsaved changes.
        /// </summary>
        /// <remarks><i>Can be used to mark slots in the UI.</i></remarks>
        public bool SlotAIsDirty
        {
            get { return _SlotA != _InitialExpansions[0]; }
        }

        /// <summary>
        /// Gets whether slot B has unsaved changes.
        /// </summary>
        /// <remarks><i>Can be used to mark slots in the UI.</i></remarks>
        public bool SlotBIsDirty
        {
            get { return _SlotB != _InitialExpansions[1]; }
        }

        /// <summary>
        /// Gets whether slot C has unsaved changes.
        /// </summary>
        /// <remarks><i>Can be used to mark slots in the UI.</i></remarks>
        public bool SlotCIsDirty
        {
            get { return _SlotC != _InitialExpansions[2]; }
        }

        /// <summary>
        /// Gets whether slot D has unsaved changes.
        /// </summary>
        /// <remarks><i>Can be used to mark slots in the UI.</i></remarks>
        public bool SlotDIsDirty
        {
            get { return _SlotD != _InitialExpansions[3]; }
        }

        /// <summary>
        /// Gets whether the specified expansion is loaded.
        /// </summary>
        /// <param name="expansion">The expansion to check.</param>
        /// <returns>True if the specified expansion is loaded, false otherwise.</returns>
        public bool IsLoaded(IntegraExpansions expansion)
        {
            if (SlotA == expansion && !SlotAIsDirty) return true;
            if (SlotB == expansion && !SlotBIsDirty) return true;
            if (SlotC == expansion && !SlotCIsDirty) return true;
            if (SlotD == expansion && !SlotDIsDirty) return true;

            return false;
        }

        /// <summary>
        /// Gets wheter the expansions are loading.
        /// </summary>
        public bool IsLoading
        {
            get { return _IsLoading; }

            private set
            {
                _IsLoading = value;
                NotifyPropertyChanged(string.Empty);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the current selection of expansions.
        /// </summary>
        public async Task<bool> Load() 
        {
            if (!SlotAIsDirty && !SlotBIsDirty && !SlotCIsDirty && !SlotDIsDirty)
                return true;

            _IsLoading = true;

            // Updates model's request with the actual slots
            Requests[0] = new IntegraRequest((byte)SlotA, (byte)SlotB, (byte)SlotC, (byte)SlotD);

            await Device.LoadExpansions(this);

            // After loading expansions set the initial expansion values to the actual ones
            _InitialExpansions[0] = SlotA;
            _InitialExpansions[1] = SlotB;
            _InitialExpansions[2] = SlotC;
            _InitialExpansions[3] = SlotD;

            NotifyPropertyChanged(string.Empty);

            return true;
        }
       
        /// <summary>
        /// Unloads all expansions.
        /// </summary>
        public async Task<bool> Unload() 
        {
            if (SlotA == IntegraExpansions.Off && SlotB == IntegraExpansions.Off && SlotC == IntegraExpansions.Off && SlotD == IntegraExpansions.Off)
                return true;

            // Updates the model's request with all slots turned off
            Requests[0] = new IntegraRequest();

            await Device.LoadExpansions(this);

            _InitialExpansions[0] = _SlotA = IntegraExpansions.Off;
            _InitialExpansions[1] = _SlotB = IntegraExpansions.Off;
            _InitialExpansions[2] = _SlotC = IntegraExpansions.Off;
            _InitialExpansions[3] = _SlotD = IntegraExpansions.Off;

            NotifyPropertyChanged(string.Empty);

            return true;
        }

        /// <summary>
        /// Sets the current selection of expansions as default.
        /// </summary>
        public void SetDefault() 
        { 
            StartupExpansions.SetStartup(_SlotA, _SlotB, _SlotC, _SlotD); 
        }

        /// <summary>
        /// Invalidates the expansion selection.
        /// </summary>
        /// <param name="expansion">The expansion to invalidate the slot against.</param>
        private void InvalidateExpansion(IntegraExpansions expansion)
        {
            InvalidateDuplicates(expansion);
            InvalidateMultiSlots(expansion);
        }

        /// <summary>
        /// Invalidates the virtual slots for duplicates.
        /// </summary>
        /// <param name="expansion">The expansion to invalidate.</param>
        /// <remarks><i>Turns the duplicate expansion off.</i></remarks>
        private void InvalidateDuplicates(IntegraExpansions expansion)
        {
            // If the expansion is already selected, deselect the expansion
            if (expansion != IntegraExpansions.Off && expansion != IntegraExpansions.ExPCM)
            {
                if (expansion == _SlotA) SlotA = IntegraExpansions.Off;
                if (expansion == _SlotB) SlotB = IntegraExpansions.Off;
                if (expansion == _SlotC) SlotC = IntegraExpansions.Off;
                if (expansion == _SlotD) SlotD = IntegraExpansions.Off;
            }
        }

        /// <summary>
        /// Invalidates the virtual slots for multi slot expansions.
        /// </summary>
        /// <param name="expansion">The expansion to invalidate.</param>
        /// <remarks><i>On multi slot selection the current slots are backed up, on deselection the previous slots are restored.</i></remarks>
        private void InvalidateMultiSlots(IntegraExpansions expansion)
        {
            // IMPORTANT: Use the backing fields to prevent recursion!

            if (expansion == IntegraExpansions.ExPCM)
            {
                // The selected expansion uses all four slots, backup the current selection
                _BackupExpansions[0] = _SlotA;
                _BackupExpansions[1] = _SlotB;
                _BackupExpansions[2] = _SlotC;
                _BackupExpansions[3] = _SlotD;

                _SlotA = _SlotB = _SlotC = _SlotD = IntegraExpansions.ExPCM;

                _AllSlotsUsed = true;

                NotifyPropertyChanged(string.Empty);
            }
            else if (_AllSlotsUsed)
            {
                // The previous expansion used all four slots, restore the previous selection
                _SlotA = _BackupExpansions[0];
                _SlotB = _BackupExpansions[1];
                _SlotC = _BackupExpansions[2];
                _SlotD = _BackupExpansions[3];

                _AllSlotsUsed = false;

                NotifyPropertyChanged(string.Empty);
            }
        }

        #endregion

        #region Overrides

        internal async override Task<bool> Initialize()
        {
            if (!StartupExpansions.IsInitialized)
                await StartupExpansions.Initialize();

            return await base.Initialize();
        }
        /// <summary>
        /// Handles system exclusive events by exact matching virtual slot specific addresses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            if(e.SystemExclusive.Address == Address)
            {
                Initialize(e.SystemExclusive.Data);
            }
            else if(e.SystemExclusive.Address == VIRTUALSLOTS_LOADING)
            {
                IsLoading = true;

                if (SynchronizationContext.Current != null)
                {
                    SynchronizationContext.Current.Post(x => VirtualSlotsLoading?.Invoke(this, new IntegraVirtualSlotsEventArgs(VirtualSlotsState.LoadStart)), null);
                }
                else
                {
                    VirtualSlotsLoading?.Invoke(this, new IntegraVirtualSlotsEventArgs(VirtualSlotsState.LoadStart));
                }
            }
            else if(e.SystemExclusive.Address == VIRTUALSLOTS_COMPLETE)
            {
                IsLoading = false;

                if (SynchronizationContext.Current != null)
                {
                    SynchronizationContext.Current.Post(x => VirtualSlotsLoading?.Invoke(this, new IntegraVirtualSlotsEventArgs(VirtualSlotsState.LoadComplete)), null);
                }
                else
                {
                    VirtualSlotsLoading?.Invoke(this, new IntegraVirtualSlotsEventArgs(VirtualSlotsState.LoadStart));
                }
            }
        }
        
        /// <summary>
        /// Initializes the model with data.
        /// </summary>
        /// <param name="data">The data to initialize the model.</param>
        /// <returns>True if the model is initialized, false otherwise.</returns>
        protected override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                _InitialExpansions[0] = _SlotA = (IntegraExpansions)data[0x00];

                if (_SlotA == IntegraExpansions.ExPCM)
                {
                    _InitialExpansions[1] = _SlotB = _SlotA;
                    _InitialExpansions[2] = _SlotC = _SlotA;
                    _InitialExpansions[3] = _SlotD = _SlotA;

                    _AllSlotsUsed = true;
                }
                else
                {
                    _InitialExpansions[1] = _SlotB = (IntegraExpansions)data[0x01];
                    _InitialExpansions[2] = _SlotC = (IntegraExpansions)data[0x02];
                    _InitialExpansions[3] = _SlotD = (IntegraExpansions)data[0x03];

                    _AllSlotsUsed = false;
                }

                NotifyPropertyChanged(string.Empty);

                IsInitialized = true;
            }

            return IsInitialized;
        }

        #endregion

        #region Enumerations

        /// <summary>
        /// Provides an enumerated list of available expansions to the UI.
        /// </summary>
        public IEnumerable<IntegraExpansions> Expansions
        {
            get { return Enum.GetValues(typeof(IntegraExpansions)).Cast<IntegraExpansions>(); }
        }

        #endregion
    }
}

