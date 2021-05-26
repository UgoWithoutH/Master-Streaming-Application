using System;
using Class;

namespace Test_DataContract
{
    class Program
    {
        static void Main(string[] args)
        {
            MainManager Mmanager = new MainManager(new Stub.Stub());
            Mmanager.ChargeDonnées();
            Mmanager.Persistance = new DataContractPersistance.DataContractPers();
            Mmanager.SauvegardeDonnées();
        }
    }
}
