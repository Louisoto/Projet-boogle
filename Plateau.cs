using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Projet_boogle
{
    internal class Plateau
    {
        #region Attributs
        private int taille;
        private De[,] plateau;
        #endregion

        #region Getters
        public int Taille
        {
            get { return taille; }
            set {
                Plateau nouveauPlateau = new Plateau(value);
                this.taille = nouveauPlateau.taille;
                this.plateau = nouveauPlateau.plateau;
            }
        }
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
            

            string message = "";
            for (int i = 0; i < (2 * this.taille + 5); i++)
            {
                message += "_";
            }
            message += "\n|\\";
            for (int i = 0; i < (2 * this.taille + 1); i++)
            {
                message += " ";
            }
            message += "/|\n";


            for (int i = 0; i < this.taille; i++)
            {
                message += "| -";
                for (int j = 0; j < this.taille; j++)
                {
                    message += "--";
                }
                message += " |\n| |";
                for (int j = 0; j < this.taille; j++)
                {
                    message +=  this.plateau[i, j].Face_visible;
                    message += "|";
                }
                message += " |\n";
            }
            message += "| -";
            for (int j = 0; j < this.taille; j++)
            {
                message += "--";
            }
            message += " |\n";


            message += "|/";
            for (int i = 0; i < (2 * this.taille + 1); i++)
            {
                message += " ";
            }
            message += "\\|\n";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            for (int i = 0; i < (2 * this.taille + 5); i++)
            {
                message += '\u203E';
            }
            message += "\n";

            return message;
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
                        for (int j = 0; j < this.plateau.GetLength(1); j++)
                        {
                            Position posTesté = new Position(i, j);
                            if (this.plateau[i, j].Face_visible == mot[0])
                            {
                                Position[] nouvelleListeInvalides = new Position[posInvalides.Length];
                                posInvalides.CopyTo(nouvelleListeInvalides, 0);
                                nouvelleListeInvalides[compteur] = posTesté;
                                if (Test_Plateau(mot.Substring(1), nouvelleListeInvalides, posTesté, compteur + 1))
                                {
                                    return true;
                                }
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
                                if (posInvalides[k] != null && posTesté.X == posInvalides[k].X && posTesté.Y == posInvalides[k].Y)
                                {
                                    test = true;
                                }
                            }
                            if (!test && 
                                posTesté.X >= 0 && posTesté.X < this.plateau.GetLength(0) && 
                                posTesté.Y >= 0 && posTesté.Y < this.plateau.GetLength(1) && 
                                this.plateau[posTesté.X, posTesté.Y].Face_visible == mot[0])
                            {
                                Position[] nouvelleListeInvalides = new Position[posInvalides.Length];
                                posInvalides.CopyTo(nouvelleListeInvalides, 0);
                                nouvelleListeInvalides[compteur] = posTesté;
                                if (Test_Plateau(mot.Substring(1), nouvelleListeInvalides, posTesté, compteur + 1))
                                {
                                    return true;
                                }
                            }
                            
                        }
                    }
                }
                return false;
            }
        }

        public void melanger()
        {
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    plateau[i, j].Lance(Program.random);
                }
            }
        }
        #endregion
    }
}
