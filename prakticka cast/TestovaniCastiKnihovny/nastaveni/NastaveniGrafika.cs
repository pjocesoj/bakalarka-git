using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestovaniCastiKnihovny
{
    class NastaveniGrafika:KnihovnaRPG.INastaveni
    {
        public int Sirka { get; private set; }
        public int Vyska { get; private set; }

        public NastaveniGrafika(int w, int h)
        {
            Sirka = w;
            Vyska = h;
        }
        public string Vypis()
        {
            string ret = $"GRAFIKA\n\n{"sirka",-8}={Sirka}\n{"vyska",-8}={Vyska}";
            return ret;
        }
    }
}
