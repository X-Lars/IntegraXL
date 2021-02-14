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
    /// Interaction logic for StudioSetPartWindow.xaml
    /// </summary>
    public partial class StudioSetPartWindow : IntegraWindow
    {
        public StudioSetPartWindow()
        {
            InitializeComponent();

            DataContext = this;

            StudioSetContext.PartChanged += StudioSetContextPartChanged;
            StudioSetContext.ToneChanged += StudioSetContextToneChanged;
        }

        public StudioSetPart Part
        {
            get { return StudioSetContext.Parts[(int)StudioSetContext.SelectedPart]; }
        }

        private void StudioSetContextToneChanged(object sender, Integra.Core.IntegraToneChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(Part));
        }

        private void StudioSetContextPartChanged(object sender, Integra.Core.IntegraPartChangeEventArgs e)
        {
            NotifyPropertyChanged(nameof(Part));
        }
    }
}
