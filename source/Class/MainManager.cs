using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Class
{
    public class MainManager
    {
        public ProfilManager ProfilCourant { get; set; }

        public ObservableCollection<ProfilManager> ListProfils { get; private set; }

        public MainManager()
        {
            ProfilCourant = null;
            ListProfils = new ObservableCollection<ProfilManager>();
        }

        public bool AjouteProfil(string nom)
        {
            if (!string.IsNullOrWhiteSpace(nom))
            {
                ListProfils.Add(new ProfilManager(nom));
                return true;
            }
            return false;
        }

        public bool SupprimeProfil(string nom)
        {
            return ListProfils.Remove(new ProfilManager(nom));
        }

        public bool Connexion(string nom)
        {
            if (ListProfils.Contains(new ProfilManager(nom))) ///truc magique jsp si ça marche
            {
                 ProfilCourant = ListProfils[ListProfils.IndexOf(new ProfilManager(nom))];
                 return true;
            }
            return false;
        }

        public void Deconnexion(string nom)
        {
            ProfilCourant = null;
        }
    }
}
