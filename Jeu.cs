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
        private Plateau plateau;
        private int nbJoueurs;
        private Joueur[] joueurs;
        private int nbToursPartie;
        #endregion

        #region Propriété
        public int NbToursPartie() {  return this.nbToursPartie; }
        #endregion

        #region Constructeur
        public Jeu(int tailleJeu, int nbJoueurs, int nbToursPartie)
        {
            this.tourEnCours = 0;
            this.nbToursPartie = nbToursPartie;
            this.nbJoueurs = nbJoueurs;
            this.joueurs = new Joueur[nbJoueurs];
            for (int i = 1; i <= nbJoueurs; i++)
            {
                Console.Write("Quel est le nom du joueur " + i + " : ");
                this.joueurs[i-1] = new Joueur(Console.ReadLine(), nbToursPartie);
            }
            this.plateau = new Plateau(tailleJeu);
        }
        #endregion
        
        static void Main(string[] args)
        {
            Console.Write("Sélectionner vos options : " +
                "\nTaille du plateau : ");
            int taillePlateau = int.Parse(Console.ReadLine());
            string langue;
            do
            {
                Console.Write("Langue du jeu (anglais ou francais) : ");
                langue = Console.ReadLine();
            } while (langue != "anglais" && langue != "francais");
            Console.Write("Nombre de tours de la partie : ");
            int nbToursPartie = int.Parse(Console.ReadLine());
            Console.Write("Nombre de joueur.s souhaité.s : ");
            int nbJoueurs = int.Parse(Console.ReadLine());
            Jeu jeu = new Jeu(taillePlateau, nbJoueurs, nbToursPartie);
            Console.WriteLine(jeu.plateau.toString());
            for (int i = 1; i <= jeu.nbToursPartie; i++)
            {

            }
            Console.Read();
        }
    }
}
