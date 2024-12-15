using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_boogle
{
    internal class Jeu
    {
        #region Attributs
        private bool jeuEnCours;
        private Plateau plateau;
        private int nbJoueurs;
        private Joueur[] joueurs;
        private int nbToursPartie;
        private TimeSpan dureeTimer;
        private TimeSpan debutJeu;
        #endregion

        #region Propriété
        public int TaillePlateau
        {
            get { return plateau.Taille; }
        }

        public double DureeTimer
        {
            get { return dureeTimer.TotalSeconds; }
            set { dureeTimer = TimeSpan.FromSeconds(value); }
        }

        public int NbToursPartie { 
            get { return this.nbToursPartie; }
            set { this.nbToursPartie = value;}
        }
        #endregion

        #region Constructeur
        public Jeu(int tailleJeu, int nbJoueurs, int nbToursPartie)
        {
            this.jeuEnCours = false;
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

        #region Methodes
        public void Commencer_partie()
        {
            jeuEnCours = true;
            debutJeu = DateTime.Now;

        }
        public bool Verification_timer()
        {
            return true;
        }
        #endregion
        static void Main(string[] args)
        {
            De.initialisationValLettres();

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
                        Console.WriteLine("Option actuelles:\nTaille plateau : " + jeu.TaillePlateau);
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
