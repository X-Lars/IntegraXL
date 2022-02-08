using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IntegraXL.Core
{
    public class IntegraStatus : INotifyPropertyChanged
    {
        public IntegraStatus() { }

        private int _Progress;
        private string _Message = string.Empty;
        private string _Text = string.Empty;
        private string _Operation = string.Empty;

        public string Operation
        {
            get => _Operation;
            set
            {
                _Operation = value;
                NotifyPropertyChanged();
            }
        }

        public int Progress
        {
            get => _Progress;
            set
            {
                _Progress = value;
                NotifyPropertyChanged();
            }
        }

        public string Message
        {
            get => _Message;
            set
            {
                _Message = value;
                NotifyPropertyChanged();
            }
        }

        #region Event Handlers

        private void DevicePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public string Text
        {
            get => _Text;
            set
            {
                _Text = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

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
