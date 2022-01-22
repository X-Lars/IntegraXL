using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using IntegraXL.Models;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

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
        private ConcurrentDictionary<uint, IntegraModel> _Models = new();

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
        /// <param name="connection"></param>
        /// <exception cref="IntegraException"></exception>
        private Integra(IntegraConnection connection)
        {
            if (connection == null)
                throw new IntegraException($"[{nameof(Integra)}.Constructor]\nConnection is null.");
            
            _Connection = connection;
            _Connection.SystemExclusiveReceived += OnSystemExclusiveReceived;
            DeviceID = connection.ID;
            _TaskManager.Initialize();
        }


        #endregion



        #region Properties

        /// <summary>
        /// Gets the device ID.
        /// </summary>
        /// <remarks><i>Equals the connection ID.</i></remarks>
        private byte DeviceID { get; }

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
                    NotifyPropertyChanged();
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
        /// <remarks><i>Use the <see cref="Setup.StudioSetPC"/> property to get or set the active studio set.</i></remarks>
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
        /// <i>Determines the partial models returned from the studio set.</i><br/>
        /// <i>Determines the partial model returned for the selected tone.</i><br/>
        /// <i>Can be used to notify and update UI applications.</i>
        /// </remarks>
        public int SelectedPart
        {
            get => _SelectedPart;
            set
            {
                if (_SelectedPart != value)
                {
                    value = Math.Min(value, 15);
                    value = Math.Max(value, 0);
                    var previous = _SelectedPart;
                    
                    _SelectedPart = value;
                    NotifyPropertyChanged();

                    NotifyPropertyChanged(nameof(SelectedTone));

                    PartChanged?.Invoke(this, new IntegraPartChangedEventArgs((Parts)value, (Parts)previous));
                }
            }
        }

        public IntegraTone ToneInfo
        {
            get => SelectedTones[(int)SelectedPart];
        }

        public IBankSelect SelectedTone
        {
            get => SelectedTones[(int)SelectedPart];
            set
            {
                if (SelectedTones[(int)SelectedPart].Equals(value))
                    return;

                SelectedTones[(int)SelectedPart].BankSelect = value;

                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ToneInfo));
            }
        }
       

        #endregion

        #endregion

        #region Methods

        public static async Task<Integra> CreateInstance(IntegraConnection connection)
        {
            Integra instance = new (connection);

            // TODO: Unnescesary ?
            await instance.InitializeInstance();

            return instance;
        }

        /// <summary>
        /// Initializes the nescesary models to operate.
        /// </summary>
        /// <returns>An awaitable <see cref="Task"/> that returns nothing.</returns>
        private async Task InitializeInstance()
        {
            Setup         = await GetModel<Setup>();
            VirtualSlots  = await GetModel<VirtualSlots>();
            StudioSets    = await GetModel<StudioSets>();
            SelectedTones = await GetModel<IntegraTones>();

            StudioSet     = await GetModel<StudioSet>();
            //SelectedTone  = SelectedTones[(int)SelectedPart];

            IsInitialized = true;
        }

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

            if(typeof(TModel).IsSubclassOf(typeof(IntegraPartial)))
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
                
                // IMPORTANT: Disconnect to remove the device event listener to free all references to the newly created instance
                // TODO: Remove model connecting logic to model class
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

            if(!model.IsInitialized)
            {
                await model.Initialize();
            }

            return model;
        }

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

        internal IntegraToneBank CreateToneBank(Type type)
        {
            IntegraToneBank? instance;

            Debug.Assert(type != null && type.IsSubclassOf(typeof(IntegraToneBank)));

            instance = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { this }, null) as IntegraToneBank;

            Debug.Assert(instance != null);

            if (_Models.TryGetValue(instance.GetModelHash(), out IntegraModel? model))
            {
                Debug.Print($"[{nameof(Integra)}] {nameof(CreateToneBank)}({type}) From Cache");

                // IMPORTANT: Disconnect to remove the device event listener to free all references to the newly created instance
                // TODO: Remove model connecting logic to model class
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
        // TODO: GetModel override to easy select tone bank
        // TODO: GetModel override to easy select partials

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
                    IntegraSystemExclusive systemExclusive = new (DeviceID, model.Address, request);
                    _Connection.SendSystemExclusiveMessage(systemExclusive);
                }

                while (!model.IsInitialized)
                {
                    Thread.Sleep(100);
                }

                //CompleteProgress(model);
                return true;
            });

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
                IntegraSystemExclusive systemExclusive = new(DeviceID, 0x0F003000, instance.Requests[0]);

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

        internal void TransmitSystemExclusive(IntegraSystemExclusive systemExclusive)
        {
            // TODO: Check if taskmanager is nescessary for sending
            Task<bool> task = new (() =>
            {
                systemExclusive.DeviceID = this.DeviceID;
                
                _Connection.SendSystemExclusiveMessage(systemExclusive);
                return true;
            });

            _TaskManager.Enqueue(task);
        }

        #endregion

        private void OnSystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            switch (e.SystemExclusive.Address[0])
            {
                case 0x01:
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

        #region Interfaces: INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }


    // Studio Set (Depends on setup)
    // Temporary Tones [1..16]
    // Virtual Slots


    // METHODS
    // Get Model
    // 


    // Cache Model
    // Send Model
    // Receive Model

    // Load
    // Save



}