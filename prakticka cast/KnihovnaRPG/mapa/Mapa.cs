using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    public class Mapa
    {
        Chunk[,] chunky;

        public Mapa(int x, int y)
        {
            chunky = new Chunk[x,y];
        }

        /// <summary>
        /// vytvoří mapu zkopírováním existujího seznamu chunků
        /// </summary>
        /// <param name="X">rozměr X</param>
        /// <param name="Y">rozměr Y</param>
        /// <param name="chunky">chunky tvořící mapu</param>
        public Mapa(int X, int Y, Chunk[,] chunky):this(X,Y)
        {
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    this.chunky[x, y] = chunky[x, y];
                }
            }
        }

        /// <summary>
        /// X romzměr mapy
        /// </summary>
        public int X
        {
            get
            {
                return chunky.GetLength(0);
            }
        }

        /// <summary>
        /// Y rozměr mapy
        /// </summary>
        public int Y
        {
            get
            {
                return chunky.GetLength(1);
            }
        }

        /// <summary>
        /// vrátí chunk na pozici X;Y
        /// </summary>
        /// <param name="x">X souřadnice</param>
        /// <param name="y">Y souřadnice</param>
        public Chunk this[int x, int y]
        {
            get
            {
                return chunky[x, y];
            }
        }
        
        /// <summary>
        /// vrátí lokaci na pozici [x2,y2] v chunku na pozici [x1,x2]
        /// </summary>
        /// <param name="x1">X souřadnice chunku</param>
        /// <param name="y1">Y souřadnice chunku</param>
        /// <param name="x2">X souřadnice lokace</param>
        /// <param name="y2">Y souřadnice lokace</param>
        public Lokace this[int x1, int y1,int x2,int y2]
        {
            get
            {
                return chunky[x1, y1][x2,y2];
            }
        }
    

    }
}
