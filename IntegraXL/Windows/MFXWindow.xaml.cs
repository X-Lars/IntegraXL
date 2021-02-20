using Integra;
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

        private UserControl _MFXControl;

        #region Constructor

        /// <summary>
        /// Creates and initialize a new <see cref="MFXWindow"/> instance for the specified <see cref="IntegraMFXTypes"/>.
        /// </summary>
        /// <param name="type">The <see cref="IntegraMFXTypes"/> to create the window for.</param>
        public MFXWindow()
        {
            InitializeComponent();

            DataContext = this;

            MFX.Reinitialize();

            InitializeControl();

            DeviceContext.StudioSet.PartChanged += StudioSetPartChanged;
            DeviceContext.StudioSet.ToneChanged += StudioSetToneChanged;
        }

        
        private void StudioSetToneChanged(object sender, IntegraToneChangedEventArgs e)
        {
            InitializeControl();
        }

        private void StudioSetPartChanged(object sender, IntegraPartChangeEventArgs e)
        {
            InitializeControl();
        }

        private void MFXTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializeControl();
        }

        #endregion

        #region Properties

        public ToneMFX MFX
        {
            get { return StudioSetContext.Parts[(int)StudioSetContext.SelectedPart].TemporaryTone.MFX; }
        }


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

      
        #endregion

        #region Methods

        private void InitializeControl()
        {
            Type type = typeof(Thru);

            switch (MFX.Type)
            {
                case IntegraMFXTypes.Equalizer:  type = typeof(Equalizer);  break;
                case IntegraMFXTypes.Spectrum:   type = typeof(Spectrum);   break;
                case IntegraMFXTypes.LowBoost:   type = typeof(LowBoost);   break;
                case IntegraMFXTypes.StepFilter: type = typeof(StepFilter); break;
            }

            MFXControl = (UserControl)Activator.CreateInstance(type);
            NotifyPropertyChanged(nameof(MFX));
        }

        #endregion

    }
}
