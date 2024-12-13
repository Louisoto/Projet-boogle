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
        private De[,] plateau;
        #endregion
        #region Constructeurs
        public Plateau(int tailleJeu)
        {
            this.taille = tailleJeu;
            this.plateau = new De[tailleJeu,tailleJeu];
            for (int i = 0; i < tailleJeu; i++)
            {
                for (int j = 0; j < tailleJeu; j++)
                {
                    plateau[i, j] = new De();
                }
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
                    resul += this.plateau[i,j].ToString();
                }
                resul += "\n";
            }
            return resul;
        }
        public bool Test_Plateau(string mot, Position[] posInvalides = null, Position posCourante = null,int compteur = 0)
        {
            if (mot == "")
            {
                return true;
            }
            else
            {
                if (posCourante == null)
                {
                    posInvalides = new Position[mot.Length];
                    for (int i = 0; i < this.plateau.GetLength(0); i++)
                    {
                        for (int j = 0; i < this.plateau.GetLength(1); j++)
                        {
                            if (this.plateau[i,j].Face_visible == mot[0])
                            {
                                posInvalides[compteur] = new Position(i,j);
                                return Test_Plateau(mot.Substring(1), posInvalides, new Position(i,j), compteur + 1);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            bool test = false;
                            Position posTesté = new Position(posCourante.X + i, posCourante.Y + j);
                            for (int k = 0; k < posInvalides.Length && !test; k++)
                            {
                                if (posTesté.X == posInvalides[i].X && posTesté.Y == posInvalides[j].Y)
                                {
                                    test = true;
                                }
                            }
                            if (!test && posTesté.X > 0 && posTesté.X < this.plateau.GetLength(0) && posTesté.Y > 0 && posTesté.Y > this.plateau.GetLength(1) && this.plateau[posTesté.X, posTesté.Y].Face_visible == mot[0])
                            {
                                posInvalides[compteur] = posTesté;
                                return Test_Plateau(mot.Substring(1), posInvalides, posTesté, compteur + 1);
                            }
                        }
                    }
                }
                return false;
            }
        }
        #endregion
    }
}
