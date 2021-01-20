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
            if (Integra.StudioSet.Part.TemporaryTone == null)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = Integra.StudioSet.Part.TemporaryTone.Type == (IntegraToneTypes)e.Parameter;
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
            MFXWindow mdiChild = (MFXWindow)Activator.CreateInstance(typeof(MFXWindow), new object[] { type });
            //mdiChild.Title = type.ToString();
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
