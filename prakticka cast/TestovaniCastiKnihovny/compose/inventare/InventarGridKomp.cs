using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;
using System.Drawing;

namespace TestovaniCastiKnihovny
{
    class InventarGridKomp : InventarKompV2
    {
        int sloupcu;
        int radku;
        public InventarGridKomp(int kapacita, int radku, int sloupcu, int left = 0, int top = 0, int vyskaBunky = 100, int sirkaBunky = 100, int okraj = 5)
        {
            grafika = new UIGrid(radku, sloupcu, left, top, vyskaBunky, sirkaBunky, okraj);
            invent = new InventarPocet(kapacita);

            this.sloupcu = sloupcu;
            this.radku = radku;
        }

        public override bool Pridej(IPredmet item)
        {
            bool ret = invent.Pridej(item);

            if (ret)
            {
                int i = (invent as InventarPocet).Neseno - 1;
                if (item.Stackovatelne)
                {
                    i = (invent as InventarPocet).IndexOfStack(item);
                }

                int x = i % sloupcu;
                int y = Y(i);
                (grafika as UIGrid).SetBunku((item as PredmetKomp).GFX, x, y);
                dopisPocet((item as PredmetKomp).GFX, (invent as InventarPocet).Pocet(i), x, y);
            }

            return ret;
        }

        public override void Odeber(PredmetKomp item)
        {
            invent.Odeber(item);

            int count = (invent as InventarPocet).Neseno;

            for (int y = 0; y < radku; y++)
            {
                for (int x = 0; x < sloupcu; x++)
                {
                    int i = (y * sloupcu) + x;

                    if (i < count)
                    {
                        dopisPocet((invent[i] as PredmetKomp).GFX, (invent as InventarPocet).Pocet(i), x, y);
                    }
                    else
                    {
                        (grafika as UIGrid).prazdny(x, y);
                    }
                }
            }
        }

        int Y(int i)
        {
            int y = 0;
            while (i >= sloupcu)
            {
                i -= sloupcu;
                y++;
            }
            return y;
        }

        void dopisPocet(Bitmap obr, int pocet, int x, int y)
        {
            using (Graphics g = Graphics.FromImage(obr))
            {
                g.DrawString($"{pocet}", new Font("Arial", 20), Brushes.Gray, 0.5f, 0.5f);
            }
            (grafika as UIGrid).SetBunku(obr, x, y);
        }
        void dopisPocet(GFX gfx, int pocet, int x, int y)
        {
            Bitmap bmp = new Bitmap(gfx.grafika.Image);
            dopisPocet(bmp, pocet, x, y);
        }

    }
}
