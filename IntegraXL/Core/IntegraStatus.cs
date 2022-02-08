using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IntegraXL.Core
{
    public class IntegraStatus : INotifyPropertyChanged
    {
        private int _Progress;
        private string _Message = string.Empty;
        private string _Text = string.Empty;
        private string _Operation = string.Empty;

        public IntegraStatus() { }

        /// <summary>
        /// Gets the a description of the current operation.
        /// </summary>
        public string Operation
        {
            get => _Operation;
            internal set
            {
                _Operation = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the progress of the current operation.
        /// </summary>
        public int Progress
        {
            get => _Progress;
            internal set
            {
                _Progress = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the user message for the current operation.
        /// </summary>
        public string Message
        {
            get => _Message;
            internal set
            {
                _Message = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the status text.
        /// </summary>
        public string Text
        {
            get => _Text;
            internal set
            {
                _Text = value;
                NotifyPropertyChanged();
            }
        }

        #region Interfaces: INotifyPropertyChanged

        /// <summary>
        /// Event to raise when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Method to invoke when a property is changed.
        /// </summary>
        /// <param name="propertyName">The name of the property, defaults to the name of the property.</param>
        /// <param name="index">The index of the property, if applicable.</param>
        public virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        #endregion
    }
}
