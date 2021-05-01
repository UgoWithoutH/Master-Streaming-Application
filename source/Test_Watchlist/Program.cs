using Class;
using System;
using System.Collections.Generic;

namespace TestWatchlist
{
    class Program
    {
        static void Main(string[] args)
        {
            Oeuvre s1 = new Serie("Elite", DateTime.Now, "C'est cool", 4, "///////", 52, new List<Auteur>() { new Auteur("Jean","Paul",Métier.Acteur),
                                                                                                             new Auteur("Paul","Jack",Métier.Cascadeur)}, new HashSet<Genre>() { new Genre("Drame") });

            Watchlist w = new Watchlist();
            Console.WriteLine($"La valeur de retour de l'ajout est { w.AjouterOeuvre(s1) } ");
            Console.WriteLine($"La valeur de retour de la suppression est { w.SupprimerOeuvre(s1) } ");
        }
    }
}
