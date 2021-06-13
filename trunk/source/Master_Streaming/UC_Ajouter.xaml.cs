using Class;
using Swordfish.NET.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
        ProfilManager PManager = (Application.Current as App).Mmanager.ProfilCourant;


        string filename;
        public UC_Ajouter()
        {
            PManager.SerieTemporaireAjout = new Serie();
            InitializeComponent();
            DataContext = PManager;
            ListEnum.ItemsSource = Enum.GetValues((typeof(Métier))).Cast<Métier>();
            (App.Current.MainWindow as MainWindow).header.Visibility = Visibility.Collapsed;
        }

        private void Button_Annul_Click(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
        }

        private void Button_Valid_Click(object sender, RoutedEventArgs e)
        {
            PManager.SerieTemporaireAjout.ImageName = filename;
            PManager.SerieTemporaireAjout.Titre = this.Titre.Text;
            if (string.IsNullOrWhiteSpace(this.DateSortie.Text) || !DateTime.TryParse(this.DateSortie.Text, out DateTime res))
            {
                MessageBox.Show("Veuillez renseigner une date de sortie valide svp (DD/MM/YYYY)");
                return;
            }
            else
            {
                PManager.SerieTemporaireAjout.DateSortie = res;
            }
            PManager.SerieTemporaireAjout.Description = this.Description.Text;
            if (string.IsNullOrWhiteSpace(this.nbSaisons.Text))
            {
                PManager.SerieTemporaireAjout.NbSaisons = 0;
            }
            else if (!int.TryParse(this.nbSaisons.Text, out int resultat))
            {
                MessageBox.Show("chaine de caractère non valide pour le nombre de saisons");
                return;
            }
            else
            {
                PManager.SerieTemporaireAjout.NbSaisons = resultat;
            }
            PManager.SerieTemporaireAjout.Note = this.BasicRatingBar.Value;
            foreach (Genre genre in this.ListGenre.SelectedItems)
            {
                PManager.SerieTemporaireAjout.TagsGenres.Add(genre);
            }
            int result = PManager.AjouterOeuvre(PManager.SerieTemporaireAjout);
            switch (result)
            {
                case 0:
                    {
                        (App.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
                        break;
                    }

                case 1:
                    {
                        MessageBox.Show("Une oeuvre avec le même titre est déjà existante");
                        break;
                    }

                case 2:
                    {
                        MessageBox.Show("Veuillez renseigner une description svp");
                        break;
                    }

                case 3:
                    {
                        MessageBox.Show("Votre Oeuvre doit appartenir à un ou plusieurs Genres");
                        break;
                    }

                case 4:
                    {
                        MessageBox.Show("Il faut donner une Image à votre Oeuvre ! ça fera plus joli :)");
                        break;
                    }

                default:
                    {
                        MessageBox.Show("Veuillez renseigner un Titre svp");
                        break;
                    }
            }
                
        }

        private void Open_File_Explorer(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"C:\Users\Public\Pictures";
            dialog.FileName = "Images";
            dialog.DefaultExt = ".png | .jpg | .gif";
            dialog.Filter = "All images files (.jpg, .png, .gif)|*.jpg;*.png;*.gif|JPG files (.jpg)|*.jpg|PNG files (.png)|*.png|GIF files (.gif)|*.gif";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                filename = dialog.FileName;
                imageChoix.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));
                imageText.Text = string.Empty;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = ListEnum.SelectedIndex;
            if (index == -1) index = 0;
            Métier[] métier = Enum.GetValues((typeof(Métier))).Cast<Métier>().ToArray();

            if (!(string.IsNullOrWhiteSpace(this.nomAuteur.Text) || string.IsNullOrWhiteSpace(this.prenomAuteur.Text)))
            {
                PManager.SerieTemporaireAjout.ListAuteur.Add(new Auteur(nomAuteur.Text, prenomAuteur.Text, métier[index]));
                nomAuteur.Text = string.Empty;
                prenomAuteur.Text = string.Empty;
            }
        }
    }
}
