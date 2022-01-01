using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    class Mapa
    {
        Chunk[,] chunky;

        public Mapa(int x, int y)
        {
            chunky = new Chunk[x,y];
        }
    }
}
