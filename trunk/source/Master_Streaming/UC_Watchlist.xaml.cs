using Class;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Master_Streaming
{
    /// <summary>
    /// Logique d'interaction pour UC_Watchlist.xaml
    /// </summary>
    public partial class UC_Watchlist : UserControl
    {
        public ProfilManager manager => (App.Current as App).Mmanager.ProfilCourant;

        public UC_Watchlist()
        {
            InitializeComponent();
            DataContext = manager;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            manager.OeuvreWatchSélectionnée = (OeuvreWatch)((ListBoxItem)MyListInWatchlist.ContainerFromElement((Button)sender)).Content;
            manager.MyWatchlist.SupprimerOeuvre(manager.OeuvreWatchSélectionnée.Oeuvre);
        }
    }
}
