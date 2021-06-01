using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    /// <summary>
    /// Enumération contenant les métiers qu'un Auteur peut avoir 
    /// (On considère que chaque Réalisateur, Acteur, Cascadeur... est un "Auteur" de l'Oeuvre)
    /// </summary>
    public enum Métier
    {
        Réalisateur = 1, // 00001
        Acteur = 2,      // 00010
        Scénariste = 4,  // 00100
        Cascadeur = 8,   // 01000
        Producteur = 16  // 10000
    }
}
