using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
   public class Serie : Oeuvre
    {
        public int NbSaisons { get; set; }

        public Serie(string titre, DateTime dateSortie, int? note, string description, string imageName, int nbSaisons, List<Auteur> listeAuteurs, HashSet<Genre> tagsGenres)
            : base(titre, dateSortie, note, description, imageName, listeAuteurs, tagsGenres)
        {
            NbSaisons = NbSaisons;
        }

        public Serie(string titre, DateTime dateSortie, string description, string imageName, int nbSaisons, HashSet<Genre> tagsGenres)
            : base(titre, dateSortie, description, imageName, tagsGenres)
        {
            NbSaisons = NbSaisons;
        }

        public override string ToString()
        {
            return $"{Titre} , {DateSortie} , {Note} , {Description}";
        }
    }
}
