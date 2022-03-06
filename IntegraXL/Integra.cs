﻿using IntegraXL.Common;
using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.File;
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
        private IntegraConnection? _Connection;

        /// <summary>
        /// 
        /// </summary>
        private readonly IntegraTaskManager _TaskManager = new();

        /// <summary>
        /// 
        /// </summary>
        private IntegraStatus _Status;

        /// <summary>
        /// 
        /// </summary>
        private readonly ConcurrentDictionary<int, IntegraModel> _Models = new();

        /// <summary>
        /// Stores the <i>zero based</i> device ID.
        /// </summary>
        private byte _DeviceID;

        /// <summary>
        /// 
        /// </summary>
        private bool _IsInitialized;

        /// <summary>
        /// 
        /// </summary>
        private bool _IsConnected;

        /// <summary>
        /// Stores the selected active part.
        /// </summary>
        private Parts _SelectedPart = Parts.Part01;

        /// <summary>
        /// Stores the selected tone of the active part.
        /// </summary>
        private IntegraTone _SelectedTone;

        /// <summary>
        /// Stores the command to preview the active tone.
        /// </summary>
        private ICommand _PreviewCommand;

        /// <summary>
        /// 
        /// </summary>
        internal static readonly SynchronizationContext? UIContext = SynchronizationContext.Current;

        /// <summary>
        /// Stores the cancellation token source that cancels all task.
        /// </summary>
        private CancellationTokenSource _CTS = new();

        private IProgress<IntegraStatus> _Progress;

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
            _Status = new();
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
            if (connection == null)
                throw new IntegraException($"[{nameof(Integra)}.{nameof(SetConnection)}]");

            if (_Connection != null)
            {
                // TODO: Connection Change
                return;
            }

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

            return IsConnected = _Connection.IsConnected;
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
        public async Task<bool> Initialize(IProgress<IntegraStatus>? progress = null)
        {
            _Progress = progress;

            if (_Connection == null)
                return IsInitialized = false;

            if (!_Connection.IsConnected)
            {
                await Connect();

                if (!IsConnected)
                    return IsInitialized = false;
            }

            _CTS = new CancellationTokenSource();

            // Initialize collections first to reduce SX and duplicate calls
            foreach (var model in _Models.Values.Where(x => x.IsInitialized == false && x.GetType().IsSubclassOf(typeof(IntegraCollection))))
            {
                if (!_CTS.IsCancellationRequested)
                {
                    ReportProgress($"Initializing {model.Name}", "Please wait...", 0, "");
                    await model.InitializeAsync();
                }
            }

            // Initialize remaining models
            foreach (var model in _Models.Values.Where(x => x.IsInitialized == false))
            {
                if (!_CTS.IsCancellationRequested)
                    await model.InitializeAsync();
            }

            if (_CTS.IsCancellationRequested)
                return IsInitialized = false;

            return IsInitialized = true;
        }

        private void OnConnectionChanged(object? sender, IntegraConnectionStatusEventArgs e)
        {
            if (e.Previous == ConnectionStatus.Connected)
            {
                _CTS.Cancel();
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
                if (_DeviceID != value)
                {
                    _DeviceID = (byte)value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsConnected
        {
            get => _IsConnected;
            private set
            {
                if (_IsConnected != value)
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
                if (_IsInitialized != value)
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
                if (_Status != value)
                {
                    _Status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IProgress<IntegraStatus> Progress { get; set; }

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
        #endregion

        #region Properties: INTEGRA-7

        /// <summary>
        /// Gets the system model.
        /// </summary>
        public SystemCommon? System { get; private set; }

        /// <summary>
        /// Gets the setup model.
        /// </summary>
        public Setup? Setup { get; private set; }

        /// <summary>
        /// Gets the studio sets collection.
        /// </summary>
        /// <remarks><i>Use the <see cref="Setup.StudioSetPC"/> property to get or set the active studio set.</i></remarks>
        public StudioSets? StudioSets { get; private set; }

        /// <summary>
        /// Gets the studio set model.
        /// </summary>
        public StudioSet? StudioSet { get; private set; }

        /// <summary>
        /// Gets the virtual slots model.
        /// </summary>
        public VirtualSlots? VirtualSlots { get; private set; }

        /// <summary>
        /// Gets the collection of selected tones for all parts.
        /// </summary>
        public IntegraTones? SelectedTones { get; private set; }

        /// <summary>
        /// Gets the collection of temporary tones for all parts.
        /// </summary>
        public TemporaryTones? TemporaryTones { get; private set; }

        /// <summary>
        /// Gets or sets the active part by index.
        /// </summary>
        /// <remarks><i>
        /// Raises the <see cref="PartChanged"/> event.<br/>
        /// </i></remarks>
        public int PartIndex
        {
            get => (int)_SelectedPart;
            set => Part = (Parts)value;
        }

        /// <summary>
        /// Gets or sets the active part.
        /// </summary>
        /// <remarks><i>
        /// Raises the <see cref="PartChanged"/> event.<br/>
        /// </i></remarks>
        public Parts Part
        {
            get => _SelectedPart;
            set
            {
                if (_SelectedPart != value)
                {
                    var previous = _SelectedPart;
                    var preview  = IsPreviewing;

                    if (preview)
                        Preview = false;

                    _SelectedPart = value;

                    if (preview)
                        Preview = true;


                    PartChanged?.Invoke(this, new IntegraPartChangedEventArgs(value, previous));

                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(PartIndex));
                    NotifyPropertyChanged(nameof(SelectedTone));
                    NotifyPropertyChanged(nameof(TemporaryTone));
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="IntegraTone"/> containing tone information of the active part.
        /// </summary>
        public IntegraTone? ToneInfo
        {
            get => SelectedTones?[PartIndex];
        }

        /// <summary>
        /// Gets or sets the tone of the active part.
        /// </summary>
        /// <remarks><i>
        /// The tone can be set using a <see cref="Templates.ToneTemplate"/>.<br/>
        /// </i></remarks>
        public IBankSelect? SelectedTone
        {
            get => SelectedTones?[PartIndex];
            set
            {
                if (value != null)
                {
                    if (SelectedTones == null)
                        return;

                    if (SelectedTones[PartIndex].Equals(value))
                        return;

                    SelectedTones[PartIndex].BankSelect = value;

                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(ToneInfo));
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="Models.TemporaryTone"/> of the active part.
        /// </summary>
        public TemporaryTone? TemporaryTone
        {
            get => TemporaryTones?[PartIndex];
        }

        #endregion

        #region Commands

        /// <summary>
        /// Provides a bindable UI command to toggle the tone preview on or off on the active part.
        /// </summary>
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
            return StudioSet?.Part != null && StudioSet.Part.IsInitialized;
        }

        #endregion

        #region Commands: Implementation

        
        #endregion

        #endregion

        #region Methods

        private void CreateModels()
        {
            System         = CreateModel<SystemCommon>();
            Setup          = CreateModel<Setup>();
            VirtualSlots   = CreateModel<VirtualSlots>();
            StudioSets     = CreateModel<StudioSets>();
            SelectedTones  = CreateModel<IntegraTones>();
            StudioSet      = CreateModel<StudioSet>();
            TemporaryTones = CreateModel<TemporaryTones>();
        }

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
                TransmitSystemExclusive(new IntegraSystemExclusive(0x0F002000, 0x00000000, new byte[] { (byte)(PartIndex + 1) }));

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
            Task<bool> task = new(() =>
            {
                systemExclusive.DeviceID = _DeviceID;

                _Connection.SendSystemExclusiveMessage(systemExclusive);
                return true;

            }, _CTS.Token);

            _TaskManager.Enqueue(task, _CTS.Token);
        }


        internal void ReportProgress(IntegraModel model, double current, int total, Parts? part = null)
        {
            Status.Operation = $"Initializing {model.Name} {part}";
            Status.Progress = (int)(current / total * 100);

            if (model.GetType().IsSubclassOf(typeof(IntegraCollection)))
            {
                Status.Text = $"{(int)current} of {total}";
            }
            else
            {
                Status.Text = $"{(int)current} byte";
            }
            
            if (_Progress != null)
                _Progress.Report(Status);

        }

        private void ReportProgress(string operation, string message, int progress = 0, string text = "Ready")
        {
            Status.Operation = operation;
            Status.Progress  = progress;
            Status.Message   = message;
            Status.Text      = text;

            if (_Progress != null)
                _Progress.Report(Status);
        }

        #endregion

        #region Methods: Model Instantiation

        /// <summary>
        /// Creates and caches a new INTEGRA-7 model or returns the model from cache if it exists.
        /// </summary>
        /// <typeparam name="TModel">The model type specifier.</typeparam>
        /// <param name="part">The associated part, only required for <see cref="IntegraPartial"/> derived models.</param>
        /// <returns>A new or cached INTEGRA-7 model.</returns>
        /// <remarks><i>
        /// - Maintains refrential integrity of models and prevents duplicates.<br/>
        /// - Newly created models are uninitialized, cached models are possibly already initialized with data.<br/>
        /// </i></remarks>
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

            if (_Models.TryGetValue(instance.GetUID(), out IntegraModel? model))
            {
                Debug.Print($"[{nameof(Integra)}.{nameof(CreateModel)}<{typeof(TModel).Name}>({part})] Existing cache entry");

                // IMPORTANT: ? Disconnect to remove the device event listener to free all references to the newly created instance?
                instance.Disconnect();
                instance = null;

                return (TModel)model;
            }

            if(!_Models.TryAdd(instance.GetUID(), instance))
            {
                Debug.Print($"[{nameof(Integra)}.{nameof(CreateModel)}<{typeof(TModel).Name}>({part})] Unable to create cache entry: 0x{instance.GetUID():X4}");
            }

            Debug.Print($"[{nameof(Integra)}.{nameof(CreateModel)}<{typeof(TModel).Name}>({part})] New cache entry: 0x{instance.GetUID():X4}");

            return (TModel)instance;
        }

        /// <summary>
        /// Gets an initialized INTEGRA-7 model.
        /// </summary>
        /// <typeparam name="TModel">The model type specifier.</typeparam>
        /// <param name="part">The associated part, only required for <see cref="IntegraPartial{TModel}"/> derived models.</param>
        /// <returns>An awaitable <see cref="Task"/> that returns an initialized INTEGRA-7 model.</returns>
        /// <exception cref="IntegraException"/>
        public async Task<TModel> GetModel<TModel>(Parts? part = null) where TModel : IntegraModel
        {
            Debug.Print($"[{nameof(Integra)}{nameof(GetModel)}<{typeof(TModel).Name}>({part})]");

            if(typeof(TModel).IsSubclassOf(typeof(IntegraPartial<TModel>)) && part == null)
                    throw new IntegraException($"[{nameof(Integra)}.{nameof(GetModel)}<{typeof(TModel).Name}>]\n" +
                                               $"The requested model requires a part argument.");

            TModel model = CreateModel<TModel>(part);

            try
            {
                if (!model.IsInitialized)
                    await model.InitializeAsync();
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
            Debug.Print($"[{nameof(Integra)}.{nameof(GetToneBank)}({tonebank})");

            Type? type = tonebank.ToneBankType();

            Debug.Assert(type != null);

            IntegraToneBank bank = CreateToneBank(type);

            if(!bank.IsInitialized)
                await bank.InitializeAsync();

            return bank;
        }

        public async Task<IntegraToneBank> GetToneBank(Type type)
        {
            Debug.Print($"[{nameof(Integra)}.{nameof(GetToneBank)}({type.Name})");

            if (!type.IsSubclassOf(typeof(IntegraToneBank)))
                throw new IntegraException($"[{nameof(Integra)}.{nameof(GetToneBank)}({type})]\n" +
                                           $"The type requires to be derived from {nameof(IntegraToneBank)}.\n" +
                                           $"Use the static class {nameof(ToneBanks)} to get a valid type.");

            IntegraToneBank bank = CreateToneBank(type);

            if (!bank.IsInitialized)
                await bank.InitializeAsync();

            return bank;
        }

        public async Task<IntegraToneBank> GetToneBank<TToneBank>() where TToneBank : IntegraToneBank
        {
            Debug.Print($"[{nameof(Integra)}.{nameof(GetToneBank)}<{typeof(TToneBank).Name}>()");

            if (!typeof(TToneBank).IsSubclassOf(typeof(IntegraToneBank)))
                throw new IntegraException($"[{nameof(Integra)}.{nameof(GetToneBank)}<{typeof(TToneBank).Name}>]\n" +
                                           $"The type requires to be derived from {nameof(IntegraToneBank)}.");

            IntegraToneBank bank = CreateToneBank(typeof(TToneBank));

            if (!bank.IsInitialized)
                await bank.InitializeAsync();

            return bank;
        }

        /// <summary>
        /// Creates and caches a new INTEGRA-7 tone bank or returns the tone bank from cache if it exists.<br/>
        /// Maintains refrential integrity of tone banks and prevents duplicates.
        /// </summary>
        /// <param name="type">The tone bank type specifier.</param>
        /// <returns>A new or cached INTEGRA-7 tone bank.</returns>
        /// <exception cref="IntegraException"></exception>
        /// <remarks><i>
        /// - Maintains refrential integrity of tone banks and prevents duplicates.<br/>
        /// - Newly created tone banks are uninitialized, cached tone banks are possibly already initialized with data.<br/>
        /// </i></remarks>
        private IntegraToneBank CreateToneBank(Type type)
        {
            IntegraToneBank? instance;

            Debug.Assert(type != null && type.IsSubclassOf(typeof(IntegraToneBank)));

            instance = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { this }, null) as IntegraToneBank;

            Debug.Assert(instance != null);

            if (_Models.TryGetValue(instance.GetUID(), out IntegraModel? model))
            {
                Debug.Print($"[{nameof(Integra)}.{nameof(CreateToneBank)}({type})] Existing cache entry");

                return (IntegraToneBank)model;
            }

            if (!_Models.TryAdd(instance.GetUID(), instance))
            {
                Debug.Print($"[{nameof(Integra)}.{nameof(CreateToneBank)}({type})] Unable to create cache entry: 0x{instance.GetUID():X4}");
            }

            Debug.Print($"[{nameof(Integra)}.{nameof(CreateToneBank)}({type})] New cache entry: 0x{instance.GetUID():X4}");

            return instance;
        }

        #endregion

        #region Methods: Model Requests

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

            Task<bool> task = new (() =>
            {
                    //InitProgress(model);
                    Debug.Print($"[{nameof(IntegraTaskManager)}] Task: {model.GetType().Name}");

                    foreach (var request in model.Requests)
                    {
                        IntegraSystemExclusive systemExclusive = new(model.Address, request);
                        systemExclusive.DeviceID = _DeviceID;

                    //if (_Connection == null)
                    //    return false;

                    _Connection.SendSystemExclusiveMessage(systemExclusive);
                    }

                    while (!model.IsInitialized)
                    {
                    if (_CTS.IsCancellationRequested)
                        return false;
                        //_ModelCTS.Token.ThrowIfCancellationRequested();
                        //await Task.Delay(100, _ModelCTS.Token);

                        //Task.Delay(100);
                        //Thread.Sleep(100);
                    }

                    //CompleteProgress(model);
                    return true;

            }, _CTS.Token);

            //task.ContinueWith((t) =>
            //{
            //    Debug.Print($"[{nameof(Integra)}] {nameof(InitializeModel)}<{model.GetType().Name}>() CANCELLED");
            //    return false;
            //}, TaskContinuationOptions.OnlyOnCanceled);

            // TODO: Error handling / Time out to prevent application lock
            //if(!_ModelCTS.IsCancellationRequested)
            _TaskManager.Enqueue(task, _CTS.Token);


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
            },_CTS.Token);

            _TaskManager.Enqueue(task, _CTS.Token);

            return task;
        }


        #endregion

        #region Methods: File IO

        /// <summary>
        /// Writes the studio set data to an in memory <see cref="StudioSetFile"/>.
        /// </summary>
        /// <returns>An in memory binary formatted <see cref="StudioSetFile"/>.</returns>
        /// <exception cref="IntegraException"/>
        public MemoryStream SaveStudioSet()
        {
            if (StudioSet == null || StudioSet.IsInitialized == false)
                throw new IntegraException($"[{nameof(Integra)}.{nameof(SaveStudioSet)}()]\n" +
                                           $"Studio set is not initialized.");

            if (VirtualSlots == null || VirtualSlots.IsInitialized == false)
                throw new IntegraException($"[{nameof(Integra)}.{nameof(SaveStudioSet)}()]\n" +
                                           $"Studio set is not initialized.");

            StudioSetFile file = StudioSet.Save();

            file.Expansions = VirtualSlots.Serialize();

            return FileManager.WriteStudioSet(file);
        }

        public MemoryStream SaveTemporaryTone()
        {
            if(TemporaryTone == null || TemporaryTone.IsInitialized == false)
                throw new IntegraException($"[{nameof(Integra)}.{nameof(SaveTemporaryTone)}()]\n" +
                                           $"Temporary tone is not initialized.");

            return FileManager.WriteTemporaryToneFile(TemporaryTone.Save());
        }

        /// <summary>
        /// Loads the studio set with the provided data.
        /// </summary>
        /// <param name="file">The file containing the data to initialize the studio set.</param>
        /// <param name="index">The studio set index to load the studio set into, defaults to the currently selected studio set.</param>
        /// <exception cref="IntegraException"/>
        /// <remarks><i>
        /// - If no index is provided, the currently selected studio set is overwritten.<br/>
        /// - If an internal studio set [0..15] is selected an exception is thrown.<br/>
        /// - Make sure to save the current user data before overwriting the the studio set.<br/>
        /// </i></remarks>
        public async Task<bool> LoadStudioSet(StudioSetFile file, int index = -1)
        {
            if(Setup == null || Setup.IsInitialized == false)
                throw new IntegraException($"[{nameof(Integra)}.{nameof(LoadStudioSet)}()]\n" +
                                           $"Setup is not initialized.");

            if (StudioSet == null || StudioSet.IsInitialized == false)
                throw new IntegraException($"[{nameof(Integra)}.{nameof(LoadStudioSet)}()]\n" +
                                           $"Studio set is not initialized.");

            if (index != -1)
            {
                if(index < 16)
                    throw new IntegraException($"[{nameof(Integra)}.{nameof(LoadStudioSet)}()]\n" +
                                               $"Cannot overwrite internal studio set #{Setup.StudioSetPC}");

                if(index > 63)
                    throw new IntegraException($"[{nameof(Integra)}.{nameof(LoadStudioSet)}()]\n" +
                                               $"Studio set slot out of range 16..63");

                if(Setup.StudioSetPC != index)
                {
                    // SWITCH CURRENT STUDIO SET
                    Setup.StudioSetPC = (byte)index;
                }
            }
            else
            {
                if (Setup.StudioSetPC < 16)
                    throw new IntegraException($"[{nameof(Integra)}.{nameof(LoadStudioSet)}()]\n" +
                                               $"Cannot overwrite internal studio set #{Setup.StudioSetPC}");
            }

            if(VirtualSlots == null || VirtualSlots.IsInitialized == false)
                throw new IntegraException($"[{nameof(Integra)}.{nameof(LoadStudioSet)}()]\n" +
                                           $"Virtual slots is not initialized.");

            int requiredSlots = file.Expansions.Where(x => x != 0).Count();

            if(requiredSlots > VirtualSlots.FreeSlots())
            {
                VirtualSlots.SlotA = (IntegraExpansions)file.Expansions[0];
                VirtualSlots.SlotB = (IntegraExpansions)file.Expansions[1];
                VirtualSlots.SlotC = (IntegraExpansions)file.Expansions[2];
                VirtualSlots.SlotD = (IntegraExpansions)file.Expansions[3];
            }
            else if(requiredSlots != 0)
            {
                for (int i = 0; i < IntegraConstants.EXP_COUNT; i++)
                {
                    if(file.Expansions[i] != 0)
                    {
                        if(!VirtualSlots.Contains((IntegraExpansions)file.Expansions[i]))
                        {
                            VirtualSlots[VirtualSlots.NextFreeSlotIndex()] = (IntegraExpansions)file.Expansions[i];
                        }
                    }
                }
            }

            await VirtualSlots.Load();

            StudioSet.Load(file);

            NotifyPropertyChanged(string.Empty);

            return true;
        }

        public void LoadTemporaryTone(TemporaryToneFile file, int part = -1)
        {
            if(part != -1)
            {
                if(part < 0 || part > 15)
                    throw new IntegraException($"[{nameof(Integra)}.{nameof(LoadTemporaryTone)}()]\n" +
                                               $"Temporary tone part out of range 0..15");

                if(TemporaryTones == null || TemporaryTones.IsInitialized == false)
                    throw new IntegraException($"[{nameof(Integra)}.{nameof(LoadTemporaryTone)}()]\n" +
                                               $"Temporary tones collection is not initialized.");

                TemporaryTones[part].Load(file);
            }
            else
            {
                if(TemporaryTone == null || TemporaryTone.IsInitialized == false)
                    throw new IntegraException($"[{nameof(Integra)}.{nameof(LoadTemporaryTone)}()]\n" +
                                               $"Temporary tone is not initialized.");

                TemporaryTone.Load(file);
            }
        }

        public void StoreTemporaryTone(int index)
        {
            Debug.Print($"{TemporaryTone.Type}");

            byte[] data = new byte[4];

            switch(TemporaryTone.Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:
                    if (index < 0 || index > 255)
                        throw new IntegraException($"");

                    data[0] = 0x59;

                    if(index > 127)
                    {
                        data[1] = 0x01;
                        index -= 128;
                        data[2] = (byte)index;
                    }
                    else
                    {
                        data[1] = 0x00;
                        data[2] = (byte)index;
                    }

                    break;

                case IntegraToneTypes.SuperNATURALSynthTone:
                    if (index < 0 || index > 511)
                        throw new IntegraException($"");
                    data[0] = 0x5F;

                    if (index >= 128)
                    {
                        data[1] = (byte)(index / 128);
                        index -= 128 * data[1];
                        data[2] = (byte)index;
                    }
                    else
                    {
                        data[1] = 0x00;
                        data[2] = (byte)index;
                    }

                    break;

                case IntegraToneTypes.SuperNATURALDrumkit:
                    if (index < 0 || index > 63)
                        throw new IntegraException($"");
                    data[0] = 0x58;
                    data[1] = 0x00;
                    data[2] = (byte)index;

                    break;

                case IntegraToneTypes.PCMSynthTone:
                    if (index < 0 || index > 255)
                        throw new IntegraException($"");
                    data[0] = 0x57;
                    if (index > 127)
                    {
                        data[1] = 0x01;
                        index -= 128;
                        data[2] = (byte)index;
                    }
                    else
                    {
                        data[1] = 0x00;
                        data[2] = (byte)index;
                    }

                    break;

                case IntegraToneTypes.PCMDrumkit:
                    if (index < 0 || index > 31)
                        throw new IntegraException($"");
                    data[0] = 0x56;
                    data[1] = 0x00;
                    data[2] = (byte)index;

                    break;
            }

            data[3] = (byte)PartIndex;

            

            TransmitSystemExclusive(new IntegraSystemExclusive(new IntegraAddress(0x0F001000), new IntegraRequest(data)));


        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the <see cref="IntegraConnection.SystemExclusiveReceived"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="IntegraConnection"/> that raised the event.</param>
        /// <param name="e">The event's associated data.</param>
        private void OnSystemExclusiveReceived(object sender, IntegraSystemExclusiveEventArgs e)
        {
            // TODO: ?? Move model system exclusive received to this for performance ??
            
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

            if(e.SystemExclusive.Address == IntegraConstants.STORING_DATA)
            {

            }
            else if(e.SystemExclusive.Address == IntegraConstants.STORING_COMPLETE)
            {

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