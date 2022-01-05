using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class HracKomp:PostavaKomp
    {
        public HracKomp(GFX GFX, Hrac logika) : base(GFX, logika) { }
        public HracKomp(string jmeno, int lv, int HP, StatList statList, Bitmap obr, int sirka = 100, int vyska = 100)
        {
            grafika = new GFX(sirka, vyska, obr);
            postava = new Hrac(jmeno, lv, HP, statList);

            postava.Smrt += smrt;
        }
    }
}
