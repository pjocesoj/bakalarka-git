using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;
using System.Drawing;

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
        public void Vygeneruj(LokaceGFX start, int X, int Y)
        {
            chunk.Vygeneruj(start, X, Y);

            vykresli();
        }
        public void Vygeneruj(ChunkKomp levo, ChunkKomp pravo, ChunkKomp nad, ChunkKomp pod)
        {
            Chunk L = levo!=null ? levo.Chunk : null;
            Chunk R = pravo != null ? pravo.Chunk : null;
            Chunk U = nad != null ? nad.Chunk : null;
            Chunk D = pod != null ? pod.Chunk : null;

            chunk.Vygeneruj(L,R,U,D);

            vykresli();
        }

        void vykresli()
        {
            for (int x = 0; x < chunk.X; x++)
            {
                for (int y = 0; y < chunk.Y; y++)
                {
                    if (chunk[x, y] != null)
                    {
                        grafika.SetBunku((chunk[x, y] as LokaceGFX).GFX, x, y);
                    }
                }
            }
        }

        public Bitmap ObrChunku()
        {
            Bitmap ret = new Bitmap(grafika.Width, grafika.Height);
            using (Graphics g = Graphics.FromImage(ret))
            {
                for (int y = 0; y < chunk.Y; y++)
                {
                    for (int x = 0; x < chunk.X; x++)
                    {
                        if (chunk[x, y] != null)
                        {
                            int X = x * grafika.RozmerBunka.X;
                            int Y = y * grafika.RozmerBunka.Y;
                            g.DrawImage((chunk[x, y] as LokaceGFX).GFX.grafika.Image, X, Y, grafika.RozmerBunka.X, grafika.RozmerBunka.Y);
                        }
                    }
                }
            }
            return ret;
        }

    }
}
