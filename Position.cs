using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_boogle
{
    internal class Position
    {
        private int x;
        private int y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int X { get { return x; } }
        public int Y { get { return y; } }
    }
}
