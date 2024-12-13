using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        public bool Test_Plateau(string mot, int position = -1, List<int> posInvalides = new List<int>())
        {
            if (mot == "")
            {
                return true;
            }
            else
            {
                if (position == -1)
                {
                    for (int i = 0; i < this.plateau.Length; i++)
                    {
                        if (this.plateau[i].Face_visible == mot[0])
                        {
                            return Test_Plateau(mot.Substring(1), i, posInvalides.Add(i));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < posInvalides.Count; j++)
                    }
                }
            }
        }
        #endregion
    }
}
