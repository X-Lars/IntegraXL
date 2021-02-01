using Integra.Models;

namespace IntegraXL.Windows
{
    /// <summary>
    /// Interaction logic for StudioSetPartEQWindow.xaml
    /// </summary>
    public partial class StudioSetPartEQWindow : IntegraWindow
    {
        public StudioSetPartEQWindow()
        {
            InitializeComponent();

            DataContext = this;
        }


        public StudioSetPartEQ PartsEQ
        {
            get { return DeviceContext.StudioSet.PartsEQ[(int)DeviceContext.StudioSet.SelectedPart]; }
        }
    }
}
