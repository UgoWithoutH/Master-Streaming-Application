using Swordfish.NET.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;

namespace Class
{
    public class ProfilManager : ObservableObject
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
            }
        }

        public Oeuvre OeuvreSélectionnée { get; private set; }

        public Genre GenreSélectionné { get; set; }

        public LinkedList<Serie> ListingSerie { get; private set; }

        public SortedDictionary<Genre,SortedSet<int>> ListingDates { get; private set; }

        public ProfilManager()
        {
            ListOeuvres = new ConcurrentObservableSortedDictionary<Genre, ObservableCollection<Oeuvre>>();
            ListingDates = new SortedDictionary<Genre, SortedSet<int>>();
            AjouterGenre(new Genre("Humour"));
            AjouterGenre( new Genre("Romance"));
            AjouterGenre( new Genre("Sci-fi"));
            AjouterGenre( new Genre("Genretest"));
            ListingSerie = new LinkedList<Serie>();
            ListingDates = new SortedDictionary<Genre,SortedSet<int>>();
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
                ListingDates.Add(genre, new SortedSet<int>());
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
                        ListingDates[genre].Add(oeuvre.DateSortie.Year);
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
                    CheckListDates(genre,oeuvre.DateSortie.Year);
                    res = true;
                }
            }

            return res;
        }

        public void CheckListDates(Genre genre, int year)
        {
            int check = 0;

            ListOeuvres.TryGetValue(genre, out ObservableCollection <Oeuvre> value);
            foreach(Oeuvre oeuvre in value)
            {
                if(oeuvre.DateSortie.Year == year)
                {
                    check = 1;
                }
            }

            if (check == 0)
            {
                ListingDates[genre].Remove(year);
            }
        }

        public void TrierOrdreAlph(Genre genre)
        {
            IEnumerable<Oeuvre> res = ListOeuvres[genre].OrderBy(oeuvre => oeuvre.Titre);
            ListOeuvres[genre] = new ObservableCollection<Oeuvre>();
            foreach (Oeuvre oeuvre in res)
            {
                ListOeuvres[genre].Add(oeuvre);
            }
        }

        public void TrierNotes(Genre genre)
        {
            IEnumerable<Oeuvre> res = ListOeuvres[genre].OrderBy(oeuvre => oeuvre.Note).ThenBy(oeuvre => oeuvre.Titre);
            ListOeuvres[genre] = new ObservableCollection<Oeuvre>();
            foreach (Oeuvre oeuvre in res)
            {
                ListOeuvres[genre].Add(oeuvre);
            }
        }
    }
}
