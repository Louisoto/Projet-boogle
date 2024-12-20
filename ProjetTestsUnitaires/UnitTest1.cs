using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Projet_boogle
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
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
    }
}
