using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{

    class Lokace
    {
        public string Nazev { get; private set; }

        public Lokace(string nazev)
        {
            Nazev = nazev;
        }
    }
}
