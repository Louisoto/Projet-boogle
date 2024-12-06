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
    }
}
