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
        ProfilManager PManager => (Application.Current as App).Pmanager;
        public UC_Detail()
        {
            InitializeComponent();
            DataContext = PManager.OeuvreSélectionnée;
        }

        private void Button_Click_chevron(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
