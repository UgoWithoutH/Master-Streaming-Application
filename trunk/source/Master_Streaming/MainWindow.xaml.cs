using Class;
using Swordfish.NET.Collections.Auxiliary;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Master_Streaming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ProfilManager manager => (App.Current as App).Mmanager.ProfilCourant;

        /// <summary>
        /// Permet de savoir quel mode de couleur est en cours
        /// </summary>
        public int ColorMode; //0 = sombre et 1 = clair

        private bool ctrlKey;
        public MainWindow()
        {
            InitializeComponent();
            VisibilyCollapsedHeader();
        }

        /// <summary>
        /// permet de masquer des éléments du header de notre application
        /// </summary>
        public void VisibilyCollapsedHeader()
        {
            header.watchlist.Visibility = Visibility.Collapsed;
            header.recherche.Visibility = Visibility.Collapsed;
            header.ajout.Visibility = Visibility.Collapsed;
            header.deconnexion.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Permet de rendre visible des éléments du header de notre application
        /// </summary>
        public void VisibilyVisibledHeader()
        {
            header.watchlist.Visibility = Visibility.Visible;
            header.recherche.Visibility = Visibility.Visible;
            header.ajout.Visibility = Visibility.Visible;
            header.deconnexion.Visibility = Visibility.Visible;
        }

        private void raccourcis_key(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
                ctrlKey = true;

            if(ctrlKey && e.Key == Key.A && (contentControlMain.Content is UC_Master || contentControlMain.Content is UC_Recherche))
            {
                (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Watchlist();
                ctrlKey = false;
            }

            else if(ctrlKey && e.Key == Key.Z && (contentControlMain.Content is UC_Master || contentControlMain.Content is UC_Recherche))
            {
                (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Ajouter();
                ctrlKey = false;
            }

            else if(ctrlKey && e.Key == Key.S && (contentControlMain.Content is UC_Master || contentControlMain.Content is UC_Recherche))
            {
                header.ButtonPopUpLogout_Click(new Object(),new RoutedEventArgs());
            }

            else if(ctrlKey && e.Key == Key.E && (contentControlMain.Content is UC_Master || contentControlMain.Content is UC_Recherche || contentControlMain.Content is UC_ChoixProfil))
            {
                ToggleButton MyToggleButton = this.header.ColorMode;
                if (MyToggleButton.IsChecked == true)
                {
                    MyToggleButton.IsChecked = false;
                    header.ToggleButton_Click(MyToggleButton, new RoutedEventArgs());
                }

                else
                {
                    MyToggleButton.IsChecked = true;
                    header.ToggleButton_Click(MyToggleButton, new RoutedEventArgs());
                }

            }


        }
    }
}
