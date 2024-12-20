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
            Thread.Sleep(3000);
            Console.Clear();

            //initialisation des options du jeu
            Jeu.DureeTimer_option = TimeSpan.FromSeconds(60);
            Jeu.NbJoueurs_option = 2;
            Jeu.NbTours_option = 2;
            Jeu.TaillePlateau_option = 4;
            Jeu.Langue_option = "francais";

            Jeu.MeilleursScores = new int[20];
            Jeu.NomJoueurMeilleursScores = new string[20];

            int choix = 0;
            while (choix != 6)
            {
                Console.WriteLine("Menu:\n" +
                    "1- Nouvelle partie\n" +
                    "2- Meilleurs scores\n" +
                    "3- Nuage de mots\n" +
                    "4- Règles du jeu\n" +
                    "5- Options\n" +
                    "6- Quitter le jeu\n\n" +
                    "Quel est votre choix ?");
                choix = Program.SaisieNombreSecur();
                Console.Clear();

                switch (choix)
                {
                    case 1:
                        Jeu jeu = new Jeu();
                        jeu.jouer();
                        break;
                    case 2:
                        if (Jeu.MeilleursScores[0] == 0)
                        {
                            Console.WriteLine("Il faut avoir deja jouer pour avoir des meilleurs scores...");
                            Console.WriteLine("\nAppuyez sur une touche pour quitter");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        Console.WriteLine("Le meilleur score est de " + Jeu.MeilleursScores[0] +
                                                  " réalisé par " + Jeu.NomJoueurMeilleursScores[0]);
                        for (int i = 1; i < Jeu.MeilleursScores.Length ; i++)
                        {
                            if (Jeu.MeilleursScores[i] != 0)
                            {
                                Console.WriteLine("Le " + (i+1) + "ème meilleur score est de " + Jeu.MeilleursScores[i] +
                                                  " réalisé par " + Jeu.NomJoueurMeilleursScores[i]);
                            }
                            else
                            {
                                break;
                            }                            
                        }
                        Console.WriteLine("\nAppuyez sur une touche pour quitter");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        if (Jeu.Mots_joueurs == null || Jeu.Mots_joueurs.Count == 0)
                        {
                            Console.WriteLine("Lancez une partie avant de pouvoir acceder à cette fonctionnalité");
                            break;
                        }

                        Console.WriteLine("Liste des joueurs disponibles :");
                        foreach (var pseudo in Jeu.Mots_joueurs.Keys)
                        {
                            Console.WriteLine("- " + pseudo);
                        }
                        Console.WriteLine("\nEntrez le nom du joueur dont vous souhaitez voir le nuage de mots :");
                        string pseudoJoueur = Console.ReadLine();

                        if (!Jeu.Mots_joueurs.ContainsKey(pseudoJoueur))
                        {
                            Console.WriteLine("Ce joueur n'existe pas encore");
                            break;
                        }

                        List<string> mots = Jeu.Mots_joueurs[pseudoJoueur];
                        if (mots.Count == 0)
                        {
                            Console.WriteLine("Il n'y a aucun mot pour former le nuage de mot");
                            break;
                        }

                        List<int> frequences = new List<int>();
                        int multiplicateur = 10; 
                        for (int i = 0; i< mots.Count; i++)
                        {
                            frequences.Add((mots.Count * multiplicateur) - (multiplicateur * i));
                        }


                        Console.WriteLine("\nUne fois que vous avez finis de regarder, quittez la fenetre");
                        Bitmap bitmapNuage = Program.GenerateWordCloud(mots, frequences);
                        WordCloudDisplay.Show(bitmapNuage);

                        Console.WriteLine("\nAppuyez sur une touche pour continuer");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        Console.WriteLine(Program.AffichageRegles());
                        Console.WriteLine("\nAppuyez sur une touche pour continuer");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 5:
                        int choix_option = 0;
                        while (choix_option != 6)
                        {
                            Console.WriteLine("Options actuelles:\n1-Taille plateau : " + Jeu.TaillePlateau_option +
                                "\n2-Temps timer : " + Jeu.DureeTimer_option +
                                "\n3-Nombre de tours : " + Jeu.NbTours_option +
                                "\n4-Nombre de joueurs : " + Jeu.NbJoueurs_option +
                                "\n5-Langue : " + Jeu.Langue_option +
                                "\n6-Sortir" +
                                "\nQue souhaitez-vous modifier ?");
                            choix_option = Program.SaisieNombreSecur();
                            Console.Clear();
                            switch (choix_option)
                            {
                                case 1:
                                    Console.WriteLine("Modification de la taille du plateau\nQuelle est la nouvelle taille ?");
                                    Jeu.TaillePlateau_option = Program.SaisieNombreSecur();
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
                    case 6:
                        Console.WriteLine(Program.AffichageFin());
                        Thread.Sleep(2000);
                        break;
                    default:
                        Console.WriteLine("Commande incorecte");
                        break;
                }
            }
        }
    }
}
