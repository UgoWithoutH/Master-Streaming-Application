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

        /// <summary>
        /// représente l'ensemble des tries appliquable dans le profils utilisateur
        /// </summary>
        [DataMember]
        public ObservableCollection<string> ListingTris { get; private set; }

        /// <summary>
        /// Collection qui récuperera 
        /// </summary>
        public ObservableCollection<Oeuvre> ListRecherche { get; set; }

        /// <summary>
        /// Série temporaire permettant l'ajout d'une oeuvre au compte utilsisateur
        /// </summary>
        [DataMember]
        public Serie SerieTemporaireAjout { get; set; }

        /// <summary>
        /// Nom du profil utilisateur
        /// </summary>
        [DataMember]
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
            SerieTemporaireAjout = new Serie();
        }

        /// <summary>
        /// Ajouter un genre dans le ObservableSortedDictionary des Oeuvres (ListOeuvres) et des Dates (ListingDates)
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
        /// Supprimer un Genre dans le ObservableSortedDictionary des Oeuvres (ListOeuvres) et des Dates (ListingDates)
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
        /// Ajouter uene oeuvre dans le ObservableSortedDictionary des Oeuvres (ListOeuvres) et la date de sortie dans (ListingDates)
        /// </summary>
        /// <param name="o">Oeuvre qui doit être ajoutée au SortedDictionary des Oeuvres (ListOeuvres)</param>
        /// <returns>-1 si l'oeuvre a un titre null ou s'il manque un champ obligatoire
        ///           0 si l'oeuvre a été ajoutée
        ///           1 si une oeuvre déjà existante possède le même titre que l'oeuvre voulant être ajoutée</returns>
        public int AjouterOeuvre(Oeuvre oeuvre)
        {

            if (oeuvre is Serie serie && checkAjoutOeuvre(oeuvre))
            {
                if (string.IsNullOrWhiteSpace(oeuvre.Titre)) return -1;

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

        /// <summary>
        /// Supprimer une oeuvre du ObservableSortedDictionary des Oeuvres (ListOeuvres) pour chaques genres(Key) que l'oeuvre possède
        /// </summary>
        /// <param name="oeuvre">Oeuvre voulant être supprimée</param>
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
                }
            }
            ListFiltrée = ListOeuvresParGenre;
        }


        /// <summary>
        /// Permet d'ajouter une oeuvre à la watchlist dans le cas de la modification d'une Oeuvre puis de l'ajout
        /// </summary>
        /// <param name="oeuvreBack">Sauvegarde de l'oeuvre qui a été supprimé lors de la modification</param>
        /// <param name="OeuvreModify">Oeuvre qui a été modifié</param>
        public void OeuvreWasInWatchlist(Oeuvre oeuvreBack, Oeuvre OeuvreModify)
        {
            try // pour empêcher cette exception : System.InvalidOperationException : 'Collection was modified; enumeration operation may not execute.'
            {
                foreach (OeuvreWatch oeuvreWatch in MyWatchlist.OeuvresVisionnees)
                {
                    if (oeuvreWatch.Oeuvre.Equals(oeuvreBack))
                    {
                        MyWatchlist.SupprimerOeuvre(oeuvreBack);
                        MyWatchlist.AjouterOeuvre(OeuvreModify);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// Permet de vérifier si l'ajout de l'oeuvre peut être effectuer, si les champs obligatoires sont remplies
        /// </summary>
        /// <param name="oeuvre"></param>
        /// <returns>false en cas d'invalidité et true si l'ajout est valide</returns>
        public bool checkAjoutOeuvre(Oeuvre oeuvre)
        {
            if (string.IsNullOrWhiteSpace(oeuvre.Titre)) return false;
            if (oeuvre.DateSortie.Equals(new DateTime(01,01,0001))) return false;
            if (string.IsNullOrWhiteSpace(oeuvre.Description)) return false;
            if (string.IsNullOrWhiteSpace(oeuvre.ImageName)) return false;
            if (oeuvre.TagsGenres.Count == 0) return false;

            return true;
        }

        /// <summary>
        /// Vérifie si pour un genre donné il existe encore une oeuvre qui possède la même année de sortie que celle passée en paramètre.
        /// </summary>
        /// <param name="genre">genre pour lequel le listing (value) d'oeuvres doit êre vérifiée</param>
        /// <param name="year">année qui doit être vérifié dans le filtrage</param>
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

        /// <summary>
        /// Permet de filtrée ListeFiltrée en fonction du filtre passé en paramètre
        /// </summary>
        /// <param name="filtre">chaine de caractère choisit par l'utiisateur pour filtrer ListeFiltrée</param>
        /// <returns>false si le filtre passé en paramètre est null
        ///          true si le filtrage à eu lieu</returns>
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
                    ListFiltrée = new ObservableCollection<Oeuvre>(); //Dans notre cas on ne peut pas utiliser clear() car ListFiltrée et ListOeuvresParGenre pointent sur la même zone mémoire dans le tas
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

        /// <summary>
        /// Permet de trier ListeFiltrée en fonction du trie passé en paramètre
        /// </summary>
        /// <param name="tri">chaine de caractère choisit par l'utiisateur pour filtrer ListeFiltrée</param>
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

        /// <summary>
        /// Permet de changer le genre sélectionné dans le cas ou l'utilisateur supprime le genre qu'il a sélectionné au préalable (pour éviter des excpetions liées à la mémoire)
        /// </summary>
        /// <param name="données">données du compte utilisateur (Genres/listing d'oeuvres)</param>
        /// <param name="Textgenre">nom du genre supprimé</param>
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

        /// <summary>
        /// Recherche les oeuvres commençant par la chaine de caractère passée en paramètres. Cette méthode délègue la responsabilité de la recherche à la classe utilitaire URecherche
        /// </summary>
        /// <param name="chaine">chaine de caractère cherchant qui doit être comparé avec le début du titre des Oeuvres du profil utilisateur</param>
        /// <returns>retourne ObservableCollection étant le résultat de la recherche</returns>
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
            return Nom.GetHashCode();
        }
    }
}
