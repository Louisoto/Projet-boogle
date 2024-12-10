using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_boogle
{
    internal class Jeu
    {
        #region Attributs
        private int tourEnCours;
        private int tailleJeu;
        private De[] jeu;
        private int nbJoueurs;
        private string[] nomJoueurs;
        #endregion
        #region Constructeur
        public Jeu(int tailleJeu, int nbJoueurs)
        {
            this.tailleJeu = tailleJeu;
            this.tourEnCours = 0;
            this.nbJoueurs = nbJoueurs;
            this.nomJoueurs = new string[nbJoueurs];
            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.Write("Quel est le nom du joueur " + i + " : ");
                this.nomJoueurs[i] = Console.ReadLine();
            }
            this.jeu = new De[tailleJeu];
            for (int i = 0; i < tailleJeu; i++)
            {
                jeu[i] = new De();
            }
        }
        #endregion
        static void Main(string[] args)
        {
            Console.WriteLine("Allo, à l'huile");
            Console.WriteLine("Skibidi dop dop");
            Console.Read();
            Console.WriteLine("Sélectionner vos options : ");
        }
    }
}
