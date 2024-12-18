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
        /// Fonction généré par inteligence artificielle, servant à generé un nuage de mot à partir
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
