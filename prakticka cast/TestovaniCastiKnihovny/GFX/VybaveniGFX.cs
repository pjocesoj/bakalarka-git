using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class VybaveniGFX:Vybaveni
    {
        GFX grafika;
        public VybaveniGFX(string jmeno, int cena, double hmotnost, StatList statList, Bitmap obr, int sirka = 100, int vyska = 100) : base(jmeno, cena, hmotnost, statList)
        {
            grafika = new GFX(sirka, vyska);
            grafika.grafika.Image = obr;
        }
        public GFX GFX
        {
            get { return grafika; }
        }
    }
}
