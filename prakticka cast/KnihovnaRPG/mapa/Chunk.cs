using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// blok lokací, umožňující, aby v paměti nebyla celá mapa současně
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
        #region podle start lokace
        /// <summary>
        /// vygeneruje lokace v chunku podle startovního a dovolených sousedů
        /// </summary>
        /// <param name="start">výchozí lokace pro generování</param>
        /// <param name="X">X souřadnice startu</param>
        /// <param name="Y">Y souřadnice startu</param>
        public virtual void Vygeneruj(Lokace start, int X, int Y)
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

                    vytvorPolicko(X - a, Y - b, rng);
                    vytvorPolicko(X + a, Y - b, rng);
                    vytvorPolicko(X - a, Y + b, rng);
                    vytvorPolicko(X + a, Y + b, rng);

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
            if (start > pul) { return rozmer - (start - pul); }

            return -1;
        }
        #endregion

        #region podle sousednich chunku
        /// <summary>
        /// vygeneruje lokace v chunku podle sousedního chunku a dovolených sousedů
        /// </summary>
        /// <param name="levo">chunk nalevo od tohoto</param>
        /// <param name="pravo">chunk naprovo od tohoto</param>
        /// <param name="nad">chunk nad tímto</param>
        /// <param name="pod">chunk pod tímto</param>
        public virtual void Vygeneruj(Chunk levo, Chunk pravo, Chunk nad, Chunk pod)
        {
            FOR A;
            FOR B;
            poradi p;
            urciSmer(out A, out B, out p, levo, pravo, nad, pod);
            Random rng = new Random();

            int a;
            int b;
            while (A.Iterace(out a))
            {
                while (B.Iterace(out b))
                {
                    int x, y;
                    if (p == poradi.XY) { AB(a, b, out x, out y); }
                    else { BA(a, b, out x, out y); }

                    vytvorPolicko(x, y, rng, levo, pravo, nad, pod);
                }
            }
        }
        private void vytvorPolicko(int X, int Y, Random rng, Chunk L, Chunk R, Chunk U, Chunk D)
        {
            if (vRozsahu(X, Y))
            {
                List<Lokace> mozne = prunikSousedu(X, Y, L, R, U, D);
                if (mozne != null)
                {
                    int r = rng.Next(mozne.Count);
                    lokace[X, Y] = mozne[r];
                }
            }
        }
        private List<Lokace> prunikSousedu(int x, int y, Chunk L, Chunk R, Chunk U, Chunk D)
        {
            Lokace levo, pravo, nad, pod;

            #region levo
            if (x - 1 >= 0)
            {
                levo = lokace[x - 1, y];
            }
            else
            {
                levo = L != null ? L[L.X - 1, y] : null;
            }
            #endregion
            #region pravo
            if (x + 1 < X)
            {
                pravo = lokace[x + 1, y];
            }
            else
            {
                pravo = R != null ? R[0, y] : null;
            }
            #endregion
            #region nad
            if (y - 1 >= 0)
            {
                nad = lokace[x, y - 1];
            }
            else
            {
                nad = U != null ? U[x, U.Y - 1] : null;
            }
            #endregion
            #region pod
            if (y + 1 < Y)
            {
                pod = lokace[x, y + 1];
            }
            else
            {
                pod = D != null ? D[x, 0] : null;
            }
            #endregion

            List<Lokace> ret = levo & pravo;
            ret = ret & nad;
            ret = ret & pod;

            return ret;
        }

        #region smer pruchodu
        void urciSmer(out FOR A, out FOR B, out poradi p, Chunk levo, Chunk pravo, Chunk nad, Chunk pod)
        {
            #region init
            bool L = levo != null;
            bool R = pravo != null;
            bool U = nad != null;
            bool D = pod != null;
            #endregion
            //chci začít v bodě kde je víc omezení
            #region 2 sousedi
            if (L && U)
            {
                A = new FOR(0, this.X, FOR.Smer.inkrement);
                B = new FOR(0, this.Y, FOR.Smer.inkrement);
                p = poradi.XY;
                return;
            }
            if (L && D)
            {
                A = new FOR(0, this.X, FOR.Smer.inkrement);
                B = new FOR(this.Y, 0, FOR.Smer.dekrement);
                p = poradi.XY;
                return;
            }
            if (R && U)
            {
                A = new FOR(this.X, 0, FOR.Smer.dekrement);
                B = new FOR(0, this.Y, FOR.Smer.inkrement);
                p = poradi.XY;
                return;
            }
            if (R && D)
            {
                A = new FOR(this.X, 0, FOR.Smer.dekrement);
                B = new FOR(this.Y, 0, FOR.Smer.dekrement);
                p = poradi.XY;
                return;
            }
            #endregion
            #region 1 soused
            if (L)
            {
                A = new FOR(0, this.X, FOR.Smer.inkrement);
                B = new FOR(0, this.Y, FOR.Smer.inkrement);
                p = poradi.XY;
                return;
            }
            if (R)
            {
                A = new FOR(this.X, 0, FOR.Smer.dekrement);
                B = new FOR(0, this.Y, FOR.Smer.inkrement);
                p = poradi.XY;
                return;
            }
            if (U)
            {
                A = new FOR(0, this.X, FOR.Smer.inkrement);
                B = new FOR(this.Y, 0, FOR.Smer.dekrement);
                p = poradi.YX;
                return;
            }
            if (D)
            {
                A = new FOR(0, this.X, FOR.Smer.inkrement);
                B = new FOR( this.Y,0, FOR.Smer.dekrement);
                p = poradi.XY;
                return;
            }
            #endregion

            //něco se pokazilo
            A = new FOR(-1, -1, FOR.Smer.inkrement);
            B = new FOR(-1, -1, FOR.Smer.dekrement);
            p = poradi.XY;
        }
        enum poradi { XY, YX }
        void AB(int a, int b, out int x, out int y)
        {
            x = a;
            y = b;
        }
        void BA(int a, int b, out int x, out int y)
        {
            x = b;
            y = a;
        }
        #endregion

        #endregion

        #region static

        /// <summary>
        /// vygeneruje nový chunku podle startovní lokace
        /// </summary>
        /// <param name="X">rozměr X</param>
        /// <param name="Y">rozměr Y</param>
        /// <param name="start">výchozí lokace pro generování</param>
        /// <param name="xs">X souřadnice startu</param>
        /// <param name="ys">Y souřadnice startu</param>
        /// <returns></returns>
        public static Chunk Vygeneruj(int X, int Y, Lokace start, int xs, int ys)
        {
            Chunk ret = new Chunk(X, Y);
            ret.Vygeneruj(start, xs, ys);
            return ret;
        }

        /// <summary>
        /// vygeneruje lokace v chunku podle sousedního chunku a dovolených sousedů
        /// </summary>
        /// <param name="X">rozměr X</param>
        /// <param name="Y">rozměr Y</param>
        /// <param name="levo">chunk nalevo od tohoto</param>
        /// <param name="pravo">chunk naprovo od tohoto</param>
        /// <param name="nad">chunk nad tímto</param>
        /// <param name="pod">chunk pod tímto</param>
        public static Chunk Vygeneruj(int X, int Y, Chunk levo, Chunk pravo, Chunk nad, Chunk pod)
        {
            Chunk ret = new Chunk(X, Y);
            ret.Vygeneruj(levo, pravo, nad, pod);
            return ret;
        }
        #endregion

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

        /// <summary>
        /// string sloužící k ukládání vygenerovaného chunku
        /// </summary>
        public virtual string SaveStream()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    sb.Append(lokace[x,y].Symbol());
                    //při refaktorizaci nahradit symbol ID
                }
            }

            return sb.ToString();
        }
    }

    class FOR
    {
        public enum Smer { inkrement, dekrement };

        int stav;
        int start;
        int stop;
        Smer podminka;

        public FOR(int start, int stop, Smer podminka)
        {
            this.start = start;
            this.stop = stop;
            this.podminka = podminka;

            //1. iterace vrací to co by vrátila deklarace -> potřebuji o průchod víc
            if (podminka == Smer.inkrement)
            {
                this.start--;
            }
            else
            {
                this.stop--;
            }

            stav = this.start;
        }

        bool Inkrementace()
        {
            stav++;
            bool ret = stav < stop;
            if (!ret) { stav = start; }
            return ret;
        }
        bool Dekrementace()
        {
            stav--;
            bool ret = stav > stop;
            if (!ret) { stav = start; }
            return ret;
        }

        public bool Iterace(out int stav)
        {
            if (podminka == Smer.inkrement)
            {
                bool ret = Inkrementace();
                stav = this.stav;
                return ret;
            }
            else
            {
                bool ret = Dekrementace();
                stav = this.stav;
                return ret;
            }
        }
    }
}
