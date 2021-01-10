using IntegraXL.Widgets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IntegraXL.Windows
{
    /// <summary>
    /// Interaction logic for MidiDevicesWindow.xaml
    /// </summary>
    public partial class MidiDevicesWindow : CommonWindow, INotifyPropertyChanged
    {
        public MidiDevicesWindow() : base()
        {
            InitializeComponent();

            Widget = new MidiDevicesWidget();
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
