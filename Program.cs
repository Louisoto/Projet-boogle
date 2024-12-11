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
        /// Fonction pour lire les informations du fichier lettre, puis tri chaque colone et place des informations dans 
        /// trois tableaux differents: les lettres, les points et la probabilité d'apparaitre.
        /// On utilise un try catch pour eviter que ça plante si on ne trouve pas le fichier
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
                string[] lignes_fichier = File.ReadAllLines("../../" + fichier);

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
        /// Fonction pour choisir une lettre aleatoirement en fonction du pourcentage qu'a chaque lettre d'apparaittre
        ///
        /// Explication:
        /// Le fonctionnement n'est pas intuitif mais comme il n'y a pas de moyen d'utiliser la fonction random pour des pourcentages d'apparaitre.
        /// On utilise donc un systeme qui va, a chaque iteration, on ajoute le purcentage dans une somme. Si le nombre aléatoire tiré au debut
        /// est passé (inferieur ou egale) alors c'est la bonne lettre, sinon on test la lettre suivante
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

        /// <summary>
        /// Fonction pour lire le fichier dictionnaire en fonction du nom du fichier(si on veut le français ou l'anglais).
        /// Elle renvoie une liste de liste de string avec l'ensemble de données du tableau. Ainsi chaque sous liste
        /// correspond à une taille de lettre (ex le premier element regroupe les lettres de deux mots) et chaque sous 
        /// liste est trié par ordre alphabetique
        /// 
        /// Explication:
        /// On fait tout les operations dans try catch pour eviter les éventuelles plantages. On va d'abord stocker les 
        /// info dans un tableau mot (ou il y aurait les 130 000 mots en bazar) Puis on va appeler la fonction pour trier 
        /// les mots en fonction de leurs tailles. Puis pour chaque taille de mot, on applique le tri fusion.
        /// </summary>
        /// <param name="fichier"></param>
        /// <returns> Liste de liste de string trié </returns>
        public static List<List<string>> Recuperer_Dictionnaire(string fichier)
        {
            List<List<string>> dictionnaire = new List<List<string>>();
            try
            {
                string contenu = File.ReadAllText("../../" + fichier);
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

        /// <summary>
        /// Fonction pour trié les mots en fonction de leurs taille, et qui les place dans une liste (en fonction de leurs taille)
        /// 
        /// Explication:
        /// On creer une liste de liste de string, et pour chaque mot on regarde sa taille, et on le place dans une sous liste 
        /// (on place à l'index taille-2 parce que y'a ni mot sans lettre, ni mot de une lettre
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Tri fusion classique en recursive, qui prend en entrée une liste de string non trié, et on la trie par ordre alphabetique
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Fonction pour fusionner des tableaux tout en respectant l'ordre alphabetique
        /// </summary>
        /// <param name="gauche"></param>
        /// <param name="droite"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Fonction recursive en divisé pour mieux regner de recherche dichotomique, qui prend en entre
        /// une liste destring, un mot à cherche, et la taille de la liste et qui va de maniere recursive
        /// chercher l'indice du mot dans ce tableau si on le trouve pas, alors on renvoie -1
        /// </summary>
        /// <param name="t"></param>
        /// <param name="elem"></param>
        /// <param name="fin"></param>
        /// <param name="debut"></param>
        /// <returns></returns>
        public static int Dichotomique(List<string> t, string elem, int fin, int debut = 0)
        {
            if (t == null || t.Count == 0 || debut > fin)
            {
                return -1;
            }

            int milieu = (debut + fin) / 2;

            if (t[milieu].CompareTo(elem) == 0) // on a trouvé le mot cherché
            {
                return milieu;
            }
            else if (t[milieu].CompareTo(elem) > 0) // Le mot cherché est avant
            {
                return Dichotomique(t, elem, milieu - 1, debut);
            }
            else // Le mot cherché est après
            {
                return Dichotomique(t, elem, fin, milieu + 1);
            }
        }

        /// <summary>
        /// Fonction pour afficher le titre du jeu
        /// </summary>
        public static void AffichageTitre()
        {
            // On place le titre dans une chaine avec @ pour ne pas que les retourns à la ligne et les \ posent un probleme
            string message = @"
 .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
| .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
| |   ______     | || |     ____     | || |     ____     | || |    ______    | || |   _____      | || |  _________   | |
| |  |_   _ \    | || |   .'    `.   | || |   .'    `.   | || |  .' ___  |   | || |  |_   _|     | || | |_   ___  |  | |
| |    | |_) |   | || |  /  .--.  \  | || |  /  .--.  \  | || | / .'   \_|   | || |    | |       | || |   | |_  \_|  | |
| |    |  __'.   | || |  | |    | |  | || |  | |    | |  | || | | |    ____  | || |    | |   _   | || |   |  _|  _   | |
| |   _| |__) |  | || |  \  `--'  /  | || |  \  `--'  /  | || | \ `.___]  _| | || |   _| |__/ |  | || |  _| |___/ |  | |
| |  |_______/   | || |   `.____.'   | || |   `.____.'   | || |  `._____.'   | || |  |________|  | || | |_________|  | |
| |              | || |              | || |              | || |              | || |              | || |              | |
| '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
 '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 

                       ____                       _    _                _     _                     _
                      / __ \                     | |  (_)              | |   | |                   (_)
 _ __    __ _  _ __  | |  | | _   _   ___  _ __  | |_  _  _ __     ___ | |_  | |       ___   _   _  _  ___
| '_ \  / _` || '__| | |  | || | | | / _ \| '_ \ | __|| || '_ \   / _ \| __| | |      / _ \ | | | || |/ __|
| |_) || (_| || |    | |__| || |_| ||  __/| | | || |_ | || | | | |  __/| |_  | |____ | (_) || |_| || |\__ \
| .__/  \__,_||_|     \___\_\ \__,_| \___||_| |_| \__||_||_| |_|  \___| \__| |______| \___/  \__,_||_||___/
| |
|_|

";

            Console.WriteLine(message);
        }
    }
}
