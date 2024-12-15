using System;
using System.IO;

namespace Projet_boogle
{
    internal class De
    {
        #region Attributs
        private char[] faces;
        private char face_visible;
        private static char[] lettres;
        private static int[] point_lettres;
        private static int[] probabilite_lettre;
        #endregion

        #region Getters
        public char[] Faces { 
            get { return faces; } 
        }   
        public char Face_visible {  
            get { return face_visible; }
        }
        public static int Point_lettre(int i)
        {
            return point_lettres[i];
        }
        #endregion

        #region Constructeur
        public De() {
            //Si on a oublié d'initialiser les valeur, alors on initialise en français (normallement pas besoin de le faire)
            if (lettres == null || point_lettres == null || probabilite_lettre == null)
            {
                initialisationValLettres("francais");
            }

            this.faces = new char[6];
            for (int i = 0; i < faces.Length; i++)//pour chaque face, on assigne une lettre aléatoirement en fonction des proba
            {
                faces[i] = Program.Choisir_Lettre_Aleatoire(lettres, probabilite_lettre);
            }

            Lance(Program.random);// on utilise la methode lance pour assigner une face
        }
        #endregion

        #region Méthode
        public static void initialisationValLettres(string langue)
        {
            Program.lire_fichier_lettres(out lettres, out point_lettres, out probabilite_lettre, "Lettres" + langue + ".txt");
        }

        /// <summary>
        /// Méthode pour modifier la face visible en tirant au hasard l'une des 6 faces du dé
        /// </summary>
        /// <param name="r"></param>
        public void Lance(Random r)
        {
            int numero_face = r.Next(0, Faces.Length);
            this.face_visible = faces[numero_face];
        }

        /// <summary>
        /// Méthode pour donner les informations sur le dé, c'est à dire ses faces ainsi que sa face visible
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string r = "Faces du dé: ";

            //on donne les 6 faces du dé
            for (int i = 0; i< faces.Length; i++) {
                r += faces[i] + ", ";
            }
            //et on donne la face visible
            r += "\nFace visible: " + face_visible;
            return r;
        }
        #endregion 
    }


}
