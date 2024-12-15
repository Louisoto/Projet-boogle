using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_boogle
{
    internal class Joueur
    {
        #region Attributs
        private string nom;
        private int score;
        private List<string>[] motsTrouvés;
        #endregion

        #region Constructeurs
        public Joueur(string nom, int nbToursPartie)
        {
            this.nom = nom;
            this.score = 0;
            this.motsTrouvés = new List<string>[nbToursPartie];
            for (int i = 0; i < nbToursPartie; i++)
            {
                this.motsTrouvés[i] = new List<string>();
            }
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Cette méthode teste si le mot passé appartient déjà aux mots trouvés par le joueur pendant le tour en cours.
        /// Si c'est le cas alors elle renvoie true et le mot n'est pas ajouté.
        /// Sinon elle renvoie false et le mot est valable vis à vis de ce critère pour être ajouter aux mots trouvés.
        /// </summary>
        /// <param name="mot"></param> Mot à tester
        /// <param name="tourEnCours"></param> Tour de la partie où il faut effectuer le test
        /// <returns></returns>
        public bool Contain(string mot, int tourEnCours)
        {
            bool test = false;
            for (int i = 0; i < this.motsTrouvés[tourEnCours].Count && !test; i++)
            {
                if (this.motsTrouvés[tourEnCours][i] == mot)
                {
                    test = true;
                }
            }
            return test;
        }

        /// <summary>
        /// ajoute le mot dans la liste des mots déjà trouvés par le joueur au cours de la partie en modifiant le nombre d’occurrences si nécessaire
        /// </summary>
        /// <param name="mot"></param>
        public void Add_Mot(string mot, int tourEnCours)
        {
            this.motsTrouvés[tourEnCours].Add(mot);
        }

        public string toString()
        {
            string resul = "Le joueur " + this.nom + " a un score de " + this.score + ".\nLes mots trouvés sont : ";
            for (int i = 0; i < this.motsTrouvés.Length; i++)
            {
                resul += "\nAu round " + i + " : ";
                for (int j = 0; j < this.motsTrouvés[i].Count; j++)
                {
                    resul += this.motsTrouvés[j][i];
                }
            }
            return resul;
        }
        /// <summary>
        /// Dans un premier temps cette fonction vérifie que un mot passé en paramètre appartient bien au dictionnaire et est bien sur le plateau.
        /// Ensuite elle met à jour le score du joueur et ajoute le mot à la liste des mots trouvés par le joueur à ce tour.
        /// Les paramètres en plus de mot permettent l'appel des fonction de test.
        /// </summary>
        /// <param name="mot"></param> Mot à tester
        /// <param name="dico"></param> Langue du jeu en cours
        /// <param name="tourEnCours"></param> Tour de la partie où le mot est entré
        /// <param name="joueur"></param> Joueur ayant entré le mot
        public void Add_Score(string mot, Dictionnaire dico, int tourEnCours, Joueur joueur)
        {
            if (mot.Length > 2 && dico.Dichotimie(mot) && !joueur.Contain(mot, tourEnCours))
            {
                int ajout = 0;
                for (int i = 0; i < mot.Length; i++)
                {
                    ajout += De.Point_lettre(mot[i] - 'a');
                }
                this.score += ajout * (mot.Length / 2);
                joueur.Add_Mot(mot, tourEnCours);
            }
        }
        #endregion
    }
}
