using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scrabble_Paul_Lacoutiere
{
    public class Sac_jeton
    {
        #region Attributs
        List<Jeton> sacjeton = new List<Jeton>();

        #endregion

        #region Constructeur 

        /// <summary>
        /// Constructeur qui permet d'initialiser le sacjeton gràce à un fichier
        /// </summary>
        /// <param name="filename"> fichier qui va être utiliser pour créer le sacjeton. Ce fichier doit contenir les 
        /// différents jétons avec leur définition complète sur un même ligne</param>
        public Sac_jeton(string filename)
        {
            ReadFile(filename);
        }


        #endregion

        #region Propriété
        /// <summary>
        /// Propriété que permet d'avoir la valeur du sacjeton
        /// </summary>
        /// 

        public List<Jeton> Sacjeton
        {
            get { return this.sacjeton; }
        }  
        

        #endregion

        #region Méthode Read Et WriteFile
        /// <summary>
        /// Méthode permettant d'ajouter les jetons contenu dans un fichier dans le sacjeton
        /// </summary>
        /// <param name="filename"> fichier contenant les jetons</param>
        public void ReadFile(string filename)
        {
            try
            {
                StreamReader lesacjeton = new StreamReader(filename);
                string str = null;
                while ((str = lesacjeton.ReadLine()) != null)
                {
                    string[] jeton = str.Split(';');
                    char lettre = Convert.ToChar(jeton[0]);
                    int score = Convert.ToInt32(jeton[1]);
                    int nbroccurence = Convert.ToInt32(jeton[2]);
                    while (nbroccurence > 0)
                    {
                        sacjeton.Add(new Jeton(lettre, score));
                        nbroccurence -= 1;
                    }

                }
                lesacjeton.Close();

            }
            catch (Exception e)  // test si le fichier n'existe pas ou si erreur
            {
                Console.WriteLine(e.Message);
            }
        }


        /// <summary>
        ///méthode qui  Enregistre dans un fichier une instance de Sacjeton
        /// </summary>
        /// <param name="filename"> Endroit ou le fichier va etre enregistrer </param>
        public void WriteFile(string filename)
        {
            try
            {
                StreamWriter writer = new StreamWriter(filename);
                foreach (Jeton element in sacjeton)
                {
                    string a = "1";

                    writer.Write(element.Lettre + ";" + element.Score + ";" + a);
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

        #region Méthode

        /// <summary>
        /// Méthode  qui retire un jeton du sac aléatoirement et le renvoie
        /// </summary>
        /// <param name="r">random r</param>
        /// <returns> jeton qui a été rétire du sac</returns>
        public Jeton Retire_Jeton(Random r)
        {
            Jeton jeton = null;
            if (sacjeton.Count() != 0)
            {
                int jetonrandom = r.Next(0, sacjeton.Count());
                jeton = sacjeton[jetonrandom];

                sacjeton.Remove(jeton);
                //Console.WriteLine("La lettre " + jeton.Lettre + " a été rétiré du sac jeton ");

            }
            else Console.WriteLine("Le sac est vide on ne peut pas retirer de jeton");
            return jeton;
        }


        /// <summary>
        /// Fonction qui renvoie le nombre de jeton d'un sacjeton
        /// </summary>
        /// <returns>nombre de jeton</returns>
        public int NombreDejeton()
        {
            int nombrejeton = 0;
            if (sacjeton.Count() != 0)
            {
                nombrejeton = sacjeton.Count();
            }

            return nombrejeton;
        }





        /// <summary>
        /// Fonction qui affiche les jetons présents dans le sacjeton en fonction du caractère, son nombre de points et le nombre de duplicata
        /// </summary>
        /// <returns> retourne une chaine de caractère expliquant tout les jetons présents dans le sac actuellement</returns>
        public string toString()
        {
            string s = null;
            if (sacjeton.Count() != 0)
            {
                Console.WriteLine("Voici tous les jetons présents dans le sac :\n");
                for (int i = 0; i < sacjeton.Count(); i++)
                {
                    int occurence = 1;
                    int j = i;
                    
                    while (i != sacjeton.Count() - 1 && sacjeton[i].Lettre == sacjeton[i + 1].Lettre)
                    {
                        occurence += 1;
                        i++;
                    }
                    s = s + sacjeton[i].toString() + " et est présent " + occurence + " fois \n";
                 


                }

            }
            else Console.WriteLine("Le sac ne contient aucun jeton");

            return s;
        }
    }

    #endregion
}
