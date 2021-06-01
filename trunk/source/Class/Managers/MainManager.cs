using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Class
{
    public class MainManager
    {
        /// <summary>
        /// Interface pour la persistance, avec 2 méthodes ChargeDonnées et SauvegardeDonnées
        /// </summary>
        public IPersistanceManager Persistance { get; set; }

        /// <summary>
        /// ProfilManager de l'utilisateur connecté
        /// </summary>
        public ProfilManager ProfilCourant { get; set; }

        /// <summary>
        /// ObservableCollection des ProfilManager de tous les utilisateurs existants
        /// </summary>
        public ObservableCollection<ProfilManager> ListProfils { get; private set; }

        public MainManager(IPersistanceManager persistance)
        {
            Persistance = persistance;
            ListProfils = new ObservableCollection<ProfilManager>();
        }

        /// <summary>
        /// Charge les ProfilManager récupérés par la persistance dans ListProfils.
        /// ProfilCourant est temporairement setté au premier ProfilManager de ListProfils (jusqu'à ce qu'un utilisateur se connecte).
        /// Si ListProfils est vide, ProfilCourant est setté à null.
        /// </summary>
        public void ChargeDonnées()
        {
            var data = Persistance.ChargeDonnées();
            foreach (ProfilManager PManager in data)
            {
                ListProfils.Add(PManager);
            }
            ProfilCourant = ListProfils.Count > 0 ? ListProfils[0] : null;
        }

        /// <summary>
        /// Sauvegarde les données de ListProfils
        /// </summary>
        public void SauvegardeDonnées()
        {
            Persistance.SauvegardeDonnées(ListProfils);
        }

        /// <summary>
        /// Ajoute un ProfilManager à ListProfils
        /// </summary>
        /// <param name="nom">attribut Nom du nouveau ProfilManager</param>
        /// <returns>true si "nom" n'est pas null ou une suite d'espaces, false sinon</returns>
        public bool AjouteProfil(string nom)
        {
            if (!string.IsNullOrWhiteSpace(nom))
            {
                ListProfils.Add(new ProfilManager(nom));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Supprime un ProfilManager de ListProfils
        /// </summary>
        /// <param name="nom">attribut Nom du ProfilManager à supprimer</param>
        /// <returns>true si la suppression a bien été effectuée, false sinon</returns>
        public bool SupprimeProfil(string nom)
        {
            return ListProfils.Remove(new ProfilManager(nom));
        }

        /// <summary>
        /// Connecte le ProfilManager dont l'attribut Nom est égal au string "nom" passé en paramètre.
        /// ProfilCourant est setté à ce ProfilManager.
        /// </summary>
        /// <param name="nom">attribut Nom du ProfilManager à connecter</param>
        public void Connexion(string nom)
        {
            if (ListProfils.Contains(new ProfilManager(nom)))
            {
                 ProfilCourant = ListProfils[ListProfils.IndexOf(new ProfilManager(nom))];
            }
        }

        /// <summary>
        /// l'utilisateur se déconnecte, le ProfilCourant est setté à null
        /// </summary>
        public void Deconnexion()
        {
            ProfilCourant = null;
        }
    }
}
