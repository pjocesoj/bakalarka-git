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
        /// vytvoří prázdný chunk o velikosti X,Y
        /// </summary>
        /// <param name="x">rozměr X</param>
        /// <param name="y">rozměr Y</param>
        public Chunk(int x,int y)
        {
            lokace = new Lokace[x, y];
        }

        /// <summary>
        /// vytvoří chunk o velikosti X,Y vyplněný lokacemi
        /// </summary>
        /// <param name="X">rozměr X</param>
        /// <param name="Y">rozměr Y</param>
        /// <param name="lokace">lokace k překopírování do chunku</param>
        public Chunk(int X, int Y,Lokace[,] lokace):this(X,Y)
        {
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    this.lokace[x, y] = lokace[x, y];
                }
            }
        }

        /// <summary>
        /// vygeneruje lokace v chunku podle startovního a dovolených sousedů
        /// </summary>
        /// <param name="start">výchozí lokace pro generování</param>
        /// <param name="X">X souřadnice startu</param>
        /// <param name="Y">Y souřadnice startu</param>
        public virtual void vygeneruj(Lokace start, int X, int Y)
        {
            lokace[X, Y] = start;
        }
    }
}
