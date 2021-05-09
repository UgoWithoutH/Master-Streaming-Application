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
using System.Windows.Shapes;
using Class;

namespace Master_Streaming
{
    /// <summary>
    /// Logique d'interaction pour WatchlistWindow.xaml
    /// </summary>
    public partial class WatchlistWindow : Window
    {
        public ProfilManager Manager => (App.Current as App).Pmanager;

        public WatchlistWindow()
        {
            InitializeComponent();
            DataContext = Manager;
        }

        private void Back_To_Menu_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Remove_From_Watchlist_Clicked(object sender, RoutedEventArgs e)
        {
            Manager.MyWatchlist.SupprimerOeuvre(sender as Oeuvre);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
