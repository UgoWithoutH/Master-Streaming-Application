using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Class;

namespace Stub
{
    public class Stub : IPersistanceManager
    {
        public ObservableCollection<ProfilManager> ChargeDonnées()
        {
            ObservableCollection<ProfilManager> collecPManager = new ObservableCollection<ProfilManager>();
            collecPManager.Add(new ProfilManager("Maël"));
            collecPManager.Add(new ProfilManager("AutreProfil"));
            collecPManager.Add(new ProfilManager("Truc"));
            collecPManager[0].AjouterGenre(new Genre("Humour"));
            collecPManager[0].AjouterGenre(new Genre("Romance"));
            collecPManager[0].AjouterGenre(new Genre("Aventure"));
            collecPManager[0].AjouterGenre(new Genre("Action"));
            collecPManager[0].AjouterOeuvre(new Serie("Des vies froissees", new DateTime(2019, 10, 1), "Série mêlant Drame et Amour", null,"/images/Drame/Des vies froissees.jpg", 3, new List<Auteur>(), new HashSet<Genre>() { new Genre("Humour"), new Genre("Romance") }));
            collecPManager[0].AjouterOeuvre(new Serie("Enola Holmes", DateTime.Now, "Série mêlant Drame et Action", null, "/images/Drame/Enola Holmes.jpg", 3, new List<Auteur>(), new HashSet<Genre>() { new Genre("Aventure") }));
            collecPManager[0].AjouterOeuvre(new Serie("La mission", new DateTime(2000, 02, 20), "Pas vraiment une série", null, "/images/Drame/La mission.jpg", 0, new List<Auteur>(), new HashSet<Genre>() { new Genre("Action") }));
            collecPManager[0].AjouterOeuvre(new Serie("Notre été", new DateTime(2010, 02, 20), "Pas vraiment une série", 4, "/images/Drame/Notre ete.jpg", 0, new List<Auteur>(), new HashSet<Genre>() { new Genre("Action") }));
            collecPManager[0].AjouterOeuvre(new Serie("Notre hiver", new DateTime(2010, 02, 20), "Pas vraiment une série", 4, "/images/Drame/Notre ete.jpg", 0, new List<Auteur>(), new HashSet<Genre>() { new Genre("Action") }));
            collecPManager[0].AjouterOeuvre(new Serie("Notre automn", new DateTime(2010, 02, 20), "Pas vraiment une série", 4, "/images/Drame/Notre ete.jpg", 0, new List<Auteur>(), new HashSet<Genre>() { new Genre("Action") }));
            collecPManager[0].AjouterOeuvre(new Serie("Notre printemps", new DateTime(2010, 02, 20), "Pas vraiment une série", 4, "/images/Drame/Notre ete.jpg", 0, new List<Auteur>(), new HashSet<Genre>() { new Genre("Action") }));
            return collecPManager;
        }

        public void SauvegardeDonnées(ObservableCollection<ProfilManager> liste_profils)
        {
            Debug.WriteLine("Sauvegarde demandée");
        }
    }
}
