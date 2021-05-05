using Class;
using System;
using System.Collections.Generic;

namespace Test_ListDates
{
    class Program
    {
        static void Main(string[] args)
        {
            var List = new SortedSet<int>();

            List.Add(50);
            List.Add(20);
            List.Add(110);

            foreach (int i in List)
            {
                Console.WriteLine(i);
            }
        }
    }
}
