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
            set { this.nbToursPartie = value; }
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

        public static int Tailleplateau_option
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
                        Console.WriteLine("C'est au tour de " + this.joueurs[j].Nom + " de jouer.\n"
                                          + "Il reste " + tempsRestant().Minutes + " minute(s) et " + tempsRestant().Seconds + " secondes au tour.\n"
                                          + this.plateau.toString()
                                          + "\nQuel mot voyez-vous ?");
                        string mot = "penis merdeux";
                        if (this.joueurs[j].Nom == "IA")
                        {
                            mot = RechercheMotIA().ToUpper();
                            Console.WriteLine(mot);
                        }
                        else
                        {
                            mot = Console.ReadLine().ToUpper();
                        }
                        Console.WriteLine(mot);
                        if (this.plateau.Test_Plateau(mot))
                        {
                            joueurs[j].Add_Score(mot, dictionnaire, i);
                        }
                    }
                    Console.WriteLine(joueurs[j].toString(i));
                    this.plateau.melanger();
                }
            }
        }

        public string rechercheMotIA(string chaineCaractères = "", Position[] posInvalides = null, Position posCourante = null, int compteur = 0)
        {
            if(compteur >= this.TaillePlateau * this.TaillePlateau)
            {
                Console.WriteLine("Tous les mots sur ce plateau sont trouvés");
                return null;
            }
            if (this.dictionnaire.Dichotomie(chaineCaractères))
            {
                return chaineCaractères;
            }
            else
            {
                if (posCourante == null)
                {
                    posInvalides = new Position[this.TaillePlateau * this.TaillePlateau];
                    for (int i = 0; i < this.TaillePlateau; i++)
                    {
                        for (int j = 0; j < this.TaillePlateau; j++)
                        {
                            Position posTestée = new Position(i, j);
                            chaineCaractères += this.plateau.elemPlateau(i, j);
                            Position[] nouvelleListeInvalides = new Position[posInvalides.Length];
                            posInvalides.CopyTo(nouvelleListeInvalides, 0);
                            nouvelleListeInvalides[compteur] = posTestée;
                            string mot = rechercheMotIA(chaineCaractères, nouvelleListeInvalides, posTestée, compteur + 1);
                            if (!string.IsNullOrEmpty(mot))
                            {
                                return mot;
                            }
                        }
                    }
                }
                else
                {
                    if (this.dictionnaire.Existence(chaineCaractères, this.dictionnaire.MotsOrdreAlpha.Count))
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                bool test = false;
                                Position posTestée = new Position(posCourante.X + i, posCourante.Y + j);
                                for (int k = 0; k < posInvalides.Length && !test; k++)
                                {
                                    if (posInvalides[k] != null && posTestée.X == posInvalides[k].X && posTestée.Y == posInvalides[k].Y)
                                    {
                                        test = true;
                                    }
                                }
                                if (!test &&
                                    posTestée.X >= 0 && posTestée.X < this.TaillePlateau &&
                                    posTestée.Y >= 0 && posTestée.Y < this.TaillePlateau)
                                {
                                    chaineCaractères += this.plateau.elemPlateau(posTestée.X, posTestée.Y);
                                    if (this.dictionnaire.Existence(chaineCaractères, this.dictionnaire.MotsOrdreAlpha.Count))
                                    {
                                        Position[] nouvelleListeInvalides = new Position[posInvalides.Length];
                                        posInvalides.CopyTo(nouvelleListeInvalides, 0);
                                        nouvelleListeInvalides[compteur] = posTestée;
                                        string mot = rechercheMotIA(chaineCaractères, nouvelleListeInvalides, posTestée, compteur + 1);
                                        if (!string.IsNullOrEmpty(mot))
                                        {
                                            return mot;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return null;
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
        #endregion
    }
}
