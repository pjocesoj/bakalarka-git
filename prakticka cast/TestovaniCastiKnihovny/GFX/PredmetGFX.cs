using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    public class PredmetGFX:Predmet
    {
        GFX grafika;
        public PredmetGFX(string jmeno, int cena, double hmotnost, Bitmap obr, int sirka = 100, int vyska = 100,bool stackovatelne = true) : base(jmeno, cena, hmotnost, stackovatelne)
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
