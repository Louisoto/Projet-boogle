using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WordCloudSharp;

using System.Threading.Tasks;

namespace Projet_boogle
{
    internal class main
    {
        static void Main(string[] args)
        {
            De.initialisationValLettres("francais");

            Console.WriteLine(Program.AffichageTitre());
            Thread.Sleep(500);
            Console.Clear();

            //initialisation des options du jeu
            Jeu.DureeTimer_option = TimeSpan.FromSeconds(60);
            Jeu.NbJoueurs_option = 2;
            Jeu.NbTours_option = 2;
            Jeu.Tailleplateau_option = 4;
            Jeu.Langue_option = "francais";

            Jeu.MeilleursScores = new int[20];
            Jeu.NomJoueurMeilleursScores = new string[20];

            int choix = 0;
            while (choix != 5)
            {
                Console.WriteLine("Menu:\n" +
                    "1- Nouvelle partie\n" +
                    "2- Meilleurs scores\n" +
                    "3- Nuage de mots\n" +
                    "4- Option\n" +
                    "5- Quitter le jeu\n\n" +
                    "Quel est votre choix ?");
                choix = Program.SaisieNombreSecur();
                Console.Clear();

                switch (choix)
                {
                    case 1:
                        //ici on deroule le jeu. Il faudrait fait une methode de jeu ou c'est "derouler jeu" et ça gere les manches etc
                        Jeu jeu = new Jeu();
                        jeu.jouer();
                        break;
                    case 2:
                        //on affiche les meilleurs scores
                        for (int i = 0; i < Jeu.MeilleursScores.Length; i++)
                        {
                            if (Jeu.MeilleursScores[i] != 0)
                            {
                                Console.WriteLine("Le " + i + "ème meilleur score est de " + Jeu.MeilleursScores[i] +
                                                  " réalisé par " + Jeu.NomJoueurMeilleursScores[i]);
                            }
                            else
                            {
                                Console.WriteLine("Aucun score enregistré");
                            }                            
                        }
                        Console.WriteLine("\nAppuyez sur une touche pour quitter");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        // Affichage du nuage de mots
                        List<string> words = new List<string> { "CLOUD", "PROGRAM", "WORD", "C#", "EXAMPLE", "LIBRARY", "VISUAL" };

                        // Calculer les fréquences en fonction des scores des mots
                        List<int> frequencies = new List<int>();
                        foreach (string word in words)
                        {
                            frequencies.Add(Joueur.Calcul_Score(word));
                        }

                        // Trier les mots et fréquences par ordre décroissant de fréquence
                        List<KeyValuePair<string, int>> wordFrequencies = Program.SortWordsByFrequency(words, frequencies);

                        // Mise à jour des listes triées
                        words = new List<string>();
                        frequencies = new List<int>();
                        foreach (var pair in wordFrequencies)
                        {
                            words.Add(pair.Key);
                            frequencies.Add(pair.Value);
                        }

                        // Générer et afficher le nuage de mots
                        Bitmap bitmap = Program.GenerateWordCloud(words, frequencies);
                        WordCloudDisplay.Show(bitmap);
                        break;
                    case 4:
                        int choix_option = 0;
                        while (choix_option != 6)
                        {
                            Console.WriteLine("Option actuelles:\n1-Taille plateau : " + Jeu.Tailleplateau_option +
                                "\n2-Temps timer : " + Jeu.DureeTimer_option +
                                "\n3-Nombre de tours : " + Jeu.NbTours_option +
                                "\n4-Nombre de joueur : " + Jeu.NbJoueurs_option +
                                "\n5-Langue : " + Jeu.Langue_option +
                                "\n6-Sortir" +
                                "\nQue souhaitez-vous modifier ?");
                            choix_option = Program.SaisieNombreSecur();
                            Console.Clear();
                            switch (choix_option)
                            {
                                case 1:
                                    Console.WriteLine("Modification de la taille du plateau\nQuelle est la nouvelle taille ?");
                                    Jeu.Tailleplateau_option = Program.SaisieNombreSecur();
                                    Console.Clear();
                                    break;
                                case 2:
                                    Console.WriteLine("Modification du timer\nQuelle est sa nouvelle durée (en secondes) ?");
                                    Jeu.DureeTimer_option = TimeSpan.FromSeconds(Program.SaisieNombreSecur());
                                    Console.Clear();
                                    break;
                                case 3:
                                    Console.WriteLine("Modification du nombre de tour\nCombien de tours souhaitez-vous ?");
                                    Jeu.NbTours_option = Program.SaisieNombreSecur();
                                    Console.Clear();
                                    break;
                                case 4:
                                    Console.WriteLine("Modification du nombre de joueur\nCombien de joueurs souhaitez-vous ?");
                                    Jeu.NbJoueurs_option = Program.SaisieNombreSecur();
                                    Console.Clear();
                                    break;
                                case 5:
                                    Console.WriteLine("Modification de la langue");
                                    string langue;
                                    do
                                    {
                                        Console.WriteLine("Quelle langue souhaitez-vous (anglais ou francais) ?");
                                        langue = Console.ReadLine();
                                    } while (langue != "anglais" && langue != "francais");
                                    Jeu.Langue_option = langue;
                                    Console.Clear();
                                    break;
                                case 6:
                                    Console.WriteLine("Sortie des options");
                                    break;
                                default:
                                    Console.WriteLine("Mauvaise manipulation");
                                    break;
                            }
                        }
                        break;
                    case 5:
                        Console.WriteLine(Program.AffichageFin());
                        Thread.Sleep(3000);
                        break;
                    default:
                        Console.WriteLine("Commande incorecte");
                        break;
                }
            }
        }
    }
}
