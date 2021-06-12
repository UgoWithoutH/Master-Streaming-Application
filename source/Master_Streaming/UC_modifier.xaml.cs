using Class;
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

namespace Master_Streaming
{
    /// <summary>
    /// Logique d'interaction pour UC_modifier.xaml
    /// </summary>
    public partial class UC_modifier : UserControl
    {
        /// <summary>
        /// ProfilManager avec lequel l'utilisateur est connecté
        /// </summary>
        ProfilManager manager => (Application.Current as App).Mmanager.ProfilCourant;

        /// <summary>
        /// En cas d'annulation de la modification, permet de revenir à la liste précédente (OeuvreSélectionnée est supprimée du ProfilManager)
        /// </summary>
        Oeuvre OeuvreSélectionnéeBackup;

        /// <summary>
        /// Sauvegarde de l'oeuvre sélectionnée précédemment dans le Detail
        /// </summary>
        ///
        Oeuvre OeuvreSauvegarde;

        public UC_modifier()
        {
            InitializeComponent();
            OeuvreSauvegarde = manager.OeuvreSélectionnée; // a cause du OnPropertyChanged() qui fait une nouvelle collection dans la vue et après manager.OeuvreSélectionée vaut null
            DataContext = OeuvreSauvegarde;
            OeuvreSélectionnéeBackup = manager.OeuvreSélectionnée.Clone() as Oeuvre;
            manager.SupprimerOeuvre(manager.OeuvreSélectionnée);
            (App.Current.MainWindow as MainWindow).header.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Ouvre l'explorateur de fichiers et laisse l'utilisateur sélectionner une image au format png ou jpg ou pdf
        /// </summary>
        /// <param name="sender">référence sur le contrôle/objet qui a déclenché l'événement</param>
        /// <param name="e">données de l'événement</param>
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
            }
        }

        /// <summary>
        /// Annule la mofification et renvoie au Master
        /// </summary>
        /// <param name="sender">référence sur le contrôle/objet qui a déclenché l'événement</param>
        /// <param name="e">données de l'événement</param>
        private void btn_annuler_Click(object sender, RoutedEventArgs e)
        {
            manager.AjouterOeuvre(OeuvreSélectionnéeBackup);
            (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
        }

        /// <summary>
        /// Ajoute l'Oeuvre mofifiée.
        /// Affiche une MessageBox avec des informations sur l'erreur si la nouvelle Oeuvre ne peut pas être ajoutée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_valider_click(object sender, RoutedEventArgs e)
        {
            int result = manager.AjouterOeuvre(OeuvreSauvegarde);
            if (result == 0)
            {
                manager.OeuvreWasInWatchlist(OeuvreSélectionnéeBackup, OeuvreSauvegarde);
                (App.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
            }
            else if (result == 1)
            {
                MessageBox.Show("Une oeuvre avec le même titre est déjà existante");
            }
            else
            {
                MessageBox.Show("Vous n'avez pas renseigné tous les champs obligatoires");
            }
        }
    }
}
