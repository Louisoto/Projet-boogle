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
        private string nom;
        private int score;
        private List<string> motsTrouvés;

        public Joueur(string nom)
        {
            this.nom = nom;
            this.score = 0;
            this.motsTrouvés = new List<string>();
        }

        /// <summary>
        ///  qui teste si le mot passé appartient déjà aux mots trouvés par le joueur pendant la partie
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>
        public bool Contain(string mot)
        {
            bool test = true;

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
    }
}
