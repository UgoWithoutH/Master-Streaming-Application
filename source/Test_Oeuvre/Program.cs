using Class;
using System;
using System.Collections.Generic;

namespace Test_Oeuvre
{
    class Program
    {
        static void Main(string[] args)
        {
            Oeuvre s1 = new Serie("Elite", new DateTime(2019, 10, 1), "Série mêlant Drame et Amour", 55,"////", 3, new List<Auteur>() { new Auteur("Jean","Peret",Métier.Producteur) },new HashSet<Genre>() { new Genre("Drame"), new Genre("Amour") });
            Oeuvre s2 = new Serie("La casa de papel", DateTime.Now, "Série mêlant Drame et Action", -2, "////", 3, new List<Auteur>(), new HashSet<Genre>() { new Genre("Drame") });
            Oeuvre s3 = new Serie("La petite maison dans la prairie", new DateTime(2000, 02, 20),"Pas vraiement une série", null, "////", 0, new List<Auteur>(), new HashSet<Genre>() { new Genre("Drame") });
            Oeuvre s4 = new Serie("Elite",new DateTime(2019,10,1),"Série mêlant Drame et Amour", 0, "////",3, new List<Auteur>(), new HashSet<Genre>(){new Genre("Drame"), new Genre("Amour")});
            Oeuvre s5 = new Serie("Une série", DateTime.Now, "Série mêlant Amour", null, "////", 3, new List<Auteur>(), new HashSet<Genre>() { new Genre("Amour") });
            Oeuvre s6 = new Serie("Bonne une série", new DateTime(2000, 02, 20), "Pas vraiment une série", null, "////", 0, new List<Auteur>(), new HashSet<Genre>() { new Genre("Amour") });

            //test sortedSet d'Oeuvre
            var list = new SortedSet<Oeuvre>() { s1, s2, s3, s4, s5, s6 };

            foreach (Oeuvre o in list)
            {
                Console.WriteLine(o);
            }

            Console.WriteLine(s1.Equals(null));
        }
    }
}
