using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IntegraXL.Core
{
    public class IntegraStatus : INotifyPropertyChanged
    {
        private readonly Integra _Device;

        public IntegraStatus(Integra device)
        {
            _Device = device;
            _Device.PropertyChanged += DevicePropertyChanged;
        }

        
        private int _Progress;
        private string _Task = string.Empty;

        public int Progress
        {
            get => _Progress;
            set
            {
                _Progress = value;
                NotifyPropertyChanged();
            }
        }
        public string Task
        {
            get => _Task;
            set
            {
                _Task = value;
                NotifyPropertyChanged();
            }
        }

        #region Event Handlers

        private void DevicePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
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
        public virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "", int? index = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        #endregion
    }
}
