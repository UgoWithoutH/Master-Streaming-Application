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
            Pmanager.OeuvreSélectionnée = new Serie("Des vies froissees", new DateTime(2019, 10, 1), "Série mêlant Drame et Amour", null, "/images/Drame/Des vies froissees.jpg", 3, new HashSet<Genre>() { new Genre("Humour"), new Genre("Romance") });
        }
    }
}
