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

        //attributs statiques permettant de stocker les options
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
        public int TaillePlateau
        {
            get { return plateau.Taille; }
        }
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
                        Console.WriteLine(this.joueurs[j].toStringSimple() + "Temps restant : " + tempsRestant().Minutes +
                            " minute(s) et " + tempsRestant().Seconds + " secondes.\n");

                        this.plateau.toStringCouleur();
                        Console.WriteLine("\nQuel mot voyez-vous ?");

                        string mot = "";
                        if (this.joueurs[j].Nom == "IA")
                        {
                            mot = RechercheMotIA().ToUpper();
                            Console.WriteLine(mot);
                        }
                        else
                        {
                            mot = Console.ReadLine().ToUpper();
                        }
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
                    }
                    Console.Clear();
                    Console.WriteLine(joueurs[j].toString(i));
                    Console.WriteLine("Appuyez sur une touche pour continuer");
                    Console.ReadKey();
                    Console.Clear();
                    this.plateau.melanger();
                }
            }

            // On cherche quel est le jouer avec le meilleur score
            Joueur vainqueur = joueurs[0];
            for (int i = 1; i < joueurs.Length; i++)
            {
                if (joueurs[i].Score > vainqueur.Score)
                {
                    vainqueur = joueurs[i];
                }
            }

            //une fois trouvé, on l'affiche à l'écran: 
            Console.WriteLine(Program.AffichageVictoire(vainqueur.Nom, vainqueur.Score));

            Console.WriteLine("\n\nAppuyez sur une touche pour continuer");
            Console.ReadKey();
            Console.Clear();


            //La dernière étape est de mettre à jouer les differents paramettres
            for (int i = 0; i < nbJoueurs; i++)
            {
                MajMotsJoueur(joueurs[i]);
                MajMeilleursScores(joueurs[i]);
            }
        }

        public string RechercheMotIA(string chaineCaractères = "", Position[] posInvalides = null, Position posCourante = null, int compteur = 0)
        {
            if (posCourante == null)
            {
                posInvalides = new Position[this.TaillePlateau * this.TaillePlateau];
                for (int i = 0; i < this.TaillePlateau; i++)
                {
                    for (int j = 0; j < this.TaillePlateau; j++)
                    {
                        Position posTestée = new Position(i, j);
                        posInvalides = new Position[this.TaillePlateau * this.TaillePlateau];
                        string nouveauMot = RechercheMotIA(this.plateau.elemPlateau(i, j).ToString(), posInvalides, posTestée, compteur + 1);
                    }
                }
                return null;
            }
            if (!this.dictionnaire.Existence(chaineCaractères, this.dictionnaire.MotsOrdreAlpha.Count))
            {
                return null;
            }
            if (this.dictionnaire.Dichotomie(chaineCaractères))
            {
                return chaineCaractères;
            }
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Position posTestée = new Position(posCourante.X + i, posCourante.Y + j);
                    if (posTestée.X >= 0 && posTestée.X < this.TaillePlateau &&
                        posTestée.Y >= 0 && posTestée.Y < this.TaillePlateau &&
                        !posTestée.PosInvalide(posInvalides))
                    {
                        string nouveauMot = chaineCaractères + this.plateau.elemPlateau(posTestée.X, posTestée.Y);
                        Position[] nouvellePosInvalides = new Position[posInvalides.Length];
                        posInvalides.CopyTo(nouvellePosInvalides, 0);
                        nouvellePosInvalides[compteur] = posTestée;
                        string motTrouvé = RechercheMotIA(nouveauMot, nouvellePosInvalides, posTestée, compteur + 1);
                    }
                }
            }
            return null;
        }
        
        private void MajMotsJoueur( Joueur joueur)
        {
            //étape pour verifier si on a deja enregistré des mots pour ce joueur
            if (mots_joueurs == null)
            {
                mots_joueurs = new Dictionary<string, List<string>>();
            }

            if (!mots_joueurs.ContainsKey(joueur.Nom))
            {
                mots_joueurs[joueur.Nom] = new List<string>();
            }

            //On prend l'ensemble des mots qu'il a écrit pour les placer dans une liste desordonné
            List<string> motsVrac = new List<string>(mots_joueurs[joueur.Nom]);
            for (int tour = 0; tour < nbToursPartie; tour++)
            {
                for (int k = 0; k < joueur.MotsTrouvés[tour].Count; k++)
                {
                    string mot = joueur.MotsTrouvés[tour][k];
                    //étape important pour éviter les doublons
                    if (!motsVrac.Contains(mot)) 
                    {
                        motsVrac.Add(mot);
                    }
                }
            }

            //étape finale pour trié les mots dans l'ordre décroissant de leurs score
            for (int i = 0; i < motsVrac.Count - 1; i++)
            {
                for (int j = i + 1; j < motsVrac.Count; j++)
                {
                    if (Joueur.Calcul_Score(motsVrac[i]) < Joueur.Calcul_Score(motsVrac[j]))
                    {
                        // Échange des mots
                        string temp = motsVrac[i];
                        motsVrac[i] = motsVrac[j];
                        motsVrac[j] = temp;
                    }
                }
            }

            //à la fin de cette étape, motsVrac ne contient plus les mots en vrac, on peut donc les ajouter à l'ensemble des mots trié
            mots_joueurs[joueur.Nom] = motsVrac;
        }

        private void MajMeilleursScores(Joueur joueur)
        {
            //on verifie si les tableaux on bien été initialisé
            if (meilleursScores == null || nomJoueurMeilleursScores == null)
            {
                meilleursScores = new int[20];
                nomJoueurMeilleursScores = new string[20];
            }

            //On place le nouveau score
            for (int i = 0; i < meilleursScores.Length; i++)
            {
                if (joueur.Score > meilleursScores[i])
                {
                    // Si le nouveau score est plus grand que les precedent, il faut decaller les autres scores..
                    for (int j = meilleursScores.Length - 1; j > i; j--)
                    {
                        meilleursScores[j] = meilleursScores[j - 1];
                        nomJoueurMeilleursScores[j] = nomJoueurMeilleursScores[j - 1];
                    }
                    // ..pour pouvoir placer le nouveau score et son nom
                    meilleursScores[i] = joueur.Score;
                    nomJoueurMeilleursScores[i] = joueur.Nom;
                    break;
                }
            }
        }


            #endregion
        }
}
