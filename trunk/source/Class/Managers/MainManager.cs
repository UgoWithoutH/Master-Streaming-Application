using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Class
{
    public class MainManager
    {
        public IPersistanceManager Persistance { get; set; }

        public ProfilManager ProfilCourant { get; set; }

        public ObservableCollection<ProfilManager> ListProfils { get; private set; }

        public MainManager(IPersistanceManager persistance)
        {
            Persistance = persistance;
            ListProfils = new ObservableCollection<ProfilManager>();
        }

        public void ChargeDonnées()
        {
            var data = Persistance.ChargeDonnées();
            foreach (ProfilManager PManager in data)
            {
                ListProfils.Add(PManager);
            }
            ProfilCourant = ListProfils.Count > 0 ? ListProfils[0] : null;
        }

        public void SauvegardeDonnées()
        {
            Persistance.SauvegardeDonnées(ListProfils);
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

        public void Connexion(string nom)
        {
            if (ListProfils.Contains(new ProfilManager(nom)))
            {
                 ProfilCourant = ListProfils[ListProfils.IndexOf(new ProfilManager(nom))];
            }
        }

        public void Deconnexion()
        {
            ProfilCourant = null;
        }
    }
}
