using System;
using Class;

namespace Test_Auteur
{
    class Program
    {
        static void Main(string[] args)
        {
            Auteur a1 = new Auteur("Jean","Paul",Métier.Acteur);
            Auteur a1bis = new Auteur("Jean", "Paul", Métier.Acteur);

            Console.WriteLine(a1);

            /////////////
            
            Console.WriteLine(a1.Equals(a1bis));
        }
    }
}
