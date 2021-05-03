using System;
using Class;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Test_ProfilManager_Tris_et_filtres
{
    class Program
    {
        static void Main(string[] args)
        {
            var PManager = new ProfilManager();

            Oeuvre o1 = new Serie("Elite", new DateTime(2019, 10, 1), "Série mêlant Drame et Amour", 5, "////", 3, new HashSet<Genre>() { new Genre("Drame"), new Genre("Action") });
            Oeuvre o2 = new Serie("A La casa de papel", DateTime.Now, "Série mêlant Drame et Action", 5, "////", 3, new HashSet<Genre>() { new Genre("Drame") });
            Oeuvre o3 = new Serie("Ah bon", new DateTime(2000, 02, 20), "Pas vraiement une série", 0, "////", 0, new HashSet<Genre>() { new Genre("Drame") });

            //Ajout
            PManager.AjouterGenre(new Genre("Drame"));
            PManager.AjouterGenre(new Genre("Action"));
            PManager.AjouterOeuvre(o1);
            PManager.AjouterOeuvre(o2);
            PManager.AjouterOeuvre(o3);

            //Affichage
            foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (Oeuvre o in kpv.Value)
                {
                    Console.WriteLine($"{o}");
                }
            }

            //var list = new List<Oeuvre>() { o1, o2, o3 };
            //foreach (Oeuvre o in list)
            //{
            //    Console.WriteLine(o);
            //}

            //Console.WriteLine("\n\n Après : \n");

            //List<Oeuvre> test = list.OrderBy(o => o.Note).ToList();
            //foreach (Oeuvre o in test)
            //{
            //    Console.WriteLine(o);
            //}

            //Tris

            Genre genreCourant = new Genre("Drame");
            // --> Notes puis alphabétique en cas de notes équivalentes
            Console.WriteLine("\nTest ordre Notes\n\n");

            PManager.TrierNotes(genreCourant);

            foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> kpv in PManager.ListOeuvres)
            {
                Console.Write($"Key : {kpv.Key} value : \n");
                foreach (Oeuvre o in kpv.Value)
                {
                    Console.WriteLine($"{o}");
                }
            }

            Console.WriteLine("\nTest ordre Alphabétique\n\n");

            PManager.TrierOrdreAlph(genreCourant);

            foreach (KeyValuePair<Genre, ObservableCollection<Oeuvre>> kpv in PManager.ListOeuvres)
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
