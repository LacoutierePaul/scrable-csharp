using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Scrabble_Paul_Lacoutiere
{
    public class Jeton
    {
        #region Attributs

        char lettre; 
        int score;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur qui permet d'initialiser un jeton, il sera définit par une lettre et un score
        /// </summary>
        /// <param name="lettre">attribut 1 du jeton : lettre</param>
        /// <param name="score">attribut 2 du jeton : score</param>
        public Jeton(char lettre, int score)
        {
            this.lettre = lettre;
            this.score = score;

        }

        #endregion

        #region Propriété

        /// <summary>
        /// Propriété pour avoir la lettre qui est sur un jeton
        /// </summary>
        public char Lettre
        {
            get { return lettre; }

        }

        /// <summary>
        /// propriété pour avoir le score qui est sur un jeton
        /// </summary>
        public int Score
        {
            get { return score; }
        }

        #endregion

        #region Méthode

        /// <summary>
        /// Fonction qui renvoie une chaine de caractère décrivant un jeton
        /// </summary>
        /// <returns>string description du jeton</returns>
        public string toString()
        {
            return " la lettre : " + lettre + " à un score de " + score;
        }

        #endregion 

    }
}
