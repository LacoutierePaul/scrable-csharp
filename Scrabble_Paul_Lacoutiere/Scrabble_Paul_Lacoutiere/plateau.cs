using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scrabble_Paul_Lacoutiere
{
    public class plateau
    {
        #region Attributs

        char[,] matricelettre;
        string[,] matricecouleur;
        Sac_jeton sacjeton;

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur qui initalise un plateau à partir d'un fichier
        /// </summary>
        /// <param name="filename">fichier  contenant le plateau </param>
        public plateau(string filename)
        {
            matricelettre = new char[15, 15];
            ReadFile(filename);
            matricecouleur = new string[15, 15];
            ColorationMatrice(matricecouleur);
            sacjeton = new Sac_jeton("Jetons.txt");
        }

        /// <summary>
        /// Constructeur qui initialise un nouveau plateau vide
        /// </summary>
        public plateau()
        {
            sacjeton = new Sac_jeton("Jetons.txt");
            matricelettre = new char[15, 15];
            matricecouleur = new string[15, 15];
            ColorationMatrice(matricecouleur);// création de la matrice coloré
            for (int i = 0; i < matricelettre.GetLength(0); i++) //initialisation vide
            {
                for (int j = 0; j < matricelettre.GetLength(1); j++)
                {
                    matricelettre[i, j] = '_';
                }
            }
            matricelettre[7, 7] = '*';
        }


        /// <summary>
        /// Méthode d'instance qui ajoute à la matrice lettre les données d'un fichier 
        /// </summary>
        /// <param name="filename">fichier qui va permettre d'instancer la matricelettre</param>
        public void ReadFile(string filename)
        {
            matricelettre = new char[15, 15];
            try
            {
                StreamReader instancetableau = new StreamReader(filename);
                string str = null;
                int i = 0;
                while ((str = instancetableau.ReadLine()) != null)
                {
                    string[] ligne = str.Split(';');
                    // if (ligne.Length == 14)// cas mauvais taille de matrice à vérifier
                    // {
                    for (int j = 0; j < ligne.Length; j++)
                    {
                        char lettre = Convert.ToChar(ligne[j]);
                        this.matricelettre[i, j] = lettre;

                    }
                    i++;

                    // }
                    //  else Console.WriteLine("Le fichier n'est pas valable, toutes les lignes ne font pas 15 caractères");
                }
                instancetableau.Close();

            }
            catch (Exception e)  // test si le fichier n'existe pas ou si erreur
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region Propriété

        /// <summary>
        /// Propriété pour récuperer ou changer les informations de matricelettre
        /// </summary>
        public char[,] Matricelettre
        {
            get { return this.matricelettre; }
            set { matricelettre = value; }
        }

        #endregion

        #region Méthode de Sauvegarder fichier

        /// <summary>
        ///Méthode d'instance qui permet d'enregistrer une instance de plateau dans un fichier 
        /// </summary>
        /// <param name="filename"> Nom du fichier avec la sauvegarde </param>
        public void WriteFile(string filename)
        {
            try
            {
                StreamWriter writer = new StreamWriter(filename);
                for (int i = 0; i < matricelettre.GetLength(0); i++)
                {
                    for (int j = 0; j < matricelettre.GetLength(1) - 1; j++)
                    {
                        writer.Write(matricelettre[i, j] + ";");
                    }
                    writer.Write(matricelettre[i, 14]);
                    writer.WriteLine();
                }
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region Méthode de creation et Affichage du plateau

        /// <summary>
        /// Méthode qui permet de créer un tableau avec la couleur de la case que l'on voudra pour l'affichage du plateau
        /// </summary>
        /// <param name="matricecouleur">matrice que permet de stocker les couleurs</param>
        public void ColorationMatrice(string[,] matricecouleur)
        {
            for (int i = 0; i < matricecouleur.GetLength(0); i++)
            {
                for (int j = 0; j < matricecouleur.GetLength(1); j++)
                {
                    if (((i == 0 || i == 14) && (j == 0 || j == 7 || j == 14)) || (i == 7 && (j == 0 || j == 14))) //Coloration des cases rouges
                    {
                        matricecouleur[i, j] = "rouge";
                    }
                    else if ((i == 1 || i == 13) && (j == 5 || j == 9) || (i == 5 || i == 9) && (j == 1 || j == 5 || j == 9 || j == 13))//cases bleus
                    {
                        matricecouleur[i, j] = "bleu";
                    }
                    else if ((i == 0 || i == 7 || i == 14) && (j == 3 || j == 11) || ((i == 2 || i == 12) && (j == 6 || j == 8)) || ((j == 0 || j == 14 || j == 7) && (i == 3 || i == 11)) || (i == 6 || i == 8) && (j == 6 || j == 8 || j == 2 || j == 12))
                    {
                        matricecouleur[i, j] = "cyan";
                    }


                    else if ((i == j) || (j == 14 - i))
                    {
                        matricecouleur[i, j] = "rose";
                    }
                    else matricecouleur[i, j] = "vert";


                }
            }
        }




        /// <summary>
        /// Fonction qui renvoie une chaine de caractère décrivant le plateau
        /// </summary>
        /// <returns> chaine de caractère avec espace entre chaque </returns>
        public string toString()
        {
            string s = null;
            for (int i = 0; i < matricelettre.GetLength(0); i++)
            {
                for (int j = 0; j < matricelettre.GetLength(1) - 1; j++)
                {
                    s += matricelettre[i, j] + ";";

                }
                s += matricelettre[i, 14];
                s += "\n";
            }
            return s;
        }

        /// <summary>
        /// Affiche le plateau avec les couleurs
        /// </summary>
        public void AfficherPlateau()
        {
            Console.WriteLine("   1  2  3  4  5  6  7  8  9 10 11 12 13 14 15"); //Permet de faire la légende des colonne en haut
            for (int i = 0; i < matricelettre.GetLength(0); i++) //legénde des lignes à gauche
            {
                if (i < 9)
                {
                    Console.Write(i + 1 + " ");
                }
                else Console.Write(i + 1);


                for (int j = 0; j < matricelettre.GetLength(1); j++)
                {

                    switch (matricecouleur[i, j])
                    {
                        case "rouge":
                            Console.BackgroundColor = ConsoleColor.Red;
                            break;
                        case "vert":
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case "cyan":
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case "rose":
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case "bleu":
                            Console.BackgroundColor = ConsoleColor.Blue;
                            break;


                    }
                    Console.Write(" " + matricelettre[i, j] + " ");
                    Console.ResetColor();

                }
                if (i < 9) //légende des lignes à droite
                {
                    Console.Write(i + 1 + " ");
                }
                else Console.Write(i + 1);
                Console.WriteLine("");
            }
            Console.WriteLine("   1  2  3  4  5  6  7  8  9 10 11 12 13 14 15"); //Légende colonne en bas
            //légende :
            Console.WriteLine("");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("  ");
            Console.ResetColor();
            Console.Write(" : Mot triple ");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("  ");
            Console.ResetColor();
            Console.Write(" : Mot double ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("  ");
            Console.ResetColor();
            Console.Write(" : Lettre triple ");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("  ");
            Console.ResetColor();
            Console.Write(" : Lettre double ");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("  ");
            Console.ResetColor();
            Console.Write(" : Lettre simple ");
            Console.WriteLine(" ");


        }


        #endregion

        #region Méthode Test Plateau

        /// <summary>
        /// Vérifie qu'il y a assez d'espace vide pour mettre le mot du joueur.
        /// </summary>
        /// <param name="mot"> mot a tester dans le tableau</param>
        /// <param name="ligne">ligne de départ</param>
        /// <param name="colonne">colonne de départ</param>
        /// <param name="direction">direction du mot</param>
        /// <returns> vrai ou faux si il y a la place ou pas de mettre le mot</returns>
        public bool Place(string mot, int ligne, int colonne, char direction)
        {

            bool possible = false;
            int compteur = 0;
            //Vérification de la place d'un mot
            if (direction == 'h')
            {
                for (int i = colonne; i < 15; i++)
                {
                    if (matricelettre[ligne, i] == '_') compteur++;

                }
            }
            else if (direction == 'v')
            {
                for (int j = ligne; j < 15; j++)
                {
                    if (matricelettre[j, colonne] == '_') compteur++;
                }


            }
            if (compteur >= mot.Length) possible = true;


            return possible;
        }





        /// <summary>
        ///Méthode d'instance qui Prend en paramètre la suite de caractère du joueur et renvoie le mot total 
        /// </summary>
        /// <param name="mot">suite de caractère du joueur</param>
        /// <param name="ligne">ligne de départ</param>
        /// <param name="colonne">colonne de départ</param>
        /// <param name="direction">h ou v direction du mot</param>
        /// <returns>le mot final que le joueur à finalement voulu utiliser</returns>
        public string MotFinal(string mot, int ligne, int colonne, char direction)
        {

            // ligne = ligne - 1;
            //  colonne = colonne - 1;

            string motvoulu = null;
            int longueur = mot.Length;
            int i = 0;
            if ((direction == 'h' || direction == 'v') && mot != null && colonne >= 0 && ligne >= 0)
            {
                bool place = Place(mot, ligne, colonne, direction);
                if (place == false) Console.WriteLine("Il n'y a pas le place de mettre le mot");
                else
                {
                    while (longueur != 0)
                    {
                        if (matricelettre[ligne, colonne] == '_')
                        {
                            motvoulu += mot[i];
                            longueur = longueur - 1;
                            i++;
                        }
                        else
                        {
                            motvoulu += matricelettre[ligne, colonne];
                        }

                        if (direction == 'h') colonne++;
                        if (direction == 'v') ligne++;

                    }
                    if (direction == 'h' && colonne < 15)
                    {
                        int j = 0;
                        while ((colonne + j < 15) && matricelettre[ligne, colonne + j] != '_')
                        {
                           // Console.WriteLine(matricelettre[ligne, colonne + j]);
                            motvoulu += matricelettre[ligne, colonne + j];
                            j++;
                        }
                    }
                    else if (direction == 'v' && ligne < 15)
                    {
                        int j = 0;
                        while ((ligne + j < 15) && matricelettre[ligne + j, colonne] != '_')
                        {
                           // Console.WriteLine(matricelettre[ligne + j, colonne]);
                            motvoulu += matricelettre[ligne + j, colonne];
                            j++;
                        }
                    }

                }
            }
            Console.WriteLine("Le mot voulu est : " + motvoulu);
            return motvoulu;
        }


        /// <summary>
        ///Méthode d'instance qui test si le mot ne sera pas collé à un autre dans le sens, soit qu'il y a un espace devant et derriere
        /// </summary>
        /// <param name="motfinal">mot joueur par le joueur</param>
        /// <param name="ligne">ligne de départ</param>
        /// <param name="colonne">colonne de départ</param>
        /// <param name="direction">direction de la pose du mot</param>
        /// <returns>vrai ou faux si il y a bien un espace devant et derriere</returns>
        public bool EspaceDevantDerriere(string motfinal, int ligne, int colonne, char direction)
        {
            bool espace = true;
            if (direction == 'h') // On vérifie qu'il y ai un espace devant et dérriere le mot , ou début/fin du tableau
            {
                if (colonne - 1 != -1)
                {
                    if (matricelettre[ligne, colonne - 1] != '_') espace = false;
                }
                if (colonne + motfinal.Length-1!= 14)
                {
                    if (matricelettre[ligne, colonne + motfinal.Length ] != '_') espace = false;
                }
            }
            else if (direction == 'v')
            {
                if (ligne - 1 != -1)
                {
                    if (matricelettre[ligne - 1, colonne] != '_') espace = false;
                }
                if (ligne + motfinal.Length-1!= 14)
                {
                    if (matricelettre[ligne + motfinal.Length, colonne] != '_') espace = false;
                }
            }


            return espace;

        }

        /// <summary>
        ///Méthode d'instance qui Converti un caractèreen paramètre en un jeton ayant cette lettre
        /// </summary>
        /// <param name="caractère"> lettre </param>
        /// <returns>Jeton ayant la meme lettre que le caractère</returns>
        public Jeton CharVersJeton(char caractère)
        {
            Jeton jeton = null;
            foreach (Jeton element in sacjeton.Sacjeton)
            {
                if (element.Lettre == caractère)
                {
                    jeton = element;
                }
            }
            return jeton;
        }



        /// <summary>
        /// Méthode d'instance  qui calcule le score d'un mot passé par le jouer et tout les mots qu'il complète
        /// </summary>
        /// <param name="ligne">ligne de départ</param>
        /// <param name="colonne">colonne de départr</param>
        /// <param name="mot">mot dont on veut calculer le score</param>
        /// <param name="direction">direction du mot</param>
        /// <returns>renvoie le score</returns>
        public int CalculerscoreFinal(int ligne, int colonne, string mot, char direction, List<string> toutlesmots) // Avant de remplir le mot, tester si la case est vide si vide ajouter la lettre multiplicative ect
        {
            int score = 0;
            int i = 0;
            int rouge = 0;
            int rose = 0;
            while (i < mot.Length)
            {
                foreach (Jeton element in sacjeton.Sacjeton)
                {
                    if (element.Lettre == mot[i])
                    {
                        if (direction == 'h')
                        {
                            if (matricelettre[ligne, colonne + i] == '_') //Test si la case est vide, si la case est vide on va regarder 
                            {                                          // la couleur afin de voir si la lettre est simple double ou triple
                                switch (matricecouleur[ligne, colonne + i])
                                {
                                    case "vert":
                                        score = score + element.Score;
                                        break;
                                    case "cyan":
                                        score = score + (2 * element.Score);
                                        break;
                                    case "bleu":
                                        score += 3 * element.Score;
                                        break;
                                    case "rouge":
                                        score += element.Score;
                                        rouge++;
                                        break;
                                    case "rose":
                                        score += element.Score;
                                        rose = rose + 1;
                                        break;

                                }
                            }
                            else score += element.Score;
                        }
                        else
                        {
                            if (matricelettre[ligne + i, colonne] == '_') //Test si la case est vide, si la case est vide on va regarder 
                            {                                          // la couleur afin de voir si la lettre est simple double ou triple
                                switch (matricecouleur[ligne + i, colonne])
                                {
                                    case "vert":
                                        score = score + element.Score;
                                        break;
                                    case "cyan":
                                        score = score + (2 * element.Score);
                                        break;
                                    case "bleu":
                                        score += 3 * element.Score;
                                        break;
                                    case "rouge":
                                        score += element.Score;
                                        break;
                                    case "rose":
                                        score += element.Score;
                                        rose++;
                                        break;

                                }
                            }
                            else score += element.Score;
                        }
                        i++;
                        break;
                    }
                }
            }

            while (rouge != 0) // on multiplie le score du mot par trois
            {
                score = score * 3;
                rouge--;

            }
            while (rose != 0)
            {
                score = score * 2;
                rose--;
            }


            if (toutlesmots != null)
            {
                score += CalculerScoreMotsComplete(toutlesmots); // Attention il manque le cas *2 ou *3 pour ce mot
            }

            return score;
        }



        /// <summary>
        /// Méthode d'instance qui calcul le score de tout les mots complétés 
        /// </summary>
        /// <param name="mots">liste de mot complète</param>
        /// <returns>score des mots complète</returns>
        public int CalculerScoreMotsComplete(List<string> mots)
        {
            int score = 0;
            if (mots != null)
            {
                foreach (string element in mots)
                {
                    for (int i = 0; i < element.Length; i++)
                    {
                        foreach (Jeton jeton in sacjeton.Sacjeton)
                        {
                            if (jeton.Lettre == element[i])
                            {
                                score += jeton.Score;
                                break;
                            }
                        }
                    }
                }
            }
            return score;
        }

        /// <summary>
        ///Méthode d'instance qui cherche tout les mots complétés par le joueur lorsqu'il pose son nom
        /// </summary>
        /// <param name="motfinal">mot du joueur</param>
        /// <param name="ligne">ligne ou le mot du joueur est placé</param>
        /// <param name="colonne">colonne ou le mot du joueur est placé</param>
        /// <param name="direction">direction du mot</param>
        /// <returns> la liste des mots complétés</returns>
        public List<string> MotCompleter(string motfinal, int ligne, int colonne, char direction)
        {
            List<string> Lesmots = new List<string>();
            int lignefinal = ligne;
            int colonnefinal = colonne;
            if (direction == 'h')
            {
                colonnefinal += motfinal.Length;
                if (ligne + 1 != 15 && ligne - 1 != -1)
                {
                    for (int i = colonne; i < colonnefinal; i++)
                    {
                        if ((matricelettre[ligne + 1, i] == '_' && matricelettre[ligne - 1, i] == '_') || matricelettre[ligne, i] != '_')   // On test pour chaque colonne si au dessus et au dessous sont nul, sinon on essaye
                        {                                                                    // de récuperer le mot présent sur cette colonne

                        }
                        else
                        {
                            string mot = null;
                            if (matricelettre[ligne - 1, i] != '_') // Le mot commence au dessus, donc on remonte jusqua un espace vide ou la ligne 0;
                            {
                                int j = 0;
                                while (ligne - j != 0 && matricelettre[ligne - j - 1, i] != '_') // On cherche j l'indice max ou il y a une lettre au dessus
                                {
                                    j++;
                                }
                                while (ligne - j != 14 && matricelettre[ligne - j, i] != '_') //puis on parcous la ligne de haut en bas jusqua trouver un espace vide (fin du mot) ou fin du tableau
                                {
                                    mot += matricelettre[ligne - j, i];
                                    j--;
                                    if (j == 0) //si j vaut 0, on ajoute la lettre du mot qui va etre placé après
                                    {
                                        mot += motfinal[i - colonne];
                                        j--;
                                    }

                                }
                                Lesmots.Add(mot);
                            }
                            else //le mot est donc en dessous on descend jusqu'a trouver un espace ou la fin du tableau
                            {
                                mot += motfinal[i - colonne];
                                int j = 1;
                                while (ligne + j != 15 && matricelettre[ligne + j, i] != '_') //puis on parcous la ligne de haut en bas jusqua trouver un espace vide (fin du mot) ou fin du tableau
                                {
                                    mot += matricelettre[ligne + j, i];
                                    j++;
                                }
                                Lesmots.Add(mot);

                            }
                        }
                    }
                }
            }
            else
            {
                if (colonne + 1 != 15 && colonne - 1 != -1)
                {
                    lignefinal += motfinal.Length;
                    for (int i = ligne; i < lignefinal; i++)
                    {
                        if ((matricelettre[i, colonne + 1] == '_' && matricelettre[i, colonne - 1] == '_') || matricelettre[i, colonne] != '_')   // On test pour chaque colonne si au dessus et au dessous sont nul, sinon on essaye
                        {                                                                    // de récuperer le mot présent sur cette colonne
                        }
                        else
                        {
                            string mot = null;

                            if (matricelettre[i, colonne - 1] != '_') // Le mot commence au dessus, donc on remonte jusqua un espace vide ou la ligne 0;
                            {
                                int j = 0;
                                while (colonne - j != 0 && matricelettre[i, colonne - j - 1] != '_') // On cherche j l'indice max ou il y a une lettre au dessus
                                {
                                    j++;
                                }
                                while (colonne - j != 15 && matricelettre[i, colonne - j] != '_') //puis on parcous la ligne de haut en bas jusqua trouver un espace vide (fin du mot) ou fin du tableau
                                {
                                    mot += matricelettre[i, colonne - j];
                                    j--;
                                    if (j == 0) //si j vaut 0, on ajoute la lettre du mot qui va etre placé après
                                    {
                                        mot += motfinal[i - ligne];
                                        j--;
                                    }
                                }
                                Lesmots.Add(mot);
                            }
                            else //le mot est donc en dessous on descend jusqu'a trouver un espace ou la fin du tableau
                            {
                                mot += motfinal[i - ligne];
                                int j = 1;
                                while (colonne + j != 15 && matricelettre[i, colonne + j] != '_') //puis on parcous la ligne de haut en bas jusqua trouver un espace vide (fin du mot) ou fin du tableau
                                {
                                    mot += matricelettre[i, ligne + j];
                                    j++;
                                }
                                Lesmots.Add(mot);

                            }
                        }
                    }
                }
            }
            return Lesmots;

        }

        /// <summary>
        ///Méthode d'instance qui test si une liste de mots appartient au dictionnaire
        /// </summary>
        /// <param name="mots">liste de mot à tester </param>
        /// <param name="dico">dictionnaire ou on recherche les mots</param>
        /// <returns>vrai ou faux si tout les mots appartiennent au dictionnaire</returns>
        public bool MotsDictionnaires(List<string> mots, Dictionnaire dico)
        {
            bool b = true;
            foreach (string element in mots)
            {

                if (dico.RechercheDichoRecursif(0, dico.Dico[element.Length - 2].Length, element) == false)
                {
                    b = false;
                }
            }
            return b;
        }





        /// <summary>
        /// Méthode d'instancequi vérifie si un mot est éligible ou pas à etre posé sur le plateau, si il est éligible, les lettrees seront posés sur le plateau
        /// les mots trouvés et  le score seront ajoutés au joueur
        /// </summary>
        /// <param name="mot">Le mot passé en paramètre est composé seulement des lettres mises par l'utilisateur</param>
        /// <param name="ligne">la ligne de départ du mot</param>
        /// <param name="colonne">la colonne de départ du mot</param>
        /// <param name="direction">la direction soit horizontal(h) soit vertical(v)</param>
        /// <param name="dico">le dictionnaire pour vérifier l'appartenance du mot</param>
        /// <param name="joueur">le joueur qui pose le mot</param>
        /// <returns>vrai ou faux si le mot à pu etre posé</returns>
        public bool Test_Plateau(string mot, int ligne, int colonne, char direction, Dictionnaire dico, Joueur joueur)
        {
            string motfinal = null;
            bool possible = false;
            colonne = colonne - 1;
            ligne = ligne - 1;
            int score = 0;
            List<string> motscomplete = new List<string>();

            if (ligne >= 0 && ligne <= 14 && colonne >= 0 && colonne <= 14)
            {
                mot = mot + "";

                mot = mot.ToUpper();
                if (direction != 'h' && direction! != 'v')
                {
                    Console.WriteLine("Direction non valide");

                }
                else
                {
                    motfinal = MotFinal(mot, ligne, colonne, direction); // remplace les lettres donnés par le joueur par le mot entier avec les lettres déja présentes sur le plateau
                                                                         // motfinal.ToUpper();
                    motscomplete = MotCompleter(motfinal, ligne, colonne, direction);
                    if (dico.RechercheDichoRecursif(0, dico.Dico[motfinal.Length - 2].Length, motfinal) == true)
                    {


                        if (motfinal != null && (motfinal.Length != mot.Length || motscomplete.Count != 0))
                        {

                            if (mot != null && motfinal.Length <= 15)
                            {

                                int longueur = motfinal.Length - 1;
                                motfinal = motfinal.ToUpper();
                                Console.WriteLine("Le mot existe");

                                if (direction == 'h')
                                {
                                    int finligne = longueur + colonne;
                                    if (finligne > 14)
                                    {
                                        Console.WriteLine("Le mot est dépasse de la grille");
                                    }
                                    else
                                    {

                                        if (joueur.TestLettres(mot))
                                        {
                                            if (EspaceDevantDerriere(motfinal, ligne, colonne, direction))
                                            {

                                                if (MotsDictionnaires(motscomplete, dico))
                                                {
                                                    possible = true;
                                                    Console.WriteLine("Le mot va être posé");
                                                    score = CalculerscoreFinal(ligne, colonne, motfinal, direction, motscomplete);
                                                    int j = 0;
                                                    for (int i = 0; i <= longueur; i++)
                                                    {
                                                        if (matricelettre[ligne, colonne + i] == '_')
                                                        {
                                                            matricelettre[ligne, colonne + i] = mot[j];
                                                            joueur.Remove_Main_Courante(joueur.ExisteMainCourante(mot[j])); // ON enleve les lettres utilisé de la main courante
                                                            j = j + 1; ;
                                                        }
                                                    }
                                                }
                                                else Console.WriteLine("Tout les mots n'appartiennet pas au dictionnaire");
                                            }
                                            else Console.WriteLine("Il n'y a pas assez d'espace devant derriere");

                                        }
                                        else Console.WriteLine("Vous n'avez pas les lettres nécessaires");
                                    }

                                }
                                else if (direction == 'v')
                                {

                                    int fincolonne = ligne + longueur;
                                    if (fincolonne > 14)
                                    {
                                        Console.WriteLine("Le mot dépasse la grille");
                                    }
                                    else
                                    {
                                        if (joueur.TestLettres(mot))
                                        {
                                            if (EspaceDevantDerriere(motfinal, ligne, colonne, direction))
                                            {

                                                if (MotsDictionnaires(motscomplete, dico))
                                                {
                                                    possible = true;
                                                    score = CalculerscoreFinal(ligne, colonne, motfinal, direction, motscomplete);
                                                    Console.WriteLine("Le mot va être posé");
                                                    int j = 0;
                                                    for (int i = 0; i <= longueur; i++)
                                                    {
                                                        if (matricelettre[ligne + i, colonne] == '_')
                                                        {
                                                            matricelettre[ligne + i, colonne] = mot[j];
                                                            joueur.Remove_Main_Courante(joueur.ExisteMainCourante(mot[j])); // ON enleve les lettres utilisé de la main courante
                                                            j = j + 1; ;
                                                        }
                                                    }
                                                }
                                                else Console.WriteLine("Tout les mots n'appartiennent pas au dictionnaire");
                                            }
                                            else Console.WriteLine("Il y a un mot devant ou derriere");
                                        }
                                        else Console.WriteLine(" Vous n'avez pas les jetons requis");

                                    }
                                }
                            }
                        }
                        else Console.WriteLine("le mot ne prend aucune lettre déja présente sur le terrain");

                    }
                    else Console.WriteLine("Mot non existant");
                }
            }
            else Console.WriteLine("Position des lignes ou colonnes non valides ");



            if (possible == true)
            {
                joueur.Add_mot(motfinal);
                foreach (string Unmot in motscomplete)
                {
                    joueur.Add_mot(Unmot);
                }
                joueur.Add_score(score);
            }

            return possible;
        }


        #endregion

    }
}
