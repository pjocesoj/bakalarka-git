using KnihovnaRPG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestovaniCastiKnihovny
{
    class KonzumovatelneKomp:PredmetKomp
    {
        public KonzumovatelneKomp(string jmeno, int cena, double hmotnost, StatList boosty, Bitmap obr, int sirka = 100, int vyska = 100)
        {
            grafika = new GFX(sirka, vyska, obr);
            predmet = new Konzumovatelne(jmeno, cena, hmotnost, boosty);
        }
    }
}
