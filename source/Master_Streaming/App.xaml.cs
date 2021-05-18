using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Class;

namespace Master_Streaming
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MainManager Mmanager { get; private set; } = new MainManager(); //existe pas non plus

        public ProfilManager Pmanager { get; set; } = new ProfilManager("existe pas mais j'en ai besoin au lancement");

        public App()
        {
            Mmanager.AjouteProfil("Maël");
        }
    }
}
