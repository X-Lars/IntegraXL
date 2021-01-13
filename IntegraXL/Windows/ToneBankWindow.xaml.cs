using Integra;
using Integra.Core;
using Integra.Models;
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
    /// Interaction logic for ToneBankWindow.xaml
    /// </summary>
    public partial class ToneBankWindow : IntegraWindow
    {
        public static DependencyProperty ToneBankProperty = DependencyProperty.Register("ToneBank", typeof(IntegraBaseToneBank), typeof(ToneBankWindow), new PropertyMetadata(null));

        public ToneBankWindow(Type type) : base()
        {
            InitializeComponent();

            DataContext = this;

            ToneBank = Activator.CreateInstance(type) as IntegraBaseToneBank;
            ToneBank.Initialize();
        }

        public IntegraBaseToneBank ToneBank
        {
            get { return (IntegraBaseToneBank)GetValue(ToneBankProperty); }
            set { SetValue(ToneBankProperty, value); NotifyPropertyChanged(); }
        }


    }
}
