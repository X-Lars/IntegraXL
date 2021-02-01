using Integra;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace IntegraXL.Windows
{
    /// <summary>
    /// Interaction logic for SessionsWindow.xaml
    /// </summary>
    public partial class SessionsWindow : CommonWindow
    {
        public SessionsWindow()
        {
            InitializeComponent();

            DataContext = this;

            CommandBindings.Add(new CommandBinding(_LoadCommand, OnLoad));
        }

        public Device DeviceContext
        {
            get { return ApplicationContext.Integra; }
        }

        private void OnLoad(object sender, ExecutedRoutedEventArgs e)
        {
            SessionsWindow caller = sender as SessionsWindow;

            if (caller != null)
            {
                Console.WriteLine((int)Sessions.SelectedValue);
                Device.Session.Select((int)Sessions.SelectedValue);
            }
        }

        public static RoutedUICommand _LoadCommand = new RoutedUICommand(nameof(Load), nameof(Load), typeof(SessionsWindow));

        public static ICommand Load
        {
            get { return _LoadCommand; }
        }
    }
}
