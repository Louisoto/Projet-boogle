using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Projet_boogle
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        #region Test méthode Test_Plateau
        public void TestMethod1()
        {
            Plateau plateau = new Plateau(4);
            plateau.ElemPlateau(0, 0).Face_visible = 'A';
            plateau.ElemPlateau(1, 1).Face_visible = 'F';
            plateau.ElemPlateau(1, 0).Face_visible = 'F';
            plateau.ElemPlateau(2, 0).Face_visible = 'E';
            plateau.ElemPlateau(2, 1).Face_visible = 'C';
            plateau.ElemPlateau(3, 0).Face_visible = 'T';
            plateau.ElemPlateau(3, 1).Face_visible = 'I';
            plateau.ElemPlateau(2, 2).Face_visible = 'O';
            plateau.ElemPlateau(3, 2).Face_visible = 'N';
            plateau.ElemPlateau(2, 3).Face_visible = 'N';
            plateau.ElemPlateau(1, 3).Face_visible = 'E';
            plateau.ElemPlateau(0, 2).Face_visible = 'E';
            plateau.ElemPlateau(0, 1).Face_visible = 'Z';
            plateau.ElemPlateau(0, 3).Face_visible = 'Z';
            plateau.ElemPlateau(1, 2).Face_visible = 'Z';
            plateau.ElemPlateau(3, 3).Face_visible = 'Z';

            plateau.toStringCouleur();

            bool result = plateau.Test_Plateau("AFFECTIONNEE");
            Assert.AreEqual(result, true);
        }
        #endregion

        [TestMethod]
        #region Test méthode initialisationValLettres
        public void TestMethod2()
        {
            De.initialisationValLettres("francais");

            char[] lettresAttendu = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            int[] pointsAttendu = new int[] { 1, 3, 3, 2, 1, 4, 2, 4, 1, 8, 10, 1, 2, 1, 1, 3, 8, 1, 1, 1, 1, 4, 10, 10, 10, 10 };
            int[] probabiliteAttendu = new int[] { 9, 2, 2, 3, 15, 2, 2, 2, 8, 1, 1, 5, 3, 6, 6, 2, 1, 6, 6, 6, 6, 2, 1, 1, 1, 1 };

            CollectionAssert.AreEqual(lettresAttendu, De.Lettres);
            CollectionAssert.AreEqual(pointsAttendu, De.Point_lettres);
            CollectionAssert.AreEqual(probabiliteAttendu, De.Probabilite_lettre);

        }
        #endregion

        [TestMethod]
        #region Test méthode Dichotomie
        public void TestMethod3()
        {
            Dictionnaire dictionnaire = new Dictionnaire("francais");

            bool result_true = dictionnaire.Dichotomie("AVION");
            bool result_false = dictionnaire.Dichotomie("BOULGIBOULGA");

            Assert.IsTrue(result_true);
            Assert.IsFalse(result_false);
        }
        #endregion

        [TestMethod]
        #region Test méthode initialisationValLettres
        public void TestMethod4()
        {
            De.initialisationValLettres("francais");

            int score_1 = Joueur.Calcul_Score("AZIMBOLICOTINATIONNALISABLE");
            int score_2 = Joueur.Calcul_Score("KZKZXWKXZW");
            int score_3 = Joueur.Calcul_Score("EE");

            Assert.AreEqual(score_1, 559);
            Assert.AreEqual(score_2, 500);
            Assert.AreEqual(score_3, 2);
        }
        #endregion


        [TestMethod]
        #region Test méthode tri_rapide
        public void TestMethod5()
        {
            List<string> listeBazar = new List<string>
            {
                "TREMPES", "BRODEQUINS", "ACCOUDA", "DEPENDEZ", "EBLOUISSONS", "BIFFER",
                "SORGHOS", "EMANEES", "AVENTURE", "ENDORMIREZ", "BRUSQUE", "MISSIONS",
                "CONTESTAIENT", "SUBJUGUERONT", "BUVARD", "FRAGMENTERENT", "BEGONIAS",
                "PRESUPPOSERAIT", "ENONCE", "HARPONNIEZ", "DISCONVENEZ", "DENSITE", "TIMIDES",
                "AMENAIS", "DRESSEUSE", "LARMOYANT", "REEDITERA"
            };

            List<string> listeTriee = new List<string>
            {
                "ACCOUDA", "AMENAIS", "AVENTURE", "BEGONIAS", "BIFFER", "BRODEQUINS", "BRUSQUE",
                "BUVARD", "CONTESTAIENT", "DENSITE", "DEPENDEZ", "DISCONVENEZ", "DRESSEUSE",
                "EBLOUISSONS", "EMANEES", "ENDORMIREZ", "ENONCE", "FRAGMENTERENT", "HARPONNIEZ",
                "LARMOYANT", "MISSIONS", "PRESUPPOSERAIT", "REEDITERA", "SORGHOS", "SUBJUGUERONT",
                "TIMIDES", "TREMPES"
            };

            CollectionAssert.AreEqual(Dictionnaire.tri_rapide(listeBazar), listeTriee);
        }
        #endregion
    }
}
