using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class LokaceKomp
    {
        protected GFX grafika;
        protected Lokace lokace;

        public LokaceKomp (GFX gfx, Lokace lokace)
        {
            grafika = gfx;
            this.lokace = lokace;
        }

        public GFX GFX
        {
            get { return grafika; }
        }
        public Lokace Lokace
        {
            get { return lokace; }
        }

        public override string ToString()
        {
            return lokace.ToString();
        }
    }
}
