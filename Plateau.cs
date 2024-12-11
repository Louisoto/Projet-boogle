using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_boogle
{
    internal class Plateau
    {
        #region Attributs
        private int taille;
        private De[] plateau;
        #endregion
        #region Constructeurs
        public Plateau(int tailleJeu)
        {
            this.taille = tailleJeu;
            this.plateau = new De[tailleJeu*tailleJeu];
            for (int i = 0; i < plateau.Length; i++)
            {
                plateau[i] = new De();
            }
        }
        #endregion
        #region Méthode
        public string toString()
        {
            string resul = "";
            for (int i = 0; i < this.taille; i++)
            {
                for (int j = 0; j < this.taille; j++)
                {
                    resul += this.plateau[i + j].ToString();
                }
                resul += "\n";
            }
            return resul;
        }
        #endregion
    }
}
