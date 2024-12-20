using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("ProjetTestsUnitaires")]

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

        #region Propriété
        public De ElemPlateau(int i, int j) { return plateau[i,j]; }
        #endregion

        #region Méthode
        /// <summary>
        /// Cette méthode retourne une chaine de caractère qui affiche le plateau avec la face visible
        /// de tous les dés et un petit décor
        /// </summary>
        /// <returns></returns>
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
                message += "| ";
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.BackgroundColor = ConsoleColor.White;
                message += " ";
                for (int j = 0; j < this.taille; j++)
                {
                    message += "  ";
                }

                Console.ResetColor();
                message += " |\n| ";

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.BackgroundColor = ConsoleColor.White;
                message += " ";
                for (int j = 0; j < this.taille; j++)
                {
                    message +=  this.plateau[i, j].Face_visible;
                    message += " ";
                }

                Console.ResetColor();
                message += " |\n";
            }
            message += "| ";

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            message += " ";
            for (int j = 0; j < this.taille; j++)
            {
                message += "  ";
            }

            Console.ResetColor();
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


        /// <summary>
        /// Methode proche de la precedente, mais on y ajoute de la couleur pour le confort de
        /// l'utilisateur. Il faut donc afficher directement le plateau, et ne pas renvoyer une
        /// chaine de caractere
        /// </summary>
        public void toStringCouleur()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            for (int i = 0; i < (2 * this.taille + 5); i++)
            {
                Console.Write("_");
            }
            Console.WriteLine();

            Console.Write("|\\");
            for (int i = 0; i < (2 * this.taille + 1); i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("/|");


            for (int i = 0; i < this.taille; i++)
            {
                
                Console.Write("| ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(" ");
                for (int j = 0; j < this.taille; j++)
                {
                    Console.Write("  ");
                }
                Console.ResetColor();
                Console.WriteLine(" |");

                Console.Write("| ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(" ");
                for (int j = 0; j < this.taille; j++)
                {
                    Console.Write($"{this.plateau[i, j].Face_visible} ");
                }
                Console.ResetColor();
                Console.WriteLine(" |");
            }


            Console.Write("| ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" ");
            for (int j = 0; j < this.taille; j++)
            {
                Console.Write("  ");
            }
            Console.ResetColor();
            Console.WriteLine(" |");


            Console.Write("|/");
            for (int i = 0; i < (2 * this.taille + 1); i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("\\|");

            for (int i = 0; i < (2 * this.taille + 5); i++)
            {
                Console.Write('‾');
            }
            Console.WriteLine();
        }


        /// <summary>
        /// Cette méthode récursive teste si le mot passé en paramètre est un mot éligible,
        /// c’est-à-dire qu’il respecte les contraintes d'existence et d’adjacence, dans le plateau, des lettres qui le compose
        /// </summary>
        /// <param name="mot"></param> C'est le mot à tester
        /// <param name="posInvalides"></param> Ce tableau stocke les positions déjà utilisées afin de ne pas y repasser
        /// <param name="posCourante"></param> Il s'agit de la position de la dernière lettre trouvé à partir de laquelle 
        ///                                    on cherche donc la suivante
        /// <param name="compteur"></param> Ce paramètre initialisé à 0 nous permet de savoir où nous en sommes dans le mot pour stocker 
        ///                                 la position courante à la bonne place dans le tableau des positions invalides
        /// <returns></returns>
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
        /// <summary>
        /// Cette fonction mélange le plateau, c'est à dire qu'elle réattribue aléatoirement une autre face, parmi les 6,
        /// à chacun des dés du plateau
        /// </summary>
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
