using Integra.Core;
using Integra.Database;
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
    /// Interaction logic for PCMDrumKitNotesWindow.xaml
    /// </summary>
    public partial class PCMDrumKitNotesWindow : IntegraWindow
    {
        private List<string> _WaveForms;
        public List<string> WaveForms
        {
            get { return _WaveForms; }
        }

        private PCMDrumKitPartial _SelectedPartial;

        public PCMDrumKitNotesWindow()
        {
            _WaveForms = DataAccess.SelectWaveForms(IntegraWaveFormType.INT);
            SelectedPartial = DrumKitContext.Partials[0];
            InitializeComponent();

            DataContext = this;
        }

        public PCMDrumKit DrumKitContext
        {
            get { return DeviceContext.StudioSet.Parts[(int)DeviceContext.StudioSet.SelectedPart].TemporaryTone.PCMDrumKit; }
        }

        public PCMDrumKitPartial SelectedPartial
        {
            get { return _SelectedPartial; }
            set
            {
                _SelectedPartial = value;
                NotifyPropertyChanged();
            }
        }
    }
}
