using Class;
using System;
using System.Collections.Generic;
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
        private ProfilManager manager = new ProfilManager();

        public MainWindow()
        {
            InitializeComponent();
            ListViewMenu.ItemsSource = manager.ListOeuvres.Keys;
            ListViewMenu.Visibility = Visibility.Collapsed;
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
            uc_listSeries.texteGenre.Text = (e.AddedItems[0] as Genre).getNom();
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            buttonAddGenre.Visibility = Visibility.Collapsed;
            buttonSuppGenre.Visibility = Visibility.Collapsed;
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
                        ListViewMenu.ItemsSource = manager.ListOeuvres.Keys;
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
                if (manager.ListOeuvres.ContainsKey(new Genre(boxSuppGenre.Text)))
                {
                   manager.SupprimerGenre(new Genre(boxSuppGenre.Text));
                   ListViewMenu.ItemsSource = manager.ListOeuvres.Keys;
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
