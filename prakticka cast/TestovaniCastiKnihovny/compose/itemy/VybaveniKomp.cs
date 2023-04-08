using KnihovnaRPG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestovaniCastiKnihovny
{
    class VybaveniKomp : PredmetKomp
    {
        public VybaveniKomp(string jmeno, int cena, double hmotnost, StatList statList, Bitmap obr, int sirka = 100, int vyska = 100)
        {
            grafika = new GFX(sirka, vyska, obr);
            predmet = new Vybaveni(jmeno, cena, hmotnost, statList);
        }
    }
}
