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
        public Chunk(int x, int y)
        {
            lokace = new Lokace[x, y];
        }

        /// <summary>
        /// vytvoří chunk o velikosti X,Y vyplněný lokacemi
        /// </summary>
        /// <param name="X">rozměr X</param>
        /// <param name="Y">rozměr Y</param>
        /// <param name="lokace">lokace k překopírování do chunku</param>
        public Chunk(int X, int Y, Lokace[,] lokace) : this(X, Y)
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
        /// X rozměr chunku
        /// </summary>
        public int X
        {
            get
            {
                return lokace.GetLength(0);
            }
        }

        /// <summary>
        /// Y rozměr chunku
        /// </summary>
        public int Y
        {
            get
            {
                return lokace.GetLength(1);
            }
        }

        /// <summary>
        /// vrátí lokaci na pozici X;Y
        /// </summary>
        /// <param name="x">X souřadnice</param>
        /// <param name="y">Y souřadnice</param>
        public Lokace this[int x, int y]
        {
            get
            {
                return lokace[x, y];
            }
        }

        #region generovani
        /// <summary>
        /// vygeneruje lokace v chunku podle startovního a dovolených sousedů
        /// </summary>
        /// <param name="start">výchozí lokace pro generování</param>
        /// <param name="X">X souřadnice startu</param>
        /// <param name="Y">Y souřadnice startu</param>
        public virtual void vygeneruj(Lokace start, int X, int Y)
        {
            lokace[X, Y] = start;
            Random rng = new Random();

            int kx = keKraji(X, this.X);
            int ky = keKraji(Y, this.Y);

            for (int a = 1; a <= kx; a++)//[x+a;y]
            {
                for (int b = 1; b <= ky; b++)//[x;y+b]
                {
                    vytvorPolicko(X - a, Y, rng);
                    vytvorPolicko(X + a, Y, rng);

                    vytvorPolicko(X, Y - b, rng);
                    vytvorPolicko(X, Y + b, rng);

                    vytvorPolicko(X - a, Y-b, rng);
                    vytvorPolicko(X + a, Y-b, rng);
                    vytvorPolicko(X - a, Y+b, rng);
                    vytvorPolicko(X + a, Y+b, rng);

                }
            }
        }

        private List<Lokace> prunikSousedu(int x, int y)
        {
            Lokace levo = vRozsahu(x - 1, y) ? lokace[x - 1, y] : null; //if? true:false
            Lokace pravo = vRozsahu(x + 1, y) ? lokace[x + 1, y] : null;
            Lokace nad = vRozsahu(x, y - 1) ? lokace[x, y - 1] : null;
            Lokace pod = vRozsahu(x, y + 1) ? lokace[x, y + 1] : null;

            List<Lokace> ret = levo & pravo;
            ret = ret & nad;
            ret = ret & pod;

            return ret;
        }

        private void vytvorPolicko(int X, int Y, Random rng)
        {
            if (vRozsahu(X, Y))
            {
                List<Lokace> mozne = prunikSousedu(X, Y);
                if (mozne != null)
                {
                    int r = rng.Next(mozne.Count);
                    lokace[X, Y] = mozne[r];
                }
            }
        }

        bool vRozsahu(int x, int y)
        {
            if (x < 0 || x >= this.X) { return false; }
            if (y < 0 || y >= this.Y) { return false; }

            return true;
        }
        int keKraji(int start, int rozmer)
        {
            int pul = rozmer / 2;
            if (pul == start) { return pul; }
            if (pul > start) { return rozmer - start; }
            if (start > pul) { return -(start - rozmer); }

            return -1;
        }
        #endregion

        /// <summary>
        /// vypíše symboly lokací jako 2D blok char
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    sb.Append(lokace[x, y].Symbol());
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
