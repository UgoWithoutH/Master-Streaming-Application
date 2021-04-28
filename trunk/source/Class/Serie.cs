using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
   public class Serie : Oeuvre
    {
        public int NbSaisons { get; set; }

        public Serie(string titre, DateTime dateSortie, int? note, string description, string imageName, int nbSaisons, List<Auteur> listeAuteurs)
            : base(titre, dateSortie, note, description, imageName, listeAuteurs)
        {
            NbSaisons = NbSaisons;
        }

        public Serie(string titre, DateTime dateSortie, string description, string imageName, int nbSaisons)
            : base(titre, dateSortie, description, imageName)
        {
            NbSaisons = NbSaisons;
        }

        public override string ToString()
        {
            return $"{Titre} , {DateSortie} , {Note} , {Description}";
        }
    }
}
