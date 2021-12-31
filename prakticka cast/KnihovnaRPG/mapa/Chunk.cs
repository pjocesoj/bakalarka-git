using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    public class Chunk
    {
        Lokace[,] lokace;

        public Chunk(int x,int y)
        {
            lokace = new Lokace[x, y];

            Lokace l = new Lokace("test flyweight");
            lokace[0, 0] = l;
            lokace[0, 1] = l;
        }
    }
}
