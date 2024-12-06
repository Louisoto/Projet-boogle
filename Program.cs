using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_boogle
{
    internal class Program
    {
        public static Random random = new Random();

        /// <summary>
        /// Lit les informations d'un fichier, puis tri chaque colone et place des informations dans trois tableaux differents
        /// en fonction de la lettre, ses points et sa probabilité d'apparaitre
        /// </summary>
        /// <param name="lettres"></param>
        /// <param name="points_lettre"></param>
        /// <param name="probabilite_lettre"></param>
        /// <param name="fichier"></param>
        public static void lire_fichier_lettres(out char[] lettres, out int[] points_lettre, out int[] probabilite_lettre, string fichier)
        {
            try
            {
                // Lecture des lignes du fichier
                string[] lignes_fichier = File.ReadAllLines(fichier);

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

            catch (Exception ex)
            {
                lettres = null;
                points_lettre = null;
                probabilite_lettre = null;

                Console.WriteLine("Erreur lors de la lecture du fichier : " + ex.Message);

            }
        }

        /// <summary>
        /// Choisis une lettre aleatoirement en fonction du pourcentage qu'a chaque lettre d'apparaittre
        /// </summary>
        /// <param name="lettres"></param>
        /// <param name="probabilite_lettre"></param>
        /// <returns></returns>
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
