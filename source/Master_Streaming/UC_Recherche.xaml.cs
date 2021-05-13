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
    /// Logique d'interaction pour UC_Recherche.xaml
    /// </summary>
    public partial class UC_Recherche : UserControl
    {
        public ProfilManager manager => (App.Current as App).Pmanager;

        public UC_Recherche(string recherche)
        {
            InitializeComponent();
            DataContext = manager;
            textRecherche.Text = "Recherche pour : " + recherche;
        }

        private void MylisteRecherche_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            manager.OeuvreSélectionnée = (sender as ListBox).SelectedItem as Oeuvre; // SelectedItem="{Binding OeuvreSélectionée}" ne marche pas car une autre listBox possède ce binding, ça n'actualise pas OeuvreSélectionée
            ContentControlDetailRecherche.Visibility = Visibility.Visible;
            ContentControlDetailRecherche.Content = new UC_Detail();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
        }
    }
}
