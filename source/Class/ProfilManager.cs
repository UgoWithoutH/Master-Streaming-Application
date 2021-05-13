using Swordfish.NET.Collections;
using Swordfish.NET.Collections.Auxiliary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;

namespace Class
{
    public class ProfilManager : ObservableObject, IComparable
    {
        public ObservableCollection<Genre> ListGenres { get; private set; }

        private ConcurrentObservableSortedDictionary<Genre, ObservableCollection<Oeuvre>> listOeuvres;

        public ConcurrentObservableSortedDictionary<Genre, ObservableCollection<Oeuvre>> ListOeuvres
        {
            get { return listOeuvres; }
            private set
            {
                listOeuvres = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ListOeuvresParGenre));
            }
        }


        public Oeuvre OeuvreSélectionnée { get; set; }


        private Genre genreSélectionné;

        public Genre GenreSélectionné
        {
            get { return genreSélectionné; }
            set
            {
                genreSélectionné = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ListingDatesParGenre));
                OnPropertyChanged(nameof(ListOeuvresParGenre));
            }
        }

        public Watchlist MyWatchlist { get; set; }

        public ObservableCollection<Oeuvre> ListOeuvresParGenre
        {
            get
            {
                if(ListOeuvres.Keys.Count == 1)
                {
                    return null;
                }
                else return ListOeuvres[GenreSélectionné];
            }
        }

        public ObservableCollection<Oeuvre> ListFiltrée { get; private set; }

        public LinkedList<Serie> ListingSerie { get; private set; }

        public ConcurrentObservableSortedDictionary<Genre,ConcurrentObservableSortedSet<string>> ListingDates
        {
            get { return listingDates; }
            set
            {
                listingDates = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ListingDatesParGenre));
            }
        }

        private ConcurrentObservableSortedDictionary<Genre, ConcurrentObservableSortedSet<string>> listingDates;

        public ConcurrentObservableSortedSet<string> ListingDatesParGenre
        {
            get 
            { 
                if(GenreSélectionné != null)
                {
                    return ListingDates[GenreSélectionné];
                }

                return new ConcurrentObservableSortedSet<string>();
            }
        }

        public ObservableCollection<string> ListingTris { get; private set; }

        public ObservableCollection<Oeuvre> ListRecherche { get; set; }

        public string Nom { get; private set; }

        public ProfilManager(string nom)
        {
            Nom = nom;
            ListOeuvres = new ConcurrentObservableSortedDictionary<Genre, ObservableCollection<Oeuvre>>();
            ListingSerie = new LinkedList<Serie>();
            ListingDates = new ConcurrentObservableSortedDictionary<Genre, ConcurrentObservableSortedSet<string>>();
            ListFiltrée = new ObservableCollection<Oeuvre>();
            MyWatchlist = new Watchlist();
            ListingTris = new ObservableCollection<string>() { "Alphabétique", "Notes" };

            /// temporaire, pour tester le Binding sur la Watchlist
            MyWatchlist.AjouterOeuvre(new Serie("Elite", DateTime.Now, "C'est cool", 4, "/images/Drame/Enola Holmes.jpg", 52, null, new HashSet<Genre>() { new Genre("Drame") }));
            MyWatchlist.AjouterOeuvre(new Serie("Harry", new DateTime(1999, 01, 15), "C'est cool", null, "/images/Drame/Notre ete.jpg", 52, new HashSet<Genre>() { new Genre("Action"), new Genre("Drame")}));
        }

        public void chargeDonnées() // temporaire
        {
            AjouterGenre(new Genre("Humour"));
            AjouterGenre(new Genre("Romance"));
            AjouterGenre(new Genre("Aventure"));
            AjouterGenre(new Genre("Action"));
            AjouterOeuvre(new Serie("Des vies froissees", new DateTime(2019, 10, 1), "Série mêlant Drame et Amour", null, "/images/Drame/Des vies froissees.jpg", 3, new HashSet<Genre>() { new Genre("Humour"), new Genre("Romance") }));
            AjouterOeuvre(new Serie("Enola Holmes", DateTime.Now, "Série mêlant Drame et Action", null, "/images/Drame/Enola Holmes.jpg", 3, new HashSet<Genre>() { new Genre("Aventure") }));
            AjouterOeuvre(new Serie("La mission", new DateTime(2000, 02, 20), "Pas vraiment une série", null, "/images/Drame/La mission.jpg", 0, new HashSet<Genre>() { new Genre("Action") }));
            AjouterOeuvre(new Serie("Notre été", new DateTime(2010, 02, 20), "Pas vraiment une série", 4, "/images/Drame/Notre ete.jpg", 0, new HashSet<Genre>() { new Genre("Action") }));
            AjouterOeuvre(new Serie("Notre hiver", new DateTime(2010, 02, 20), "Pas vraiment une série", 4, "/images/Drame/Notre ete.jpg", 0, new HashSet<Genre>() { new Genre("Action") }));
            AjouterOeuvre(new Serie("Notre automn", new DateTime(2010, 02, 20), "Pas vraiment une série", 4, "/images/Drame/Notre ete.jpg", 0, new HashSet<Genre>() { new Genre("Action") }));
            AjouterOeuvre(new Serie("Notre printemps", new DateTime(2010, 02, 20), "Pas vraiment une série", 4, "/images/Drame/Notre ete.jpg", 0, new HashSet<Genre>() { new Genre("Action") }));
        }

        /// <summary>
        /// Ajouter un genre dans le SortedDictionary des Oeuvres (ListOeuvres) et des Dates (ListingDates)
        /// </summary>
        /// <param name="g">Genre qui doit être ajouté</param>
        /// <returns>true si l'ajout du genre a réussi sinon false</returns>
        public bool AjouterGenre(Genre genre)
        {
            if (genre == null) return false;

            if (!ListOeuvres.ContainsKey(genre))
            {
                ListOeuvres.Add(genre, new ObservableCollection<Oeuvre>());
                ListingDates.Add(genre, new ConcurrentObservableSortedSet<string>() { "Toutes dates" });
                return true;
            }
            else return false;

        }

        /// <summary>
        /// Supprimer un Genre dans le le SortedDictionary des Oeuvres (ListOeuvres) et des Dates (ListingDates)
        /// </summary>
        /// <param name="g">Genre qui doit être supprimé</param>
        /// <returns>true si la suppression du genre a réussi sinon false</returns>
        public bool SupprimerGenre(Genre genre)
        {
            if (genre == null) return false;

            if (ListOeuvres.ContainsKey(genre))
            {
                ListOeuvres.Remove(genre);
                ListingDates.Remove(genre);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">Oeuvre qui doit être ajoutée au SortedDictionary des Oeuvres (ListOeuvres)</param>
        /// <returns></returns>
        public bool AjouterOeuvre(Oeuvre oeuvre)
        {

            if (oeuvre is Serie) ListingSerie.AddFirst((Serie)oeuvre);

            bool res = false;

            if (oeuvre == null) return false;

            foreach (Genre genre in oeuvre.TagsGenres)
            {
                if (ListOeuvres.ContainsKey(genre))
                {
                    ListOeuvres.TryGetValue(genre, out ObservableCollection<Oeuvre> value);

                    if (!value.Contains(oeuvre))
                    {
                        ListOeuvres[genre].Add(oeuvre);
                        ListingDates[genre].Add(oeuvre.DateSortie.Year.ToString());
                    }
                    res = true;
                }
            }

            return res;
        }

        public bool SupprimerOeuvre(Oeuvre oeuvre)
        {
            if (oeuvre is Serie) ListingSerie.Remove((Serie)oeuvre);

            bool res = false;

            if (oeuvre == null) return false;

            foreach (Genre genre in oeuvre.TagsGenres)
            {
                ListOeuvres.TryGetValue(genre, out ObservableCollection<Oeuvre> value);
                if (value.Contains(oeuvre))
                {
                    value.Remove(oeuvre);
                    CheckListDates(genre,oeuvre.DateSortie.Year.ToString());
                    res = true;
                }
            }

            return res;
        }

        public void CheckListDates(Genre genre, string year)
        {
            int check = 0;

            ListOeuvres.TryGetValue(genre, out ObservableCollection <Oeuvre> value);
            foreach(Oeuvre oeuvre in value)
            {
                if(oeuvre.DateSortie.Year.ToString() == year)
                {
                    check = 1;
                }
            }

            if (check == 0)
            {
                ListingDates[genre].Remove(year);
            }
        }

        //public void TrierOrdreAlph(Genre genre)
        //{
        //    IEnumerable<Oeuvre> res = ListOeuvres[genre].OrderBy(oeuvre => oeuvre.Titre);
        //    ListOeuvres[genre] = new ObservableCollection<Oeuvre>();
        //    foreach (Oeuvre oeuvre in res)
        //    {
        //        ListOeuvres[genre].Add(oeuvre);
        //    }
        //}

        //public void TrierNotes(Genre genre)
        //{
        //    IEnumerable<Oeuvre> res = ListOeuvres[genre].OrderBy(oeuvre => oeuvre.Note).ThenBy(oeuvre => oeuvre.Titre);
        //    ListOeuvres[genre] = new ObservableCollection<Oeuvre>();
        //    foreach (Oeuvre oeuvre in res)
        //    {
        //        ListOeuvres[genre].Add(oeuvre);
        //    }
        //}

        public void Filtrage(string filtre)
        {
            if(filtre.ToUpper().Equals("TOUTES DATES"))
            {
                ListFiltrée = ListOeuvresParGenre;
            }
            else 
            {
                int dateFiltre = int.Parse(filtre);
                ListFiltrée = new ObservableCollection<Oeuvre>(); // faire Clear() et Add() si pas null
                ListFiltrée.AddRange(ListOeuvresParGenre.Where(oeuvre => oeuvre.DateSortie.Year == dateFiltre));
                //foreach (Oeuvre oeuvre in ListOeuvresParGenre.ToList())
                //{
                //    if (oeuvre.DateSortie.Year == dateFiltre)
                //        ListFiltrée.Add(oeuvre);
                //}
            }
            OnPropertyChanged(nameof(ListFiltrée));
        }

        public void tri(string tri)
        {
            IEnumerable<Oeuvre> liste = null;

            if (tri.Equals("Alphabétique"))
            {
                liste = ListFiltrée.OrderBy(oeuvre => oeuvre.Titre);
            }
            else if (tri.Equals("Notes"))
            {
                liste = ListFiltrée.OrderByDescending(oeuvre => oeuvre.Note).ThenBy(oeuvre => oeuvre.Titre); //décroissant
            }
            ListFiltrée = new ObservableCollection<Oeuvre>();
            foreach(Oeuvre o in liste) //Addrange
            {
                ListFiltrée.Add(o);
            }
            OnPropertyChanged(nameof(ListFiltrée));
        }

        public void ChangeGenreSélectionné(ConcurrentObservableSortedDictionary<Genre, ObservableCollection<Oeuvre>> données, string Textgenre)
        {

            if (données.Count == 1)
            {
                GenreSélectionné = null;
            }

            int index = Array.IndexOf(ListOeuvres.Keys.ToArray(), new Genre(Textgenre));

            Genre[] listingGenre = données.Keys.ToArray();

            if (index == 0 && données.Count != 1)
            {
                GenreSélectionné = listingGenre[1];
            }

            else if (index != 0 && index == (ListOeuvres.Count) - 1)
            {
                GenreSélectionné = listingGenre[(ListOeuvres.Count) - 2];
            }

            else if (index != 0)
            {
                GenreSélectionné = listingGenre[index + 1];
            }
        }

        public ObservableCollection<Oeuvre> Recherche(string chaine)
        {
            return ListOeuvres.RechercherOeuvres(chaine);
        }

        int IComparable.CompareTo(object obj)
        {
            if (!(obj is ProfilManager))
            {
                throw new ArgumentException("Argument is not a ProfilManager", "obj");
            }
            ProfilManager otherpm = obj as ProfilManager;
            return this.CompareTo(otherpm);
        }

        public int CompareTo(ProfilManager other)
        {
            return Nom.CompareTo(other.Nom);
        }
    }
}
