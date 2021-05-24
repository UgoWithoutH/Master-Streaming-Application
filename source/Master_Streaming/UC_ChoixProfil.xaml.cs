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
        }

        private void Account_Connexion(object sender, RoutedEventArgs e)
        {
            mainManager.Connexion("Maël");
            (Application.Current.MainWindow as MainWindow).contentControlMain.Content = new UC_Master();
        }

        private void Click_newAccount(object sender, RoutedEventArgs e)
        {
            entry_name_new_account.Visibility = Visibility.Visible;
        }

        private void Entry_Name_Return_Key(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                if (entry_name_new_account.Text == "")
                {
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(entry_name_new_account, "Nom invalide");
                    return;
                }
                foreach (ProfilManager pm in mainManager.ListProfils)
                    if (pm.Nom.Equals(entry_name_new_account.Text))
                    {
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(entry_name_new_account, "Nom invalide");
                        entry_name_new_account.Text = "";
                        return;
                    }
                mainManager.ListProfils.Add(new ProfilManager(entry_name_new_account.Text));
                MaterialDesignThemes.Wpf.HintAssist.SetHint(entry_name_new_account, "Nom du profil :");
                entry_name_new_account.Text = "";
            }
        }
    }
}
