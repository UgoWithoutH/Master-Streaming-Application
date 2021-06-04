using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Class;
using MaterialDesignThemes.Wpf;

namespace Master_Streaming
{
    /// <summary>
    /// Logique d'interaction pour UC_header.xaml
    /// </summary>
    public partial class UC_header : UserControl
    {
        public MainManager Mmanager => (App.Current as App).Mmanager;

        public ProfilManager Pmanager => (App.Current as App).Mmanager.ProfilCourant;

        private readonly PaletteHelper _paletteHelper = new PaletteHelper();

        public UC_header()
        {
            InitializeComponent();
            ToggleBaseColour(true);
            InitializeColorModeHeader();
            DataContext = Pmanager;
        }

        private void InitializeColorModeHeader()
        {
            if ((App.Current.MainWindow as MainWindow).ColorMode == 0)
            {
                moon.Visibility = Visibility.Visible;
                sun.Visibility = Visibility.Collapsed;
                titre.Foreground = Brushes.White;
                this.HeaderColor.Background = (Brush)new BrushConverter().ConvertFrom("#232323"); //Dark
            }
            else
            {
                moon.Visibility = Visibility.Collapsed;
                sun.Visibility = Visibility.Visible;
                titre.Foreground = (Brush) new BrushConverter().ConvertFrom("#313131");
                this.HeaderColor.Background = (Brush)new BrushConverter().ConvertFrom("#D1D1D1"); //Light
            }

        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Mmanager.SauvegardeDonnées();
            (App.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_ChoixProfil();
            Mmanager.Deconnexion();
        }

        private void Ajouter_Button_Clicked(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Ajouter();
        }

        private void Watchlist_Button_Clicked(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Watchlist();
            ChangeColorUcWatchlist((App.Current.MainWindow as MainWindow).contentControlMain.Content as UC_Watchlist, ColorMode);
        }

        private void Recherche()
        {
            if (!String.IsNullOrWhiteSpace(entree_Recherche.Text))
            {
                (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Recherche(entree_Recherche.Text);
                Pmanager.ListRecherche = Pmanager.Recherche(entree_Recherche.Text);
            }
        }

        private void Enter_Recherche(object sender, RoutedEventArgs e)
        {
            Recherche();
        }

        private void entree_Recherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Recherche();
            }
        }

        private void ToggleBaseColour(bool isDark)
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton MyToggleButton = sender as ToggleButton;
            MainWindow MyMainWindow = App.Current.MainWindow as MainWindow;
            ChangeColorHeader(MyMainWindow, MyToggleButton);
            if (MyMainWindow.contentControlMain.Content is UC_Master ucMaster) ChangeColorUcMaster(ucMaster, MyToggleButton);
            if (MyMainWindow.contentControlMain.Content is UC_Recherche ucRecherche) ChangeColorUcRecherche(ucRecherche, MyToggleButton);
            if (MyToggleButton.IsChecked == false)
            {
                ToggleBaseColour(true);
                (App.Current.MainWindow as MainWindow).ColorMode = 0;
            }
            else
            {
                ToggleBaseColour(false);
                (App.Current.MainWindow as MainWindow).ColorMode = 1;
            }
        }

        private void ChangeColorUcWatchlist(UC_Watchlist ucWatchlist, ToggleButton toggleButton)
        {
            if (toggleButton.IsChecked == false)
            {
                ucWatchlist.Titre.Foreground = Brushes.White;
                ucWatchlist.barre.Fill = Brushes.White;
            }
            else
            {
                ucWatchlist.Titre.Foreground = (Brush)new BrushConverter().ConvertFrom("#313131");
                ucWatchlist.barre.Fill = (Brush)new BrushConverter().ConvertFrom("#313131");
            }

        }

        private void ChangeColorUcRecherche(UC_Recherche ucRecherche, ToggleButton toggleButton)
        {
            if (toggleButton.IsChecked == false)
            {
                ucRecherche.textRecherche.Foreground = Brushes.White;
            }
            else
            {
                ucRecherche.textRecherche.Foreground = (Brush)new BrushConverter().ConvertFrom("#313131");
            }

        }

        private void ChangeColorUcMaster(UC_Master ucMaster,ToggleButton toggleButton)
        {
            if(toggleButton.IsChecked == false)
            {
                ucMaster.uc_listSeries.texteGenre.Foreground = Brushes.White;
            }
            else
            {
                ucMaster.uc_listSeries.texteGenre.Foreground = (Brush) new BrushConverter().ConvertFrom("#313131");
            }

        }

        private void ChangeColorHeader(MainWindow mainWindow, ToggleButton toggleButton)
        {
            if(toggleButton.IsChecked == false)
            {
                this.HeaderColor.Background = (Brush)new BrushConverter().ConvertFrom("#232323");
                moon.Visibility = Visibility.Visible;
                sun.Visibility = Visibility.Collapsed;
                titre.Foreground = Brushes.White;
            }
            else
            {
                this.HeaderColor.Background = (Brush)new BrushConverter().ConvertFrom("#D1D1D1");
                moon.Visibility = Visibility.Collapsed;
                sun.Visibility = Visibility.Visible;
                titre.Foreground = Brushes.Black;
            }
        }
    }
}
