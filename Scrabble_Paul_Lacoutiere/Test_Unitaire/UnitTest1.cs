using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Scrabble_Paul_Lacoutiere
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test si on peut bien ajouter un score à un joueur
        /// </summary>
        [TestMethod]

        public void TestMethod1()
        { 
            Joueur joueur=new Joueur ("Paul");
            joueur.Add_score(19);
            bool b = false;
            if (joueur.Score == 19) b = true;
            Assert.AreEqual(true, b);

        }

        /// <summary>
        /// Test si la recherche dans le dictionnaire fontionne bien
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            Dictionnaire dico = new Dictionnaire("Dico.txt", "français");
            string mot = "poule";
            bool b=dico.RechercheDichoRecursif(0, dico.Dico[mot.Length - 2].Length, mot);
            Assert.AreEqual(true, b);
        }

        /// <summary>
        /// Test si on peut bien ajouter un jeton à la main courante d'un joueur
        /// </summary>
        [TestMethod]

        public void TestMethod3()
        {
            Joueur joueur = new Joueur("Louis"); 
            Jeton jeton = new Jeton('A', 1);
            joueur.Add_Main_Courante(jeton);
            bool b = false;
            if(joueur.Maincourante[0].Lettre == jeton.Lettre && joueur.Maincourante[0].Score==jeton.Score) b=true;
            Assert.AreEqual(true, b);


        }
        /// <summary>
        /// On test si une lettre à un jeton correspond dans la main (existemain courante)
        /// On fait deux test, cas vrai et cas faux
        /// On test aussi si on peut enlever un jeton
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        { 
            Joueur joueur = new Joueur("Louis");
            Jeton jeton = new Jeton('A', 1);
            joueur.Add_Main_Courante(jeton);
            Jeton jeton2=joueur.ExisteMainCourante('A');
            bool b = false;
            if (jeton2 != null &&jeton.Lettre == jeton2.Lettre && jeton.Score == jeton2.Score) b = true;
            Assert.AreEqual(true, b);
            bool c = false;
            Jeton jeton3 = joueur.ExisteMainCourante('Z');
            if (jeton3 != null && jeton.Lettre == jeton3.Lettre && jeton.Score == jeton3.Score) c = true;
            Assert.AreEqual(false, c);
            joueur.Remove_Main_Courante(jeton);
            bool d = false;
            if (joueur.Maincourante.Count == 0) d = true;
            Assert.AreEqual(true, d);
        }

        [TestMethod]

        public  void TestMethode5()
        {
            Sac_jeton monsacjeton = new Sac_jeton("Jetons.txt");
            Random r = new Random();
            monsacjeton.Retire_Jeton(r);
            monsacjeton.Retire_Jeton(r);
            bool f = false;
            if (monsacjeton.NombreDejeton() == 100) f = true;
            Assert.AreEqual(true, f);
        }


    }



}
