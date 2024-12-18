using System;
using System.Collections.Generic;
using System.IO;
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
        private List<string> motsOrdreAlpha;
        #endregion

        #region Getters
        public List<string> MotsOrdreAlpha
        {
            get { return motsOrdreAlpha; }
        }
        #endregion

        #region Constructeur
        public Dictionnaire(string langue) { 
            this.langue = langue;
            this.mots = Recuperer_Dictionnaire(langue);
            this.motsOrdreAlpha = Recuperer_Dictionnaire_Alpha(langue);
        }
        #endregion

        #region Méthode
        public string toString()
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

        public bool Dichotomie(string mot)
        {
            int taille_mot = mot.Length;
            if (taille_mot >= 0 && taille_mot < mots.Count)
            {
                int index = Dichotomique(mots[taille_mot], mot, mots[taille_mot].Count - 1);
                return (index >= 0);
            }
            return false;
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
                string contenu = File.ReadAllText("../../" + fichier + ".txt");
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
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la lecture du fichier dictionnaire : " + ex.Message);
            }
            return dictionnaire;
        }

        public static List<string> Recuperer_Dictionnaire_Alpha(string fichier)
        {
            List<string> dictionnaire = new List<string>();
            List<string> mot_liste = new List<string>();
            try
            {
                string contenu = File.ReadAllText("../../" + fichier + ".txt");
                string[] mots = contenu.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

                mot_liste.AddRange(mots);

                dictionnaire = tri_fusion(mot_liste);      
            }
            catch (Exception ex)
            {
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
        private static List<List<string>> tri_taille(string[] tableau)
        {
            List<List<string>> dictionnaire = new List<List<string>>();

            for (int i = 0; i < tableau.Length; i++)
            {
                string mot = tableau[i];
                int index = mot.Length;
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
        private static List<string> tri_fusion(List<string> tableau)
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
        private static List<string> Fusionner(List<string> gauche, List<string> droite)
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
        /// Fonction recursive en diviser pour regner de recherche dichotomique, qui prend en entrée
        /// une liste de string, un mot à chercher, et la taille de la liste et qui va de maniere recursive
        /// chercher l'indice du mot dans ce tableau, si on ne le trouve pas alors on renvoie -1
        /// </summary>
        /// <param name="t"></param>
        /// <param name="elem"></param>
        /// <param name="fin"></param>
        /// <param name="debut"></param>
        /// <returns></returns>
        public static int Dichotomique(List<string> dicoMotXLettres, string elem, int fin, int debut = 0)
        {
            if (dicoMotXLettres == null || dicoMotXLettres.Count == 0 || debut > fin)
            {
                return -1;
            }

            int milieu = (debut + fin) / 2;

            if (dicoMotXLettres[milieu].CompareTo(elem) == 0) // on a trouvé le mot cherché
            {
                return milieu;
            }
            else if (dicoMotXLettres[milieu].CompareTo(elem) > 0) // Le mot cherché est avant
            {
                return Dichotomique(dicoMotXLettres, elem, milieu - 1, debut);
            }
            else // Le mot cherché est après
            {
                return Dichotomique(dicoMotXLettres, elem, fin, milieu + 1);
            }
        }

        public bool Existence(string chaineCaractères, int fin, int debut = 0)
        {
            if (this.motsOrdreAlpha == null || this.motsOrdreAlpha.Count == 0 || debut > fin)
            {
                return false;
            }
            int milieu = (debut + fin) / 2;
            for (int i = 0; i < chaineCaractères.Length; i++)
            {
                if (chaineCaractères[i] != this.motsOrdreAlpha[milieu][i])
                {
                    if (this.motsOrdreAlpha[milieu].CompareTo(chaineCaractères) > 0) // Le mot cherché est avant
                    {
                        return Existence(chaineCaractères, milieu - 1, debut);
                    }
                    else // Le mot cherché est après
                    {
                        return Existence(chaineCaractères, fin, milieu + 1);
                    }
                }
            }
            return true;
        }
        #endregion
    }
}
