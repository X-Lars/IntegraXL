using ControlsXL;
using Integra;
using Integra.Core;
using IntegraXL.Widgets;
using IntegraXL.Windows;
using Integra.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Integra.Database;

namespace IntegraXL
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ProgressDialog _Dialog;
        private MessageDialog _MessageDialog;

        private static readonly object _Lock = new object();
        #region Constructor
        
        /// <summary>
        /// Creates and initializes the <see cref="MainWindow"/> which is the application's starting point.
        /// </summary>
        public MainWindow()
        {
            // [REQUIRED]
            Device.StatusChanged += DeviceStatusChanged;

            StyleManager.Style = ControlStyle.Default;

            Widgets.Add(new CommonWidget());

            InitializeComponent();

            Device.OperationStart    += IntegraOperationStart;
            Device.OperationProgress += IntegraOperationProgress;
            Device.OperationComplete += IntegraOperationComplete;


            CommandBindings.Add(new CommandBinding(_ShowWindowCommand, OnShowWindow));
            CommandBindings.Add(new CommandBinding(_ShowIntegraWindowCommand, OnShowIntegraWindow, CanExecuteOnConnection));
            CommandBindings.Add(new CommandBinding(_ShowIntegraMFXWindowCommand, OnShowIntegraMFXWindow));
            CommandBindings.Add(new CommandBinding(_ShowIntegraToneWindowCommand, OnShowIntegraToneWindow, CanExecuteOnToneType));
            CommandBindings.Add(new CommandBinding(_LoadCommand, OnLoad, CanExecuteOnLoad));
            CommandBindings.Add(new CommandBinding(_SaveCommand, OnSave, CanExecuteOnSave));
            CommandBindings.Add(new CommandBinding(_UpdateCommand, OnUpdate, CanExecuteUpdate));
            CommandBindings.Add(new CommandBinding(_TruncateCommand, OnTruncate));
            CommandBindings.Add(new CommandBinding(_AddFavoriteCommand, OnAddFavorite));
            Host.SelectionChanged += HostSelectionChanged;

            Loaded += MainWindowLoaded;
            //Config<IntegraConfiguration>.Print();


        }

        
       

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            //_FavoriteTones = new ObservableCollection<IntegraTone>(DataAccess.Select(Integra.StudioSet.Tone, new IntegraTone()));
            //Integra.OperationStart += IntegraOperationStart;
            //Integra.OperationProgress += IntegraOperationProgress;
            //Integra.OperationComplete += IntegraOperationComplete;

            ////Integra.Initialize();
            if (!Integra.IsConnected)
                DeviceStatusChanged(this, new IntegraStatusEventArgs(Integra.Flags));
        }

        #endregion

        #region Commands

        #region Commands: Registration


        private static RoutedUICommand _LoadCommand = new RoutedUICommand(nameof(Load), nameof(Load), typeof(MainWindow));
        private static RoutedUICommand _SaveCommand = new RoutedUICommand(nameof(Save), nameof(Save), typeof(MainWindow));
        private static RoutedUICommand _TruncateCommand = new RoutedUICommand(nameof(Truncate), nameof(Truncate), typeof(MainWindow));
        private static RoutedUICommand _UpdateCommand = new RoutedUICommand(nameof(Update), nameof(Update), typeof(MainWindow));
        private static RoutedUICommand _AddFavoriteCommand = new RoutedUICommand(nameof(AddFavorite), nameof(AddFavorite), typeof(MainWindow));

        /// <summary>
        /// Registers the command to show a <see cref="MDIChild"/> window.
        /// </summary>
        private static RoutedUICommand _ShowWindowCommand = new RoutedUICommand(nameof(ShowWindow), nameof(ShowWindow), typeof(MainWindow));

        /// <summary>
        /// Registers the commant to show a <see cref="MDIChild"/> window with INTEGRA-7 tone type dependencies.
        /// </summary>
        private static RoutedUICommand _ShowIntegraToneWindowCommand = new RoutedUICommand(nameof(ShowIntegraToneWindow), nameof(ShowIntegraToneWindow), typeof(MainWindow));

        private static RoutedUICommand _ShowIntegraMFXWindowCommand = new RoutedUICommand(nameof(ShowIntegraMFXWindow), nameof(ShowIntegraMFXWindow), typeof(MainWindow));
        /// <summary>
        /// Registers the command to show a <see cref="MDIChild"/> window with INTEGRA-7 dependencies.
        /// </summary>
        private static RoutedUICommand _ShowIntegraWindowCommand = new RoutedUICommand(nameof(ShowIntegraWindow), nameof(ShowIntegraWindow), typeof(MainWindow));

        #endregion

        #region Commands: Properties

        public static ICommand Update
        {
            get { return _UpdateCommand; }
        }
        public static ICommand Load
        {
            get { return _LoadCommand; }
        }

        public static ICommand Save
        {
            get { return _SaveCommand; }
        }

        public static ICommand Truncate
        {
            get { return _TruncateCommand; }
        }

        public static ICommand AddFavorite
        {
            get { return _AddFavoriteCommand; }
        }

        /// <summary>
        /// Gets the command to show a <see cref="MDIChild"/> window.
        /// </summary>
        public static ICommand ShowIntegraWindow
        {
            get { return _ShowIntegraWindowCommand; }
        }

        public static ICommand ShowIntegraMFXWindow
        {
            get { return _ShowIntegraMFXWindowCommand; }
        }
        /// <summary>
        /// Gets the command to show a <see cref="MDIChild"/> window with INTEGRA-7 tone type dependencies.
        /// </summary>
        public static ICommand ShowIntegraToneWindow
        {
            get { return _ShowIntegraToneWindowCommand; }
        }

        /// <summary>
        /// Gets the command to show a <see cref="MDIChild"/> window with INTEGRA-7 dependencies.
        /// </summary>
        public static ICommand ShowWindow
        {
            get { return _ShowWindowCommand; }
        }

        #endregion

        #region Commands: Handlers
        private void OnUpdate(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow caller = sender as MainWindow;

            if(caller != null)
            {
                Device.Session.Update();
            }
        }

        private void OnLoad(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow caller = sender as MainWindow;

            if(caller != null)
            {
                // TODO: Parameter to select type of data structure to load
                // TODO: Parameter ID to load
                // TODO: Remove temporary fixed parameter
                Device.Session.Select(1);
            }
        }

        private void OnSave(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow caller = sender as MainWindow;

            if(caller != null)
            {
                Device.Session.Insert();
                //caller.Integra.ses.Save();
            }
        }

        private async void OnTruncate(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow caller = sender as MainWindow;

            DialogResults result = await DialogManager.QuestionDialog("Delete", "This will erase all data, are you sure?").Result();


            if (caller != null)
            {
                if(result == DialogResults.DialogYes)
                    Device.Session.Truncate();
            }
        }

        private void OnAddFavorite(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow caller = sender as MainWindow;

            if (caller != null)
            {
                Integra.StudioSet.SaveFavorite();
            }
        }

        /// <summary>
        /// Handles the <see cref="ShowIntegraWindow"/> command.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> containing event data.</param>
        private void OnShowIntegraWindow(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
                return;

            Type type = (Type)e.Parameter;

            if (!type.IsSubclassOf(typeof(IntegraWindow)) && !type.IsSubclassOf(typeof(IntegraBaseToneBank)))
                return;

            AddWindow(type);
        }

        private void OnShowIntegraMFXWindow(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
                return;

            Type type = e.Parameter.GetType();

            if (type != typeof(IntegraMFXTypes))
                return;

            AddMFXWindow((IntegraMFXTypes)e.Parameter);
        }


        /// <summary>
        /// Handles the <see cref="ShowIntegraToneWindow"/> command.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> containing event data.</param>
        private void OnShowIntegraToneWindow(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
                return;

            Type type = (Type)e.Parameter;

            if (type != typeof(IntegraToneTypes))
                return;

            AddWindow(type);
        }

        /// <summary>
        /// Handles the <see cref="ShowWindow"/> command.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> containing event data.</param>
        private void OnShowWindow(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
                return;

            Type type = (Type)e.Parameter;

            if (!type.IsSubclassOf(typeof(CommonWindow)))
                return;

            AddWindow(type);
        }

        /// <summary>
        /// Determins if the <see cref="ShowIntegraWindow"/> command can be executed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> containing event data.</param>
        /// <remarks><i>Depends on the connection state of the INTEGRA-7.</i></remarks>
        private void CanExecuteOnConnection(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Integra.IsConnected;
        }

        private void CanExecuteUpdate(object sender, CanExecuteRoutedEventArgs e)
        {
            // TODO: Can update?
            e.CanExecute = true;
        }


        private void CanExecuteOnLoad(object sender, CanExecuteRoutedEventArgs e)
        {
            // TODO: Ensure a user Studio Set is selected 32-64
            // TODO: Enable after save current changes request
            e.CanExecute = true;
        }


        private void CanExecuteOnSave(object sender, CanExecuteRoutedEventArgs e)
        {
            // TODO: Enable save on changes made
            e.CanExecute = true;
        }


        private void CanExecuteOnToneType(object sender, CanExecuteRoutedEventArgs e)
        {
            Type type = e.Parameter.GetType();

            if (type != typeof(IntegraToneTypes))
                e.CanExecute = false;

            //if (Integra.StudioSet.Parts[(int)Integra.SelectedPart].TemporaryTone == null)
            //{
            //    e.CanExecute = false;
            //    return;
            //}
            //e.CanExecute = Integra.StudioSet.Parts[(int)Integra.SelectedPart].TemporaryTone.Type == (IntegraToneTypes)e.Parameter;
            if (Integra.StudioSet.Parts[(int)Integra.StudioSet.SelectedPart].TemporaryTone == null)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = Integra.StudioSet.Parts[(int)Integra.StudioSet.SelectedPart].TemporaryTone.Type == (IntegraToneTypes)e.Parameter;
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets a reference to the INTEGRA-7.
        /// </summary>
        public Device Integra 
        { 
            get { return Device.Instance; }
        }

        /// <summary>
        /// Gets the collections of loaded <see cref="Widget"/>s.
        /// </summary>
        public ObservableCollection<UIElement> Widgets { get; private set; } = new ObservableCollection<UIElement>();
       

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the selection changed event of the <see cref="MDIHost"/> to attach or remove the <see cref="MDIChild.Widget"/>s.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">A <see cref="MDIChildEventArgs"/> containing event data.</param>
        private void HostSelectionChanged(object sender, MDIChildEventArgs e)
        {
            if (e.Deactivated != null)
            {
                if (e.Deactivated.Widget != null)
                {
                    Widgets.Remove(e.Deactivated.Widget);
                }
            }
            
            if (e.Activated != null)
            {
                if (e.Activated.Widget != null)
                {
                    if(!Widgets.Contains(e.Activated.Widget))
                        Widgets.Add(e.Activated.Widget);
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="Device.OperationStart"/> event to initialize progress reporting.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="IntegraOperationEventArgs"/> containing event data.</param>
        private void IntegraOperationStart(object sender, IntegraOperationEventArgs e)
        {

            if (!IsLoaded)
                return;


            //if (_Dialog != null)
            //    return;
            

            if (sender.GetType() == typeof(VirtualSlots))
                _Dialog = DialogManager.ProgressDialog(e.Action, e.Message, e.Text, true);
            else
                _Dialog = DialogManager.ProgressDialog(e.Action, e.Message, e.Text);
        }

        /// <summary>
        /// Handles the <see cref="Device.OperationProgress"/> event to report progress.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="IntegraOperationEventArgs"/> containing event data.</param>
        private void IntegraOperationProgress(object sender, IntegraOperationEventArgs e)
        {
            if (!IsLoaded)
                return;

            if(_Dialog == null)
                _Dialog = DialogManager.ProgressDialog(e.Action, e.Message, e.Text);


            _Dialog.Title = e.Action;
            _Dialog.Message = e.Message;
            _Dialog.Progress = e.Progress;
            _Dialog.Status = e.Text;
        }

        /// <summary>
        /// Handles the <see cref="Device.OperationComplete"/> event to finalize progress reporting.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="IntegraOperationEventArgs"/> containing event data.</param>
        private void IntegraOperationComplete(object sender, IntegraOperationEventArgs e)
        {

            if (!IsLoaded || _Dialog == null)
                return;

            _Dialog.Title = e.Action;
            _Dialog.Message = e.Message;
            _Dialog.Progress = e.Progress;
            _Dialog.Status = e.Text;

            _Dialog.Close();

        }

        /// <summary>
        /// Handles the <see cref="Device.StatusChanged"/> event.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the class that raised the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing event data.</param>
        private void DeviceStatusChanged(object sender, IntegraStatusEventArgs e)
        {
            Console.WriteLine($"[{nameof(MainWindow)}.{nameof(DeviceStatusChanged)}] {e.DeviceFlags}");

            if (e.DeviceFlags.HasFlag(DeviceStatusFlags.DEVICE_NO_MIDI_OUTPUT_DEVICES) || 
               e.DeviceFlags.HasFlag(DeviceStatusFlags.DEVICE_NO_MIDI_INPUT_DEVICES))
            {
                _MessageDialog = new MessageDialog("MIDI DEVICE ERROR", "The system is missing MIDI devices, functionality of the program is limited.");

                if (IsLoaded)
                {
                    DialogManager.Show(_MessageDialog);
                    AddWindow(typeof(MidiDevicesWindow));
                }

                return;
            }

            if (e.DeviceFlags.HasFlag(DeviceStatusFlags.DEVICE_NO_MIDI_OUTPUT_SELECTED) ||
                e.DeviceFlags.HasFlag(DeviceStatusFlags.DEVICE_NO_MIDI_INPUT_SELECTED))
            {
                if(IsLoaded)
                    AddWindow(typeof(MidiDevicesWindow));

                return;
            }

            if(e.DeviceFlags.HasFlag(DeviceStatusFlags.DEVICE_CONNECTION_LOST))
            {
                // TODO: Close or disable Integra dependent windows
            }

            if(e.DeviceFlags.HasFlag(DeviceStatusFlags.DEVICE_NOT_CONNECTED))
            {
                if (IsLoaded)
                {
                    //AddWindow(typeof(MidiDevicesWindow));

                    //DialogManager.MessageDialog("MIDI DEVICE ERROR", "The currently selected MIDI configuration failed to connect to the INTEGRA-7.");
                }

                return;
            }


            if (e.DeviceFlags == DeviceStatusFlags.DEVICE_READY)
            {
                return;
                // TODO: Ready for use
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specified <see cref="MDIChild"/> window if it does not exist or selects the existing window.
        /// </summary>
        /// <param name="type">The window <see cref="Type"/> derived from <see cref="MDIChild"/> to add.</param>
        private void AddWindow(Type type)
        {
            if(type.IsSubclassOf(typeof(IntegraBaseToneBank)))
            {
                foreach (MDIChild child in Host.Items)
                {
                    if (child.GetType() == typeof(ToneBankWindow))
                    {
                        if (((ToneBankWindow)child).ToneBank.GetType() == type)
                        {
                            Host.SelectChild(child);

                            return;
                        }
                    }
                }

                ToneBankWindow mdiChild = (ToneBankWindow)Activator.CreateInstance(typeof(ToneBankWindow), new object[] { type });

                mdiChild.Title = type.Name;
                
                Host.Items.Add(mdiChild);

                return;
            }

            if (type.IsSubclassOf(typeof(MDIChild)))
            {

                foreach (MDIChild child in Host.Items)
                {
                    // Prevent opening a duplicate window
                    if (child.GetType() == type)
                    {
                        Host.SelectChild(child);

                        return;
                    }
                }

                Host.Items.Add((MDIChild)Activator.CreateInstance(type));
            }

        }

        private void AddMFXWindow(IntegraMFXTypes type)
        {
            foreach (MDIChild child in Host.Items)
            {
                if (child.GetType() == typeof(MFXWindow))
                {
                    // TODO: Remove child
                    ((MFXWindow)child).MFXType = type;
                    
                    return;
                    //if (((MFXWindow)child).ToneBank.GetType() == type)
                    //{
                    //    Host.SelectChild(child);

                    //    return;
                    //}
                }
            }

            MFXWindow mdiChild = (MFXWindow)Activator.CreateInstance(typeof(MFXWindow));
            mdiChild.Title = "MFX";
            Host.Items.Add(mdiChild);
        }

        #endregion

        #region INotifyPropertyChanged

        /// <summary>
        /// Event raised when a property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the specified property.
        /// </summary>
        /// <param name="propertyName">A <see cref="string"/> containing the name of the property that is changed.</param>
        /// <remarks><i>If no property name is specified, the actual name of the property in code is used.</i></remarks>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
