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

            //Integra.OperationStart += IntegraOperationStart;
            //Integra.OperationProgress += IntegraOperationProgress;
            //Integra.OperationComplete += IntegraOperationComplete;


            CommandBindings.Add(new CommandBinding(_ShowWindowCommand, OnShowWindow));
            CommandBindings.Add(new CommandBinding(_ShowIntegraWindowCommand, OnShowIntegraWindow, CanExecuteShowIntegraWindow));
            


            Host.SelectionChanged += HostSelectionChanged;

            Loaded += MainWindowLoaded;
            //Config<IntegraConfiguration>.Print();
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            Integra.OperationStart += IntegraOperationStart;
            Integra.OperationProgress += IntegraOperationProgress;
            Integra.OperationComplete += IntegraOperationComplete;

            //Integra.Initialize();
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

            if (!type.IsSubclassOf(typeof(IntegraWindow)))
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

            if (!type.IsSubclassOf(typeof(CommonWindow)) && !type.IsSubclassOf(typeof(IntegraBaseToneBank)))
                return;

            AddWindow(type);
        }

        /// <summary>
        /// Determins if the <see cref="ShowIntegraWindow"/> command can be executed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> containing event data.</param>
        /// <remarks><i>Depends on the connection state of the INTEGRA-7.</i></remarks>
        private void CanExecuteShowIntegraWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Integra.IsConnected;
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

            //if (!IsLoaded)
            //    return;


            //if (_Dialog != null)
            //    return;
            _Dialog = DialogManager.ProgressDialog(e.Action, e.Message, e.Text);

               
        }

        /// <summary>
        /// Handles the <see cref="Device.OperationProgress"/> event to report progress.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="IntegraOperationEventArgs"/> containing event data.</param>
        private void IntegraOperationProgress(object sender, IntegraOperationEventArgs e)
        {
            if (!IsLoaded || _Dialog == null)
                return;
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
