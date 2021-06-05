using Swordfish.NET.Collections;
using Swordfish.NET.Collections.Auxiliary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace Class
{
    /// <summary>
    /// Manager relatif au Profil de l'utilisateur 
    /// </summary>
    [DataContract]
    public class ProfilManager : ObservableObject
    {
        /// <summary>
        /// Dictionnaire complet, contenant des Genre pour Keys. 
        /// Chaque Value est une ObservableCollection d'Oeuvre dont chaque Oeuvre possède au moins le Genre en Key dans son attribut TagsGenres.
        /// </summary>
        public ConcurrentObservableSortedDictionary<Genre, ObservableCollection<Oeuvre>> ListOeuvres
        {
            get { return listOeuvres; }
            private set
            {
                listOeuvres = value;
            }
        }

        [DataMember]
        private ConcurrentObservableSortedDictionary<Genre, ObservableCollection<Oeuvre>> listOeuvres;

        /// <summary>
        /// Oeuvre sélectionnée par un double-click dans le Master, et dont le Detail est actuellement ouvert
        /// </summary>
        public Oeuvre OeuvreSélectionnée 
        {
            get => oeuvreSélectionnée;
            set
            {
                oeuvreSélectionnée = value;
                OnPropertyChanged();
            }
        }

        private Oeuvre oeuvreSélectionnée;


        /// <summary>
        /// OeuvreWatch sélectionnée lors de la suppression d'une des OeuvreWatch depuis la Watchlist
        /// </summary>
        public OeuvreWatch OeuvreWatchSélectionnée { get; set; }

        
        
        /// <summary>
        /// Genre selectionné par un click dans le menu déroulant des genres.
        /// Si aucun Genre n'est sélectionné par l'utilisateur, le Genre sélectionné par défaut est Action.
        /// </summary>
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
		[DataMember]
		private Genre genreSélectionné;


        /// <summary>
        /// Watchlist personnelle du profil connecté 
        /// </summary>
        [DataMember]
        public Watchlist MyWatchlist { get; private set; }

        /// <summary>
        /// retourne l'ObservableCollection d'Oeuvre dans ListOeuvres dont la Key est le genre sélectionné actuellement.
        /// </summary>
        public ObservableCollection<Oeuvre> ListOeuvresParGenre
        {
            get
            {
                return ListOeuvres[GenreSélectionné];
            }
        }

        /// <summary>
        /// l'ObservableCollection d'Oeuvre correspondant au filtrage et au tri actuels
        /// </summary>
        public ObservableCollection<Oeuvre> ListFiltrée 
        {
            get => listFiltrée;
            private set
            {
                listFiltrée = value;
                OnPropertyChanged();
            }
        }
        [DataMember]
        private ObservableCollection<Oeuvre> listFiltrée;

        /// <summary>
        /// List contenant chacune des Serie présentes dans le dictionnaire ListOeuvre, en exemplaire unique 
        /// </summary>
        [DataMember]
        public LinkedList<Serie> ListingSerie { get; private set; }

        /// <summary>
        /// SortedSet comprenant chacune des différentes dates des Oeuvres présentes dans chaque genre (pour le filtrage par date)
        /// </summary>
        public ConcurrentObservableSortedDictionary<Genre,ConcurrentObservableSortedSet<string>> ListingDates
        {
            get { return listingDates; }
            private set
            {
                listingDates = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ListingDatesParGenre));
            }
        }

        [DataMember]
        private ConcurrentObservableSortedDictionary<Genre, ConcurrentObservableSortedSet<string>> listingDates;

        /// <summary>
        /// retourne le ObservableCollection comprenant chacune des différentes dates des Oeuvres présentes dans le Genre sélectionné par l'utilisateur (pas très propre mais on a préféré ce concentrer sur autre chose)
        /// </summary>
        public ObservableCollection<string> ListingDatesParGenre
        {
            get 
            { 
                if(GenreSélectionné != null)
                {
                    var liste = new ObservableCollection<string>();
                    liste.AddRange(ListingDates[GenreSélectionné].ToList());
                    return liste;
                }

                return new ObservableCollection<string>();
            }
        }

        [DataMember]
        public ObservableCollection<string> ListingTris { get; private set; }

        public ObservableCollection<Oeuvre> ListRecherche { get; set; }

        [DataMember]
        public Serie SerieTemporaireAjout { get; set; }

        [DataMember]
        public string Nom { get; private set; }

        public Auteur AuteurTemporaireAjout { get; set; }

        public ProfilManager(string nom)
        {
            Nom = nom;
            ListOeuvres = new ConcurrentObservableSortedDictionary<Genre, ObservableCollection<Oeuvre>>();
            ListingSerie = new LinkedList<Serie>();
            ListingDates = new ConcurrentObservableSortedDictionary<Genre, ConcurrentObservableSortedSet<string>>();
            ListFiltrée = new ObservableCollection<Oeuvre>();
            MyWatchlist = new Watchlist();
            ListingTris = new ObservableCollection<string>() { "Alphabétique", "Notes" };
            SerieTemporaireAjout = new Serie();
        }

        /// <summary>
        /// Ajouter un genre dans le SortedDictionary des Oeuvres (ListOeuvres) et des Dates (ListingDates)
        /// </summary>
        /// <param name="g">Genre qui doit être ajouté</param>
        /// <returns>true si l'ajout du genre a réussi sinon false</returns>
        public void AjouterGenre(Genre genre)
        {
            if (genre == null) throw new NullReferenceException("Le genre est null");

            if (!ListOeuvres.ContainsKey(genre))
            {
                ListOeuvres.Add(genre, new ObservableCollection<Oeuvre>());
                ListingDates.Add(genre, new ConcurrentObservableSortedSet<string>() { "Toutes dates" });
            }
            if (ListOeuvres.Count == 1)
            {
                GenreSélectionné = genre;
            }
        }

        /// <summary>
        /// Supprimer un Genre dans le le SortedDictionary des Oeuvres (ListOeuvres) et des Dates (ListingDates)
        /// </summary>
        /// <param name="g">Genre qui doit être supprimé</param>
        /// <returns>true si la suppression du genre a réussi sinon false</returns>
        public void SupprimerGenre(Genre genre)
        {
            if (genre == null) throw new NullReferenceException("Le genre est null");

            if (ListOeuvres.ContainsKey(genre))
            {
                ListOeuvres.Remove(genre);
                ListingDates.Remove(genre);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">Oeuvre qui doit être ajoutée au SortedDictionary des Oeuvres (ListOeuvres)</param>
        /// <returns></returns>
        public int AjouterOeuvre(Oeuvre oeuvre)
        {

            if (oeuvre is Serie serie)
            {
                if (!ListingSerie.Contains(oeuvre))
                {
                    ListingSerie.AddFirst(serie);
                }
                else return 1;
            }

            if (oeuvre == null) throw new NullReferenceException("L'oeuvre est null");

            if (checkAjoutOeuvre(oeuvre))
            {

                foreach (Genre genre in oeuvre.TagsGenres)
                {
                    if (ListOeuvres.ContainsKey(genre))
                    {
                        ListOeuvres.TryGetValue(genre, out ObservableCollection<Oeuvre> value);

                        if (!value.Contains(oeuvre))
                        {
                            ListOeuvres[genre].Add(oeuvre);
                            if (!ListingDates[genre].Contains(oeuvre.DateSortie.Year.ToString()))
                            {
                                ListingDates[genre].Add(oeuvre.DateSortie.Year.ToString());
                                OnPropertyChanged(nameof(ListingDates));
                                OnPropertyChanged(nameof(ListingDatesParGenre));
                            }
                            ListFiltrée = ListOeuvresParGenre;
                        }
                    }
                }
                return 0;
            }
            else return -1;
        }


        public void SupprimerOeuvre(Oeuvre oeuvre)
        {
            if (oeuvre is Serie serie) ListingSerie.Remove(serie);

            if (oeuvre == null) throw new NullReferenceException("L'oeuvre est null");

            foreach (Genre genre in oeuvre.TagsGenres)
            {
                ListOeuvres.TryGetValue(genre, out ObservableCollection<Oeuvre> value);
                if (value.Contains(oeuvre))
                {
                    value.Remove(oeuvre);
                    CheckListDates(genre, oeuvre.DateSortie.Year.ToString());
                    OeuvreWatch OeuvreWatchSuppression = new OeuvreWatch(DateTime.Now, oeuvre);
                    if (MyWatchlist.OeuvresVisionnees.Contains(OeuvreWatchSuppression))
                    {
                        MyWatchlist.OeuvresVisionnees.Remove(OeuvreWatchSuppression);
                    }
                }
            }
            ListFiltrée = ListOeuvresParGenre;
        }

        public bool checkAjoutOeuvre(Oeuvre oeuvre)
        {
            if (string.IsNullOrWhiteSpace(oeuvre.Titre)) return false;
            if (oeuvre.DateSortie == null) return false;
            if (string.IsNullOrWhiteSpace(oeuvre.Description)) return false;
            if (string.IsNullOrWhiteSpace(oeuvre.ImageName)) return false;
            if (oeuvre.TagsGenres.Count == 0) return false;

            return true;
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
                OnPropertyChanged(nameof(ListingDatesParGenre));
            }
        }

        public bool Filtrage(string filtre)
        {
            bool result = true;

            if (filtre != null)
            {
                if (filtre.ToUpper().Equals("TOUTES DATES"))
                {
                    ListFiltrée = ListOeuvresParGenre;
                }
                else
                {
                    int dateFiltre = int.Parse(filtre);
                    ListFiltrée = new ObservableCollection<Oeuvre>(); //Dans notre cas on ne peut pas utiliser clear() car ListFiltrée et ListOeuvresParGenre pointe sur la même zone mémoire dans le tas
                    ListFiltrée.AddRange(ListOeuvresParGenre.Where(oeuvre => oeuvre.DateSortie.Year == dateFiltre));
                }
            }
            else
            {
                ListFiltrée = null;
                result = false;
            }
            return result;
        }

        public void tri(string tri)
        {
            if (ListFiltrée != null)
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
                ListFiltrée.AddRange(liste);
            }
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

        public bool Equals(ProfilManager other)
        {
            return Nom.Equals(other.Nom);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != (obj.GetType())) return false;

            return Equals(obj as ProfilManager);
        }

        public override int GetHashCode()
        {
            return 217408413 + EqualityComparer<string>.Default.GetHashCode(Nom);
        }
    }
}
