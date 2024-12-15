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
        private DateTime debutJeu;
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
            TimeSpan tempsEcoule = DateTime.Now - debutJeu;
            TimeSpan tempsRestant = dureeTimer - tempsEcoule;

            if (tempsRestant <= TimeSpan.Zero)
            {
                jeuEnCours = false;
                return false;
            }
            return true;
        }
        #endregion
        static void Main(string[] args)
        {
            De.initialisationValLettres();

            Program.AffichageTitre();
            Thread.Sleep(3000);
            Console.Clear();


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
                        while (choix != 6)
                        {
                            Console.WriteLine("Option actuelles:\n1-Taille plateau : " + jeu.TaillePlateau);
                            Console.WriteLine("2-Temps timer : ");
                            Console.WriteLine("3-Nombre de tours : ");
                            Console.WriteLine("4-Nombre de joueur : ");
                            Console.WriteLine("5-Langue : ");
                            Console.WriteLine("6- Sortir");
                            choix_option = int.Parse(Console.ReadLine());
                            switch (choix_option)
                            {
                                case 1:
                                    Console.WriteLine("Modification de la taille du plateau\nQuelle est la nouvelle taille ?");
                                    int taille = int.Parse(Console.ReadLine());
                                    jeu.plateau.Taille = taille;
                                    break;
                                case 2:
                                    Console.WriteLine("Modification du timer\n Quelle est sa nouvelle durée (en secondes) ?");
                                    int newtimer = int.Parse(Console.ReadLine());*

                                    break;

                                case 6:
                                    Console.WriteLine("Sorrtie des options");
                                    break;
                                default:
                                    Console.WriteLine("Mauvaise manipulation");
                                    break;
                            }
                        }

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
