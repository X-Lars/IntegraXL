using Integra.Core;
using Integra.Core.Interfaces;
using IntegraXL.UserControls.MFX;
using System;
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
        public MFXWindow(IntegraMFXTypes type)
        {
            InitializeComponent();

            DataContext = this;

            Type = type;
            Type mfxType = typeof(Thru);

            switch(type)
            {
                case IntegraMFXTypes.Equalizer: mfxType = typeof(Equalizer); break;
            }

            MFXControl = (UserControl)Activator.CreateInstance(mfxType);
        }

        #endregion

        #region Dependency Properties

        #region Dependency Properties : Registration

        /// <summary>
        /// Registers the property to get the MFX control for the <see cref="MFXWindow"/>.
        /// </summary>
        public static readonly DependencyProperty MFXControlProperty = DependencyProperty.Register(nameof(MFXControl), typeof(UserControl), typeof(MFXWindow), new PropertyMetadata(null));



        public IntegraMFXTypes Type
        {
            get { return (IntegraMFXTypes)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(IntegraMFXTypes), typeof(MFXWindow), new PropertyMetadata(IntegraMFXTypes.Thru, new PropertyChangedCallback(TypePropertyChanged)));

        private static void TypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            //throw new NotImplementedException();
        }


        #endregion

        #region Dependency Properties : Implementation

        /// <summary>
        /// Gets or sets the MFX control for the <see cref="MFXWindow"/>.
        /// </summary>
        public UserControl MFXControl
        {
            get { return (UserControl)GetValue(MFXControlProperty); }
            set { SetValue(MFXControlProperty, value); }
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets the MFX data context for the MFX control.
        /// </summary>
        public IToneMFX MFXContext
        {
            get { return DeviceContext.StudioSet.MFXDataContext; }
        }

        #endregion
    }
}
