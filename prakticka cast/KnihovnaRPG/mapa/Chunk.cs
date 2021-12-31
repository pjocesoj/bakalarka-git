using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// blok lokací, umožňující, aby v paměti byla jen část mapy
    /// </summary>
    public class Chunk
    {
        Lokace[,] lokace;

        /// <summary>
        /// vytvoří chunk o velikosti X,Y
        /// </summary>
        /// <param name="x">rozměr X</param>
        /// <param name="y">rozměr Y</param>
        public Chunk(int x,int y)
        {
            lokace = new Lokace[x, y];

            Lokace l = new Lokace("test flyweight");
            lokace[0, 0] = l;
            lokace[0, 1] = l;
        }
    }
}
