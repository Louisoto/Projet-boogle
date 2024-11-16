using System;
using System.IO;

namespace Projet_boogle
{
    internal class De
    {
        //attributs
        private char[] faces;
        private char face_visible;

        //propriétés
        public char[] Faces { 
            get { return faces; } 
        }   

        public char Face_visible {  
            get { return face_visible; } 
            private set { face_visible = value; }
        }

        //constructeur
        public De() {
            //on creer les tableau pour stocker les donner 
            char[] lettres;
            int[] points_lettre; //ici ça nous sert à rien, mais la fonction lire_fichier_lettres à été creer pour etre le plus general possible
            int[] probabilite_lettre;

            Program.lire_fichier_lettres(out lettres, out points_lettre, out probabilite_lettre);


            this.faces = new char[6];

            for (int i = 0; i < faces.Length; i++)//pour chaque face, on assigne une lettre aléatoirement en fonction des proba
            {
                faces[i] = Program.Choisir_Lettre_Aleatoire(lettres, probabilite_lettre);
            }

            int numero_face = Program.random.Next(0, 6); //on assigne une des lettre pour dire qu'il sagit de la face
            this.face_visible = faces[numero_face];
        } 

        //Méthodes
        public void Lance(Random r)
        {
            int numero_face = r.Next(0, Faces.Length);
            Face_visible = faces[numero_face];
        }

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
    }

    internal class Program
    {
        public static Random random = new Random();

        public static void lire_fichier_lettres(out char[] lettres, out int[] points_lettre, out int[] probabilite_lettre)
        {
            // Lecture des lignes du fichier
            string[] lignes_fichier = File.ReadAllLines("Lettres.txt");

            // initiqlisation des tableaux
            lettres = new char[lignes_fichier.Length];
            points_lettre = new int[lignes_fichier.Length];
            probabilite_lettre = new int[lignes_fichier.Length];

            for (int i = 0; i < lignes_fichier.Length; i++)
            {
                // Découper la ligne en trois parties, chaqu'une separée par ;
                string[] parties = lignes_fichier[i].Split(';');

                // Stocker chaque valeur dans le tableau correspondant
                lettres[i] = char.Parse(parties[0]);          // lettres
                points_lettre[i] = int.Parse(parties[1]);     // points pas lettres
                probabilite_lettre[i] = int.Parse(parties[2]); // probabilité de chasue lettre
            }
        }

        public static char Choisir_Lettre_Aleatoire(char[] lettres, int[] probabilite_lettre)
        {
            int tirage = random.Next(1, 101); // Nombre aléatoire entre 1 et 100
            int somme = 0;

            //pour chaque iteration, on ajoute le "pourcentage", et dès que ça correspond au nombre aléatoire alors on prend la lettre qui correspond
            for (int i = 0; i < lettres.Length; i++)
            {
                somme += probabilite_lettre[i];
                if (tirage <= somme)
                {
                    return lettres[i];
                }
            }

            // Retour par défaut au cas ou (normallement ça sert à rien mais bon on sait jamais)
            return lettres[lettres.Length - 1];
        }
    }
}
