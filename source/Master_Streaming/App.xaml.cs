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
        public ProfilManager Pmanager { get; private set; } = new ProfilManager();

        public App()
        {
            Pmanager.chargeDonnées();
        }
    }
}
