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
    /// Interaction logic for SuperNATURALDrumKitNotesWindow.xaml
    /// </summary>
    public partial class SuperNATURALDrumKitNotesWindow : IntegraWindow
    {

        private List<string> _WaveForms;
        public List<string> WaveForms
        {
            get { return _WaveForms; }
        }


        SuperNATURALDrumKitNote _SelectedNote;

        public SuperNATURALDrumKitNotesWindow()
        {
            _WaveForms = DataAccess.SelectWaveForms(IntegraWaveFormType.SND);

            InitializeComponent();

            DataContext = this;

        }
       
        public SuperNATURALDrumKit DrumKitContext
        {
            get { return DeviceContext.StudioSet.Parts[(int)DeviceContext.StudioSet.SelectedPart].TemporaryTone.SuperNATURALDrumKit; }
        }

        public SuperNATURALDrumKitNote SelectedNote
        {
            get { return _SelectedNote; }
            set
            {
                _SelectedNote = value;
                NotifyPropertyChanged();
            }
        }

    }
}
