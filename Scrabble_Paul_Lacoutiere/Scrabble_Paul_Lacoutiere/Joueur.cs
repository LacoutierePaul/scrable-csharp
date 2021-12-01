using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Scrabble_Paul_Lacoutiere
{
    public class Joueur
    {
        #region Attributs
        /// <summary>
        /// Initialisation de trois variables qui décrivent un joueur : son nom, son score et les mots qu'il a trouvé
        /// </summary>
        string nom;
        int score;
        List<string> motTrouve;
        List<Jeton> maincourante;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur qui permet d'initialiser un joueur avec son nom et un score à 0 et les mots trouvés à null
        /// </summary>
        /// <param name="nom">string avec le nom du Joueur</param>
        public Joueur(string nom)
        {
            while (nom == null || nom == "")
            {
                Console.WriteLine("Donnez un nom valide");
                nom = Console.ReadLine();

            }
            while (int.TryParse(nom, out int result))
            {
                Console.WriteLine("Un nom ne peut pas un nombre");
                nom = Console.ReadLine();
            }
            string nomUpper = nom.ToUpper();
            string nomLower = nom.ToLower();
            this.nom = nomUpper[0] + nomLower.Substring(1);
            this.score = 0;
            this.motTrouve = new List<string>();
            this.maincourante = new List<Jeton>(7);
        }

        /// <summary>
        /// Constructeur qui permet d'initialiser un joueur avec tout ses attribut à partir d'un fichier
        /// </summary>
        /// <param name="filename">fichier dans lequel on va lire les informations du joueur</param>
        /// <param name="c">permet de différencier le constructeur sans fichier de celui avec fichier</param>
        public Joueur(string filename, char c)
        {
            this.motTrouve = new List<string>();
            this.maincourante = new List<Jeton>(7);
            Readfile(filename);
        }


        /// <summary>
        /// Fonction qui lit un fichier et créer un joueur avec ses informations
        /// </summary>
        /// <param name="filename">fichier ou on lit les informations d'un joueur</param>
        public void Readfile(string filename)
        {
            try
            {
                StreamReader joueur = new StreamReader(filename);
                string str = null;
                str = joueur.ReadLine(); //La première ligne correspond au nom
                this.nom = str;

                str = joueur.ReadLine();  //la deuxième ligne correspond au score
                this.score = Convert.ToInt32(str);

                str = joueur.ReadLine();  //La troisième ligne correspond au mot séparé par un virgule
                string[] lesmots = str.Split(';');
                foreach (string element in lesmots)
                {
                    motTrouve.Add(element); //on ajoute chaque mot dans la liste des mots trouvés
                }

                while ((str = joueur.ReadLine()) != null) //toutes les lignes restantes sont les jetons
                {
                    string[] jeton = str.Split(';');
                    char lettre = Convert.ToChar(jeton[0]);
                    int score = Convert.ToInt32(jeton[1]);
                    maincourante.Add(new Jeton(lettre, score));
                }
                joueur.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region Propriétés
        /// <summary>
        /// Propriété main courante ,get et set
        /// </summary>
        public List<Jeton> Maincourante
        {
            get { return maincourante; }
            set { maincourante = value; }
        }

        /// <summary>
        /// Propriété qui retourne le nom
        /// </summary>
        public string Nom
        {
            get { return this.nom; }
        }

        /// <summary>
        /// propriété qui retourne le score
        /// </summary>
        public int Score
        {
            get { return this.score; }
        }
        #endregion

        #region Méthode

        /// <summary>
        /// Méthode d'instance qui ajoute un mot dans la liste des mots déja trouvé d'un joueur
        /// </summary>
        /// <param name="mot"> string qui va être ajouté à la liste des mots</param>
        public void Add_mot(string mot)
        {
            if (mot != null)
            {
                string motUpper = mot.ToUpper();
                string motLower = mot.ToLower();
                mot = motUpper[0] + motLower.Substring(1);
                motTrouve.Add(mot);
            }
        }


        /// <summary>
        ///Méthode d'instance qui ajoute une valeur au score total du joueur
        /// </summary>
        /// <param name="val">valeur à ajouter </param>
        public void Add_score(int val)
        {
            score += val;
        }


        /// <summary>
        /// Méthode d'instance qui permet d'ajouter 
        /// </summary>
        /// <param name="monJeton"></param>
        public void Add_Main_Courante(Jeton monJeton)
        {
            if (maincourante.Count < 7)
            {
                if (monJeton != null)
                {
                    maincourante.Add(monJeton);
                }
                else Console.WriteLine("Le jeton est nul car le sac doit etre vide");
            }
            else Console.WriteLine("La main est déja pleine");


        }

        /// <summary>
        /// Méthode d'instance qui permet de vérifier si un joueur a dans sa main un jeton contenant la lettre passé en paramètre
        /// </summary>
        /// <param name="lettre">lettre dont un cherche le jeton correspondant dans la main</param>
        /// <returns> renvoie le jeton correspondant à la lettre</returns>
        public Jeton ExisteMainCourante(char lettre)  
        {
            Jeton existe = null;
            foreach (Jeton element in maincourante)
            {
                if (element.Lettre == lettre) existe = element;

            }


            return existe;

        }


        /// <summary>
        /// Méthode d'instance qui enleve un jeton de la main du joueur
        /// </summary>
        /// <param name="monJeton">jeton qui va être enleve de de la main</param>
        public void Remove_Main_Courante(Jeton monJeton)
        {

            maincourante.Remove(monJeton);
        }



        /// <summary>
        /// Méthode d'instance qui permet l'Affichage de la main courante
        /// </summary>
        public void AfficherMainCourante()
        {
            if (maincourante.Count() == 0)
            {
                Console.WriteLine("La main est vide ");
            }
            else
            {
                Console.Write("La main de " + nom + " est : ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("|");

                foreach (Jeton element in maincourante)
                {
                    if (element != null)
                    {
                        Console.Write(element.Lettre + "(" + element.Score + ")" + "|");
                    }
                    else Console.WriteLine("l'élement est nul");
                }
                Console.ResetColor();
                Console.WriteLine("");
            }

        }


        /// <summary>
        /// Méthode d'instance qui test si le joueur à les lettres néccessaires pour poser un mot
        /// </summary>
        /// <param name="mot"> lettre</param>
        /// <returns> renvoie true ou false si il a les jetons nécessaires dans sa main </returns>
        public bool TestLettres(string mot) // Test si le joueur à les lettres nécessaire pour poser le mot

        {
            int compteur = 0;
            bool possible = false;
            mot = mot.ToUpper();
            List<Jeton> main = new List<Jeton>();
            foreach (Jeton element in maincourante)
            {
                main.Add(element);
            }
            for (int i = 0; i < mot.Length; i++)
            {
                if (ExisteMainCourante(mot[i]) != null)
                {
                    main.Remove(ExisteMainCourante(mot[i]));
                    compteur = compteur + 1;
                }

            }
            
            if (compteur == mot.Length)
            {
                possible = true;
            }


            return possible;
        }





        /// <summary>
        /// Méthode d'instance qui envoie une chaine de caractère avec les informations d'un joueur soit son nom, son score et les mots qu'il à trouvé.
        /// </summary>
        /// <returns>la chaine de caractère permettant la description sous forme de string </returns>
        public string toString()
        {
            string toutlesmots = null;
            if (motTrouve.Count != 0)
            {
                foreach (string element in motTrouve)
                {
                    toutlesmots = toutlesmots + element + ";";
                }
            }
            return nom + " a finit avec " + score + " points et il a trouvé : " + toutlesmots;
        }



        /// <summary>
        ///Méthode d'instance qui écrit dans un fichier toutes les informations d'un joueur
        /// </summary>
        /// <param name="filename"> nom et endroit du fichier </param>
        public void WriteFile(string filename)
        {
            try
            {
                StreamWriter writer = new StreamWriter(filename);
                writer.WriteLine(nom);
                writer.WriteLine(score);
                for (int i = 0; i < motTrouve.Count - 1; i++)
                {
                    writer.Write(motTrouve[i] + ";");
                }
                writer.WriteLine(motTrouve[motTrouve.Count - 1]);
                for (int j = 0; j < maincourante.Count; j++)
                {
                    writer.WriteLine(maincourante[j].Lettre + ";" + maincourante[j].Score);

                }
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        #endregion
    }
}
