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
        public ProfilManager manager => (App.Current as App).Pmanager;

        public MainWindow()
        {
            InitializeComponent();
            ListViewMenu.Visibility = Visibility.Collapsed;
            DataContext = manager;
            manager.GenreSélectionné = manager.ListOeuvres.Keys.FirstOrDefault();
            uc_listSeries.filtrage.SelectedItem = "Toutes dates";
            //uc_listSeries.trie.SelectedItem = "Alphabétique"; //Quand la liste sera faite pour se binde dessus
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            buttonAddGenre.Visibility = Visibility.Collapsed;
            buttonSuppGenre.Visibility = Visibility.Collapsed;
            boxAddGenre.Visibility = Visibility.Collapsed;
            boxSuppGenre.Visibility = Visibility.Collapsed;
            ListViewMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            buttonAddGenre.Visibility = Visibility.Visible;
            buttonSuppGenre.Visibility = Visibility.Visible;
            ListViewMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                manager.GenreSélectionné = (e.AddedItems[0] as Genre);
                CollapsedVisibility();
                uc_listSeries.filtrage.SelectedItem = "Toutes dates"; //valeur par défaut de la combobox du filtrage
                /*uc_listSeries.trie.SelectedIndex = 0;*/ //valeur par défaut de la combobox du trie
            }
        }

        private void CollapsedVisibility()
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            buttonAddGenre.Visibility = Visibility.Collapsed;
            buttonSuppGenre.Visibility = Visibility.Collapsed;
            boxSuppGenre.Visibility = Visibility.Collapsed;
            ListViewMenu.Visibility = Visibility.Collapsed;
        }

        private void AddGenreButton_Clicked(object sender, RoutedEventArgs e)
        {
            boxAddGenre.Visibility = Visibility.Visible;
            boxSuppGenre.Visibility = Visibility.Collapsed;
            MaterialDesignThemes.Wpf.HintAssist.SetHint(boxAddGenre, "Nom du genre à ajouter");
            boxAddGenre.Background = Brushes.Transparent;
        }

        private void SuppGenreButton_Clicked(object sender, RoutedEventArgs e)
        {
            boxAddGenre.Visibility = Visibility.Collapsed;
            boxSuppGenre.Visibility = Visibility.Visible;
            MaterialDesignThemes.Wpf.HintAssist.SetHint(boxSuppGenre, "Nom du genre à supprimer");
            boxAddGenre.Background = Brushes.Transparent;
        }

        private void AddGenreBox_Validated_With_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                    if (!manager.ListOeuvres.ContainsKey(new Genre(boxAddGenre.Text)))
                    {
                        manager.AjouterGenre(new Genre(boxAddGenre.Text));
                        boxAddGenre.Text = null;
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(boxAddGenre, "Nom du genre à ajouter");
                        boxAddGenre.Background = Brushes.Transparent;
                    }

                    else
                    {
                        boxAddGenre.Text = null;
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(boxAddGenre,"Renseignez un nom valide");
                        boxAddGenre.Background = Brushes.Tomato;
                        return;
                    }    
            }
        }

        private void SuppGenreBox_Validated_With_Enter(object sender, KeyEventArgs e)
        {
            //bool isExistant = false;

            if (e.Key == Key.Return)
            {
                if (manager.ListOeuvres.ContainsKey(new Genre(boxSuppGenre.Text.ToUpper())))
                {
                    if (manager.GenreSélectionné.Nom.Equals(boxSuppGenre.Text.ToUpper()))
                    {
                        manager.ChangeGenreSélectionné(manager.ListOeuvres, boxSuppGenre.Text);
                        CollapsedVisibility();
                        Storyboard sb = this.FindResource("CloseMenu") as Storyboard; //pour chercher la Storyboard (animation) dans MainWindow.xaml
                        sb.Begin(); //lancer la Storyboard
                    }
                    manager.SupprimerGenre(new Genre(boxSuppGenre.Text));
                    boxSuppGenre.Text = null;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(boxSuppGenre, "Nom du genre à supprimer");
                    boxSuppGenre.Background = Brushes.Transparent;
                }

                else
                {
                    boxSuppGenre.Text = null;
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(boxSuppGenre, "Renseignez un nom valide");
                    boxSuppGenre.Background = Brushes.Tomato;
                }
            }
        }
    }
}
