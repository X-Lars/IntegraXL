using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{
    public class IntegraStatus : INotifyPropertyChanged
    {
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
