using Integra.Core;
using Integra.Models;
using IntegraXL.UserControls.MFX;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MFXWindow.xaml
    /// </summary>
    public partial class MFXWindow : IntegraWindow
    {
        //public static DependencyProperty MFXControlProperty = DependencyProperty.Register("ToneBank", typeof(IntegraBaseToneBank), typeof(ToneBankWindow), new PropertyMetadata(null));


        public UserControl MFXControl
        {
            get { return (UserControl)GetValue(MFXControlProperty); }
            set { SetValue(MFXControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MFXControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MFXControlProperty =
            DependencyProperty.Register(nameof(MFXControl), typeof(UserControl), typeof(MFXWindow), new PropertyMetadata(null));


        public MFXWindow(IntegraMFXTypes type)
        {
            InitializeComponent();

            DataContext = this;

            switch(type)
            {
                case IntegraMFXTypes.Thru:
                    break;
                case IntegraMFXTypes.Equalizer:
                    MFXControl = (UserControl)Activator.CreateInstance(typeof(Equalizer));
                    break;
            }

            
        }

        public IToneMFX MFXContext
        {
            get { return DeviceContext.MFX; }
        }


    }
}
