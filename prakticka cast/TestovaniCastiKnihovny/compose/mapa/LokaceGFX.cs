using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;
using System.Drawing;

namespace TestovaniCastiKnihovny
{
    class LokaceGFX:Lokace
    {
        GFX gfx;
        public LokaceGFX(string nazev, Bitmap obr, int sirka = 100,int vyska=100):base(nazev)
        {
            gfx = new GFX(sirka, vyska, obr);
            symbol = nazev[0];
        }
        public LokaceGFX(string nazev, Bitmap obr, char symbol, int sirka = 100, int vyska = 100) : base(nazev)
        {
            gfx = new GFX(sirka, vyska, obr);
            this.symbol = symbol;
        }

        public LokaceGFX(string nazev, List<LokaceGFX> sousedi, Bitmap obr,char symbol ,int sirka = 100, int vyska = 100) :base(nazev,cast(sousedi))
        {
            gfx = new GFX(sirka, vyska, obr);
            this.symbol = symbol;
        }
        public GFX GFX
        {
            get { return gfx; }
        }

        char symbol;
        public override char Symbol()
        {
            return symbol;
        }

        //z důvodu bezpečnosti C# neumožňuje přímý převod listu na list rodičů
        static List<Lokace> cast(List<LokaceGFX> orig)
        {
            List<Lokace> ret=new List<Lokace>();
            foreach (LokaceGFX l in orig)
            {
                ret.Add(l);
            }
            return ret;
        }
        public void ojeb()
        {
            this.MuzeSousedit.RemoveAt(0);
        }
    }
}
