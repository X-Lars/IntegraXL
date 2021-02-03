using Integra.Core;
using Integra.Models;
using System;
using System.Windows.Controls;

namespace IntegraXL.Windows
{
    /// <summary>
    /// Interaction logic for StudioSetCommonReverbWindow.xaml
    /// </summary>
    public partial class StudioSetCommonReverbWindow : IntegraWindow
    {
        public StudioSetCommonReverbWindow()
        {
            InitializeComponent();

            DataContext = this;

            InitializeControl();
        }

        private UserControl _ReverbControl;

        public StudioSetCommonReverb CommonReverb
        {
            get { return DeviceContext.StudioSet.CommonReverb; }
        }

        public IntegraReverbTypes ReverbType
        {
            get
            {
                return CommonReverb.Type;
            }

            set
            {
                if (CommonReverb.Type != value)
                {
                    CommonReverb.Type = value;

                    NotifyPropertyChanged();
                    InitializeControl();
                }
            }
        }

        /// <summary>
        /// Gets or sets the MFX control for the <see cref="MFXWindow"/>.
        /// </summary>
        public UserControl ReverbControl
        {
            get { return _ReverbControl; }
            private set
            {
                _ReverbControl = value;
                NotifyPropertyChanged();
            }
        }

        private void InitializeControl()
        {
            Type reverbType;

            switch (ReverbType)
            {

                case IntegraReverbTypes.Room1:
                case IntegraReverbTypes.Room2:
                case IntegraReverbTypes.Hall1:
                case IntegraReverbTypes.Hall2:
                case IntegraReverbTypes.Plate:
                    reverbType = typeof(UserControls.MFX.CommonReverbRoom);
                    break;

                case IntegraReverbTypes.GM2:
                    reverbType = typeof(UserControls.MFX.CommonReverbGM2);
                    break;

                default:
                    reverbType = typeof(UserControls.MFX.CommonOff);
                    break;
            }

            ReverbControl = (UserControl)Activator.CreateInstance(reverbType);
        }

        private void ReverbTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializeControl();
        }
    }
}
