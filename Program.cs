using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WordCloudSharp;

namespace Projet_boogle
{
    internal class Program
    {
        /// <summary>
        /// Initialisation de la fonction random
        /// </summary>
        public static Random random = new Random();
        /// <summary>
        /// Permet de sécuriser la saisie
        /// </summary>
        /// <returns> Renvoie un int correspondant au nombre saisi </returns>
        public static int SaisieNombreSecur()
        {
            int result = 0;
            while (!int.TryParse(Console.ReadLine(), out result) || result <= 0) { }
            return result;
        }

        /// <summary>
        /// Fonction pour afficher le titre du jeu
        /// </summary>
        /// /// <returns> renvoie un string avec le message </returns>
        public static string AffichageTitre()
        {
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

            return message;
        }

        /// <summary>
        /// Fonction pour afficher le message de fin
        /// </summary>
        /// <returns> renvoie un string avec le message </returns>
        public static string AffichageFin()
        {
            string message = @"
 __  __                    _           _                                                  _
|  \/  |                  (_)         | |                                                (_)
| \  / |  ___  _ __   ___  _      ___ | |_      __ _  _   _     _ __   ___ __   __  ___   _  _ __
| |\/| | / _ \| '__| / __|| |    / _ \| __|    / _` || | | |   | '__| / _ \\ \ / / / _ \ | || '__|
| |  | ||  __/| |   | (__ | |   |  __/| |_    | (_| || |_| |   | |   |  __/ \ V / | (_) || || |
|_|  |_| \___||_|    \___||_|    \___| \__|    \__,_| \__,_|   |_|    \___|  \_/   \___/ |_||_|


";
            return message;

        }
        /// <summary>
        /// Fonction pour afficher le gagnant d'une partie et son score
        /// </summary>
        /// <param name="pseudo"></param> pseudo du gagnant de la partie
        /// <param name="score"></param> score du gagnant de la partie 
        /// <returns> renvoie un string avec le message </returns>
        public static string AffichageVictoire(string pseudo, int score)
        {
            string message = @"
__      __ _____   _____  _______   ____   _____  _____   ______      _____   ______
\ \    / /|_   _| / ____||__   __| / __ \ |_   _||  __ \ |  ____|    |  __ \ |  ____|
 \ \  / /   | |  | |        | |   | |  | |  | |  | |__) || |__       | |  | || |__
  \ \/ /    | |  | |        | |   | |  | |  | |  |  _  / |  __|      | |  | ||  __|
   \  /    _| |_ | |____    | |   | |__| | _| |_ | | \ \ | |____     | |__| || |____
    \/    |_____| \_____|   |_|    \____/ |_____||_|  \_\|______|    |_____/ |______|


    >>>>>>>>>>>>>>>>>>>>>>>>  " + pseudo + @"  <<<<<<<<<<<<<<<<<<<<<<<<
                                                                                    _
                                                                                   | |        _
  __ _ __   __  ___   ___     _   _  _ __      ___   ___   ___   _ __   ___      __| |  ___  (_)
 / _` |\ \ / / / _ \ / __|   | | | || '_ \    / __| / __| / _ \ | '__| / _ \    / _` | / _ \
| (_| | \ V / |  __/| (__    | |_| || | | |   \__ \| (__ | (_) || |   |  __/   | (_| ||  __/  _
 \__,_|  \_/   \___| \___|    \__,_||_| |_|   |___/ \___| \___/ |_|    \___|    \__,_| \___| (_)

    >>>>>>>>>>>>>>>>>>>>>>>>  " + score + @"  <<<<<<<<<<<<<<<<<<<<<<<<";
            return message;
        }
        /// <summary>
        /// Fonction pour afficher les règles
        /// </summary>
        /// <returns> renvoie un string avec le message </returns>
        public static string AffichageRegles()
        {
            string message = @"
Le jeu de Boggle est un jeu de société fascinant et stimulant qui met à l'épreuve les compétences en vocabulaire et en 
observation des joueurs. Bien que sa simplicité apparente puisse être trompeuse, Boggle offre une profondeur qui le 
rend addictif et difficile à maîtriser. Ce jeu a été créé en 1972 par Allan Turoff, un ingénieur et inventeur, et il 
est rapidement devenu un classique des jeux de mots. Son succès s'explique par sa capacité à captiver les joueurs de 
tout âge, grâce à son côté à la fois compétitif et convivial.

Les règles de base de Boggle sont relativement simples à comprendre, mais elles demandent une grande concentration et 
une certaine rapidité d'esprit. Voici un aperçu des règles essentielles du jeu :

    Grille de lettres : La grille de Boggle est composée de 16 cases, chacune contenant une lettre. Ces lettres sont 
    choisies aléatoirement et les joueurs peuvent les relier entre elles pour former des mots. La grille est un carré de
    taille initiale de 4x4, mais il est possible de la modifier dans les paramètres.

    Formation des mots : Les mots doivent être formés en reliant des lettres adjacentes. Les lettres peuvent être 
    reliées horizontalement, verticalement ou en diagonale. Il n'est pas permis d'utiliser une même case plus d'une fois
    pour un même mot. De plus, les mots doivent comporter au moins deux lettres pour être valides.

    Durée de la partie : Un chronomètre est utilisé pour limiter le temps de chaque ronde. Les joueurs ont un
    temps défini (1 minute initiallement, mais ce temps est aussi modifiable) pour trouver autant de mots que possible.

    Validité des mots : Les mots doivent être inscrits dans un dictionnaire standard (français ou anglais) pour être validés.
    Les noms propres, les abréviations et les mots étrangers ne sont pas autorisés.

    Comptage des points : Les points sont attribués en fonction des lettres qui composent le mot. A la manière d'un scrabble où
    plus une lettre est rare, plus elle rapportera de points. De plus un bonus est appliqué en fonction de la taille du mot.

    Fin de la partie : La partie se termine après un nombre défini de tours. Le joueur avec le plus de points à la fin de
    la partie est déclaré vainqueur.

Amusez vous bien et bonne chance !";

            return message;
        }


        /// <summary>
        /// Fonction provisoire
        /// </summary>
        /// <param name="words"></param>
        /// <param name="frequencies"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, int>> SortWordsByFrequency(List<string> words, List<int> frequencies)
        {
            List<KeyValuePair<string, int>> wordFrequencies = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < words.Count; i++)
            {
                wordFrequencies.Add(new KeyValuePair<string, int>(words[i], frequencies[i]));
            }
            wordFrequencies.Sort((a, b) => b.Value.CompareTo(a.Value));
            return wordFrequencies;
        }

        /// <summary>
        /// Fonction généré par intelligence artificielle, servant à generé un nuage de mot à partir
        /// d'une liste de mot, et d'une liste de fréquence (correspondant aux mots)
        /// </summary>
        /// <param name="words"></param>
        /// <param name="frequencies"></param>
        /// <returns></returns>
        public static Bitmap GenerateWordCloud(List<string> words, List<int> frequencies)
        {
            WordCloud wordCloud = new WordCloud(800, 600, false);
            System.Drawing.Image image = wordCloud.Draw(words, frequencies);
            return (Bitmap)image;
        }
    }
}
