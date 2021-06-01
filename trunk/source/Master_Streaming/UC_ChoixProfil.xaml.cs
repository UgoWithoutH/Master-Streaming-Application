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
using Class;

namespace Master_Streaming
{
    /// <summary>
    /// Logique d'interaction pour UC_ChoixProfil.xaml
    /// </summary>
    public partial class UC_ChoixProfil : UserControl
    {
        public MainManager mainManager => (App.Current as App).Mmanager;

        public UC_ChoixProfil()
        {
            InitializeComponent();
            DataContext = mainManager;
            entry_name_new_account.Visibility = Visibility.Collapsed;
            entry_name_supp_account.Visibility = Visibility.Collapsed;
        }

        private void Account_Connexion(object sender, RoutedEventArgs e)
        {
            mainManager.ProfilCourant = (ProfilManager) ((ListBoxItem)MylisteProfils.ContainerFromElement((Button)sender)).Content;
            mainManager.Connexion(mainManager.ProfilCourant.Nom);
            (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
        }

        private void Click_newAccount(object sender, RoutedEventArgs e)
        {
            entry_name_new_account.Visibility = Visibility.Visible;
            entry_name_supp_account.Visibility = Visibility.Collapsed;
        }

        private void Click_suppAccount(object sender, RoutedEventArgs e)
        {
            entry_name_supp_account.Visibility = Visibility.Visible;
            entry_name_new_account.Visibility = Visibility.Collapsed;
        }

        private void Entry_Name_Add_Return_Key(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                if (string.IsNullOrWhiteSpace(entry_name_new_account.Text))
                {
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(entry_name_new_account, "Nom invalide");
                    return;
                }
                foreach (ProfilManager pm in mainManager.ListProfils)
                    if (pm.Nom.Equals(entry_name_new_account.Text))
                    {
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(entry_name_new_account, "Nom invalide");
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(entry_name_new_account, "Nom du profil :");
                        entry_name_new_account.Text = string.Empty;
                        return;
                    }
                mainManager.ListProfils.Add(new ProfilManager(entry_name_new_account.Text));
                MaterialDesignThemes.Wpf.HintAssist.SetHint(entry_name_new_account, "Nom du profil :");
                entry_name_new_account.Text = string.Empty;
                mainManager.SauvegardeDonnées();
            }
        }

        private void Entry_Name_Supp_Return_Key(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (mainManager.ListProfils.Contains(new ProfilManager(entry_name_supp_account.Text)))
                {
                    mainManager.ListProfils.RemoveAt(mainManager.ListProfils.IndexOf(new ProfilManager(entry_name_supp_account.Text)));
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(entry_name_supp_account, "Nom du profil :");
                    entry_name_supp_account.Text = string.Empty;
                    mainManager.SauvegardeDonnées();
                    return;
                }
                MaterialDesignThemes.Wpf.HintAssist.SetHint(entry_name_supp_account, "Nom invalide");
                entry_name_supp_account.Text = string.Empty;
            }
        }
    }
}
