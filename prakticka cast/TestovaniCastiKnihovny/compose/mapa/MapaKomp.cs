using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class MapaKomp
    {
        ChunkKomp[,] fujtajbl;
        public UIGrid GFX
        {
            get;private set;
        }
        public Mapa Mapa
        {
            get;private set;
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
            fujtajbl = chunky;
        }
        public void vykresli()
        {
            for (int x = 0; x < Mapa.X; x++)
            {
                for (int y = 0; y < Mapa.Y; y++)
                {
                    if (Mapa[x, y] != null)
                    {
                        GFX.SetBunku(fujtajbl[x, y].ObrChunku(), x, y);
                    }
                }
            }
        }
    }
}
