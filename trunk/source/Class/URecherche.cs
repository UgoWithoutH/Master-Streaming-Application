using Swordfish.NET.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static ObservableCollection<Oeuvre> RechercherOeuvres(this ConcurrentObservableSortedDictionary<Genre, ObservableCollection<Oeuvre>> données, string chaine) //this pour faire une méthode d'extension
        {
            var résultat = new ObservableCollection<Oeuvre>();
            ObservableCollection<Oeuvre> listVariable = null;

            foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> ensemble in données)
            {
                foreach(Oeuvre oeuvre in listVariable = ensemble.Value)
                {
                    if (oeuvre.Titre.ToUpper().StartsWith(chaine.ToUpper()) && !résultat.Contains(oeuvre))
                        résultat.Add(oeuvre);
                }
                
            }

            return résultat;
        }
    }
}
