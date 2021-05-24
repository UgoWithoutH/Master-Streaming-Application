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
        public UC_modifier()
        {
            InitializeComponent();
            DataContext = manager.OeuvreSélectionnée;
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

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();

        }
    }
}
