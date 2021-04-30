using Class;
using System;
using System.Collections.Generic;

namespace Test_ProfilManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ProfilManager PManager = new ProfilManager();

            PManager.AjouterGenre(new Genre("Drame"));
            PManager.AjouterGenre(new Genre("Action"));

            foreach (KeyValuePair<Genre,HashSet<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.WriteLine($"Key : {kpv.Key} Value : {kpv.Value}");
            }

            PManager.AjouterOeuvre(new Serie("Elite", new DateTime(2019, 10, 1), "Série mêlant Drame et Amour", "////", 3, new HashSet<Genre>() { new Genre("Drame"), new Genre("Action") }));
            PManager.AjouterOeuvre(new Serie("La casa de papel", DateTime.Now, "Série mêlant Drame et Action", "////", 3, new HashSet<Genre>() { new Genre("Aventure") }));
            PManager.AjouterOeuvre(new Serie("La petite maison dans la prairie", new DateTime(2000, 02, 20), "Pas vraiement une série", "////", 0, new HashSet<Genre>() { new Genre("Drame") }));

            foreach (KeyValuePair<Genre, HashSet<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach(Oeuvre o in kpv.Value)
                {
                    Console.WriteLine($"{o}");
                }
            }

            Console.WriteLine("\nSuppression Genre");
            PManager.SupprimerGenre(new Genre("Drame"));
            foreach (KeyValuePair<Genre, HashSet<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (Oeuvre o in kpv.Value)
                {
                    Console.WriteLine($"{o}");
                }
            }

            Console.WriteLine("\nSuppression oeuvre");
            PManager.SupprimerOeuvre(new Serie("Elite", new DateTime(2019, 10, 1), "Série mêlant Drame et Amour", "////", 3, new HashSet<Genre>() { new Genre("Drame"), new Genre("Action") }));
            foreach (KeyValuePair<Genre, HashSet<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (Oeuvre o in kpv.Value)
                {
                    Console.WriteLine($"{o}");
                }
            }


        }
    }
}
