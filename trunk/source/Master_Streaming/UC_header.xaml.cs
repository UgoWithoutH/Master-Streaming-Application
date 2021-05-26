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
        public MainManager Mmanager => (App.Current as App).Mmanager;

        public ProfilManager Pmanager => (App.Current as App).Mmanager.ProfilCourant;

        public UC_header()
        {
            InitializeComponent();
            DataContext = Pmanager;
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Mmanager.SauvegardeDonnées();
            (App.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_ChoixProfil();
            Mmanager.Deconnexion();
        }

        private void Ajouter_Button_Clicked(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Ajouter();
        }

        private void Watchlist_Button_Clicked(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Watchlist();
        }

        private void Recherche()
        {
            if (!String.IsNullOrWhiteSpace(entree_Recherche.Text))
            {
                (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Recherche(entree_Recherche.Text);
                Pmanager.ListRecherche = Pmanager.Recherche(entree_Recherche.Text);
            }
        }

        private void Enter_Recherche(object sender, RoutedEventArgs e)
        {
            Recherche();
        }

        private void entree_Recherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Recherche();
            }
        }
    }
}
