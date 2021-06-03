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
    /// Logique d'interaction pour UC_Detail.xaml
    /// </summary>
    public partial class UC_Detail : UserControl
    {
        ProfilManager manager => (Application.Current as App).Mmanager.ProfilCourant;
        public UC_Detail()
        {
            InitializeComponent();
            DataContext = manager.OeuvreSélectionnée;
            Text_BtnWatch();
        }

        private void Text_BtnWatch()
        {
            btn_watch.Content = GetText();
        }

        private string GetText()
        {
            return manager.MyWatchlist.OeuvresVisionnees.Contains(new OeuvreWatch(DateTime.Now, manager.OeuvreSélectionnée)) ? "Supprimer de la Watchlist" : "Ajouter à la Watchlist"; 
        }

        private void btn_watch_Click(object sender, RoutedEventArgs e)
        {

            if (manager.MyWatchlist.OeuvresVisionnees.Contains(new OeuvreWatch(DateTime.Now, manager.OeuvreSélectionnée)))
            {
                manager.MyWatchlist.SupprimerOeuvre(manager.OeuvreSélectionnée);
            }
            else
            {
                manager.MyWatchlist.AjouterOeuvre(manager.OeuvreSélectionnée);
            }
            (sender as Button).Content = GetText();
        }

        private void Button_Click_chevron(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void btn_modif_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_modifier();
        }

        private void Btn_supprimer_click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            manager.SupprimerOeuvre(manager.OeuvreSélectionnée);
            manager.tri((((App.Current.MainWindow as MainWindow).contentControlMain.Content as UC_Master).uc_listSeries.trie.SelectedItem.ToString())); //pour relancer le tri car il est écrasé
        }
    }
}
