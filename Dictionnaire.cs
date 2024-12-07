using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_boogle
{
    internal class Dictionnaire
    {
        #region Attributs
        private string langue;// anglais ou français
        private List<List<string>> mots;
        #endregion

        #region Getters
        public string Langue
        {
            get { return langue; }
        }

        public List<List<string>> Mots
        {
            get { return mots; }
        }
        #endregion

        #region Constructeur
        public Dictionnaire(string langue) { 
            this.langue = langue;
            this.mots = Program.Recuperer_Dictionnaire(langue);
        }
        #endregion

        #region Méthode
        public string tostring()
        {
            string r = "Le dictionnaire est en " + langue + ".\nIl contient :";

            // Compteur par rapport à la longueur
            r += "\n\nNombre de mots par longueur :";
            for (int i = 0; i < mots.Count; i++)
            {
                r += "\nLongueur " + (i + 2) + " : " + mots[i].Count + " mots";
            }

            // Compteur par rapport à la lettre
            r += "\n\nNombre de mots par lettre :";
            // On compte les mots par lettre à chaque fois, en les plaçant dans un tableau
            int[] CompteurLettres = new int[26];
            for (int i = 0; i < mots.Count; i++)
            {
                for (int j = 0; j < mots[i].Count; j++)
                {
                    char premiereLettre = mots[i][j][0]; //  on prend la premiere lettre du mot
                    if (premiereLettre >= 'A' && premiereLettre <= 'Z')
                    {
                        CompteurLettres[premiereLettre - 'A']++; // on utilise la valeur ASCII des lettres pour retrouver la place qu'on leurs donne dans le tableau (ex: A donne 65)
                    }
                }
            }

            // Afficher les résultats pour chaque lettre
            for (int i = 0; i < 26; i++)
            {
                if (CompteurLettres[i] > 0)
                {
                    r += "\n" + (char)(i + 'a') + " : " + CompteurLettres[i] + " mots"; // Affiche la lettre et le nombre de mots qui commencent par cette lettre
                }
            }

            return r;
        }

        public bool Dichotimie(string mot)
        {
            int taille_mot = mot.Length - 2;
            if (taille_mot >= 0 && taille_mot < mots.Count)
            {
                int index = Program.Dichotomique(mots[taille_mot], mot, mots[taille_mot].Count - 1);
                return (index >= 0);
            }
            return false;
        }
        #endregion
    }
}
