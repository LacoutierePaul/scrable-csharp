using System;
using System.IO;

namespace Scrabble_Paul_Lacoutiere
{
    public class Projet_main
    {

        #region Méthode SaisieNombre
        public static int SaisieNombre()
        {
            int nombre = Convert.ToInt32(Console.ReadLine());
            while (nombre > 2 || nombre < 1)
            {
                Console.WriteLine("Donnez 1 ou 2 ");

                nombre = Convert.ToInt32(Console.ReadLine());
            }
            return nombre;
        }
        #endregion

        #region Main
        public static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            Console.WindowHeight = 40;
            Console.WindowWidth = 120;

            {
                Console.Clear();
                Console.WriteLine("Bienvenue au jeu du Scrabble! \nMenu :\n"
                                 + "Choix 1 : Création d'une nouvelle partie\n"
                                 + "Choix 2: Chargement d'une partie déja existante\n"

                                 + "Faire votre choix ");
                int choix = SaisieNombre();
                switch (choix)
                {
                    case 1:
                        Console.WriteLine("Vous avez donc choisi de créer une nouvelle partie ");

                        Dictionnaire mondico = new Dictionnaire("Dico.txt", "français");
                        plateau monplateau = new plateau();
                        Sac_jeton monsacjeton = new Sac_jeton("Jetons.txt");
                        Jeu monjeu = new Jeu(mondico, monplateau, monsacjeton); // On initialise le jeu
                        monjeu.AjouterJoueur(); // on ajoute les personnes
                        monjeu.AffichageRegle();
                        monjeu.Partie();

                        break;

                    case 2:
                        Console.WriteLine("Vous reprennez une partie en cours ");
                        Dictionnaire mondico2 = new Dictionnaire("Dico.txt", "français");
                        plateau monplateau2 = new plateau("InstancePlateau.txt");
                        Sac_jeton monsacjeton2 = new Sac_jeton("NouveauSacjeton.txt");
                        Jeu monjeu2 = new Jeu(mondico2, monplateau2, monsacjeton2);
                        monjeu2.RecuperationJoueurTour();
                        monjeu2.RecuperationJoueur();
                        monjeu2.Partie();

                        break;




                }

                Console.WriteLine("Tapez Escape pour sortir ou un numero d exo");
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape) ;

            Console.Read();
        }


        #endregion

    }
}
