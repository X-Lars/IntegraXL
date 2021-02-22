using Integra;
using Integra.Core;
using Integra.Database;
using Integra.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace IntegraXL.UserControls
{
    /// <summary>
    /// Interaction logic for Favorites.xaml
    /// </summary>
    public partial class FavoritesList : ItemsControl
    {
        public FavoritesList()
        {
            InitializeComponent();

            DataContext = this;

            if(!DesignerProperties.GetIsInDesignMode(this))
                ItemsSource = DataAccess.SelectAll<Tone>();

            CommandBindings.Add(new CommandBinding(Select, OnSelect, CanExecuteSelect));
            CommandBindings.Add(new CommandBinding(Remove, OnRemove));
        }

       
        private void OnRemove(object sender, ExecutedRoutedEventArgs e)
        {
            DataAccess.Delete(((Tone)((Button)e.OriginalSource).DataContext));

            ItemsSource = DataAccess.SelectAll<Tone>();
        }

        private void CanExecuteSelect(object sender, CanExecuteRoutedEventArgs e)
        {
            Tone tone = ((Tone)e.Parameter);

            if (tone != null)
            {
                e.CanExecute = Device.Instance.VirtualSlots[tone.GetExpansion()];
            }
            else
                e.CanExecute = false;
        }

        public void OnSelect(object sender, ExecutedRoutedEventArgs e)
        {
            Device.Instance.StudioSet.Tone = ((Tone)e.Parameter);
        }

        public static RoutedUICommand _SelectCommand = new RoutedUICommand(nameof(Select), nameof(Select), typeof(FavoritesList));
        public static RoutedUICommand _RemoveCommand = new RoutedUICommand(nameof(Remove), nameof(Remove), typeof(ContextMenu));

        public ICommand Select
        {
            get { return _SelectCommand; }
        }
        public ICommand Remove
        {
            get { return _RemoveCommand; }
        }

        private void ContextMenuOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            (sender as ContextMenu).DataContext = this;
        }
    }
}
