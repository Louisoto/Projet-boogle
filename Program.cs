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

                Console.WriteLine("Erreur lors de la lecture du fichier lettres : " + ex.Message);

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

        public static List<List<string>> Recuperer_Dictionnaire(string fichier)
        {
            List<List<string>> dictionnaire = new List<List<string>>();
            try
            {
                string contenu = File.ReadAllText(fichier);
                string[] mots = contenu.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                dictionnaire = tri_taille(mots);

                for (int i = 0; i < dictionnaire.Count; i++)
                {
                    if (dictionnaire[i] != null)
                    {
                        dictionnaire[i] = tri_fusion(dictionnaire[i]);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Erreur lors de la lecture du fichier dictionnaire : " + ex.Message);
            }
            return dictionnaire;
        }
        static List<List<string>> tri_taille(string[] tableau)
        {
            List<List<string>> dictionnaire = new List<List<string>>();

            for (int i = 0; i < tableau.Length; i++)
            {
                string mot = tableau[i];
                int index = mot.Length - 2;//comme on considere qu'il n'y a pas de mot de taille 0 ou 1
                while (index >= dictionnaire.Count)
                {
                    dictionnaire.Add(new List<string>());
                }

                dictionnaire[index].Add(mot);
            }
            return dictionnaire;
        }

        static List<string> tri_fusion(List<string> tableau)
        {
            if (tableau.Count <= 1)
            {
                return tableau;
            }

            int milieu = tableau.Count / 2;

            List<string> gauche = new List<string>(tableau.GetRange(0, milieu));
            List<string> droite = new List<string>(tableau.GetRange(milieu, tableau.Count - milieu));


            gauche = tri_fusion(gauche);
            droite = tri_fusion(droite);

            return Fusionner(gauche, droite);
        }

        // Fonction pour fusionner des tableaux
        static List<string> Fusionner(List<string> gauche, List<string> droite)
        {
            List<string> resultat = new List<string>();
            int i = 0, j = 0;

            while (i < gauche.Count && j < droite.Count)
            {
                if (gauche[i].CompareTo(droite[j]) <= 0)
                {
                    resultat.Add(gauche[i++]);
                }
                else
                {
                    resultat.Add(droite[j++]);
                }
            }

            while (i < gauche.Count)
            {
                resultat.Add(gauche[i++]);
            }

            while (j < droite.Count)
            {
                resultat.Add(droite[j++]);
            }

            return resultat;
        }
    }
}
