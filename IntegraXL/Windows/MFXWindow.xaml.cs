using Integra.Core;
using Integra.Core.Interfaces;
using Integra.Models;
using IntegraXL.UserControls.MFX;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace IntegraXL.Windows
{
    /// <summary>
    /// Base window for all MFX controls, provides a data context for the MFX based on the tone type and attaches the MFX associated user control.
    /// </summary>
    public partial class MFXWindow : IntegraWindow
    {
        #region Constructor

        /// <summary>
        /// Creates and initialize a new <see cref="MFXWindow"/> instance for the specified <see cref="IntegraMFXTypes"/>.
        /// </summary>
        /// <param name="type">The <see cref="IntegraMFXTypes"/> to create the window for.</param>
        public MFXWindow()
        {
            InitializeComponent();

            DataContext = this;

            InitializeControl();

            DeviceContext.StudioSet.PropertyChanged += StudioSetPropertyChanged;
        }

        private void StudioSetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(StudioSet.MFXDataContext))
            {
                NotifyPropertyChanged(nameof(MFXContext));
            }
        }

        private void MFXTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializeControl();
            //MFXContext.MFX.Reinitialize();
        }

        #endregion

        #region Properties

        public IntegraMFXTypes MFXType 
        { 
            get
            {
                return MFXContext.MFX.Type;
            }

            set
            {
                if (MFXContext.MFX.Type != value)
                {
                    MFXContext.MFX.Type = value;

                    NotifyPropertyChanged();
                    InitializeControl();
                }
            }
        }

        

        private UserControl _MFXControl;
        /// <summary>
        /// Gets or sets the MFX control for the <see cref="MFXWindow"/>.
        /// </summary>
        public UserControl MFXControl
        {
            get { return _MFXControl; }
            private set 
            { 
                _MFXControl = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the MFX data context for the MFX control.
        /// </summary>
        public IToneMFX MFXContext
        {
            get { return DeviceContext.StudioSet.MFXDataContext; }
        }

        #endregion

        #region Methods

        private void InitializeControl()
        {
            Type mfxType = typeof(Thru);

            switch (MFXType)
            {
                case IntegraMFXTypes.Equalizer: mfxType = typeof(Equalizer); break;
                case IntegraMFXTypes.Spectrum: mfxType = typeof(Spectrum); break;
            }

            MFXControl = (UserControl)Activator.CreateInstance(mfxType);
        }

        #endregion

        #region Overrides

       
        #endregion
    }
}
