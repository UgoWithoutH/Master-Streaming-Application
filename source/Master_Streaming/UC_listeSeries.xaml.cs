﻿using Class;
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
        public UC_listeSeries()
        {
            InitializeComponent();

            var listSerie = new List<Serie>()
            {
                new Serie("Des vies froissees",new DateTime(2021,03,12),"description_test","/images/Drame/Des vies froissees.jpg",1),
                new Serie("Enola Holmes",new DateTime(2020,07,23),"description_test","/images/Drame/Enola Holmes.jpg",1),
                new Serie("La mission",new DateTime(2020,01,21),"description_test","/images/Drame/La mission.jpg",1),
                new Serie("Notre ete",new DateTime(2021,03,25),"description_test","/images/Drame/Notre ete.jpg",1),
            };

            MylisteSerie.ItemsSource = listSerie;

            filtrage.SelectedIndex = 0; //valeur par défaut de la combobox du filtrage
            trie.SelectedIndex = 0; //valeur par défaut de la combobox du trie
        }
    }
}