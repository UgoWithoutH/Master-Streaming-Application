using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
     public static class URecherche
    {
        /// <summary>
        /// Rechercher s'il y a des Titres d'Oeuvres qui correspondent avec la chaine de caractère passée en paramètres
        /// </summary>
        /// <param name="données">contient l'ensemble des listes associées à leurs genres</param>
        /// <param name="chaine">chaine de caractères devant être comparée au début du Titre des Oeuvres</param>
        /// <returns>La liste des Oeuvres contenant au début de leur Titre la chaine de caractères</returns>
        public static List<Oeuvre> RechercherOeuvres(this Dictionary<Genre, List<Oeuvre>> données, string chaine) //this pour lier la méthode aux Dictionary
        {
            var résultat = new List<Oeuvre>();
            List<Oeuvre> listVariable = null;

            foreach (KeyValuePair<Genre,List<Oeuvre>> ensemble in données)
            {
                foreach(Oeuvre oeuvre in listVariable = ensemble.Value)
                {
                    if (oeuvre.Titre.ToUpper().StartsWith(chaine.ToUpper()) && !listVariable.Contains(oeuvre))
                        résultat.Add(oeuvre);
                }
                
            }

            return résultat;
        }
    }
}
