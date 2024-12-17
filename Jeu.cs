﻿using System;
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
        private Plateau plateau;
        private int nbJoueurs;
        private Joueur[] joueurs;
        private int nbToursPartie;
        private Dictionnaire dictionnaire;
        private TimeSpan dureeTimer;
        private DateTime debutTour;

        //attributs statiques permettant de stocker les options
        private static TimeSpan dureeTimer_option;
        private static int nbJoueurs_option;
        private static int nbTours_option;
        private static int tailleplateau_option;
        private static string langue_option;

        private static int[] meilleursScores;
        private static string[] nomJoueurMeilleursScores;
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
        public Jeu()
        {
            this.nbToursPartie = nbTours_option;
            this.nbJoueurs = nbJoueurs_option;
            this.dureeTimer = dureeTimer_option;
            this.dictionnaire = new Dictionnaire(langue_option);
            this.joueurs = new Joueur[nbJoueurs];
            for (int i = 1; i <= nbJoueurs; i++)
            {
                Console.Write("Quel est le nom du joueur " + i + " : ");
                this.joueurs[i - 1] = new Joueur(Console.ReadLine(), nbToursPartie);
            }
            this.plateau = new Plateau(tailleplateau_option);
        }
        #endregion

        #region Methodes
        public void Commencer_tour()
        {
            debutTour = DateTime.Now;
        }

        public TimeSpan tempsRestant()
        {
            return (this.dureeTimer - (DateTime.Now - this.debutTour));
        }

        public bool Verification_timer()
        {
            if (tempsRestant() <= TimeSpan.Zero)
            {
                return false;
            }
            return true;
        }

        public void jouer()
        {

            for (int i = 0; i < nbToursPartie; i++)
            {
                for (int j = 0; j < nbJoueurs; j++)
                {
                    Commencer_tour();
                    while (Verification_timer()) {
                        Console.WriteLine("C'est au tour de " + joueurs[j].Nom + " de jouer.\n"
                                          + "Il reste " + tempsRestant().Minutes + " minute(s) et " + tempsRestant().Seconds + " secondes au tour.\n"
                                          + plateau.toString() 
                                          + "\nQuel mot voyez-vous ?");
                        string mot = Console.ReadLine().ToUpper();
                        if (plateau.Test_Plateau(mot))
                        {
                            joueurs[j].Add_Score(mot, dictionnaire, i);
                        }
                    }
                    Console.WriteLine(joueurs[j].toString(i));
                    plateau.melanger();
                }
            }
        }
        static int SaisieNombreSecur()
        {
            int result = 0;
            while (!int.TryParse(Console.ReadLine(), out result) || result <= 0) { }
            return result;
        }
        #endregion


        static void Main(string[] args)
        {
            De.initialisationValLettres("francais");

            Console.WriteLine (Program.AffichageTitre());
            Thread.Sleep(500);
            Console.Clear();

            //initialisation des options du jeu
            Jeu.dureeTimer_option = TimeSpan.FromSeconds(60);
            Jeu.nbJoueurs_option = 2;
            Jeu.nbTours_option = 2;
            Jeu.tailleplateau_option = 4;
            Jeu.langue_option = "francais";

            Jeu.meilleursScores = new int[20];
            Jeu.nomJoueurMeilleursScores = new string[20];

            int choix = 0;
            while (choix != 5)
            {
                Console.WriteLine("Menu:\n" +
                    "1- Nouvelle partie\n" +
                    "2- Meilleurs scores\n" +
                    "3- Nuage de mots\n" +
                    "4- Option\n" +
                    "5- Quitter le jeu\n\n" +
                    "Quel est votre choix ?");
                choix = SaisieNombreSecur();
                Console.Clear();

                switch (choix)
                {
                    case 1:
                        //ici on deroule le jeu. Il faudrait fait une methode de jeu ou c'est "derouler jeu" et ça gere les manches etc
                        Jeu jeu = new Jeu();
                        jeu.jouer();
                        break;
                    case 2:
                        //on affiche les meilleurs scores
                        for (int i = 0; i < Jeu.meilleursScores.Length; i++)
                        {
                            if (Jeu.meilleursScores[i] != 0)
                            {
                                Console.WriteLine("Le " + i + "ème meilleur score est de " + Jeu.meilleursScores[i] + 
                                                  " réalisé par " + Jeu.nomJoueurMeilleursScores[i]);
                            }
                            else
                            {
                                Console.WriteLine("Aucun score enregistré");
                            }
                            Console.WriteLine();
                        }
                        break;
                    case 3:
                        //affichage du nuage de mot
                        break;
                    case 4:
                        int choix_option = 0;
                        while (choix_option != 6)
                        {
                            Console.WriteLine("Option actuelles:\n1-Taille plateau : " + Jeu.tailleplateau_option +
                                "\n2-Temps timer : " + Jeu.dureeTimer_option +
                                "\n3-Nombre de tours : " + Jeu.nbTours_option +
                                "\n4-Nombre de joueur : " + Jeu.nbJoueurs_option +
                                "\n5-Langue : " + Jeu.langue_option +
                                "\n6-Sortir" +
                                "\nQue souhaitez-vous modifier ?");
                            choix_option = SaisieNombreSecur();
                            Console.Clear();
                            switch (choix_option)
                            {
                                case 1:
                                    Console.WriteLine("Modification de la taille du plateau\nQuelle est la nouvelle taille ?");
                                    Jeu.tailleplateau_option = SaisieNombreSecur();
                                    Console.Clear();
                                    break;
                                case 2:
                                    Console.WriteLine("Modification du timer\nQuelle est sa nouvelle durée (en secondes) ?");
                                    Jeu.dureeTimer_option = TimeSpan.FromSeconds(SaisieNombreSecur());
                                    Console.Clear();
                                    break;
                                case 3:
                                    Console.WriteLine("Modification du nombre de tour\nCombien de tours souhaitez-vous ?");
                                    Jeu.nbTours_option = SaisieNombreSecur();
                                    Console.Clear();
                                    break;
                                case 4:
                                    Console.WriteLine("Modification du nombre de joueur\nCombien de joueurs souhaitez-vous ?");
                                    Jeu.nbJoueurs_option = SaisieNombreSecur();
                                    Console.Clear();
                                    break;
                                case 5:
                                    Console.WriteLine("Modification de la langue");
                                    string langue;
                                    do
                                    {
                                        Console.WriteLine("Quelle langue souhaitez-vous (anglais ou francais) ?");
                                        langue = Console.ReadLine();
                                    } while (langue != "anglais" && langue != "francais");
                                    Jeu.langue_option = langue;
                                    Console.Clear();
                                    break;
                                case 6:
                                    Console.WriteLine("Sortie des options");
                                    break;
                                default:
                                    Console.WriteLine("Mauvaise manipulation");
                                    break;
                            }
                        }
                        break;
                    case 5:
                        Console.WriteLine(Program.AffichageFin());
                        Thread.Sleep(3000);
                        break;
                    default:
                        Console.WriteLine("Commande incorecte");
                        break;
                }
            }
        }
    }
}
