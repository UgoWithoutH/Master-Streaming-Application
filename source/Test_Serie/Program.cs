using Class;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Test_Serie
{
    class Program
    {
        static void Main(string[] args)
        {
            Oeuvre s1 = new Serie("Elite", DateTime.Now, 6,"C'est cool", "///////", 52, new List<Auteur>() { new Auteur("Jean","Paul",Métier.Acteur),
                                                                                                             new Auteur("Paul","Jack",Métier.Cascadeur)});

            Oeuvre s2 = new Serie("Heyy", DateTime.Now, -5,"C'est cool", "///////", 52,null);

            Serie s3 = new Serie("Elite", new DateTime(1999,01,15), null, "C'est cool", "///////", 52,null);

            Serie s4 = new Serie("Harry", new DateTime(1999,01,15), "C'est cool", "///////",52);

            Console.WriteLine(s1);
            Console.WriteLine(s2);
            Console.WriteLine(s3);
            Console.WriteLine(s4);

            Console.WriteLine(s1.Equals(s3));

            Console.WriteLine("Affichage Auteurs");
            
            foreach(Auteur a in s1.ListAuteur)
            {
                Console.WriteLine(a);
            }
        }
    }
}
