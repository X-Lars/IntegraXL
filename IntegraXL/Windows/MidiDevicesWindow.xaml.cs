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
    public partial class MidiDevicesWindow : CommonWindow
    {
        public MidiDevicesWindow() : base()
        {
            InitializeComponent();

            Widget = new MidiDevicesWidget();
        }
       
    }
}
