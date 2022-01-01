using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class ChunkKomp
    {
        protected UIGrid grafika;
        protected Chunk chunk;

        public ChunkKomp(UIGrid gfx, Chunk chunk)
        {
            grafika = gfx;
            this.chunk = chunk;
        }
        public ChunkKomp(int x, int y, int left = 0, int top = 0, int sirka = 100, int vyska = 100)
        {
            grafika = new UIGrid(y, x, left, top, sirka, vyska, 1);
            this.chunk = new Chunk(x, y);
        }

        public UIGrid GFX
        {
            get { return grafika; }
        }
        public Chunk Chunk
        {
            get { return chunk; }
        }

        public override string ToString()
        {
            return chunk.ToString();
        }
        //public void Vygeneruj(LokaceKomp start, int X, int Y, List<LokaceKomp> GFX)
        public void Vygeneruj(LokaceGFX start, int X, int Y)
        {
            chunk.vygeneruj(start, X, Y);

            for (int x = 0; x < chunk.X; x++)
            {
                for (int y = 0; y < chunk.Y; y++)
                {
                    if (chunk[x, y] != null)
                    {
                        /*GFX g = najdiGFX(chunk[x, y], GFX);
                        grafika.SetBunku(g, x, y);*/
                        grafika.SetBunku((chunk[x, y] as LokaceGFX).GFX, x, y);
                    }
                }
            }
        }
        GFX najdiGFX(Lokace lok, List<LokaceKomp> list)
        {
            foreach (LokaceKomp g in list)
            {
                if (g.Lokace == lok)
                {
                    return g.GFX;
                }
            }
            return null;
        }

    }
}
