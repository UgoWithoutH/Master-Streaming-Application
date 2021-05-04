using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;

namespace Class
{
    public class ProfilManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyname)
         => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));


        public ObservableCollection<Genre> ListGenres { get; private set; }
        public SortedDictionary<Genre, ObservableCollection<Oeuvre>> ListOeuvres { get; private set; }

        public Oeuvre OeuvreSélectionnée { get; private set; }

        public Genre GenreSélectionné { get; private set; }

        public LinkedList<Serie> ListingSerie { get; private set; }

        public SortedDictionary<Genre,SortedSet<int>> ListingDates { get; private set; }

        public ProfilManager()
        {
            ListOeuvres = new SortedDictionary<Genre, ObservableCollection<Oeuvre>>();
            ListGenres = new ObservableCollection<Genre>() { new Genre("Humour"), new Genre("Romance"), new Genre("Sci-Fi"), new Genre("GenreTest"), };
            ListingSerie = new LinkedList<Serie>();
            ListingDates = new SortedDictionary<Genre,SortedSet<int>>();
        }

        /// <summary>
        /// Ajouter un genre dans le SortedDictionary des Oeuvres (ListOeuvres) et des Dates (ListingDates)
        /// </summary>
        /// <param name="g">Genre qui doit être ajouté</param>
        /// <returns>true si l'ajout du genre a réussi sinon false</returns>
        public bool AjouterGenre(Genre g)
        {
            if (g == null) return false;

            if (!ListOeuvres.ContainsKey(g))
            {
                ListOeuvres.Add(g, null);
                ListingDates.Add(g, null);
                return true;
            }
            else return false;

        }

        /// <summary>
        /// Supprimer un Genre dans le le SortedDictionary des Oeuvres (ListOeuvres) et des Dates (ListingDates)
        /// </summary>
        /// <param name="g">Genre qui doit être supprimé</param>
        /// <returns>true si la suppression du genre a réussi sinon false</returns>
        public bool SupprimerGenre(Genre g)
        {
            if (g == null) return false;

            if (ListOeuvres.ContainsKey(g))
            {
                ListOeuvres.Remove(g);
                ListingDates.Remove(g);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">Oeuvre qui doit être ajoutée au SortedDictionary des Oeuvres (ListOeuvres)</param>
        /// <returns></returns>
        public bool AjouterOeuvre(Oeuvre o)
        {

            if (o is Serie) ListingSerie.AddFirst((Serie)o);

            bool res = false;

            if (o == null) return false;

            foreach (Genre g in o.TagsGenres)
            {
                if (ListOeuvres.ContainsKey(g))
                {
                    ListOeuvres.TryGetValue(g, out ObservableCollection<Oeuvre> value);

                    if (value == null)
                    {
                        ListOeuvres[g] = new ObservableCollection<Oeuvre>();
                        ListOeuvres[g].Add(o);
                        if(ListingDates[g] == null)
                        {
                            ListingDates[g] = new SortedSet<int>();
                        }
                        ListingDates[g].Add(o.DateSortie.Year);
                    }
                    else if (!value.Contains(o))
                    {
                        ListOeuvres[g].Add(o);
                        if (ListingDates[g] == null)
                        {
                            ListingDates[g] = new SortedSet<int>();
                        }
                        ListingDates[g].Add(o.DateSortie.Year);
                    }
                    res = true;
                }
            }

            return res;
        }

        public bool SupprimerOeuvre(Oeuvre o)
        {
            if (o is Serie) ListingSerie.Remove((Serie)o);

            bool res = false;

            if (o == null) return false;

            foreach (Genre g in o.TagsGenres)
            {
                ListOeuvres.TryGetValue(g, out ObservableCollection<Oeuvre> value);
                if (value != null && value.Contains(o))
                {
                    value.Remove(o);
                    CheckListDates(g,o.DateSortie.Year);
                    res = true;
                }
            }

            return res;
        }

        public void CheckListDates(Genre g, int year)
        {
            int check = 0;

            ListOeuvres.TryGetValue(g, out ObservableCollection <Oeuvre> value);
            foreach(Oeuvre o in value)
            {
                if(o.DateSortie.Year == year)
                {
                    check = 1;
                }
            }

            if (check == 0)
            {
                ListingDates[g].Remove(year);
            }
        }

        public void TrierOrdreAlph(Genre g)
        {
            IEnumerable<Oeuvre> res = ListOeuvres[g].OrderBy(o => o.Titre);
            ListOeuvres[g] = new ObservableCollection<Oeuvre>();
            foreach (Oeuvre o in res)
            {
                ListOeuvres[g].Add(o);
            }
        }

        public void TrierNotes(Genre g)
        {
            IEnumerable<Oeuvre> res = ListOeuvres[g].OrderBy(o => o.Note).ThenBy(o => o.Titre);
            ListOeuvres[g] = new ObservableCollection<Oeuvre>();
            foreach (Oeuvre o in res)
            {
                ListOeuvres[g].Add(o);
            }
        }
    }
}
