using ControlsXL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IntegraXL.Windows
{
    /// <summary>
    /// Provides a base class for application windows without dependencies.
    /// </summary>
    public class CommonWindow : MDIChild, INotifyPropertyChanged 
    {
        /// <summary>
        /// Get the application main window context.
        /// </summary>
        public MainWindow ApplicationContext 
        {
            get { return (MainWindow)Application.Current.MainWindow; }
        }

        #region INotifyPropertyChanged

        /// <summary>
        /// Event to raise when a property value of an <see cref="INotifyPropertyChanged"/> implementing class is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method to call on a property of an <see cref="INotifyPropertyChanged"/> implementing class when it's value is changed.
        /// </summary>
        /// <param name="propertyName">A <see cref="string"/> equal to the name of the property that is changed.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
