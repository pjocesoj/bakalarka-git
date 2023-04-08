using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// mapa herního světa
    /// </summary>
    public class Mapa
    {
        Chunk[,] chunky;

        /// <summary>
        /// vytvoří prádnou mapu o velikosti X,Y
        /// </summary>
        /// <param name="x">rozměr X</param>
        /// <param name="y">rozměr Y</param>
        public Mapa(int x, int y)
        {
            chunky = new Chunk[x, y];
            vygenerovano = new bool[x, y];
        }

        /// <summary>
        /// vytvoří mapu zkopírováním existujího seznamu chunků
        /// </summary>
        /// <param name="X">rozměr X</param>
        /// <param name="Y">rozměr Y</param>
        /// <param name="chunky">chunky tvořící mapu</param>
        public Mapa(int X, int Y, Chunk[,] chunky) : this(X, Y)
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
        public Lokace this[int x1, int y1, int x2, int y2]
        {
            get
            {
                return chunky[x1, y1][x2, y2];
            }
        }

        /// <summary>
        /// vygeneruje startovní chunk a chunky v určeném raduisu
        /// </summary>
        /// <param name="start">startovní lokace</param>
        /// <param name="XL">X souřadnice startovní lokace v chunku</param>
        /// <param name="YL">Y souřadnice startovní lokace v chunku</param>
        /// <param name="Sx">X rozměr chunku</param>
        /// <param name="Sy">Y rozměr chunku</param>
        /// <param name="XC">X souřadnice startovního chunku</param>
        /// <param name="YC">Y souřadnice startovního chunku</param>
        /// <param name="radius">kolik chunků od startovního se má generovat</param>
        public void Vygeneruj(Lokace start, int XL, int YL, int Sx, int Sy, int XC, int YC, int radius)
        {
            chunky[XC, YC] = Chunk.Vygeneruj(Sx, Sy, start, XL, YL);

            for (int a = 1; a <= radius; a++)//[x+a;y]
            {
                for (int b = 1; b <= radius; b++)//[x;y+b]
                {
                    vytvorChunk(Sx, Sy, XC - a, YC);
                    vytvorChunk(Sx, Sy, XC + a, YC);

                    vytvorChunk(Sx, Sy, XC, YC - b);
                    vytvorChunk(Sx, Sy, XC, YC + b);

                    vytvorChunk(Sx, Sy, XC - a, YC - b);
                    vytvorChunk(Sx, Sy, XC + a, YC - b);
                    vytvorChunk(Sx, Sy, XC - a, YC + b);
                    vytvorChunk(Sx, Sy, XC + a, YC + b);
                }
            }

        }

        /// <summary>
        /// vygeneruje startovní chunk a chunky v určeném raduisu
        /// </summary>
        /// <param name="start">startovní lokace</param>
        /// <param name="lokace">souřadnice startovní lokace v chunku</param>
        /// <param name="velikost">rozměr chunku</param>
        /// <param name="chunk">souřadnice startovního chunku</param>      
        /// <param name="radius">kolik chunků od startovního se má generovat</param>
        public void Vygeneruj(Lokace start, Point lokace, Point velikost, Point chunk, int radius)
        {
            Vygeneruj(start, lokace.X, lokace.Y, velikost.X, velikost.Y, chunk.X, chunk.Y, radius);
        }

        /// <summary>
        /// vygeneruje nový chunk na základě svých sousedů
        /// </summary>
        /// <param name="Sx">X rozměr chunku</param>
        /// <param name="Sy">Y rozměr chunku</param>
        /// <param name="X">X souřadnice chunku</param>
        /// <param name="Y">Y souřadnice chunku</param>
        public void vytvorChunk(int Sx, int Sy, int X, int Y)
        {
            if (X >= 0 && X < this.X)
            {
                if (Y >= 0 && Y < this.Y)
                {
                    if (chunky[X, Y] == null)
                    {
                        Chunk L = (X - 1) > 0 ? chunky[X - 1, Y] : null;
                        Chunk R = (X + 1) < this.X ? chunky[X + 1, Y] : null;
                        Chunk U = (Y - 1) > 0 ? chunky[X, Y - 1] : null;
                        Chunk D = (Y + 1) < this.Y ? chunky[X, Y + 1] : null;

                        chunky[X, Y] = Chunk.Vygeneruj(Sx, Sy, L, R, U, D);
                        vygenerovano[X, Y] = true;
                    }
                }
            }
        }

        /// <summary>
        /// vypíše 2D char, kde chunk='C', null='-'
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    if (chunky[x, y] != null)
                    {
                        sb.Append("C");
                    }
                    else
                    {
                        sb.Append("-");
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// vygeneruje startovní chunk a chunky v určeném raduisu
        /// </summary>
        /// <param name="X">rozměr X</param>
        /// <param name="Y">rozměr Y</param>
        /// <param name="start">startovní lokace</param>
        /// <param name="lokace">souřadnice startovní lokace v chunku</param>
        /// <param name="velikostChunk">rozměr chunku</param>
        /// <param name="chunk">souřadnice startovního chunku</param>      
        /// <param name="radius">kolik chunků od startovního se má generovat</param>
        /// <returns></returns>
        public static Mapa Vygeneruj(int X, int Y, Lokace start, Point lokace, Point velikostChunk, Point chunk, int radius)
        {
            Mapa ret = new Mapa(X, Y);
            ret.Vygeneruj(start, lokace, velikostChunk, chunk, radius);
            return ret;
        }
        /// <summary>
        /// vygeneruje mapu podle MapaConfig
        /// </summary>
        /// <param name="conf">MapaConfig obsahující informace o generaci mapy</param>
        /// <returns></returns>
        public static Mapa Vygeneruj(MapaConfig conf)
        {
            Mapa ret = new Mapa(conf.Mapa.X, conf.Mapa.Y);
            Point4D spawn = conf.Spawn;
            ret.Vygeneruj(conf.SpawnLokace, spawn.CX, spawn.CY, conf.Chunk.X, conf.Chunk.Y, spawn.MX, spawn.MY, conf.RenderVzdalenost);
            return ret;
        }

        bool[,] vygenerovano;
        /// <summary>
        /// vrátí zda již tento chunk byl generován
        /// </summary>
        /// <param name="X">X souřadnice</param>
        /// <param name="Y">Y souřadnice</param>
        public bool Vygeneovano(int X, int Y)
        {
            return vygenerovano[X, Y];
        }

        /// <summary>
        /// string sloužící k ukládání vygenerované mapy
        /// </summary>
        public virtual string SaveStream()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    Chunk c = chunky[x, y];
                    if (c == null)
                    {
                        sb.Append("ø");
                    }
                    else
                    {
                        sb.Append(c.SaveStream());
                    }
                }
            }

            return sb.ToString();
        }
    }
}
