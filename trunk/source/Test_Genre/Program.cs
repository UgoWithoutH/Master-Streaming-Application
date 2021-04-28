using Class;
using System;

namespace Test_Genre
{
    class Program
    {
        static void Main(string[] args)
        {
            Genre g1 = new Genre("Drame");
            Genre g2 = new Genre("Drame");
            Genre g3 = new Genre("horreur");

            Console.WriteLine(g1);
            Console.WriteLine(g2);
            Console.WriteLine(g3);

            Console.WriteLine(g1.Equals(g2));
            Console.WriteLine(g1.Equals(g3));
        }
    }
}
