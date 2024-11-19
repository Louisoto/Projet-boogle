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
        private int nbToursPartie; //Nombre de tours dans la partie en cours 
        #endregion

        #region Constructeurs
        public Joueur(string nom, int nbToursPartie)
        {
            this.nbToursPartie = nbToursPartie;
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
        /// Fonction qui teste si le mot passé appartient déjà aux mots trouvés par le joueur pendant le tour en cours
        /// </summary>
        /// <param name="mot"></param>
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
        public void Add_Mot(string mot)
        {

        }

        public string toString()
        {
            string resul = "";

            return resul;
        }
        #endregion
    }
}
