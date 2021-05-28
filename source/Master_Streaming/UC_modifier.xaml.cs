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
        ProfilManager manager => (Application.Current as App).Mmanager.ProfilCourant;

        Oeuvre OeuvreSélectionnéeBackup;
        public UC_modifier()
        {
            InitializeComponent();
            DataContext = manager.OeuvreSélectionnée;
            OeuvreSélectionnéeBackup = manager.OeuvreSélectionnée.Clone() as Oeuvre;
            //int index = manager.ListFiltrée.IndexOf(manager.OeuvreSélectionnée);
            manager.SupprimerOeuvre(manager.OeuvreSélectionnée); //provoque le bug
            //manager.OeuvreSélectionnée = manager.ListFiltrée[index + 1];
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
            }
        }

        private void btn_annuler_Click(object sender, RoutedEventArgs e)
        {
            manager.OeuvreSélectionnée = OeuvreSélectionnéeBackup;
            manager.AjouterOeuvre(manager.OeuvreSélectionnée); // provoque le bug aussi
            (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
        }

        private void btn_valider_click(object sender, RoutedEventArgs e)
        {
            int result = manager.AjouterOeuvre(manager.OeuvreSélectionnée); // provoque le bug aussi
            if (result == 0)
            {
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
