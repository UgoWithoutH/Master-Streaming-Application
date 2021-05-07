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
using Class;

namespace Master_Streaming
{
    /// <summary>
    /// Logique d'interaction pour UC_header.xaml
    /// </summary>
    public partial class UC_header : UserControl
    {
        public ProfilManager Manager => (App.Current as App).Pmanager;

        public UC_header()
        {
            InitializeComponent();
            DataContext = Manager;
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Ajouter_Button_Clicked(object sender, RoutedEventArgs e)
        {
            var AddWindow = new AjouterWindow();
            AddWindow.Show();
        }

        private void Watchlist_Button_Clicked(object sender, RoutedEventArgs e)
        {
            var theWatchlist = new WatchlistWindow();
            theWatchlist.Show();
        }

        private void Recherche_Button_Clicked(object sender, RoutedEventArgs e)
        {
            var theRecherche = new RechercheWindow();
            theRecherche.Show();
        }
    }
}
