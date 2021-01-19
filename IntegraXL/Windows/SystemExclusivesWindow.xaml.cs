using Integra.Common;
using Integra.Core;
using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace IntegraXL.Windows
{
    /// <summary>
    /// Interaction logic for SystemExclusivesWindow.xaml
    /// </summary>
    public partial class SystemExclusivesWindow : IntegraWindow
    {
        #region Fields

        /// <summary>
        /// Stores the received system exclusives.
        /// </summary>
        private string _SystemExclusives;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes a new <see cref="SystemExclusivesWindow"/> instance.
        /// </summary>
        public SystemExclusivesWindow() : base()
        {
            InitializeComponent();

            DataContext = this;

            DeviceContext.IntegraSystemExclusiveReceived += SystemExclusiveReceived;

            CommandBindings.Add(new CommandBinding(ClearCommand, OnClearCommand));
            CommandBindings.Add(new CommandBinding(CopyCommand, OnCopyCommand));
            CommandBindings.Add(new CommandBinding(SendCommand, OnSendCommand, CanExecuteSendCommand));
        }

        #endregion

        #region Commands

        #region Commands: Registration

        /// <summary>
        /// Registers the command to clear the <see cref="SystemExclusivesWindow"/>.
        /// </summary>
        private readonly ICommand _ClearCommand = new RoutedUICommand(nameof(ClearCommand), nameof(ClearCommand), typeof(SystemExclusivesWindow));

        /// <summary>
        /// Registers the command to copy the system exclusives to the clip board.
        /// </summary>
        private readonly ICommand _CopyCommand = new RoutedUICommand(nameof(CopyCommand), nameof(CopyCommand), typeof(SystemExclusivesWindow));

        /// <summary>
        /// Registers the command to send a system exclusive request.
        /// </summary>
        private readonly ICommand _SendCommand = new RoutedUICommand(nameof(SendCommand), nameof(SendCommand), typeof(SystemExclusivesWindow));

        #endregion

        #region Commands: Properties

        /// <summary>
        /// Gets the command to clear the <see cref="SystemExclusivesWindow"/>.
        /// </summary>
        public ICommand ClearCommand
        {
            get { return _ClearCommand; }
        }

        /// <summary>
        /// Gets the command to copy the system exclusives.to the clip board.
        /// </summary>
        public ICommand CopyCommand
        {
            get { return _CopyCommand; }
        }

        /// <summary>
        /// Gets the command to send a system exclusive request.
        /// </summary>
        public ICommand SendCommand
        {
            get { return _SendCommand; }
        }

        #endregion

        #region Commands: Handlers

        /// <summary>
        /// Clears the <see cref="SystemExclusivesWindow"/>.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> containing event data.</param>
        private void OnClearCommand(object sender, ExecutedRoutedEventArgs e)
        {
            SystemExclusives = string.Empty;
        }

        /// <summary>
        /// Copies system exclusives to the clip board.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> containing event data.</param>
        private void OnCopyCommand(object sender, ExecutedRoutedEventArgs e)
        {
            Clipboard.SetText(SystemExclusives);
        }

        /// <summary>
        /// Send the system exclusive request.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> containing event data.</param>
        private void OnSendCommand(object sender, ExecutedRoutedEventArgs e)
        {
            uint address = ConvertText(AddressTextBox.Text);
            uint request = ConvertText(RequestTextBox.Text);

            DeviceContext.SendSystemExclusive(address, request);
        }

        /// <summary>
        /// Determins if the <see cref="SendCommand"/> can be executed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> containing event data.</param>
        private void CanExecuteSendCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            if (AddressTextBox.Text.Length != 8 || RequestTextBox.Text.Length != 8)
            {
                e.CanExecute = false;
                return;
            }

            uint address = ConvertText(AddressTextBox.Text);
            uint request = ConvertText(RequestTextBox.Text);

            e.CanExecute = true;
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets the received system exclusives.
        /// </summary>
        public string SystemExclusives
        {
            get { return _SystemExclusives; }
            private set
            {
                _SystemExclusives = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private uint ConvertText(string text)
        {
            int length = text.Length;

            while(length != 8)
            {
                text.Prepend('0');
                length++;
            }

            byte[] bytes = new byte[4];

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(text.Substring(i * 2, 2), 16);
            }

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToUInt32(bytes, 0);
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles received system exclusive messages.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="IntegraSystemExclusiveEventArgs"/> containing event data.</param>
        private void SystemExclusiveReceived(object sender, IntegraSystemExclusiveEventArgs e)
        {
            if (e.Address.Like(FilterTextBox.Text))
            {
                SystemExclusives += e.Message;
                SystemExclusives += Environment.NewLine;
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Overrides the OnCloseCommand to remove the system exclusive received event handler
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> containing event data.</param>
        protected override void OnCloseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            DeviceContext.IntegraSystemExclusiveReceived -= SystemExclusiveReceived;

            base.OnCloseCommand(sender, e);
        }

        #endregion
    }
}
