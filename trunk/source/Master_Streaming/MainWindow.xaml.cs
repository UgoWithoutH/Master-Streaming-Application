using Class;
using Swordfish.NET.Collections.Auxiliary;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Master_Streaming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ProfilManager manager => (App.Current as App).Mmanager.ProfilCourant;

        /// <summary>
        /// Permet de savoir quel mode de couleur est en cours
        /// </summary>
        public int ColorMode; //0 = sombre et 1 = clair

        public MainWindow()
        {
            InitializeComponent();
            VisibilyCollapsedHeader();
        }

        /// <summary>
        /// permet de masquer des éléments du header de notre application
        /// </summary>
        public void VisibilyCollapsedHeader()
        {
            header.watchlist.Visibility = Visibility.Collapsed;
            header.recherche.Visibility = Visibility.Collapsed;
            header.ajout.Visibility = Visibility.Collapsed;
            header.deconnexion.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Permet de rendre visible des éléments du header de notre application
        /// </summary>
        public void VisibilyVisibledHeader()
        {
            header.watchlist.Visibility = Visibility.Visible;
            header.recherche.Visibility = Visibility.Visible;
            header.ajout.Visibility = Visibility.Visible;
            header.deconnexion.Visibility = Visibility.Visible;
        }

        //public (Brush,Brush)CheckColorMode()
        //{
        //    Brush brush1, brush2;
        //    if ((contentControlMain.Content as UC_Master).header.ColorMode.IsChecked == false)
        //    {
        //        brush1 = (Brush)new BrushConverter().ConvertFrom("#313131");
        //        brush2 = (Brush)new BrushConverter().ConvertFrom("#232323");
        //        return (brush1,brush2);
        //    }
        //    else
        //    {
        //        brush1 = Brushes.White;
        //        brush2 = (Brush)new BrushConverter().ConvertFrom("#D1D1D1");
        //        return (brush1, brush2);
        //    }
        //}
    }
}
