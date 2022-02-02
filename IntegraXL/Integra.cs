using IntegraXL.Common;
using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using IntegraXL.Models;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace IntegraXL
{
    public class Integra : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Stores the associated connection.
        /// </summary>
        private IntegraConnection _Connection;

        /// <summary>
        /// 
        /// </summary>
        private IntegraTaskManager _TaskManager = new();

        /// <summary>
        /// 
        /// </summary>
        private IntegraStatus _Status = new();

        /// <summary>
        /// 
        /// </summary>
        private ConcurrentDictionary<int, IntegraModel> _Models = new();

        /// <summary>
        /// Stores the <i>zero based</i> device ID.
        /// </summary>
        private byte _DeviceID;

        /// <summary>
        /// 
        /// </summary>
        private bool _IsInitialized;

        /// <summary>
        /// Stores the selected active part.
        /// </summary>
        private int _SelectedPart = 0;

        /// <summary>
        /// Stores the selected tone of the active part.
        /// </summary>
        private IntegraTone _SelectedTone;

        /// <summary>
        /// Stores the command to preview the active tone.
        /// </summary>
        private ICommand _PreviewCommand;

        private CancellationTokenSource _ModelCTS;
        private CancellationToken _ModelCancellationToken;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when a system exclusive is received.
        /// </summary>
        public event EventHandler<IntegraSystemExclusiveEventArgs>? SystemExclusiveReceived;

        /// <summary>
        /// Event raised when a different part is selected.
        /// </summary>
        public event EventHandler<IntegraPartChangedEventArgs>? PartChanged;

        /// <summary>
        /// Event raised when a different tone is selected.
        /// </summary>
        public event EventHandler<IntegraToneChangedEventArgs>? ToneChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <remarks><i>
        /// - Creates required models (Uninitialized)<br/>
        /// </i></remarks>
        public Integra()
        {
            CreateModels();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <remarks><i>
        /// - Creates required models (Uninitialized)<br/>
        /// - Sets the connection<br/>
        /// - Sets the device ID<br/>
        /// - Binds the connection event handlers<br/>
        /// - Initializes the task manager<br/>
        /// </i></remarks>
        public Integra(IntegraConnection connection) : this()
        {
            SetConnection(connection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <exception cref="IntegraException"></exception>
        /// <remarks><i>
        /// - Sets the connection<br/>
        /// - Sets the device ID<br/>
        /// - Binds the connection event handlers<br/>
        /// - Initializes the task manager<br/>
        /// </i></remarks>
        public void SetConnection(IntegraConnection connection)
        {
            if(_Connection != null)
            {
                // TODO: Connection Change
            }
                
            if (connection == null)
                throw new IntegraException($"[{nameof(Integra)}.{nameof(SetConnection)}]");

            DeviceID = connection.ID;

            _Connection = connection;
            _Connection.SystemExclusiveReceived += OnSystemExclusiveReceived;
            _Connection.ConnectionChanged += OnConnectionChanged;

            _TaskManager.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks><i>
        /// - Invalidates the connection<br/>
        /// - Sets the <see cref="IsConnected"/> property
        /// </i></remarks>
        public async Task<bool> Connect()
        {
            if (_Connection == null)
                return IsConnected = false;

            await _Connection.Invalidate();

            if(_Connection.Status == ConnectionStatus.Connected)
            {
                return IsConnected = true;
            }

            return IsConnected = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks><i>
        /// - Invalidates the connection if not connected<br/>
        /// - Initializes all uninitialized cached models<br/>
        /// - Sets the <see cref="IsInitialized"/> property<br/>
        /// </i></remarks>
        public async Task<bool> Initialize()
        {
            if (_Connection == null)
                return IsInitialized = false;

            if (_Connection.Status != ConnectionStatus.Connected)
            {
                await Connect();

                if (!IsConnected)
                    return IsInitialized = false;
            }

            foreach (var model in _Models.Values.Where(x => x.IsInitialized == false))
            {
                await model.Initialize();
            }

            return IsInitialized = true;
        }

        private void OnConnectionChanged(object? sender, IntegraConnectionStatusEventArgs e)
        {
            Debug.Print($"[{nameof(Integra)}] Connection Changed => {e.Status}");

            if(e.Status != ConnectionStatus.Connected)
            {
                // Cancel running tasks
                // Disconnect models
                // Mark models out of sync
            }
            else
            {
                // Cancel running tasks
                // Disconnect models
                // Mark models out of sync
            }
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets the device ID.
        /// </summary>
        public int DeviceID 
        { 
            // Device ID is zero based
            get => _DeviceID + 1; 

            private set
            {
                if(_DeviceID != value)
                {
                    _DeviceID = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _IsConnected;
        public bool IsConnected
        {
            get => _IsConnected;
            private set
            {
                if(_IsConnected != value)
                {
                    _IsConnected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets whether the nescesary models are initialized and the INTEGRA-7 is ready for operation.
        /// </summary>
        public bool IsInitialized
        {
            get => _IsInitialized;

            private set
            {
                if(_IsInitialized != value)
                {
                    _IsInitialized = value;
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }

        public IntegraStatus Status
        {
            get { return _Status; }
            set
            {
                if(_Status != value)
                {
                    _Status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets whether the tone preview is running.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public bool IsPreviewing { get; private set; }

        /// <summary>
        /// On get, gets wheter the tone preview is running.<br/>
        /// On set, starts or stops the tone preview on the active part based on a true or false value respectively.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public bool Preview
        {
            get => IsPreviewing;
            set
            {
                if (IsPreviewing != value)
                {
                    if (CanExecutePreview())
                    {
                        ExecutePreview();
                    }
                }
            }
        }

        #region Properties: INTEGRA-7

        /// <summary>
        /// Gets the setup model.
        /// </summary>
        public Setup? Setup { get; private set; }

        /// <summary>
        /// Gets the studio sets collection.
        /// </summary>
        /// <remarks><i>Use the <see cref="Setup.StudioSetPC"/> property to get or set the active studio set by index.</i></remarks>
        public StudioSets? StudioSets { get; private set; }

        /// <summary>
        /// Gets the studio set model.
        /// </summary>
        public StudioSet? StudioSet { get; private set;}

        /// <summary>
        /// Gets the virtual slots model.
        /// </summary>
        public VirtualSlots? VirtualSlots { get; private set; }

        /// <summary>
        /// Gets the collection of selected tones for all parts.
        /// </summary>
        public IntegraTones SelectedTones { get; private set; }

        /// <summary>
        /// Gets or sets the active part.
        /// </summary>
        /// <remarks>
        /// <i>Determines the indexed partial models returned from the studio set.</i><br/>
        /// <i>Determines the indexed partial model returned for the selected tone.</i><br/>
        /// <i>Can be used to notify and update UI applications.</i>
        /// </remarks>
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public int SelectedPart
        {
            get => _SelectedPart;
            set
            {
                if (_SelectedPart != value)
                {
                    var previous = _SelectedPart;
                    var preview  = IsPreviewing;

                    // Clamp to part index 0..15
                    value = Math.Min(value, 15);
                    value = Math.Max(value, 0);

                    if (preview)
                        Preview = false;

                    _SelectedPart = value;

                    if (preview)
                        Preview = true;

                    
                    PartChanged?.Invoke(this, new IntegraPartChangedEventArgs((Parts)value, (Parts)previous));

                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(SelectedTone));
                }
            }
        }

        public IntegraTone ToneInfo
        {
            get => SelectedTones[SelectedPart];
        }

        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public IBankSelect SelectedTone
        {
            get => SelectedTones[SelectedPart];
            set
            {
                if (SelectedTones[SelectedPart].Equals(value))
                    return;

                SelectedTones[SelectedPart].BankSelect = value;

                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ToneInfo));
            }
        }

        private TemporaryTone _TemporaryTone;

        public TemporaryTone TemporaryTone
        {
            get => _TemporaryTone;
            private set
            {
                if(_TemporaryTone != value)
                {
                    _TemporaryTone = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TemporaryTones TemporaryTones { get; private set; }
        #endregion

        #endregion

        #region Commands

        /// <summary>
        /// Provides a bindable UI command to toggle the tone preview on or off on the active part.
        /// </summary>
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public ICommand PreviewCommand
        {
            get
            {
                if (_PreviewCommand != null)
                    return _PreviewCommand;

                return _PreviewCommand = new UICommand(ExecutePreview, CanExecutePreview, this);
            }
        }

        #region Commands: CanExecute

        /// <summary>
        /// Gets wheter the <see cref="PreviewCommand"/> can be executed.
        /// </summary>
        /// <returns>True if the command can be executed.</returns>
        private bool CanExecutePreview()
        {
            return StudioSet.Part != null && StudioSet.Part.IsInitialized;
        }

        #endregion

        #region Commands: Implementation

        
        #endregion

        #endregion

        #region Methods

        private void CreateModels()
        {
            Setup         = CreateModel<Setup>();
            VirtualSlots  = CreateModel<VirtualSlots>();
            StudioSets    = CreateModel<StudioSets>();
            SelectedTones = CreateModel<IntegraTones>();
            StudioSet     = CreateModel<StudioSet>();
        }

        /// <summary>
        /// Initializes the nescesary models to operate.
        /// </summary>
        /// <returns>An awaitable <see cref="Task"/> that returns nothing.</returns>
        private async Task<bool> InitializeInstance()
        {

            Status.Task = "Initializing";

            Setup          = await GetModel<Setup>();
            VirtualSlots   = await GetModel<VirtualSlots>();
            StudioSets     = await GetModel<StudioSets>();
            SelectedTones  = await GetModel<IntegraTones>();
            StudioSet      = await GetModel<StudioSet>();

            //TemporaryTones = await GetModel<TemporaryTones>();

            //TemporaryTone = await GetModel<TemporaryTone>(Parts.Part01);
            //await TemporaryTone.Initialize();
            //TemporaryTone = await GetModel<TemporaryTone>(Parts.Part01);
            Status.Task = "Ready";
            return IsInitialized = true;
        }

        #region Model Instantiation

        /// <summary>
        /// Creates and caches a new INTEGRA-7 model or returns the model from cache if it exists.<br/>
        /// Maintains refrential integrity of models and prevents duplicates.
        /// </summary>
        /// <typeparam name="TModel">The model type specifier.</typeparam>
        /// <param name="part">The associated part, only required for <see cref="IntegraPartial"/> derived models.</param>
        /// <returns>A new or cached INTEGRA-7 model.</returns>
        /// <remarks><i>Newly created models are uninitialized, cached models are possibly already initialized with data.</i></remarks>
        internal TModel CreateModel<TModel>(Parts? part = null) where TModel : IntegraModel
        {
            IntegraModel? instance;

            if(typeof(TModel).IsSubclassOf(typeof(IntegraPartial<TModel>)))
            {
                Debug.Assert(part != null);

                instance = Activator.CreateInstance(typeof(TModel), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { this, part }, null) as IntegraModel;
            }
            else
            {
                instance = Activator.CreateInstance(typeof(TModel), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { this }, null) as IntegraModel;
            }

            Debug.Assert(instance != null);

            if (_Models.TryGetValue(instance.GetModelHash(), out IntegraModel? model))
            {
                Debug.Print($"[{nameof(Integra)}] {nameof(CreateModel)}<{typeof(TModel).Name}>({part}) From Cache");
                
                // IMPORTANT: Disconnect to remove the device event listener to free all references to the newly created instance?
                instance.Disconnect();
                instance = null;

                return (TModel)model;
            }

            if(!_Models.TryAdd(instance.GetModelHash(), instance))
            {
                throw new IntegraException("Integra model cache model");
            }

            Debug.Print($"[{nameof(Integra)}] {nameof(CreateModel)}<{typeof(TModel).Name}>({part}) New Cache Entry: 0x{instance.GetModelHash():X4}");

            return (TModel)instance;
        }

        /// <summary>
        /// Gets an initialized INTEGRA-7 model.
        /// </summary>
        /// <typeparam name="TModel">The model type specifier.</typeparam>
        /// <param name="part">The associated part, only required for <see cref="IntegraPartial"/> derived models.</param>
        /// <returns>An awaitable <see cref="Task"/> that returns an initialized INTEGRA-7 model.</returns>
        public async Task<TModel> GetModel<TModel>(Parts? part = null) where TModel : IntegraModel
        {
            Debug.Print($"[{nameof(Integra)}] {nameof(GetModel)}<{typeof(TModel).Name}>({part})");

            TModel model = CreateModel<TModel>(part);

            try
            {
                if (!model.IsInitialized)
                    await model.Initialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return model;
        }

        /// <summary>
        /// Gets an initialized INTEGRA-7 tone bank.
        /// </summary>
        /// <param name="tonebank">The tone bank to retreive.</param>
        /// <returns>An awaitable <see cref="Task"/> that returns an initialized INTEGRA-7 tone bank.</returns>
        public async Task<IntegraToneBank> GetToneBank(IntegraToneBanks tonebank)
        {
            Debug.Print($"[{nameof(Integra)}] {nameof(GetToneBank)}({tonebank})");

            Type? type = tonebank.ToneBankType();

            Debug.Assert(type != null);

            IntegraToneBank model = CreateToneBank(type);

            if(!model.IsInitialized)
                await model.Initialize();

            return model;
        }

        /// <summary>
        /// Creates and caches a new INTEGRA-7 tone bank or returns the tone bank from cache if it exists.<br/>
        /// Maintains refrential integrity of tone banks and prevents duplicates.
        /// </summary>
        /// <param name="type">The tone bank type specifier.</param>
        /// <returns>A new or cached INTEGRA-7 tone bank.</returns>
        /// <exception cref="IntegraException"></exception>
        /// <remarks><i>Newly created models are uninitialized, cached models are possibly already initialized with data.</i></remarks>
        private IntegraToneBank CreateToneBank(Type type)
        {
            IntegraToneBank? instance;

            Debug.Assert(type != null && type.IsSubclassOf(typeof(IntegraToneBank)));

            instance = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { this }, null) as IntegraToneBank;

            Debug.Assert(instance != null);

            if (_Models.TryGetValue(instance.GetModelHash(), out IntegraModel? model))
            {
                Debug.Print($"[{nameof(Integra)}] {nameof(CreateToneBank)}({type}) From Cache");

                // IMPORTANT: Disconnect to remove the device event listener to free all references to the newly created instance?
                instance.Disconnect();
                instance = null;

                return (IntegraToneBank)model;
            }

            if (!_Models.TryAdd(instance.GetModelHash(), instance))
            {
                throw new IntegraException("Integra model cache model");
            }

            Debug.Print($"[{nameof(Integra)}] {nameof(CreateToneBank)}({type}) New Cache Entry: 0x{instance.GetModelHash():X4}");

            return instance;
        }

        #endregion


        #region INTEGRA-7 Requests

        internal Task<bool> ReinitializeModel(IntegraModel model)
        {
            Debug.Print($"[{nameof(Integra)}] {nameof(ReinitializeModel)}<{model.GetType().Name}>()");

            if(model.IsInitialized)
                model.IsInitialized = false;

            return InitializeModel(model);
        }

        /// <summary>
        /// Initializes an INTEGRA-7 model with data.
        /// </summary>
        /// <param name="model">The model to initialize.</param>
        /// <returns>An awaitable <see cref="Task"/> that returns true if the model is initialized.</returns>
        internal Task<bool> InitializeModel(IntegraModel model)
        {
            Debug.Print($"[{nameof(Integra)}] {nameof(InitializeModel)}<{model.GetType().Name}>()");

            //if (!model.IsConnected)
            //    model.Connect();

            Task<bool> task = new (() =>
            {
                //InitProgress(model);
                Debug.Print($"[{nameof(IntegraTaskManager)}] Task: {model.GetType().Name}");

                foreach (var request in model.Requests)
                {
                    IntegraSystemExclusive systemExclusive = new (model.Address, request);
                    systemExclusive.DeviceID = _DeviceID;
                    _Connection.SendSystemExclusiveMessage(systemExclusive);
                }

                while (!model.IsInitialized)
                {
                    Thread.Sleep(100);
                }

                //CompleteProgress(model);
                return true;
            }, _ModelCancellationToken);

            task.ContinueWith((t) =>
            {
                Debug.Print($"[{nameof(Integra)}] {nameof(InitializeModel)}<{model.GetType().Name}>() CANCELLED");
            }, TaskContinuationOptions.OnlyOnCanceled);


            // TODO: Error handling / Time out to prevent application lock
            _TaskManager.Enqueue(task);

            return task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        internal Task<bool> LoadExpansions(VirtualSlots instance)
        {
            Task<bool> task = new (() =>
            {
                IntegraSystemExclusive systemExclusive = new(0x0F003000, instance.Requests[0]);
                systemExclusive.DeviceID = _DeviceID;
                _Connection.SendSystemExclusiveMessage(systemExclusive);

                while (instance.IsLoading)
                {
                    Thread.Sleep(100);
                }

                return true;
            });

            _TaskManager.Enqueue(task);

            return task;
        }


        #endregion

        /// <summary>
        /// Toggles the tone preview on or off.
        /// </summary>
        private void ExecutePreview()
        {
            if (IsPreviewing)
            {
                TransmitSystemExclusive(new IntegraSystemExclusive(0x0F002000, 0x00000000, new byte[] { 0x00 }));

                IsPreviewing = false;
            }
            else
            {
                TransmitSystemExclusive(new IntegraSystemExclusive(0x0F002000, 0x00000000, new byte[] { (byte)(SelectedPart + 1) }));

                IsPreviewing = true;
            }

            NotifyPropertyChanged(nameof(IsPreviewing));
            NotifyPropertyChanged(nameof(Preview));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemExclusive"></param>
        /// <remarks><i>Sets the system exclusive device ID.</i></remarks>
        internal void TransmitSystemExclusive(IntegraSystemExclusive systemExclusive)
        {
            // TODO: Check if taskmanager is nescessary for sending
            Task<bool> task = new (() =>
            {
                systemExclusive.DeviceID = _DeviceID;
                
                _Connection.SendSystemExclusiveMessage(systemExclusive);
                return true;
            });

            _TaskManager.Enqueue(task);
        }

        #endregion

        #region Event Handlers

        private void OnSystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            switch (e.SystemExclusive.Address[0])
            {
                case 0x01:
                    // TODO: Studio set changed
                    //Debug.Print($"0x01 Setup Address");
                    break;
                case 0x02:
                    //Debug.Print($"0x02 System Address");
                    break;
                case 0x18:
                    //Debug.Print($"0x18 Studio Set Address");
                    break;
                case byte x when x >= 0x19 && x < 0x1D:
                    //Debug.Print($"0x19 Temporary Tone Address");
                    break;
                default:
                    break;
            }

            SystemExclusiveReceived?.Invoke(this, e);
        }

        #endregion

        #region Interfaces: INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    // Database model
    // Cache Model
    // Send Model
    // Receive Model

    // Load
    // Save



}