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
using System.Windows.Shapes;
using Class;

namespace Master_Streaming
{
    /// <summary>
    /// Logique d'interaction pour RechercheWindow.xaml
    /// </summary>
    public partial class RechercheWindow : Window
    {
        public ProfilManager Manager => (App.Current as App).Pmanager;

        public RechercheWindow()
        {
            InitializeComponent();
            DataContext = Manager;
        }

        private void Back_To_Menu_Clicked (object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
