using Integra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
using ControlsXL;
using Integra.Core;

namespace IntegraXL
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window//, INotifyPropertyChanged
    {
        #region Constructor
        
        /// <summary>
        /// Creates and initializes the <see cref="MainWindow"/> which is the application's starting point.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            StyleManager.Style = ControlStyle.Default;

            // [REQUIRED]
            Device.StatusChanged += DeviceStatusChanged;
            Device.Error += DeviceOnError;
            DataContext = this;

        }

       
        #endregion

        #region Properties

        /// <summary>
        /// Gets a reference to the INTEGRA-7.
        /// </summary>
        public Device Integra 
        { 
            get { return Device.Instance; }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the <see cref="Device.StatusChanged"/> event.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the class that raised the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing event data.</param>
        private void DeviceStatusChanged(object sender, IntegraEventArgs e)
        {
            switch (e.StatusFlags)
            {
                case DeviceStatusFlags.DEVICE_NO_MIDI_OUTPUT_DEVICES:
                    // TODO: Show devices window
                    break;
            }
        }

        /// <summary>
        /// Handles the <see cref="Device.Error"/> event.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the class that raised the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing event data.</param>
        private void DeviceOnError(object sender, IntegraEventArgs e)
        {
            throw new NotImplementedException();
        }


        #endregion

        //#region INotifyPropertyChanged

        ///// <summary>
        ///// Event raised when a property value is changed.
        ///// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;

        ///// <summary>
        ///// Raises the <see cref="PropertyChanged"/> event for the specified property.
        ///// </summary>
        ///// <param name="propertyName">A <see cref="string"/> containing the name of the property that is changed.</param>
        ///// <remarks><i>If no property name is specified, the actual name of the property in code is used.</i></remarks>
        //private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //#endregion
    }
}
