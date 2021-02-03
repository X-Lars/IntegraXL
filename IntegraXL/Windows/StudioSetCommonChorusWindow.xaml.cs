using Integra.Core;
using Integra.Models;
using Integra.Models.MFX;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace IntegraXL.Windows
{
    /// <summary>
    /// Interaction logic for StudioSetCommonChorus.xaml
    /// </summary>
    public partial class StudioSetCommonChorusWindow : IntegraWindow
    {
        public StudioSetCommonChorusWindow()
        {
            InitializeComponent();

            DataContext = this;

            InitializeControl();
        }

        private UserControl _ChorusControl;

        public StudioSetCommonChorus CommonChorus
        {
            get { return DeviceContext.StudioSet.CommonChorus; }
        }

        public IntegraChorusTypes ChorusType
        {
            get
            {
                return CommonChorus.Type;
            }

            set
            {
                if (CommonChorus.Type != value)
                {
                    CommonChorus.Type = value;

                    NotifyPropertyChanged();
                    InitializeControl();
                }
            }
        }

        /// <summary>
        /// Gets or sets the MFX control for the <see cref="MFXWindow"/>.
        /// </summary>
        public UserControl ChorusControl
        {
            get { return _ChorusControl; }
            private set
            {
                _ChorusControl = value;
                NotifyPropertyChanged();
            }
        }

        private void InitializeControl()
        {
            Type chorusType;

            switch (ChorusType)
            {
                
                case IntegraChorusTypes.Chorus: 
                    chorusType = typeof(UserControls.MFX.CommonChorus); 
                    break;

                case IntegraChorusTypes.Delay:
                    chorusType = typeof(UserControls.MFX.CommonDelay);
                    break;

                case IntegraChorusTypes.GM2Chorus:
                    chorusType = typeof(UserControls.MFX.CommonGM2Chorus);
                    break;

                default:
                    chorusType = typeof(UserControls.MFX.CommonOff);
                    break;
            }

            ChorusControl = (UserControl)Activator.CreateInstance(chorusType);
        }

        private void ChorusTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializeControl();
        }
    }
}
