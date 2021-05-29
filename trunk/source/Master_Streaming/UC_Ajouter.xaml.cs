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
            PManager.AuteurTemporaireAjout = null;//juste pour les test pcq le Binding reprend le nom de l'auteur de l'ancienne oeuvre ajoutée
            InitializeComponent();
            DataContext = PManager;
            ListEnum.ItemsSource = Enum.GetValues((typeof(Métier))).Cast<Métier>();
        }

        private void Button_Annul_Click(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
        }

        private void Button_Valid_Click(object sender, RoutedEventArgs e)
        {
            PManager.SerieTemporaireAjout.ImageName = filename;
            PManager.SerieTemporaireAjout.Titre = this.Titre.Text;
            if (string.IsNullOrWhiteSpace(this.DateSortie.Text))
            {
                PManager.SerieTemporaireAjout.DateSortie = new DateTime(0);
            }
            else
            {
                PManager.SerieTemporaireAjout.DateSortie = Convert.ToDateTime(this.DateSortie.Text);
            }
            PManager.SerieTemporaireAjout.Description = this.Description.Text;
            if (string.IsNullOrWhiteSpace(this.nbSaisons.Text))
            {
                PManager.SerieTemporaireAjout.NbSaisons = 0;
            }
            else
            {
                PManager.SerieTemporaireAjout.Note = this.BasicRatingBar.Value;
            }
            foreach (Genre genre in this.ListGenre.SelectedItems)
            {
                PManager.SerieTemporaireAjout.TagsGenres.Add(genre);
            }
            int result = PManager.AjouterOeuvre(PManager.SerieTemporaireAjout);
            if (result == 0)
            {
                (App.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
            }
            else if(result == 1)
            {
                MessageBox.Show("Une oeuvre avec le même titre est déjà existante");
            }
            else
            {
                MessageBox.Show("Vous n'avez pas renseigné tous les champs obligatoires");
            }
                
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
                PManager.AuteurTemporaireAjout = new Auteur(nomAuteur.Text, prenomAuteur.Text, métier[index]);
                PManager.SerieTemporaireAjout.ListAuteur.Add(PManager.AuteurTemporaireAjout);
                nomAuteur.Text = string.Empty;
                prenomAuteur.Text = string.Empty;
                //this.ListGenre.SelectedItem = null;
            }
        }
    }
}
