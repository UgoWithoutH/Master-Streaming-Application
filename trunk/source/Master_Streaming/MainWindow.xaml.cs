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
        public MainWindow()
        {
            InitializeComponent();
            var listGenres = new List<Genre>()
            {
                new Genre("Humour"),
                new Genre("Action"),
                new Genre("Drame"),
                new Genre("Amour"),
             };



            ListViewMenu.ItemsSource = listGenres;


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
        }

        private void SuppGenreButton_Clicked(object sender, RoutedEventArgs e)
        {
            boxAddGenre.Visibility = Visibility.Collapsed;
            boxSuppGenre.Visibility = Visibility.Visible;
        }
    }
}
