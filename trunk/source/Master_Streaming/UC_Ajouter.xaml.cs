using Class;
using Swordfish.NET.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour UC_Ajouter.xaml
    /// </summary>
    public partial class UC_Ajouter : UserControl
    {
        ProfilManager PManager = (Application.Current as App).Pmanager;


        public UC_Ajouter()
        {
            InitializeComponent();
        }

        private void Button_Annul_Click(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
        }

        private void Button_Valid_Click(object sender, RoutedEventArgs e)
        {
            List<Auteur> Auteurs = new List<Auteur>();
            HashSet<Genre> Genres = new HashSet<Genre>();

            DateTime ReleaseDate = new DateTime(int.Parse(Release_date.Text.Substring(6, 4)), int.Parse(Release_date.Text.Substring(3, 2)), int.Parse(Release_date.Text.Substring(0, 2)));

            //beaucoup de simplifications possibles ici, je sais pas comment les faire pour l'instant (tous les métiers sont réalisateur pour l'instant)
            //
            if (!string.IsNullOrEmpty(Author_first_name_1.Text) && !string.IsNullOrEmpty(Author_last_name_1.Text) && !string.IsNullOrEmpty(Author_work_1.Text))
                Auteurs.Add(new Auteur(Author_first_name_1.Text, Author_first_name_1.Text, Métier.Réalisateur));
            if (!string.IsNullOrEmpty(Author_first_name_2.Text) && !string.IsNullOrEmpty(Author_last_name_2.Text) && !string.IsNullOrEmpty(Author_work_2.Text))
                Auteurs.Add(new Auteur(Author_first_name_2.Text, Author_first_name_2.Text, Métier.Réalisateur));
            if (!string.IsNullOrEmpty(Author_first_name_3.Text) && !string.IsNullOrEmpty(Author_last_name_3.Text) && !string.IsNullOrEmpty(Author_work_3.Text))
                Auteurs.Add(new Auteur(Author_first_name_3.Text, Author_first_name_3.Text, Métier.Réalisateur));
            if (!string.IsNullOrEmpty(Author_first_name_4.Text) && !string.IsNullOrEmpty(Author_last_name_4.Text) && !string.IsNullOrEmpty(Author_work_4.Text))
                Auteurs.Add(new Auteur(Author_first_name_4.Text, Author_first_name_4.Text, Métier.Réalisateur));
            if (!string.IsNullOrEmpty(Author_first_name_5.Text) && !string.IsNullOrEmpty(Author_last_name_5.Text) && !string.IsNullOrEmpty(Author_work_5.Text))
                Auteurs.Add(new Auteur(Author_first_name_5.Text, Author_first_name_5.Text, Métier.Réalisateur));

            
            if (!string.IsNullOrWhiteSpace(Genre_1.Text))
                Genres.Add(new Genre(Genre_1.Text));
            if (!string.IsNullOrWhiteSpace(Genre_2.Text))
                Genres.Add(new Genre(Genre_2.Text));
            if (!string.IsNullOrWhiteSpace(Genre_3.Text))
                Genres.Add(new Genre(Genre_3.Text));


            if (PManager.ListOeuvres.ContainsKey(new Genre(Genre_1.Text)) && !string.IsNullOrWhiteSpace(Genre_1.Text))
            {
                PManager.ListOeuvres[new Genre(Genre_1.Text)].Add(new Serie(Title.Text, ReleaseDate, Desc_text.Text, BasicRatingBar.Value, "///", int.Parse(Number_seasons.Text), Auteurs, Genres));
                PManager.ListingDates[new Genre(Genre_1.Text)].Add(ReleaseDate.Year.ToString());
            }
            else if (!string.IsNullOrWhiteSpace(Genre_1.Text))
            {
                PManager.ListOeuvres.Add(new Genre(Genre_1.Text), new ObservableCollection<Oeuvre> { new Serie(Title.Text, ReleaseDate, Desc_text.Text, BasicRatingBar.Value, "///", int.Parse(Number_seasons.Text), Auteurs, Genres) });
                PManager.ListingDates.Add(new Genre(Genre_1.Text), new ConcurrentObservableSortedSet<string> { ReleaseDate.Year.ToString() });
            }

            if (PManager.ListOeuvres.ContainsKey(new Genre(Genre_2.Text)) && !string.IsNullOrWhiteSpace(Genre_2.Text))
                PManager.ListOeuvres[new Genre(Genre_2.Text)].Add(new Serie(Title.Text, ReleaseDate, Desc_text.Text, BasicRatingBar.Value, "///", int.Parse(Number_seasons.Text), Auteurs, Genres));
            else if (!string.IsNullOrWhiteSpace(Genre_2.Text))
                PManager.ListOeuvres.Add(new Genre(Genre_2.Text), new ObservableCollection<Oeuvre> { new Serie(Title.Text, ReleaseDate, Desc_text.Text, BasicRatingBar.Value, "///", int.Parse(Number_seasons.Text), Auteurs, Genres) });

            if (PManager.ListOeuvres.ContainsKey(new Genre(Genre_3.Text)) && !string.IsNullOrWhiteSpace(Genre_3.Text))
                PManager.ListOeuvres[new Genre(Genre_3.Text)].Add(new Serie(Title.Text, ReleaseDate, Desc_text.Text, BasicRatingBar.Value, "///", int.Parse(Number_seasons.Text), Auteurs, Genres));
            else if (!string.IsNullOrWhiteSpace(Genre_3.Text))
                PManager.ListOeuvres.Add(new Genre(Genre_3.Text), new ObservableCollection<Oeuvre> { new Serie(Title.Text, ReleaseDate, Desc_text.Text, BasicRatingBar.Value, "///", int.Parse(Number_seasons.Text), Auteurs, Genres) });
            //

            (App.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
        }

        private void Open_File_Explorer(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"C:\Users\Public\Pictures";
            dialog.FileName = "Images";
            dialog.DefaultExt = ".png | .jpg | .gif";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                imageChoix.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));
                imageText.Text = string.Empty;
            }
        }
    }
}
