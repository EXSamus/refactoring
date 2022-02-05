using NUnit.Framework;
using refactoring;
using System;
namespace test
{
    public class SecteurUnitTests
    {
        public Secteur secteur;
        [SetUp]
        public void Setup()
        {
            secteur = new Secteur();
        }

        [Test]
        public void my_randTest()
        {
            int test1 = secteur.my_rand(1, 20);
            Assert.NotZero(test1, "Test 1 Echec(Egal à 0)");
            int test2 = secteur.my_rand(1, 20);
            Assert.NotZero(test2, "Test 2 Echec(Egal à 0)");
            int test3 = secteur.my_rand(1, 20);
            Assert.NotZero(test3, "Test 3 Echec(Egal à 0)");
        }
        [Test]
        public void crashTest()
        {
            Boolean test1 = secteur.crash(0);
            Assert.IsFalse(test1, "crashTest 1 Echec(IsFalse)");
            Boolean test2 = secteur.crash(0);
            Assert.IsFalse(test2, "crashTest 2 Echec(IsFalse)");
            Boolean test3 = secteur.crash(1000000);
            Assert.IsTrue(test3, "crashTest 3 Echec(isTrue)");
        }
        [Test]
        public void standTest()
        {
            Boolean test1 = secteur.stand();
            if(test1){Assert.IsTrue(test1, "standTest 1 Echec(isTrue)");}
            else if(!test1){Assert.IsFalse(test1, "standTest 1 Echec(isFalse)");}          
            else{Assert.Fail();}

            Boolean test2 = secteur.stand();
            if(test2){Assert.IsTrue(test2, "standTest 2 Echec(isTrue)");}
            else if(!test2){Assert.IsFalse(test2, "standTest 2 Echec(isFalse)");}          
            else{Assert.Fail();}
        }

        [Test]
        public void secteurTest()
        {
            int test1 = secteur.secteur(1,200,0);
            Assert.NotZero(test1, "secteurTest 1 Echec(NotZero)");
            int test2 = secteur.secteur(1,2,1000000);
            Assert.Zero(test2, "secteurTest 1 Echec(Zero, Crash)");
        }
    }
}