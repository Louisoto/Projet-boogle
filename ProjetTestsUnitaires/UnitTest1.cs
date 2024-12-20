using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
        #region louis
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
    }
}
