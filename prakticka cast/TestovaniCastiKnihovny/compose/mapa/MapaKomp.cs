using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class MapaKomp
    {
        public UIGrid GFX
        {
            get;private set;
        }
        public Mapa Mapa
        {
            get;private set;
        }
        public MapaKomp(Mapa mapa, int left = 0, int top = 0, int sirka = 100, int vyska = 100)
        {
            GFX = new UIGrid(mapa.X,mapa.Y, left, top, sirka, vyska, 1);
            Mapa = mapa;
        }
        public MapaKomp(int x, int y, int left = 0, int top = 0, int sirka = 100, int vyska = 100)
        {
            GFX = new UIGrid(y, x, left, top, sirka, vyska, 1);
            Mapa = new Mapa(x, y);
        }
        public MapaKomp(int x, int y,ChunkKomp[,]chunky, int left = 0, int top = 0, int sirka = 100, int vyska = 100)
        {
            GFX = new UIGrid(y, x, left, top, sirka, vyska, 1);

            Chunk[,] temp = new Chunk[x, y];
            for (int X = 0; X < x; X++)
            {
                for (int Y = 0; Y < y; Y++)
                {
                    if (chunky[X, Y] != null)
                    {
                        temp[X, Y] = chunky[X, Y].Chunk;
                    }
                }
            }
            Mapa = new Mapa(x, y,temp);
        }
        public void vykresli()
        {
            for (int x = 0; x < Mapa.X; x++)
            {
                for (int y = 0; y < Mapa.Y; y++)
                {
                    if (Mapa[x, y] != null)
                    {
                        ChunkKomp temp = new ChunkKomp(null, Mapa[x, y]);
                        GFX.SetBunku(temp.ObrChunku(GFX.RozmerBunka.X,GFX.RozmerBunka.Y), x, y);
                    }
                }
            }
        }

        public void Vygeneruj(Lokace start, int XL, int YL, int Sx, int Sy, int XC, int YC, int radius)
        {
            this.Mapa.Vygeneruj(start, XL, YL, Sx, Sy, XC, YC, radius);
        }

        public Bitmap ObrMapy(int w, int h)
        {
            Bitmap ret = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(ret))
            {
                for (int y = 0; y < Mapa.Y; y++)
                {
                    for (int x = 0; x < Mapa.X; x++)
                    {
                        if (Mapa[x, y] != null)
                        {
                            int bx = w / Mapa.X;
                            int by = h / Mapa.Y;
                            int X = x * bx;
                            int Y = y * by;

                            ChunkKomp temp = new ChunkKomp(null, Mapa[x, y]);
                            g.DrawImage(temp.ObrChunku(bx, by), X, Y, bx, by);
                        }
                    }
                }
            }
            return ret;
        }
    }
}
