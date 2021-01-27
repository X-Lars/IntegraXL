using Integra;
using Integra.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace IntegraXL.UserControls
{
    /// <summary>
    /// Interaction logic for Favorites.xaml
    /// </summary>
    public partial class FavoritesList : UserControl, INotifyPropertyChanged
    {
        public Favorites Favorites { get; set; } = new Favorites();

        public FavoritesList()
        {
            InitializeComponent();

            DataContext = this;
            Favorites.Initialize();

            Device.Instance.VirtualSlots.PropertyChanged += VirtualSlotsPropertyChanged;
        }

        /// <summary>
        /// Event to raised when a property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Raises the property changed event for the specified property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void VirtualSlotsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(Favorites));
        }
    }
}
