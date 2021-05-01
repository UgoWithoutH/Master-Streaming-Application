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
        public Dictionary<Genre, ObservableCollection<Oeuvre>> DicoGenreEtOeuvres { get; private set; }
        public Dictionary<Genre, SortedSet<Oeuvre>> ListOeuvres { get; private set; }

        public Oeuvre OeuvreSélectionnée { get; private set; }

        public Genre GenreSélectionné { get; private set; }

        public LinkedList<Serie> listing;

        private Watchlist MyWatchlist;

        public ProfilManager()
        {
            DicoGenreEtOeuvres = new Dictionary<Genre, ObservableCollection<Oeuvre>>();
            ListGenres = new ObservableCollection<Genre>() { new Genre("Humour"), new Genre("Romance"), new Genre("Sci-Fi"), new Genre("GenreTest"), };
            DicoGenreEtOeuvres.Add(ListGenres[3],new ObservableCollection<Oeuvre> { new Serie("titreGenreTest", new DateTime(2020, 5, 12), "descriptiontest", 2,"images/drame/Des vies froissees.jpg", 1, new HashSet<Genre> { ListGenres[3], ListGenres[0] })});
            ListOeuvres = new Dictionary<Genre, SortedSet<Oeuvre>>();
            listing = new LinkedList<Serie>();
        }

        public bool AjouterGenre(Genre g)
        {
            if (g == null) return false;

            if (!ListOeuvres.ContainsKey(g))
            {
                ListOeuvres.Add(g, null);
                return true;
            }
            else return false;

        }

        public bool SupprimerGenre(Genre g)
        {
            if (g == null) return false;

            if (ListOeuvres.ContainsKey(g))
            {
                ListOeuvres.Remove(g);
                return true;
            }
            else return false;
        }

        public bool AjouterOeuvre(Oeuvre o)
        {

            if (o is Serie) listing.AddFirst((Serie)o);

            bool res = false;

            if (o == null) return false;

            foreach (Genre g in o.TagsGenres)
            {
                if (ListOeuvres.ContainsKey(g))
                {
                    ListOeuvres.TryGetValue(g, out SortedSet<Oeuvre> value);

                    if (value == null)
                    {
                        ListOeuvres[g] = new SortedSet<Oeuvre>();
                        ListOeuvres[g].Add(o);
                        res = true;
                    }
                    else if (!value.Contains(o))
                    {
                        ListOeuvres[g].Add(o);
                        res = true;
                    }
                }
            }

            return res;
        }

        public bool SupprimerOeuvre(Oeuvre o)
        {
            if (o is Serie) listing.Remove((Serie)o);

            bool res = false;

            if (o == null) return false;

            foreach (Genre g in o.TagsGenres)
            {
                ListOeuvres.TryGetValue(g, out SortedSet<Oeuvre> value);
                if (value != null && value.Contains(o))
                {
                    value.Remove(o);
                    res = true;
                }
            }

            return res;
        }

        public void TrierOrdreAlph(Genre g)
        {
            ListOeuvres[g].OrderBy(o => o.Titre);
        }

        //public void TrierNotes(Genre g)
        //{
        //    ListOeuvres[g].OrderBy(o => o.Note); // marche pas
        //}
    }
}
