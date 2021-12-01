using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Scrabble_Paul_Lacoutiere
{
    public class Dictionnaire
    {

        #region Attributs
        List<string[]> dico = new List<string[]>();
        string langue;
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur qui va initaliser un dictionnaire à partir d'un fichier
        /// </summary>
        /// <param name="filename">nom et chemin du fichier à lire</param>
        /// <param name="langue">langue du dictionnaire</param>
        public Dictionnaire(string filename, string langue)
        {
            this.langue = langue;
            ReadFile(filename);
        }

        #endregion

        #region Propriété

        /// <summary>
        /// Propriété permettant d'accéder a l'attribut dico du dictonnaire
        /// </summary>
        public List<string[]> Dico
        {
            get { return dico; }
        }

        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode permettant de lire un fichier pour l'initialisation du dictionnaire
        /// </summary>
        /// <param name="filename">nom et chemin du fichier que l'on veut lire</param>
        public void ReadFile(string filename)
        {
            try
            {
                StreamReader dictionnaire = new StreamReader(filename);
                string str = null;
                while ((str = dictionnaire.ReadLine()) != null)
                {

                    string[] tab = str.Split(' ');
                    int result;
                    bool b = int.TryParse(tab[0], out result);
                    if (b != true)
                    {
                        dico.Add(tab);
                    }

                }
                dictionnaire.Close();

            }
            catch (Exception e)  // test si le fichier n'existe pas ou si erreur
            {
                Console.WriteLine(e.Message);
            }
        }


   
        /// <summary>
        /// Fonction qui fait une recherche dichotomique pour tester si un mot appartient au dictionnaire
        /// </summary>
        /// <param name="debut">0 de base</param>
        /// <param name="fin">nombre de mot de la même taille que le mot que l'on cherche</param>
        /// <param name="mot"> mot dont on veut vérifier l'appartenance au dictionnaire</param>
        /// <returns>vrai ou faux si le mot appartient ou non au dictionnaire</returns>
        public bool RechercheDichoRecursif(int debut, int fin, string mot)
        {
            if (mot != null && mot.Length > 1)
            {
                mot = mot.ToUpper();
                int milieu = ((debut + fin) / 2);
                if (debut > fin)

                {
                    Console.WriteLine("Le mot " + mot + " n'existe pas ");
                    return false;
                }
                else
                {
                    string[] tab = dico[mot.Length - 2];
                    if (tab[milieu].CompareTo(mot) == 0) return true;
                    else
                    {
                        if (mot.CompareTo(tab[milieu]) ==1) return RechercheDichoRecursif(milieu + 1, fin, mot);
                        else return RechercheDichoRecursif(debut, milieu - 1, mot);
                    }
                }


            }
            else return false;

        }


        /// <summary>
        /// Méthode qui renvoie une chaine de caractère décrivant un dictionnaire c'est à dire son nombre de mot par lettre et sa langue
        /// </summary>
        /// <returns>string de la description du dico</returns>
        public string toString()
        {
            string s = null;
            s = "la langue de ce dictionnaire est  : " + langue + "\n";
            for (int i = 0; i < dico.Count(); i++)
            {
                int j = dico[i].Length;
                s += "Il y a  " + j + " mots à " + (i + 2) + " lettres\n";

            }
            return s;
        }


        #endregion
    }
}
