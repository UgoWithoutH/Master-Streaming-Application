using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
   public class Serie : Oeuvre
    {
        public int NbSaisons { get; set; }

        public Serie()
            : base()
        {
        }

        public Serie(string titre, DateTime dateSortie, string description, int? note, string imageName, int nbSaisons, List<Auteur> listeAuteurs, HashSet<Genre> tagsGenres)
            : base(titre, dateSortie, description, note,imageName, listeAuteurs, tagsGenres)
        {
            NbSaisons = NbSaisons;
        }

        //public Serie(string titre, DateTime dateSortie, string description, int? note,string imageName, int nbSaisons, HashSet<Genre> tagsGenres)
        //    : base(titre, dateSortie, description, note,imageName, tagsGenres)
        //{
        //    NbSaisons = NbSaisons;
        //}

        public override string ToString()
        {
            return $"{Titre} , {DateSortie} , {Note} , {Description}";
        }
    }
}
