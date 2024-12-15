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

        #region Getters
        public int TaillePlateau
        {
            get { return plateau.Taille; }
        }
        #endregion

        #region Propriété
        public int NbToursPartie { get { return this.nbToursPartie; } }
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
            //selection de la taille du plateau
            Console.Write("Sélectionner vos options : " +
                "\nTaille du plateau : ");
            int taillePlateau = int.Parse(Console.ReadLine());

            //selection de la langue des mots
            string langue;
            do
            {
                Console.Write("Langue du jeu (anglais ou francais) : ");
                langue = Console.ReadLine();
            } while (langue != "anglais" && langue != "francais");

            //selection du nombre de tours
            Console.Write("Nombre de tours de la partie : ");
            int nbToursPartie = int.Parse(Console.ReadLine());

            //selection du nombre de joueurs
            Console.Write("Nombre de joueur.s souhaité.s : ");
            int nbJoueurs = int.Parse(Console.ReadLine());

            //création du jeu
            Jeu jeu = new Jeu(taillePlateau, nbJoueurs, nbToursPartie);

            int choix = 0;
            int choix_option = 0;
            while (choix != 5)
            {
                Console.WriteLine("Menu:\n1- Nouvelle partie\n2- Meilleurs scores\n3- Afficher le nuage de mots precedent\n4- Option\n5- Quitter le jeu\n\nQuel est votre choix ?");
                choix = int.Parse(Console.ReadLine());

                switch (choix)
                {
                    case 1:
                        //ici on deroule le jeu. Il faudrait fait une methode de jeu ou c'est "derouler jeu" et ça gere les manches etc

                        break;
                    case 2:
                        //on change les option
                        break;
                    case 3:
                        //affichage du nuage de mot
                        break;
                    case 4:
                        Console.WriteLine("option actuelles:\nTaille plateau : " + jeu.TaillePlateau);
                        break;
                    case 5:
                        Console.WriteLine(Program.AffichageFin());
                        break;
                    default:
                        Console.WriteLine("Commande incorecte");
                        break;
                }

            }

            Console.WriteLine(jeu.plateau.toString());
            for (int i = 1; i <= jeu.nbToursPartie; i++)
            {

            }
            
        }
    }
}
