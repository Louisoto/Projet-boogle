using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_boogle
{
    internal class Dictionnaire
    {
        private string langue;// anglais ou français
        private List<List<string>> mots;

        public string Langue
        {
            get { return langue; }
        }

        public List<List<string>> Mots
        {
            get { return mots; }
        }

        public Dictionnaire(string langue) { 
            this.langue = langue;
            this.mots = Program.Recuperer_Dictionnaire(langue);
        }

        public string tostring()
        {
            string r = "le dictionnaire est en " + langue + ".\nIl contient :\n";


            return r;
        }

        public bool Dichotimie(string mot)
        {
            int taille_mot = mot.Length - 2;
            if (taille_mot >= 0 && taille_mot < mots.Count)
            {
                int index = Program.Dichotomique(mot[taille_mot], mot, mots[taille_mot].Count - 1);
                if (index >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
