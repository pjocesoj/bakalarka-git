using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestovaniCastiKnihovny
{
    class NastaveniOvladani:KnihovnaRPG.INastaveni
    {
        public Keys Nahoru { get; private set; }
        public Keys Dolu { get; private set; }
        public Keys Doleva { get; private set; }
        public Keys Doprava { get; private set; }

        public NastaveniOvladani(Keys nahoru, Keys dolu, Keys doleva, Keys doprava)
        {
            this.Nahoru = nahoru;
            this.Dolu = dolu;
            this.Doleva = doleva;
            this.Doprava = doprava;
        }
        public NastaveniOvladani() : this(Keys.W, Keys.S, Keys.A, Keys.D)
        { }

        public string Vypis()
        {
            string ret = $"OVLÁDÁNÍ\n\n{"nahoru",-8}={Nahoru}\n{"Dolu",-8}={Dolu}\n{"Doleva",-8}={Doleva}\n{"Doprava",-8}={Doprava}";
            return ret;
        }
    }
}
