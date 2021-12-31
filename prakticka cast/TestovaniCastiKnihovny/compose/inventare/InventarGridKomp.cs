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
        public InventarGridKomp(int kapacita, int radku, int sloupcu, int left = 0, int top = 0)
        {
            grafika = new UIGrid(radku, sloupcu, left, top);
            invent = new InventarPocet(kapacita);

            this.sloupcu = sloupcu;
        }

        public override bool Pridej(Sebratelne item)
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
            }

            return ret;
        }

        public override void Odeber(PredmetKomp item)
        {
            invent.Odeber(item);
            //(grafika as UIVypis).Text = invent.ToString();
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
    }
}
