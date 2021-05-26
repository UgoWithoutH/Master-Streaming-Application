using Microsoft.VisualStudio.TestTools.UnitTesting;
using Class;
using System.Collections.Generic;
using System;
using Swordfish.NET.Collections;
using System.Collections.ObjectModel;

namespace TestU_Recherche
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRecherche()
        {
            var ensemble = new ConcurrentObservableSortedDictionary<Genre, ObservableCollection<Oeuvre>>();

            var l1 = new ObservableCollection<Oeuvre>()
            {
                new Serie("Elite",new DateTime(2019,10,1),"Série mêlant Drame et Amour",0,"////",3, new List<Auteur>() {new Auteur("Dupond", "Dupont", Métier.Cascadeur)},new HashSet<Genre>(){new Genre("Drame"), new Genre("Amour")}),
                new Serie("La casa de papel",DateTime.Now,"Série mêlant Drame et Action",null,"////",3, new List<Auteur>() {new Auteur("Dupond", "Dupont", Métier.Cascadeur)},new HashSet<Genre>(){new Genre("Drame")}),
                new Serie("La petite maison dans la prairie",new DateTime(2000,02,20),"Pas vraiement une série",null,"////",0, new List<Auteur>() {new Auteur("Dupond", "Dupont", Métier.Cascadeur)},new HashSet<Genre>(){new Genre("Drame")}),
            };

            var l2 = new ObservableCollection<Oeuvre>()
            {
                new Serie("Elite",new DateTime(2019,10,1),"Série mêlant Drame et Amour",null,"////",3, new List<Auteur>() {new Auteur("Dupond", "Dupont", Métier.Cascadeur)}, new HashSet<Genre>(){new Genre("Drame"), new Genre("Amour")}),
                new Serie("Une série",DateTime.Now,"Série mêlant Amour",null,"////",3, new List<Auteur>() {new Auteur("Dupond", "Dupont", Métier.Cascadeur)}, new HashSet<Genre>(){new Genre("Amour")}),
                new Serie("Bonne une série",new DateTime(2000,02,20),"Pas vraiment une série",null,"////",0, new List<Auteur>() {new Auteur("Dupond", "Dupont", Métier.Cascadeur)}, new HashSet<Genre>(){new Genre("Amour")}),
            };

            ensemble.Add(new Genre("Drame"), l1);
            ensemble.Add(new Genre("Amour"), l2);

           
            Assert.AreEqual(ensemble.Count, 2);

            foreach (var listing in ensemble)
            {
                Assert.AreEqual(listing.Value.Count, 3);
            }

            var MaRecherche = ensemble.RechercherOeuvres("elite");
            Assert.AreEqual(MaRecherche.Count, 1);
            foreach (Oeuvre o in MaRecherche)
            {
                Assert.IsTrue(o.Titre.StartsWith("Elite"));
            }
        }
    }
}
