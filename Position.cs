using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Projet_boogle
{
    internal class Position
    {
        /*Cette classe sert à faciliter la fonction qui vérifie qu'un mot est présent sur le plateau (Test_Plateau)
        en permettant d'enregistrer des positions dans un format simplifié.*/

        #region Attribut
        private int x;
        private int y;
        #endregion

        #region Constructeur
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region Propriétés
        public int X { get { return x; } }
        public int Y { get { return y; } }
        #endregion

        public bool PosInvalide(Position[] posInvalides)
        {
            for (int i = 0; i < posInvalides.Length; i++)
            {

                if (posInvalides[i] != null && posInvalides[i].X == this.X && posInvalides[i].Y == this.Y)
                {

                    return true;
                }
            }

            return false;
        }
    }
}
