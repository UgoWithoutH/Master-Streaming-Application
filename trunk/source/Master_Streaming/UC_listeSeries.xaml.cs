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
    /// Logique d'interaction pour UC_listeSeries.xaml
    /// </summary>
    public partial class UC_listeSeries : UserControl
    {
        public ProfilManager manager => (App.Current as App).Pmanager;
        public UC_listeSeries()
        {
            InitializeComponent();

            var listSerie = new List<Serie>()
            {
                new Serie("Des vies froissees",new DateTime(2021,03,12),"description_test", 2, "/images/Drame/Des vies froissees.jpg",1,null /*Hashset<Genre> à ajouter*/),
                new Serie("Enola Holmes",new DateTime(2020,07,23),"description_test", 3, "/images/Drame/Enola Holmes.jpg",1,null /*idem*/),
                new Serie("La mission",new DateTime(2020,01,21),"description_test", 4, "/images/Drame/La mission.jpg",1,null /*idem*/),
                new Serie("Notre ete",new DateTime(2021,03,25),"description_test", 5, "/images/Drame/Notre ete.jpg",1,null /*idem*/),
            };

            DataContext = manager;

            filtrage.SelectedIndex = 0; //valeur par défaut de la combobox du filtrage
            trie.SelectedIndex = 0; //valeur par défaut de la combobox du trie
        }

        private void filtrage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void trie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
