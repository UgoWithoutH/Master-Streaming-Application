using Class;
using Swordfish.NET.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Test_ProfilManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ProfilManager PManager = new ProfilManager("test");

            PManager.AjouterGenre(new Genre("Drame"));
            PManager.AjouterGenre(new Genre("Action"));

            foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.WriteLine($"Key : {kpv.Key}");
            }

            PManager.AjouterOeuvre(new Serie("Elite", new DateTime(2019, 10, 1), "Série mêlant Drame et Amour", null, "////", 3, new List<Auteur>(),new HashSet<Genre>() { new Genre("Drame"), new Genre("Action") }));
            PManager.AjouterOeuvre(new Serie("Elite", new DateTime(2019, 10, 1), "Série mêlant Drame et Amour", null, "////", 3, new List<Auteur>(), new HashSet<Genre>() { new Genre("Drame"), new Genre("Action") }));
            PManager.AjouterOeuvre(new Serie("La casa de papel", DateTime.Now, "Série mêlant Drame et Action", null, "////", 3, new List<Auteur>(), new HashSet<Genre>() { new Genre("Aventure") }));
            PManager.AjouterOeuvre(new Serie("La petite maison dans la prairie", new DateTime(2000, 02, 20), "Pas vraiement une série", null, "////", 0, new List<Auteur>(), new HashSet<Genre>() { new Genre("Drame") }));
            PManager.AjouterOeuvre(new Serie("Serie 2", new DateTime(2000, 02, 20), "Pas vraiement une série", null, "////", 0, new List<Auteur>(), new HashSet<Genre>() { new Genre("Drame") }));

            foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach(Oeuvre o in kpv.Value)
                {
                    Console.WriteLine($"{o}");
                }
            }


            Console.WriteLine("\nListe des Dates :");
            foreach (KeyValuePair<Genre, ConcurrentObservableSortedSet<string>> kpv in PManager.ListingDates)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (string chaine in kpv.Value)
                {
                    Console.WriteLine($"{chaine}");
                }
            }

            Console.WriteLine("\nSuppression oeuvre La petite maison dans la prairie");
            PManager.SupprimerOeuvre(new Serie("La petite maison dans la prairie", new DateTime(2000, 02, 20), "Pas vraiement une série", null, "////", 0, new List<Auteur>(), new HashSet<Genre>() { new Genre("Drame") }));
            foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (Oeuvre o in kpv.Value)
                {
                    Console.WriteLine($"{o}");
                }
            }

            Console.WriteLine("\nListe des Dates :");
            foreach (KeyValuePair<Genre, ConcurrentObservableSortedSet<string>> kpv in PManager.ListingDates)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (string chaine in kpv.Value)
                {
                    Console.WriteLine($"{chaine}");
                }
            }

            //Console.WriteLine("\nSuppression Genre Drame");
            //PManager.SupprimerGenre(new Genre("Drame"));
            //foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> kpv in PManager.ListOeuvres)
            //{
            //    Console.Write($"Key : {kpv.Key} value : \n");
            //    foreach (Oeuvre o in kpv.Value)
            //    {
            //        Console.WriteLine($"{o}");
            //    }
            //}

            Console.WriteLine("\nSuppression et ajout Oeuvre Elite");
            PManager.SupprimerOeuvre(new Serie("Elite", new DateTime(2019, 10, 1), "Série mêlant Drame et Amour", null, "////", 3, new List<Auteur>(), new HashSet<Genre>() { new Genre("Drame"), new Genre("Action") }));
            PManager.AjouterOeuvre(new Serie("Elite", new DateTime(2019, 10, 1), "Série mêlant Drame et Amour", null, "////", 3, new List<Auteur>(), new HashSet<Genre>() { new Genre("Drame"), new Genre("Action") }));
            foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (Oeuvre o in kpv.Value)
                {
                    Console.WriteLine($"{o}");
                }
            }

            Console.WriteLine("\nListe des Dates :");
            foreach (KeyValuePair<Genre, ConcurrentObservableSortedSet<string>> kpv in PManager.ListingDates)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (string chaine in kpv.Value)
                {
                    Console.WriteLine($"{chaine}");
                }
            }

        }
    }
}
