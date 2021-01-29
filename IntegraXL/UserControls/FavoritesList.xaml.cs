using Integra;
using Integra.Core;
using Integra.Database;
using Integra.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace IntegraXL.UserControls
{
    /// <summary>
    /// Interaction logic for Favorites.xaml
    /// </summary>
    public partial class FavoritesList : ItemsControl, INotifyPropertyChanged
    {
        public Favorites Favorites { get; set; } = new Favorites();

        public FavoritesList()
        {
            InitializeComponent();

            DataContext = this;

            Favorites.Initialize();
            ItemsSource = Favorites;

            CommandBindings.Add(new CommandBinding(Select, OnSelect, CanExecuteSelect));
            CommandBindings.Add(new CommandBinding(Remove, OnRemove));

            //Focus();
        }

       
        private void OnRemove(object sender, ExecutedRoutedEventArgs e)
        {
            //IntegraTone tone = ((IntegraTone)e.Parameter);
            
            Console.WriteLine("Remove tone");
            Console.WriteLine(sender);
            Console.WriteLine(e.OriginalSource);
            Console.WriteLine(((Button)e.OriginalSource).DataContext);
            Console.WriteLine(e.Parameter);

            Console.WriteLine($"Remove: {((IntegraTone)((Button)e.OriginalSource).DataContext).Name}");

            Tone tone = new Tone(((IntegraTone)((Button)e.OriginalSource).DataContext));
            //tone.Delete(tone.ID);
            DataAccess.Delete(tone);
            Favorites.Initialize();
            //NotifyPropertyChanged(nameof(Favorites));
        }

        private void CanExecuteSelect(object sender, CanExecuteRoutedEventArgs e)
        {
            IntegraTone tone = ((IntegraTone)e.Parameter);

            e.CanExecute = Device.Instance.VirtualSlots[tone.Expansion];
        }

        public void OnSelect(object sender, ExecutedRoutedEventArgs e)
        {
            Device.Instance.StudioSet.Tone = new Tone((IntegraTone)e.Parameter);
        }

        public static RoutedUICommand _SelectCommand = new RoutedUICommand(nameof(Select), nameof(Select), typeof(FavoritesList));
        public static RoutedUICommand _RemoveCommand = new RoutedUICommand(nameof(Remove), nameof(Remove), typeof(ContextMenu));

        public event PropertyChangedEventHandler PropertyChanged;

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

        #region INotifyPropertyChanged

        /// <summary>
        /// Event raised when a property value is changed.
        /// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the specified property.
        /// </summary>
        /// <param name="propertyName">A <see cref="string"/> containing the name of the property that is changed.</param>
        /// <remarks><i>If no property name is specified, the actual name of the property in code is used.</i></remarks>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
