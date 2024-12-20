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
        private Plateau plateau;
        private int nbJoueurs;
        private Joueur[] joueurs;
        private int nbToursPartie;
        private Dictionnaire dictionnaire;
        private TimeSpan dureeTimer;
        private DateTime debutTour;

        private static TimeSpan dureeTimer_option;
        private static int nbJoueurs_option;
        private static int nbTours_option;
        private static int tailleplateau_option;
        private static string langue_option;

        private static int[] meilleursScores;
        private static string[] nomJoueurMeilleursScores;

        private static Dictionary<string, List<string>> mots_joueurs = new Dictionary<string, List<string>>();
        #endregion

        #region Propriété
        public static TimeSpan DureeTimer_option
        {
            get { return dureeTimer_option; }
            set { dureeTimer_option = value; }
        }
        public static int NbJoueurs_option
        {
            get { return nbJoueurs_option; }
            set { nbJoueurs_option = value; }
        }
        public static int NbTours_option
        {
            get { return nbTours_option; }
            set { nbTours_option = value; }
        }
        public static int TaillePlateau_option
        {
            get { return tailleplateau_option; }
            set { tailleplateau_option = value; }
        }
        public static string Langue_option
        {
            get { return langue_option; }
            set { langue_option = value; }
        }
        public static int[] MeilleursScores
        {
            get { return meilleursScores; }
            set { meilleursScores = value; }
        }
        public static string[] NomJoueurMeilleursScores
        {
            get { return nomJoueurMeilleursScores; }
            set { nomJoueurMeilleursScores = value; }
        }
        public static Dictionary<string, List<string>> Mots_joueurs
        {
            get { return mots_joueurs; }
            set {} 
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
                        Console.Clear();
                        this.joueurs[j].toStringSimple();
                        Console.WriteLine("Temps restant : " + tempsRestant().Minutes +
                            " minute(s) et " + tempsRestant().Seconds + " secondes.\n");

                        this.plateau.toStringCouleur();
                        Console.WriteLine("\nQuel mot voyez-vous ?");
                        string mot = Console.ReadLine().ToUpper().Trim();
                        if (this.plateau.Test_Plateau(mot))
                        {
                            bool test = joueurs[j].Add_Score(mot, dictionnaire, i);
                            if (test)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Mot Correct");
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Mot Incorrect");
                                Thread.Sleep(1000);
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Mot Absent du plateau");
                            Thread.Sleep(1000);
                        }
                        Console.ResetColor();
                    }
                    Console.Clear();
                    Console.WriteLine(joueurs[j].toString(i));
                    Console.Write("C'est au tour ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (j == joueurs.Length - 1)
                    {
                        Console.Write(joueurs[0].Nom);
                    }
                    else
                    {
                        Console.Write(joueurs[j + 1].Nom);
                    }
                    Console.ResetColor();
                    Console.WriteLine(" de jouer\n" +
                                      "Appuyez sur une touche pour continuer");
                    Console.ReadKey();
                    Console.Clear();
                    this.plateau.melanger();
                }
            }
            Joueur vainqueur = joueurs[0];
            for (int i = 1; i < joueurs.Length; i++)
            {
                if (joueurs[i].Score > vainqueur.Score)
                {
                    vainqueur = joueurs[i];
                }
            }
            Console.WriteLine(Program.AffichageVictoire(vainqueur.Nom, vainqueur.Score));
            Console.WriteLine("\n\nAppuyez sur une touche pour continuer");
            Console.ReadKey();
            Console.Clear();
            for (int i = 0; i < nbJoueurs; i++)
            {
                MajMotsJoueur(joueurs[i]);
                MajMeilleursScores(joueurs[i]);
            }
        }
        
        private void MajMotsJoueur( Joueur joueur)
        {
            if (mots_joueurs == null)
            {
                mots_joueurs = new Dictionary<string, List<string>>();
            }
            if (!mots_joueurs.ContainsKey(joueur.Nom))
            {
                mots_joueurs[joueur.Nom] = new List<string>();
            }
            List<string> motsVrac = new List<string>(mots_joueurs[joueur.Nom]);
            for (int tour = 0; tour < nbToursPartie; tour++)
            {
                for (int k = 0; k < joueur.MotsTrouvés[tour].Count; k++)
                {
                    string mot = joueur.MotsTrouvés[tour][k];
                    if (!motsVrac.Contains(mot)) 
                    {
                        motsVrac.Add(mot);
                    }
                }
            }
            for (int i = 0; i < motsVrac.Count - 1; i++)
            {
                for (int j = i + 1; j < motsVrac.Count; j++)
                {
                    if (Joueur.Calcul_Score(motsVrac[i]) < Joueur.Calcul_Score(motsVrac[j]))
                    {

                        string temp = motsVrac[i];
                        motsVrac[i] = motsVrac[j];
                        motsVrac[j] = temp;
                    }
                }
            }
            mots_joueurs[joueur.Nom] = motsVrac;
        }

        private void MajMeilleursScores(Joueur joueur)
        {
            if (meilleursScores == null || nomJoueurMeilleursScores == null)
            {
                meilleursScores = new int[20];
                nomJoueurMeilleursScores = new string[20];
            }

            for (int i = 0; i < meilleursScores.Length; i++)
            {
                if (joueur.Score > meilleursScores[i])
                {
                    for (int j = meilleursScores.Length - 1; j > i; j--)
                    {
                        meilleursScores[j] = meilleursScores[j - 1];
                        nomJoueurMeilleursScores[j] = nomJoueurMeilleursScores[j - 1];
                    }
                    meilleursScores[i] = joueur.Score;
                    nomJoueurMeilleursScores[i] = joueur.Nom;
                    break;
                }
            }
        }
            #endregion
        }
}
