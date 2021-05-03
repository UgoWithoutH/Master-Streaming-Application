using Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Test_ProfilManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ProfilManager PManager = new ProfilManager();

            PManager.AjouterGenre(new Genre("Drame"));
            PManager.AjouterGenre(new Genre("Action"));

            foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.WriteLine($"Key : {kpv.Key} Value : {kpv.Value}");
            }

            PManager.AjouterOeuvre(new Serie("Elite", new DateTime(2019, 10, 1), "Série mêlant Drame et Amour", null, "////", 3, new HashSet<Genre>() { new Genre("Drame"), new Genre("Action") }));
            PManager.AjouterOeuvre(new Serie("La casa de papel", DateTime.Now, "Série mêlant Drame et Action", null, "////", 3, new HashSet<Genre>() { new Genre("Aventure") }));
            PManager.AjouterOeuvre(new Serie("La petite maison dans la prairie", new DateTime(2000, 02, 20), "Pas vraiement une série", null, "////", 0, new HashSet<Genre>() { new Genre("Drame") }));
            PManager.AjouterOeuvre(new Serie("Peppa pig", new DateTime(2000, 02, 20), "Pas vraiement une série", null, "////", 0, new HashSet<Genre>() { new Genre("Drame") }));

            foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach(Oeuvre o in kpv.Value)
                {
                    Console.WriteLine($"{o}");
                }
            }


            Console.WriteLine("\nListe des Dates :");
            foreach (KeyValuePair<Genre,SortedSet<int>> kpv in PManager.ListingDates)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (int i in kpv.Value)
                {
                    Console.WriteLine($"{i}");
                }
            }

            Console.WriteLine("\nSuppression oeuvre La petite maison dans la prairie");
            PManager.SupprimerOeuvre(new Serie("La petite maison dans la prairie", new DateTime(2000, 02, 20), "Pas vraiement une série", null, "////", 0, new HashSet<Genre>() { new Genre("Drame") }));
            foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (Oeuvre o in kpv.Value)
                {
                    Console.WriteLine($"{o}");
                }
            }

            Console.WriteLine("\nListe des Dates :");
            foreach (KeyValuePair<Genre, SortedSet<int>> kpv in PManager.ListingDates)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (int i in kpv.Value)
                {
                    Console.WriteLine($"{i}");
                }
            }

            Console.WriteLine("\nSuppression Genre Drame");
            PManager.SupprimerGenre(new Genre("Drame"));
            foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (Oeuvre o in kpv.Value)
                {
                    Console.WriteLine($"{o}");
                }
            }

            Console.WriteLine("\nListe des Dates :");
            foreach (KeyValuePair<Genre, SortedSet<int>> kpv in PManager.ListingDates)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (int i in kpv.Value)
                {
                    Console.WriteLine($"{i}");
                }
            }

        }
    }
}
