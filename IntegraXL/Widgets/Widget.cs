using Integra;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace IntegraXL.Widgets
{
    /// <summary>
    /// Represents the base class for all widgets, provides application and device context properties and implements the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public abstract class Widget : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Creates and initalizes a new <see cref="Widget"/> instance.
        /// </summary>
        public Widget() : base()
        {
            DataContext = this;
        }

        #region Properties

        /// <summary>
        /// Gets the application context.
        /// </summary>
        public MainWindow ApplicationContext
        {
            get { return (MainWindow)Application.Current.MainWindow; }
        }

        /// <summary>
        /// Gets the device context.
        /// </summary>
        public Device DeviceContext
        {
            get { return ApplicationContext.Integra; }
        }

        #endregion

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
