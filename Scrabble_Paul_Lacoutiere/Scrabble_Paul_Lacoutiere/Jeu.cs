using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Scrabble_Paul_Lacoutiere
{
   
        public class Jeu
        {

        #region Attributs

        Dictionnaire mondico;
            plateau monplateau;
            Sac_jeton monsacjeton;
            List<Joueur> mesjoueurs;
            int Touractuelle;
        int nombrejoueur;
        int tempsmax;

        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur permettant d'initialiser un nouveau jeu
        /// </summary>
        /// <param name="mondico">dictionnaire du jeu</param>
        /// <param name="monplateau">plateau utiliser pour le jeu</param>
        /// <param name="monsacjeton">sacjeton utiliser pour le jeu</param>
        public Jeu(Dictionnaire mondico, plateau monplateau, Sac_jeton monsacjeton)
            {
                this.mondico = mondico;
                this.monplateau = monplateau;
                this.monsacjeton = monsacjeton;
                mesjoueurs = new List<Joueur>();
                Touractuelle = 0;
                 nombrejoueur = 0;
             tempsmax = 0;
            }


        #endregion

        #region Méthode Utile
        /// <summary>
        /// Méthode de classe qui vérifie  qu'un string puisse être convertible en un int
        /// </summary>
        /// <param name="nom"> string à convertir</param>
        /// <returns> return 0 si pas possible ou la convertistion du string en int</returns>
        public static int Valable(string nom)
            {
                int result = 0;
                bool b = int.TryParse(nom, out result);
                while (b == false)
                {
                    Console.WriteLine("Donner un chiffre");
                    nom = Console.ReadLine();
                    b = int.TryParse(nom, out result);
                }
                return result;

            }

            /// <summary>
            /// méthode d'instance  permettant la saisie d'un nombre en 1 et 3
            /// </summary>
            /// <returns>renvoie le nombre</returns>
            public static int SaisieNombre()
            {
                int nombre = Valable(Console.ReadLine());

                while (nombre > 3 || nombre < 1)
                {
                    Console.WriteLine("Donnez 1 ou 2 ou 3 ");
                    nombre = Valable(Console.ReadLine());
                }
                return nombre;
            }



        /// <summary>
        /// Méthode d'instance qui va créer les joueurs et les ajouter à la liste du joueur pour le lancement d'une nouvelle partie
        /// </summary>
        public void AjouterJoueur() // Evitez que deux personnnes s'appellent pareil
        {
            Console.WriteLine("A combien voulez-vous jouez ? Le jeu du Scrabble se joue entre 2 et 4 joueurs");
            int nbjoueur;
            string nombrejoueurs = Console.ReadLine();
            bool valable = int.TryParse(nombrejoueurs, out nbjoueur);
            string verification = "";
            while (verification != "ok")
            {

                while (nbjoueur != 2 && nbjoueur != 3 && nbjoueur != 4 || verification == "non")
                {
                    verification = null;
                    Console.WriteLine("Le jeu du Scrabble se joue entre 2 et 4 joueurs");
                    nombrejoueurs = Console.ReadLine();
                    valable = int.TryParse(nombrejoueurs, out nbjoueur);
                    while (valable == false)
                    {
                        Console.WriteLine("Donnez un chiffre seulement");
                        valable = int.TryParse(Console.ReadLine(), out nbjoueur);

                    }

                }
                Console.WriteLine("Il y aura donc " + nbjoueur + " joueurs");
                Console.WriteLine("Confirmez avec ok si c'est bon, sinon tapez non");
                verification = Console.ReadLine();
            }
            //----------- Initialisation des joueurs ---------------------//
            Console.WriteLine("Donnez le nom du premier joueur");
            string nom1 = Console.ReadLine();
            Joueur joueur1 = new Joueur(nom1);
            while (joueur1.Maincourante.Count != 7)           //On ajoute 7 jetons à la main courante
            {
                joueur1.Add_Main_Courante(monsacjeton.Retire_Jeton(new Random()));
            }
            mesjoueurs.Add(joueur1);
            nombrejoueur++;
            Console.WriteLine("Donnez le nom du deuxième joueur");
            string nom2 = Console.ReadLine();
            Joueur joueur2 = new Joueur(nom2);
            while (joueur2.Maincourante.Count != 7)           //On ajoute 7 jetons à la main courante
            {
                joueur2.Add_Main_Courante(monsacjeton.Retire_Jeton(new Random()));
            }
            mesjoueurs.Add(joueur2);
            nombrejoueur++;
            if (nbjoueur >= 3)
            {
                Console.WriteLine("Donnez le nom du troisième joueur");
                string nom3 = Console.ReadLine();
                Joueur joueur3 = new Joueur(nom3);
                while (joueur3.Maincourante.Count != 7)           //On ajoute 7 jetons à la main courante
                {
                    joueur3.Add_Main_Courante(monsacjeton.Retire_Jeton(new Random()));
                }
                mesjoueurs.Add(joueur3);
                nombrejoueur++;
                if (nbjoueur == 4)
                {
                    Console.WriteLine("Donnez le nom du quatrième joueur");
                    string nom4 = Console.ReadLine();
                    Joueur joueur4 = new Joueur(nom4);
                    while (joueur4.Maincourante.Count != 7)           //On ajoute 7 jetons à la main courante
                    {
                        joueur4.Add_Main_Courante(monsacjeton.Retire_Jeton(new Random()));
                    }
                    mesjoueurs.Add(joueur4);
                    nombrejoueur++;
                }
            }
            Console.WriteLine("Vous allez décider du temps que chaque joueur aura pour jouer (en minutes)");
            this.tempsmax = Valable(Console.ReadLine());

        }

        /// <summary>
        /// FMéthode qui met une lettre passé en paramètre en majuscule
        /// </summary>
        /// <param name="lettre"> lettre que l'on veut mettre en majuscule</param>
        /// <returns> lettre en majuscule</returns>
        public char MettreEnMaj(char lettre)
        {
            string s = lettre + "";
            s = s.ToUpper();
            return Convert.ToChar(s);
        }

        /// <summary>
        /// Méthode qui test si le premier a déja été placé.
        /// </summary>
        /// <returns>vrai ou faux </returns>
        public bool PremierMotplace()
        {
            bool b = false;
            if (monplateau.Matricelettre[7, 7] != '*')
            {
                b = true;
            }
            return b;
        }

        #endregion

        #region Recuperation D'une partie
        /// <summary>
        /// Méthode qui récupere le nombre de tour et le nombre de joueur pour la reprise d'une partie sauvegardé
        /// </summary>
        public void RecuperationJoueurTour()
            {
                try
                {
                    StreamReader reader = new StreamReader("JoueurTour.txt");
                    string str = null;
                    str = reader.ReadLine(); //La première ligne correspond au nom
                this.Touractuelle = Convert.ToInt32(str);
                str = reader.ReadLine();
                this.nombrejoueur = Convert.ToInt32(str);
                str = reader.ReadLine();
                this.tempsmax = Convert.ToInt32(str);

            }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }



            /// <summary>
            /// Méthode qui récupere le joueur pour une la reprise d'une sauvegardé
            /// </summary>
            public void RecuperationJoueur()
             { 
                

                if (nombrejoueur >= 2)
                {
                    Joueur joueur1 = new Joueur("Joueur1.txt", 'c');
                    mesjoueurs.Add(joueur1);
                    Joueur joueur2 = new Joueur("Joueur2.txt", 'c');
                    mesjoueurs.Add(joueur2);
                    if (nombrejoueur >= 3)
                    {
                        Joueur joueur3 = new Joueur("Joueur3.txt", 'c');
                        mesjoueurs.Add(joueur3);
                        if (nombrejoueur == 4)
                        {
                            Joueur joueur4 = new Joueur("Joueur4.txt", 'c');
                            mesjoueurs.Add(joueur4);
                        }
                    }
                }

            }





        #endregion

        #region Partie Et FinPartie

        /// <summary>
        /// C'est le code principal qui va exécuter tout le jeu 
        /// Elle va faire permettre tout le déroulé du jeu.
        /// </summary>
        public void Partie()
            {
          
                string pause = null;
                Console.WriteLine(mondico.toString());
                AfficherLesJoueurs();


                //Comment se déroule une partie : Chaque joueur joue tour à tour
                while (monsacjeton.Sacjeton.Count != 0 && pause != "sauvegarde" && pause != "fin") //tant que le sac n'est pas vide, chacun va joueur tour a tour sauf si l'utilisateur rentre stop pour faire pause a la partie
                {
                    Touractuelle++;
                    Console.WriteLine("Nous sommes au tour n°" + Touractuelle);
                    foreach (Joueur joueur in mesjoueurs) // On commence un tour 
                    {
                        if (monsacjeton.Sacjeton.Count != 0)
                        {
                            Stopwatch mamontre = new Stopwatch();
                            mamontre.Start();

 
                            Console.WriteLine("");
                            Console.WriteLine("Il reste actuellement : " + monsacjeton.Sacjeton.Count + " jetons dans le sac");
                            Console.WriteLine("");
                            Console.Write("C'est au tour de ");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(joueur.Nom);
                            Console.ResetColor();
                            Console.WriteLine("Vous avez " + tempsmax + " minutes pour jouer");
                            Console.WriteLine("");
                            Console.Write("Rappel des scores : ");
                            foreach (Joueur joueur2 in mesjoueurs)
                            {
                                Console.Write(joueur2.Nom + " a : ");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(joueur2.Score);

                                Console.Write(" points  ");
                                Console.ResetColor();
                            }
                            Console.WriteLine("\n");
                            monplateau.AfficherPlateau();
                            Console.WriteLine("");
                            joueur.AfficherMainCourante();
                            bool valable = false;
                            while (valable == false)
                            {
                                Console.WriteLine("Que voulez-vous faire pendant ce tour ? :\n"
                                                 + "Choix 1 : Poser un mot \n"
                                                 + "Choix 2: Passez\n"
                                                 + "Choix 3 Défaussez des jetons");
                                int choix = SaisieNombre();
                                switch (choix)
                                {
                                    case 1:
                                        Console.WriteLine("Vous avez donc choisi de poser un mot");
                                        string motjoueur = null;
                                        string ecriture = null;
                                        Console.WriteLine("Veuillez donner, lettre par lettre les lettres de votre main \nque vous voulez poser et écrivez stop quand vous avez finis");
                                        Console.WriteLine("Donner une lettre");
                                        ecriture = Console.ReadLine();
                                        while (ecriture != "stop" )
                                        {
                                            char lettre;
                                            int result;
                                            bool a = int.TryParse(ecriture, out result);
                                            bool b = char.TryParse(ecriture, out lettre);
                                            while (a == true)  //test si le caractère envoyé n'est pas un chiffre
                                            {
                                                if (ecriture == "stop") break;
                                                Console.WriteLine("Donner une lettre ou stop pour arreter");
                                                ecriture = Console.ReadLine();

                                                a = int.TryParse(ecriture, out result);
                                                if (a == false)
                                                {
                                                    b = char.TryParse(ecriture, out lettre); // test si c'est bien un seul caractère et non un string
                                                    while (b == false)
                                                    {
                                                        Console.WriteLine("Donner une lettre ou stop pour arreter");
                                                        ecriture = Console.ReadLine();
                                                        if (ecriture == "stop") break;
                                                        b = char.TryParse(ecriture, out lettre);

                                                    }
                                                }
                                            }
                                            if (ecriture == "stop") break;
                                            b = char.TryParse(ecriture, out lettre); // test si c'est bien un seul caractère et non un string
                                            while (b == false)
                                            {
                                                Console.WriteLine("Donner une lettre");
                                                ecriture = Console.ReadLine();
                                                if (ecriture == "stop")
                                                {
                                                    ecriture = "";
                                                    break;
                                                }

                                                b = char.TryParse(ecriture, out lettre);

                                            }
                                            if (ecriture == "*") // cas du joker
                                            {
                                                Console.WriteLine("Quel est la lettre que vous voulez utilisez à la place du joker");
                                                char lalettre = Convert.ToChar(Console.ReadLine());
                                                lalettre = MettreEnMaj(lalettre);
                                                joueur.Remove_Main_Courante(joueur.ExisteMainCourante('*'));
                                                Jeton newjeton = monplateau.CharVersJeton(lalettre);
                                                 joueur.Add_Main_Courante(newjeton);
                                                 joueur.Add_score(-newjeton.Score);
                                                ecriture = lalettre + "";

                                            }

                                            motjoueur += ecriture;

                                            Console.WriteLine("Tapez stop si vous voulez vous arretez, sinon donner une lettre");
                                            ecriture = Console.ReadLine();
                                        }
                                        Console.WriteLine("Vous voulez donc poser : " + motjoueur);
                                        Console.WriteLine("Vous allez maintenant donner la ligne ou votre mot commence ");
                                        string ligne = Console.ReadLine();
                                        int laligne;
                                        bool possible = int.TryParse(ligne, out laligne);
                                        while (possible == false)
                                        {
                                            Console.WriteLine("Donner un nombre");
                                            ligne = Console.ReadLine();
                                            possible = int.TryParse(ligne, out laligne);
                                        }
                                        Console.WriteLine("Vous allez maintenant donner la colonne ou votre mot commence ");
                                        string colonne = Console.ReadLine();
                                        int lacolonne;
                                        bool possible2 = int.TryParse(colonne, out lacolonne);
                                        while (possible2 == false)
                                        {

                                            Console.WriteLine("Donner un nombre");
                                            colonne = Console.ReadLine();
                                            possible2 = int.TryParse(colonne, out lacolonne);
                                        }
                                        Console.WriteLine("Donner la direction dans lequel le mot va être posé : h pour horizontal ou v pour vertical");
                                        string ecriture3 =Console.ReadLine();
                                         char direction;
                                         int result2;
                                         bool c = int.TryParse(ecriture3, out result2);
                                         bool d = char.TryParse(ecriture3, out direction);
                                         while (c == true && d ==false)
                                         {
                                        Console.WriteLine("Donner une direction valide");
                                           ecriture3 = Console.ReadLine();
                                             c= int.TryParse(ecriture3, out result2);
                                           d= char.TryParse(ecriture3, out direction);
                                         }
                                         

                                        mamontre.Stop();
                                        TimeSpan temps = new TimeSpan();
                                        temps = mamontre.Elapsed;
                                        if (temps.Minutes < tempsmax)
                                        {
                                            if (PremierMotplace() == true)
                                            {
                                            

                                                valable = monplateau.Test_Plateau(motjoueur, laligne, lacolonne, direction, mondico, joueur);
                                                if (valable == true)
                                                {
                                                    Console.WriteLine("Votre mot à bien été posé : ");
                                                    Console.WriteLine("Le score de " + joueur.Nom + " est maintenant de " + joueur.Score);
                                                    monplateau.AfficherPlateau();
                                                    Console.WriteLine("");
                                                    //joueur.AfficherMainCourante();
                                                    if (joueur.Maincourante.Count == 0)
                                                    {
                                                        Console.WriteLine("SCRAAABLLLLE !!!! Félicitations, vous gagnez 50 points bonus");
                                                        joueur.Add_score(50);
                                                    }
                                                    while (joueur.Maincourante.Count != 7 && monsacjeton.NombreDejeton() != 0)
                                                    {
                                                        joueur.Add_Main_Courante(monsacjeton.Retire_Jeton(new Random()));
                                                    }
                                                    // joueur.AfficherMainCourante();
                                                    Console.WriteLine(joueur.toString());
                                                    Console.WriteLine("");
                                                    if (monsacjeton.NombreDejeton() == 0)
                                                    {
                                                        Partiefin();
                                                        break;

                                                    }

                                                }

                                            }
                                            else
                                            {//  Cas du premier touuuuur
                                            if (motjoueur.Length >= 2 && motjoueur!=null)
                                            {
                                                motjoueur = motjoueur.ToUpper();
                                                if (mondico.RechercheDichoRecursif(0, mondico.Dico[motjoueur.Length - 2].Length, motjoueur))
                                                {

                                                    if (joueur.TestLettres(motjoueur))
                                                    {


                                                        if (direction == 'h')
                                                        {

                                                            if (laligne == 8 && (lacolonne <= 8 && (lacolonne + motjoueur.Length - 1) >= 8))
                                                            {

                                                                int j = 0;
                                                                monplateau.Matricelettre[7, 7] = '_';
                                                                joueur.Add_score(monplateau.CalculerscoreFinal(laligne - 1, lacolonne - 1, motjoueur, direction, null));
                                                                valable = true;
                                                                for (int i = lacolonne - 1; i < lacolonne + motjoueur.Length - 1; i++)
                                                                {

                                                                    monplateau.Matricelettre[laligne - 1, i] = motjoueur[j];
                                                                    joueur.Remove_Main_Courante(joueur.ExisteMainCourante(motjoueur[j]));
                                                                    j++;
                                                                    ;
                                                                }
                                                                joueur.Add_mot(motjoueur);
                                                                while (joueur.Maincourante.Count != 7 && monsacjeton.NombreDejeton() != 0)
                                                                {
                                                                    joueur.Add_Main_Courante(monsacjeton.Retire_Jeton(new Random()));

                                                                }

                                                            }
                                                            else Console.WriteLine(" Le mot ne passe pas par la case centrale ");
                                                        }
                                                        else if (direction == 'v')

                                                        {
                                                            if (lacolonne == 8 && laligne <= 8 && laligne + motjoueur.Length - 1 >= 8)
                                                            {
                                                                int j = 0;
                                                                monplateau.Matricelettre[7, 7] = '_';
                                                                valable = true;
                                                                joueur.Add_score(monplateau.CalculerscoreFinal(laligne - 1, lacolonne - 1, motjoueur, direction, null));
                                                                for (int i = laligne - 1; i < laligne + motjoueur.Length - 1; i++)
                                                                {

                                                                    monplateau.Matricelettre[i, lacolonne - 1] = motjoueur[j];
                                                                    joueur.Remove_Main_Courante(joueur.ExisteMainCourante(motjoueur[j]));
                                                                    j++;
                                                                    ;
                                                                }
                                                                joueur.Add_mot(motjoueur);
                                                                Console.WriteLine("Bravo votre mot va être posé");
                                                                while (joueur.Maincourante.Count != 7 && monsacjeton.NombreDejeton() != 0)
                                                                {
                                                                    joueur.Add_Main_Courante(monsacjeton.Retire_Jeton(new Random()));

                                                                }
                                                            }
                                                            else Console.WriteLine(" Le mot ne passe pas par la case centrale ");
                                                        }
                                                        else Console.WriteLine("Ligne ou colonne non valide");
                                                    }
                                                    else Console.WriteLine("Nous n'avons pas posé votre mot, vous n'avez pas les lettres nécessaires");
                                                }
                                            }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Vous n'avez pas jouer dans le temps imparti : " + temps.Minutes + " min" + temps.Seconds + " secondes");
                                            valable = true;
                                            //break;
                                        }

                                        break;
                                    case 2:

                                        Console.WriteLine("On va donc passer votre tour \n\n");
                                        valable = true;
                                        break;

                                    case 3:

                                        mamontre.Stop();
                                        TimeSpan temps2 = new TimeSpan();
                                        temps2 = mamontre.Elapsed;
                                        if (temps2.Minutes < tempsmax)
                                        {
                                            if (monsacjeton.NombreDejeton() > 7)
                                            {
                                                valable = true;
                                                Console.WriteLine("Vous voulez donc vous défaussez d'un ou plusieurs jetons");
                                                Console.WriteLine("Donnez la lettre du jeton donc vous voulez vous séparer");
                                                string ecriture2 = Console.ReadLine();
                                                while (ecriture2 != "stop" && joueur.Maincourante.Count != 1)
                                                {
                                                    char lettre;
                                                    int result;
                                                    bool a = int.TryParse(ecriture2, out result);

                                                    while (a == true)  //test si le caractère envoyé n'est pas un chiffre
                                                    {
                                                 
                                                    Console.WriteLine("Donner une lettre ");
                                                        ecriture2 = Console.ReadLine();
                                                  
                                                         a = int.TryParse(ecriture2, out result);
                                                            if (a == false)
                                                        {
                                                            bool b = char.TryParse(ecriture2, out lettre); // test si c'est bien un seul caractère et non un string
                                                            while (b == false)
                                                            {

                                                                 Console.WriteLine("Donner une lettre ");
                                                                ecriture2 = Console.ReadLine();
                                                                
                                                            b = char.TryParse(ecriture2, out lettre);
    
                                                            }
                                                            break;

                                                        }
                                                    }
                                                    if (a == false)
                                                    {
                                                        bool b = char.TryParse(ecriture2, out lettre); // test si c'est bien un seul caractère et non un string
                                                        while (b == false)
                                                        {
                                                        Console.WriteLine("Donner une lettre");
                                                            ecriture2 = Console.ReadLine();

                                                            b = char.TryParse(ecriture2, out lettre);

                                                        }
                                                        lettre = MettreEnMaj(lettre);
                                                        string stringlettre = Convert.ToString(lettre);

                                                        bool main = joueur.TestLettres(stringlettre);
                                                        if (main)
                                                        {
                                                            Jeton monjeton = joueur.ExisteMainCourante(lettre);
                                                            joueur.Remove_Main_Courante(monjeton);
                                                            monsacjeton.Sacjeton.Add(monjeton);

                                                        }

                                                    }

                                                    Console.WriteLine("Tapez stop si vous voulez vous arretez, sinon donner une lettre");
                                                    ecriture2 = Console.ReadLine();
                                                }
                                                while (joueur.Maincourante.Count != 7)
                                                {
                                                    joueur.Add_Main_Courante(monsacjeton.Retire_Jeton(new Random()));
                                                }
                                            }
                                            else Console.WriteLine("Il y a trop de peu de jeton dans le sac, on ne peut se défausser de jeton");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Vous n'avez pas jouer dans le temps imparti : " + temps2.Minutes + " min" + temps2.Seconds + " secondes");
                                            valable = true;
                                            // break;
                                        }
                                        break;



                                }
                            }
                        }



                        //Console.ReadLine();
                    }
                    Console.WriteLine("Maintenant que chaque joueur à joué, si vous voulez continuer appuyez sur entrée");
                    Console.WriteLine("Si vous voulez sauvegarder votre partie tapez : sauvegarde");
                    Console.WriteLine("Si vous voulez arréter la partie : tapez fin");
                    pause = Console.ReadLine();

                }
                if (pause == "sauvegarde") // On va sauvegarder les informations du jeu dans des fichiers
                {
                    SauvegarderJoueurTour("JoueurTour.txt");
                    monplateau.WriteFile("InstancePlateau.txt");
                    monsacjeton.WriteFile("NouveauSacjeton.txt");

                    if (mesjoueurs.Count >= 2)
                    {
                        mesjoueurs[0].WriteFile("Joueur1.txt");
                        mesjoueurs[1].WriteFile("Joueur2.txt");
                        if (mesjoueurs.Count >= 3)
                        {
                            mesjoueurs[2].WriteFile("Joueur3.txt");
                            if (mesjoueurs.Count == 4)
                            {
                                mesjoueurs[3].WriteFile("Joueur4.txt");
                            }
                        }
                    }
                }
                if (pause == "fin")
                {
                    Partiefin();
                }
            }



            /// <summary>
            /// C'est la méthode appelé pour la fin de partie 
            /// Elle regarde  a le plus grand score et renvoie le vainqueur ainsi qu'un récapitulatif des mots trouvés.
            /// </summary>
            public void Partiefin()
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("Merci d'avoir jouer au Scrabble ! ");

                Console.Write("Et notre grand vainqueur eeest : ");
                Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Joueur gagnant = null;
                int maximum = 0;
                foreach (Joueur joueur in mesjoueurs)
                {
                    if (joueur.Score > maximum) // test du score le plus grand
                    {
                        gagnant = joueur;
                        maximum = joueur.Score;
                    }
                }
                Console.Write("**************");
                Console.Write(gagnant.Nom);
                Console.WriteLine("**************");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("           Felicitations ! ");
                Console.ResetColor();
                Console.WriteLine("Voici un récapitulatif de la partie :");
                foreach (Joueur joueur in mesjoueurs)
                {
                    Console.WriteLine(joueur.toString());
                    Console.WriteLine("");
                }

            }

        #endregion

        #region SauvegardeDuNombreDetour

        /// <summary>
        /// fonction qui écrit dans un fichier le nombre de tour et le nombre de joueur
        /// </summary>
        /// <param name="filename">Fichier ou est enregistrer le nombre de tour</param>
        public void SauvegarderJoueurTour(string filename)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(filename);
                    writer.WriteLine(Touractuelle);
                    writer.WriteLine(nombrejoueur);
                writer.WriteLine(tempsmax);
                    writer.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        #endregion

        #region Affichage Joueur et Règle
        /// <summary>
        /// Méthode qui permet d'afficher tout les informations des joueurs 
        /// </summary>
        public void AfficherLesJoueurs()
            {
                Console.WriteLine("Voici tout les joueurs :");
                int i = 1;
                foreach (Joueur element in mesjoueurs)
                {
                    Console.WriteLine("Joueur " + i + " s'appelle " + element.Nom + "\n");
                    i++;

                }
                Console.WriteLine("\n Que le meilleur gagne !\n");
            }

        /// <summary>
        /// Méthode qui permet l'affichage des règles en début de partie
        /// </summary>
            public void AffichageRegle()
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Avant de commençer voici un rappel des règles\n");
                Console.ResetColor();
                Console.WriteLine("Le but est d'obtenir un maximum de points en posant des mots");
                Console.WriteLine("Chaque joueur possède un main de 7 jetons");
                Console.WriteLine("Chaque tour, tout les joueurs vont jouer parmis 3 choix : ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("-Poser un mot");
                Console.ResetColor();
                Console.WriteLine("   Dans ce cas, vous allez donner les lettres de votre main que vous voulez posez une par une");
                Console.WriteLine("   Une fois le mot finit, vous écrirez stop et vous allez ensuite donner la ligne et la colonne où votre mot COMMENCE");
                Console.WriteLine("   Vous n'avez pas à donner les lettres déja présentes sur le tableau, elles seront automatiquement prise en compte");
                Console.WriteLine("   Ensuite vous choisissez le sens de votre mot, en vertical, haut vers le bas ou en horizontal, gauche à droite");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("-Passez votre tour");
                Console.ResetColor();
                Console.WriteLine("   Votre tour sera automatiquement passé et le joueur suivant jouera");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("-Défaussez de jeton");
                Console.ResetColor();
                Console.WriteLine("   Vous pouvez vous défaussez d'autant de jeton que vous souhaitez, il suffira de donner lettre par lettre le jeton");
                Console.WriteLine("   Que vous voulez supprimer de votre main");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("   Attention, on ne peut se défausser de jetons que si le sac de jeton à plus de 7 jetons");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine("Après qu'un joueur est joué, le joueur regagne de jetons jusqu'a que ca main revienne à 7 jetons");
                Console.WriteLine("Les points sont calculés automatiquement, les points d'un jeton sont affichés entre parenthèses ");
                Console.WriteLine("Lorsqu'on pose un mot, des bonus multiplicatifs peuvent être appliqué en fonction de la couleur de la case");
                Console.WriteLine("Les légendes seront présentes sur l'affichage du plateau de jeux");
                Console.WriteLine("A la fin d'un tour, vous pourrez choisir :");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("-Continuer en appuyant sur une touche ");
                Console.WriteLine("-Mettre fin à la partie en tapant fin");
                Console.WriteLine("-Sauvegarder la partie");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Attention, vous ne pouvez sauvegarder qu'à partir du moment où chaque joueur a posé un mot ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n La fin de la partie se fera sinon automatiquement quand le sac sera vide");
                Console.ResetColor();


                Console.WriteLine("Appuyez sur un touche pour commencer");
                Console.ReadLine();






            }

        #endregion

    }
    
}
