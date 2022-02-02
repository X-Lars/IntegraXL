using System.ComponentModel;
using System.Windows.Input;

namespace IntegraXL.Common
{
    /// <summary>
    /// Defines an UI bindable command.
    /// </summary>
    // TODO: REMOVE 
    public class UICommand : ICommand
    {
        #region Fields

        /// <summary>
        /// Reference to the method to execute.
        /// </summary>
        private Action _Execute;

        /// <summary>
        /// Reference to the can execute conditions function.
        /// </summary>
        private Func<bool>? _CanExecute;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the execution conditions are changed.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The method to excecute on command invokation.</param>
        internal UICommand(Action execute) : this(execute, null, null) { }

        /// <summary>
        /// Creates a new command with validation.
        /// </summary>
        /// <param name="execute">The method to execute on command invokation.</param>
        /// <param name="canExecute">The function to validate if the command can be executed.</param>
        /// <param name="provider">Optional property change provider to reevalute if the command can be executed.</param>
        internal UICommand(Action execute, Func<bool>? canExecute, INotifyPropertyChanged? provider = null)
        {
            _Execute    = execute;
            _CanExecute = canExecute;

            if (provider != null && canExecute != null)
                provider.PropertyChanged += PropertyChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the can execute changed event when a property of the associated model is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Action raiseChange = () => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            Task.Run(() => raiseChange);
            
            //Synchronization problem
            //CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Gets whether the command can be executed.
        /// </summary>
        /// <param name="parameter">Unused.</param>
        /// <returns>True if the command can be executed, false otherwise.</returns>
        public bool CanExecute(object? parameter)
        {
            return _CanExecute == null || _CanExecute();
        }

        /// <summary>
        /// Executes the method associated with the command.
        /// </summary>
        /// <param name="parameter">Unused.</param>
        public void Execute(object? parameter)
        {
            _Execute();
        }

        #endregion
    }
}
