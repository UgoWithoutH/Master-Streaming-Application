using System;
using Xunit;
using Class;

namespace TestU_MainManager
{
    public class UnitTest1
    {
        [Fact]
        public void TestMainManager()
        {
            MainManager LeManager = new MainManager(new Stub.Stub());
            TestAjouteProfil(LeManager);
            TestSupprimeProfil(LeManager);
            TestProfilCourant(LeManager);
        }

        public void TestAjouteProfil(MainManager Mana)
        {
            Mana.AjouteProfil("Jean");
            Mana.AjouteProfil("Sébastien");
            Mana.AjouteProfil("Marie");
            Mana.AjouteProfil("Joseph");
            Assert.Equal(Mana.ListProfils.Count, 4);
        }

        public void TestSupprimeProfil(MainManager Mana)
        {
            Mana.SupprimeProfil("Jean");
            Mana.SupprimeProfil("Sébastien");
            Assert.Equal(Mana.ListProfils.Count, 2);
        }

        public void TestProfilCourant(MainManager Mana)
        {
            Assert.True(Mana.Connexion("Marie"));
            Mana.Deconnexion();
            Assert.Equal(Mana.ProfilCourant, null);
        }
    }
}
