using Class;
using System;
using System.Collections.Generic;
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
        private MainManager manager = new MainManager();

        public MainWindow()
        {
            InitializeComponent();
            ListViewMenu.ItemsSource = manager.listGenres;
        }



      

        private const string TEXT_ADD_GENRE = "Nom du genre à ajouter";
        private const string TEXT_SUPP_GENRE = "Nom du genre à supprimer";

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            buttonAddGenre.Visibility = Visibility.Collapsed;
            buttonSuppGenre.Visibility = Visibility.Collapsed;
            boxAddGenre.Visibility = Visibility.Collapsed;
            boxSuppGenre.Visibility = Visibility.Collapsed;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            buttonAddGenre.Visibility = Visibility.Visible;
            buttonSuppGenre.Visibility = Visibility.Visible;
            boxAddGenre.Text = TEXT_ADD_GENRE;
            boxSuppGenre.Text = TEXT_SUPP_GENRE;
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
            boxSuppGenre.Text = TEXT_SUPP_GENRE;
        }

        private void SuppGenreButton_Clicked(object sender, RoutedEventArgs e)
        {
            boxAddGenre.Visibility = Visibility.Collapsed;
            boxSuppGenre.Visibility = Visibility.Visible;
            boxAddGenre.Text = TEXT_ADD_GENRE;
        }

        private void AddGenreBox_Validated_With_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                foreach(Genre genre in manager.listGenres)  //liste sans doublons
                    if (boxAddGenre.Text != TEXT_ADD_GENRE && boxAddGenre.Text != null && boxAddGenre.Text.ToUpper() != genre.getNom())
                    {
                        continue;
                    }

                    else
                    {
                        boxAddGenre.Text = "Renseignez un nom valide";
                        boxAddGenre.Foreground = Brushes.Red;
                        return;
                    }

                manager.listGenres.Add(new Genre(boxAddGenre.Text));
                ListViewMenu.ItemsSource = manager.listGenres;
                boxAddGenre.Text = TEXT_ADD_GENRE;
                boxAddGenre.Foreground = Brushes.Black;
            }
        }

        private void SuppGenreBox_Validated_With_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (boxSuppGenre.Text != TEXT_SUPP_GENRE && boxSuppGenre.Text != null)
                {
                    manager.listGenres.Remove(new Genre(boxSuppGenre.Text));
                    ListViewMenu.ItemsSource = manager.listGenres;
                    boxSuppGenre.Text = TEXT_SUPP_GENRE;
                    boxSuppGenre.Foreground = Brushes.Black;
                }

                else
                {
                    boxSuppGenre.Text = "Renseignez un nom valide";
                    boxSuppGenre.Foreground = Brushes.Red;
                }
            }
        }
    }
}
