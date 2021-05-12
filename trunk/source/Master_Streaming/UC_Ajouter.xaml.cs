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
    /// Logique d'interaction pour UC_Ajouter.xaml
    /// </summary>
    public partial class UC_Ajouter : UserControl
    {
        ProfilManager PManager = (Application.Current as App).Pmanager;


        public UC_Ajouter()
        {
            InitializeComponent();
        }

        private void Button_Annul_Click(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
        }
    }
}
